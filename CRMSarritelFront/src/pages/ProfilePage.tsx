import { useAuth } from "@/hooks/useAuth";
import { useParams, useNavigate } from "react-router-dom";
import { useUsuario } from "@/hooks/useUsuarios";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Badge } from "@/components/ui/badge";
import { Separator } from "@/components/ui/separator";
import { 
  Shield, 
  Calendar, 
  Key, 
  CheckCircle2, 
  XCircle,
  Briefcase,
  Smartphone,
  Mail,
  Lock,
  Info,
  Users,
  Percent,
  LayoutDashboard,
  ShoppingCart
} from "lucide-react";
import { motion } from "framer-motion";
import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";

export default function ProfilePage() {
  const { user: currentUser, hasPermission } = useAuth();
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const targetId = id ? parseInt(id, 10) : null;
  const isMyProfile = !targetId || String(targetId) === String(currentUser?.id);

  // React Query solo se ejecutará si targetId existe y no es el propio usuario
  const { data: targetUser, isLoading } = useUsuario(!isMyProfile ? targetId : null);

  if (isLoading) {
    return (
      <div className="flex items-center justify-center p-12 text-primary">
        <span className="animate-pulse flex items-center gap-2">Cargando perfil...</span>
      </div>
    );
  }

  // Determinar el modelo final de usuario a mostrar
  const rawDisplayUser = isMyProfile ? currentUser : targetUser;

  if (!rawDisplayUser) {
    return (
      <div className="flex flex-col items-center justify-center p-12 text-muted-foreground gap-4">
        <Users className="h-12 w-12" />
        <h2 className="text-xl font-bold">Usuario no encontrado</h2>
        <Button onClick={() => navigate(-1)} variant="outline">Volver</Button>
      </div>
    );
  }

  // Cast to any to handle both User (from Auth) and Usuario (from API) seamlessly
  const displayUser = rawDisplayUser as any;

  const permissions = [
    { name: "Ver todas las comisiones", key: "view_all_commissions", granted: displayUser.canViewAllCommissions },
    { name: "Administrador", key: "is_admin", granted: displayUser.rol === "Admin" || displayUser.rol === "SuperAdmin" },
    { name: "Ver Precios", key: "ventas:view_prices", granted: true }, // Referencia básica
  ];

  const handleRoleClick = () => {
    if (hasPermission('usuarios:read')) {
      navigate('/usuarios');
    }
  };

  return (
    <div className="space-y-6 max-w-5xl mx-auto pb-10">
      <div className="flex flex-col gap-2">
        <h1 className="text-3xl font-bold tracking-tight">
          {isMyProfile ? "Mi Perfil" : "Perfil de Comercial"}
        </h1>
        <p className="text-muted-foreground">
          {isMyProfile ? "Gestiona tu información personal y revisa tus permisos de acceso." : `Vista detallada de ${displayUser.nombre}.`}
        </p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        {/* Columna Izquierda: Avatar e Info Básica */}
        <motion.div 
          initial={{ opacity: 0, x: -20 }}
          animate={{ opacity: 1, x: 0 }}
          className="md:col-span-1 space-y-6"
        >
          <Card className="glass border-white/10 overflow-hidden">
            <div className="h-24 bg-gradient-to-r from-primary/20 to-primary/5" />
            <CardContent className="pt-0 -mt-12 flex flex-col items-center text-center pb-8">
              <Avatar className="h-24 w-24 border-4 border-background shadow-xl">
                <AvatarImage src={`https://avatar.iran.liara.run/username?username=${displayUser.nombre}`} />
                <AvatarFallback className="bg-primary/10 text-primary text-2xl font-bold">
                  {displayUser.nombre?.substring(0, 2).toUpperCase()}
                </AvatarFallback>
              </Avatar>
              <h2 className="mt-4 text-xl font-bold">{displayUser.nombre}</h2>
              <Badge 
                variant="secondary" 
                className={cn(
                  "mt-1 bg-primary/10 text-primary border-primary/20",
                  hasPermission("usuarios:read") && "cursor-pointer hover:bg-primary/20 transition-colors"
                )}
                onClick={handleRoleClick}
              >
                {displayUser.rol || displayUser.rol_Nombre || "Sin Rol"}
              </Badge>
              <p className="mt-4 text-sm text-muted-foreground flex items-center gap-2">
                <Mail className="h-3.5 w-3.5" /> {displayUser.email || "Sin email"}
              </p>
            </CardContent>
            <Separator className="bg-white/5" />
            <div className="p-6 space-y-4">
              <div className="flex items-center justify-between text-sm">
                <span className="text-muted-foreground flex items-center gap-2">
                  <Briefcase className="h-4 w-4" /> Puesto
                </span>
                <span className="font-medium">{displayUser.rol || displayUser.rol_Nombre || "Comercial"}</span>
              </div>
              <div className="flex items-center justify-between text-sm">
                <span className="text-muted-foreground flex items-center gap-2">
                  <Smartphone className="h-4 w-4" /> Móvil
                </span>
                <span className="font-medium">N/A</span>
              </div>
              <div className="flex items-center justify-between text-sm">
                <span className="text-muted-foreground flex items-center gap-2">
                  <Calendar className="h-4 w-4" /> Alta
                </span>
                <span className="font-medium">
                  {displayUser.fechaContratacion ? new Date(displayUser.fechaContratacion).toLocaleDateString() : "Desconocido"}
                </span>
              </div>
            </div>
          </Card>

          {isMyProfile ? (
            <Card className="glass border-white/10">
              <CardHeader className="pb-3">
                <CardTitle className="text-sm font-bold uppercase tracking-wider text-muted-foreground flex items-center gap-2">
                  <Shield className="h-4 w-4 text-primary" /> Seguridad
                </CardTitle>
              </CardHeader>
              <CardContent className="space-y-4">
                <Button variant="outline" className="w-full justify-start gap-2 border-white/5 bg-white/5 hover:bg-white/10">
                  <Key className="h-4 w-4" /> Cambiar Contraseña
                </Button>
                <Button variant="outline" className="w-full justify-start gap-2 border-white/5 bg-white/5 hover:bg-white/10">
                  <Lock className="h-4 w-4" /> Activar 2FA
                </Button>
              </CardContent>
            </Card>
          ) : (
             <Card className="glass bg-primary/5 border-primary/20 border-dashed">
              <CardHeader className="pb-3">
                <CardTitle className="text-sm font-bold uppercase tracking-wider text-primary flex items-center gap-2">
                  <LayoutDashboard className="h-4 w-4" /> Acciones Rápidas
                </CardTitle>
              </CardHeader>
              <CardContent className="space-y-3">
                <Button 
                  onClick={() => navigate(`/ventas?vendedor=${displayUser.id}`)} 
                  className="w-full justify-start gap-2 bg-background hover:bg-accent text-foreground border border-border shadow-sm"
                >
                  <ShoppingCart className="h-4 w-4 text-emerald-500" /> Ver ventas del comercial
                </Button>
                {hasPermission('view_all_commissions') && (
                   <Button 
                    onClick={() => navigate(`/comisiones?empleadoId=${displayUser.id}`)} 
                    className="w-full justify-start gap-2 bg-background hover:bg-accent text-foreground border border-border shadow-sm"
                   >
                     <Percent className="h-4 w-4 text-blue-500" /> Verificar comisiones
                   </Button>
                )}
              </CardContent>
            </Card>
          )}
        </motion.div>

        {/* Columna Derecha: Detalles y Permisos */}
        <motion.div 
          initial={{ opacity: 0, x: 20 }}
          animate={{ opacity: 1, x: 0 }}
          className="md:col-span-2 space-y-6"
        >
          <Card className="glass border-white/10 h-full">
            <CardHeader className="border-b border-white/5 pb-4">
              <CardTitle className="flex items-center justify-between">
                <div className="flex items-center gap-2">
                  <Shield className="h-5 w-5 text-primary" /> Permisos de Nivel
                </div>
                {displayUser.activo ? (
                    <Badge className="bg-green-500/20 text-green-500 border-none">Cuenta Activa</Badge>
                ) : (
                    <Badge variant="destructive" className="border-none">Cuenta Inactiva</Badge>
                )}
              </CardTitle>
            </CardHeader>
            <CardContent className="pt-6">
              <div className="space-y-1">
                {permissions.map((perm, idx) => (
                  <div key={perm.key} className={cn(
                    "flex items-center justify-between p-4 rounded-xl transition-colors",
                    idx % 2 === 0 ? "bg-white/5" : "bg-transparent"
                  )}>
                    <div className="flex items-center gap-4">
                      <div className={cn(
                        "h-10 w-10 rounded-lg flex items-center justify-center",
                        perm.granted ? "bg-green-500/10 text-green-500" : "bg-muted text-muted-foreground"
                      )}>
                        {perm.granted ? <CheckCircle2 className="h-5 w-5" /> : <XCircle className="h-5 w-5" />}
                      </div>
                      <div>
                        <p className="font-bold text-sm tracking-tight">{perm.name}</p>
                        <p className="text-[10px] font-mono text-muted-foreground uppercase">{perm.key}</p>
                      </div>
                    </div>
                    {perm.granted ? (
                      <Badge className="bg-green-500/20 text-green-500 border-none hover:bg-green-500/30">Habilitado</Badge>
                    ) : (
                      <Badge variant="outline" className="opacity-50">Inactivo</Badge>
                    )}
                  </div>
                ))}
              </div>

              <div className="mt-8 p-6 rounded-2xl bg-primary/5 border border-primary/10 relative overflow-hidden group">
                <div className="absolute top-0 right-0 p-4 opacity-10 group-hover:scale-110 transition-transform">
                  <Shield className="h-24 w-24" />
                </div>
                <h4 className="font-bold text-primary flex items-center gap-2">
                  <Info className="h-4 w-4" /> Contexto de Seguridad
                </h4>
                <p className="mt-2 text-sm text-muted-foreground leading-relaxed">
                  El rol de <b>{displayUser.rol || displayUser.rol_Nombre}</b> determina el alcance dentro de CRMSarritel. 
                  {displayUser.canViewAllCommissions ? " Dispone de visibilidad global sin filtros prescriptivos." : " Operaciones aisladas a su nodo o equipo asignado."}
                </p>
              </div>
            </CardContent>
          </Card>
        </motion.div>
      </div>
    </div>
  );
}
