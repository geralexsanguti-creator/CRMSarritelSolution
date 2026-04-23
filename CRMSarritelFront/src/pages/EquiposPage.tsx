import { useState, useMemo, useEffect } from "react";
import { 
  Users, 
  Plus, 
  MoreVertical, 
  Edit, 
  Trash2, 
  UserPlus,
  LayoutGrid,
  List as ListIcon,
  ShieldAlert,
  Users2,
  ArrowRight,
  Star,
  Settings,
  X,
  Target,
  UserCheck
} from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Badge } from "@/components/ui/badge";
import { 
  DropdownMenu, 
  DropdownMenuContent, 
  DropdownMenuItem, 
  DropdownMenuTrigger 
} from "@/components/ui/dropdown-menu";
import { ScrollArea } from "@/components/ui/scroll-area";
import { glass, card, flexCenter } from "@/lib/styles";
import { cn, getUploadUrl } from "@/lib/utils";
import { equiposService } from "@/services/api/equipos.service";
import { UsuariosService } from "@/services/api/usuarios.service";
import { Equipo, Usuario } from "@/types";
import { toast } from "sonner";
import { Sheet, SheetContent, SheetHeader, SheetTitle } from "@/components/ui/sheet";
import { Label } from "@/components/ui/label";
import { Checkbox } from "@/components/ui/checkbox";
import { KpiCard } from "@/components/shared/KpiCard";
import { useAuth } from "@/hooks/useAuth";
import { UnifiedSearchBar } from "@/components/shared/UnifiedSearchBar";
import { motion, AnimatePresence } from "framer-motion";
import { ImageUpload } from "@/components/shared/ImageUpload";

