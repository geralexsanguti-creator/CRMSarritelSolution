import { useState } from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { DatePicker } from "@/components/ui/date-picker";
import { Label } from "@/components/ui/label";
import { Calendar } from "lucide-react";
import { usePeriodos } from "@/hooks/usePeriodos";
import { toast } from "sonner";
import { Checkbox } from "@/components/ui/checkbox";

export function PeriodoFormModal({
  open,
  onClose
}: {
  open: boolean;
  onClose: () => void;
}) {
  const { createPeriodo, isCreating } = usePeriodos();
  const [formData, setFormData] = useState({
    nombre: "",
    fechaInicio: "",
    fechaFin: "",
    esPrincipal: false
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!formData.nombre.trim() || !formData.fechaInicio || !formData.fechaFin) {
      toast.error("El nombre y las fechas son requeridos");
      return;
    }
    
    try {
      await createPeriodo({
        nombre: formData.nombre,
        fechaInicio: new Date(formData.fechaInicio).toISOString(),
        fechaFin: new Date(formData.fechaFin).toISOString(),
        esPrincipal: formData.esPrincipal
      });
      onClose();
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <Dialog open={open} onOpenChange={(o) => !o && onClose()}>
      <DialogContent className="glass border-border max-w-md">
        <DialogHeader>
          <DialogTitle className="text-foreground flex items-center gap-2">
            <Calendar className="h-5 w-5 text-primary" />
            Crear Nuevo Periodo
          </DialogTitle>
        </DialogHeader>
        
        <form onSubmit={handleSubmit} className="space-y-6 mt-4">
          <div className="space-y-4">
            <div className="space-y-2">
              <Label>Nombre del Periodo</Label>
              <Input
                placeholder="Ej. Marzo 2026"
                value={formData.nombre}
                onChange={(e) => setFormData(prev => ({ ...prev, nombre: e.target.value }))}
                disabled={isCreating}
              />
            </div>
            
            <div className="grid grid-cols-2 gap-4">
              <div className="space-y-2">
                <Label>Fecha Inicio</Label>
                <DatePicker
                  value={formData.fechaInicio}
                  onChange={(val) => setFormData(prev => ({ ...prev, fechaInicio: val }))}
                  disabled={isCreating}
                />
              </div>
              <div className="space-y-2">
                <Label>Fecha Fin</Label>
                <DatePicker
                  value={formData.fechaFin}
                  onChange={(val) => setFormData(prev => ({ ...prev, fechaFin: val }))}
                  disabled={isCreating}
                />
              </div>
            </div>

            <div className="flex items-center space-x-2 pt-2">
              <Checkbox 
                id="principal" 
                checked={formData.esPrincipal}
                onCheckedChange={(c) => setFormData(prev => ({ ...prev, esPrincipal: !!c }))}
                disabled={isCreating}
              />
              <Label htmlFor="principal" className="text-sm font-medium cursor-pointer">
                Marcar como periodo principal
              </Label>
            </div>
            <p className="text-[10px] text-muted-foreground leading-tight">
              Si marcas esta casilla, este periodo será el predeterminado para todos los cálculos y KPIs globales tras crearlo.
            </p>
          </div>

          <DialogFooter>
            <Button type="button" variant="ghost" onClick={onClose} disabled={isCreating}>Cancelar</Button>
            <Button type="submit" className="bg-primary text-primary-foreground font-bold shadow-glow" disabled={isCreating}>
              {isCreating ? "Guardando..." : "Crear Periodo"}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
