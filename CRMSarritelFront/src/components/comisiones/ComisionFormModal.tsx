import { useEffect, useMemo, useState } from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import { useQuery } from '@tanstack/react-query';
import { useComisiones } from "@/hooks/useComisiones";
import { usePeriodos } from "@/hooks/usePeriodos";
import { UsuariosService } from "@/services/api/usuarios.service";
import { Comision, SaleCommissionSummary, BeneficiarySummary, Usuario } from "@/types";
import { toast } from "sonner";
import apiClient from "@/lib/axios";
import { 
  Users, AlertTriangle, Coins, TrendingUp, ShieldCheck, 
  UserCircle2, Trash2, Plus, UserPlus, Users2, 
  Briefcase, Calendar, Star, Info, Wallet
} from "lucide-react";
import { cn } from "@/lib/utils";
import { Separator } from "@/components/ui/separator";
import { Select, SelectContent, SelectGroup, SelectItem, SelectLabel, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Switch } from "@/components/ui/switch";

interface ComisionFormModalProps {
  open: boolean;
  onClose: () => void;
  initialData?: Comision;
  refetchSummary?: () => void;
}

export function ComisionFormModal({ open, onClose, initialData, refetchSummary: propRefetch }: ComisionFormModalProps) {
  const { refetch: refetchCommissions, createComision, updateComision } = useComisiones();
  const { periodos = [] } = usePeriodos();
  const [loadingSummary, setLoadingSummary] = useState(false);
  const [saving, setSaving] = useState(false);
  const [localSummary, setLocalSummary] = useState<SaleCommissionSummary | null>(null);

  // Modo de creación: Extra (Manual) vs Distribución (Venta)
  const [isExtraMode, setIsExtraMode] = useState(false);
  
  // Estado para creación manual individual
  const [manualData, setManualData] = useState<Partial<Comision>>({
    empleadoId: 0,
    montoComision: 0,
    baseCalculo: 0,
    periodo: "",
    notas: "",
    esExtra: true,
    proveedorId: undefined
  });

  const { data: usuarios = [] } = useQuery({ queryKey: ["usuarios"], queryFn: UsuariosService.getAll, enabled: open });
  const { data: equipos = [] } = useQuery({ 
    queryKey: ["equipos"], 
    queryFn: async () => {
      const res = await apiClient.get("/Equipos");
      return res.data;
    },
    enabled: open
  });

  const { data: proveedores = [] } = useQuery({
    queryKey: ["proveedores"],
    queryFn: async () => {
      const res = await apiClient.get("/Proveedores");
      return res.data;
    },
    enabled: open
  });

  // Determinar modo inicial y cargar periodo principal
  useEffect(() => {
    if (open) {
      const principal = periodos.find(p => p.esPrincipal);
      const defaultPeriod = principal?.nombre || new Date().toLocaleString('es-ES', { month: 'long', year: 'numeric' });

      if (initialData?.id) {
        setIsExtraMode(initialData.esExtra || false);
        setManualData({
           ...initialData,
           periodo: initialData.periodo || defaultPeriod
        });
      } else if (!initialData?.ventaId) {
        setIsExtraMode(true);
        setManualData(prev => ({ ...prev, periodo: defaultPeriod }));
      } else {
        setIsExtraMode(false);
      }
    }
  }, [open, initialData, periodos]);

  // Cargar el resumen de la repartición completa si estamos en modo Distribución
  useEffect(() => {
    if (open && initialData?.ventaId && !isExtraMode) {
      setLoadingSummary(true);
      const url = `/Comisiones/sale-summary/${initialData.ventaId}${initialData.detalleVentaId ? `?detalleVentaId=${initialData.detalleVentaId}` : ""}`;
      apiClient.get(url)
        .then(res => {
          setLocalSummary(res.data);
        })
        .catch(err => {
          console.error("Error cargando resumen de comisiones:", err);
          toast.error("No se pudo cargar la repartición completa");
        })
        .finally(() => setLoadingSummary(false));
    }
  }, [open, initialData, isExtraMode]);

  const stats = useMemo(() => {
    if (!localSummary) return { totalHumans: 0, remanente: 0, isOverBudget: false };
    const humans = localSummary.beneficiarios.filter(b => b.empleadoId !== 99).reduce((sum, b) => sum + b.monto, 0);
    const remanente = localSummary.baseBruta - humans;
    const isOverBudget = humans > localSummary.baseBruta + 0.05;
    return { totalHumans: humans, remanente, isOverBudget };
  }, [localSummary]);

  const handleMontoChange = (beneficiaryIndex: number, newMonto: number) => {
    if (!localSummary) return;
    const updatedBeneficiarios = [...localSummary.beneficiarios];
    updatedBeneficiarios[beneficiaryIndex] = { ...updatedBeneficiarios[beneficiaryIndex], monto: Math.max(0, newMonto) };
    setLocalSummary({ ...localSummary, beneficiarios: updatedBeneficiarios });
  };

  const handleAddParticipant = (userId: string) => {
    if (!localSummary) return;
    const user = usuarios.find(u => String(u.id) === userId);
    if (!user || localSummary.beneficiarios.some(b => b.empleadoId === user.id)) return;

    const newParticipant: BeneficiarySummary = {
      id: 0,
      detalleVentaId: localSummary.detalleVentaId,
      empleadoId: user.id,
      nombre: user.id === 99 ? "SISTEMA" : user.nombre,
      monto: 0,
      tipo: "Ajuste Manual",
      estado: "Activa"
    };

    setLocalSummary({ ...localSummary, beneficiarios: [newParticipant, ...localSummary.beneficiarios] });
  };

  const handleSave = async () => {
    setSaving(true);
    try {
      if (isExtraMode) {
        if (!manualData.empleadoId || manualData.montoComision === undefined || !manualData.periodo) {
           toast.error("Complete los campos obligatorios (Beneficiario, Monto y Periodo)");
           setSaving(false);
           return;
        }

        if (manualData.id) {
          await updateComision(manualData as Comision & { id: number });
        } else {
          // Asegurar que forzamos creación de tipo MANUAL si es extra
          const payload = { 
            ...manualData, 
            tipo: { codigo: "MANUAL", nombre: "Ajuste Manual" },
            estado: { codigo: "ACTI", nombre: "Activa" } // Forzar activa para que aparezca
          };
          await createComision(payload);
        }
        onClose();
        setTimeout(() => refetchCommissions(), 500); // Dar margen al server
      } else {
        if (!localSummary || stats.isOverBudget) return;
        await apiClient.post("/Comisiones/update-group", localSummary);
        toast.success("Repartición sincronizada");
        onClose();
        if (propRefetch) propRefetch();
        refetchCommissions();
      }
    } catch (err: any) {
      toast.error(err.response?.data?.message || "Error al guardar");
    } finally {
      setSaving(false);
    }
  };

  return (
    <Dialog open={open} onOpenChange={(o) => (!o ? onClose() : null)}>
      <DialogContent className="glass border-border max-w-2xl overflow-hidden flex flex-col p-0 gap-0 rounded-[2rem] shadow-2xl">
        <DialogHeader className="p-8 pb-4">
          <div className="flex justify-between items-start">
            <DialogTitle className="text-2xl font-bold flex items-center gap-3">
              <div className="p-2.5 rounded-2xl bg-primary/10 text-primary shadow-sm border border-primary/10">
                <Coins className="w-6 h-6" />
              </div>
              <div className="flex flex-col text-left">
                <span>{isExtraMode ? "Nueva Comisión Manual" : "Repartición de Comisiones"}</span>
                <p className="text-[10px] font-normal text-muted-foreground mt-0.5 uppercase tracking-widest">
                  {isExtraMode ? "Concepto Extra / Ajuste" : `Venta: ${localSummary?.numeroVenta || initialData?.venta_Numero || "..."}`}
                </p>
              </div>
            </DialogTitle>
            
            {!initialData?.id && (
              <div className="flex flex-col items-end gap-2 pr-4">
                <Label className="text-[9px] font-black uppercase text-muted-foreground tracking-tighter">Modo Standalone</Label>
                <Switch 
                  checked={isExtraMode} 
                  onCheckedChange={setIsExtraMode} 
                  className="data-[state=checked]:bg-amber-500"
                />
              </div>
            )}
          </div>
        </DialogHeader>

        <div className="flex-1 overflow-y-auto px-8 pb-8 pt-2 space-y-6 max-h-[80vh]">
          {isExtraMode ? (
            <div className="space-y-6 animate-in fade-in slide-in-from-right-4 duration-300">
              <div className="grid grid-cols-2 gap-6">
                <div className="space-y-2">
                  <Label className="text-[11px] font-black uppercase text-muted-foreground flex items-center gap-2">
                    <UserCircle2 className="w-3.5 h-3.5" /> Beneficiario
                  </Label>
                  <Select 
                    value={String(manualData.empleadoId)} 
                    onValueChange={(v) => setManualData(prev => ({ ...prev, empleadoId: Number(v) }))}
                  >
                    <SelectTrigger className="h-12 bg-muted/20 border-border/40 rounded-2xl">
                      <SelectValue placeholder="Seleccionar usuario..." />
                    </SelectTrigger>
                    <SelectContent className="glass">
                      {usuarios.map(u => (
                        <SelectItem key={u.id} value={String(u.id)}>{u.id === 99 ? "SISTEMA (Margen Empresa)" : u.nombre}</SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                </div>

                <div className="space-y-2">
                  <Label className="text-[11px] font-black uppercase text-muted-foreground flex items-center gap-2">
                    <Star className="w-3.5 h-3.5 text-amber-500" /> Es Concepto Extra?
                  </Label>
                  <div className="flex items-center gap-3 h-12 px-4 bg-muted/10 rounded-2xl border border-border/40">
                    <Switch 
                      checked={manualData.esExtra} 
                      onCheckedChange={(v) => setManualData(prev => ({ ...prev, esExtra: v }))} 
                    />
                    <span className="text-xs font-bold text-foreground">
                      {manualData.esExtra ? "SÍ (Bono/Incentivo)" : "NO (Ajuste Manual)"}
                    </span>
                  </div>
                </div>
              </div>

              <div className="grid grid-cols-2 gap-6">
                <div className="space-y-2">
                  <Label className="text-[11px] font-black uppercase text-muted-foreground flex items-center gap-2">
                    <Wallet className="w-3.5 h-3.5" /> Monto Comisión
                  </Label>
                  <div className="relative">
                    <span className="absolute left-4 top-1/2 -translate-y-1/2 text-sm font-bold opacity-30">€</span>
                    <Input 
                      type="number" 
                      value={manualData.montoComision} 
                      onChange={(e) => setManualData(prev => ({ ...prev, montoComision: Number(e.target.value) }))}
                      className="h-12 pl-8 font-black text-lg bg-muted/20 border-border/40 rounded-2xl"
                    />
                  </div>
                </div>

                <div className="space-y-2">
                  <Label className="text-[11px] font-black uppercase text-muted-foreground flex items-center gap-2">
                    <Briefcase className="w-3.5 h-3.5" /> Proveedor Asociado
                  </Label>
                  <Select 
                    value={String(manualData.proveedorId)} 
                    onValueChange={(v) => setManualData(prev => ({ ...prev, proveedorId: Number(v) }))}
                  >
                    <SelectTrigger className="h-12 bg-muted/20 border-border/40 rounded-2xl">
                      <SelectValue placeholder="Opcional..." />
                    </SelectTrigger>
                    <SelectContent className="glass">
                      {proveedores.map((p: any) => (
                        <SelectItem key={p.id} value={String(p.id)}>{p.nombre}</SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                </div>
              </div>

              <div className="grid grid-cols-2 gap-6">
                <div className="space-y-2">
                  <Label className="text-[11px] font-black uppercase text-muted-foreground flex items-center gap-2">
                    <Calendar className="w-3.5 h-3.5" /> Periodo / Mes
                  </Label>
                  <Select 
                    value={manualData.periodo} 
                    onValueChange={(v) => setManualData(prev => ({ ...prev, periodo: v }))}
                  >
                    <SelectTrigger className="h-12 bg-muted/20 border-border/40 rounded-2xl font-bold">
                      <SelectValue placeholder="Seleccionar periodo..." />
                    </SelectTrigger>
                    <SelectContent className="glass">
                      {periodos.map(p => (
                        <SelectItem key={p.id} value={p.nombre}>{p.nombre} {p.esPrincipal ? "(Principal)" : ""}</SelectItem>
                      ))}
                      {!periodos.some(p => p.nombre === manualData.periodo) && manualData.id && manualData.periodo && (
                         <SelectItem value={manualData.periodo}>{manualData.periodo} (No listado)</SelectItem>
                      )}
                    </SelectContent>
                  </Select>
                </div>
                
                {!manualData.esExtra && (
                  <div className="space-y-2">
                    <Label className="text-[11px] font-black uppercase text-muted-foreground flex items-center gap-2">
                      <Info className="w-3.5 h-3.5" /> Ref. Venta (Opcional)
                    </Label>
                    <Input 
                      placeholder="Ej: CRT-001"
                      className="h-12 bg-muted/20 border-border/40 rounded-2xl font-mono text-xs"
                    />
                  </div>
                )}
              </div>

              <div className="space-y-2">
                <Label className="text-[11px] font-black uppercase text-muted-foreground">Notas / Justificación</Label>
                <Textarea 
                  value={manualData.notas} 
                  onChange={(e) => setManualData(prev => ({ ...prev, notas: e.target.value }))}
                  placeholder="Explique el motivo de este cargo manual..."
                  className="min-h-[100px] bg-muted/10 border-border/40 rounded-[1.5rem] p-4 text-sm resize-none"
                />
              </div>
            </div>
          ) : (
            loadingSummary ? (
              <div className="py-20 flex flex-col items-center gap-4 animate-pulse">
                <div className="h-12 w-12 rounded-full border-4 border-t-primary border-primary/20 animate-spin" />
                <p className="text-sm font-medium text-muted-foreground italic">Analizando flujos monetarios...</p>
              </div>
            ) : localSummary ? (
              <div className="space-y-6 animate-in slide-in-from-left-4 duration-500">
                <div className="grid grid-cols-2 gap-4">
                  <div className="p-6 bg-primary/10 rounded-[2rem] border border-primary/20 shadow-sm text-left">
                    <div className="flex items-center gap-2 mb-2">
                      <TrendingUp className="w-3.5 h-3.5 text-primary opacity-60" />
                      <span className="text-[10px] font-black uppercase text-primary tracking-widest">Base Disponible</span>
                    </div>
                    <div className="flex items-baseline gap-1.5 font-black font-mono text-primary text-3xl">
                      {localSummary.baseBruta.toLocaleString()} <span className="text-sm opacity-40">€</span>
                    </div>
                  </div>

                  <div className={cn(
                    "p-6 rounded-[2rem] border shadow-sm transition-all text-right flex flex-col items-end justify-center",
                    stats.isOverBudget ? "bg-destructive/10 border-destructive/20" : "bg-emerald-500/10 border-emerald-500/20"
                  )}>
                    <div className="flex items-center gap-2 mb-2">
                      <span className={cn("text-[10px] font-black uppercase tracking-widest", stats.isOverBudget ? "text-destructive" : "text-emerald-600")}>
                        {stats.isOverBudget ? "Déficit Detectado" : "Margen Empresa"}
                      </span>
                      <ShieldCheck className={cn("w-3.5 h-3.5 opacity-60", stats.isOverBudget ? "text-destructive" : "text-emerald-600")} />
                    </div>
                    <div className={cn("text-3xl font-black font-mono flex items-baseline gap-1.5", stats.isOverBudget ? "text-destructive" : "text-emerald-600")}>
                      {stats.remanente.toLocaleString()} <span className="text-sm opacity-40">€</span>
                    </div>
                  </div>
                </div>

                <div className="grid grid-cols-2 gap-4">
                   <div className="space-y-2 text-left">
                      <Label className="text-[10px] font-black uppercase text-muted-foreground tracking-widest ml-1 flex items-center gap-2"><UserPlus className="w-3 h-3" /> Añadir Agente</Label>
                      <Select onValueChange={handleAddParticipant}>
                         <SelectTrigger className="h-10 bg-muted/20 border-border/40 rounded-xl">
                            <SelectValue placeholder="Seleccionar..." />
                         </SelectTrigger>
                         <SelectContent className="glass">
                            {usuarios.map(u => (
                               <SelectItem key={u.id} value={String(u.id)}>{u.id === 99 ? "SISTEMA (Margen Empresa)" : u.nombre}</SelectItem>
                            ))}
                         </SelectContent>
                      </Select>
                   </div>
                   <div className="space-y-2 text-left">
                      <Label className="text-[10px] font-black uppercase text-muted-foreground tracking-widest ml-1 flex items-center gap-2"><Users2 className="w-3 h-3" /> Añadir Equipo</Label>
                      <Select onValueChange={(v) => {
                         const team = equipos.find((e: any) => String(e.id) === v);
                         if (team) {
                            const newParticipants: BeneficiarySummary[] = [];
                            team.usuarioEquipos?.filter((ue: any) => ue.esManager).forEach((m: any) => {
                               if (!localSummary.beneficiarios.some(b => b.empleadoId === m.usuarioId)) {
                                  const user = usuarios.find(u => u.id === m.usuarioId);
                                  newParticipants.push({ id: 0, detalleVentaId: localSummary.detalleVentaId, empleadoId: m.usuarioId, nombre: user?.nombre || "Manager", monto: 0, tipo: "Gestión Equipo", estado: "Activa" });
                               }
                            });
                            setLocalSummary({ ...localSummary, beneficiarios: [...newParticipants, ...localSummary.beneficiarios] });
                         }
                      }}>
                         <SelectTrigger className="h-10 bg-muted/20 border-border/40 rounded-xl">
                            <SelectValue placeholder="Seleccionar..." />
                         </SelectTrigger>
                         <SelectContent className="glass">
                            {equipos.map((e: any) => (
                               <SelectItem key={e.id} value={String(e.id)}>{e.nombre}</SelectItem>
                            ))}
                         </SelectContent>
                      </Select>
                   </div>
                </div>

                <div className="rounded-[2.5rem] border border-border/50 bg-muted/20 overflow-hidden shadow-inner">
                  <div className="divide-y divide-border/30">
                    {localSummary.beneficiarios.map((b, idx) => (
                      <div key={b.id || `new-${idx}`} className={cn("flex items-center justify-between p-5", b.empleadoId === 99 ? "bg-primary/5" : "hover:bg-background/80")}>
                        <div className="flex items-center gap-4">
                          <div className={cn("h-11 w-11 rounded-2xl flex items-center justify-center", b.empleadoId === 99 ? "bg-primary text-primary-foreground" : "bg-muted text-muted-foreground border")}>
                            {b.empleadoId === 99 ? <ShieldCheck className="w-5 h-5" /> : <UserCircle2 className="w-5 h-5" />}
                          </div>
                          <div className="flex flex-col text-left">
                            <span className={cn("text-sm font-bold", b.empleadoId === 99 && "text-primary uppercase")}>{b.nombre}</span>
                            <span className="text-[10px] text-muted-foreground/70 font-medium uppercase tracking-tighter">{b.tipo}</span>
                          </div>
                        </div>
                        <div className="flex items-center gap-3">
                           {b.empleadoId === 99 ? (
                             <span className="text-xl font-black font-mono text-primary/80 pr-6">{stats.remanente.toLocaleString()} €</span>
                           ) : (
                             <>
                               <div className="relative w-32">
                                 <span className="absolute left-3 top-1/2 -translate-y-1/2 text-[10px] font-bold opacity-30">€</span>
                                 <Input 
                                    type="number" 
                                    value={b.monto || ""} 
                                    onChange={(e) => handleMontoChange(idx, parseFloat(e.target.value) || 0)}
                                    className="h-10 pl-7 text-right font-black rounded-xl border-border/60"
                                  />
                               </div>
                               <Button variant="ghost" size="icon" className="text-destructive/40 hover:text-destructive" onClick={() => {
                                 const updated = localSummary.beneficiarios.filter((_, i) => i !== idx);
                                 setLocalSummary({ ...localSummary, beneficiarios: updated });
                               }}><Trash2 className="w-4 h-4" /></Button>
                             </>
                           )}
                        </div>
                      </div>
                    ))}
                  </div>
                </div>
              </div>
            ) : (
              <div className="py-20 text-center space-y-4">
                <div className="h-16 w-16 bg-muted rounded-full mx-auto flex items-center justify-center opacity-30">
                  <AlertTriangle className="w-8 h-8" />
                </div>
                <div className="space-y-1">
                  <p className="text-muted-foreground font-medium italic">No se encontró una venta asociada.</p>
                  <p className="text-[10px] text-muted-foreground">Cambia al modo "Standalone" arriba si quieres crear una comisión de ajuste directo o concepto extra.</p>
                </div>
              </div>
            )
          )}
        </div>

        <div className="p-8 pt-4 border-t border-border/50 bg-background/40 backdrop-blur-md flex justify-between items-center">
           <div className="flex items-center gap-2 text-muted-foreground/40 font-black text-[9px] uppercase tracking-widest px-2">
             <ShieldCheck className="w-3.5 h-3.5" /> Auditoría Integrada Sarritel
           </div>
           <div className="flex gap-3">
             <Button variant="outline" onClick={onClose} className="h-14 px-8 rounded-2xl font-black transition-all hover:bg-muted/50">Cancelar</Button>
             <Button 
                onClick={handleSave} 
                disabled={saving || (isExtraMode ? (!manualData.empleadoId || !manualData.montoComision || !manualData.periodo) : (stats.isOverBudget || !localSummary))} 
                className={cn(
                  "h-14 px-12 rounded-2xl font-black uppercase shadow-2xl transition-all min-w-[200px]",
                  !isExtraMode && stats.isOverBudget ? "bg-destructive/50" : "bg-primary text-primary-foreground shadow-primary/20 scale-105 active:scale-95"
                )}
              >
                {saving ? "Procesando..." : manualData.id ? "Actualizar" : "Guardar Comisión"}
              </Button>
           </div>
        </div>
      </DialogContent>
    </Dialog>
  );
}
