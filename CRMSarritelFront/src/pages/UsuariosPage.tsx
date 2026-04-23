import { useState, useMemo, useCallback } from "react";
import { motion } from "framer-motion";
import { Search, Plus, UserCheck, UserX, Users, Trash2, CheckSquare, ShieldAlert, Edit, File as FileIcon, Upload, LayoutList, Columns3, FilterX, Mail, Building2, Briefcase, ArrowRight, Phone } from "lucide-react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { StatusBadge } from "@/components/shared/StatusBadge";
import { ResponsiveModal } from "@/components/shared/ResponsiveModal";
import { EmptyState } from "@/components/shared/EmptyState";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import { useIsMobile } from "@/hooks/use-mobile";
import { Separator } from "@/components/ui/separator";
import { KpiCard } from "@/components/shared/KpiCard";
import { useUsuarios } from "@/hooks/useUsuarios";
import { useRoles } from "@/hooks/useRoles";
import { useAuth } from "@/hooks/useAuth";
import { Usuario } from "@/types";
import { EntityFilesModal } from "@/components/shared/EntityFilesModal";
import { UsuarioFormModal } from "@/components/usuarios/UsuarioFormModal";
import { toast } from "sonner";
import { BulkImportModal } from "@/components/shared/BulkImportModal";
import { Checkbox } from "@/components/ui/checkbox";
import { BulkActionsBar } from "@/components/shared/BulkActionsBar";
import { getInsensitive } from "@/lib/import-utils";
import { cn } from "@/lib/utils";
import { UnifiedSearchBar } from "@/components/shared/UnifiedSearchBar";
import { FilterField, type FilterOption } from "@/components/shared/FilterField";
import React from 'react';

// Memoized table row for Users
const UserRow = React.memo(({ 
  usr, 
  index, 
  selectionMode, 
  isSelected, 
  onSelect, 
  onClick, 
  isLargeList,
  formatAmount
}: { 
  usr: Usuario, 
  index: number, 
  selectionMode: boolean, 
  isSelected: boolean, 
  onSelect: (id: number, selected: boolean) => void,
  onClick: (u: Usuario) => void,
  isLargeList: boolean,
  formatAmount: (n: number) => string
}) => {
  return (
    <motion.tr 
      initial={isLargeList ? false : { opacity: 0 }} 
      animate={isLargeList ? false : { opacity: 1 }} 
      transition={isLargeList ? { duration: 0 } : { delay: Math.min(index * 0.01, 0.5) }} 
      className={cn(
        "border-b border-border hover:bg-muted/50 cursor-pointer transition-colors",
        isSelected && "bg-primary/5"
      )}
      onClick={() => selectionMode ? null : onClick(usr)}
    >
      {selectionMode && (
        <td className="p-4" onClick={(e) => e.stopPropagation()}>
          <Checkbox 
            checked={isSelected}
            onCheckedChange={(checked) => onSelect(usr.id, !!checked)}
          />
        </td>
      )}
      <td className="p-4">
        <div className="flex items-center gap-3">
            <div className="h-8 w-8 rounded-full bg-primary/10 flex items-center justify-center text-primary font-bold text-xs shrink-0">
                {usr.nombre.charAt(0)}
            </div>
            <div>
                <p className="font-semibold text-foreground leading-tight">{usr.nombre}</p>
                <p className="text-[10px] text-muted-foreground">{usr.email || "—"}</p>
            </div>
        </div>
      </td>
      <td className="p-4 text-muted-foreground">{usr.departamento || "—"}</td>
      <td className="p-4 text-muted-foreground">{usr.puesto || "—"}</td>
      <td className="p-4">
        <span className="text-[10px] bg-muted border border-border px-2 py-0.5 rounded text-muted-foreground font-medium uppercase tracking-wider">
          {usr.rol_Nombre || "Sin Rol"}
        </span>
      </td>
      <td className="p-4 font-mono font-medium text-foreground">{usr.salarioBase ? formatAmount(usr.salarioBase) : "—"}</td>
      <td className="p-4"><StatusBadge status={usr.activo ? "Activo" : "Inactivo"} /></td>
    </motion.tr>
  );
});

const roleTeamSchema = z.object({
  departamento: z.string().optional(),
  puesto: z.string().optional(),
  rolId: z.string().min(1, "Debe seleccionar un rol"),
});

