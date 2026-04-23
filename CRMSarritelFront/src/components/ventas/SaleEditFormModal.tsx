import { useState, useEffect, useMemo } from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { DatePicker } from "@/components/ui/date-picker";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Textarea } from "@/components/ui/textarea";
import { Separator } from "@/components/ui/separator";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Badge } from "@/components/ui/badge";
import { 
  Calendar, 
  Euro, 
  FileText, 
  Info, 
  Save, 
  X, 
  Clock, 
  MapPin, 
  Package, 
  TrendingDown,
  Upload,
  File as FileIcon,
  ArrowRight,
  PlusCircle,
  Trash2
} from "lucide-react";
import { Venta, Cliente, Usuario, DetalleVenta } from "@/types";
import { useUpdateVenta } from "@/hooks/useSales";
import { TiposVentaService } from "@/services/api/proveedores.service";
import { ProductosService } from "@/services/api/productos.service";
import { cn } from "@/lib/utils";
import { StatusBadge } from "@/components/shared/StatusBadge";
import { useQuery } from "@tanstack/react-query";
import { PermissionGuard } from "@/components/auth/PermissionGuard";

interface SaleEditFormModalProps {
  open: boolean;
  onClose: () => void;
  sale: Venta;
  clients: Cliente[];
  users: Usuario[];
}

