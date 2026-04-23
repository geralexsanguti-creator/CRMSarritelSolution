import { useState, useEffect } from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Rol } from "@/types";
import { Loader2 } from "lucide-react";

interface RolesFormModalProps {
  open: boolean;
  onClose: () => void;
  rol: Rol | null;
  onSave: (data: Partial<Rol>) => Promise<void>;
}

export function RolesFormModal({ open, onClose, rol, onSave }: RolesFormModalProps) {
  const [nombre, setNombre] = useState("");
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (rol) {
      setNombre(rol.nombre);
    } else {
      setNombre("");
    }
  }, [rol, open]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!nombre.trim()) return;
    
    setLoading(true);
    try {
      await onSave({ nombre });
      onClose();
    } catch (error) {
      console.error("Error saving role:", error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog open={open} onOpenChange={(o) => !o && onClose()}>
      <DialogContent className="glass border-white/10 max-w-md">
        <DialogHeader>
          <DialogTitle className="text-lg font-bold">
            {rol ? "Editar Rol" : "Nuevo Rol"}
          </DialogTitle>
        </DialogHeader>
        <form onSubmit={handleSubmit} className="space-y-5 pt-4">
          <div className="space-y-2">
            <Label htmlFor="nombre" className="text-xs font-bold uppercase tracking-wider text-muted-foreground">
              Nombre del Rol
            </Label>
            <Input 
              id="nombre" 
              value={nombre} 
              onChange={e => setNombre(e.target.value)}
              placeholder="Ej: Supervisor de Ventas"
              className="bg-background/40 border-border/40 h-10"
              autoFocus
            />
          </div>
          <DialogFooter className="mt-8 flex gap-2">
            <Button type="button" variant="ghost" onClick={onClose}>Cancelar</Button>
            <Button 
              type="submit" 
              disabled={loading || !nombre.trim()} 
              className="bg-primary hover:bg-primary/90 shadow-glow px-6 font-bold"
            >
              {loading ? <Loader2 className="h-4 w-4 animate-spin mr-2" /> : null}
              {rol ? "Editar rol" : "Crear rol"}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
