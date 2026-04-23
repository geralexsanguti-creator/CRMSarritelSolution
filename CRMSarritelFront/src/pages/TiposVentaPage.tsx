import { useState, useEffect, useMemo } from 'react';
import { motion, AnimatePresence, Reorder } from 'framer-motion';
import { 
  Network, Plus, Edit, Trash2, Tag, Info, Check, 
  Settings2, Layers, AlertCircle, X, ChevronDown, ChevronUp
} from 'lucide-react';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Badge } from '@/components/ui/badge';
import { Separator } from '@/components/ui/separator';
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from '@/components/ui/dialog';
import { KpiCard } from '@/components/shared/KpiCard';
import { UnifiedSearchBar } from '@/components/shared/UnifiedSearchBar';
import { cn } from '@/lib/utils';
import { glass, card, flexCenter } from '@/lib/styles';
import { useAuth } from '@/hooks/useAuth';
import { toast } from 'sonner';
import { TiposVentaService } from '@/services/api/proveedores.service';
import type { TipoVenta, TipoVentaCreateDto, VariableDefinicion, EstadoVentaConfig } from '@/types/proveedor.types';
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { GripVertical } from "lucide-react";

export const PIPELINE_COLORS = [
  { id: 'slate', name: 'Gris', bgClass: 'bg-slate-500', badge: 'bg-slate-500/20 text-slate-500 border-slate-500/30' },
  { id: 'blue', name: 'Azul', bgClass: 'bg-blue-500', badge: 'bg-blue-500/20 text-blue-500 border-blue-500/30' },
  { id: 'indigo', name: 'Índigo', bgClass: 'bg-indigo-500', badge: 'bg-indigo-500/20 text-indigo-500 border-indigo-500/30' },
  { id: 'purple', name: 'Morado', bgClass: 'bg-purple-500', badge: 'bg-purple-500/20 text-purple-500 border-purple-500/30' },
  { id: 'pink', name: 'Rosa', bgClass: 'bg-pink-500', badge: 'bg-pink-500/20 text-pink-500 border-pink-500/30' },
  { id: 'red', name: 'Rojo', bgClass: 'bg-red-500', badge: 'bg-red-500/20 text-red-500 border-red-500/30' },
  { id: 'orange', name: 'Naranja', bgClass: 'bg-orange-500', badge: 'bg-orange-500/20 text-orange-500 border-orange-500/30' },
  { id: 'amber', name: 'Ámbar', bgClass: 'bg-amber-500', badge: 'bg-amber-500/20 text-amber-600 border-amber-500/30' },
  { id: 'emerald', name: 'Verde', bgClass: 'bg-emerald-500', badge: 'bg-emerald-500/20 text-emerald-500 border-emerald-500/30' },
  { id: 'teal', name: 'Turquesa', bgClass: 'bg-teal-500', badge: 'bg-teal-500/20 text-teal-500 border-teal-500/30' },
  { id: 'cyan', name: 'Cyan', bgClass: 'bg-cyan-500', badge: 'bg-cyan-500/20 text-cyan-500 border-cyan-500/30' },
  { id: 'primary', name: 'Principal', bgClass: 'bg-primary', badge: 'bg-primary/20 text-primary border-primary/30' },
];

