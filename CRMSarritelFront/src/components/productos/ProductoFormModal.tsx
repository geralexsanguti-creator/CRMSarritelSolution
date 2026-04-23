import { useState, useEffect, useMemo } from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogDescription } from "@/components/ui/dialog";
import { Search } from "lucide-react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Switch } from "@/components/ui/switch";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Producto } from "@/types";
import { Plus, Trash2, Settings, Box, Scale, Info, Layers, Tag as TagIcon, Calendar as CalendarIcon, CheckSquare, Image as ImageIcon, Upload } from "lucide-react";
import { ReglasComisionService, ProveedoresService, TiposVentaService } from "@/services/api/proveedores.service";
import { carpetasReglasService } from "@/services/api/carpetasReglas.service";
import { ReglaComision, Proveedor, TipoVenta, CarpetaReglas } from "@/types/proveedor.types";
import { Badge } from "@/components/ui/badge";
import { cn } from "@/lib/utils";
import { motion, AnimatePresence } from "framer-motion";
import { Calendar } from "@/components/ui/calendar";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { format } from "date-fns";
import { es } from "date-fns/locale";
import { toast } from "sonner";
import { ImageUpload } from "@/components/shared/ImageUpload";


interface ProductFormModalProps {
  open: boolean;
  onClose: () => void;
  product?: Producto | null;
  onSave: (product: Partial<Producto>) => void;
  categories?: string[]; // Kept for interface compatibility but we use CategoriasService
}