export default function EquiposPage() {
  const { hasPermission } = useAuth();
  const [equipos, setEquipos] = useState<Equipo[]>([]);
  const [usuarios, setUsuarios] = useState<Usuario[]>([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState("");
  const [viewMode, setViewMode] = useState<"table" | "grid">("grid");
  
  // Sidebar State
  const [isDetailOpen, setIsDetailOpen] = useState(false);
  const [sidebarMode, setSidebarMode] = useState<"view" | "edit">("view");
  const [editingEquipo, setEditingEquipo] = useState<Equipo | null>(null);
  const [formData, setFormData] = useState<{ nombre: string, descripcion: string, logoUrl: string }>({ nombre: "", descripcion: "", logoUrl: "" });
  const [selectedUserIds, setSelectedUserIds] = useState<number[]>([]);
  const [managerIds, setManagerIds] = useState<number[]>([]);

  const fetchEquipos = async () => {
    setLoading(true);
    try {
      const data = await equiposService.getAll();
      setEquipos(data);
      if (editingEquipo) {
          const fresh = data.find(e => e.id === editingEquipo.id);
          if (fresh) setEditingEquipo(fresh);
      }
    } catch (error) {
      toast.error("Error al cargar equipos");
    } finally {
      setLoading(false);
    }
  };

  const fetchUsuarios = async () => {
    try {
      const data = await UsuariosService.getAll();
      setUsuarios(data);
    } catch (error) {
      console.error("Error loading users:", error);
    }
  };

  useEffect(() => {
    fetchEquipos();
    fetchUsuarios();
  }, []);

  const totalMembers = useMemo(() => {
    const uniqueUserIds = new Set<number>();
    equipos.forEach(eq => {
      eq.usuarioEquipos?.forEach(ue => uniqueUserIds.add(ue.usuarioId));
    });
    return uniqueUserIds.size;
  }, [equipos]);

  const kpis = useMemo(() => [
    { title: "Equipos", value: String(equipos.length), icon: Users2, sparkData: [2, 3, 3, 4, 4, 5, 5] },
    { title: "Integrantes", value: String(totalMembers), icon: Users, sparkData: [10, 15, 20, 18, 25, 22, 30] },
    { title: "Sin asignar", value: String(usuarios.length - totalMembers), icon: ShieldAlert, sparkData: [5, 4, 3, 3, 2, 2, 1] },
  ], [equipos.length, totalMembers, usuarios.length]);

  const filteredEquipos = useMemo(() => {
    return equipos.filter(e => 
      e.nombre.toLowerCase().includes(searchTerm.toLowerCase()) ||
      e.descripcion?.toLowerCase().includes(searchTerm.toLowerCase())
    );
  }, [equipos, searchTerm]);

  const handleGlobalSave = async () => {
    if (!formData.nombre.trim()) return toast.error("El nombre es obligatorio");
    try {
      let currentId = editingEquipo?.id;
      
      // 1. Guardar/Actualizar metadatos
      if (editingEquipo) {
          await equiposService.update(editingEquipo.id, { ...formData, id: editingEquipo.id });
      } else {
          const nuevo = await equiposService.create(formData);
          currentId = nuevo.id;
      }

      // 2. Sincronizar Integrantes
      if (currentId) {
          const payload = selectedUserIds.map(id => ({
              usuarioId: Number(id),
              esManager: managerIds.includes(Number(id))
          }));
          await equiposService.syncUsuarios(currentId, payload);
      }

      toast.success("Equipo guardado correctamente");
      await fetchEquipos();
      setSidebarMode("view");
    } catch (error) {
      toast.error("Error al guardar los cambios");
    }
  };

  const openSidebar = (equipo: Equipo | null, mode: "view" | "edit" = "view") => {
    if (equipo) {
        setEditingEquipo(equipo);
        setFormData({ nombre: equipo.nombre, descripcion: equipo.descripcion || "", logoUrl: equipo.logoUrl || "" });
        setSelectedUserIds(equipo.usuarioEquipos?.map(ue => ue.usuarioId) || []);
        setManagerIds(equipo.usuarioEquipos?.filter(ue => ue.esManager).map(ue => ue.usuarioId) || []);
        setSidebarMode(mode);
    } else {
        setEditingEquipo(null);
        setFormData({ nombre: "", descripcion: "", logoUrl: "" });
        setSelectedUserIds([]);
        setManagerIds([]);
        setSidebarMode("edit");
    }
    setIsDetailOpen(true);
  };

  const handleDelete = async (id: number) => {
    if (!confirm("¿Deseas eliminar este equipo?")) return;
    try {
      await equiposService.delete(id);
      toast.success("Equipo eliminado");
      setIsDetailOpen(false);
      fetchEquipos();
    } catch (error) {
      toast.error("No se pudo eliminar");
    }
  };

  return (
    <div className="space-y-4 animate-in fade-in duration-500 pb-10">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-xl font-bold text-foreground flex items-center gap-2">
            Equipos <Badge variant="secondary" className="font-mono text-[9px] h-4">v3.0</Badge>
          </h1>
          <p className="text-[10px] text-muted-foreground font-medium uppercase tracking-wider opacity-60 italic">Gestión operativa descentralizada</p>
        </div>
        
        <div className="flex items-center gap-2">
          <div className="flex items-center glass p-0.5 rounded-lg border border-border/40">
            <button onClick={() => setViewMode("grid")} className={cn("p-1.5 rounded-sm transition-colors", viewMode === "grid" ? "bg-secondary text-secondary-foreground" : "hover:bg-muted text-muted-foreground")}>
              <LayoutGrid className="h-3.5 w-3.5" />
            </button>
            <button onClick={() => setViewMode("table")} className={cn("p-1.5 rounded-sm transition-colors", viewMode === "table" ? "bg-secondary text-secondary-foreground" : "hover:bg-muted text-muted-foreground")}>
              <ListIcon className="h-3.5 w-3.5" />
            </button>
          </div>
          {hasPermission("equipos:create") && (
            <Button onClick={() => openSidebar(null)} size="sm" className="h-8 gap-2 px-3 font-semibold rounded-md">
              <Plus className="h-3.5 w-3.5" /> Nuevo Equipo
            </Button>
          )}
        </div>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
        {kpis.map((kpi, i) => (<KpiCard key={kpi.title} {...kpi} index={i} />))}
      </div>

      <UnifiedSearchBar
        selectionMode={false}
        onSelectionModeToggle={() => {}}
        searchValue={searchTerm}
        onSearchChange={setSearchTerm}
        searchPlaceholder="Buscar por nombre u objetivo..."
        onClearFilters={() => setSearchTerm('')}
      />

      {loading ? (
        <div className="h-48 flex flex-col items-center justify-center">
           <div className="w-6 h-6 border-2 border-primary border-t-transparent rounded-full animate-spin mb-2" />
        </div>
      ) : viewMode === "grid" ? (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-3">
          {filteredEquipos.map((equipo, i) => (
            <motion.div 
              key={equipo.id} 
              initial={{ opacity: 0, scale: 0.98 }}
              animate={{ opacity: 1, scale: 1 }}
              transition={{ delay: i * 0.02 }}
              onClick={() => openSidebar(equipo, "view")}
              className={cn("group transition-all hover:border-primary/30 cursor-pointer overflow-hidden relative", card, "p-0")}
            >
              <div className="p-4">
                <div className="flex items-start justify-between mb-2">
                  <div className="flex items-center gap-3">
                    {equipo.logoUrl && equipo.logoUrl !== "default.png" ? (
                      <div className="h-9 w-9 rounded-md overflow-hidden bg-primary/10 flex items-center justify-center shrink-0">
                        <img src={getUploadUrl(equipo.logoUrl)} alt="logo" className="h-full w-full object-cover" />
                      </div>
                    ) : (
                      <div className="h-9 w-9 rounded-md bg-primary/10 flex items-center justify-center text-primary font-bold text-sm shrink-0">
                        {equipo.nombre.charAt(0)}
                      </div>
                    )}
                    <div>
                      <h3 className="font-bold text-sm group-hover:text-primary transition-colors">{equipo.nombre}</h3>
                      <p className="text-[9px] font-bold text-muted-foreground/60 uppercase flex items-center gap-1 mt-0.5">
                         <Users className="h-2.5 w-2.5" /> {equipo.usuarioEquipos?.length || 0} Miembros
                      </p>
                    </div>
                  </div>
                  <Target className="h-3.5 w-3.5 text-muted-foreground/20" />
                </div>

                <p className="text-[10px] text-muted-foreground/60 italic line-clamp-2 mb-4 min-h-[2.5em]">
                  {equipo.descripcion || "Sin objetivo definido"}
                </p>

                <div className="flex items-center justify-between border-t border-border/10 pt-3">
                    <div className="flex -space-x-2">
                        {equipo.usuarioEquipos?.slice(0, 4).map((ue) => (
                            <div key={ue.usuarioId} className={cn("h-6 w-6 rounded-full border-2 border-background flex items-center justify-center text-[8px] font-black uppercase text-white", ue.esManager ? "bg-amber-500" : "bg-primary/40")}>
                                {ue.usuario?.nombre.charAt(0)}
                            </div>
                        ))}
                        {(equipo.usuarioEquipos?.length || 0) > 4 && (
                            <div className="h-6 w-6 rounded-full border-2 border-background bg-muted flex items-center justify-center text-[7px] font-bold">
                                +{(equipo.usuarioEquipos?.length || 0) - 4}
                            </div>
                        )}
                    </div>
                    <ArrowRight className="h-3 w-3 text-primary opacity-0 group-hover:opacity-100 transition-all translate-x-2 group-hover:translate-x-0" />
                </div>
              </div>
            </motion.div>
          ))}
        </div>
      ) : (
        <div className={cn("overflow-hidden border border-border/40 rounded-lg", glass)}>
          <table className="w-full text-left border-collapse">
            <thead className="bg-muted/30">
              <tr>
                <th className="px-4 py-2 font-bold text-[9px] uppercase text-muted-foreground tracking-widest">Equipo</th>
                <th className="px-4 py-2 font-bold text-[9px] uppercase text-muted-foreground tracking-widest">Objetivo</th>
                <th className="px-4 py-2 font-bold text-[9px] uppercase text-muted-foreground text-center">Managers</th>
                <th className="px-4 py-2 font-bold text-[9px] uppercase text-muted-foreground text-center">Total</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-border/10">
              {filteredEquipos.map((equipo) => (
                <tr key={equipo.id} className="hover:bg-primary/[0.01] transition-colors group cursor-pointer" onClick={() => openSidebar(equipo, "view")}>
                  <td className="px-4 py-2 text-xs font-semibold group-hover:text-primary">{equipo.nombre}</td>
                  <td className="px-4 py-2 text-[10px] text-muted-foreground truncate max-w-[200px]">{equipo.descripcion || "-"}</td>
                  <td className="px-4 py-2 text-center">
                    <span className="text-[10px] font-bold text-amber-500">{equipo.usuarioEquipos?.filter(ue => ue.esManager).length || 0}</span>
                  </td>
                  <td className="px-4 py-2 text-center text-[10px] font-mono">{equipo.usuarioEquipos?.length || 0}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {/* SIDEBAR OPERATIVO */}
      <Sheet open={isDetailOpen} onOpenChange={setIsDetailOpen}>
        <SheetContent side="right" className="w-full sm:max-w-md p-0 glass flex flex-col h-full bg-background/95">
          <SheetHeader className="p-5 pb-3">
             <div className="flex items-center justify-between mb-1">
                <Badge variant={sidebarMode === 'edit' ? "default" : "outline"} className="text-[8px] font-black h-4 uppercase">
                    {sidebarMode === 'edit' ? "Editor de Estructura" : "Ficha de Equipo"}
                </Badge>
                <Button variant="ghost" size="icon" className="rounded-full h-6 w-6" onClick={() => setIsDetailOpen(false)}><X className="h-3.5 w-3.5" /></Button>
             </div>
             <SheetTitle className="text-base font-bold flex items-center gap-2">
                {editingEquipo ? editingEquipo.nombre : "Nuevo Equipo"}
             </SheetTitle>
          </SheetHeader>

          <ScrollArea className="flex-1 px-5 h-full">
            <AnimatePresence mode="wait">
                {sidebarMode === "view" && editingEquipo ? (
                    <motion.div 
                        key="view"
                        initial={{ opacity: 0, x: 10 }}
                        animate={{ opacity: 1, x: 0 }}
                        exit={{ opacity: 0, x: -10 }}
                        className="space-y-6 pt-2 pb-10"
                    >
                        {/* Objetivo */}
                        <div className="space-y-1.5">
                            <Label className="text-[9px] font-bold uppercase text-primary/60 tracking-widest flex items-center gap-1.5">
                                <Target className="h-2.5 w-2.5" /> Objetivo Principal
                            </Label>
                            <p className="text-xs text-foreground/80 leading-relaxed font-medium bg-muted/20 p-3 rounded-lg border border-border/10 italic">
                                {editingEquipo.descripcion || "No se ha definido un objetivo para este equipo."}
                            </p>
                        </div>

                        {/* Listado de Managers */}
                        <div className="space-y-3">
                            <Label className="text-[9px] font-bold uppercase text-amber-500 tracking-widest flex items-center gap-1.5">
                                <Star className="h-2.5 w-2.5 fill-amber-500" /> Responsables (Managers)
                            </Label>
                            <div className="grid grid-cols-1 gap-2">
                                {editingEquipo.usuarioEquipos?.filter(ue => ue.esManager).length === 0 ? (
                                    <p className="text-[10px] text-muted-foreground italic pl-1">Sin managers asignados</p>
                                ) : (
                                    editingEquipo.usuarioEquipos?.filter(ue => ue.esManager).map(ue => (
                                        <div key={ue.usuarioId} className="flex items-center justify-between p-2 rounded-lg bg-amber-500/[0.03] border border-amber-500/10">
                                            <div className="flex items-center gap-2">
                                                <div className="h-7 w-7 rounded bg-amber-500/10 flex items-center justify-center text-amber-600 font-black text-[10px]">
                                                    {ue.usuario?.nombre.charAt(0)}
                                                </div>
                                                <span className="text-xs font-bold text-amber-700/80">{ue.usuario?.nombre}</span>
                                            </div>
                                            <Badge variant="outline" className="text-[8px] border-amber-500/20 text-amber-600 uppercase">PRORRATEO</Badge>
                                        </div>
                                    ))
                                )}
                            </div>
                        </div>

                        {/* Resto de Integrantes */}
                        <div className="space-y-3">
                            <Label className="text-[9px] font-bold uppercase text-muted-foreground tracking-widest flex items-center gap-1.5 pt-2">
                                <Users className="h-2.5 w-2.5" /> Equipo Operativo
                            </Label>
                            <div className="grid grid-cols-1 gap-1.5">
                                {editingEquipo.usuarioEquipos?.filter(ue => !ue.esManager).length === 0 ? (
                                    <p className="text-[10px] text-muted-foreground italic pl-1">Sin integrantes adicionales</p>
                                ) : (
                                    editingEquipo.usuarioEquipos?.filter(ue => !ue.esManager).map(ue => (
                                        <div key={ue.usuarioId} className="flex items-center gap-2 p-2 rounded-lg hover:bg-muted/30 transition-colors">
                                            <div className="h-6 w-6 rounded bg-primary/5 flex items-center justify-center text-primary/40 text-[9px] font-bold">
                                                {ue.usuario?.nombre.charAt(0)}
                                            </div>
                                            <span className="text-[11px] font-medium text-foreground/70">{ue.usuario?.nombre}</span>
                                        </div>
                                    ))
                                )}
                            </div>
                        </div>
                    </motion.div>
                ) : (
                    <motion.div 
                        key="edit"
                        initial={{ opacity: 0, x: 10 }}
                        animate={{ opacity: 1, x: 0 }}
                        exit={{ opacity: 0, x: -10 }}
                        className="space-y-5 pt-2 pb-10"
                    >
                        <div className="space-y-4">
                            <div className="flex gap-4 items-center">
                              <div className="w-20 h-20 shrink-0">
                                <ImageUpload 
                                  value={formData.logoUrl} 
                                  onChange={(val) => setFormData({...formData, logoUrl: val || 'default.png'})} 
                                  placeholder="Subir Logo"
                                  className="rounded-lg"
                                />
                              </div>
                              <div className="flex-1 space-y-4">
                                <div className="space-y-1">
                                    <Label className="text-[9px] font-bold uppercase text-muted-foreground">Nombre</Label>
                                    <Input value={formData.nombre} onChange={e => setFormData({...formData, nombre: e.target.value})} className="h-9 text-sm" />
                                </div>
                              </div>
                            </div>
                            <div className="space-y-1">
                                <Label className="text-[9px] font-bold uppercase text-muted-foreground">Objetivo</Label>
                                <Input value={formData.descripcion} onChange={e => setFormData({...formData, descripcion: e.target.value})} className="h-9 text-xs" />
                            </div>
                        </div>

                        <div className="space-y-2 pt-4">
                            <Label className="text-[9px] font-bold uppercase text-muted-foreground mb-2 block border-b border-border/10 pb-1">Administrar Personal</Label>
                            <div className="space-y-1">
                                {usuarios.map(u => (
                                    <div key={u.id} className={cn("flex items-center justify-between p-2 rounded-md border text-xs", selectedUserIds.includes(u.id) ? "bg-primary/[0.01] border-primary/20" : "border-border/10")}>
                                        <div className="flex items-center gap-2 flex-1 min-w-0">
                                            <Checkbox 
                                                checked={selectedUserIds.includes(u.id)} 
                                                onCheckedChange={(checked) => {
                                                    if (checked) setSelectedUserIds([...selectedUserIds, u.id]);
                                                    else {
                                                        setSelectedUserIds(selectedUserIds.filter(id => id !== u.id));
                                                        setManagerIds(managerIds.filter(id => id !== u.id));
                                                    }
                                                }} 
                                            />
                                            <span className={cn("truncate font-semibold", selectedUserIds.includes(u.id) ? "text-primary" : "text-muted-foreground")}>{u.nombre}</span>
                                        </div>
                                        {selectedUserIds.includes(u.id) && (
                                            <button 
                                                onClick={() => {
                                                    if (managerIds.includes(u.id)) setManagerIds(managerIds.filter(i => i !== u.id));
                                                    else setManagerIds([...managerIds, u.id]);
                                                }}
                                                className={cn("p-1 transition-colors", managerIds.includes(u.id) ? "text-amber-500" : "text-muted-foreground/20 hover:text-amber-500/40")}
                                            >
                                                <Star className={cn("h-4 w-4", managerIds.includes(u.id) && "fill-current")} />
                                            </button>
                                        )}
                                    </div>
                                ))}
                            </div>
                        </div>
                    </motion.div>
                )}
            </AnimatePresence>
          </ScrollArea>

          <div className="p-4 border-t border-border/10 bg-background/50 flex gap-2">
             {sidebarMode === "view" ? (
                <>
                    <Button variant="outline" className="flex-1 h-9 text-[10px] font-bold uppercase" onClick={() => setIsDetailOpen(false)}>Cerrar</Button>
                    <Button className="flex-1 h-9 text-[10px] font-bold uppercase gap-2 bg-primary" onClick={() => setSidebarMode("edit")}>
                        <Edit className="h-3 w-3" /> Editar Equipo
                    </Button>
                </>
             ) : (
                <>
                    <Button variant="outline" className="flex-1 h-9 text-[10px] font-bold uppercase" onClick={() => editingEquipo ? setSidebarMode("view") : setIsDetailOpen(false)}>
                        Cancelar
                    </Button>
                    <Button className="flex-1 h-9 text-[10px] font-bold uppercase bg-primary" onClick={handleGlobalSave}>
                        Guardar Cambios
                    </Button>
                </>
             )}
          </div>
        </SheetContent>
      </Sheet>
    </div>
  );
}