// ─── Variable Builder Component (Inner) ───────────────────────────────────────
function VariableBuilder({ 
  variables, 
  onChange 
}: { 
  variables: VariableDefinicion[], 
  onChange: (vars: VariableDefinicion[]) => void 
}) {
  const addVariable = () => {
    onChange([...variables, { nombre: '', etiqueta: '', tipo: 'numero', requerido: false }]);
  };

  const removeVariable = (index: number) => {
    onChange(variables.filter((_, i) => i !== index));
  };

  const updateVariable = (index: number, data: Partial<VariableDefinicion>) => {
    const newVars = [...variables];
    newVars[index] = { ...newVars[index], ...data };
    onChange(newVars);
  };

  return (
    <div className="space-y-3">
      <div className="flex items-center justify-between">
        <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Variables Dinámicas</Label>
        <Button type="button" variant="outline" size="sm" onClick={addVariable} className="h-7 text-[10px] gap-1 px-2 border-primary/20 hover:bg-primary/5 text-primary">
          <Plus className="w-3 h-3" /> Añadir Variable
        </Button>
      </div>

      <div className="space-y-2 max-h-[250px] overflow-y-auto pr-2 custom-scrollbar">
        {variables.length === 0 ? (
          <div className="text-center p-6 border border-dashed border-border/40 rounded-xl bg-muted/5">
            <Info className="w-5 h-5 text-muted-foreground/30 mx-auto mb-2" />
            <p className="text-[11px] text-muted-foreground">No hay variables definidas para este tipo de venta.</p>
          </div>
        ) : (
          variables.map((v, idx) => (
            <motion.div 
              key={idx}
              initial={{ opacity: 0, x: -10 }}
              animate={{ opacity: 1, x: 0 }}
              className="p-3 rounded-lg border border-border/40 bg-background/40 space-y-3 group relative"
            >
              <div className="grid grid-cols-2 gap-2">
                <div className="space-y-1">
                  <Label className="text-[10px] text-muted-foreground uppercase">Nombre Interno (ID)</Label>
                  <Input 
                    value={v.nombre} 
                    onChange={e => updateVariable(idx, { nombre: e.target.value.toLowerCase().replace(/\s+/g, '_') })}
                    placeholder="ej: num_lineas" 
                    className="h-8 text-xs bg-background/60"
                  />
                </div>
                <div className="space-y-1">
                  <Label className="text-[10px] text-muted-foreground uppercase">Etiqueta (UI)</Label>
                  <Input 
                    value={v.etiqueta} 
                    onChange={e => updateVariable(idx, { etiqueta: e.target.value })}
                    placeholder="ej: Nº de Líneas" 
                    className="h-8 text-xs bg-background/60"
                  />
                </div>
              </div>
              <div className="flex items-center gap-3">
                <div className="flex-1">
                  <Label className="text-[10px] text-muted-foreground uppercase mb-1 block">Tipo</Label>
                  <div className="flex gap-1 flex-wrap">
                    {['numero', 'texto', 'booleano', 'fecha'].map((t) => (
                      <button
                        key={t}
                        type="button"
                        onClick={() => updateVariable(idx, { tipo: t as any })}
                        className={cn(
                          "px-2 py-1 rounded text-[10px] border transition-all truncate min-w-[60px]",
                          v.tipo === t 
                            ? "bg-primary text-primary-foreground border-primary font-bold" 
                            : "bg-muted/10 border-border/40 hover:border-primary/30"
                        )}
                      >
                        {t === 'numero' ? 'Numérico' : t.charAt(0).toUpperCase() + t.slice(1)}
                      </button>
                    ))}
                  </div>
                </div>
                <div className="flex items-center gap-2 pt-4">
                   <input 
                    type="checkbox" 
                    id={`req-${idx}`}
                    checked={v.requerido} 
                    onChange={e => updateVariable(idx, { requerido: e.target.checked })}
                    className="h-3 w-3 rounded border-border text-primary"
                   />
                   <Label htmlFor={`req-${idx}`} className="text-[10px] cursor-pointer">Req.</Label>
                </div>
              </div>
              <Button 
                type="button" 
                variant="ghost" 
                size="icon" 
                onClick={() => removeVariable(idx)}
                className="absolute -top-1 -right-1 h-5 w-5 rounded-full bg-destructive/10 text-destructive opacity-0 group-hover:opacity-100 transition-opacity"
              >
                <X className="w-3 h-3" />
              </Button>
            </motion.div>
          ))
        )}
      </div>
    </div>
  );
}

