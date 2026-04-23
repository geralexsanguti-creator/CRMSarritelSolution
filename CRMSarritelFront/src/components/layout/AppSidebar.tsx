import { 
  Users, ShoppingCart, Percent, LayoutDashboard, Package, 
  UserCheck, Clock, Network, ShieldCheck, LogOut, Building2, User
} from "lucide-react";
import { NavLink } from "@/components/NavLink";
import { useLocation } from "react-router-dom";
import {
  Sidebar, SidebarContent, SidebarGroup, SidebarGroupContent, SidebarGroupLabel,
  SidebarMenu, SidebarMenuButton, SidebarMenuItem, SidebarHeader, SidebarFooter, useSidebar,
} from "@/components/ui/sidebar";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";
import { useAuth } from "@/contexts/AuthContext";

const modules = [
  { title: "Dashboard", url: "/", icon: LayoutDashboard },
  { title: "Clientes", url: "/clientes", icon: Users },
  { title: "Usuarios y Roles", url: "/usuarios", icon: UserCheck },
  { title: "Permisos de Roles", url: "/roles", icon: ShieldCheck },
  { title: "Equipos", url: "/equipos", icon: Network },
  { title: "Fichajes", url: "/fichajes", icon: Clock },
  { title: "Productos", url: "/productos", icon: Package },
  { title: "Proveedores", url: "/proveedores", icon: Building2 },
  { title: "Tipos de Venta", url: "/tipos-venta", icon: Network },
  { title: "Ventas", url: "/ventas", icon: ShoppingCart },
  { title: "Comisiones", url: "/comisiones", icon: Percent },
];

export function AppSidebar() {
  const { state } = useSidebar();
  const { user, logout, hasPermission } = useAuth();
  const collapsed = state === "collapsed";

  const filteredModules = modules.filter(item => {
    if (item.title === "Dashboard") return true; // Everyone sees dashboard
    const permissionKey = item.title === "Usuarios y Roles" ? "usuarios" :
                         item.title === "Permisos de Roles" ? "roles" :
                         item.title === "Proveedores" ? "proveedores" :
                         item.title === "Tipos de Venta" ? "tipos-venta" :
                         item.title.toLowerCase();
    return hasPermission(`${permissionKey}:read`);
  });

  return (
    <Sidebar collapsible="icon" className="border-r-0 border-border">
      <SidebarHeader className="h-14 flex flex-col justify-center px-4 border-b border-border">
        {!collapsed ? (
          <div className="flex items-center gap-2">
            <div className="h-8 w-8 rounded-lg flex items-center justify-center overflow-hidden">
              <img src="https://assets.zyrosite.com/cdn-cgi/image/format=auto,w=1440,h=1406,fit=crop/A85VPGJ19kuDnEpd/logo-1-YleQOGROkNuo7E06.png" alt="CRMSarritel Logo" className="w-full h-full object-cover" />
            </div>
            <div>
              <h2 className="text-sm font-bold text-foreground leading-none mb-1">CRMSarritel</h2>
              <p className="text-[10px] text-muted-foreground leading-none">Gestión Comercial</p>
            </div>
          </div>
        ) : (
          <div className="h-8 w-8 rounded-lg flex items-center justify-center mx-auto overflow-hidden">
            <img src="https://assets.zyrosite.com/cdn-cgi/image/format=auto,w=1440,h=1406,fit=crop/A85VPGJ19kuDnEpd/logo-1-YleQOGROkNuo7E06.png" alt="CRMSarritel Logo" className="w-full h-full object-cover" />
          </div>
        )}
      </SidebarHeader>
      <SidebarContent>
        <SidebarGroup>
          <SidebarGroupLabel className="text-muted-foreground/60 text-[10px] uppercase tracking-widest">
            {!collapsed && "Módulos"}
          </SidebarGroupLabel>
          <SidebarGroupContent>
            <SidebarMenu>
              {filteredModules.map((item) => (
                <SidebarMenuItem key={item.title}>
                  <SidebarMenuButton asChild>
                    <NavLink to={item.url} end={item.url === "/"} className="hover:bg-sidebar-accent/60 transition-colors" activeClassName="bg-primary/10 text-primary font-medium border-l-2 border-primary">
                      <item.icon className="mr-2 h-4 w-4" />
                      {!collapsed && <span>{item.title}</span>}
                    </NavLink>
                  </SidebarMenuButton>
                </SidebarMenuItem>
              ))}
            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>
      <SidebarFooter className="border-t border-border p-2">
         <SidebarMenu>
            <SidebarMenuItem>
                 <div className={cn(
                  "flex items-center gap-2 p-2 rounded-lg transition-colors group cursor-pointer",
                  !collapsed && "hover:bg-sidebar-accent/50"
                )}
                onClick={() => window.location.href = '/perfil'}
                >
                  <Avatar className="h-8 w-8 shrink-0 border border-border/50">
                    <AvatarImage src={user?.imagen_Url} alt={user?.nombre} />
                    <AvatarFallback className="bg-primary/10 text-primary text-[10px] font-bold">
                        {user?.nombre?.charAt(0) || "U"}
                    </AvatarFallback>
                  </Avatar>
                  {!collapsed && (
                    <div className="flex flex-col min-w-0 flex-1">
                      <span className="text-xs font-bold truncate text-foreground group-hover:underline">{user?.nombre}</span>
                      <span className="text-[10px] text-muted-foreground truncate uppercase tracking-tighter font-medium">
                        {user?.rol_Nombre || (user?.roles?.[0]) || 'Usuario'}
                      </span>
                    </div>
                  )}
                  {!collapsed && (
                    <Button 
                      variant="ghost" 
                      size="icon" 
                      onClick={() => logout()}
                      className="h-7 w-7 opacity-0 group-hover:opacity-100 transition-opacity hover:text-destructive hover:bg-destructive/10"
                      title="Cerrar Sesión"
                    >
                      <LogOut className="h-3.5 w-3.5" />
                    </Button>
                  )}
                </div>
                {collapsed && (
                   <SidebarMenuButton 
                    tooltip="Cerrar Sesión" 
                    onClick={() => logout()} 
                    className="mt-2 text-muted-foreground hover:text-destructive hover:bg-destructive/10"
                   >
                      <LogOut className="h-4 w-4" />
                   </SidebarMenuButton>
                )}
            </SidebarMenuItem>
         </SidebarMenu>
      </SidebarFooter>
    </Sidebar>
  );
}
