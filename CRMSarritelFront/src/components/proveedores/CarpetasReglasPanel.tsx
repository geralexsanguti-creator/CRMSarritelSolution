import { useState, useEffect } from 'react';
import { Plus, Edit, Trash2, Folder, Check, LayoutGrid, X, Search } from 'lucide-react';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Badge } from '@/components/ui/badge';
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter, DialogDescription } from '@/components/ui/dialog';
import { Switch } from '@/components/ui/switch';
import { ScrollArea } from '@/components/ui/scroll-area';
import { toast } from 'sonner';
import { useAuth } from '@/hooks/useAuth';
import { cn } from '@/lib/utils';
import type { ReglaComision, CarpetaReglas, CarpetaReglasCreateDto } from '@/types/proveedor.types';
import { carpetasReglasService } from '@/services/api/carpetasReglas.service';

interface CarpetasReglasPanelProps {
  proveedorId: number;
  reglas: ReglaComision[];
}

const getRuleRoles = (regla: ReglaComision) => {
  const roles = new Set<string>();
  // Base splits
  regla.reparticionesBase?.forEach(r => {
    if (r.rol?.nombre) roles.add(r.rol.nombre);
    if (r.equipo?.nombre) roles.add(`Líder ${r.equipo.nombre}`);
  });
  // Tier splits
  regla.tiers?.forEach(t => {
    t.reparticiones?.forEach(r => {
      if (r.rol?.nombre) roles.add(r.rol.nombre);
      if (r.equipo?.nombre) roles.add(`Líder ${r.equipo.nombre}`);
    });
  });
  return Array.from(roles);
};

const getRuleFolder = (regla: ReglaComision) => {
  // Support both camelCase and PascalCase
  const list = regla.carpetaReglasComision || (regla as any).CarpetaReglasComision;
  if (!list || !Array.isArray(list)) return null;

  const folders = list.filter(crc => {
    const folder = crc.carpetaReglas || crc.CarpetaReglas;
    return folder && (folder.activo !== false);
  });
  
  if (folders.length > 0) {
    const folder = folders[0].carpetaReglas || folders[0].CarpetaReglas;
    return folder?.nombre;
  }
  return null;
};