// ─── Estado Venta Builder Component (Inner) ──────────────────────────────────
function EstadoVentaBuilder({
  estados,
  onChange
}: {
  estados: EstadoVentaConfig[],
  onChange: (estados: EstadoVentaConfig[]) => void
}) {
  const addEstado = () => {
    onChange([...estados, {
      _uid: Math.random().toString(36).substr(2, 9),
      codigo: 'NUEVO_ESTADO', nombre: 'Nuevo Estado', color: PIPELINE_COLORS[0].badge, icono: '📋', orden: (estados.length + 1) * 10, esInicial: false, esFinal: false, esGanada: false
    } as any]);
  };

  const updateEstado = (idx: number, data: Partial<EstadoVentaConfig>) => {
    const estadoAActualizar = sortedEstados[idx] as any;
    const realIdx = estados.findIndex((e: any) => e._uid === estadoAActualizar._uid);
    
    const newEstados = [...estados];
    if (realIdx >= 0) {
      newEstados[realIdx] = { ...newEstados[realIdx], ...data };
      onChange(newEstados);
    }
  };

  const removeEstado = (idx: number) => {
    const estadoAEliminar = sortedEstados[idx] as any;
    onChange(estados.filter((e: any) => e._uid !== estadoAEliminar._uid));
  };

  const sortedEstados = useMemo(() => [...estados].sort((a,b) => a.orden - b.orden), [estados]);

  const handleReorder = (reorderedEstados: EstadoVentaConfig[]) => {
    const newEstados = reorderedEstados.map((est, index) => ({
      ...est,
      orden: (index + 1) * 10
    }));
    onChange(newEstados);
  };

  return (
    <div className="flex flex-col h-full space-y-3 relative">
      <div className="flex items-center justify-between sticky top-0 z-10 bg-background/95 backdrop-blur pt-2 pb-3 border-b border-border/20 mb-2">
        <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Flujo de Estados (Pipeline)</Label>
        <Button type="button" variant="outline" size="sm" onClick={addEstado} className="h-7 text-[10px] gap-1 px-2 border-primary/20 hover:bg-primary/5 text-primary">
          <Plus className="w-3 h-3" /> Añadir Etapa
        </Button>
      </div>

      <div className="flex-1 space-y-3 overflow-y-auto pr-2 pb-4 custom-scrollbar">
        {estados.length === 0 ? (
          <div className="text-center p-6 border border-dashed border-border/40 rounded-xl bg-muted/5">
     <Info className="w-5 h-5 text-muted-foreground/30 mx-auto mb-2" />
            <p className="text-[11px] text-muted-foreground">No hay estados configurados. Configura al menos un inicio y fin.</p>
          </div>
        ) : (
          <Reorder.Group axis="y" values={sortedEstados} onReorder={handleReorder} className="m-0 p-0 space-y-3" style={{ listStyleType: "none" }}>
          {sortedEstados.map((estado, idx) => (
            <Reorder.Item 
              key={(estado as any)._uid} 
              value={estado}
              className="p-3 rounded-lg border border-border/40 bg-background/40 hover:bg-muted/10 transition-colors space-y-3 relative group"
            >
              <div className="flex justify-between items-center mb-1">
                <div className="flex items-center gap-2 cursor-grab active:cursor-grabbing">
                  <GripVertical className="w-4 h-4 text-muted-foreground/40" />
                  <span className="text-[10px] font-bold text-foreground bg-primary/10 text-primary px-2 rounded">Etapa {idx + 1}</span>
                </div>
                <Button type="button" variant="ghost" size="icon" onClick={() => removeEstado(idx)} className="h-6 w-6 text-destructive/70 hover:text-destructive hover:bg-destructive/10"><Trash2 className="w-3 h-3" /></Button>
              </div>
              <div className="grid grid-cols-1 md:grid-cols-3 gap-2">
                 <div className="space-y-1">
                  <Label className="text-[10px] text-muted-foreground uppercase">Nombre</Label>
                  <Input value={estado.nombre} onChange={e => updateEstado(idx, { nombre: e.target.value })} className="h-8 text-xs bg-background/60" />
                 </div>
                 <div className="space-y-1">
                  <Label className="text-[10px] text-muted-foreground uppercase">Código</Label>
                  <Input value={estado.codigo} onChange={e => updateEstado(idx, { codigo: e.target.value.toUpperCase().replace(/\s+/g, '_') })} className="h-8 text-xs bg-background/60" placeholder="NUEVO_LEAD" />
                 </div>
                 <div className="space-y-1">
                  <Label className="text-[10px] text-muted-foreground uppercase">Color en Tablero</Label>
                  <Popover>
                    <PopoverTrigger asChild>
                      <Button variant="outline" className="w-full justify-start text-left font-normal bg-background/60 h-8 text-xs border-border/50">
                         {estado.color ? (
                           <div className="flex items-center gap-2">
                             <span className={cn("w-3 h-3 rounded-full border border-background shadow-sm", PIPELINE_COLORS.find(c => c.badge === estado.color)?.bgClass || "bg-primary")} />
                             <span>{PIPELINE_COLORS.find(c => c.badge === estado.color)?.name || 'Personalizado'}</span>
                           </div>
                         ) : (
                           <span className="text-muted-foreground">Elegir color...</span>
                         )}
                      </Button>
                    </PopoverTrigger>
                    <PopoverContent className="w-[280px] p-3 glass" align="start">
                      <div className="mb-2">
                        <p className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Seleccionar color</p>
                      </div>
                      <div className="flex flex-wrap gap-2.5">
                         {PIPELINE_COLORS.map(c => (
                            <button 
                               type="button" 
                               key={c.id} 
                               onClick={() => updateEstado(idx, { color: c.badge })}
                               className={cn(
                                 "w-7 h-7 rounded-full transition-transform hover:scale-110 flex items-center justify-center border-2 border-background shadow-sm ring-1 ring-border/20",
                                 c.bgClass,
                                 estado.color === c.badge ? "ring-2 ring-primary ring-offset-1 ring-offset-background scale-110" : ""
                               )}
                               title={c.name}
                            />
                         ))}
                      </div>
                    </PopoverContent>
                  </Popover>
                 </div>
              </div>
              <div className="flex flex-wrap gap-4 pt-2 border-t border-border/30">
                 <label className="flex items-center gap-1.5 cursor-pointer">
                    <input type="checkbox" checked={estado.esInicial} onChange={e => updateEstado(idx, { esInicial: e.target.checked })} className="rounded border-border text-primary h-3.5 w-3.5" />
                    <span className="text-[10px] text-foreground font-medium">Estado Inicial</span>
                 </label>
                 <label className="flex items-center gap-1.5 cursor-pointer">
                    <input type="checkbox" checked={estado.esFinal} onChange={e => updateEstado(idx, { esFinal: e.target.checked })} className="rounded border-border text-primary h-3.5 w-3.5" />
                    <span className="text-[10px] text-foreground font-medium">Estado Final (Cierre)</span>
                 </label>
                 {estado.esFinal && (
                   <motion.label initial={{ opacity: 0, scale: 0.9 }} animate={{ opacity: 1, scale: 1 }} className="flex items-center gap-1.5 cursor-pointer bg-green-500/10 px-2 py-0.5 rounded border border-green-500/20">
                      <input type="checkbox" checked={estado.esGanada} onChange={e => updateEstado(idx, { esGanada: e.target.checked })} className="rounded border-green-500 text-green-500 h-3 w-3" />
                      <span className="text-[10px] text-green-600 dark:text-green-400 font-bold uppercase">Suma a Venta Neta? (Ganada)</span>
                   </motion.label>
                 )}
              </div>
            </Reorder.Item>
          ))}
          </Reorder.Group>
        )}
      </div>
    </div>
  );
}

