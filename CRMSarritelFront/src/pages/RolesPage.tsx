import { useEffect, useState, useMemo } from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { RolesService } from "@/services/api/roles.service";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Badge } from "@/components/ui/badge"; // Added Badge import
import { 
  Shield, 
  ShieldAlert, 
  Loader2, 
  Save, 
  CheckCircle2, 
  Lock, 
  Info, 
  Fingerprint,
  Users,
  KeyRound,
  ShieldCheck,
  AlertCircle,
  Plus,
  Trash2,
  Edit
} from "lucide-react";
import { toast } from "sonner";
import { Permiso } from "@/types";
import { glass, card, shadowGlow } from "@/lib/styles";
import { cn } from "@/lib/utils";
import { useAuth } from "@/hooks/useAuth";
import { motion, AnimatePresence } from "framer-motion";
import { RolesFormModal } from "@/components/roles/RolesFormModal";
import { Rol } from "@/types";

const RolesPage = () => {
  const { hasPermission } = useAuth();
  const queryClient = useQueryClient();
  const [selectedRolId, setSelectedRolId] = useState<number | null>(null);
  const [selectedPermissionIds, setSelectedPermissionIds] = useState<number[]>([]);
  const [isFormOpen, setIsFormOpen] = useState(false);
  const [rolToEdit, setRolToEdit] = useState<Rol | null>(null);

  const { data: roles, isLoading: loadingRoles } = useQuery({
    queryKey: ["roles"],
    queryFn: RolesService.getAll,
  });

  const { data: allPermissions, isLoading: loadingAllPerms } = useQuery({
    queryKey: ["all-permissions"],
    queryFn: RolesService.getAllPermissions,
  });

  const { data: rolPermissions, isFetching: loadingRolPerms } = useQuery({
    queryKey: ["rol-permissions", selectedRolId],
    queryFn: () => selectedRolId ? RolesService.getPermissionsByRolId(selectedRolId) : Promise.resolve([]),
    enabled: !!selectedRolId,
  });

  // Sync permissions when rolPermissions changes
  useEffect(() => {
    if (rolPermissions) {
      setSelectedPermissionIds(rolPermissions.map(p => p.id));
    }
  }, [rolPermissions, selectedRolId]);

  const updateMutation = useMutation({
    mutationFn: ({ rolId, permIds }: { rolId: number, permIds: number[] }) => 
      RolesService.updateRolPermissions(rolId, permIds),
    onSuccess: () => {
      toast.success("Permisos actualizados correctamente", {
        icon: <ShieldCheck className="h-4 w-4 text-green-500" />,
        className: "glass"
      });
      queryClient.invalidateQueries({ queryKey: ["rol-permissions", selectedRolId] });
    },
    onError: (error) => {
      toast.error("Error al actualizar permisos: " + (error as any).message);
    }
  });

  const createRolMutation = useMutation({
    mutationFn: (data: Partial<Rol>) => RolesService.create(data),
    onSuccess: (newRol) => {
      toast.success("Rol creado correctamente");
      queryClient.invalidateQueries({ queryKey: ["roles"] });
      setSelectedRolId(newRol.id);
    },
    onError: (error) => {
      toast.error("Error al crear el rol: " + (error as any).message);
    }
  });

  const deleteRolMutation = useMutation({
    mutationFn: (id: number) => RolesService.delete(id),
    onSuccess: () => {
      toast.success("Rol eliminado");
      queryClient.invalidateQueries({ queryKey: ["roles"] });
      setSelectedRolId(null);
    },
    onError: (error) => {
      toast.error("Error al eliminar el rol: " + (error as any).message);
    }
  });

  const updateRolMutation = useMutation({
    mutationFn: (data: Partial<Rol>) => RolesService.update(data.id!, data),
    onSuccess: () => {
      toast.success("Rol actualizado correctamente");
      queryClient.invalidateQueries({ queryKey: ["roles"] });
    },
    onError: (error) => {
      toast.error("Error al actualizar el rol: " + (error as any).message);
    }
  });

  // Group permissions by modulo
  const groupedPermissions = useMemo(() => {
    return allPermissions?.reduce((acc, perm) => {
      if (!acc[perm.modulo]) acc[perm.modulo] = [];
      acc[perm.modulo].push(perm);
      return acc;
    }, {} as Record<string, Permiso[]>) || {};
  }, [allPermissions]);

  const handleRolSelect = (rolId: number) => {
    setSelectedRolId(rolId);
    // Fetch permissions via query, then sync in the UI
  };

  const togglePermission = (permId: number) => {
    const current = selectedRolId ? (rolPermissions?.map(p => p.id) || []) : [];
    // Note: We should ideally use a separate local state for modifications before saving
    // But for now let's use the mutation directly or a smarter sync
  };

  const handleSave = () => {
    if (!selectedRolId) return;
    updateMutation.mutate({ rolId: selectedRolId, permIds: selectedPermissionIds });
  };

  const isPermissionAssigned = (permId: number) => {
      return rolPermissions?.some(p => p.id === permId) || false;
  };

  const handleToggleLocal = (permId: number) => {
    if (!hasPermission("roles:update")) return;
    setSelectedPermissionIds(prev => 
      prev.includes(permId) ? prev.filter(id => id !== permId) : [...prev, permId]
    );
  };

  const hasChanges = useMemo(() => {
    if (!rolPermissions) return false;
    const initialIds = [...rolPermissions.map(p => p.id)].sort((a, b) => a - b);
    const currentIds = [...selectedPermissionIds].sort((a, b) => a - b);
    return JSON.stringify(initialIds) !== JSON.stringify(currentIds);
  }, [rolPermissions, selectedPermissionIds]);

  return (
    <div className="flex flex-col h-[calc(100vh-140px)] overflow-hidden animate-in fade-in duration-700">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4 shrink-0 mb-6">
        <div>
          <h1 className="text-2xl font-bold text-foreground">Gestión de Roles</h1>
          <p className="text-sm text-muted-foreground">Configura los privilegios y accesos para cada perfil operativo</p>
        </div>
        <div className="flex items-center gap-2">
          {hasPermission("roles:create") && (
            <Button onClick={() => { setRolToEdit(null); setIsFormOpen(true); }} className="bg-primary text-primary-foreground hover:bg-primary/90 gap-2 font-bold shadow-glow">
              <Plus className="h-4 w-4" /> Nuevo Rol
            </Button>
          )}
          {selectedRolId && hasChanges && hasPermission("roles:update") && (
            <Button 
              onClick={() => updateMutation.mutate({ rolId: selectedRolId, permIds: selectedPermissionIds })}
              disabled={updateMutation.isPending}
              className="bg-primary text-primary-foreground hover:bg-primary/90 font-bold gap-2 shadow-glow"
            >
              {updateMutation.isPending ? <Loader2 className="h-5 w-5 animate-spin" /> : <Save className="h-5 w-5" />}
              Guardar Cambios
            </Button>
          )}
        </div>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-4 gap-8 flex-1 min-h-0">
        {/* Roles List */}
        <div className="md:col-span-1 flex flex-col min-h-0">
          <div className="flex items-center gap-2 px-2 mb-2">
            <Users className="h-4 w-4 text-primary" />
            <span className="text-xs font-bold uppercase tracking-widest text-muted-foreground">Listado de Roles</span>
          </div>
          
          <ScrollArea className={cn("glass rounded-2xl border border-white/10 flex-1 min-h-0 mb-4", glass)}>
            <div className="divide-y divide-white/5">
            {loadingRoles ? (
              <div className="p-12 flex justify-center"><Loader2 className="animate-spin h-6 w-6 text-primary/50" /></div>
            ) : (
              roles?.map((rol) => (
                <div
                  key={rol.id}
                  onClick={() => setSelectedRolId(rol.id)}
                  role="button"
                  tabIndex={0}
                  onKeyDown={(e) => { if (e.key === "Enter" || e.key === " ") setSelectedRolId(rol.id); }}
                  className={cn(
                    "w-full text-left px-5 py-4 transition-all flex items-center justify-between group relative cursor-pointer outline-none focus-visible:ring-1 focus-visible:ring-primary/50",
                    selectedRolId === rol.id 
                      ? "bg-primary/10 text-primary border-l-4 border-primary" 
                      : "text-muted-foreground hover:bg-white/5"
                  )}
                >
                  <div className="flex flex-col min-w-0">
                    <span className={cn("text-sm font-bold truncate transition-colors", selectedRolId === rol.id ? "text-primary" : "group-hover:text-foreground")}>
                      {rol.nombre}
                    </span>
                    <span className="text-[10px] opacity-60 truncate">Nivel operacional</span>
                  </div>
                  {selectedRolId === rol.id ? (
                    <ShieldCheck className="h-4 w-4 text-primary shrink-0" />
                  ) : (
                    <div className="flex gap-1 opacity-0 group-hover:opacity-100 uppercase tracking-tighter">
                       {hasPermission("roles:update") && (
                         <Button 
                           variant="ghost" 
                           size="icon" 
                           className="h-6 w-6 text-primary hover:bg-primary/10" 
                           onClick={(e) => { e.stopPropagation(); setRolToEdit(rol); setIsFormOpen(true); }}
                         >
                           <Edit className="h-3.5 w-3.5" />
                         </Button>
                       )}
                       {hasPermission("roles:delete") && (
                         <Button 
                           variant="ghost" 
                           size="icon" 
                           className="h-6 w-6 text-destructive hover:bg-destructive/10" 
                           onClick={(e) => { e.stopPropagation(); if (confirm("¿Borrar rol?")) deleteRolMutation.mutate(rol.id); }}
                         >
                           <Trash2 className="h-3.5 w-3.5" />
                         </Button>
                       )}
                    </div>
                  )}
                </div>
              ))
            )}
            </div>
          </ScrollArea>
          
          <div className="p-4 rounded-2xl bg-amber-500/5 border border-amber-500/10 flex gap-3 shrink-0">
             <Info className="h-5 w-5 text-amber-500 shrink-0" />
             <p className="text-[10px] text-amber-500/80 leading-relaxed font-medium">
               Los cambios en los permisos se aplicarán la próxima vez que el usuario inicie sesión.
             </p>
          </div>
        </div>

        {/* Permissions Panel */}
        <div className="md:col-span-3 flex flex-col min-h-0">
          <div className="flex items-center justify-between px-2 mb-2">
            <div className="flex items-center gap-2">
              <ShieldAlert className="h-4 w-4 text-primary" />
              <span className="text-xs font-bold uppercase tracking-widest text-muted-foreground">Matrices de Acceso</span>
            </div>
            {selectedRolId && (
              <Badge variant="secondary" className="bg-primary/5 text-primary border-primary/10 font-bold px-3">
                {selectedPermissionIds.length} Permisos Activos
              </Badge>
            )}
          </div>

          <div className={cn("glass rounded-3xl border border-white/10 flex-1 min-h-0 flex flex-col relative overflow-hidden", glass)}>
            {!selectedRolId ? (
              <div className="flex-1 flex flex-col items-center justify-center p-12 text-center">
                <div className="h-20 w-20 rounded-full bg-primary/5 flex items-center justify-center mb-6">
                  <Lock className="h-10 w-10 text-primary/30" />
                </div>
                <h3 className="text-xl font-bold mb-2">Configuración Bloqueada</h3>
                <p className="text-sm text-muted-foreground max-w-xs">
                  Selecciona un rol para modificar su matriz de permisos.
                </p>
              </div>
            ) : loadingAllPerms || loadingRolPerms ? (
              <div className="flex-1 flex flex-col items-center justify-center">
                 <Loader2 className="animate-spin h-10 w-10 text-primary/40 mb-4" />
                 <p className="text-xs font-bold text-muted-foreground uppercase tracking-widest">Sincronizando Matriz...</p>
              </div>
            ) : (
              <>
                <div className="p-8 border-b border-white/5 bg-white/5 flex items-center justify-between">
                    <div className="flex items-center gap-4 group/title">
                       <div className="h-12 w-12 rounded-2xl bg-primary flex items-center justify-center shadow-glow">
                         <Fingerprint className="h-6 w-6 text-primary-foreground" />
                       </div>
                       <div>
                         <div className="flex items-center gap-2">
                           <h3 className="text-lg font-black tracking-tight leading-none">
                             {roles?.find(r => r.id === selectedRolId)?.nombre}
                           </h3>
                           {hasPermission("roles:update") && (
                             <Button 
                               variant="ghost" 
                               size="icon" 
                               className="h-6 w-6 opacity-0 group-hover/title:opacity-100 transition-opacity text-primary"
                               onClick={() => {
                                 const current = roles?.find(r => r.id === selectedRolId);
                                 if (current) {
                                   setRolToEdit(current);
                                   setIsFormOpen(true);
                                 }
                               }}
                             >
                               <Edit className="h-3.5 w-3.5" />
                             </Button>
                           )}
                         </div>
                         <p className="text-xs text-muted-foreground font-medium uppercase tracking-tighter mt-1">Matriz de Competencias</p>
                       </div>
                    </div>
                   
                   {!hasPermission("roles:update") && (
                     <Badge variant="destructive" className="gap-1.5 py-1.5 px-3">
                        <AlertCircle className="h-3.5 w-3.5" />
                        Solo Lectura
                     </Badge>
                   )}
                </div>

                <ScrollArea className="flex-1">
                  <div className="p-8 grid grid-cols-1 lg:grid-cols-2 gap-6">
                    <AnimatePresence mode="popLayout">
                      {Object.entries(groupedPermissions).map(([modulo, perms], idx) => (
                        <motion.div 
                          key={modulo}
                          initial={{ opacity: 0, x: 20 }}
                          animate={{ opacity: 1, x: 0 }}
                          transition={{ delay: idx * 0.05 }}
                          className="space-y-4 p-6 rounded-2xl border border-white/5 bg-white/5 relative group hover:border-primary/20 transition-all"
                        >
                          <div className="flex items-center justify-between mb-4 border-b border-white/5 pb-3">
                            <h3 className="font-black text-xs uppercase tracking-[0.1em] text-primary flex items-center gap-2">
                              {modulo}
                            </h3>
                            <Badge variant="outline" className="text-[9px] border-white/10 opacity-60">
                              {perms.length} acciones
                            </Badge>
                          </div>
                          
                          <div className="space-y-3">
                            {perms.map((perm) => (
                              <div 
                                key={perm.id} 
                                className={cn(
                                  "flex items-start space-x-3 p-3 rounded-xl transition-all border border-transparent",
                                  selectedPermissionIds.includes(perm.id) 
                                    ? "bg-primary/5 border-primary/10" 
                                    : "hover:bg-white/5"
                                )}
                              >
                                <Checkbox 
                                  id={"perm-" + perm.id} 
                                  disabled={!hasPermission("roles:update")}
                                  checked={selectedPermissionIds.includes(perm.id)}
                                  onCheckedChange={() => handleToggleLocal(perm.id)}
                                  className="mt-1 h-4 w-4 border-primary/30 data-[state=checked]:bg-primary"
                                />
                                <div className="grid gap-1 leading-none cursor-pointer flex-1" onClick={() => handleToggleLocal(perm.id)}>
                                  <label
                                    htmlFor={"perm-" + perm.id}
                                    className="text-xs font-bold leading-none cursor-pointer group-hover:text-primary transition-colors text-foreground block"
                                  >
                                    {perm.nombre}
                                  </label>
                                  <p className="text-[10px] text-muted-foreground leading-relaxed italic">
                                    {perm.descripcion || ("Acción de " + perm.nombre.split(':')[1] + " en " + modulo)}
                                  </p>
                                </div>
                              </div>
                            ))}
                          </div>
                        </motion.div>
                      ))}
                    </AnimatePresence>
                  </div>
                </ScrollArea>
              </>
            )}
          </div>
        </div>
      </div>
      <RolesFormModal 
        open={isFormOpen} 
        onClose={() => setIsFormOpen(false)} 
        rol={rolToEdit} 
        onSave={async (data) => {
          if (rolToEdit) {
            await updateRolMutation.mutateAsync({ ...data, id: rolToEdit.id });
          } else {
            await createRolMutation.mutateAsync(data);
          }
        }}
      />
    </div>
  );
};

export default RolesPage;