export function SaleEditFormModal({ open, onClose, sale, clients, users }: SaleEditFormModalProps) {
  const updateMutation = useUpdateVenta();
  const [activeTab, setActiveTab] = useState("general");
  const [isSubmitting, setIsSubmitting] = useState(false);

  // Form State
  const [formData, setFormData] = useState<Partial<Venta>>({});

  const { data: allTiposVenta = [] } = useQuery({
    queryKey: ["tipos-venta"],
    queryFn: TiposVentaService.getAll,
    enabled: open
  });

  const { data: productos = [] } = useQuery({
    queryKey: ["productos"],
    queryFn: ProductosService.getAll,
    enabled: open
  });

  const [selectedProductId, setSelectedProductId] = useState<string>("");
  const [productQuantity, setProductQuantity] = useState<number>(1);

  const availableProductsList = useMemo(() => {
    const now = new Date();
    return productos.filter(p => {
      // 1. Activo
      const isActive = p.activo !== false;
      
      // 2. Stock (Infinito o cantidad > 0)
      const hasStock = p.esInfinito || (p.cantidad != null && Number(p.cantidad) > 0);
      
      // 3. Fecha Límite (No pasada)
      const isNotExpired = !p.fechaLimite || new Date(p.fechaLimite) >= now;
      
      // 4. Compatibilidad con Tipo de Venta de la Venta
      // Si la venta tiene un tipo asignado, solo mostrar productos de ese tipo
      const matchesTipoVenta = !sale.tipoVentaId || p.tipoVentaId === sale.tipoVentaId;

      return isActive && hasStock && isNotExpired && matchesTipoVenta;
    });
  }, [productos, sale.tipoVentaId]);

  useEffect(() => {
    if (sale && open) {
      console.log("DEBUG: Editing sale object:", sale);
      setFormData({
        ...sale,
        // Ensure finance fields are numbers
        montoVenta: Number(sale.montoVenta || 0),
        descuentoPorcentaje: Number(sale.descuentoPorcentaje || 0),
        descuentoMonto: Number(sale.descuentoMonto || 0),
        montoTotal: Number(sale.montoTotal || 0),
        // Format dates for HTML input type="date" using local timezone to avoid day shifts
        fechaVenta: sale.fechaVenta ? new Date(sale.fechaVenta).toLocaleDateString('en-CA') : "",
        fechaInstalacionPrevista: sale.fechaInstalacionPrevista ? new Date(sale.fechaInstalacionPrevista).toLocaleDateString('en-CA') : "",
        fechaInstalacionReal: sale.fechaInstalacionReal ? new Date(sale.fechaInstalacionReal).toLocaleDateString('en-CA') : "",
      });
    }
  }, [sale, open]);

  const currentTipoVenta = useMemo(() => {
    return allTiposVenta.find(t => t.id === sale.tipoVentaId || t.codigo === sale.tipoVenta_Codigo);
  }, [allTiposVenta, sale]);

  const availableStates = useMemo(() => {
    if (!currentTipoVenta?.estadosVentaJson) return [];
    try {
      return JSON.parse(currentTipoVenta.estadosVentaJson);
    } catch {
      return [];
    }
  }, [currentTipoVenta]);
  
  const { data: historial = [], refetch: refetchHistorial } = useQuery({
    queryKey: ["sale-history", sale.id],
    queryFn: async () => {
      const response = await fetch(`${import.meta.env.VITE_API_URL}/api/ventas/${sale.id}/historial`, {
        headers: {
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      });
      if (!response.ok) return [];
      return response.json();
    },
    enabled: open && !!sale.id
  });

  const dynamicSchema = useMemo(() => {
    if (!currentTipoVenta?.esquemaVariablesJson) return [];
    try {
      const parsed = typeof currentTipoVenta.esquemaVariablesJson === 'string' 
        ? JSON.parse(currentTipoVenta.esquemaVariablesJson) 
        : currentTipoVenta.esquemaVariablesJson;
      return Array.isArray(parsed) ? parsed : [];
    } catch (e) {
      console.error("Error parsing dynamic schema:", e);
      return [];
    }
  }, [currentTipoVenta]);

  const dynamicValues = useMemo(() => {
    const firstDetail = formData.detalles?.[0];
    if (firstDetail?.datosConfiguracion) {
      try {
        const config = firstDetail.datosConfiguracion;
        return typeof config === 'string' ? JSON.parse(config) : (config || {});
      } catch {
        return {};
      }
    }
    return {};
  }, [formData.detalles]);

  const handleDynamicChange = (name: string, value: any) => {
    setFormData(prev => {
      const details = [...(prev.detalles || [])];
      if (details.length === 0) return prev;
      
      let currentConfig = {};
      try {
        const rawConfig = details[0].datosConfiguracion;
        currentConfig = typeof rawConfig === 'string' ? JSON.parse(rawConfig) : (rawConfig || {});
      } catch {
        currentConfig = {};
      }
      
      const newConfig = { ...currentConfig, [name]: value };
      
      details[0] = {
        ...details[0],
        datosConfiguracion: JSON.stringify(newConfig)
      };
      
      return { ...prev, detalles: details };
    });
  };

  const availableOrigins = useMemo(() => {
    if (!currentTipoVenta?.origenesJson) return ["presencial"];
    try {
      const parsed = JSON.parse(currentTipoVenta.origenesJson);
      return parsed.length > 0 ? parsed : ["presencial"];
    } catch {
      return ["presencial"];
    }
  }, [currentTipoVenta]);

  const handleChange = (field: keyof Venta, value: any) => {
    setFormData(prev => {
      const newData = { ...prev, [field]: value };

      if (field === "montoVenta" || field === "descuentoPorcentaje") {
        const monto = field === "montoVenta" ? Number(value) : Number(prev.montoVenta || 0);
        const descPct = field === "descuentoPorcentaje" ? Number(value) : Number(prev.descuentoPorcentaje || 0);
        const descMonto = (monto * descPct) / 100;
        newData.descuentoMonto = descMonto;
        newData.montoTotal = monto - descMonto;
      }

      return newData;
    });
  };

  const handleAddProduct = () => {
    if (!selectedProductId || productQuantity < 1) return;
    const prod = productos.find(p => p.id === Number(selectedProductId));
    if (!prod) return;

    const price = prod.precioFinal ?? prod.costeBase ?? 0;
    const totalLine = price * productQuantity;

    const newDetalle: DetalleVenta = {
      id: 0, // 0 in backend means new
      idProducto: prod.id,
      productoNombre: prod.nombre,
      cantidad: productQuantity,
      total: totalLine,
      datosConfiguracion: "{}"
    };

    setFormData(prev => {
      const existingDetalles = prev.detalles || [];
      const updatedDetalles = [...existingDetalles, newDetalle];

      // Auto adjust MontoVenta
      const currentMonto = Number(prev.montoVenta || 0);
      const newMonto = currentMonto + totalLine;
      const descPct = Number(prev.descuentoPorcentaje || 0);
      const descMonto = (newMonto * descPct) / 100;
      const newMontoTotal = newMonto - descMonto;

      return {
        ...prev,
        detalles: updatedDetalles,
        montoVenta: newMonto,
        descuentoMonto: descMonto,
        montoTotal: newMontoTotal
      };
    });

    setSelectedProductId("");
    setProductQuantity(1);
  };

  const handleRemoveProduct = (idProducto: number, index: number) => {
    setFormData(prev => {
      const updatedDetalles = [...(prev.detalles || [])];
      const removedItem = updatedDetalles[index];
      if (!removedItem) return prev;

      updatedDetalles.splice(index, 1);

      // Recalcular montos
      const currentMonto = Number(prev.montoVenta || 0);
      const newMonto = currentMonto - Number(removedItem.total);
      const descPct = Number(prev.descuentoPorcentaje || 0);
      const descMonto = (newMonto * descPct) / 100;
      const newMontoTotal = newMonto - descMonto;

      return {
        ...prev,
        detalles: updatedDetalles,
        montoVenta: Math.max(0, newMonto),
        descuentoMonto: Math.max(0, descMonto),
        montoTotal: Math.max(0, newMontoTotal)
      };
    });
  };

  const handleSave = async () => {
    setIsSubmitting(true);
    try {
      // Clean up dates: convert empty strings to null for the backend
      const payload = { ...formData };
      if (!payload.fechaVenta) payload.fechaVenta = undefined;
      if (!payload.fechaInstalacionPrevista) payload.fechaInstalacionPrevista = null;
      if (!payload.fechaInstalacionReal) payload.fechaInstalacionReal = null;

      console.log("DEBUG: Saving sale payload:", payload);

      await updateMutation.mutateAsync({
        id: sale.id,
        data: payload
      });
      onClose();
    } catch (error) {
      console.error("Error updating sale:", error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const formatAmount = (amount: number) => {
    return new Intl.NumberFormat('es-ES', { style: 'currency', currency: 'EUR' }).format(amount);
  };

  return (
    <Dialog open={open} onOpenChange={(o) => !o && onClose()}>
      <DialogContent className="glass border-white/10 max-w-4xl h-[90vh] flex flex-col p-0 overflow-hidden shadow-2xl">
        <DialogHeader className="p-6 pb-2 shrink-0 flex flex-row items-center justify-between">
          <div>
            <DialogTitle className="text-2xl font-bold flex items-center gap-3">
              <div className="h-10 w-10 rounded-xl bg-primary/20 flex items-center justify-center">
                <FileText className="w-6 h-6 text-primary" />
              </div>
              <div>
                <span>Editor</span>
                <p className="text-xs font-mono text-muted-foreground font-normal mt-0.5">{sale.numeroVenta}</p>
              </div>
            </DialogTitle>
          </div>
          <div className="flex items-center gap-3 pr-8">
            <StatusBadge status={formData.estado_Nombre} colorClass={formData.estado_Color} />
            <Separator orientation="vertical" className="h-8 bg-white/10" />
            <div className="text-right">
              <p className="text-[10px] uppercase text-muted-foreground font-bold">Total Neto</p>
              <PermissionGuard permission="ventas:view_prices" fallback={<p className="text-lg font-bold text-primary font-mono select-none blur-[4px]">******</p>}>
                <p className="text-lg font-bold text-primary font-mono">{formatAmount(Number(formData.montoTotal || 0))}</p>
              </PermissionGuard>
            </div>
          </div>
        </DialogHeader>

        <Tabs value={activeTab} onValueChange={setActiveTab} className="flex-1 flex flex-col overflow-hidden">
          <div className="px-6 border-b border-white/5 bg-white/5">
            <TabsList className="bg-transparent h-12 gap-6">
              <TabsTrigger value="general" className="data-[state=active]:bg-transparent data-[state=active]:shadow-none data-[state=active]:border-b-2 data-[state=active]:border-primary rounded-none px-0 gap-2 h-full">
                <Info className="w-4 h-4" /> Información General
              </TabsTrigger>
              <TabsTrigger value="fechas" className="data-[state=active]:bg-transparent data-[state=active]:shadow-none data-[state=active]:border-b-2 data-[state=active]:border-primary rounded-none px-0 gap-2 h-full">
                <Calendar className="w-4 h-4" /> Fechas e Instalación
              </TabsTrigger>
              <PermissionGuard permission="ventas:view_finances">
                <TabsTrigger value="finanzas" className="data-[state=active]:bg-transparent data-[state=active]:shadow-none data-[state=active]:border-b-2 data-[state=active]:border-primary rounded-none px-0 gap-2 h-full">
                  <Euro className="w-4 h-4" /> Finanzas
                </TabsTrigger>
              </PermissionGuard>
              <TabsTrigger value="productos" className="data-[state=active]:bg-transparent data-[state=active]:shadow-none data-[state=active]:border-b-2 data-[state=active]:border-primary rounded-none px-0 gap-2 h-full">
                <Package className="w-4 h-4" /> Productos
              </TabsTrigger>
              <TabsTrigger value="documentos" className="data-[state=active]:bg-transparent data-[state=active]:shadow-none data-[state=active]:border-b-2 data-[state=active]:border-primary rounded-none px-0 gap-2 h-full">
                <FileText className="w-4 h-4" /> Documentos
              </TabsTrigger>
            </TabsList>
          </div>

          <ScrollArea className="flex-1 p-6">
            <TabsContent value="general" className="mt-0 space-y-6">
              <div className="grid grid-cols-2 gap-6">
                <div className="space-y-2">
                  <Label className="text-xs uppercase font-bold text-muted-foreground">Cliente</Label>
                  <Input value={clients.find(c => c.id === formData.clienteId)?.nombre || ""} disabled className="bg-muted/30 border-dashed" />
                  <p className="text-[10px] text-muted-foreground flex items-center gap-1"><Info className="w-3 h-3" /> El cliente no es editable desde aquí.</p>
                </div>
                <div className="space-y-2">
                  <Label className="text-xs uppercase font-bold text-muted-foreground">Vendedor / Usuario</Label>
                  <Input value={users.find(u => u.id === formData.usuarioId)?.nombre || ""} disabled className="bg-muted/30 border-dashed" />
                </div>
              </div>

              <div className="grid grid-cols-1 gap-6">
                <div className="space-y-4">
                  <div className="space-y-2">
                    <Label className="text-xs uppercase font-bold text-muted-foreground">Estado del Pipeline</Label>
                    <Select 
                      value={formData.estado_Codigo} 
                      onValueChange={(val) => {
                        const st = availableStates.find((s: any) => s.codigo === val);
                        if (st) {
                          handleChange("estado_Codigo", st.codigo);
                          handleChange("estado_Nombre", st.nombre);
                          handleChange("estado_Color", st.color);
                          handleChange("estado_Icono", st.icono);
                          handleChange("estado_EsGanada", st.esGanada);
                          handleChange("estado_EsFinal", st.esFinal);
                        }
                      }}
                    >
                      <SelectTrigger className="bg-background/40 border-white/10 h-11 rounded-xl">
                        <SelectValue placeholder="Seleccione un estado" />
                      </SelectTrigger>
                      <SelectContent className="glass border-white/10">
                        {availableStates.map((st: any) => (
                          <SelectItem key={st.codigo} value={st.codigo}>
                            <div className="flex items-center gap-2">
                               <span className={cn(
                                 "w-2 h-2 rounded-full", 
                                 st.color?.replace('badge-', 'bg-') || 'bg-primary'
                               )} />
                               {st.nombre}
                            </div>
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                  </div>

                  {dynamicSchema.length > 0 && (
                    <div className="p-5 rounded-[1.5rem] bg-primary/5 border border-primary/20 space-y-4 shadow-sm">
                       <div className="flex items-center justify-between">
                         <p className="text-[10px] uppercase font-black text-primary flex items-center gap-2 tracking-widest">
                           <Package className="w-3.5 h-3.5" /> Configuración de Servicio
                         </p>
                         <Badge variant="outline" className="text-[8px] font-black uppercase bg-background/50 border-primary/20 text-primary px-2">
                           {currentTipoVenta?.nombre}
                         </Badge>
                       </div>
                       
                       <div className="grid grid-cols-1 gap-4">
                          {dynamicSchema.map((fieldObj: any, idx: number) => {
                                                        const field = typeof fieldObj === 'string' 
                              ? { name: fieldObj, label: fieldObj, type: 'text', placeholder: `Ingrese ${fieldObj}...` }
                              : { 
                                  name: fieldObj.name || fieldObj.nombre || fieldObj.key || `field_${idx}`,
                                  label: fieldObj.label || fieldObj.etiqueta || fieldObj.nombre || fieldObj.name || `Campo ${idx + 1}`,
                                  type: fieldObj.type || fieldObj.tipo || 'text',
                                  required: fieldObj.required || fieldObj.obligatorio || false,
                                  options: fieldObj.options || fieldObj.opciones || [],
                                  placeholder: fieldObj.placeholder || fieldObj.ayuda || ""
                                };
                            const val = dynamicValues[field.name] ?? "";
                            return (
                              <div key={field.name} className="space-y-1.5 group">
                                <Label className="text-[10px] text-muted-foreground uppercase font-bold pl-1 transition-colors group-focus-within:text-primary">
                                  {field.label}
                                  {field.required && <span className="text-destructive ml-1">*</span>}
                                </Label>
                                {field.type === "boolean" ? (
                                  <div className="flex gap-2">
                                    <Button
                                      type="button"
                                      variant="outline"
                                      size="sm"
                                      className={cn(
                                        "flex-1 h-9 rounded-xl text-[10px] font-bold uppercase transition-all",
                                        val === true ? "bg-primary/20 border-primary text-primary shadow-glow" : "bg-background/40 border-white/5 opacity-60"
                                      )}
                                      onClick={() => handleDynamicChange(field.name, true)}
                                    >
                                      Sí
                                    </Button>
                                    <Button
                                      type="button"
                                      variant="outline"
                                      size="sm"
                                      className={cn(
                                        "flex-1 h-9 rounded-xl text-[10px] font-bold uppercase transition-all",
                                        val === false ? "bg-primary/20 border-primary text-primary shadow-glow" : "bg-background/40 border-white/5 opacity-60"
                                      )}
                                      onClick={() => handleDynamicChange(field.name, false)}
                                    >
                                      No
                                    </Button>
                                  </div>
                                ) : field.type === "select" ? (
                                  <Select 
                                    value={val.toString()} 
                                    onValueChange={(v) => handleDynamicChange(field.name, v)}
                                  >
                                    <SelectTrigger className="bg-background/60 border-white/10 h-9 rounded-xl text-xs font-medium">
                                      <SelectValue placeholder={`Seleccione ${field.label}...`} />
                                    </SelectTrigger>
                                    <SelectContent className="glass border-white/10">
                                      {field.options?.map((opt: any) => (
                                        <SelectItem key={opt.value} value={opt.value} className="text-xs">
                                          {opt.label}
                                        </SelectItem>
                                      ))}
                                    </SelectContent>
                                  </Select>
                                ) : (
                                  <Input 
                                    value={val}
                                    placeholder={field.placeholder || `Ingrese ${field.label}...`}
                                    onChange={(e) => handleDynamicChange(field.name, e.target.value)}
                                    className="bg-background/60 border-white/10 h-9 rounded-xl text-xs font-medium focus-visible:ring-primary/30"
                                  />
                                )}
                              </div>
                            );
                          })}
                       </div>
                    </div>
                  )}

                  <div className="space-y-3">
                    <Label className="text-xs uppercase font-bold text-muted-foreground">Origen de Venta</Label>
                    <div className="grid grid-cols-2 gap-2">
                      {availableOrigins.map((orig: string) => (
                        <button
                          key={orig}
                          type="button"
                          onClick={() => handleChange("origenVenta", orig)}
                          className={cn(
                            "px-3 py-2 rounded-xl border text-[11px] font-bold transition-all uppercase truncate bg-background/40",
                            formData.origenVenta?.toLowerCase() === orig?.toLowerCase()
                              ? "bg-primary/10 border-primary text-primary shadow-glow scale-[1.02]"
                              : "border-border/40 hover:border-primary/30 text-muted-foreground hover:bg-muted/10"
                          )}
                        >
                          {orig}
                        </button>
                      ))}
                    </div>
                  </div>
                </div>
              </div>

              <div className="space-y-2">
                <Label className="text-xs uppercase font-bold text-muted-foreground">Notas Internas</Label>
                <Textarea
                  value={formData.notas || ""}
                  onChange={(e) => handleChange("notas", e.target.value)}
                  className="min-h-[120px] bg-background/50"
                  placeholder="Detalles adicionales sobre la venta..."
                />
              </div>
            </TabsContent>

            <TabsContent value="fechas" className="mt-0 space-y-8">
              <div className="p-4 rounded-xl bg-primary/5 border border-primary/10 flex items-start gap-4">
                <div className="h-10 w-10 rounded-full bg-primary/20 flex items-center justify-center shrink-0">
                  <Clock className="w-5 h-5 text-primary" />
                </div>
                <div>
                  <h4 className="text-sm font-bold text-foreground">Seguimiento de Tiempos</h4>
                  <p className="text-xs text-muted-foreground">Configura las fechas clave del proceso de instalación.</p>
                </div>
              </div>

              <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                <div className="space-y-2">
                  <Label className="text-xs uppercase font-bold text-muted-foreground flex items-center gap-2">
                    <Calendar className="w-3 h-3" /> Fecha de Venta
                  </Label>
                  <DatePicker
                    value={formData.fechaVenta || ""}
                    onChange={(val) => handleChange("fechaVenta", val)}
                  />
                </div>
                <div className="space-y-2">
                  <Label className="text-xs uppercase font-bold text-muted-foreground flex items-center gap-2">
                    <Clock className="w-3 h-3" /> Instalación Prevista
                  </Label>
                  <DatePicker
                    value={formData.fechaInstalacionPrevista || ""}
                    onChange={(val) => handleChange("fechaInstalacionPrevista", val)}
                  />
                </div>
                <div className="space-y-2">
                  <Label className="text-xs uppercase font-bold text-muted-foreground flex items-center gap-2">
                    <MapPin className="w-3 h-3" /> Instalación Real
                  </Label>
                  <DatePicker
                    value={formData.fechaInstalacionReal || ""}
                    onChange={(val) => handleChange("fechaInstalacionReal", val)}
                  />
                </div>
              </div>

              <div className="space-y-3">
                <Label className="text-xs uppercase font-bold text-muted-foreground flex items-center gap-2">
                  <Clock className="w-3 h-3 text-primary" /> Historial de Modificaciones
                </Label>
                <ScrollArea className="h-[280px] rounded-[1.5rem] border border-white/10 bg-background/20 p-4">
                  <div className="space-y-4 pr-3">
                    {historial.length === 0 ? (
                      <div className="flex flex-col items-center justify-center h-full py-8 opacity-40">
                        <Info className="w-8 h-8 mb-2" />
                        <p className="text-[10px] italic">Sin registros de actividad</p>
                      </div>
                    ) : (
                      historial.map((h: any, idx: number) => (
                        <div key={h.id || idx} className="relative pl-6 pb-4 group last:pb-0">
                           {/* Timeline Line */}
                           <div className="absolute left-[7px] top-3 bottom-0 w-[1px] bg-white/10 group-last:hidden" />
                           {/* Timeline Dot */}
                           <div className="absolute left-0 top-1 w-[15px] h-[15px] rounded-full bg-background border-2 border-primary flex items-center justify-center shadow-glow">
                              <div className="w-1.5 h-1.5 rounded-full bg-primary animate-pulse" />
                           </div>
                           
                           <div className="flex justify-between items-start mb-0.5">
                             <p className="text-[11px] font-black text-foreground uppercase tracking-tight">{h.accion}</p>
                             <p className="text-[9px] text-muted-foreground font-mono bg-white/5 px-1.5 py-0.5 rounded">
                               {new Date(h.fechaCambio).toLocaleString('es-ES', { 
                                 day: '2-digit', month: '2-digit', year: '2-digit',
                                 hour: '2-digit', minute: '2-digit', second: '2-digit' 
                               })}
                             </p>
                           </div>
                           <p className="text-[11px] text-muted-foreground leading-tight mb-1">{h.descripcion}</p>
                           <div className="flex items-center gap-1.5">
                             <div className="h-4 w-4 rounded-full bg-primary/20 flex items-center justify-center text-[8px] font-bold text-primary">
                               {h.usuarioNombre?.charAt(0).toUpperCase()}
                             </div>
                             <p className="text-[9px] text-primary/70 font-bold uppercase tracking-wider">{h.usuarioNombre}</p>
                           </div>
                        </div>
                      ))
                    )}
                  </div>
                </ScrollArea>
              </div>
            </TabsContent>

            <PermissionGuard permission="ventas:view_finances">
              <TabsContent value="finanzas" className="mt-0 space-y-6">
                <div className="grid grid-cols-2 gap-8">
                  <div className="space-y-6">
                    <div className="space-y-2">
                      <Label className="text-xs uppercase font-bold text-muted-foreground flex items-center gap-2">
                        <Euro className="w-3 h-3" /> Base de Venta (Subtotal)
                      </Label>
                      <div className="relative">
                        <Input
                          type="number"
                          value={formData.montoVenta || 0}
                          onChange={(e) => handleChange("montoVenta", e.target.value)}
                          className="pl-8 font-mono text-lg font-bold"
                        />
                        <span className="absolute left-3 top-1/2 -translate-y-1/2 text-muted-foreground">€</span>
                      </div>
                    </div>

                    <div className="space-y-2">
                      <div className="flex justify-between items-center">
                        <Label className="text-xs uppercase font-bold text-muted-foreground flex items-center gap-2">
                          <TrendingDown className="w-3 h-3" /> Descuento Aplicado
                        </Label>
                        <span className="text-xs font-bold text-primary font-mono">-{formatAmount(Number(formData.descuentoMonto || 0))}</span>
                      </div>
                      <div className="flex items-center gap-3">
                        <Input
                          type="number"
                          value={formData.descuentoPorcentaje || 0}
                          onChange={(e) => handleChange("descuentoPorcentaje", e.target.value)}
                          className="font-mono"
                        />
                        <span className="text-lg font-bold text-muted-foreground">%</span>
                      </div>
                      <p className="text-[10px] text-muted-foreground">El monto neto se recalculará automáticamente.</p>
                    </div>
                  </div>

                  <div className="flex flex-col justify-center items-center p-8 rounded-2xl bg-white/5 border border-white/10 space-y-4">
                    <p className="text-xs uppercase tracking-widest text-muted-foreground font-bold italic">Resumen de Liquidación</p>
                    <div className="flex flex-col items-center">
                      <span className="text-sm text-muted-foreground line-through opacity-50">{formatAmount(Number(formData.montoVenta || 0))}</span>
                      <span className="text-4xl font-black text-primary font-mono tracking-tighter">{formatAmount(Number(formData.montoTotal || 0))}</span>
                    </div>
                    <div className="flex items-center gap-2 text-[10px] text-green-500 font-bold bg-green-500/10 px-3 py-1 rounded-full border border-green-500/20">
                      <TrendingDown className="w-3 h-3" /> AHORRO CLIENTE: {formatAmount(Number(formData.descuentoMonto || 0))}
                    </div>
                  </div>
                </div>
              </TabsContent>
            </PermissionGuard>

            <TabsContent value="productos" className="mt-0">
              <div className="space-y-4">
                <div className="flex flex-col sm:flex-row items-center gap-4 bg-muted/30 p-4 rounded-xl border border-white/5">
                  <div className="flex-1 w-full space-y-1">
                    <Label className="text-[10px] uppercase font-bold text-muted-foreground">Productos Disponibles</Label>
                    <Select 
                      value={selectedProductId}
                      onValueChange={setSelectedProductId}
                    >
                      <SelectTrigger className="h-9 bg-background/50 border-white/10">
                        <SelectValue placeholder="-- Seleccionar producto --" />
                      </SelectTrigger>
                      <SelectContent className="glass border-white/10 max-h-[300px]">
                        {availableProductsList.length === 0 ? (
                           <div className="p-4 text-center text-[10px] text-muted-foreground italic">No hay productos compatibles disponibles</div>
                        ) : availableProductsList.map(p => {
                           const isAlreadyInSale = formData.detalles?.some(d => d.idProducto === p.id);
                           return (
                              <SelectItem key={p.id} value={p.id.toString()} className="text-xs">
                                 <div className="flex items-center justify-between w-full gap-2">
                                    <span>{p.nombre}</span>
                                    <div className="flex items-center gap-2 opacity-70">
                                       <span className="text-[10px] font-mono px-1.5 py-0.5 rounded bg-primary/20 text-primary">
                                          {formatAmount(p.precioFinal || p.costeBase || 0)}
                                       </span>
                                       {isAlreadyInSale && (
                                          <span className="text-[9px] font-bold text-amber-500 uppercase px-1.5 py-0.5 rounded bg-amber-500/10 border border-amber-500/20">
                                             En venta
                                          </span>
                                       )}
                                    </div>
                                 </div>
                              </SelectItem>
                           );
                        })}
                      </SelectContent>
                    </Select>
                  </div>
                  <div className="w-24 space-y-1">
                    <Label className="text-[10px] uppercase font-bold text-muted-foreground">Cantidad</Label>
                    <Input type="number" min={1} value={productQuantity} onChange={e => setProductQuantity(Number(e.target.value))} className="h-9 bg-background/50" />
                  </div>
                  <div className="mt-5">
                    <Button onClick={handleAddProduct} disabled={!selectedProductId} size="sm" className="font-bold gap-2">
                      <PlusCircle className="w-4 h-4" /> Añadir
                    </Button>
                  </div>
                </div>

                <div className="border border-white/10 rounded-xl overflow-hidden bg-background/40">
                  <table className="w-full text-sm">
                    <thead>
                      <tr className="bg-white/5 border-b border-white/10">
                        <th className="text-left p-3 text-[10px] uppercase font-bold text-muted-foreground">Producto</th>
                        <th className="text-center p-3 text-[10px] uppercase font-bold text-muted-foreground">Cant.</th>
                        <th className="text-right p-3 text-[10px] uppercase font-bold text-muted-foreground">Precio Unit.</th>
                        <th className="text-right p-3 text-[10px] uppercase font-bold text-muted-foreground">Subtotal</th>
                        <th className="w-10"></th>
                      </tr>
                    </thead>
                    <tbody>
                      {formData.detalles?.map(det => (
                        <tr key={det.id || Math.random()} className={cn("border-b border-white/5 transition-colors", det.id === 0 ? "bg-emerald-500/5 group hover:bg-emerald-500/10" : "group hover:bg-white/5")}>
                          <td className="p-3">
                            <div className="flex items-center gap-3">
                              <div className="h-8 w-8 rounded bg-primary/20 flex items-center justify-center">
                                <Package className="w-4 h-4 text-primary" />
                              </div>
                              <div>
                                <p className="font-bold text-foreground">{det.productoNombre || "Producto"}</p>
                                <p className="text-[10px] text-muted-foreground font-mono">ID: {det.idProducto}</p>
                              </div>
                            </div>
                          </td>
                          <td className="p-3 text-center font-mono">x{det.cantidad}</td>
                          <td className="p-3 text-right font-mono">
                            <PermissionGuard permission="ventas:view_prices" fallback={<span className="blur-[3px] select-none opacity-50">*** €</span>}>
                              {formatAmount(det.total / det.cantidad)}
                            </PermissionGuard>
                          </td>
                          <td className="p-3 text-right font-mono font-bold text-primary">
                            <PermissionGuard permission="ventas:view_prices" fallback={<span className="blur-[3px] select-none opacity-50">*** €</span>}>
                              {formatAmount(det.total)}
                            </PermissionGuard>
                          </td>
                          <td className="p-3">
                            <Button 
                              variant="ghost" 
                              size="icon" 
                              className="h-7 w-7 text-destructive hover:bg-destructive/10 hover:text-destructive transition-all"
                              onClick={(e) => {
                                e.stopPropagation();
                                const index = formData.detalles?.findIndex(d => d === det) ?? -1;
                                if (index !== -1) handleRemoveProduct(det.idProducto, index);
                              }}
                            >
                              <Trash2 className="w-3.5 h-3.5" />
                            </Button>
                          </td>
                        </tr>
                      )) || (
                          <tr>
                            <td colSpan={5} className="p-8 text-center text-muted-foreground italic">No hay detalles de productos.</td>
                          </tr>
                        )}
                    </tbody>
                    <tfoot className="bg-white/5">
                      <tr>
                        <td colSpan={3} className="text-right p-4 py-2 font-bold text-muted-foreground text-xs uppercase">Suma de productos:</td>
                        <td className="text-right p-4 py-2 font-bold font-mono text-foreground">
                          <PermissionGuard permission="ventas:view_prices" fallback={<span className="blur-[3px] select-none opacity-50">*** €</span>}>
                            {formatAmount(Number(formData.montoVenta) || 0)}
                          </PermissionGuard>
                        </td>
                      </tr>
                    </tfoot>
                  </table>
                </div>
              </div>
            </TabsContent>

            <TabsContent value="documentos" className="mt-0 space-y-6">
              <div className="p-4 rounded-xl bg-primary/5 border border-primary/10 flex items-start gap-4">
                <div className="h-10 w-10 rounded-full bg-primary/20 flex items-center justify-center shrink-0">
                  <FileText className="w-5 h-5 text-primary" />
                </div>
                <div>
                  <h4 className="text-sm font-bold text-foreground">Gestión Documental Vista 360</h4>
                  <p className="text-xs text-muted-foreground">Adjunta contratos, fotos de instalaciones o documentos de identidad.</p>
                </div>
              </div>

              <div className="border-2 border-dashed border-white/10 rounded-2xl p-10 flex flex-col items-center justify-center gap-4 bg-white/5 hover:bg-white/10 hover:border-primary/30 transition-all group cursor-pointer">
                <div className="h-16 w-16 rounded-full bg-primary/10 flex items-center justify-center group-hover:scale-110 transition-transform">
                  <Upload className="h-8 w-8 text-primary" />
                </div>
                <div className="text-center">
                  <p className="text-sm font-bold">Haz clic o arrastra archivos aquí</p>
                  <p className="text-xs text-muted-foreground mt-1">Soporta PDF, PNG, JPG hasta 10MB</p>
                </div>
                <Button variant="outline" size="sm" className="mt-2 font-bold bg-background shadow-sm border-border">
                  Seleccionar Archivos
                </Button>
              </div>

              <div className="mt-8">
                <h5 className="text-xs font-bold uppercase tracking-wider text-muted-foreground mb-4">Archivos Recientes</h5>
                <div className="space-y-2">
                  <div className="glass p-3 rounded-lg flex items-center justify-between group">
                    <div className="flex items-center gap-3">
                      <div className="h-9 w-9 rounded-lg bg-orange-500/10 flex items-center justify-center">
                        <FileIcon className="h-5 w-5 text-orange-500" />
                      </div>
                      <div>
                        <p className="text-sm font-medium">Contrato_Servicios.pdf</p>
                        <p className="text-[10px] text-muted-foreground">2.4 MB • Subido hace 2 horas</p>
                      </div>
                    </div>
                    <Button variant="ghost" size="icon" className="h-8 w-8 opacity-0 group-hover:opacity-100 transition-opacity">
                      <ArrowRight className="h-4 w-4" />
                    </Button>
                  </div>
                </div>
              </div>
            </TabsContent>
          </ScrollArea>

          <DialogFooter className="p-6 border-t border-white/10 bg-white/5 shrink-0">
            <div className="flex w-full items-center justify-between">
              <Button variant="ghost" type="button" onClick={onClose} className="gap-2">
                <X className="w-4 h-4" /> Cancelar
              </Button>
              <Button
                type="button"
                onClick={handleSave}
                disabled={isSubmitting || updateMutation.isPending}
                className="gap-2 px-8 font-bold shadow-glow"
              >
                {isSubmitting ? <Clock className="w-4 h-4 animate-spin" /> : <Save className="w-4 h-4" />}
                Guardar Cambios
              </Button>
            </div>
          </DialogFooter>
        </Tabs>
      </DialogContent>
    </Dialog>
  );
}
