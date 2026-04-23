import { useState, useEffect } from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Checkbox } from "@/components/ui/checkbox";
import { Textarea } from "@/components/ui/textarea";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Cliente, Venta } from "@/types";
import { 
  Building2, 
  User, 
  Info, 
  History, 
  ShoppingCart, 
  FileText, 
  LayoutGrid, 
  MapPin, 
  CreditCard,
  AlertTriangle,
  Clock,
  Package,
  ArrowRight,
  Upload,
  File as FileIcon
} from "lucide-react";
import { cn } from "@/lib/utils";
import { ClientesService } from "@/services/api/clientes.service";
import { useQuery } from "@tanstack/react-query";
import { StatusBadge } from "@/components/shared/StatusBadge";
import { PermissionGuard } from "@/components/auth/PermissionGuard";

interface ClienteFormModalProps {
  open: boolean;
  onClose: () => void;
  cliente?: Cliente | null;
  onSave: (data: Partial<Cliente>) => void;
}

export function ClienteFormModal({ open, onClose, cliente, onSave }: ClienteFormModalProps) {
  const [activeTab, setActiveTab] = useState("general");
  const [formData, setFormData] = useState<Partial<Cliente>>({
    nombre: "",
    email: "",
    dni: "",
    tipo: "EMPRESA",
    movil: "",
    numeroCuenta: "",
    direccion: {
      calle: "",
      poblacion: "",
      provincia: "",
      codigoPostal: ""
    },
    notaPublica: "",
    penalizado: false
  });

  // Fetch product history for this client
  const { data: productsHistory = [], isLoading: loadingProducts } = useQuery({
    queryKey: ["cliente-productos", cliente?.id],
    queryFn: () => cliente?.id ? ClientesService.getProductsByClient(cliente.id) : Promise.resolve([]),
    enabled: !!cliente?.id && open && activeTab === "productos"
  });

  useEffect(() => {
    if (cliente) {
      setFormData({
        ...cliente,
        direccion: cliente.direccion || { calle: "", poblacion: "", provincia: "", codigoPostal: "" }
      });
    } else {
      setFormData({
        nombre: "",
        email: "",
        dni: "",
        tipo: "EMPRESA",
        movil: "",
        numeroCuenta: "",
        direccion: {
          calle: "",
          poblacion: "",
          provincia: "",
          codigoPostal: ""
        },
        notaPublica: "",
        penalizado: false
      });
    }
  }, [cliente, open]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave(formData);
  };

  const formatAmount = (amount: number) => {
    return new Intl.NumberFormat('es-ES', { style: 'currency', currency: 'EUR' }).format(amount);
  };

  return (
    <Dialog open={open} onOpenChange={(o) => !o && onClose()}>
      <DialogContent className="glass border-white/10 max-w-4xl h-[90vh] flex flex-col p-0 overflow-hidden shadow-2xl">
        <DialogHeader className="p-6 pb-2 shrink-0 border-b border-white/5 bg-white/5">
          <div className="flex items-center justify-between w-full">
            <DialogTitle className="flex items-center gap-3 text-2xl font-bold">
              <div className="h-10 w-10 rounded-xl bg-primary/20 flex items-center justify-center">
                {formData.tipo === "EMPRESA" ? <Building2 className="h-6 w-6 text-primary" /> : <User className="h-6 w-6 text-primary" />}
              </div>
              <div>
                <span>{cliente ? "Vista 360 de Cliente" : "Nuevo Cliente"}</span>
                {cliente && <p className="text-xs font-mono text-muted-foreground font-normal mt-0.5">ID: {cliente.id}</p>}
              </div>
            </DialogTitle>
            {formData.penalizado && (
              <div className="flex items-center gap-2 px-3 py-1 bg-destructive/10 border border-destructive/20 rounded-full text-destructive text-[10px] font-bold uppercase tracking-wider animate-pulse">
                <AlertTriangle className="w-3 h-3" /> Penalizado
              </div>
            )}
          </div>
        </DialogHeader>

        <Tabs value={activeTab} onValueChange={setActiveTab} className="flex-1 flex flex-col overflow-hidden">
          <div className="px-6 border-b border-white/5 bg-white/5">
            <TabsList className="bg-transparent h-12 gap-6">
              <TabsTrigger value="general" className="data-[state=active]:bg-transparent data-[state=active]:shadow-none data-[state=active]:border-b-2 data-[state=active]:border-primary rounded-none px-0 gap-2 h-full">
                <LayoutGrid className="w-4 h-4" /> Información General
              </TabsTrigger>
              <TabsTrigger value="productos" className="data-[state=active]:bg-transparent data-[state=active]:shadow-none data-[state=active]:border-b-2 data-[state=active]:border-primary rounded-none px-0 gap-2 h-full">
                <History className="w-4 h-4" /> Historial de Productos
              </TabsTrigger>
              <TabsTrigger value="documentos" className="data-[state=active]:bg-transparent data-[state=active]:shadow-none data-[state=active]:border-b-2 data-[state=active]:border-primary rounded-none px-0 gap-2 h-full">
                <FileText className="w-4 h-4" /> Documentos
              </TabsTrigger>
            </TabsList>
          </div>

          <ScrollArea className="flex-1 p-6">
            <TabsContent value="general" className="mt-0 space-y-8">
              <form id="cliente-form" onSubmit={handleSubmit} className="space-y-8">
                {/* Sección Identificación */}
                <div className="space-y-4">
                  <div className="flex items-center gap-2 text-xs font-bold text-muted-foreground uppercase tracking-wider">
                    <Info className="w-3 h-3" /> Identificación del Sujeto
                  </div>
                  <div className="grid grid-cols-2 gap-6 p-5 rounded-2xl bg-white/5 border border-white/5">
                    <div className="space-y-2">
                      <Label className="text-xs font-bold">Tipo de Solicitante</Label>
                      <Select value={formData.tipo} onValueChange={v => setFormData({...formData, tipo: v})}>
                        <SelectTrigger className="bg-background/50 border-white/10"><SelectValue /></SelectTrigger>
                        <SelectContent className="glass">
                          <SelectItem value="EMPRESA">Empresa / Corporativo</SelectItem>
                          <SelectItem value="PARTICULAR">Particular / Autónomo</SelectItem>
                        </SelectContent>
                      </Select>
                    </div>
                    <div className="space-y-2">
                      <Label className="text-xs font-bold">Documento (DNI/CIF/RNC)</Label>
                      <Input value={formData.dni || ""} onChange={e => setFormData({...formData, dni: e.target.value})} className="bg-background/50 border-white/10 font-mono" />
                    </div>
                    <div className="col-span-2 space-y-2">
                      <Label className="text-xs font-bold">Nombre Completo / Razón Social <span className="text-destructive">*</span></Label>
                      <Input required value={formData.nombre} onChange={e => setFormData({...formData, nombre: e.target.value})} className="bg-background/50 border-white/10 text-lg font-bold" />
                    </div>
                  </div>
                </div>

                {/* Sección Contacto y Dirección */}
                <div className="grid grid-cols-2 gap-8">
                  <div className="space-y-4">
                    <div className="flex items-center gap-2 text-xs font-bold text-muted-foreground uppercase tracking-wider">
                      <ShoppingCart className="w-3 h-3" /> Datos de Contacto
                    </div>
                    <div className="space-y-4 p-5 rounded-2xl bg-white/5 border border-white/5">
                      <div className="space-y-2">
                        <Label className="text-xs font-bold">Teléfono Móvil</Label>
                        <Input value={formData.movil || ""} onChange={e => setFormData({...formData, movil: e.target.value})} className="bg-background/50 border-white/10" />
                      </div>
                      <div className="space-y-2">
                        <Label className="text-xs font-bold">Correo Electrónico</Label>
                        <Input type="email" value={formData.email || ""} onChange={e => setFormData({...formData, email: e.target.value})} className="bg-background/50 border-white/10" />
                      </div>
                      <div className="space-y-2pt-4">
                        <Label className="text-xs font-bold flex items-center gap-2"><CreditCard className="w-3 h-3" /> IBAN / Cuenta Bancaria</Label>
                        <Input value={formData.numeroCuenta || ""} onChange={e => setFormData({...formData, numeroCuenta: e.target.value})} className="bg-background/50 border-white/10 font-mono text-xs" placeholder="ES00 0000 0000 0000 0000 0000" />
                      </div>
                    </div>
                  </div>

                  <div className="space-y-4">
                    <div className="flex items-center gap-2 text-xs font-bold text-muted-foreground uppercase tracking-wider">
                      <MapPin className="w-3 h-3" /> Ubicación
                    </div>
                    <div className="space-y-4 p-5 rounded-2xl bg-white/5 border border-white/5">
                      <div className="space-y-2">
                        <Label className="text-xs font-bold">Dirección Principal</Label>
                        <Input 
                          value={formData.direccion?.calle || ""} 
                          onChange={e => setFormData({
                            ...formData, 
                            direccion: { ...formData.direccion!, calle: e.target.value }
                          })} 
                          className="bg-background/50 border-white/10" 
                        />
                      </div>
                      <div className="grid grid-cols-3 gap-3">
                        <div className="space-y-1">
                          <Label className="text-[10px] uppercase font-bold text-muted-foreground">CP</Label>
                          <Input 
                            value={formData.direccion?.codigoPostal || ""} 
                            onChange={e => setFormData({
                              ...formData, 
                              direccion: { ...formData.direccion!, codigoPostal: e.target.value }
                            })} 
                            className="bg-background/50 border-white/10 h-9 font-mono" 
                          />
                        </div>
                        <div className="col-span-2 space-y-1">
                          <Label className="text-[10px] uppercase font-bold text-muted-foreground">Población</Label>
                          <Input 
                            value={formData.direccion?.poblacion || ""} 
                            onChange={e => setFormData({
                              ...formData, 
                              direccion: { ...formData.direccion!, poblacion: e.target.value }
                            })} 
                            className="bg-background/50 border-white/10 h-9" 
                          />
                        </div>
                      </div>
                      <div className="space-y-1">
                        <Label className="text-[10px] uppercase font-bold text-muted-foreground">Provincia</Label>
                        <Input 
                          value={formData.direccion?.provincia || ""} 
                          onChange={e => setFormData({
                            ...formData, 
                            direccion: { ...formData.direccion!, provincia: e.target.value }
                          })} 
                          className="bg-background/50 border-white/10 h-9" 
                        />
                      </div>
                    </div>
                  </div>
                </div>

                <div className="space-y-4">
                  <div className="flex items-center gap-2 text-xs font-bold text-muted-foreground uppercase tracking-wider">
                    <AlertTriangle className="w-3 h-3" /> Observaciones y Seguridad
                  </div>
                  <div className="grid grid-cols-2 gap-6">
                    <div className="space-y-2">
                      <Label className="text-xs font-bold">Nota Explicativa (Pública)</Label>
                      <Textarea value={formData.notaPublica || ""} onChange={e => setFormData({...formData, notaPublica: e.target.value})} className="bg-background/50 border-white/10 min-h-[100px]" />
                    </div>
                    <div className="flex items-center justify-center p-6 border-2 border-destructive/20 rounded-2xl bg-destructive/5 hover:bg-destructive/10 transition-colors cursor-pointer group" onClick={() => setFormData({...formData, penalizado: !formData.penalizado})}>
                       <div className="flex flex-col items-center gap-2 text-center">
                          <Checkbox checked={formData.penalizado} className="h-6 w-6 border-destructive data-[state=checked]:bg-destructive" />
                          <Label className="text-destructive font-black text-sm uppercase group-hover:scale-105 transition-transform">Marcar como Penalizado</Label>
                          <p className="text-[10px] text-destructive/60">Aparecerá en la lista negra de ventas.</p>
                       </div>
                    </div>
                  </div>
                </div>
              </form>
            </TabsContent>

            <TabsContent value="productos" className="mt-0 h-full">
               <div className="space-y-6 flex flex-col h-full">
                  <div className="p-4 rounded-xl bg-primary/5 border border-primary/10 flex items-start gap-4">
                    <div className="h-10 w-10 rounded-full bg-primary/20 flex items-center justify-center shrink-0">
                      <History className="w-5 h-5 text-primary" />
                    </div>
                    <div>
                      <h4 className="text-sm font-bold text-foreground">Historial Consolidado de Productos</h4>
                      <p className="text-xs text-muted-foreground">Lista de todos los productos y servicios contratados a través de sus ventas.</p>
                    </div>
                  </div>

                  {loadingProducts ? (
                    <div className="flex-1 flex items-center justify-center py-20">
                      <Clock className="w-8 h-8 animate-spin text-primary/40" />
                    </div>
                  ) : productsHistory.length === 0 ? (
                    <div className="flex-1 flex flex-col items-center justify-center py-20 text-muted-foreground opacity-50 space-y-3">
                       <Package className="w-12 h-12" />
                       <p className="text-sm font-medium italic">El cliente no tiene productos registrados.</p>
                    </div>
                  ) : (
                    <div className="border border-white/10 rounded-2xl overflow-hidden bg-background/40">
                       <table className="w-full text-sm">
                          <thead>
                             <tr className="bg-white/5 border-b border-white/10">
                                <th className="text-left p-4 text-[10px] uppercase font-bold text-muted-foreground">Producto / Servicio</th>
                                <th className="text-center p-4 text-[10px] uppercase font-bold text-muted-foreground">Origen Venta</th>
                                <th className="text-right p-4 text-[10px] uppercase font-bold text-muted-foreground">Precio Acordado</th>
                                <th className="text-center p-4 text-[10px] uppercase font-bold text-muted-foreground">Estado</th>
                             </tr>
                          </thead>
                          <tbody>
                             {productsHistory.map((item: any, idx: number) => (
                               <tr key={idx} className="border-b border-white/5 group hover:bg-white/5 transition-colors">
                                  <td className="p-4">
                                     <div className="flex items-center gap-3">
                                        <div className="h-9 w-9 rounded-lg bg-primary/10 flex items-center justify-center">
                                           <Package className="w-5 h-5 text-primary" />
                                        </div>
                                        <div>
                                           <p className="font-bold text-foreground">{item.nombreProducto}</p>
                                           <p className="text-[10px] text-muted-foreground font-mono">Contratado el {new Date(item.fechaContratacion).toLocaleDateString()}</p>
                                        </div>
                                     </div>
                                  </td>
                                  <td className="p-4 text-center">
                                     <span className="text-xs font-medium text-muted-foreground font-mono uppercase">{item.ventaNumero}</span>
                                  </td>
                                  <td className="p-4 text-right">
                                    <PermissionGuard permission="ventas:view_prices" fallback={<span className="blur-[3px] select-none opacity-50">*** €</span>}>
                                      <span className="font-bold font-mono text-primary">{formatAmount(item.precio)}</span>
                                    </PermissionGuard>
                                  </td>
                                  <td className="p-4 text-center">
                                     <StatusBadge status={item.estadoVenta} />
                                  </td>
                               </tr>
                             ))}
                          </tbody>
                       </table>
                    </div>
                  )}
               </div>
            </TabsContent>

            <TabsContent value="documentos" className="mt-0 h-full">
               <div className="space-y-6 flex flex-col h-full">
                  <div className="p-4 rounded-xl bg-primary/5 border border-primary/10 flex items-start gap-4">
                    <div className="h-10 w-10 rounded-full bg-primary/20 flex items-center justify-center shrink-0">
                      <FileText className="w-5 h-5 text-primary" />
                    </div>
                    <div>
                      <h4 className="text-sm font-bold text-foreground">Expediente Digital de Cliente</h4>
                      <p className="text-xs text-muted-foreground">Adjunta documentos de identidad, comprobantes fiscales o contratos maestros.</p>
                    </div>
                  </div>

                  <div className="flex-1 flex flex-col">
                    <div className="border-2 border-dashed border-white/10 rounded-2xl p-10 flex flex-col items-center justify-center gap-4 bg-white/5 hover:bg-white/10 hover:border-primary/30 transition-all group cursor-pointer">
                      <div className="h-16 w-16 rounded-full bg-primary/10 flex items-center justify-center group-hover:scale-110 transition-transform">
                        <Upload className="h-8 w-8 text-primary" />
                      </div>
                      <div className="text-center">
                        <p className="text-sm font-bold">Arrastra documentos del cliente aquí</p>
                        <p className="text-xs text-muted-foreground mt-1">Soporta PDF, PNG, JPG hasta 10MB</p>
                      </div>
                      <Button variant="outline" size="sm" className="mt-2 font-bold bg-background shadow-sm border-border">
                        Seleccionar Archivos
                      </Button>
                    </div>
                    
                    <div className="mt-8">
                       <h5 className="text-xs font-bold uppercase tracking-wider text-muted-foreground mb-4">Documentación Existente</h5>
                       <p className="text-sm text-muted-foreground italic text-center py-4 bg-white/5 rounded-xl border border-dashed border-white/5">No se han subido documentos todavía.</p>
                    </div>
                  </div>
               </div>
            </TabsContent>
          </ScrollArea>
        </Tabs>
        
        <DialogFooter className="p-6 pt-4 border-t border-white/10 bg-white/5 shrink-0">
          <div className="flex justify-between w-full items-center">
            <Button variant="ghost" type="button" onClick={onClose} className="gap-2">
              Cancelar
            </Button>
            <Button type="submit" form="cliente-form" className="bg-primary hover:bg-primary/90 font-bold px-8 shadow-glow">
              {cliente ? "Guardar Cambios" : "Crear Cliente"}
            </Button>
          </div>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
