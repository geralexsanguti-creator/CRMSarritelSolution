import { useState, useEffect, useMemo } from 'react';
import { 
  Building2, Plus, ArrowRight, Check, Coins, Layers, Trash, 
  Info, AlertTriangle, ShieldCheck, ChevronDown, Percent
} from 'lucide-react';
import { RolesService } from '@/services/api/roles.service';
import { equiposService } from '@/services/api/equipos.service';
import type { Rol, Equipo } from '@/types';
import type { TipoVenta, ReglaComision, ReglaComisionCreateDto, ReparticionComision, ReglaComisionTier } from '@/types/proveedor.types';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { SelectGroup, SelectLabel } from '@/components/ui/select';
import { Badge } from '@/components/ui/badge';
import { Separator } from '@/components/ui/separator';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Accordion, AccordionContent, AccordionItem, AccordionTrigger } from '@/components/ui/accordion';
import { DialogContent, DialogHeader, DialogTitle, DialogFooter, DialogDescription } from '@/components/ui/dialog';
import { Switch } from '@/components/ui/switch';
import { cn } from '@/lib/utils';
import { toast } from 'sonner';

interface ReglaComisionWizardProps {
  regla?: ReglaComision | null;
  proveedorId: number;
  tiposVenta: TipoVenta[];
  onClose: () => void;
  onSave: (data: ReglaComisionCreateDto) => Promise<void>;
}

