import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import { Play, Calendar, Save } from "lucide-react";
import { ResponsiveModal } from "@/components/shared/ResponsiveModal";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Button } from "@/components/ui/button";
import { Fichaje } from "@/types";

const fichajeSchema = z.object({
  tipoRegistro: z.string().min(1, "Campo obligatorio"),
  notas: z.string().optional(),
  horaEntrada: z.string().min(1, "La fecha de entrada es obligatoria"),
  horaSalida: z.string().optional(),
});

interface FichajeFormModalProps {
  open: boolean;
  onClose: () => void;
  onSubmit: (data: any) => void;
  isLoading: boolean;
  initialData?: Fichaje | null;
  mode: "create" | "edit";
}

export function FichajeFormModal({ open, onClose, onSubmit, isLoading, initialData, mode }: FichajeFormModalProps) {
  const form = useForm<z.infer<typeof fichajeSchema>>({
    resolver: zodResolver(fichajeSchema),
    defaultValues: {
      tipoRegistro: "Trabajando",
      notas: "",
      horaEntrada: "",
      horaSalida: "",
    },
  });

  const tipo = form.watch("tipoRegistro");
  const isNormal = tipo === "Trabajando";

  useEffect(() => {
    if (open) {
      if (mode === "edit" && initialData) {
        form.reset({
          tipoRegistro: initialData.tipoRegistro,
          notas: initialData.notas || "",
          horaEntrada: initialData.horaEntrada ? new Date(initialData.horaEntrada).toISOString().slice(0, 16) : "",
          horaSalida: initialData.horaSalida ? new Date(initialData.horaSalida).toISOString().slice(0, 16) : "",
        });
      } else {
        form.reset({
          tipoRegistro: "Trabajando",
          notas: "",
          horaEntrada: mode === "create" ? "" : new Date().toISOString().slice(0, 16),
          horaSalida: "",
        });
      }
    }
  }, [open, mode, initialData, form]);

  return (
    <ResponsiveModal 
      open={open} 
      onOpenChange={(o) => !o && onClose()} 
      title={mode === "create" ? "Iniciar Turno" : "Editar Registro de Fichaje"}
    >
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
          <FormField
            control={form.control}
            name="tipoRegistro"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Tipo de Fichaje</FormLabel>
                <Select onValueChange={field.onChange} value={field.value}>
                  <FormControl>
                    <SelectTrigger className="bg-background border-border">
                      <SelectValue placeholder="Seleccione tipo..." />
                    </SelectTrigger>
                  </FormControl>
                  <SelectContent className="glass">
                    <SelectItem value="Trabajando">Normal (Trabajando)</SelectItem>
                    <SelectItem value="Vacaciones">Vacaciones</SelectItem>
                    <SelectItem value="Baja Medica">Baja Médica</SelectItem>
                    <SelectItem value="Permiso">Permiso Personal</SelectItem>
                  </SelectContent>
                </Select>
                <FormMessage />
              </FormItem>
            )}
          />

          <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
            <FormField
              control={form.control}
              name="horaEntrada"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Entrada / Inicio</FormLabel>
                  <FormControl>
                    <Input type="datetime-local" className="bg-background border-border" {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="horaSalida"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Salida / Fin</FormLabel>
                  <FormControl>
                    <Input 
                      type="datetime-local" 
                      className="bg-background border-border" 
                      {...field} 
                      disabled={mode === "create" && isNormal}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>

          <FormField
            control={form.control}
            name="notas"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Notas u Observaciones</FormLabel>
                <FormControl>
                  <Input placeholder="Escriba información adicional (opcional)" className="bg-background border-border" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />

          <div className="flex justify-end gap-2 pt-4">
            <Button type="button" variant="ghost" onClick={onClose} disabled={isLoading}>Cancelar</Button>
            <Button type="submit" className="bg-primary text-primary-foreground gap-2" disabled={isLoading}>
              {mode === "create" ? (
                isNormal ? (
                  <><Play className="h-4 w-4" /> {isLoading ? "Iniciando..." : "Comenzar Turno"}</>
                ) : (
                  <><Calendar className="h-4 w-4" /> {isLoading ? "Guardando..." : "Registrar Periodo"}</>
                )
              ) : (
                <><Save className="h-4 w-4" /> {isLoading ? "Actualizando..." : "Guardar Cambios"}</>
              )}
            </Button>
          </div>
        </form>
      </Form>
    </ResponsiveModal>
  );
}