// ─── Orígenes Venta Builder Component (Inner) ──────────────────────────────
function OrigenesVentaBuilder({
  origenes,
  onChange
}: {
  origenes: string[],
  onChange: (origenes: string[]) => void
}) {
  const [newOrigen, setNewOrigen] = useState('');

  const addOrigen = () => {
    const val = newOrigen.trim();
    if (!val) return;
    if (origenes.includes(val)) {
      toast.error("Este origen ya existe");
      return;
    }
    onChange([...origenes, val]);
    setNewOrigen('');
  };

  const removeOrigen = (index: number) => {
    onChange(origenes.filter((_, i) => i !== index));
  };

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Orígenes de Venta</Label>
      </div>

      <div className="flex gap-2">
        <Input 
          value={newOrigen}
          onChange={e => setNewOrigen(e.target.value)}
          placeholder="Ej: Telemarketing, Web, Feria..."
          className="h-9 bg-background/40"
          onKeyDown={e => e.key === 'Enter' && (e.preventDefault(), addOrigen())}
        />
        <Button type="button" onClick={addOrigen} size="sm" className="h-9 gap-1 font-bold">
          <Plus className="w-3 h-3" /> Añadir
        </Button>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 gap-2 max-h-[300px] overflow-y-auto pr-2 custom-scrollbar">
        {origenes.length === 0 ? (
          <div className="col-span-2 text-center p-6 border border-dashed border-border/40 rounded-xl bg-muted/5">
            <p className="text-[11px] text-muted-foreground italic">No hay orígenes definidos. Se usará "Presencial" por defecto.</p>
          </div>
        ) : (
          origenes.map((orig, idx) => (
            <motion.div 
              key={idx}
              initial={{ opacity: 0, scale: 0.95 }}
              animate={{ opacity: 1, scale: 1 }}
              className="flex items-center justify-between p-2 pl-3 rounded-lg border border-border/40 bg-background/40 group hover:border-primary/30 transition-all"
            >
              <span className="text-xs font-medium text-foreground">{orig}</span>
              <Button 
                type="button" 
                variant="ghost" 
                size="icon" 
                onClick={() => removeOrigen(idx)}
                className="h-6 w-6 text-destructive opacity-0 group-hover:opacity-100 transition-opacity"
              >
                <X className="w-3 h-3" />
              </Button>
            </motion.div>
          ))
        )}
      </div>
      
      <div className="p-3 rounded-lg bg-primary/5 border border-primary/10">
        <p className="text-[10px] text-muted-foreground leading-relaxed flex gap-2">
          <Info className="w-3 h-3 shrink-0 text-primary" />
          Define aquí los puntos de entrada para este tipo de flujo de ventas. Estos serán seleccionables en el formulario de creación y edición.
        </p>
      </div>
    </div>
  );
}