export function ProductoFormModal({ open, onClose, product, onSave }: ProductFormModalProps) {
  const [formData, setFormData] = useState<Partial<Producto>>({
    nombre: "",
    descripcion: "",
    precio: 0,
    precioOferta: 0,
    cantidad: 0,
    tipoVentaId: undefined,
    imagen: "default.png",
    carpetaIds: [],
    proveedorId: undefined,
    esInfinito: false,
    fechaLimite: undefined,
    activo: true,
    fechaActivacion: undefined,
    fechaBaja: undefined
  });
  const [folderSearch, setFolderSearch] = useState('');

  const [carpetasDisponibles, setCarpetasDisponibles] = useState<CarpetaReglas[]>([]);
  const [proveedores, setProveedores] = useState<Proveedor[]>([]);
  const [tiposVenta, setTiposVenta] = useState<TipoVenta[]>([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const [carpetasData, provData, tvData] = await Promise.all([
          carpetasReglasService.getAll(),
          ProveedoresService.getAll(),
          TiposVentaService.getAll()
        ]);
        setCarpetasDisponibles(carpetasData);
        setProveedores(provData);
        setTiposVenta(tvData);
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        setLoading(false);
      }
    };

    if (open) {
      fetchData();
    }
  }, [open]);

  // Tipos de venta que opera el proveedor seleccionado
  const availableTiposVenta = useMemo(() => {
    if (!formData.proveedorId) return [];
    const selectedProv = proveedores.find(p => p.id === formData.proveedorId);
    return selectedProv?.tiposVenta || [];
  }, [proveedores, formData.proveedorId]);

  const filteredCarpetas = useMemo(() => {
    if (!formData.proveedorId) return [];
    return carpetasDisponibles.filter(c => c.proveedorId === formData.proveedorId);
  }, [carpetasDisponibles, formData.proveedorId]);

  const selectedCarpetas = useMemo(() => {
    if (!formData.carpetaIds || formData.carpetaIds.length === 0) return [];
    return carpetasDisponibles.filter(c => formData.carpetaIds?.includes(c.id));
  }, [carpetasDisponibles, formData.carpetaIds]);

  useEffect(() => {
    if (product) {
      setFormData({
        ...product,
        carpetaIds: product.productoCarpetas?.map(pc => pc.carpetaReglasId) || product.carpetaIds || []
      });
      setFolderSearch('');
    } else {
      setFormData({
        nombre: "",
        descripcion: "",
        precio: 0,
        precioOferta: 0,
        cantidad: 0,
        tipoVentaId: undefined,
        imagen: "default.png",
        carpetaIds: [],
        proveedorId: undefined,
        esInfinito: false,
        fechaLimite: undefined,
        activo: true,
        fechaActivacion: undefined,
        fechaBaja: undefined
      });
    }
  }, [product, open]);

  // Auto-fill TipoVentaId if only one folder is selected and it has one type
  useEffect(() => {
    if (selectedCarpetas.length === 1 && !formData.tipoVentaId) {
       // Since the folder has rules, and rules have types, we could theoretically find it.
       // For now, let the user select the type explicitly or infer from rules.
    }
  }, [selectedCarpetas]);


  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!formData.proveedorId || !formData.tipoVentaId || !formData.carpetaIds || formData.carpetaIds.length === 0) {
      alert("Es obligatorio seleccionar proveedor, tipo de venta y al menos un paquete de reglas.");
      return;
    }

    const finalData = { ...formData };
    
    // Evitar errores de tracking en el backend eliminando las propiedades de navegación
    delete finalData.tipoVenta;
    delete finalData.proveedor;
    delete finalData.productoCarpetas;

    // Si es infinito, la cantidad es nula (no determinada)
    if (finalData.esInfinito) {
      finalData.cantidad = null as any; // Typecast to satisfy Partial<Producto> if needed
    }

    onSave(finalData);
  };

  return (
    <Dialog open={open} onOpenChange={(o) => !o && onClose()}>
      <DialogContent className="glass border-border max-w-2xl max-h-[92vh] flex flex-col p-0 shadow-2xl overflow-hidden">
        {/* Header sticky */}
        <div className="shrink-0 px-6 pt-6 pb-4 border-b border-border/40 bg-background/60 backdrop-blur-sm">
          <div className="flex items-center justify-between">
            <div className="flex items-center gap-3">
              <div className="h-10 w-10 rounded-xl bg-primary/10 flex items-center justify-center border border-primary/20 shrink-0">
                <Box className="h-6 w-6 text-primary" />
              </div>
              <div>
                <DialogTitle className="text-xl font-bold text-foreground leading-tight">
                  {product ? "Editar Producto" : "Nuevo Producto"}
                </DialogTitle>
                <DialogDescription className="text-xs text-muted-foreground font-medium mt-0.5">
                  Define las reglas y características del producto comercial
                </DialogDescription>
              </div>
            </div>
          </div>
        </div>

        <div className="flex-1 overflow-y-auto px-6 py-5 custom-scrollbar">
          <form id="product-form" onSubmit={handleSubmit} className="space-y-8">
            
            {/* Image Upload */}
            <div className="space-y-4">
              <div className="flex items-center gap-2 border-b border-border/40 pb-2">
                <ImageIcon className="h-4 w-4 text-primary" />
                <h3 className="text-xs font-bold text-foreground uppercase tracking-widest">Imagen del Producto</h3>
              </div>
              
              <div className="flex items-center gap-6 p-4 bg-muted/20 rounded-xl border border-dashed border-border/60">
                <div className="w-32 h-32 shrink-0">
                  <ImageUpload 
                    value={formData.imagen} 
                    onChange={(val) => setFormData({...formData, imagen: val || 'default.png'})} 
                    placeholder="Subir Logo"
                  />
                </div>
                <div className="flex-1 space-y-2">
                  <Label className="text-[10px] text-muted-foreground uppercase font-black tracking-tighter">URL de la Imagen</Label>
                  <Input 
                    placeholder="URL Manual (o usa el botón de subir)"
                    value={formData.imagen === 'default.png' ? '' : (formData.imagen ?? '')} 
                    onChange={e => setFormData({...formData, imagen: e.target.value || 'default.png'})}
                    className="h-9 bg-background/40 border-border/40 text-xs"
                  />
                  <p className="text-[9px] text-muted-foreground italic mt-2">Sube una imagen o pega una URL. Se recomienda una imagen cuadrada de al menos 400x400px.</p>
                </div>
              </div>
            </div>

            {/* Basic Info */}
            <div className="space-y-6">
              <div className="flex items-center gap-2 border-b border-border/40 pb-2">
                <Info className="h-4 w-4 text-primary" />
                <h3 className="text-xs font-bold text-foreground uppercase tracking-widest">Información General</h3>
              </div>
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                {/* 0. ESTADO */}
                <div className="col-span-1 md:col-span-2 flex items-center justify-between p-4 bg-primary/5 border border-primary/20 rounded-xl">
                  <div className="space-y-1">
                    <Label className="text-sm font-bold text-primary">Estado del Producto</Label>
                    <p className="text-xs text-muted-foreground">Si desactivas, no estará disponible para nuevas ventas.</p>
                  </div>
                  <div className="flex items-center gap-3">
                    <Label htmlFor="activo-switch" className="text-xs font-bold uppercase">{formData.activo ? 'ACTIVO' : 'INACTIVO'}</Label>
                    <Switch
                      id="activo-switch"
                      checked={formData.activo}
                      onCheckedChange={(checked) => {
                        if (!checked && product) {
                           if (!window.confirm("¿Seguro que deseas desactivar este producto? Dejará de estar disponible para futuros contratos.")) return;
                        }
                        setFormData({...formData, activo: checked})
                      }}
                    />
                  </div>
                </div>

                {/* Fechas de Actividad */}
                <div className="col-span-1 md:col-span-2 grid grid-cols-1 md:grid-cols-2 gap-4">
                   <div className="space-y-2">
                     <Label className="text-[10px] text-muted-foreground uppercase font-black tracking-tighter">Fecha de Activación</Label>
                     <Popover>
                       <PopoverTrigger asChild>
                         <Button variant={"outline"} className={cn("flex-1 w-full h-11 justify-start text-left font-normal bg-background/40 border-border/40", !formData.fechaActivacion && "text-muted-foreground")}>
                           <CalendarIcon className="mr-2 h-4 w-4" />
                           {formData.fechaActivacion ? format(new Date(formData.fechaActivacion), "PPP", { locale: es }) : <span>Auto (Creación)</span>}
                         </Button>
                       </PopoverTrigger>
                       <PopoverContent className="w-auto p-0 glass" align="start">
                         <Calendar mode="single" selected={formData.fechaActivacion ? new Date(formData.fechaActivacion) : undefined} onSelect={(date) => setFormData({...formData, fechaActivacion: date?.toISOString()})} initialFocus />
                       </PopoverContent>
                     </Popover>
                   </div>
                   <div className="space-y-2">
                     <Label className="text-[10px] text-muted-foreground uppercase font-black tracking-tighter">Fecha de Baja (Fin Operatividad)</Label>
                     <div className="flex gap-2">
                       <Popover>
                         <PopoverTrigger asChild>
                           <Button variant={"outline"} disabled={formData.activo} className={cn("flex-1 h-11 justify-start text-left font-normal bg-background/40 border-border/40", !formData.fechaBaja && "text-muted-foreground")}>
                             <CalendarIcon className="mr-2 h-4 w-4" />
                             {formData.fechaBaja ? format(new Date(formData.fechaBaja), "PPP", { locale: es }) : <span>{formData.activo ? 'Hasta Desactivar' : 'Baja'}</span>}
                           </Button>
                         </PopoverTrigger>
                         <PopoverContent className="w-auto p-0 glass" align="start">
                           <Calendar mode="single" selected={formData.fechaBaja ? new Date(formData.fechaBaja) : undefined} onSelect={(date) => setFormData({...formData, fechaBaja: date?.toISOString()})} initialFocus />
                         </PopoverContent>
                       </Popover>
                       {formData.fechaBaja && !formData.activo && (
                          <Button variant="ghost" size="icon" className="h-11 w-11 text-destructive hover:bg-destructive/10" onClick={() => setFormData({...formData, fechaBaja: undefined})}><Trash2 className="h-4 w-4" /></Button>
                       )}
                     </div>
                   </div>
                </div>

                <div className="col-span-1 md:col-span-2 space-y-2">
                  <Label className="text-[10px] text-muted-foreground uppercase font-black tracking-tighter">Nombre del Producto comercial</Label>
                  <Input 
                    required 
                    placeholder="Ej: Fibra 1GB + TV Total"
                    value={formData.nombre ?? ''} 
                    onChange={e => setFormData({...formData, nombre: e.target.value})} 
                    className="bg-background/40 border-border/40 h-12 text-lg font-medium focus:ring-primary/20" 
                  />
                </div>

                {/* 1. SELECCIONAR PROVEEDOR */}
                <div className="col-span-1 md:col-span-2 space-y-2">
                  <Label className="text-[10px] text-muted-foreground uppercase font-black tracking-tighter">Proveedor de servicio *</Label>
                  <Select 
                    required
                    value={formData.proveedorId ? String(formData.proveedorId) : ""} 
                    onValueChange={v => setFormData({...formData, proveedorId: Number(v), tipoVentaId: undefined, carpetaIds: []})}
                  >
                    <SelectTrigger className="bg-background/40 border-border/40 h-11">
                      <TagIcon className="h-4 w-4 mr-2 text-muted-foreground" />
                      <SelectValue placeholder="Seleccionar proveedor" />
                    </SelectTrigger>
                    <SelectContent className="glass">
                      {proveedores.map(p => (
                        <SelectItem key={p.id} value={String(p.id)}>{p.nombre}</SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                </div>

                <div className="col-span-1 md:col-span-2 grid grid-cols-1 md:grid-cols-2 gap-4 bg-muted/20 p-4 rounded-xl border border-border/40">
                  <div className="space-y-2">
                    <Label className="text-[10px] text-muted-foreground uppercase font-black tracking-tighter">Stock / Inventario</Label>
                    <div className="flex items-center gap-3 bg-background/60 p-3 rounded-lg border border-border/40 h-11">
                      <Switch 
                        id="es-infinito"
                        checked={formData.esInfinito} 
                        onCheckedChange={(checked) => setFormData({...formData, esInfinito: checked})} 
                      />
                      <Label htmlFor="es-infinito" className="text-xs font-bold uppercase cursor-pointer">Stock Infinito</Label>
                    </div>
                  </div>

                  {!formData.esInfinito && (
                    <div className="space-y-2 animate-in fade-in slide-in-from-left-2">
                      <Label className="text-[10px] text-muted-foreground uppercase font-black tracking-tighter">Cantidad Disponible</Label>
                      <Input 
                        required 
                        type="number" 
                        value={formData.cantidad === 0 ? '' : (formData.cantidad ?? '')} 
                        onChange={e => setFormData({...formData, cantidad: e.target.value === '' ? '' : Number(e.target.value) as any})} 
                        className="h-11 bg-background/60 font-bold border-border/40" 
                      />
                    </div>
                  )}
                </div>

                <div className="col-span-1 md:col-span-2 space-y-2">
                  <Label className="text-[10px] text-muted-foreground uppercase font-black tracking-tighter">Descripción Comercial</Label>
                  <Input 
                    required 
                    placeholder="Breve descripción para el comercial..."
                    value={formData.descripcion ?? ''} 
                    onChange={e => setFormData({...formData, descripcion: e.target.value})} 
                    className="bg-background/40 border-border/40 h-11" 
                  />
                </div>

                {/* Fecha Limite */}
                <div className="col-span-1 md:col-span-2 space-y-2">
                  <Label className="text-[10px] text-muted-foreground uppercase font-black tracking-tighter">Fecha Límite de Oferta / Venta</Label>
                  <div className="flex gap-2">
                    <Popover>
                      <PopoverTrigger asChild>
                        <Button
                          variant={"outline"}
                          className={cn(
                            "flex-1 h-11 justify-start text-left font-normal bg-background/40 border-border/40",
                            !formData.fechaLimite && "text-muted-foreground"
                          )}
                        >
                          <CalendarIcon className="mr-2 h-4 w-4" />
                          {formData.fechaLimite ? format(new Date(formData.fechaLimite), "PPP", { locale: es }) : <span>Seleccionar fecha límite</span>}
                        </Button>
                      </PopoverTrigger>
                      <PopoverContent className="w-auto p-0 glass" align="start">
                        <Calendar
                          mode="single"
                          selected={formData.fechaLimite ? new Date(formData.fechaLimite) : undefined}
                          onSelect={(date) => setFormData({...formData, fechaLimite: date?.toISOString()})}
                          initialFocus
                        />
                      </PopoverContent>
                    </Popover>
                    {formData.fechaLimite && (
                      <Button 
                        variant="ghost" 
                        size="icon" 
                        className="h-11 w-11 text-destructive hover:bg-destructive/10"
                        onClick={() => setFormData({...formData, fechaLimite: undefined})}
                      >
                        <Trash2 className="h-4 w-4" />
                      </Button>
                    )}
                  </div>
                  <p className="text-[9px] text-muted-foreground italic">El producto dejará de estar disponible después de esta fecha</p>
                </div>
              </div>
            </div>

            {/* Rule Configuration Section */}
            <div className="space-y-6 pt-2">
              <div className="flex items-center gap-2 border-b border-border/40 pb-2">
                <Scale className="h-4 w-4 text-primary" />
                <h3 className="text-xs font-bold text-foreground uppercase tracking-widest">Configuración de Regla</h3>
              </div>
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-6 items-start">
                {/* 3. SELECCIONAR CARPETAS */}
                <div className="col-span-1 md:col-span-2 space-y-2">
                  <div className="flex items-center justify-between">
                    <Label className="text-[10px] text-muted-foreground uppercase font-black tracking-tighter">Asignar Paquetes (Regla) *</Label>
                    <div className="relative w-40 sm:w-56">
                      <Search className="absolute left-2.5 top-1/2 -translate-y-1/2 w-3 h-3 text-muted-foreground" />
                      <Input 
                        placeholder="Buscar carpeta..." 
                        value={folderSearch}
                        onChange={(e) => setFolderSearch(e.target.value)}
                        className="h-7 pl-8 text-[10px] bg-muted/20 border-white/5"
                      />
                    </div>
                  </div>
                  
                  <div className="p-4 border border-border/40 bg-muted/10 rounded-xl">
                    <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
                      {filteredCarpetas
                        .filter(c => c.nombre.toLowerCase().includes(folderSearch.toLowerCase()))
                        .map(c => {
                          const isSelected = formData.carpetaIds?.includes(c.id);
                          return (
                            <button key={c.id} type="button" 
                              onClick={() => {
                                const current = formData.carpetaIds || [];
                                const next = isSelected ? current.filter(id => id !== c.id) : [...current, c.id];
                                setFormData({...formData, carpetaIds: next});
                              }}
                              className={cn(
                                "flex items-center gap-3 p-3 rounded-lg border text-left transition-all",
                                isSelected 
                                  ? "bg-primary/10 border-primary ring-1 ring-primary/20" 
                                  : "bg-background/40 border-border/40 hover:bg-muted/40"
                              )}>
                              <div className={cn(
                                "w-4 h-4 rounded-full border flex items-center justify-center shrink-0",
                                isSelected ? "bg-primary border-primary text-primary-foreground" : "border-muted-foreground"
                              )}>
                                {isSelected && <CheckSquare className="w-3 h-3" />}
                              </div>
                              <div className="flex-1 min-w-0">
                                <span className="text-xs font-bold truncate block">{c.nombre}</span>
                                <span className="text-[9px] opacity-60 uppercase font-mono">{c.carpetaReglasComision?.length || 0} Reglas</span>
                              </div>
                            </button>
                          );
                        })}
                      {filteredCarpetas.filter(c => c.nombre.toLowerCase().includes(folderSearch.toLowerCase())).length === 0 && (
                        <div className="col-span-2 p-4 text-center text-xs text-muted-foreground border border-dashed rounded-lg">
                          {formData.proveedorId 
                            ? (filteredCarpetas.length === 0 ? "No existen paquetes de reglas para este proveedor." : "No se encontraron carpetas con ese nombre.")
                            : "Primero seleccione un proveedor."}
                        </div>
                      )}
                    </div>
                  </div>
                </div>

                <div className="col-span-1 md:col-span-2 space-y-2">
                  <Label className="text-[10px] text-muted-foreground uppercase font-black tracking-tighter">Tipo de Venta *</Label>
                  <Select 
                    required
                    disabled={!formData.proveedorId}
                    value={formData.tipoVentaId ? String(formData.tipoVentaId) : ""} 
                    onValueChange={v => setFormData({...formData, tipoVentaId: Number(v)})}
                  >
                    <SelectTrigger className="bg-background/40 border-border/40 h-11">
                      <SelectValue placeholder="Seleccionar tipo de venta" />
                    </SelectTrigger>
                    <SelectContent className="glass">
                      {availableTiposVenta.map(tv => (
                        <SelectItem key={tv.id} value={String(tv.id)}>{tv.nombre}</SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                </div>
              </div>

              <AnimatePresence>
                {selectedCarpetas.length > 0 && (
                  <motion.div 
                    initial={{ opacity: 0, scale: 0.95, y: 10 }}
                    animate={{ opacity: 1, scale: 1, y: 0 }}
                    exit={{ opacity: 0, scale: 0.95, y: 10 }}
                    className="p-4 rounded-xl border border-primary/20 bg-primary/5 shadow-inner"
                  >
                    <div className="flex items-center justify-between">
                      <div className="flex flex-col">
                        <span className="text-[10px] uppercase font-black text-primary tracking-widest mb-1">Configuración Multi-Regla Activa</span>
                        <div className="flex items-center gap-2">
                          <Layers className="h-4 w-4 text-primary" />
                          <span className="text-lg font-black tracking-tighter">
                            {selectedCarpetas.reduce((acc, c) => acc + (c.carpetaReglasComision?.length || 0), 0)} Reglas Vinculadas
                          </span>
                        </div>
                      </div>
                      <div className="flex flex-col items-end gap-1 text-right">
                         <Badge variant="outline" className="bg-background/80 border-primary/30 text-primary uppercase text-[9px] font-black h-5">Paquetes {selectedCarpetas.length}</Badge>
                         <span className="text-[10px] text-muted-foreground font-medium">Asignación dinámica por carpetas</span>
                      </div>
                    </div>
                  </motion.div>
                )}
              </AnimatePresence>
            </div>
          </form>
        </div>
        
        <div className="p-6 pt-4 border-t border-border bg-muted/20 shrink-0">
          <div className="flex justify-end gap-3">
            <Button variant="ghost" type="button" onClick={onClose} className="px-8 font-medium">Cancelar</Button>
            <Button 
                type="submit" 
                form="product-form" 
                disabled={!formData.proveedorId || !formData.tipoVentaId || !formData.carpetaIds || formData.carpetaIds.length === 0}
                className="bg-primary hover:bg-primary/90 px-10 font-black uppercase tracking-wider shadow-glow text-primary-foreground disabled:opacity-50 disabled:grayscale transition-all"
            >
              {product ? "Actualizar Producto" : "Finalizar y Crear"}
            </Button>
          </div>
        </div>
      </DialogContent>
    </Dialog>
  );
}