export function ReglaComisionWizard({ regla, proveedorId, tiposVenta, onClose, onSave }: ReglaComisionWizardProps) {
  const [roles, setRoles] = useState<Rol[]>([]);
  const [equipos, setEquipos] = useState<Equipo[]>([]);
  const [activeSection, setActiveSection] = useState("identidad");
  
  const [form, setForm] = useState<ReglaComisionCreateDto>({
    id: regla?.id,
    nombre: regla?.nombre ?? '',
    descripcion: regla?.descripcion ?? '',
    variable: regla?.variable ?? 'monto_total',
    operador: regla?.operador ?? 'between',
    valorMin: regla?.valorMin ?? 0,
    valorMax: regla?.valorMax,
    tipoRemuneracionGross: regla?.tipoRemuneracionGross ?? 'fijo',
    valorRemuneracionGross: regla?.valorRemuneracionGross ?? 0,
    valorVenta: regla?.valorVenta ?? 0,
    reparticionesBase: regla?.reparticionesBase ?? [],
    tiers: regla?.tiers ?? [],
    proveedorId,
    tipoVentaId: regla?.tipoVentaId,
    activa: regla?.activa ?? true,
    prioridad: regla?.prioridad ?? 1,
  });

  useEffect(() => {
    if (regla) {
      setForm({
        id: regla.id,
        nombre: regla.nombre,
        descripcion: regla.descripcion ?? '',
        variable: regla.variable,
        operador: regla.operador,
        valorMin: regla.valorMin,
        valorMax: regla.valorMax,
        tipoRemuneracionGross: regla.tipoRemuneracionGross,
        valorRemuneracionGross: regla.valorRemuneracionGross,
        valorVenta: regla.valorVenta ?? 0,
        reparticionesBase: regla.reparticionesBase ?? [],
        tiers: regla.tiers ?? [],
        proveedorId,
        tipoVentaId: regla.tipoVentaId,
        activa: regla.activa,
        prioridad: regla.prioridad,
      });
    } else {
      setForm({
        nombre: '',
        descripcion: '',
        variable: 'monto_total',
        operador: 'between',
        valorMin: 0,
        tipoRemuneracionGross: 'fijo',
        valorRemuneracionGross: 0,
        valorVenta: 0,
        reparticionesBase: [],
        tiers: [],
        proveedorId,
        activa: true,
        prioridad: 1,
      });
    }
  }, [regla, proveedorId]);

  const [saving, setSaving] = useState(false);

  useEffect(() => {
    RolesService.getAll().then(setRoles).catch(() => toast.error('Error al cargar roles'));
    equiposService.getAll().then(setEquipos).catch(() => toast.error('Error al cargar equipos'));
  }, []);

  // Dynamic Variables Logic
  const availableVariables = useMemo(() => {
    const vars = [{ value: 'total_ventas', label: 'Número de Ventas (Volumen)' }];
    if (form.tipoVentaId) {
      const tv = tiposVenta.find(x => x.id === form.tipoVentaId);
      if (tv?.esquemaVariablesJson) {
        try {
          const schema = JSON.parse(tv.esquemaVariablesJson);
          if (Array.isArray(schema)) {
            schema.forEach((v: any) => {
              // Only numeric variables can be used for measurement rules
              if (v.tipo === 'numero') {
                vars.push({ value: v.nombre, label: v.etiqueta });
              }
            });
          }
        } catch (e) {
          console.error("Error parsing schema:", e);
        }
      }
    }
    return vars;
  }, [form.tipoVentaId, tiposVenta]);

  // Reset variable if it's no longer available for the selected Tipo de Venta
  useEffect(() => {
    if (!availableVariables.find(v => v.value === form.variable)) {
      setForm(f => ({ ...f, variable: 'total_ventas' }));
    }
  }, [availableVariables, form.variable]);

  // Memorable Margins
  const marginPreview = useMemo(() => {
    if (form.tipoRemuneracionGross !== 'porcentaje') return null;
    const totalSplits = form.reparticionesBase.reduce((sum, r) => sum + (r.tipoCalculo === 'porcentaje' ? (r.valor || 0) : 0), 0);
    const margin = form.valorRemuneracionGross - totalSplits;
    return {
      totalSplits,
      margin,
      isNegative: margin < 0,
      isValid: totalSplits <= form.valorRemuneracionGross
    };
  }, [form.valorRemuneracionGross, form.reparticionesBase, form.tipoRemuneracionGross]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    // Validaciones finales
    if (marginPreview?.isNegative) {
      toast.error('No se puede guardar: Las reparticiones superan el ingreso bruto.');
      return;
    }

    setSaving(true);
    try {
      await onSave(form);
    } catch (err) {
      toast.error('Error al guardar la regla');
    } finally {
      setSaving(false);
    }
  };

  // Helper actions
  const addSplit = (tierIndex?: number) => {
    const newSplit: ReparticionComision = { 
      id: -Math.floor(Math.random() * 1000000), // Stable key for UI
      rolId: roles[0]?.id || undefined, 
      tipoCalculo: 'porcentaje', 
      valor: 0 
    };
    if (tierIndex !== undefined) {
      const newTiers = [...form.tiers];
      newTiers[tierIndex].reparticiones = [...newTiers[tierIndex].reparticiones, newSplit];
      setForm({ ...form, tiers: newTiers });
    } else {
      setForm({ ...form, reparticionesBase: [...form.reparticionesBase, newSplit] });
    }
  };

  const removeSplit = (splitIndex: number, tierIndex?: number) => {
    if (tierIndex !== undefined) {
      const newTiers = [...form.tiers];
      newTiers[tierIndex].reparticiones = newTiers[tierIndex].reparticiones.filter((_, i) => i !== splitIndex);
      setForm({ ...form, tiers: newTiers });
    } else {
      setForm({ ...form, reparticionesBase: form.reparticionesBase.filter((_, i) => i !== splitIndex) });
    }
  };

  const addTier = () => {
    const newTier: ReglaComisionTier = {
      id: -Math.floor(Math.random() * 1000000), // Temporary ID for new tiers
      nombre: `Tier ${form.tiers.length + 1}`,
      valorMin: 0,
      reparticiones: []
    };
    setForm({ ...form, tiers: [...form.tiers, newTier] });
  };

  const removeTier = (index: number) => {
    setForm({ ...form, tiers: form.tiers.filter((_, i) => i !== index) });
  };

  const updateTier = (index: number, data: Partial<ReglaComisionTier>) => {
    const newTiers = [...form.tiers];
    newTiers[index] = { ...newTiers[index], ...data };
    setForm({ ...form, tiers: newTiers });
  };

  // Enforce at least one tier ONLY on create
  useEffect(() => {
    if (!regla && form.tiers.length === 0) {
      addTier();
    }
  }, []);

  return (
    <DialogContent className="glass border-white/10 max-w-3xl max-h-[92vh] overflow-hidden flex flex-col p-0">
      <DialogHeader className="p-6 pb-2">
        <DialogTitle className="text-xl font-bold flex items-center gap-3">
          <div className="p-2 rounded-lg bg-primary/10 text-primary">
            <Coins className="w-6 h-6" />
          </div>
          <div className="flex flex-col">
            <span>{regla ? 'Rediseñar Regla Avanzada' : 'Nueva Regla de Comisión'}</span>
            <p className="text-xs font-normal text-muted-foreground mt-0.5">Define cómo se distribuyen los ingresos por venta y escalados.</p>
          </div>
        </DialogTitle>
        <DialogDescription className="sr-only">Asistente para la configuración de reglas de comisión, tiers y reparticiones</DialogDescription>
      </DialogHeader>

      <form onSubmit={handleSubmit} className="flex-1 overflow-y-auto px-6 py-2 space-y-4">
        <Accordion type="single" collapsible value={activeSection} onValueChange={setActiveSection} className="space-y-4 border-none">
          
          {/* SECCIÓN 1: IDENTIDAD */}
          <AccordionItem value="identidad" className="border rounded-xl bg-card/40 border-border/40 overflow-hidden px-4">
            <AccordionTrigger className="hover:no-underline py-4">
              <div className="flex items-center gap-3 text-left">
                <div className="h-8 w-8 rounded-full bg-blue-500/10 text-blue-500 flex items-center justify-center">
                  <Info className="w-4 h-4" />
                </div>
                <div>
                  <h4 className="text-sm font-bold uppercase tracking-wider">Sección 1: Identidad</h4>
                  <p className="text-[10px] text-muted-foreground">Nombre, contexto y variable de medición</p>
                </div>
              </div>
            </AccordionTrigger>
            <AccordionContent className="pb-6 pt-2 space-y-4">
              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div className="space-y-2 col-span-2">
                  <Label className="text-xs font-bold text-muted-foreground">Nombre de la Regla</Label>
                  <Input required value={form.nombre} onChange={e => setForm({ ...form, nombre: e.target.value })}
                    placeholder="Ej: Fibra + Móvil Residencial" className="h-10 bg-background/50 border-white/5" />
                </div>
                
                <div className="space-y-2">
                  <Label className="text-xs font-bold text-muted-foreground">Tipo de Venta (Obligatorio)</Label>
                  <Select value={form.tipoVentaId ? String(form.tipoVentaId) : ''}
                    onValueChange={v => setForm({ ...form, tipoVentaId: Number(v) })}>
                    <SelectTrigger className="h-10 bg-background/50">
                      <SelectValue placeholder="Seleccionar tipo..." />
                    </SelectTrigger>
                    <SelectContent className="glass">
                      {tiposVenta.map(tv => <SelectItem key={tv.id} value={String(tv.id)}>{tv.nombre}</SelectItem>)}
                    </SelectContent>
                  </Select>
                </div>

                <div className="space-y-2">
                  <Label className="text-xs font-bold text-muted-foreground">Variable de Medición</Label>
                  <Select value={form.variable} onValueChange={v => setForm({ ...form, variable: v })}>
                    <SelectTrigger className="h-10 bg-background/50">
                      <SelectValue />
                    </SelectTrigger>
                    <SelectContent className="glass">
                      {availableVariables.map(v => (
                        <SelectItem key={v.value} value={v.value}>{v.label}</SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                </div>
              </div>
            </AccordionContent>
          </AccordionItem>

          {/* SECCIÓN 2: INGRESO BRUTO */}
          <AccordionItem value="gross" className="border rounded-xl bg-card/40 border-border/40 overflow-hidden px-4">
            <AccordionTrigger className="hover:no-underline py-4">
              <div className="flex items-center gap-3 text-left">
                <div className="h-8 w-8 rounded-full bg-emerald-500/10 text-emerald-500 flex items-center justify-center">
                  <Building2 className="w-4 h-4" />
                </div>
                <div className="flex-1">
                  <h4 className="text-sm font-bold uppercase tracking-wider">Sección 2: Ingreso Bruto</h4>
                  <p className="text-[10px] text-muted-foreground">Lo que el proveedor paga a la empresa</p>
                </div>
                {marginPreview && (
                  <Badge variant={marginPreview.isNegative ? 'destructive' : 'default'} className="mr-4 h-5 text-[10px]">
                    Margen: {marginPreview.margin.toFixed(1)}%
                  </Badge>
                )}
              </div>
            </AccordionTrigger>
            <AccordionContent className="pb-6 pt-2 space-y-4">
              <div className="p-4 rounded-lg bg-emerald-500/5 border border-emerald-500/10 flex flex-col md:flex-row gap-4 items-end">
                <div className="flex-1 space-y-2">
                  <Label className="text-xs text-muted-foreground uppercase tracking-widest text-[10px]">Tipo Remuneración Base</Label>
                  <Select value={form.tipoRemuneracionGross} onValueChange={v => setForm({ ...form, tipoRemuneracionGross: v as any })}>
                    <SelectTrigger className="h-10 bg-background/50">
                      <SelectValue />
                    </SelectTrigger>
                    <SelectContent className="glass">
                      <SelectItem value="fijo">Importe Fijo (€/unidad)</SelectItem>
                      <SelectItem value="porcentaje">Porcentaje de Venta (%)</SelectItem>
                    </SelectContent>
                  </Select>
                </div>
                <div className="w-full md:w-48 space-y-2">
                  <Label className="text-xs text-muted-foreground uppercase tracking-widest text-[10px]">Valor Total (Cap)</Label>
                  <div className="relative">
                    <Input type="number" step="0.01" value={form.valorRemuneracionGross} 
                      onChange={e => setForm({ ...form, valorRemuneracionGross: Number(e.target.value) })}
                      className="h-10 pl-8 font-bold text-base" />
                    <div className="absolute left-3 top-1/2 -translate-y-1/2 text-muted-foreground">
                      {form.tipoRemuneracionGross === 'fijo' ? '€' : '%'}
                    </div>
                  </div>
                </div>
              </div>
              <p className="text-[10px] text-muted-foreground">
                * Este valor representa el máximo total disponible para repartir en esta regla.
              </p>
            </AccordionContent>
          </AccordionItem>

          {/* SECCIÓN 3: REPARTICIÓN BASE */}
          <AccordionItem value="reparticion" className="border rounded-xl bg-card/40 border-border/40 overflow-hidden px-4">
            <AccordionTrigger className="hover:no-underline py-4">
              <div className="flex items-center gap-3 text-left">
                <div className="h-8 w-8 rounded-full bg-amber-500/10 text-amber-500 flex items-center justify-center">
                  <Percent className="w-4 h-4" />
                </div>
                <div className="flex-1">
                  <h4 className="text-sm font-bold uppercase tracking-wider">Sección 3: Repartición Base</h4>
                  <p className="text-[10px] text-muted-foreground">Splits por defecto si no se alcanza ningún tier</p>
                </div>
              </div>
            </AccordionTrigger>
            <AccordionContent className="pb-6 pt-2 space-y-4">
              <div className="space-y-3">
                <div className="flex items-center justify-between">
                  <Label className="text-[10px] font-black uppercase text-muted-foreground tracking-widest">Configuración de Splits</Label>
                  <Button type="button" size="sm" variant="outline" onClick={() => addSplit()} className="h-7 text-[10px] gap-1">
                    <Plus className="w-3 h-3" /> Añadir Rol
                  </Button>
                </div>
                <div className="space-y-2">
                  {form.reparticionesBase.map((rep, idx) => (
                    <div key={rep.id || `base-${idx}`} className="flex items-center gap-2 group bg-background/30 p-2 rounded-lg border border-white/5">
                      <Select value={rep.equipoId ? `equipo_${rep.equipoId}` : `rol_${rep.rolId}`} onValueChange={v => {
                        const newSplits = [...form.reparticionesBase];
                        if (v.startsWith('equipo_')) {
                          newSplits[idx].equipoId = Number(v.replace('equipo_', ''));
                          newSplits[idx].rolId = undefined;
                        } else {
                          newSplits[idx].rolId = Number(v.replace('rol_', ''));
                          newSplits[idx].equipoId = undefined;
                        }
                        setForm({ ...form, reparticionesBase: newSplits });
                      }}>
                        <SelectTrigger className="h-9 text-xs flex-1">
                          <SelectValue placeholder="Beneficiario..." />
                        </SelectTrigger>
                        <SelectContent className="glass">
                          <SelectGroup>
                            <SelectLabel className="text-[10px] uppercase font-bold text-muted-foreground">Roles</SelectLabel>
                            {roles.map(r => <SelectItem key={`rol_${r.id}`} value={`rol_${r.id}`}>{r.nombre}</SelectItem>)}
                          </SelectGroup>
                          {equipos.length > 0 && (
                            <SelectGroup>
                              <SelectLabel className="text-[10px] uppercase font-bold text-muted-foreground">Equipos</SelectLabel>
                              {equipos.map(e => <SelectItem key={`equipo_${e.id}`} value={`equipo_${e.id}`}>{e.nombre}</SelectItem>)}
                            </SelectGroup>
                          )}
                        </SelectContent>
                      </Select>
                      <Select value={rep.tipoCalculo} onValueChange={v => {
                        const newSplits = [...form.reparticionesBase];
                        newSplits[idx].tipoCalculo = v as any;
                        setForm({ ...form, reparticionesBase: newSplits });
                      }}>
                        <SelectTrigger className="h-9 text-xs w-20">
                          <SelectValue />
                        </SelectTrigger>
                        <SelectContent className="glass">
                          <SelectItem value="fijo">€ Fijo</SelectItem>
                          <SelectItem value="porcentaje">% Bruto</SelectItem>
                        </SelectContent>
                      </Select>
                      <Input type="number" step="0.01" value={rep.valor} className="h-9 w-20 text-center font-bold"
                        onChange={e => {
                          const newSplits = [...form.reparticionesBase];
                          newSplits[idx].valor = Number(e.target.value);
                          setForm({ ...form, reparticionesBase: newSplits });
                        }} />
                      <Button type="button" variant="ghost" size="icon" className="h-8 w-8 text-destructive"
                        onClick={() => removeSplit(idx)}>
                        <Trash className="w-4 h-4" />
                      </Button>
                    </div>
                  ))}
                  {form.reparticionesBase.length === 0 && (
                    <div className="text-center py-4 rounded-lg border border-dashed border-border/40 bg-muted/5">
                      <p className="text-[10px] text-muted-foreground italic">No hay reparticiones base definidas.</p>
                    </div>
                  )}
                </div>
              </div>
            </AccordionContent>
          </AccordionItem>


          {/* SECCIÓN 4: ESCALADOS POR VOLUMEN */}
          <AccordionItem value="tiers" className="border rounded-xl bg-card/40 border-border/40 overflow-hidden px-4">
            <AccordionTrigger className="hover:no-underline py-4">
              <div className="flex items-center gap-3 text-left">
                <div className="h-8 w-8 rounded-full bg-violet-500/10 text-violet-500 flex items-center justify-center">
                  <Layers className="w-4 h-4" />
                </div>
                <div className="flex-1">
                  <h4 className="text-sm font-bold uppercase tracking-wider">Sección 4: Escalados (Tiers)</h4>
                  <p className="text-[10px] text-muted-foreground">Incentivos por mayor volumen conseguido</p>
                </div>
                <Badge variant="secondary" className="mr-4">
                  {form.tiers.length} Tiers
                </Badge>
              </div>
            </AccordionTrigger>
            <AccordionContent className="pb-6 pt-2 space-y-4">
              <div className="space-y-4">
                {form.tiers.map((tier, tIdx) => {
                  const key = tier.id || `tier-${tIdx}`;
                  const tierGross = tier.valorRemuneracionGross ?? form.valorRemuneracionGross;
                  const tierSplitsTotal = tier.reparticiones.reduce((sum, r) => sum + (r.valor || 0), 0);
                  const isSplitsPercent = (tier.tipoRemuneracionGross ?? form.tipoRemuneracionGross) === 'porcentaje';
                  const remainder = tierGross - tierSplitsTotal;
                  const isOverLimit = remainder < 0;

                  return (
                    <div key={key} className={cn(
                      "rounded-xl border bg-muted/30 overflow-hidden relative group transition-all",
                      isOverLimit ? "border-destructive/50" : "border-border/60"
                    )}>
                      <div className="bg-muted/50 px-4 py-3 border-b border-border/60 flex items-center justify-between">
                        <div className="flex items-center gap-4">
                          <div className="h-6 w-6 rounded-full bg-primary/20 text-primary flex items-center justify-center text-[10px] font-bold">
                            # {tIdx + 1}
                          </div>
                          <Input value={tier.nombre} onChange={e => updateTier(tIdx, { nombre: e.target.value })}
                            className="h-7 w-48 bg-transparent border-none font-bold text-xs p-0 focus-visible:ring-0" />
                        </div>
                        {form.tiers.length > 1 && (
                          <Button type="button" variant="ghost" size="icon" className="h-7 w-7 text-destructive"
                            onClick={() => removeTier(tIdx)}>
                            <Trash className="w-3.5 h-3.5" />
                          </Button>
                        )}
                      </div>

                      <div className="p-4 space-y-4">
                        {/* Rangos */}
                        <div className="grid grid-cols-2 gap-4">
                          <div className="space-y-1.5">
                            <Label className="text-[10px] text-muted-foreground uppercase font-bold tracking-tight">Rango Min ({form.variable})</Label>
                            <Input type="number" value={tier.valorMin} onChange={e => updateTier(tIdx, { valorMin: Number(e.target.value) })}
                              className="h-9 bg-background/50 border-white/5 font-medium" />
                          </div>
                          <div className="space-y-1.5">
                            <Label className="text-[10px] text-muted-foreground uppercase font-bold tracking-tight">Rango Max ({form.variable})</Label>
                            <Input type="number" value={tier.valorMax || ''} placeholder="∞ Infinito"
                              onChange={e => updateTier(tIdx, { valorMax: e.target.value ? Number(e.target.value) : undefined })}
                              className="h-9 bg-background/50 border-white/5 font-medium" />
                          </div>
                        </div>

                        {/* Override Gross Toggle */}
                        <div className="flex items-center justify-between p-3 rounded-lg bg-background/20 border border-white/5">
                          <div className="flex items-center gap-2">
                            <div className={cn("p-1.5 rounded-full", tier.valorRemuneracionGross !== undefined ? "bg-primary/10 text-primary" : "bg-muted text-muted-foreground")}>
                              <ShieldCheck className="w-4 h-4" />
                            </div>
                            <div>
                              <p className="text-xs font-bold">Diferenciar Ingreso para este Tier</p>
                              <p className="text-[10px] text-muted-foreground">Opcional: Si este escalado genera un ingreso mayor/menor a la empresa.</p>
                            </div>
                          </div>
                          <Switch 
                            checked={tier.valorRemuneracionGross != null}
                            onCheckedChange={(checked) => {
                              if (checked) {
                                updateTier(tIdx, { 
                                  tipoRemuneracionGross: form.tipoRemuneracionGross as 'fijo' | 'porcentaje',
                                  valorRemuneracionGross: form.valorRemuneracionGross
                                });
                              } else {
                                updateTier(tIdx, { 
                                  tipoRemuneracionGross: undefined,
                                  valorRemuneracionGross: undefined
                                });
                              }
                            }}
                          />
                        </div>

                        {tier.valorRemuneracionGross == null && (
                          <div className="px-3 py-1.5 rounded-md bg-muted/20 border border-border/40 flex items-center gap-2">
                            <Info className="w-3 h-3 text-muted-foreground" />
                            <span className="text-[10px] text-muted-foreground italic">
                              Heredando Ingreso Base: {form.tipoRemuneracionGross === 'fijo' ? '€' : ''}{form.valorRemuneracionGross}{form.tipoRemuneracionGross === 'porcentaje' ? '%' : ''}
                            </span>
                          </div>
                        )}

                        {tier.valorRemuneracionGross != null && (
                          <div className="grid grid-cols-2 gap-4 p-3 rounded-lg bg-primary/5 border border-primary/10 animate-in slide-in-from-top-2 duration-300">
                             <div className="space-y-2">
                               <Label className="text-[10px] text-muted-foreground uppercase">Tipo Ingreso Tier</Label>
                               <Select value={tier.tipoRemuneracionGross} onValueChange={v => updateTier(tIdx, { tipoRemuneracionGross: v as any })}>
                                 <SelectTrigger className="h-8 bg-background/40">
                                   <SelectValue />
                                 </SelectTrigger>
                                 <SelectContent className="glass">
                                   <SelectItem value="fijo">Fijo (€)</SelectItem>
                                   <SelectItem value="porcentaje">Porcentual (%)</SelectItem>
                                 </SelectContent>
                               </Select>
                             </div>
                             <div className="space-y-2">
                               <Label className="text-[10px] text-muted-foreground uppercase">Monto Ingreso Tier</Label>
                               <Input type="number" step="0.01" value={tier.valorRemuneracionGross} 
                                 onChange={e => updateTier(tIdx, { valorRemuneracionGross: Number(e.target.value) })}
                                 className="h-8 bg-background/40 font-bold" />
                             </div>
                          </div>
                        )}

                        {/* Reparticiones específicas del Tier */}
                        <div className="pt-2 border-t border-border/40 space-y-3">
                          <div className="flex items-center justify-between">
                            <span className="text-[10px] font-bold text-primary uppercase tracking-widest">
                              Reparto de Comisiones (Splits)
                            </span>
                            <Button type="button" size="sm" variant="ghost" onClick={() => addSplit(tIdx)} className="h-6 text-[9px] gap-1 hover:bg-primary/10 hover:text-primary">
                              <Plus className="w-3 h-3" /> Añadir Rol
                            </Button>
                          </div>
                          
                          <div className="space-y-2">
                            {tier.reparticiones.map((rep, rIdx) => (
                              <div key={rep.id || `rep-${rIdx}`} className="flex items-center gap-2 bg-background/40 p-1.5 rounded-md">
                                 <Select value={rep.equipoId ? `equipo_${rep.equipoId}` : `rol_${rep.rolId}`} onValueChange={v => {
                                   const newTiers = [...form.tiers];
                                   if (v.startsWith('equipo_')) {
                                     newTiers[tIdx].reparticiones[rIdx].equipoId = Number(v.replace('equipo_', ''));
                                     newTiers[tIdx].reparticiones[rIdx].rolId = undefined;
                                   } else {
                                     newTiers[tIdx].reparticiones[rIdx].rolId = Number(v.replace('rol_', ''));
                                     newTiers[tIdx].reparticiones[rIdx].equipoId = undefined;
                                   }
                                   setForm({ ...form, tiers: newTiers });
                                 }}>
                                   <SelectTrigger className="h-8 text-[10px] flex-1 font-medium">
                                     <SelectValue placeholder="Beneficiario..." />
                                   </SelectTrigger>
                                   <SelectContent className="glass">
                                     <SelectGroup>
                                       <SelectLabel className="text-[10px] uppercase font-bold text-muted-foreground bg-muted/20">Por Rol</SelectLabel>
                                       {roles.map(r => <SelectItem key={`rol_${r.id}`} value={`rol_${r.id}`}>{r.nombre}</SelectItem>)}
                                     </SelectGroup>
                                     {equipos.length > 0 && (
                                       <SelectGroup>
                                         <SelectLabel className="text-[10px] uppercase font-bold text-muted-foreground bg-muted/20">Por Equipo (Líder)</SelectLabel>
                                         {equipos.map(e => <SelectItem key={`equipo_${e.id}`} value={`equipo_${e.id}`}>{e.nombre}</SelectItem>)}
                                       </SelectGroup>
                                     )}
                                   </SelectContent>
                                 </Select>
                                 <div className="flex items-center gap-1">
                                   <Select value={rep.tipoCalculo} onValueChange={v => {
                                      const newTiers = [...form.tiers];
                                      newTiers[tIdx].reparticiones[rIdx].tipoCalculo = v as any;
                                      setForm({ ...form, tiers: newTiers });
                                   }}>
                                      <SelectTrigger className="h-8 w-14 text-[9px]">
                                        <SelectValue />
                                      </SelectTrigger>
                                      <SelectContent className="glass">
                                        <SelectItem value="fijo">€</SelectItem>
                                        <SelectItem value="porcentaje">%</SelectItem>
                                      </SelectContent>
                                   </Select>
                                   <Input type="number" step="0.01" value={rep.valor} 
                                     onChange={e => {
                                       const newTiers = [...form.tiers];
                                       newTiers[tIdx].reparticiones[rIdx].valor = Number(e.target.value);
                                       setForm({ ...form, tiers: newTiers });
                                     }} 
                                     className="h-8 w-16 text-center text-xs font-bold" />
                                   <Button type="button" variant="ghost" size="icon" className="h-7 w-7 text-destructive"
                                     onClick={() => removeSplit(rIdx, tIdx)}>
                                     <Trash className="w-3.5 h-3.5" />
                                   </Button>
                                 </div>
                              </div>
                            ))}
                            
                            {/* INTELLIGENCE: REMAINDER INDICATOR */}
                            <div className={cn(
                              "mt-4 p-3 rounded-xl border flex flex-col gap-2 transition-all shadow-inner",
                              isOverLimit ? "bg-destructive/5 border-destructive/20" : "bg-primary/5 border-primary/10"
                            )}>
                              <div className="flex justify-between items-center">
                                <span className="text-[10px] font-bold text-muted-foreground uppercase">Cálculo de Beneficio Empresa</span>
                                <Badge variant={isOverLimit ? "destructive" : "outline"} className="h-5 text-[9px]">
                                  {isOverLimit ? "DÉFICIT" : "SOBRANTE OK"}
                                </Badge>
                              </div>
                              <div className="flex items-end justify-between">
                                <div>
                                  <p className="text-[9px] text-muted-foreground uppercase opacity-70">Suma de Repartos</p>
                                  <p className="text-sm font-black">{tierSplitsTotal.toFixed(2)}{isSplitsPercent ? '%' : '€'}</p>
                                </div>
                                <div className="text-center opacity-30 px-2 pb-1">
                                  <ArrowRight className="w-4 h-4" />
                                </div>
                                <div className="text-right">
                                  <p className="text-[9px] text-muted-foreground uppercase opacity-70">Beneficio Sistema (Admin)</p>
                                  <p className={cn(
                                    "text-lg font-black",
                                    isOverLimit ? "text-destructive" : "text-primary"
                                  )}>
                                    {remainder.toFixed(2)}{isSplitsPercent ? '%' : '€'}
                                  </p>
                                </div>
                              </div>
                              {isOverLimit && (
                                <p className="text-[9px] text-destructive flex items-center gap-1 font-bold mt-1">
                                  <AlertTriangle className="w-3 h-3" /> Los repartos no pueden superar el ingreso del tier.
                                </p>
                              )}
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  );
                })}
                
                <Button type="button" variant="outline" onClick={addTier} 
                  className="w-full h-12 border-dashed bg-primary/5 hover:bg-primary/10 border-primary/20 text-primary font-bold gap-2">
                  <div className="h-6 w-6 rounded-md bg-primary text-white flex items-center justify-center">
                    <Plus className="w-4 h-4" />
                  </div>
                  Añadir Otro Escalado (Tier {form.tiers.length + 1})
                </Button>
              </div>
            </AccordionContent>
          </AccordionItem>
          
        </Accordion>
      </form>

      <DialogFooter className="p-6 bg-muted/40 border-t border-border/40 gap-3">
        <Button type="button" variant="ghost" onClick={onClose} disabled={saving} className="px-6">
          Cancelar
        </Button>
        <Button 
          onClick={handleSubmit} 
          disabled={saving || (marginPreview?.isNegative ?? false)} 
          className={cn(
            "h-11 px-8 font-bold gap-2 shadow-lg transition-all",
            marginPreview?.isNegative ? "bg-muted cursor-not-allowed" : "bg-primary hover:scale-[1.02]"
          )}
        >
          {saving ? (
            <span className="animate-spin w-5 h-5 border-2 border-white/30 border-t-white rounded-full" />
          ) : (
            <>
              {regla ? <Check className="w-5 h-5" /> : <Plus className="w-5 h-5" />}
              {regla ? 'Guardar Cambios' : 'Crear Regla de Comisión'}
            </>
          )}
        </Button>
      </DialogFooter>
    </DialogContent>
  );
}