export function CarpetasReglasPanel({ proveedorId, reglas }: CarpetasReglasPanelProps) {
  const { hasPermission } = useAuth();
  const canEdit = hasPermission('proveedores:update');

  const [carpetas, setCarpetas] = useState<CarpetaReglas[]>([]);
  const [loading, setLoading] = useState(true);
  
  const [showModal, setShowModal] = useState(false);
  const [editing, setEditing] = useState<CarpetaReglas | null>(null);
  const [form, setForm] = useState<CarpetaReglasCreateDto>({
    nombre: '',
    activo: true,
    proveedorId,
    reglaIds: []
  });
  const [searchTerm, setSearchTerm] = useState('');

  const loadData = async () => {
    setLoading(true);
    try {
      // Backend returns all. In a real scenario we'd query by proveedorId
      // but for now we filter locally or rely on the array returned.
      // Wait, I should add a query string or just filter locally.
      const all = await carpetasReglasService.getAll();
      setCarpetas(all.filter(c => c.proveedorId === proveedorId));
    } catch {
      toast.error('Error al cargar carpetas');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadData();
  }, [proveedorId]);

  const handleOpenModal = (carpeta?: CarpetaReglas) => {
    if (carpeta) {
      setEditing(carpeta);
      setForm({
        id: carpeta.id,
        nombre: carpeta.nombre,
        activo: carpeta.activo,
        proveedorId,
        reglaIds: carpeta.carpetaReglasComision?.map(c => c.reglaComisionId) || []
      });
    } else {
      setEditing(null);
      setForm({
        nombre: '',
        activo: true,
        proveedorId,
        reglaIds: []
      });
    }
    setSearchTerm('');
    setShowModal(true);
  };

  const handleSave = async () => {
    if (!form.nombre) return toast.error('Debes proporcionar un nombre');
    try {
      if (editing) {
        await carpetasReglasService.update(editing.id, form);
        toast.success('Carpeta actualizada');
      } else {
        await carpetasReglasService.create(form);
        toast.success('Carpeta creada');
      }
      setShowModal(false);
      loadData();
    } catch {
      toast.error('Error al guardar la carpeta');
    }
  };

  const handleDelete = async (id: number) => {
    if (!confirm('¿Eliminar esta carpeta? Las Reglas seguirán existiendo, solo desaparece la asociación.')) return;
    try {
      await carpetasReglasService.delete(id);
      toast.success('Carpeta eliminada');
      setCarpetas(c => c.filter(x => x.id !== id));
    } catch {
      toast.error('Error al eliminar');
    }
  };

  const toggleRegla = (id: number) => {
    setForm(f => {
      if (f.reglaIds.includes(id)) {
        return { ...f, reglaIds: f.reglaIds.filter(x => x !== id) };
      }
      return { ...f, reglaIds: [...f.reglaIds, id] };
    });
  };

  if (loading) {
    return <div className="h-20 flex items-center justify-center animate-pulse text-muted-foreground">Cargando carpetas...</div>;
  }

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <div>
          <h4 className="text-[10px] font-bold text-muted-foreground uppercase tracking-widest">
            Carpetas y Paquetes de Reglas ({carpetas.length})
          </h4>
        </div>
        {canEdit && (
          <Button size="sm" onClick={() => handleOpenModal()}
            className="h-7 px-3 text-xs bg-primary hover:bg-primary/90 shadow-glow font-bold gap-1">
            <Plus className="w-3 h-3" /> Nueva Carpeta
          </Button>
        )}
      </div>

      {carpetas.length === 0 ? (
        <div className={cn('h-24 border rounded-xl bg-card border-dashed flex items-center justify-center flex-col gap-2')}>
          <Folder className="w-6 h-6 text-muted-foreground/40" />
          <p className="text-xs text-muted-foreground text-center max-w-xs">
            Crea carpetas para agrupar diferentes reglas de comisión y asignarlas a los productos fácilmente.
          </p>
        </div>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-3">
          {carpetas.map(c => (
            <div key={c.id} className={cn(
              "rounded-xl border p-3 bg-card/60 relative group transition-all",
              !c.activo && "opacity-60"
            )}>
              <div className="flex justify-between items-center">
                <div className="flex gap-2">
                  <div className={cn(
                    "w-8 h-8 rounded-lg flex items-center justify-center shrink-0",
                    c.activo ? "bg-amber-500/20 text-amber-500" : "bg-muted text-muted-foreground"
                  )}>
                    <Folder className="w-4 h-4 fill-current opacity-20" />
                  </div>
                  <div>
                    <h5 className="font-bold text-[13px] leading-tight text-foreground truncate max-w-[140px] sm:max-w-none">{c.nombre}</h5>
                    <p className="text-[10px] text-muted-foreground font-mono mt-0.5">
                      {c.carpetaReglasComision?.length || 0} REGLAS
                    </p>
                  </div>
                </div>
                {canEdit && (
                  <div className="flex opacity-0 group-hover:opacity-100 transition-opacity">
                    <Button variant="ghost" size="icon" className="h-6 w-6" onClick={() => handleOpenModal(c)}>
                      <Edit className="w-3 h-3" />
                    </Button>
                    <Button variant="ghost" size="icon" className="h-6 w-6 text-destructive" onClick={() => handleDelete(c.id)}>
                      <Trash2 className="w-3 h-3" />
                    </Button>
                  </div>
                )}
              </div>

            </div>
          ))}
        </div>
      )}

      <Dialog open={showModal} onOpenChange={setShowModal}>
        <DialogContent className="glass border-white/10 max-w-2xl max-h-[85vh] p-0 overflow-hidden flex flex-col">
          <DialogHeader className="p-6 pb-2">
            <DialogTitle className="text-lg font-bold flex items-center gap-2">
              <Folder className="w-5 h-5 text-amber-500" />
              {editing ? 'Editar Paquete' : 'Nuevo Paquete de Reglas'}
            </DialogTitle>
            <DialogDescription className="sr-only">Formulario para configurar paquetes de reglas de comisión</DialogDescription>
          </DialogHeader>

          <div className="p-6 pt-2 flex-1 overflow-y-auto space-y-6">
            <div className="space-y-4 bg-muted/20 p-4 rounded-xl border border-white/5">
              <div className="space-y-2 flex-1">
                <Label className="text-xs font-bold text-muted-foreground uppercase">Nombre de la Carpeta</Label>
                <Input value={form.nombre} onChange={e => setForm({ ...form, nombre: e.target.value })}
                  placeholder="Ej: Paquete Oro - Ventas de Fibra" className="bg-background/50 h-10 border-white/5" />
              </div>
              <div className="flex items-center gap-3">
                <Switch checked={form.activo} onCheckedChange={c => setForm({ ...form, activo: c })} />
                <Label className="text-sm font-bold">Carpeta Activa</Label>
              </div>
            </div>

            <div className="space-y-2">
              <Label className="text-[10px] font-black uppercase text-primary tracking-widest flex items-center gap-1.5">
                <LayoutGrid className="w-3 h-3" />
                Asignación de Reglas 
                <Badge variant="secondary" className="ml-2 font-mono text-[9px]">{form.reglaIds.length} Asignadas</Badge>
              </Label>
              
              <div className="relative mt-2">
                <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-3.5 h-3.5 text-muted-foreground" />
                <Input 
                  placeholder="Buscar regla por nombre o variable..." 
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                  className="pl-9 bg-background/50 h-9 text-xs border-white/5"
                />
              </div>

              <ScrollArea className="h-[300px] mt-2 pr-4 -mr-4">
                <div className="grid grid-cols-1 sm:grid-cols-2 gap-2">
                  {reglas
                    .filter(r => 
                      r.nombre.toLowerCase().includes(searchTerm.toLowerCase()) || 
                      r.variable.toLowerCase().includes(searchTerm.toLowerCase())
                    )
                    .map(r => {
                      const seleccionado = form.reglaIds.includes(r.id);
                      return (
                        <button type="button" key={r.id} onClick={() => toggleRegla(r.id)}
                          className={cn(
                            "text-left p-3 rounded-lg border text-sm transition-all focus:outline-none flex gap-3",
                            seleccionado 
                              ? "bg-primary/10 border-primary text-foreground shadow-sm ring-1 ring-primary/20" 
                              : "bg-background/40 hover:bg-muted/40 border-border/40 text-muted-foreground"
                          )}>
                          <div className={cn(
                            "w-4 h-4 rounded-full border shrink-0 flex items-center justify-center mt-0.5",
                            seleccionado ? "border-primary bg-primary text-primary-foreground" : "border-muted-foreground"
                          )}>
                            {seleccionado && <Check className="w-2.5 h-2.5" />}
                          </div>
                          <div className="flex-1 min-w-0">
                            <div className="font-bold truncate text-[11px] text-foreground">{r.nombre}</div>
                            <div className="flex flex-wrap gap-1 mt-1 font-bold">
                               <Badge variant={r.activa ? "secondary" : "outline"} className={cn(
                                 "text-[8px] px-1 h-3.5 uppercase tracking-tighter shadow-none",
                                 r.activa ? "bg-emerald-500/10 text-emerald-600 border-emerald-500/10" : "opacity-40"
                               )}>
                                 {r.activa ? 'Activa' : 'Desactivada'}
                               </Badge>
                               
                               {getRuleRoles(r).slice(0, 3).map(role => (
                                 <Badge key={role} variant="outline" className="text-[8px] px-1 h-3.5 border-primary/20 text-primary/70 font-normal bg-primary/5">
                                   {role}
                                 </Badge>
                               ))}
                               {getRuleRoles(r).length > 3 && <span className="text-[8px] text-muted-foreground">+{getRuleRoles(r).length - 3}</span>}
                            </div>
                            <div className="text-[9px] text-muted-foreground/60 mt-1 truncate font-mono uppercase tracking-widest flex items-center gap-2">
                               <span>{r.variable}</span>
                               {getRuleFolder(r) && (
                                 <>
                                   <span className="opacity-40">•</span>
                                   <div className="flex items-center gap-1 text-primary/70 font-bold">
                                     <Folder className="w-2.5 h-2.5" />
                                     <span className="truncate">{getRuleFolder(r)}</span>
                                   </div>
                                 </>
                               )}
                            </div>
                          </div>
                        </button>
                      );
                    })}
                  {reglas.filter(r => 
                    r.nombre.toLowerCase().includes(searchTerm.toLowerCase()) || 
                    r.variable.toLowerCase().includes(searchTerm.toLowerCase())
                  ).length === 0 && (
                    <div className="col-span-2 p-6 text-center text-xs text-muted-foreground border border-dashed rounded-lg">
                      {reglas.length === 0 ? "No hay reglas creadas." : "No se encontraron reglas con ese filtro."}
                    </div>
                  )}
                </div>
              </ScrollArea>
            </div>
          </div>

          <DialogFooter className="p-4 bg-muted/40 border-t gap-2">
            <Button variant="ghost" onClick={() => setShowModal(false)}>Cancelar</Button>
            <Button onClick={handleSave} className="bg-primary shadow-glow font-bold gap-2">
              <Check className="w-4 h-4" /> Guardar Carpeta
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
}
