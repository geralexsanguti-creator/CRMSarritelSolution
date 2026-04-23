import { useState } from "react";
import { ResponsiveModal } from "@/components/shared/ResponsiveModal";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Trash2, Plus, GripVertical, CheckCircle2 } from "lucide-react";
import { usePipelineConfig } from "@/hooks/usePipelineConfig";
import { PipelineConfig, TipoVentaConfig, EstadoVentaConfig } from "@/services/api/configuracion.service";
import { toast } from "sonner";

export function PipelineConfigModal({ open, onClose, initialData }: { open: boolean; onClose: () => void; initialData: PipelineConfig }) {
  const [tempConfig, setTempConfig] = useState<PipelineConfig>(JSON.parse(JSON.stringify(initialData)));
  const { updateConfig, isUpdating } = usePipelineConfig();

  const handleSave = async () => {
    try {
      await updateConfig(tempConfig);
      toast.success("Configuración actualizada correctamente");
      onClose();
    } catch (error) {
      toast.error("Error al guardar la configuración");
    }
  };

  const addTipo = () => {
    const newTipo: TipoVentaConfig = { codigo: "NUEVO", nombre: "Nuevo Tipo" };
    setTempConfig({ ...tempConfig, tiposVenta: [...tempConfig.tiposVenta, newTipo] });
  };

  const removeTipo = (idx: number) => {
    const newTipos = [...tempConfig.tiposVenta];
    newTipos.splice(idx, 1);
    setTempConfig({ ...tempConfig, tiposVenta: newTipos });
  };

  const updateTipo = (idx: number, field: keyof TipoVentaConfig, value: string) => {
    const newTipos = [...tempConfig.tiposVenta];
    newTipos[idx] = { ...newTipos[idx], [field]: value };
    setTempConfig({ ...tempConfig, tiposVenta: newTipos });
  };

  const addEstado = () => {
    const newEstado: EstadoVentaConfig = { 
        codigo: "NUEVO", 
        nombre: "Nuevo Estado", 
        color: "badge-neutral", 
        icono: "📋", 
        orden: (tempConfig.estadosVenta.length + 1) * 10,
        esInicial: false,
        esFinal: false
    };
    setTempConfig({ ...tempConfig, estadosVenta: [...tempConfig.estadosVenta, newEstado] });
  };

  const removeEstado = (idx: number) => {
    const newEstados = [...tempConfig.estadosVenta];
    newEstados.splice(idx, 1);
    setTempConfig({ ...tempConfig, estadosVenta: newEstados });
  };

  const updateEstado = (idx: number, field: keyof EstadoVentaConfig, value: any) => {
    const newEstados = [...tempConfig.estadosVenta];
    newEstados[idx] = { ...newEstados[idx], [field]: value };
    setTempConfig({ ...tempConfig, estadosVenta: newEstados });
  };

  return (
    <ResponsiveModal open={open} onOpenChange={(o) => !o && onClose()} title="Configuración del Pipeline de Ventas">
      <div className="space-y-6 max-h-[70vh] overflow-y-auto px-1">
        <Tabs defaultValue="tipos">
          <TabsList className="grid w-full grid-cols-2 bg-muted/20">
            <TabsTrigger value="tipos">Tipos de Venta</TabsTrigger>
            <TabsTrigger value="estados">Estados (SLA)</TabsTrigger>
          </TabsList>

          <TabsContent value="tipos" className="space-y-4 pt-4">
            <div className="space-y-3">
              {tempConfig.tiposVenta.map((tipo, idx) => (
                <div key={idx} className="flex gap-2 items-center glass p-3 rounded-lg">
                  <div className="flex-1 grid grid-cols-2 gap-2">
                    <div className="space-y-1">
                      <Label className="text-[10px] text-muted-foreground uppercase">Código</Label>
                      <Input value={tipo.codigo} onChange={(e) => updateTipo(idx, "codigo", e.target.value)} className="h-8 bg-background border-border" />
                    </div>
                    <div className="space-y-1">
                      <Label className="text-[10px] text-muted-foreground uppercase">Nombre</Label>
                      <Input value={tipo.nombre} onChange={(e) => updateTipo(idx, "nombre", e.target.value)} className="h-8 bg-background border-border" />
                    </div>
                  </div>
                  <Button variant="ghost" size="icon" onClick={() => removeTipo(idx)} className="text-destructive hover:bg-destructive/10"><Trash2 className="h-4 w-4" /></Button>
                </div>
              ))}
              <Button variant="outline" className="w-full border-dashed border-2 gap-2" onClick={addTipo}>
                <Plus className="h-4 w-4" /> Añadir Tipo de Venta
              </Button>
            </div>
          </TabsContent>

          <TabsContent value="estados" className="space-y-4 pt-4">
             <div className="space-y-3">
              {tempConfig.estadosVenta.sort((a,b) => a.orden - b.orden).map((estado, idx) => (
                <div key={idx} className="flex flex-col gap-3 glass p-4 rounded-lg">
                  <div className="flex justify-between items-center">
                    <div className="flex items-center gap-2">
                      <GripVertical className="h-4 w-4 text-muted-foreground/40 cursor-grab" />
                      <span className="font-bold text-sm text-foreground">Etapa {idx + 1}</span>
                    </div>
                    <Button variant="ghost" size="icon" onClick={() => removeEstado(idx)} className="text-destructive h-7 w-7"><Trash2 className="h-4 w-4" /></Button>
                  </div>
                  
                  <div className="grid grid-cols-2 md:grid-cols-4 gap-3">
                    <div className="space-y-1">
                      <Label className="text-[10px] text-muted-foreground uppercase">Nombre</Label>
                      <Input value={estado.nombre} onChange={(e) => updateEstado(idx, "nombre", e.target.value)} className="h-8 bg-background border-border" />
                    </div>
                    <div className="space-y-1">
                      <Label className="text-[10px] text-muted-foreground uppercase">Estado Color</Label>
                      <Input value={estado.color} onChange={(e) => updateEstado(idx, "color", e.target.value)} className="h-8 bg-background border-border" placeholder="badge-info" />
                    </div>
                    <div className="space-y-1">
                      <Label className="text-[10px] text-muted-foreground uppercase">Icono</Label>
                      <Input value={estado.icono} onChange={(e) => updateEstado(idx, "icono", e.target.value)} className="h-8 bg-background border-border" />
                    </div>
                    <div className="space-y-1">
                      <Label className="text-[10px] text-muted-foreground uppercase">Orden</Label>
                      <Input type="number" value={estado.orden} onChange={(e) => updateEstado(idx, "orden", Number(e.target.value))} className="h-8 bg-background border-border" />
                    </div>
                  </div>

                  <div className="flex gap-4 pt-1">
                    <label className="flex items-center gap-2 cursor-pointer">
                      <input type="checkbox" checked={estado.esInicial} onChange={(e) => updateEstado(idx, "esInicial", e.target.checked)} className="rounded border-border bg-background" />
                      <span className="text-xs text-muted-foreground">Estado Inicial</span>
                    </label>
                    <label className="flex items-center gap-2 cursor-pointer">
                      <input type="checkbox" checked={estado.esFinal} onChange={(e) => updateEstado(idx, "esFinal", e.target.checked)} className="rounded border-border bg-background" />
                      <span className="text-xs text-muted-foreground">Estado de Cierre (Final)</span>
                    </label>
                  </div>
                </div>
              ))}
              <Button variant="outline" className="w-full border-dashed border-2 gap-2" onClick={addEstado}>
                <Plus className="h-4 w-4" /> Añadir Nueva Etapa
              </Button>
            </div>
          </TabsContent>
        </Tabs>

        <div className="flex justify-end gap-2 pt-4 border-t border-border">
          <Button variant="ghost" onClick={onClose} disabled={isUpdating}>Cancelar</Button>
          <Button onClick={handleSave} disabled={isUpdating} className="gap-2">
            {isUpdating ? "Guardando..." : <><CheckCircle2 className="h-4 w-4" /> Guardar Cambios</>}
          </Button>
        </div>
      </div>
    </ResponsiveModal>
  );
}
