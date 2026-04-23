import { Bell } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { Badge } from "@/components/ui/badge";
import { ScrollArea } from "@/components/ui/scroll-area";
import { useState } from "react";
import { cn } from "@/lib/utils";
import { useNotifications } from "@/hooks/useNotifications";
import { formatDistanceToNow } from "date-fns";
import { es } from "date-fns/locale";

export interface Notification {
  id: string;
  title: string;
  description: string;
  time: string;
  read: boolean;
  type: "success" | "info" | "warning" | "error";
}

export function NotificationBell() {
  const { notifications, unreadCount, markAsRead, markAllAsRead } = useNotifications();

  return (
    <Popover>
      <PopoverTrigger asChild>
        <Button variant="ghost" size="icon" className="relative text-muted-foreground hover:text-foreground">
          <Bell className="h-5 w-5" />
          {unreadCount > 0 && (
             <Badge className="absolute -top-1 -right-1 h-4 min-w-[16px] px-1 flex items-center justify-center bg-primary text-[10px] text-primary-foreground border-2 border-background">
               {unreadCount}
             </Badge>
          )}
        </Button>
      </PopoverTrigger>
      <PopoverContent className="w-80 p-0 glass border-border mt-2" align="end">
        <div className="flex items-center justify-between p-4 border-b border-border">
          <h3 className="font-semibold text-sm text-foreground">Notificaciones</h3>
          {unreadCount > 0 && (
            <button onClick={markAllAsRead} className="text-[10px] text-primary hover:underline font-medium">
              Marcar todo como leído
            </button>
          )}
        </div>
        <ScrollArea className="h-80">
          <div className="flex flex-col">
            {notifications.length === 0 ? (
              <div className="p-8 text-center text-muted-foreground text-sm">
                No tienes notificaciones
              </div>
            ) : (
              notifications.map((n) => (
                <div 
                  key={n.id} 
                  onClick={() => markAsRead(n.id)}
                  className={cn(
                    "p-4 border-b border-border/50 cursor-pointer transition-colors hover:bg-muted/30",
                    !n.read && "bg-primary/5 border-l-2 border-l-primary"
                  )}
                >
                    <div className="flex justify-between items-start gap-2">
                      <p className={cn("text-xs font-semibold", n.read ? "text-foreground/70" : "text-foreground")}>{n.title}</p>
                      <span className="text-[10px] text-muted-foreground whitespace-nowrap">
                        {formatDistanceToNow(new Date(n.time), { addSuffix: true, locale: es })}
                      </span>
                    </div>
                  <p className="text-[11px] text-muted-foreground mt-1 line-clamp-2">{n.description}</p>
                </div>
              ))
            )}
          </div>
        </ScrollArea>
        <div className="p-2 border-t border-border bg-muted/20">
          <Button variant="ghost" className="w-full h-8 text-xs text-muted-foreground hover:text-foreground">
            Ver todas las notificaciones
          </Button>
        </div>
      </PopoverContent>
    </Popover>
  );
}
