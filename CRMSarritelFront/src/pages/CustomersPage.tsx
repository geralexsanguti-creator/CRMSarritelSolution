import { useState, useMemo, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { motion, AnimatePresence } from "framer-motion";
import { Search, Plus, Phone, Mail, MapPin, Filter, Users as UsersIcon, AlertTriangle, Building2, User, ShieldAlert, Users, File as FileIcon, Edit, Upload, MoreHorizontal, Trash2, CheckSquare, LayoutGrid, List, FilterX, ArrowRight } from "lucide-react";
import { KpiCard } from "@/components/shared/KpiCard";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import React from 'react';
import { StatusBadge } from "@/components/shared/StatusBadge";
import { ResponsiveModal } from "@/components/shared/ResponsiveModal";
import { EmptyState } from "@/components/shared/EmptyState";
import { useQueryClient } from '@tanstack/react-query';
import { ClientesService } from '@/services/api/clientes.service';
import { type Cliente } from '@/types';
import { useIsMobile } from "@/hooks/use-mobile";
import { Separator } from "@/components/ui/separator";
import { EntityFilesModal } from "@/components/shared/EntityFilesModal";
import { ClienteFormModal } from "@/components/clientes/ClienteFormModal";
import { BulkImportModal } from "@/components/shared/BulkImportModal";
import { toast } from "sonner";
import { getInsensitive } from "@/lib/import-utils";
import { cn } from "@/lib/utils";
import { UnifiedSearchBar } from "@/components/shared/UnifiedSearchBar";
import { FilterField, type FilterOption } from "@/components/shared/FilterField";
import { useClientes } from "@/hooks/useClientes";
import { useAuth } from "@/hooks/useAuth";

// Memoized table row to prevent unnecessary re-renders during selection toggle
const CustomerRow = React.memo(({ 
  customer, 
  index, 
  selectionMode, 
  isSelected, 
  onSelect, 
  onClick, 
  isLargeList 
}: { 
  customer: Cliente, 
  index: number, 
  selectionMode: boolean, 
  isSelected: boolean, 
  onSelect: (id: number, selected: boolean) => void,
  onClick: (c: Cliente) => void,
  isLargeList: boolean
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
      onClick={() => selectionMode ? null : onClick(customer)}
    >
      {selectionMode && (
        <td className="p-4" onClick={(e) => e.stopPropagation()}>
          <Checkbox 
            checked={isSelected}
            onCheckedChange={(checked) => onSelect(customer.id, !!checked)}
          />
        </td>
      )}
      <td className="p-4 font-medium text-foreground">
        <div className="flex items-center gap-2">
          {customer.penalizado && <AlertTriangle className="h-3.5 w-3.5 text-destructive shrink-0" />}
          <span 
            className="hover:text-primary hover:underline transition-colors decoration-primary/30 underline-offset-4"
            onClick={(e) => {
              e.stopPropagation();
              onClick(customer);
            }}
          >
            {customer.nombre}
          </span>
        </div>
      </td>
      <td className="p-4 text-muted-foreground opacity-70 font-mono text-xs">{customer.dni}</td>
      <td className="p-4"><StatusBadge status={customer.tipo} /></td>
      <td className="p-4 text-muted-foreground">{customer.email}</td>
      <td className="p-4 text-muted-foreground">{customer.movil}</td>
      <td className="p-4 text-muted-foreground">{customer.direccion?.poblacion || '---'}</td>
      <td className="p-4 text-right">
        {customer.penalizado ? <StatusBadge status="Penalizado" /> : <StatusBadge status="Activo" />}
      </td>
    </motion.tr>
  );
});

export default function CustomersPage() {
  const { hasPermission } = useAuth();
  const [selectedIds, setSelectedIds] = useState<Set<number>>(new Set());
  const [selectionMode, setSelectionMode] = useState(false);
  const [viewMode, setViewMode] = useState<'table' | 'grid'>('table');
  const [search, setSearch] = useState("");
  const [tipoFilter, setTipoFilter] = useState("all");
  const [poblacionFilter, setPoblacionFilter] = useState("all");
  const [estadoFilter, setEstadoFilter] = useState("all");
  const [selectedCustomer, setSelectedCustomer] = useState<Cliente | null>(null);
  const [showFilters, setShowFilters] = useState(false);
  const [filesModalOpen, setFilesModalOpen] = useState(false);
  const [formModalOpen, setFormModalOpen] = useState(false);
  const [importModalOpen, setImportModalOpen] = useState(false);
  const [editingClient, setEditingClient] = useState<Cliente | null>(null);
  const [displayLimit, setDisplayLimit] = useState(50);
  
  const isMobile = useIsMobile();
  const navigate = useNavigate();

  const { 
    clientes, 
    isLoading, 
    isError,
    createCliente, 
    updateCliente, 
    deleteCliente 
  } = useClientes();

  const queryClient = useQueryClient();


  const filtered = useMemo(() => {
    const searchLower = search.toLowerCase();
    return clientes.filter((c) => {
      const matchSearch = searchLower === "" || 
        c.nombre?.toLowerCase().includes(searchLower) ||
        c.email?.toLowerCase().includes(searchLower) ||
        c.dni?.includes(searchLower) ||
        c.direccion?.poblacion?.toLowerCase().includes(searchLower);
      
      const matchTipo = tipoFilter === "all" || c.tipo === tipoFilter;
      const matchPoblacion = poblacionFilter === "all" || (c.direccion?.poblacion?.toLowerCase() === poblacionFilter.toLowerCase());
      const matchEstado = estadoFilter === "all" || (estadoFilter === "penalizado" ? c.penalizado : !c.penalizado);

      return matchSearch && matchTipo && matchPoblacion && matchEstado;
    });
  }, [search, clientes, tipoFilter, poblacionFilter, estadoFilter]);

  const uniquePoblaciones = useMemo(() => {
    const set = new Set(clientes.map(c => c.direccion?.poblacion).filter(Boolean));
    return Array.from(set).sort();
  }, [clientes]);

  const kpis = useMemo(() => [
    { 
      title: "Total Clientes", 
      value: String(clientes.length), 
      icon: Users, 
      sparkData: [10, 15, 12, 18, 22, 25, clientes.length] 
    },
    { 
      title: "Empresas vs Part.", 
      value: `${clientes.filter(c => c.tipo === 'EMPRESA').length} / ${clientes.filter(c => c.tipo !== 'EMPRESA').length}`, 
      icon: Building2, 
      sparkData: [5, 6, 5, 7, 8, 8, 10] 
    },
    { 
      title: "Penalizados", 
      value: String(clientes.filter(c => c.penalizado).length), 
      icon: ShieldAlert, 
      sparkData: [2, 1, 3, 2, 1, 0, clientes.filter(c => c.penalizado).length] 
    },
  ], [clientes]);

  const handleSelect = useCallback((id: number, selected: boolean) => {
    setSelectedIds(prev => {
      const next = new Set(prev);
      if (selected) next.add(id);
      else next.delete(id);
      return next;
    });
  }, []);

  const handleSelectAll = useCallback((checked: boolean) => {
    if (checked) setSelectedIds(new Set(filtered.map(c => c.id)));
    else setSelectedIds(new Set());
  }, [filtered]);

  const handleSaveCliente = async (data: Partial<Cliente>) => {
    try {
      if (editingClient) {
        await updateCliente({ id: editingClient.id, data });
      } else {
        await createCliente(data as any);
      }
      setFormModalOpen(false);
    } catch {
      // Error handled in hook
    }
  };


  const handleImport = async (data: any[], onProgress: (p: number) => void) => {
    let successCount = 0;
    let errorCount = 0;
    const total = data.length;

    for (let i = 0; i < total; i++) {
      const row = data[i];
      try {
        const clientData = {
          nombre: getInsensitive(row, ["nombre", "nombre completo", "name", "cliente", "full_name"]) || "Sin Nombre",
          dni: getInsensitive(row, ["dni", "nif", "documento", "id_fiscal", "ruc"]) || null,
          tipo: String(getInsensitive(row, ["tipo", "type", "categoria"]) || "PARTICULAR").toUpperCase(),
          email: getInsensitive(row, ["email", "correo", "mail", "e-mail"]) || null,
          movil: getInsensitive(row, ["movil", "telefono", "phone", "mobile", "celular", "tel"]) ? String(getInsensitive(row, ["movil", "telefono", "phone", "mobile", "celular", "tel"])) : null,
          numeroCuenta: getInsensitive(row, ["cuenta", "iban", "account", "banco"]) || "",
          direccion: {
            calle: getInsensitive(row, ["calle", "direccion", "address", "street", "via"]) || null,
            codigoPostal: getInsensitive(row, ["cp", "codigo postal", "zip", "zipcode", "postal", "c.p.", "c_p"]) ? String(getInsensitive(row, ["cp", "codigo postal", "zip", "zipcode", "postal", "c.p.", "c_p"])) : null,
            poblacion: getInsensitive(row, ["poblacion", "ciudad", "city", "localidad", "municipio"]) || null,
            provincia: getInsensitive(row, ["provincia", "state", "region", "comunidad"]) || null
          }
        };
        await ClientesService.create(clientData as any);
        successCount++;
      } catch (err: any) {
        errorCount++;
      }
      onProgress(Math.round(((i + 1) / total) * 100));
    }
    
    queryClient.invalidateQueries({ queryKey: ["clientes"] });
    if (errorCount > 0) toast.warning(`Importación parcial: ${successCount} exitosos, ${errorCount} fallidos.`);
    else toast.success(`Se han importado ${successCount} clientes correctamente`);
  };

  const handleBulkDelete = async () => {
    if (selectedIds.size === 0) return;
    if (!confirm(`¿Estás seguro de que deseas eliminar ${selectedIds.size} registros?`)) return;

    const idsArray = Array.from(selectedIds);
    const toastId = toast.loading(`Eliminando ${idsArray.length} clientes... (0%)`);
    try {
      const total = idsArray.length;
      
      for (let i = 0; i < total; i++) {
        await deleteCliente(idsArray[i]);
        const progress = Math.min(100, Math.round(((i + 1) / total) * 100));
        toast.loading(`Eliminando ${idsArray.length} clientes... (${progress}%)`, { id: toastId });
      }
      
      setSelectedIds(new Set());
      setSelectionMode(false);
      toast.success("Registros eliminados correctamente", { id: toastId });
    } catch (error) {
      toast.error("Error en la eliminación masiva", { id: toastId });
    }
  };


  const handleCreateNew = () => {
    setEditingClient(null);
    setFormModalOpen(true);
  };

  const handleEdit = (c: Cliente) => {
    setEditingClient(c);
    setFormModalOpen(true);
  };

  const handleCustomerClick = useCallback((c: Cliente) => setSelectedCustomer(c), []);

  return (
    <div className="space-y-6 pb-20 sm:pb-6">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl font-bold text-foreground">Clientes</h1>
          <p className="text-sm text-muted-foreground">{filtered.length} registros encontrados</p>
        </div>
        <div className="flex items-center gap-2">
          <div className="hidden sm:flex items-center bg-muted/30 p-1 rounded-lg border border-border mr-2">
            <Button 
              variant={viewMode === 'table' ? 'secondary' : 'ghost'} 
              size="icon" 
              className="h-7 w-7 rounded-sm" 
              onClick={() => setViewMode('table')}
              title="Vista de Tabla"
            >
              <List className="h-4 w-4" />
            </Button>
            <Button 
              variant={viewMode === 'grid' ? 'secondary' : 'ghost'} 
              size="icon" 
              className="h-7 w-7 rounded-sm" 
              onClick={() => setViewMode('grid')}
              title="Vista de Widgets"
            >
              <LayoutGrid className="h-4 w-4" />
            </Button>
          </div>
          {hasPermission("clientes:create") && (
            <Button variant="outline" size="icon" className="border-border bg-background" onClick={() => setImportModalOpen(true)} title="Importar CSV/Excel">
              <Upload className="h-4 w-4" />
            </Button>
          )}
          {hasPermission("clientes:create") && (
            <Button className="bg-primary text-primary-foreground hover:bg-primary/90 gap-2 flex-1 sm:flex-none font-bold shadow-glow" onClick={handleCreateNew}>
              <Plus className="h-4 w-4" /> {isMobile ? "Nuevo" : "Nuevo Cliente"}
            </Button>
          )}
        </div>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {kpis.map((kpi, i) => (
          <KpiCard key={kpi.title} {...kpi} index={i} />
        ))}
      </div>

      <UnifiedSearchBar
        selectionMode={selectionMode}
        onSelectionModeToggle={hasPermission("clientes:delete") ? () => {
          setSelectionMode(!selectionMode);
          if (selectionMode) setSelectedIds(new Set());
        } : undefined}
        bulkActions={
          hasPermission("clientes:delete") && (
            <div className="flex items-center gap-3 w-full">
              <span className="text-xs font-bold text-primary bg-primary/10 px-2 py-1 rounded-md border border-primary/20">
                {selectedIds.size} seleccionados
              </span>
              <div className="h-4 w-px bg-border/40 mx-1" />
              <Button 
                variant="destructive" 
                size="sm" 
                className="h-8 gap-2 shadow-sm font-bold" 
                disabled={selectedIds.size === 0}
                onClick={handleBulkDelete}
              >
                <Trash2 className="h-3.5 w-3.5" /> Eliminar Clientes
              </Button>
            </div>
          )
        }
        searchValue={search}
        onSearchChange={setSearch}
        searchPlaceholder="Buscar por nombre, email o DNI..."
        onClearFilters={() => {
          setSearch('');
          setTipoFilter('all');
          setPoblacionFilter('all');
          setEstadoFilter('all');
        }}
        showFilterBadge={tipoFilter !== 'all' || poblacionFilter !== 'all' || estadoFilter !== 'all'}
      >
        <div className="flex flex-wrap items-end gap-4 w-full">
          <FilterField 
            columnLabel="Tipo de Cliente"
            type="select"
            value={tipoFilter}
            onChange={setTipoFilter}
            options={[
              { value: "EMPRESA", label: "Empresa" },
              { value: "PARTICULAR", label: "Particular" }
            ]}
            placeholder="Todos los tipos"
          />

          <FilterField 
            columnLabel="Población"
            type="select"
            value={poblacionFilter}
            onChange={setPoblacionFilter}
            options={uniquePoblaciones.map(p => ({ value: p!, label: p! }))}
            placeholder="Todas las poblaciones"
          />

          <FilterField 
            columnLabel="Estado"
            type="select"
            value={estadoFilter}
            onChange={setEstadoFilter}
            options={[
              { value: "ACTIVO", label: "Activo" },
              { value: "INACTIVO", label: "Inactivo" }
            ]}
            placeholder="Cualquier estado"
          />
        </div>
      </UnifiedSearchBar>

      {isLoading ? (
        <div className="flex justify-center items-center h-40 glass rounded-xl text-muted-foreground animate-pulse font-bold tracking-widest uppercase text-xs">
          Sincronizando clientes...
        </div>
      ) : isError ? (
        <div className="flex flex-col gap-2 justify-center items-center h-40 glass rounded-xl text-destructive text-center p-6">
          <AlertTriangle className="h-8 w-8 mb-2" />
          <p className="font-semibold">Error de conexión</p>
          <p className="text-xs opacity-70">Asegúrate de que la API de C# esté corriendo.</p>
        </div>
      ) : filtered.length === 0 ? (
        <EmptyState icon={UsersIcon} title="Sin clientes" description="No se encontraron clientes." />
      ) : (viewMode === 'grid' || isMobile) ? (
        <div className="space-y-4">
            <div className="grid gap-3 sm:grid-cols-2 lg:grid-cols-3">
            {filtered.slice(0, displayLimit).map((customer, i) => (
                <motion.div key={customer.id} initial={{ opacity: 0, y: 10 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: Math.min(i * 0.05, 0.5) }} className="glass glass-hover rounded-xl p-4 cursor-pointer group flex flex-col h-full" onClick={() => selectionMode ? null : handleCustomerClick(customer)}>
                <div className="flex items-start justify-between mb-2">
                    <div className="flex items-center gap-2 text-sm">
                    {selectionMode && (
                        <Checkbox 
                        checked={selectedIds.has(customer.id)} 
                        onCheckedChange={(checked) => handleSelect(customer.id, !!checked)}
                        onClick={(e) => e.stopPropagation()}
                        />
                    )}
                    {customer.tipo === "EMPRESA" ? <Building2 className="h-4 w-4 text-primary" /> : <User className="h-4 w-4 text-muted-foreground" />}
                    <div>
                        <h3 className="font-bold text-foreground text-sm flex items-center gap-1.5 leading-tight group-hover:text-primary transition-colors">
                            {customer.nombre}
                            {customer.penalizado && <AlertTriangle className="h-3 w-3 text-destructive" />}
                        </h3>
                        <p className="text-[10px] text-muted-foreground font-mono">{customer.dni}</p>
                    </div>
                    </div>
                    <StatusBadge status={customer.tipo} />
                </div>
                <p className="text-[11px] text-muted-foreground mb-4 line-clamp-1 opacity-80 mt-1 italic">
                    <MapPin className="h-2.5 w-2.5 inline mr-1" />
                    {customer.direccion?.poblacion && customer.direccion?.provincia
                    ? `${customer.direccion?.poblacion}, ${customer.direccion?.provincia}`
                    : 'Sin dirección configurada'}
                </p>
                <div className="flex gap-2 mt-auto pt-2">
                    <a href={`tel:${customer.movil}`} onClick={e => e.stopPropagation()} className="flex-1 flex items-center justify-center gap-1.5 text-[9px] uppercase font-bold tracking-wider bg-background/50 rounded-lg py-2 text-muted-foreground hover:text-primary hover:bg-primary/5 transition-all outline-none">
                    <Phone className="h-3 w-3" /> Llamar
                    </a>
                    <a href={`mailto:${customer.email}`} onClick={e => e.stopPropagation()} className="flex-1 flex items-center justify-center gap-1.5 text-[9px] uppercase font-bold tracking-wider bg-background/50 rounded-lg py-2 text-muted-foreground hover:text-primary hover:bg-primary/5 transition-all outline-none">
                    <Mail className="h-3 w-3" /> Email
                    </a>
                </div>
                </motion.div>
            ))}
            </div>
            {filtered.length > displayLimit && (
                <div className="flex justify-center pt-4">
                    <Button variant="outline" size="sm" onClick={() => setDisplayLimit(prev => prev + 50)} className="gap-2 font-bold">
                        Cargar más clientes ({filtered.length - displayLimit} restantes)
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
                  <th className="text-left p-4 font-bold text-muted-foreground uppercase tracking-tighter text-xs">Nombre</th>
                  <th className="text-left p-4 font-bold text-muted-foreground uppercase tracking-tighter text-xs">DNI</th>
                  <th className="text-left p-4 font-bold text-muted-foreground uppercase tracking-tighter text-xs">Tipo</th>
                  <th className="text-left p-4 font-bold text-muted-foreground uppercase tracking-tighter text-xs">Email</th>
                  <th className="text-left p-4 font-bold text-muted-foreground uppercase tracking-tighter text-xs">Móvil</th>
                  <th className="text-left p-4 font-bold text-muted-foreground uppercase tracking-tighter text-xs">Población</th>
                  <th className="text-right p-4 font-bold text-muted-foreground pr-8 uppercase tracking-tighter text-xs">Estado</th>
                </tr>
              </thead>
              <tbody>
                {filtered.slice(0, displayLimit).map((customer, i) => (
                  <CustomerRow 
                    key={customer.id}
                    customer={customer}
                    index={i}
                    selectionMode={selectionMode}
                    isSelected={selectedIds.has(customer.id)}
                    isLargeList={filtered.length > 80}
                    onSelect={handleSelect}
                    onClick={handleCustomerClick}
                  />
                ))}
              </tbody>
            </table>
            {filtered.length > displayLimit && (
                <div className="p-4 text-center border-t border-border bg-muted/5">
                    <Button variant="ghost" size="sm" onClick={() => setDisplayLimit(prev => prev + 100)} className="text-primary hover:bg-primary/5 font-bold">
                        Mostrando {displayLimit} de {filtered.length} - Cargar más...
                    </Button>
                </div>
            )}
          </div>
        </div>
      )}

      <ResponsiveModal 
        open={!!selectedCustomer} 
        onOpenChange={(open) => !open && setSelectedCustomer(null)} 
        title="Detalle del Cliente"
        variant="dialog"
        maxWidth="800px"
      >
        {selectedCustomer && (
          <div className="space-y-6">
            <div className="flex items-center gap-4">
              <div className="h-14 w-14 rounded-full bg-primary/10 flex items-center justify-center text-primary font-bold text-xl uppercase shrink-0">
                {selectedCustomer.nombre?.charAt(0) || "C"}
              </div>
              <div className="min-w-0">
                <h3 className="font-bold text-lg text-foreground truncate">{selectedCustomer.nombre}</h3>
                <div className="flex items-center gap-2 flex-wrap">
                  <StatusBadge status={selectedCustomer.tipo} />
                  {selectedCustomer.penalizado && <StatusBadge status="Penalizado" />}
                </div>
              </div>
            </div>
            <div className="space-y-3">
              <div className="flex items-center gap-3 text-sm font-medium"><Mail className="h-4 w-4 text-primary opacity-70" /><span className="text-foreground truncate">{selectedCustomer.email || 'Sin email'}</span></div>
              <div className="flex items-center gap-3 text-sm font-medium"><Phone className="h-4 w-4 text-primary opacity-70" /><span className="text-foreground">{selectedCustomer.movil || 'Sin móvil'}</span></div>
              <div className="flex items-start gap-3 text-sm font-medium"><MapPin className="h-4 w-4 text-primary opacity-70 mt-0.5" />
                <span className="text-foreground text-xs leading-relaxed italic opacity-80">
                  {selectedCustomer.direccion?.calle ? `${selectedCustomer.direccion.calle}, ${selectedCustomer.direccion.codigoPostal} ${selectedCustomer.direccion.poblacion}, ${selectedCustomer.direccion.provincia}` : 'Sin dirección guardada'}
                </span>
              </div>
            </div>
            <Separator className="bg-white/5" />
            <div className="grid grid-cols-2 gap-4 text-[11px]">
              <div><span className="text-muted-foreground block uppercase tracking-tighter mb-1 font-bold">DNI / NIF / RNC</span><span className="text-foreground font-mono font-bold">{selectedCustomer.dni || "—"}</span></div>
              <div><span className="text-muted-foreground block uppercase tracking-tighter mb-1 font-bold">Cuenta Bancaria</span><span className="text-foreground font-mono font-bold truncate inline-block w-full">{selectedCustomer.numeroCuenta || "—"}</span></div>
              <div><span className="text-muted-foreground block uppercase tracking-tighter mb-1 font-bold">Fecha Alta</span><span className="text-foreground font-bold">{new Date(selectedCustomer.createdAt).toLocaleDateString("es-ES")}</span></div>
              <div><span className="text-muted-foreground block uppercase tracking-tighter mb-1 font-bold">Últ. Actividad</span><span className="text-foreground font-bold">{new Date(selectedCustomer.updatedAt).toLocaleDateString("es-ES")}</span></div>
            </div>
            {selectedCustomer.notaPublica && (
              <div className="space-y-2">
                  <h4 className="text-[10px] font-bold text-muted-foreground uppercase tracking-widest opacity-60">Nota Interna</h4>
                  <p className="text-xs text-foreground bg-muted/40 rounded-xl p-4 leading-relaxed border border-border/30 italic">{selectedCustomer.notaPublica}</p>
              </div>
            )}
            <div className="pt-4 border-t border-border mt-4 flex flex-col gap-2">
              <div className="flex gap-2">
                <a 
                  href={`tel:${selectedCustomer.movil}`} 
                  className="flex-1 flex items-center justify-center gap-2 py-2.5 rounded-xl border border-primary/20 text-primary hover:bg-primary/10 transition-colors text-sm font-bold shadow-sm"
                >
                  <Phone className="h-4 w-4" /> Llamar
                </a>
                <a 
                  href={`mailto:${selectedCustomer.email}`} 
                  className="flex-1 flex items-center justify-center gap-2 py-2.5 rounded-xl border border-primary/20 text-primary hover:bg-primary/10 transition-colors text-sm font-bold shadow-sm"
                >
                  <Mail className="h-4 w-4" /> Email
                </a>
              </div>
              <Separator className="bg-white/5 my-1" />
              <div className="flex gap-2">
                {(hasPermission("clientes:update") || hasPermission("archivos:read")) && (
                  <Button variant="outline" className="flex-1 gap-2 border-primary/10 text-muted-foreground hover:text-foreground hover:bg-muted/30 font-bold" onClick={() => setFilesModalOpen(true)}>
                      <FileIcon className="h-4 w-4" /> Archivos 
                  </Button>
                )}
                {hasPermission("clientes:update") && (
                  <Button variant="secondary" className="flex-1 gap-2 border border-border font-bold bg-primary/10 text-primary hover:bg-primary/20" onClick={() => handleEdit(selectedCustomer)}>
                      <LayoutGrid className="h-4 w-4" /> Vista 360
                  </Button>
                )}
              </div>
              {hasPermission("ventas:read") && (
                <Button variant="outline" className="w-full gap-2 border-primary/5 bg-muted/20 text-muted-foreground hover:text-foreground font-bold" onClick={() => {
                    if (selectedCustomer) {
                      navigate(`/ventas?clienteId=${selectedCustomer.id}`);
                    }
                }}>
                  <MoreHorizontal className="h-4 w-4" /> Ver Historial Comercial
                </Button>
              )}
            </div>
          </div>
        )}
      </ResponsiveModal>

      <ClienteFormModal open={formModalOpen} onClose={() => setFormModalOpen(false)} cliente={editingClient} onSave={handleSaveCliente} />

      <EntityFilesModal isOpen={filesModalOpen} onClose={() => setFilesModalOpen(false)} entidadTipo="Cliente" entidadId={selectedCustomer?.id || null} entidadNombre={selectedCustomer?.nombre} />

      <BulkImportModal 
        open={importModalOpen} 
        onClose={() => setImportModalOpen(false)} 
        entityName="Clientes" 
        onImport={handleImport}
        requiredFields={["Nombre", "Email", "Movil"]}
        optionalFields={["DNI", "Tipo", "Cuenta", "Calle", "CP", "Poblacion", "Provincia"]}
      />

      {isMobile && !isLoading && !isError && hasPermission("clientes:create") && (
        <motion.button whileTap={{ scale: 0.9 }} className="fixed bottom-6 right-6 h-14 w-14 rounded-full bg-primary text-primary-foreground shadow-glow flex items-center justify-center z-40" onClick={handleCreateNew}>
          <Plus className="h-6 w-6" />
        </motion.button>
      )}
    </div>
  );
}