export default function UsuariosPage() {
  const { user, hasPermission } = useAuth();
  const isAdmin = user?.roles?.includes("Admin") || user?.roles?.includes("SuperAdmin");
  
  const { data: usuarios = [], isLoading, createUsuario, updateUsuario, deleteUsuario } = useUsuarios();
  const { data: roles = [], isLoading: rolesLoading } = useRoles();
  
  const [selectedIds, setSelectedIds] = useState<Set<number>>(new Set());
  const [selectionMode, setSelectionMode] = useState(false);
  const [view, setView] = useState<"table" | "grid">("table");
  const [search, setSearch] = useState("");
  const [rolFilter, setRolFilter] = useState("all");
  const [estadoFilter, setEstadoFilter] = useState("all");
  const [selectedUsuario, setSelectedUsuario] = useState<Usuario | null>(null);
  const [editingRole, setEditingRole] = useState(false);
  const [filesModalOpen, setFilesModalOpen] = useState(false);
  const [userModalOpen, setUserModalOpen] = useState(false);
  const [importModalOpen, setImportModalOpen] = useState(false);
  const [userToEdit, setUserToEdit] = useState<Usuario | null>(null);
  const [displayLimit, setDisplayLimit] = useState(50);
  
  const form = useForm<z.infer<typeof roleTeamSchema>>({
    resolver: zodResolver(roleTeamSchema),
    defaultValues: { departamento: "", puesto: "", rolId: "" },
  });
  const isMobile = useIsMobile();
  const formatAmount = useCallback((n: number) => `${n.toLocaleString("es-ES")} €`, []);

  const filtered = useMemo(() => {
    return usuarios.filter(usr => {
      const matchSearch = search === "" || 
        usr.nombre.toLowerCase().includes(search.toLowerCase()) || 
        (usr.email || "").toLowerCase().includes(search.toLowerCase());
      
      const matchRol = rolFilter === "all" || usr.rol_Nombre === rolFilter;
      const matchEstado = estadoFilter === "all" || (estadoFilter === "activo" ? usr.activo : !usr.activo);

      return matchSearch && matchRol && matchEstado;
    });
  }, [search, usuarios, rolFilter, estadoFilter]);

  const activos = useMemo(() => usuarios.filter(c => c.activo).length, [usuarios]);

  const kpis = useMemo(() => [
    { title: "Usuarios Activos", value: String(activos), icon: UserCheck, sparkData: [2, 3, 3, 3, 4, 4, 4] },
    { title: "Inactivos", value: String(usuarios.length - activos), icon: UserX, sparkData: [1, 1, 0, 1, 0, 1, 1] },
    { title: "Personal Total", value: String(usuarios.length), icon: Users, sparkData: [3, 4, 5, 5, 6, 7, 7] },
  ], [activos, usuarios.length]);

  const handleSelect = useCallback((id: number, selected: boolean) => {
    setSelectedIds(prev => {
      const next = new Set(prev);
      if (selected) next.add(id);
      else next.delete(id);
      return next;
    });
  }, []);

  const handleSelectAll = useCallback((checked: boolean) => {
    if (checked) setSelectedIds(new Set(filtered.map(u => u.id)));
    else setSelectedIds(new Set());
  }, [filtered]);

  const handleBulkDelete = async () => {
    if (selectedIds.size === 0) return;
    if (!confirm(`¿Estás seguro de que deseas eliminar ${selectedIds.size} empleados?`)) return;

    const idsArray = Array.from(selectedIds);
    const toastId = toast.loading(`Eliminando ${idsArray.length} usuarios... (0%)`);
    try {
      const chunkSize = 15;
      const total = idsArray.length;
      
      for (let i = 0; i < total; i += chunkSize) {
        const chunk = idsArray.slice(i, i + chunkSize);
        await Promise.all(chunk.map(id => deleteUsuario(id)));
        const progress = Math.min(100, Math.round(((i + chunkSize) / total) * 100));
        toast.loading(`Eliminando ${idsArray.length} usuarios... (${progress}%)`, { id: toastId });
      }
      
      setSelectedIds(new Set());
      setSelectionMode(false);
      toast.success("Usuarios eliminados correctamente", { id: toastId });
    } catch (error) {
      toast.error("Error en la eliminación masiva", { id: toastId });
    }
  };

  const handleUserClick = useCallback((u: Usuario) => setSelectedUsuario(u), []);

  if (isLoading) {
    return (
      <div className="space-y-6">
        <div>
          <h1 className="text-2xl font-bold text-foreground">Usuarios y Roles</h1>
          <p className="text-sm text-muted-foreground font-medium animate-pulse">Cargando personal...</p>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
          {[1, 2, 3].map(i => <div key={i} className="h-32 glass animate-pulse rounded-xl" />)}
        </div>
        <div className="h-64 glass animate-pulse rounded-xl" />
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-20 sm:pb-6">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl font-bold text-foreground">Usuarios y Roles</h1>
          <p className="text-sm text-muted-foreground">Gestión de personal y roles operativos</p>
        </div>
        <div className="flex items-center gap-2 w-full sm:w-auto">
          <div className="flex items-center gap-2">
            {hasPermission("usuarios:create") && (
              <Button variant="outline" size="icon" className="border-border bg-background" onClick={() => setImportModalOpen(true)} title="Importar CSV/Excel">
                <Upload className="h-4 w-4" />
              </Button>
            )}
            <div className="hidden sm:flex items-center bg-muted/30 p-1 rounded-lg border border-border mr-2">
              <Button 
                variant={view === 'table' ? 'secondary' : 'ghost'} 
                size="icon" 
                className="h-7 w-7 rounded-sm" 
                onClick={() => setView('table')}
                title="Vista de Tabla"
              >
                <LayoutList className="h-4 w-4" />
              </Button>
              <Button 
                variant={view === 'grid' ? 'secondary' : 'ghost'} 
                size="icon" 
                className="h-7 w-7 rounded-sm" 
                onClick={() => setView('grid')}
                title="Vista de Widgets"
              >
                <Columns3 className="h-4 w-4" />
              </Button>
            </div>
            {hasPermission("usuarios:create") && (
              <Button className="bg-primary text-primary-foreground hover:bg-primary/90 gap-2 flex-1 sm:flex-none font-bold shadow-glow" onClick={() => { setUserToEdit(null); setUserModalOpen(true); }}>
                <Plus className="h-4 w-4" /> {isMobile ? "Nuevo" : "Nuevo Empleado"}
              </Button>
            )}
          </div>
        </div>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {kpis.map((kpi, i) => (<KpiCard key={kpi.title} {...kpi} index={i} />))}
      </div>

      <UnifiedSearchBar
        selectionMode={selectionMode}
        onSelectionModeToggle={hasPermission("usuarios:delete") ? () => {
          setSelectionMode(!selectionMode);
          if (selectionMode) setSelectedIds(new Set());
        } : undefined}
        bulkActions={
          <div className="flex items-center gap-3 w-full">
            <span className="text-xs font-bold text-primary bg-primary/10 px-2 py-1 rounded-md border border-primary/20">
              {selectedIds.size} seleccionados
            </span>
            <div className="h-4 w-px bg-border/40 mx-1" />
            {hasPermission("usuarios:delete") && (
              <Button 
                variant="destructive" 
                size="sm" 
                className="h-8 gap-2 shadow-sm" 
                disabled={selectedIds.size === 0}
                onClick={handleBulkDelete}
              >
                <Trash2 className="h-3.5 w-3.5" /> Eliminar Registros
              </Button>
            )}
          </div>
        }
        searchValue={search}
        onSearchChange={setSearch}
        searchPlaceholder="Buscar empleado por nombre, email o rol..."
        onClearFilters={() => {
          setSearch('');
          setRolFilter('all');
          setEstadoFilter('all');
        }}
        showFilterBadge={rolFilter !== 'all' || estadoFilter !== 'all'}
      >
        <div className="flex flex-wrap items-end gap-4 w-full">
          <FilterField 
            columnLabel="Rol"
            type="select"
            value={rolFilter}
            onChange={setRolFilter}
            options={[
              { value: "SuperAdmin", label: "Super Admin" },
              { value: "Admin", label: "Administrador" },
              { value: "Supervisor", label: "Supervisor" },
              { value: "Vendedor", label: "Vendedor" }
            ]}
            placeholder="Todos los roles"
          />

          <FilterField 
            columnLabel="Estado"
            type="select"
            value={estadoFilter}
            onChange={setEstadoFilter}
            options={[
              { value: "activo", label: "Activo" },
              { value: "inactivo", label: "Inactivo" }
            ]}
            placeholder="Cualquier estado"
          />
        </div>
      </UnifiedSearchBar>

      {filtered.length === 0 ? (
        <EmptyState icon={Users} title="Sin resultados" description="No se encontraron usuarios activos en la plataforma." />
      ) : (view === "grid" || isMobile) ? (
        <div className="space-y-4">
            <div className="grid gap-3 sm:grid-cols-2 lg:grid-cols-3">
            {filtered.slice(0, displayLimit).map((usr, i) => (
                <motion.div key={usr.id} initial={{ opacity: 0, y: 10 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: Math.min(i * 0.05, 0.5) }} className="glass glass-hover rounded-xl p-4 cursor-pointer relative flex flex-col group" onClick={() => selectionMode ? null : handleUserClick(usr)}>
                <div className="flex justify-between items-start mb-3">
                    <div className="flex items-center gap-3">
                    {selectionMode && (
                        <Checkbox 
                        checked={selectedIds.has(usr.id)} 
                        onCheckedChange={(checked) => handleSelect(usr.id, !!checked)}
                        onClick={(e) => e.stopPropagation()}
                        />
                    )}
                        <div className="h-10 w-10 rounded-full bg-primary/10 flex items-center justify-center text-primary font-bold text-sm shrink-0">
                            {usr.nombre.charAt(0)}
                        </div>
                        <div>
                            <h3 className="font-bold text-sm text-foreground leading-tight group-hover:text-primary transition-colors">{usr.nombre}</h3>
                            <p className="text-[10px] text-muted-foreground truncate max-w-[120px]">{usr.email}</p>
                        </div>
                    </div>
                    <StatusBadge status={usr.activo ? "Activo" : "Inactivo"} />
                </div>
                
                    <div className="grid grid-cols-2 gap-2 mt-2 mb-4">
                        <div className="flex items-center gap-1.5 text-[11px] text-muted-foreground">
                            <Building2 className="h-3 w-3 shrink-0" /> <span className="truncate">{usr.departamento || "N/A"}</span>
                        </div>
                        <div className="flex items-center gap-1.5 text-[11px] text-muted-foreground">
                            <Briefcase className="h-3 w-3 shrink-0" /> <span className="truncate">{usr.puesto || "N/A"}</span>
                        </div>
                    </div>

                <div className="flex justify-between items-end pt-3 border-t border-white/5 mt-auto">
                    <div className="flex flex-col">
                        <span className="text-[8px] uppercase tracking-tighter text-muted-foreground/50 mb-0.5">Rol de Sistema</span>
                        <span className="text-xs font-semibold text-primary">{usr.rol_Nombre || "Sin Rol"}</span>
                    </div>
                    <div className="flex items-center gap-2">
                        <div className="flex flex-col items-end mr-1">
                            <span className="text-[8px] uppercase tracking-tighter text-muted-foreground/50 mb-0.5">Salario Base</span>
                            <span className="text-[11px] font-mono font-medium text-foreground">{usr.salarioBase ? formatAmount(usr.salarioBase) : "—"}</span>
                        </div>
                        <div className="h-7 w-7 rounded-full bg-background flex items-center justify-center border border-border group-hover:bg-primary group-hover:border-primary transition-all">
                            <ArrowRight className="h-3 w-3 text-muted-foreground group-hover:text-primary-foreground" />
                        </div>
                    </div>
                </div>
                </motion.div>
            ))}
            </div>
            {filtered.length > displayLimit && (
                <div className="flex justify-center pt-4">
                    <Button variant="outline" size="sm" onClick={() => setDisplayLimit(prev => prev + 50)} className="gap-2">
                        Cargar más empleados ({filtered.length - displayLimit} restantes)
                    </Button>
                </div>
            )}
        </div>
      ) : (
        <div className="glass rounded-xl overflow-hidden relative border border-border/50">
          <div className="overflow-x-auto scrollbar-thin scrollbar-thumb-primary/20 scrollbar-track-transparent">
            <table className="w-full text-sm min-w-[1000px]">
                <thead>
                <tr className="border-b border-border bg-muted/20">
                    {selectionMode && (
                    <th className="p-4 w-10">
                        <Checkbox 
                        checked={selectedIds.size === filtered.length && filtered.length > 0}
                        onCheckedChange={handleSelectAll}
                        />
                    </th>
                    )}
                    <th className="text-left p-4 font-medium text-muted-foreground">Nombre</th>
                    <th className="text-left p-4 font-medium text-muted-foreground">Departamento</th>
                    <th className="text-left p-4 font-medium text-muted-foreground">Puesto</th>
                    <th className="text-left p-4 font-medium text-muted-foreground">Rol</th>
                    <th className="text-left p-4 font-medium text-muted-foreground">Salario Base</th>
                    <th className="text-left p-4 font-medium text-muted-foreground pr-8">Estado</th>
                </tr>
                </thead>
                <tbody>
                {filtered.slice(0, displayLimit).map((usr, i) => (
                    <UserRow 
                    key={usr.id}
                    usr={usr}
                    index={i}
                    selectionMode={selectionMode}
                    isSelected={selectedIds.has(usr.id)}
                    isLargeList={filtered.length > 80}
                    formatAmount={formatAmount}
                    onSelect={handleSelect}
                    onClick={handleUserClick}
                    />
                ))}
                </tbody>
            </table>
            {filtered.length > displayLimit && (
                <div className="p-4 text-center border-t border-border bg-muted/5">
                    <Button variant="ghost" size="sm" onClick={() => setDisplayLimit(prev => prev + 100)} className="text-primary hover:bg-primary/5">
                        Mostrando {displayLimit} de {filtered.length} - Cargar más...
                    </Button>
                </div>
            )}
          </div>
        </div>
      )}

      <ResponsiveModal open={!!selectedUsuario} onOpenChange={(o) => !o && setSelectedUsuario(null)} title="Desglose del Empleado">
        {selectedUsuario && (
            <div className="space-y-5">
              <div className="flex items-center gap-4">
                <div className="h-14 w-14 rounded-full bg-primary/10 flex items-center justify-center text-primary font-bold text-xl shrink-0">
                  {selectedUsuario.nombre.charAt(0)}
                </div>
                <div className="min-w-0">
                  <h3 className="font-bold text-lg text-foreground truncate">{selectedUsuario.nombre}</h3>
                  <StatusBadge status={selectedUsuario.activo ? "Activo" : "Inactivo"} />
                </div>
              </div>

              <div className="glass rounded-xl p-4 grid grid-cols-1 sm:grid-cols-2 gap-4">
                 <div className="space-y-1">
                    <span className="text-[10px] uppercase tracking-wider text-muted-foreground font-semibold">Email</span>
                    <div className="flex items-center gap-2 text-sm font-medium text-foreground">
                        <Mail className="h-3 w-3 text-primary" /> <span className="truncate">{selectedUsuario.email || "—"}</span>
                    </div>
                 </div>
                 <div className="space-y-1">
                    <span className="text-[10px] uppercase tracking-wider text-muted-foreground font-semibold">Rol Operativo</span>
                    <p className="text-sm font-medium text-primary">{selectedUsuario.rol_Nombre || "Sin Rol Asignado"}</p>
                 </div>
              </div>

              <Separator className="bg-white/5" />

              <div className="grid grid-cols-2 gap-4">
                 <div className="space-y-1">
                    <span className="text-[10px] uppercase tracking-wider text-muted-foreground font-semibold">Departamento</span>
                    <p className="text-sm font-medium text-foreground">{selectedUsuario.departamento || "—"}</p>
                 </div>
                 <div className="space-y-1">
                    <span className="text-[10px] uppercase tracking-wider text-muted-foreground font-semibold">Puesto</span>
                    <p className="text-sm font-medium text-foreground">{selectedUsuario.puesto || "—"}</p>
                 </div>
                 <div className="space-y-1 mt-2">
                    <span className="text-[10px] uppercase tracking-wider text-muted-foreground font-semibold">Salario Base</span>
                    <p className="text-base font-mono font-bold text-primary">{selectedUsuario.salarioBase ? formatAmount(selectedUsuario.salarioBase) : "—"}</p>
                 </div>
                 <div className="space-y-1 mt-2">
                    <span className="text-[10px] uppercase tracking-wider text-muted-foreground font-semibold">Fecha de Ingreso</span>
                    <p className="text-sm font-medium text-foreground">{selectedUsuario.fechaContratacion ? new Date(selectedUsuario.fechaContratacion).toLocaleDateString("es-ES") : "—"}</p>
                 </div>
              </div>

              <div className="flex flex-col gap-2 pt-4">
                <div className="flex gap-2">
                  <a 
                    href={selectedUsuario.email ? `mailto:${selectedUsuario.email}` : '#'} 
                    className={`flex-1 flex items-center justify-center gap-2 py-2.5 rounded-xl border border-primary/20 text-primary transition-colors text-sm font-semibold ${!selectedUsuario.email ? 'opacity-50 cursor-not-allowed' : 'hover:bg-primary/10'}`}
                    onClick={e => !selectedUsuario.email && e.preventDefault()}
                  >
                    <Mail className="h-4 w-4" /> Email
                  </a>
                  <Button 
                    variant="outline" 
                    disabled
                    className="flex-1 gap-2 border-primary/20 text-primary opacity-50 cursor-not-allowed"
                  >
                    <Phone className="h-4 w-4" /> Llamar
                  </Button>
                </div>
                
                <Separator className="bg-white/5 my-1" />
                
                <div className="flex gap-2 w-full">
                    <Button variant="outline" className="flex-1 gap-2 border-primary/10 text-muted-foreground hover:text-foreground hover:bg-muted/30" onClick={() => setFilesModalOpen(true)}>
                    <FileIcon className="h-4 w-4" /> Expediente
                    </Button>
                    {hasPermission("usuarios:update") && (
                      <Button variant="default" className="flex-1 gap-2" onClick={() => {
                          setUserToEdit(selectedUsuario);
                          setUserModalOpen(true);
                      }}>
                          <Edit className="h-4 w-4" /> Editar Perfil
                      </Button>
                    )}
                </div>
              </div>
            </div>
        )}
      </ResponsiveModal>

      <EntityFilesModal
        isOpen={filesModalOpen}
        onClose={() => setFilesModalOpen(false)}
        entidadTipo="Usuario"
        entidadId={selectedUsuario?.id || null}
        entidadNombre={selectedUsuario?.nombre}
      />

      <UsuarioFormModal 
        open={userModalOpen} 
        onClose={() => setUserModalOpen(false)} 
        usuario={userToEdit}
        onSave={async (formData) => {
          if (userToEdit) await updateUsuario({ id: userToEdit.id, data: formData });
          else await createUsuario(formData);
          setUserModalOpen(false);
          setSelectedUsuario(null);
        }}
      />

      <BulkImportModal 
        open={importModalOpen} 
        onClose={() => setImportModalOpen(false)} 
        entityName="Usuarios" 
        onImport={async (data, onProgress) => {
            let successCount = 0;
            let errorCount = 0;
            const total = data.length;
            for (let i = 0; i < total; i++) {
                const row = data[i];
                try {
                    const userData = {
                        nombre: getInsensitive(row, ["nombre", "full_name", "usuario", "name", "empleado"]) || "Nuevo Usuario",
                        email: getInsensitive(row, ["email", "correo", "mail", "e-mail"]) || `user${i}@sarritel.com`,
                        username: getInsensitive(row, ["username", "user_id", "login", "id"]) || `user${Date.now()}${i}`,
                        passwordHash: "Sarritel123!",
                        activo: true,
                        salarioBase: Number(getInsensitive(row, ["salario", "sueldo", "base", "salary"]) || 0)
                    };
                    await createUsuario(userData);
                    successCount++;
                } catch (err) {
                    errorCount++;
                }
                onProgress(Math.round(((i + 1) / total) * 100));
            }
            if (errorCount > 0) toast.warning(`Importación parcial: ${successCount} exitosos, ${errorCount} fallidos.`);
            else toast.success(`Se han importado ${successCount} usuarios correctamente`);
        }}
        requiredFields={["Nombre", "Email"]}
        optionalFields={["Departamento", "Puesto", "Salario"]}
      />

            {isMobile && !isLoading && hasPermission("usuarios:create") && (
                <motion.button whileTap={{ scale: 0.9 }} className="fixed bottom-6 right-6 h-14 w-14 rounded-full bg-primary text-primary-foreground shadow-glow flex items-center justify-center z-40" onClick={() => { setUserToEdit(null); setUserModalOpen(true); }}>
                  <Plus className="h-6 w-6" />
                </motion.button>
            )}
    </div>
  );
}