// ─── Modal Form Component ─────────────────────────────────────────────────────

function TipoVentaModal({ 
  tipo, 
  onClose, 
  onSave 
}: { 
  tipo?: TipoVenta | null; 
  onClose: () => void; 
  onSave: (data: TipoVentaCreateDto) => Promise<void> 
}) {
  const [form, setForm] = useState<TipoVentaCreateDto>({
    nombre: tipo?.nombre ?? '',
    codigo: tipo?.codigo ?? '',
    descripcion: tipo?.descripcion ?? '',
    activo: tipo?.activo ?? true,
    esquemaVariablesJson: tipo?.esquemaVariablesJson ?? '[]',
    estadosVentaJson: tipo?.estadosVentaJson ?? '[]',
    origenesJson: tipo?.origenesJson ?? '[]'
  });
  
  const [variables, setVariables] = useState<VariableDefinicion[]>([]);
  const [estados, setEstados] = useState<EstadoVentaConfig[]>([]);
  const [origenes, setOrigenes] = useState<string[]>([]);
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    if (tipo?.esquemaVariablesJson) {
      try { setVariables(JSON.parse(tipo.esquemaVariablesJson)); } catch { setVariables([]); }
    } else { setVariables([]); }

    if (tipo?.estadosVentaJson) {
      try { 
        const parsed = JSON.parse(tipo.estadosVentaJson);
        setEstados(parsed.map((e: any) => ({...e, _uid: Math.random().toString(36).substr(2, 9)})));
      } catch { setEstados([]); }
    } else {
      setEstados([
        { _uid: Math.random().toString(36).substr(2, 9), codigo: 'NUEVO', nombre: 'Nuevo/Lead', color: PIPELINE_COLORS[0].badge, icono: '📋', orden: 10, esInicial: true, esFinal: false, esGanada: false } as any,
        { _uid: Math.random().toString(36).substr(2, 9), codigo: 'CERRADA_GANADA', nombre: 'Cerrada Ganada', color: PIPELINE_COLORS[8].badge, icono: '✅', orden: 90, esInicial: false, esFinal: true, esGanada: true } as any,
        { _uid: Math.random().toString(36).substr(2, 9), codigo: 'CANCELADA', nombre: 'Cancelada / Perdida', color: PIPELINE_COLORS[5].badge, icono: '❌', orden: 99, esInicial: false, esFinal: true, esGanada: false } as any
      ]);
    }

    if (tipo?.origenesJson) {
      try { setOrigenes(JSON.parse(tipo.origenesJson)); } catch { setOrigenes(['presencial']); }
    } else {
      setOrigenes(['presencial']);
    }
  }, [tipo]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!estados.some(e => e.esInicial)) {
      toast.error("El flujo debe tener al menos un Estado Inicial");
      return;
    }
    if (!estados.some(e => e.esFinal)) {
      toast.error("El flujo debe tener al menos un Estado Final configurado");
      return;
    }

    setSaving(true);
    try {
      const updatedForm = {
        ...form,
        esquemaVariablesJson: JSON.stringify(variables),
        estadosVentaJson: JSON.stringify(estados.map((e: any) => {
          const { _uid, ...rest } = e;
          return rest;
        })),
        origenesJson: JSON.stringify(origenes)
      };
      console.log("DEBUG: Saving TipoVenta Payload:", updatedForm);
      await onSave(updatedForm);
    } finally {
      setSaving(false);
    }
  };

  return (
    <DialogContent className="glass border-white/10 max-w-2xl shadow-2xl overflow-hidden p-0 h-[85vh] sm:h-[75vh] flex flex-col">
      <DialogHeader className="p-6 pb-2 shrink-0">
        <DialogTitle className="text-xl font-bold flex items-center gap-2">
          {tipo ? <Edit className="w-5 h-5 text-primary" /> : <Plus className="w-5 h-5 text-primary" />}
          {tipo ? 'Editar Tipo de Venta' : 'Nuevo Tipo de Venta'}
        </DialogTitle>
      </DialogHeader>
      
      <form onSubmit={handleSubmit} className="flex-1 overflow-hidden flex flex-col">
        <Tabs defaultValue="general" className="flex-1 flex flex-col overflow-hidden w-full">
          <div className="px-6 mb-2">
            <TabsList className="grid w-full grid-cols-3 bg-muted/20">
              <TabsTrigger value="general">Info & Variables</TabsTrigger>
              <TabsTrigger value="pipeline">Pipeline</TabsTrigger>
              <TabsTrigger value="origenes">Orígenes</TabsTrigger>
            </TabsList>
          </div>

          <TabsContent value="general" className="flex-1 overflow-y-auto px-6 py-2 m-0 custom-scrollbar">
            <div className="space-y-6">
              <div className="grid grid-cols-2 gap-4">
                <div className="col-span-2 space-y-2">
                  <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Nombre del Tipo *</Label>
                  <Input 
                    required 
                    value={form.nombre} 
                    onChange={e => setForm(f => ({...f, nombre: e.target.value}))}
                    placeholder="Ej: Telefonía Móvil" 
                    className="bg-background/40 border-border/40 h-10" 
                  />
                </div>
                <div className="space-y-2">
                  <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Código / Ref</Label>
                  <Input 
                    value={form.codigo ?? ''} 
                    onChange={e => setForm(f => ({...f, codigo: e.target.value.toUpperCase()}))}
                    placeholder="TEL-MOB" 
                    className="bg-background/40 border-border/40 h-10" 
                  />
                </div>
                <div className="space-y-2">
                  <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Estado Activo</Label>
                  <div className="flex items-center gap-2 h-10 px-3 rounded-md border border-border/40 bg-background/40">
                    <input 
                      type="checkbox" 
                      id="activo-tipo" 
                      checked={form.activo}
                      onChange={e => setForm(f => ({...f, activo: e.target.checked}))}
                      className="h-4 w-4 rounded border-border text-primary"
                    />
                    <Label htmlFor="activo-tipo" className="text-sm cursor-pointer">Activo y Visible</Label>
                  </div>
                </div>
                <div className="col-span-2 space-y-2">
                  <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Descripción</Label>
                  <Input 
                    value={form.descripcion ?? ''} 
                    onChange={e => setForm(f => ({...f, descripcion: e.target.value}))}
                    placeholder="Breve descripción del flujo o variables..." 
                    className="bg-background/40 border-border/40 h-10" 
                  />
                </div>
              </div>
      
              <Separator className="bg-white/5" />
      
              <VariableBuilder 
                variables={variables} 
                onChange={setVariables} 
              />
            </div>
          </TabsContent>

          <TabsContent value="pipeline" className="flex-1 overflow-y-auto px-6 py-2 m-0 custom-scrollbar">
             <EstadoVentaBuilder estados={estados} onChange={setEstados} />
          </TabsContent>

          <TabsContent value="origenes" className="flex-1 overflow-y-auto px-6 py-2 m-0 custom-scrollbar">
             <OrigenesVentaBuilder origenes={origenes} onChange={setOrigenes} />
          </TabsContent>
        </Tabs>

        <DialogFooter className="mt-auto px-6 py-4 bg-muted/10 border-t border-border/50 shrink-0 gap-2">
          <Button type="button" variant="ghost" onClick={onClose}>Cancelar</Button>
          <Button 
            type="submit" 
            disabled={saving} 
            className="bg-primary hover:bg-primary/90 shadow-glow px-8 font-bold gap-2"
          >
            {saving 
              ? <span className="animate-spin w-4 h-4 border-2 border-white/30 border-t-white rounded-full" /> 
              : <Check className="w-4 h-4" />}
            {tipo ? 'Guardar Cambios' : 'Crear Tipo de Venta'}
          </Button>
        </DialogFooter>
      </form>
    </DialogContent>
  );
}

