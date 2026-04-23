import { useState, useEffect } from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { DatePicker } from "@/components/ui/date-picker";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { Switch } from "@/components/ui/switch";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Usuario, Rol, Equipo } from "@/types";
import { UserCheck, Shield, Users } from "lucide-react";
import { RolesService } from "@/services/api/roles.service";
import { equiposService } from "@/services/api/equipos.service";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Badge } from "@/components/ui/badge";
import { Checkbox } from "@/components/ui/checkbox";
import { ImageUpload } from "@/components/shared/ImageUpload";

interface UsuarioFormModalProps {
  open: boolean;
  onClose: () => void;
  usuario?: Usuario | null;
  onSave: (data: any) => void;
}

export function UsuarioFormModal({ open, onClose, usuario, onSave }: UsuarioFormModalProps) {
  const [isSaving, setIsSaving] = useState(false);
  const [formData, setFormData] = useState<any>({
    nombre: "",
    username: "",
    email: "",
    passwordHash: "",
    activo: true,
    rolId: 0,
    equipoIds: [] as number[],
    fechaContratacion: new Date().toISOString().split('T')[0],
    salarioBase: 0,
    fotoPerfil: ""
  });

  const [roles, setRoles] = useState<Rol[]>([]);
  const [availableEquipos, setAvailableEquipos] = useState<Equipo[]>([]);
  const [loadingOptions, setLoadingOptions] = useState(false);

  useEffect(() => {
    const fetchOptions = async () => {
      setLoadingOptions(true);
      try {
        const [r, e] = await Promise.all([
          RolesService.getAll(),
          equiposService.getAll()
        ]);
        setRoles(r);
        setAvailableEquipos(e);
      } catch (error) {
        console.error("Error fetching roles, teams or users:", error);
      } finally {
        setLoadingOptions(false);
      }
    };
    if (open) {
      fetchOptions();
      setIsSaving(false);
    }
  }, [open]);

  useEffect(() => {
    if (usuario) {
      setFormData({
        ...usuario,
        nombre: usuario.nombre || "",
        username: (usuario as any).username || "",
        email: usuario.email || "",
        salarioBase: usuario.salarioBase || 0,
        fechaContratacion: usuario.fechaContratacion ? new Date(usuario.fechaContratacion).toISOString().split('T')[0] : "",
        passwordHash: "", 
        rolId: usuario.rolId || (usuario.usuarioRoles && usuario.usuarioRoles.length > 0 ? usuario.usuarioRoles[0].rolId : 0) || 0,
        equipoIds: usuario.equipoIds?.length ? usuario.equipoIds : (usuario.equipos?.map(eq => eq.equipoId) || []),
        fotoPerfil: usuario.fotoPerfil || ""
      });
    } else {
      setFormData({
        nombre: "",
        username: "",
        email: "",
        passwordHash: "",
        activo: true,
        fechaContratacion: new Date().toISOString().split('T')[0],
        salarioBase: 0,
        rolId: 0,
        equipoIds: [],
        fotoPerfil: ""
      });
    }
  }, [usuario, open]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (isSaving) return;

    setIsSaving(true);
    try {
      // Limpiar el payload para enviar solo lo que el DTO espera (Distinct equipos)
      const payload: any = {
        id: usuario?.id || 0,
        nombre: formData.nombre,
        username: formData.username,
        email: formData.email && formData.email.trim() !== "" ? formData.email : null,
        activo: formData.activo,
        departamento: null,
        puesto: null,
        rolId: formData.rolId || 0,
        equipoIds: Array.from(new Set(formData.equipoIds || [])).filter((id: number) => id > 0),
        salarioBase: Number(formData.salarioBase || 0),
        comisiones: 0,
        superiorId: null,
        fotoPerfil: formData.fotoPerfil
      };

      if (formData.fechaContratacion && formData.fechaContratacion !== "") {
        payload.fechaContratacion = formData.fechaContratacion;
      } else {
        payload.fechaContratacion = null;
      }

      if (formData.passwordHash) {
        payload.password = formData.passwordHash;
      }

      console.log("Saving user payload:", JSON.stringify(payload, null, 2));
      await onSave(payload);
    } catch (error: any) {
      console.error("Error saving user:", error);
      if (error.response) {
        console.error("Backend Error Data:", JSON.stringify(error.response.data, null, 2));
      }
      setIsSaving(false);
    }
  };

  return (
    <Dialog open={open} onOpenChange={(o) => !o && !isSaving && onClose()}>
      <DialogContent className="glass border-border max-w-xl max-h-[85vh] h-full sm:h-auto overflow-hidden flex flex-col pt-6 pb-0 px-0">
        <DialogHeader className="px-6 pb-2 shrink-0">
          <DialogTitle className="flex items-center gap-2 text-foreground">
            <UserCheck className="h-5 w-5 text-primary" />
            {usuario ? "Editar Usuario" : "Nuevo Usuario"}
          </DialogTitle>
        </DialogHeader>

        <ScrollArea className="flex-1 px-6 pb-6 overflow-y-auto w-full">
          <form id="usuario-form" onSubmit={handleSubmit} className="space-y-6 pt-2 px-1">
            
            <div className="space-y-4">
              <h3 className="text-sm font-medium text-muted-foreground uppercase tracking-wider">Credenciales</h3>
              
              <div className="flex gap-6 items-start">
                <div className="w-24 h-24 shrink-0">
                  <ImageUpload 
                    value={formData.fotoPerfil} 
                    onChange={(val) => setFormData({...formData, fotoPerfil: val || ''})} 
                    placeholder="Foto de Perfil"
                    className="rounded-full"
                  />
                </div>
                <div className="flex-1 space-y-4 pt-2">
                  <div className="space-y-2">
                    <Label>Nombre Completo <span className="text-destructive">*</span></Label>
                    <Input required disabled={isSaving} value={formData.nombre} onChange={e => setFormData({...formData, nombre: e.target.value})} className="bg-background/50" />
                  </div>
                </div>
              </div>

              <div className="grid grid-cols-2 gap-4">
                <div className="space-y-2">
                  <Label>Username <span className="text-destructive">*</span></Label>
                  <Input required disabled={isSaving} value={formData.username} onChange={e => setFormData({...formData, username: e.target.value})} className="bg-background/50" />
                </div>
                <div className="space-y-2">
                  <Label>Email</Label>
                  <Input type="email" disabled={isSaving} value={formData.email || ""} onChange={e => setFormData({...formData, email: e.target.value})} className="bg-background/50" />
                </div>
              </div>

              <div className="space-y-2">
                <Label>{usuario ? "Nueva Contraseña (Dejar en blanco para no cambiar)" : "Contraseña"} <span className={usuario ? "hidden": "text-destructive"}>*</span></Label>
                <Input required={!usuario} disabled={isSaving} type="password" value={formData.passwordHash} onChange={e => setFormData({...formData, passwordHash: e.target.value})} className="bg-background/50" />
              </div>
            </div>

            <div className="space-y-4 border-t border-border pt-4">
              <h3 className="text-sm font-medium text-muted-foreground uppercase tracking-wider">Perfil Operativo</h3>
              
              <div className="space-y-2">
                <Label className="flex items-center gap-1.5">
                  <Shield className="h-3.5 w-3.5" />
                  Rol Jerárquico
                </Label>
                <Select 
                  disabled={isSaving}
                  value={formData.rolId ? formData.rolId.toString() : undefined} 
                  onValueChange={v => setFormData({...formData, rolId: parseInt(v)})}
                >
                  <SelectTrigger className="bg-background/50">
                    <SelectValue placeholder="Seleccionar Rol" />
                  </SelectTrigger>
                  <SelectContent>
                    {roles.map(r => (
                      <SelectItem key={r.id} value={r.id.toString()}>{r.nombre}</SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>

              <div className="space-y-3">
                <Label className="flex items-center gap-1.5">
                  <Users className="h-3.5 w-3.5" />
                  Equipos Asignados
                </Label>
                <div className="grid grid-cols-1 sm:grid-cols-2 gap-2 p-3 glass rounded-lg max-h-40 overflow-y-auto">
                  {availableEquipos.map(e => (
                    <div key={e.id} className="flex items-center space-x-2">
                      <Checkbox 
                        id={`team-${e.id}`} 
                        disabled={isSaving}
                        checked={formData.equipoIds.includes(e.id)}
                        onCheckedChange={(checked) => {
                          const currentIds = formData.equipoIds || [];
                          const newIds = checked 
                            ? Array.from(new Set([...currentIds, e.id]))
                            : currentIds.filter((id: number) => id !== e.id);
                          setFormData({...formData, equipoIds: newIds});
                        }}
                      />
                      <label htmlFor={`team-${e.id}`} className="text-sm font-medium leading-none cursor-pointer">
                        {e.nombre}
                      </label>
                    </div>
                  ))}
                  {availableEquipos.length === 0 && <span className="text-xs text-muted-foreground italic">No hay equipos disponibles</span>}
                </div>
              </div>

              <div className="grid grid-cols-2 gap-4">
                <div className="space-y-2">
                  <Label>Fecha de Ingreso</Label>
                  <DatePicker disabled={isSaving} value={formData.fechaContratacion || ""} onChange={val => setFormData({...formData, fechaContratacion: val})} className="border-border/60" />
                </div>
                <div className="space-y-2">
                  <Label>Salario Base (Mensual)</Label>
                  <Input type="number" disabled={isSaving} step="0.01" value={formData.salarioBase || 0} onChange={e => setFormData({...formData, salarioBase: e.target.value})} className="bg-background/50" />
                </div>
              </div>

              <div className="flex items-center justify-between p-3 glass rounded-lg mt-2">
                 <Label htmlFor="activo" className="font-medium cursor-pointer">Cuenta Activa (Habilita Login)</Label>
                 <Switch id="activo" disabled={isSaving} checked={formData.activo} onCheckedChange={c => setFormData({...formData, activo: c})} />
              </div>
            </div>
            
          </form>
        </ScrollArea>
        
        <div className="p-6 pt-4 border-t border-border bg-muted/10">
          <div className="flex justify-end gap-2">
            <Button variant="outline" type="button" disabled={isSaving} onClick={onClose}>Cancelar</Button>
            <Button type="submit" form="usuario-form" disabled={isSaving} className="bg-primary hover:bg-primary/90 min-w-[120px]">
              {isSaving ? "Guardando..." : (usuario ? "Guardar Cambios" : "Crear Usuario")}
            </Button>
          </div>
        </div>
      </DialogContent>
    </Dialog>
  );
}
