import { useState, useMemo } from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Label } from "@/components/ui/label";
import { Switch } from "@/components/ui/switch";
import { motion, AnimatePresence } from "framer-motion";
import { Plus, CheckCircle2, Tag, ShoppingCart, ChevronsUpDown, Check, Settings, Trash2 } from "lucide-react";
import { useAuth } from "@/hooks/useAuth";
import { useQuery } from "@tanstack/react-query";
import { ClientesService } from "@/services/api/clientes.service";
import { useProductos } from "@/hooks/useProductos";
import { useCreateVenta } from "@/hooks/useSales";
import { ProveedoresService, TiposVentaService } from "@/services/api/proveedores.service";
import { toast } from "sonner";
import { Producto, Cliente } from "@/types";
import { Textarea } from "@/components/ui/textarea";
import { ClienteFormModal } from "@/components/clientes/ClienteFormModal";
import { Badge } from "@/components/ui/badge";
import { cn } from "@/lib/utils";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { Command, CommandEmpty, CommandGroup, CommandInput, CommandItem, CommandList } from "@/components/ui/command";

export function SaleWizard({ open, onClose }: { open: boolean; onClose: () => void }) {
  const { user } = useAuth();
  const [step, setStep] = useState(1);
  const [submitting, setSubmitting] = useState(false);

  // Api Queries
  const { data: clients = [], refetch: refetchClients } = useQuery({ queryKey: ["clientes"], queryFn: ClientesService.getAll });
  const { productos = [] } = useProductos();
  const { data: proveedores = [] } = useQuery({ queryKey: ["proveedores"], queryFn: ProveedoresService.getAll });
  const { data: allTiposVenta = [] } = useQuery({ queryKey: ["tipos-venta"], queryFn: TiposVentaService.getAll });
  const createMutation = useCreateVenta();

  // Wizard Data State
  const [clienteId, setClienteId] = useState<string>("");
  const [tipoVenta, setTipoVenta] = useState<string>("");
  const [productoIds, setProductoIds] = useState<string[]>([]);
  const [precio, setPrecio] = useState<number | string>("");
  const [cantidad, setCantidad] = useState<number | string>(1);
  const [dinamicos, setDinamicos] = useState<Record<string, any>>({});
  const [notas, setNotas] = useState("");
  const [proveedorId, setProveedorId] = useState<string>("");
  const [origenVenta, setOrigenVenta] = useState<string>("presencial");

  // Client modal state
  const [clientModalOpen, setClientModalOpen] = useState(false);

  // Combobox states
  const [openClientCombo, setOpenClientCombo] = useState(false);
  const [clientSearch, setClientSearch] = useState("");
  
  const [openProductCombo, setOpenProductCombo] = useState(false);
  const [productSearch, setProductSearch] = useState("");
  
  const [openProveedorCombo, setOpenProveedorCombo] = useState(false);
  const [proveedorSearch, setProveedorSearch] = useState("");

  const filteredClients = useMemo(() => {
    const term = clientSearch.toLowerCase();
    if (!term) return clients.slice(0, 50);
    return clients.filter(c => 
      c.nombre?.toLowerCase().includes(term) ||
      c.dni?.toLowerCase().includes(term) ||
      c.email?.toLowerCase().includes(term) ||
      c.movil?.includes(term) ||
      c.direccion?.poblacion?.toLowerCase().includes(term)
    ).slice(0, 50);
  }, [clients, clientSearch]);

  const filteredProveedores = useMemo(() => {
    const term = proveedorSearch.toLowerCase();
    let base = proveedores;
    
    if (tipoVenta) {
      const selectedTv = allTiposVenta.find(t => t.id.toString() === tipoVenta || t.codigo === tipoVenta);
      if (selectedTv) {
        // Only show providers that have this sale type assigned
        base = base.filter(p => p.tipoVentaIds?.includes(selectedTv.id) || p.tiposVenta?.some(tv => tv.id === selectedTv.id));
      }
    }
    
    if (!term) return base;
    return base.filter(p => p.nombre?.toLowerCase().includes(term));
  }, [proveedores, proveedorSearch, tipoVenta, allTiposVenta]);

  const filteredProducts = useMemo(() => {
    const term = productSearch.toLowerCase();
    let base = productos.filter(p => p.activo !== false);
    
    // Filter by sale type if selected
    if (tipoVenta) {
      const selectedTv = allTiposVenta.find(t => t.id.toString() === tipoVenta || t.codigo === tipoVenta);
      if (selectedTv) {
        base = base.filter(p => p.tipoVentaId === selectedTv.id || p.tipoVenta?.id === selectedTv.id);
      }
    }

    // Filter by provider if selected
    if (proveedorId) {
       base = base.filter(p => p.proveedorId?.toString() === proveedorId);
    }
    
    // Filter by search term
    if (term) {
      base = base.filter(p => 
        p.nombre?.toLowerCase().includes(term) ||
        p.descripcion?.toLowerCase().includes(term) ||
        p.proveedor?.nombre?.toLowerCase().includes(term) ||
        p.tipoVenta?.nombre?.toLowerCase().includes(term)
      );
    }

    return base.slice(0, 50);
  }, [productos, productSearch, proveedorId, tipoVenta, allTiposVenta]);

  const selectedProduct = useMemo(() => productos.find(p => productoIds.length > 0 && p.id === Number(productoIds[0])), [productos, productoIds]);

  const dynamicSchema = useMemo(() => {
    // Buscar el tipo de venta en la tabla maestra, no en el pipeline config
    const selectedTipo = allTiposVenta.find(t => t.codigo === tipoVenta || t.id.toString() === tipoVenta);
    if (!selectedTipo?.esquemaVariablesJson) return [];
    try {
      const vars = JSON.parse(selectedTipo.esquemaVariablesJson);
      return vars.map((v: any) => ({
        name: v.nombre,
        label: v.etiqueta,
        type: v.tipo === 'numero' ? 'number' : v.tipo === 'booleano' ? 'boolean' : v.tipo === 'fecha' ? 'date' : 'text',
        required: v.requerido
      }));
    } catch {
      return [];
    }
  }, [tipoVenta, allTiposVenta]);

  const availableOrigins = useMemo(() => {
    const selectedTipo = allTiposVenta.find(t => t.codigo === tipoVenta || t.id.toString() === tipoVenta);
    if (!selectedTipo?.origenesJson) return ["presencial"];
    try {
      const parsed = JSON.parse(selectedTipo.origenesJson);
      return parsed.length > 0 ? parsed : ["presencial"];
    } catch {
      return ["presencial"];
    }
  }, [tipoVenta, allTiposVenta]);

  const handleProductSelect = (id: string | number) => {
    const stringId = id.toString();
    // Toggle logic
    let nextIds = [];
    if (productoIds.includes(stringId)) {
      nextIds = productoIds.filter(pid => pid !== stringId);
    } else {
      nextIds = [...productoIds, stringId];
    }
    setProductoIds(nextIds);

    // Dynamic schema and price prepopulation only runs based on the primary (first selected) product
    if (nextIds.length > 0) {
      const primaryId = nextIds[0];
      const prod = productos.find(p => p.id === Number(primaryId));
      if (prod) {
        // Priorizar el valor de la regla de comisión si existe
        const primaryRule = prod.productoReglas?.[0]?.reglaComision;
        if (primaryRule) {
          setPrecio(primaryRule.valorRemuneracionGross);
        } else {
          setPrecio(prod.precioOferta > 0 ? prod.precioOferta : prod.precio);
        }

        // We keep prepopulation of price for the first product
        // but we respect the pre-selected tipoVenta if it exists
        if (prod.tipoVentaId && !tipoVenta) {
           setTipoVenta(prod.tipoVentaId.toString());
        }
      }
    } else {
      setPrecio("");
      // Don't clear tipoVenta if the flow is Sale Type first
    }
  };

  const removeProduct = (id: string) => {
    setProductoIds(prev => prev.filter(pid => pid !== id));
  };

  const handleSaveNewClient = async (data: Partial<Cliente>) => {
    try {
      const res = await ClientesService.create(data as any);
      toast.success("Cliente creado");
      await refetchClients();
      setClienteId(res.id.toString());
      setClientModalOpen(false);
    } catch {
      toast.error("Error al crear cliente");
    }
  };

  const handleSubmit = async () => {
    if (!clienteId || productoIds.length === 0 || !tipoVenta) {
      toast.error("Faltan datos obligatorios");
      return;
    }

    // Validate dynamic schema required fields
    for (const field of dynamicSchema) {
      if (field.required && (dinamicos[field.name] === undefined || dinamicos[field.name] === "")) {
         if (field.type === "boolean") continue; 
         toast.error(`El campo "${field.label || field.name}" es obligatorio.`);
         return;
      }
    }

    const selectedTipoVentaObj = allTiposVenta.find(t => t.codigo === tipoVenta || t.id.toString() === tipoVenta);
    let initialEstado = { codigo: "PENDIENTE", nombre: "Pendiente", icono: "📋", color: "badge-neutral" };
    
    if (selectedTipoVentaObj?.estadosVentaJson) {
      try {
        const estados = JSON.parse(selectedTipoVentaObj.estadosVentaJson);
        const inicial = estados.find((e: any) => e.esInicial);
        if (inicial) {
          initialEstado = { 
            codigo: inicial.codigo, 
            nombre: inicial.nombre,
            icono: inicial.icono,
            color: inicial.color
          };
        }
      } catch (e) {}
    }

    setSubmitting(true);
    try {
      const payload = {
        clienteId: Number(clienteId),
        usuarioId: Number(user?.id || 1), 
        productoPrincipalId: Number(productoIds[0]),
        numeroVenta: `VTA-${Date.now().toString().slice(-6)}`,
        // Calculamos el monto total sumando todos los precios. 
        // Nota: Solo aplicamos el override manual de 'precio' si hay un solo producto (para compatibilidad).
        montoTotal: productoIds.length === 1 
                      ? (Number(precio) || 0) * (Number(cantidad) || 1)
                      : productoIds.reduce((sum, pid) => {
                          const prod = productos.find(p => p.id === Number(pid));
                          const prodPrice = prod?.productoReglas?.[0]?.reglaComision?.valorRemuneracionGross || (prod?.precioOferta > 0 ? prod.precioOferta : prod?.precio) || 0;
                          return sum + prodPrice;
                        }, 0) * (Number(cantidad) || 1),
        origenVenta,
        notas,
        fechaVenta: new Date().toISOString(),
        tipoVenta_Nombre: selectedTipoVentaObj?.nombre || tipoVenta,
        tipoVenta_Codigo: selectedTipoVentaObj?.codigo || tipoVenta,
        estado_Codigo: initialEstado.codigo,
        estado_Nombre: initialEstado.nombre,
        estado_Color: initialEstado.color,
        estado_Icono: initialEstado.icono,
        detalles: productoIds.map((pid, idx) => {
          const prod = productos.find(p => p.id === Number(pid));
          const prodPrice = prod?.productoReglas?.[0]?.reglaComision?.valorRemuneracionGross || (prod?.precioOferta > 0 ? prod.precioOferta : prod?.precio) || 0;
          
          return {
            id: 0,
            idProducto: Number(pid),
            cantidad: Number(cantidad) || 1,
            // Aplicar el override manual unicamente al producto primario (primer item) 
            // O si solo hay uno, para evitar desvirtuar los demás.
            total: (idx === 0 && productoIds.length === 1 ? (Number(precio) || 0) : prodPrice) * (Number(cantidad) || 1),
            datosConfiguracion: (idx === 0 && dynamicSchema.length > 0) ? JSON.stringify(dinamicos) : null
          };
        })
      };

      await createMutation.mutateAsync(payload);
      toast.success("Venta registrada con éxito");
      handleClose();
    } catch (error) {
      toast.error("Hubo un error al registrar la venta");
    } finally {
      setSubmitting(false);
    }
  };

  const handleClose = () => {
    setStep(1);
    setClienteId("");
    setTipoVenta("");
    setProductoIds([]);
    setOrigenVenta("presencial");
    setPrecio("");
    setCantidad(1);
    setDinamicos({});
    setNotas("");
    setClientSearch("");
    setProductSearch("");
    setProveedorId("");
    setProveedorSearch("");
    onClose();
  };

  return (
    <Dialog open={open} onOpenChange={(o) => !o && handleClose()}>
      <DialogContent className="glass border-border max-w-xl w-full max-h-[92vh] flex flex-col p-0 overflow-hidden">
        <DialogHeader className="p-6 pb-3 shrink-0 border-b border-border/40">
          <DialogTitle className="text-foreground">Registro de Venta Interactiva</DialogTitle>
        </DialogHeader>
        
        <div className="px-6 relative">
          <div className="flex justify-between mb-8 relative px-2">
            {[
              { id: 1, label: "Cliente" },
              { id: 2, label: "Producto" },
              { id: 3, label: "Cierre" }
            ].map((s) => (
              <div key={s.id} className="flex flex-col items-center gap-2 z-10">
                <div 
                  className={cn(
                    "h-8 w-8 rounded-full flex items-center justify-center text-xs font-bold transition-all duration-300 border-2",
                    s.id < step ? "bg-primary border-primary text-primary-foreground" : 
                    s.id === step ? "bg-background border-primary text-primary shadow-glow" : 
                    "bg-muted/50 border-muted text-muted-foreground"
                  )}
                >
                  {s.id < step ? <CheckCircle2 className="h-4 w-4" /> : s.id}
                </div>
                <span className={cn(
                  "text-[10px] font-bold uppercase tracking-widest transition-colors",
                  s.id <= step ? "text-primary" : "text-muted-foreground"
                )}>
                  {s.label}
                </span>
              </div>
            ))}
            <div className="absolute top-4 left-0 right-0 h-0.5 bg-muted -z-0 mx-10">
               <motion.div 
                 initial={{ width: "0%" }}
                 animate={{ width: `${(step - 1) * 50}%` }}
                 className="h-full bg-primary shadow-glow" 
               />
            </div>
          </div>
        </div>

        <div className="flex-1 overflow-y-auto px-6 pb-6 custom-scrollbar">
          <div className="min-h-[320px]">
          <AnimatePresence mode="wait">
            <motion.div key={step} initial={{ opacity: 0, y: 10 }} animate={{ opacity: 1, y: 0 }} exit={{ opacity: 0, y: -10 }} transition={{ duration: 0.2 }} className="overflow-visible">
              
              {/* STEP 1: Client */}
              {step === 1 && (
                <div className="space-y-6">
                  <div className="space-y-2">
                    <Label className="text-xs font-bold text-muted-foreground uppercase tracking-wider flex items-center gap-2">
                      <Plus className="h-3 w-3" /> Identificación del Cliente
                    </Label>
                    <div className="flex gap-2">
                      <Popover open={openClientCombo} onOpenChange={setOpenClientCombo}>
                        <PopoverTrigger asChild>
                          <Button 
                            variant="outline" 
                            role="combobox" 
                            aria-expanded={openClientCombo}
                            className="bg-background/50 border-border h-11 flex-1 shadow-inner justify-between font-normal"
                          >
                            <span className="truncate flex-1 text-left">
                               {clienteId ? clients.find(c => c.id.toString() === clienteId)?.nombre : "Seleccionar cliente..."}
                            </span>
                            <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                          </Button>
                        </PopoverTrigger>
                        <PopoverContent className="w-[var(--radix-popover-trigger-width)] p-0 glass border-border" align="start">
                          <Command shouldFilter={false}>
                            <CommandInput 
                              placeholder="Buscar cliente (Nombre, DNI, Email, Tel)..." 
                              value={clientSearch} 
                              onValueChange={setClientSearch} 
                            />
                            <CommandList>
                              {filteredClients.length === 0 && <CommandEmpty>No se encontró ningún cliente.</CommandEmpty>}
                              <CommandGroup>
                                {filteredClients.map(c => (
                                  <CommandItem
                                    key={c.id}
                                    value={c.id.toString()}
                                    onSelect={(val) => {
                                      setClienteId(val);
                                      setOpenClientCombo(false);
                                    }}
                                  >
                                    <Check className={cn("mr-2 h-4 w-4 shrink-0", clienteId === c.id.toString() ? "opacity-100" : "opacity-0")} />
                                    <div className="flex flex-col overflow-hidden">
                                      <span className="truncate">{c.nombre}</span>
                                      <span className="text-[10px] text-muted-foreground truncate">
                                        {[c.dni, c.movil, c.email].filter(Boolean).join(" · ")}
                                      </span>
                                    </div>
                                  </CommandItem>
                                ))}
                              </CommandGroup>
                            </CommandList>
                          </Command>
                        </PopoverContent>
                      </Popover>
                      <Button variant="outline" size="icon" className="h-11 w-11 border-border/50 hover:bg-primary/5" onClick={() => setClientModalOpen(true)} title="Nuevo Cliente">
                        <Plus className="h-5 w-5" />
                      </Button>
                    </div>
                  </div>
                </div>
              )}

              {/* Step 2: Configuration & Products */}
              {step === 2 && (
                <div className="space-y-6">
                  {/* Row 1: Tipo de Venta (FIRST) */}
                  <div className="space-y-3 p-4 rounded-2xl bg-primary/5 border border-primary/10 shadow-sm">
                    <Label className="text-xs font-black text-primary uppercase tracking-widest flex items-center justify-center gap-2">
                      <Tag className="h-4 w-4" /> 1. Seleccionar Tipo de Venta
                    </Label>
                    <Select value={tipoVenta} onValueChange={(val) => {
                      setTipoVenta(val);
                      setProveedorId("");
                      setProductoIds([]);
                    }}>
                      <SelectTrigger className="bg-background border-primary/20 h-12 shadow-md max-w-[300px] mx-auto text-center justify-center font-bold text-base">
                        <SelectValue placeholder="Elegir tipo de venta..." />
                      </SelectTrigger>
                      <SelectContent className="glass border-border">
                        {allTiposVenta.map(t => (
                          <SelectItem key={t.id} value={t.id.toString()} className="font-medium">
                            {t.nombre}
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                  </div>

                  <AnimatePresence>
                    {tipoVenta && (
                      <motion.div 
                        initial={{ opacity: 0, height: 0 }} 
                        animate={{ opacity: 1, height: "auto" }} 
                        exit={{ opacity: 0, height: 0 }}
                        className="space-y-6"
                      >
                        {/* Row 2: Proveedor & Productos */}
                        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                          <div className="space-y-2">
                            <Label className="text-[10px] font-black text-muted-foreground uppercase tracking-widest">2. Proveedor</Label>
                            <Popover open={openProveedorCombo} onOpenChange={setOpenProveedorCombo}>
                              <PopoverTrigger asChild>
                                <Button 
                                  variant="outline" 
                                  role="combobox" 
                                  className="w-full h-11 bg-background/50 border-border justify-between px-3"
                                >
                                  <span className="truncate">{proveedorId ? proveedores.find(p => p.id.toString() === proveedorId)?.nombre : "Cualquier proveedor"}</span>
                                  <ChevronsUpDown className="h-4 w-4 opacity-50" />
                                </Button>
                              </PopoverTrigger>
                              <PopoverContent className="w-[var(--radix-popover-trigger-width)] p-0">
                                <Command shouldFilter={false}>
                                  <CommandInput placeholder="Buscar proveedor..." value={proveedorSearch} onValueChange={setProveedorSearch} />
                                  <CommandList>
                                    {filteredProveedores.length === 0 && <CommandEmpty>No hay proveedores para este tipo.</CommandEmpty>}
                                    <CommandGroup>
                                      {filteredProveedores.map(p => (
                                        <CommandItem 
                                          key={p.id} 
                                          onSelect={() => {
                                            setProveedorId(p.id.toString());
                                            setOpenProveedorCombo(false);
                                          }}
                                        >
                                          <Check className={cn("mr-2 h-4 w-4", proveedorId === p.id.toString() ? "opacity-100" : "opacity-0")} />
                                          {p.nombre}
                                        </CommandItem>
                                      ))}
                                    </CommandGroup>
                                  </CommandList>
                                </Command>
                              </PopoverContent>
                            </Popover>
                          </div>

                          <div className="space-y-2">
                            <Label className="text-[10px] font-black text-primary uppercase tracking-widest">3. Añadir Productos</Label>
                            <Popover open={openProductCombo} onOpenChange={setOpenProductCombo}>
                              <PopoverTrigger asChild>
                                <Button 
                                  variant="outline" 
                                  className="w-full h-11 bg-primary/5 border-primary/20 hover:bg-primary/10 transition-all font-bold justify-between px-3"
                                >
                                  <span>{productSearch ? "Buscando..." : "Explorar catálogo..."}</span>
                                  <Plus className="h-4 w-4 text-primary" />
                                </Button>
                              </PopoverTrigger>
                              <PopoverContent className="w-80 p-0 glass border-border" align="end">
                                <Command shouldFilter={false}>
                                  <CommandInput placeholder="Buscar por nombre o descripción..." value={productSearch} onValueChange={setProductSearch} />
                                  <CommandList>
                                    {filteredProducts.length === 0 && <CommandEmpty>No se encontraron productos.</CommandEmpty>}
                                    <CommandGroup>
                                      {filteredProducts.map(p => (
                                        <CommandItem
                                          key={p.id}
                                          onSelect={() => {
                                            handleProductSelect(p.id);
                                            setOpenProductCombo(false);
                                          }}
                                        >
                                          <div className="flex flex-col">
                                            <span className="font-bold">{p.nombre}</span>
                                            <span className="text-[10px] text-muted-foreground">{p.proveedor?.nombre}</span>
                                          </div>
                                          {productoIds.includes(p.id.toString()) && <Check className="ml-auto h-4 w-4 text-primary" />}
                                        </CommandItem>
                                      ))}
                                    </CommandGroup>
                                  </CommandList>
                                </Command>
                              </PopoverContent>
                            </Popover>
                          </div>
                        </div>

                        {/* Selected Products List */}
                        {productoIds.length > 0 && (
                          <div className="space-y-3">
                            <Label className="text-[10px] font-black text-muted-foreground uppercase tracking-widest">Productos Asignados</Label>
                            <div className="grid gap-2">
                              <AnimatePresence>
                                {productoIds.map((pid, idx) => {
                                  const prod = productos.find(p => p.id.toString() === pid);
                                  if (!prod) return null;
                                  return (
                                    <motion.div 
                                      key={pid}
                                      initial={{ opacity: 0, x: -10 }}
                                      animate={{ opacity: 1, x: 0 }}
                                      exit={{ opacity: 0, x: 10 }}
                                      className="flex items-center justify-between p-3 rounded-xl bg-background border border-border/60 shadow-sm group hover:border-primary/40 transition-colors"
                                    >
                                      <div className="flex items-center gap-3">
                                        <div className="h-8 w-8 rounded-full bg-primary/10 flex items-center justify-center text-primary font-bold text-xs">
                                          {idx + 1}
                                        </div>
                                        <div className="flex flex-col">
                                          <span className="text-sm font-bold">{prod.nombre}</span>
                                          <span className="text-[10px] text-muted-foreground uppercase">{prod.proveedor?.nombre || "General"}</span>
                                        </div>
                                      </div>
                                      <Button 
                                        variant="ghost" 
                                        size="icon" 
                                        className="h-8 w-8 text-muted-foreground hover:text-destructive hover:bg-destructive/10 opacity-0 group-hover:opacity-100 transition-opacity"
                                        onClick={() => removeProduct(pid)}
                                      >
                                        <Trash2 className="h-4 w-4" />
                                      </Button>
                                    </motion.div>
                                  );
                                })}
                              </AnimatePresence>
                            </div>
                          </div>
                        )}

                        {/* Dynamic Variables Section */}
                        {dynamicSchema.length > 0 && (
                          <div className="p-6 rounded-3xl border border-primary/20 bg-primary/5 space-y-6 shadow-inner relative overflow-hidden">
                            <div className="absolute top-0 right-0 p-4 opacity-5 pointer-events-none">
                              <Settings className="h-16 w-16 text-primary" />
                            </div>
                            <div className="flex items-center justify-between border-b border-primary/10 pb-4">
                              <h4 className="text-xs font-black uppercase text-primary tracking-[0.2em] flex items-center gap-2">
                                <Tag className="h-4 w-4" /> Configuración de Servicio
                              </h4>
                              <Badge variant="outline" className="text-[9px] border-primary/30 text-primary bg-primary/5 uppercase px-2 py-0.5 rounded-full">Variables Activas</Badge>
                            </div>
                            <div className="grid gap-5">
                              {dynamicSchema.map((field: any, idx: number) => (
                                <div key={idx} className="space-y-2 flex flex-col justify-center animate-in fade-in slide-in-from-bottom-1 duration-300">
                                  {field.type === "boolean" ? (
                                    <div className="flex items-center justify-between bg-background/40 p-3 rounded-xl border border-border/40">
                                      <Label className="text-sm cursor-pointer font-bold">{field.label || field.name} {field.required && <span className="text-destructive">*</span>}</Label>
                                      <Switch 
                                        checked={dinamicos[field.name] || false} 
                                        onCheckedChange={(c) => setDinamicos({...dinamicos, [field.name]: c})} 
                                      />
                                    </div>
                                  ) : (
                                    <>
                                      <Label className="text-[10px] text-muted-foreground font-black uppercase tracking-widest pl-1">{field.label || field.name} {field.required && <span className="text-destructive">*</span>}</Label>
                                      <Input 
                                        type={field.type === "number" ? "number" : field.type === "date" ? "date" : "text"}
                                        value={dinamicos[field.name] || ""} 
                                        onChange={(e) => setDinamicos({...dinamicos, [field.name]: field.type === "number" ? (e.target.value === '' ? '' : Number(e.target.value)) : e.target.value})}
                                        className="h-11 bg-background/60 border-border shadow-sm focus:border-primary/50 transition-all rounded-xl"
                                        placeholder={`Escribir ${field.label?.toLowerCase() || field.name.toLowerCase()}...`}
                                      />
                                    </>
                                  )}
                                </div>
                              ))}
                            </div>
                          </div>
                        )}
                      </motion.div>
                    )}
                  </AnimatePresence>

                  {!tipoVenta && (
                    <div className="flex flex-col items-center justify-center p-16 border-2 border-dashed rounded-[2.5rem] border-primary/10 bg-primary/5 gap-4">
                      <div className="h-16 w-16 rounded-3xl bg-primary/10 flex items-center justify-center animate-pulse">
                        <Tag className="h-8 w-8 text-primary/40" />
                      </div>
                      <div className="text-center">
                        <p className="text-sm font-bold text-primary/60">Configure el tipo de venta para comenzar</p>
                        <p className="text-[11px] text-muted-foreground mt-1">El catálogo se adaptará según su selección.</p>
                      </div>
                    </div>
                  )}
                </div>
              )}

              {/* Step 3: CIERRE (Summary) */}
              {step === 3 && (
                <div className="space-y-6">
                   <div className="p-8 glass rounded-[2.5rem] border-2 border-primary/20 bg-primary/5 space-y-6 shadow-2xl relative overflow-hidden group">
                    <div className="absolute top-0 right-0 p-6 opacity-5 group-hover:scale-110 transition-transform duration-500">
                      <CheckCircle2 className="h-32 w-32 text-primary" />
                    </div>
                    
                    <div className="text-center space-y-1 pb-2">
                      <h4 className="text-xs font-black text-primary uppercase tracking-[0.3em]">Cierre de Operación</h4>
                      <p className="text-[10px] text-muted-foreground font-medium italic">Revise los detalles antes de confirmar</p>
                    </div>

                    <div className="grid grid-cols-2 gap-6 bg-background/40 p-6 rounded-[2rem] border border-white/5 shadow-inner">
                      <div className="space-y-1">
                        <p className="text-[9px] text-muted-foreground uppercase font-black tracking-widest">Cliente</p>
                        <p className="font-bold text-sm text-foreground truncate">{clients.find(c => c.id.toString() === clienteId)?.nombre}</p>
                      </div>
                      <div className="space-y-1">
                        <p className="text-[9px] text-muted-foreground uppercase font-black tracking-widest">Canal / Vendedor</p>
                        <p className="font-bold text-sm text-foreground truncate">{user?.nombre}</p>
                      </div>
                      <div className="col-span-2 space-y-2">
                        <p className="text-[9px] text-muted-foreground uppercase font-black tracking-widest">Servicios Contratados</p>
                        <div className="flex flex-wrap gap-1.5">
                           {productoIds.map(pid => {
                              const p = productos.find(prod => prod.id.toString() === pid);
                              if (!p) return null;
                              return <Badge key={pid} variant="secondary" className="bg-primary/20 text-primary border-primary/10 font-bold px-3 py-1 rounded-full">{p.nombre}</Badge>
                           })}
                        </div>
                      </div>
                    </div>
                    
                    <div className="flex items-center justify-between px-6 py-4 bg-primary text-primary-foreground rounded-2xl shadow-glow">
                       <div className="flex flex-col">
                         <span className="text-[8px] font-black uppercase tracking-widest opacity-70">Tipo de Venta</span>
                         <span className="text-sm font-black">{allTiposVenta.find(t => t.id.toString() === tipoVenta || t.codigo === tipoVenta)?.nombre || "—"}</span>
                       </div>
                       <div className="text-right flex flex-col">
                         <span className="text-[8px] font-black uppercase tracking-widest opacity-70">Total Estimado</span>
                         <span className="text-xl font-black font-mono">{precio ? `${precio} €` : "Pendiente"}</span>
                       </div>
                    </div>

                    {Object.keys(dinamicos).length > 0 && (
                      <div className="space-y-2 pt-2 border-t border-primary/10">
                        <p className="text-[9px] text-muted-foreground uppercase font-black tracking-widest">Configuración del Servicio</p>
                        <div className="grid grid-cols-2 gap-x-4 gap-y-2">
                          {dynamicSchema.map(field => {
                            const value = dinamicos[field.name];
                            if (value === undefined || value === "") return null;
                            return (
                              <div key={field.name} className="flex justify-between items-center text-[11px] py-1 border-b border-white/5 last:border-0">
                                <span className="text-muted-foreground font-medium">{field.label || field.name}:</span>
                                <span className="font-bold text-foreground">
                                  {field.type === "boolean" ? (value ? "Sí" : "No") : value}
                                </span>
                              </div>
                            );
                          })}
                        </div>
                      </div>
                    )}
                  </div>

                  <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                    <div className="space-y-3">
                      <Label className="text-[10px] font-black text-muted-foreground uppercase tracking-widest pl-2">Origen de Venta</Label>
                      <div className="flex flex-wrap gap-2">
                         {availableOrigins.map((orig: string) => (
                           <button
                             key={orig}
                             type="button"
                             onClick={() => setOrigenVenta(orig)}
                             className={cn(
                               "px-4 py-2.5 rounded-2xl border text-[10px] font-black transition-all uppercase bg-background/50 flex-1 min-w-[80px]",
                               origenVenta === orig 
                                 ? "bg-primary/10 border-primary text-primary shadow-glow scale-105" 
                                 : "border-border hover:border-primary/40 text-muted-foreground opacity-60"
                             )}
                           >
                             {orig}
                           </button>
                         ))}
                      </div>
                    </div>

                    <div className="space-y-3">
                      <Label className="text-[10px] font-black text-muted-foreground uppercase tracking-widest pl-2">Notas Finales</Label>
                      <Textarea 
                        placeholder="Cualquier observación relevante..." 
                        value={notas} 
                        onChange={e => setNotas(e.target.value)} 
                        className="bg-background/50 border-border min-h-[80px] rounded-2xl resize-none focus:ring-primary/20 text-xs p-3" 
                      />
                    </div>
                  </div>
                </div>
              )}
            </motion.div>
          </AnimatePresence>
          </div>
        </div>

        <div className="p-4 border-t border-border bg-muted/10 flex justify-between">
          <div>
            {step > 1 && <Button variant="outline" onClick={() => setStep(step - 1)}>Anterior</Button>}
          </div>
          <div className="flex gap-2">
            <Button variant="ghost" onClick={handleClose} disabled={submitting}>Cancelar</Button>
            {step < 3 ? (
               <Button onClick={() => setStep(step + 1)} disabled={
                 (step === 1 && !clienteId) || 
                 (step === 2 && (productoIds.length === 0 || !tipoVenta))
               }>Siguiente</Button>
            ) : (
               <Button onClick={handleSubmit} disabled={submitting} className="bg-primary hover:bg-primary/90 text-primary-foreground font-bold">
                 {submitting ? "Procesando..." : "Confirmar Venta"}
               </Button>
            )}
          </div>
        </div>
      </DialogContent>

      <ClienteFormModal
        open={clientModalOpen}
        onClose={() => setClientModalOpen(false)}
        onSave={handleSaveNewClient}
      />
    </Dialog>
  );
}