// ─── Main Page ────────────────────────────────────────────────────────────────
export default function TiposVentaPage() {
  const { hasPermission } = useAuth();
  const canCreate = hasPermission('tipos-venta:create');
  const canEdit   = hasPermission('tipos-venta:update');
  const canDelete = hasPermission('tipos-venta:delete');

  const [tipos, setTipos] = useState<TipoVenta[]>([]);
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState('');
  const [showModal, setShowModal] = useState(false);
  const [selected, setSelected] = useState<TipoVenta | null>(null);

  const loadData = async () => {
    setLoading(true);
    try {
      const data = await TiposVentaService.getAll();
      setTipos(data);
    } catch {
      toast.error('Error al cargar tipos de venta');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { loadData(); }, []);

  const filtered = useMemo(() => {
    if (!search) return tipos;
    const q = search.toLowerCase();
    return tipos.filter(t => 
      t.nombre.toLowerCase().includes(q) || 
      (t.codigo ?? '').toLowerCase().includes(q)
    );
  }, [tipos, search]);

  const handleSave = async (data: TipoVentaCreateDto) => {
    try {
      if (selected) {
        await TiposVentaService.update(selected.id, data);
        toast.success('Tipo de venta actualizado');
      } else {
        await TiposVentaService.create(data);
        toast.success('Tipo de venta creado');
      }
      setShowModal(false);
      setSelected(null);
      loadData();
    } catch {
      toast.error('Error al guardar el tipo de venta');
    }
  };

  const handleDelete = async (id: number) => {
    if (!confirm('¿Seguro que deseas eliminar este tipo de venta? Se perderán las configuraciones de variables asociadas.')) return;
    try {
      await TiposVentaService.delete(id);
      toast.success('Eliminado correctamente');
      setTipos(prev => prev.filter(t => t.id !== id));
    } catch {
      toast.error('No se pudo eliminar el tipo de venta');
    }
  };

  const kpis = [
    { title: 'Total Tipos', value: String(tipos.length), icon: Layers, sparkData: [2, 3, 3, 4, 4, 5, 5] },
    { title: 'Activos', value: String(tipos.filter(t => t.activo).length), icon: Check, sparkData: [1, 2, 2, 3, 3, 4, 4] },
    { title: 'Variables Totales', value: String(tipos.reduce((acc, t) => acc + (JSON.parse(t.esquemaVariablesJson || '[]').length), 0)), icon: Settings2, sparkData: [10, 12, 15, 14, 18, 20, 22] },
  ];

  return (
    <div className="space-y-6 animate-in fade-in duration-500 pb-20 sm:pb-6">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl font-bold text-foreground">Tipos de Venta</h1>
          <p className="text-sm text-muted-foreground">Configura las variables dinámicas para tus reglas de comisión</p>
        </div>
        {canCreate && (
          <Button 
            onClick={() => { setSelected(null); setShowModal(true); }}
            className="bg-primary hover:bg-primary/90 shadow-glow font-bold gap-2 px-6"
          >
            <Plus className="w-4 h-4" /> Nuevo Tipo
          </Button>
        )}
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {kpis.map((kpi, i) => <KpiCard key={kpi.title} {...kpi} index={i} />)}
      </div>

      <UnifiedSearchBar 
        selectionMode={false}
        onSelectionModeToggle={() => {}}
        searchValue={search}
        onSearchChange={setSearch}
        searchPlaceholder="Buscar por nombre o código..."
        onClearFilters={() => setSearch('')}
      />

      {loading ? (
        <div className="h-64 flex flex-col items-center justify-center gap-4">
          <div className="w-12 h-12 border-4 border-primary border-t-transparent rounded-full animate-spin" />
          <p className="text-muted-foreground animate-pulse">Cargando configuración...</p>
        </div>
      ) : filtered.length === 0 ? (
        <div className={cn('h-64', card, flexCenter, 'flex-col space-y-4 text-center border-dashed')}>
           <Network className="h-12 w-12 text-muted-foreground/30" />
           <div>
             <h3 className="text-lg font-medium">No hay registros</h3>
             <p className="text-sm text-muted-foreground">Crea un tipo de venta para empezar a definir variables.</p>
           </div>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          <AnimatePresence>
            {filtered.map((tipo, idx) => {
              const vars: VariableDefinicion[] = JSON.parse(tipo.esquemaVariablesJson || '[]');
              return (
                <motion.div
                  key={tipo.id}
                  initial={{ opacity: 0, y: 10 }}
                  animate={{ opacity: 1, y: 0 }}
                  exit={{ opacity: 0, scale: 0.95 }}
                  transition={{ delay: idx * 0.05 }}
                  className={cn('group relative overflow-hidden transition-all hover:scale-[1.02]', card)}
                >
                  <div className="p-6">
                    <div className="flex items-start justify-between mb-4">
                      <div className="flex items-center gap-3">
                        <div className="h-10 w-10 rounded-xl bg-primary/10 flex items-center justify-center text-primary border border-primary/20">
                          <Network className="w-5 h-5" />
                        </div>
                        <div>
                          <h3 className="font-bold text-base leading-none mb-1">{tipo.nombre}</h3>
                          <Badge variant="outline" className="text-[10px] font-mono border-primary/20 text-primary uppercase">
                            {tipo.codigo || 'S/C'}
                          </Badge>
                        </div>
                      </div>
                      <div className="flex gap-1 opacity-0 group-hover:opacity-100 transition-opacity">
                        {canEdit && (
                          <Button variant="ghost" size="icon" className="h-8 w-8 rounded-full"
                             onClick={() => { setSelected(tipo); setShowModal(true); }}>
                            <Edit className="w-4 h-4" />
                          </Button>
                        )}
                        {canDelete && (
                          <Button variant="ghost" size="icon" className="h-8 w-8 rounded-full text-destructive hover:bg-destructive/10"
                             onClick={() => handleDelete(tipo.id)}>
                            <Trash2 className="w-4 h-4" />
                          </Button>
                        )}
                      </div>
                    </div>

                    <div className="space-y-4">
                      <p className="text-xs text-muted-foreground line-clamp-2 h-8">
                        {tipo.descripcion || 'Sin descripción disponible.'}
                      </p>
                      
                      <div className="pt-4 border-t border-white/5">
                        <div className="flex items-center justify-between mb-2">
                           <span className="text-[10px] font-bold text-muted-foreground uppercase tracking-widest">Pipeline ({JSON.parse(tipo.estadosVentaJson || '[]').length} Estados)</span>
                           <span className="text-[10px] font-bold text-muted-foreground uppercase tracking-widest">Vars ({vars.length})</span>
                           {!tipo.activo && <Badge variant="outline" className="text-[9px] border-amber-500/20 text-amber-500">Inactivo</Badge>}
                        </div>
                        <div className="flex flex-wrap gap-x-2 gap-y-1 min-h-[40px] items-center">
                          {(() => {
                            const estados = JSON.parse(tipo.estadosVentaJson || '[]');
                            if (estados.length === 0) return <span className="text-[10px] text-destructive italic font-bold">Sin Pipeline</span>;
                            
                            const sorted = estados.sort((a: any, b: any) => a.orden - b.orden);
                            const displayed = sorted.slice(0, 4);
                            const hasMore = sorted.length > 4;
                            
                            return (
                              <p className="text-[10px] text-muted-foreground/80 font-medium leading-relaxed">
                                {displayed.map((e: any) => e.nombre).join(' · ')}
                                {hasMore && ' · ...'}
                              </p>
                            );
                          })()}
                        </div>
                      </div>
                    </div>
                  </div>
                </motion.div>
              );
            })}
          </AnimatePresence>
        </div>
      )}

      {/* TipoVenta Modal */}
      <Dialog open={showModal} onOpenChange={v => { if(!v) { setShowModal(false); setSelected(null); } }}>
        <TipoVentaModal 
          tipo={selected}
          onClose={() => { setShowModal(false); setSelected(null); }}
          onSave={handleSave}
        />
      </Dialog>
    </div>
  );
}
