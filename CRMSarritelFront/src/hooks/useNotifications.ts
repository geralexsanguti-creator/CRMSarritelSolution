import { useState, useEffect, useCallback } from "react";
import { toast } from "sonner";
import apiClient from "@/lib/axios";
import { Comision, Venta } from "@/types";

export interface Notification {
  id: string;
  title: string;
  description: string;
  time: Date;
  read: boolean;
  type: "success" | "info" | "warning" | "error";
  link?: string;
}

// Singleton state for notifications across the app session
let globalNotifications: Notification[] = [];
const listeners: ((n: Notification[]) => void)[] = [];

function notifyListeners() {
  listeners.forEach(l => l([...globalNotifications]));
}

export function useNotifications() {
  const [notifications, setInternalNotifications] = useState<Notification[]>(globalNotifications);

  useEffect(() => {
    const listener = (n: Notification[]) => setInternalNotifications(n);
    listeners.push(listener);
    return () => {
      const idx = listeners.indexOf(listener);
      if (idx > -1) listeners.splice(idx, 1);
    };
  }, []);

  const addNotification = useCallback((n: Omit<Notification, "id" | "read" | "time">) => {
    const newNotification: Notification = {
      ...n,
      id: Math.random().toString(36).substring(7),
      time: new Date(),
      read: false
    };
    
    globalNotifications = [newNotification, ...globalNotifications].slice(0, 50); // Keep last 50
    notifyListeners();
    
    // Trigger Toast
    toast[n.type](n.title, {
      description: n.description,
    });
  }, []);

  const markAsRead = useCallback((id: string) => {
    globalNotifications = globalNotifications.map(n => n.id === id ? { ...n, read: true } : n);
    notifyListeners();
  }, []);

  const markAllAsRead = useCallback(() => {
    globalNotifications = globalNotifications.map(n => ({ ...n, read: true }));
    notifyListeners();
  }, []);

  // background checking for new sales/commissions (Polling)
  useEffect(() => {
    let lastCheck = new Date();
    
    const checkUpdates = async () => {
      try {
        // Here we could have a specific /Notifications endpoint
        // For now we check Comisiones to see if there are new ones since last check
        const { data: comisiones } = await apiClient.get<Comision[]>('/Comisiones');
        const newComisiones = comisiones.filter(c => new Date(c.createdAt) > lastCheck);
        
        if (newComisiones.length > 0) {
          newComisiones.forEach(c => {
            addNotification({
              title: "Nueva Comisión Generada",
              description: `Se ha registrado una comisión de ${c.montoComision.toLocaleString("es-ES")} € por la venta ${c.venta_Numero || ''}`,
              type: "info"
            });
          });
          lastCheck = new Date();
        }

        const { data: ventas } = await apiClient.get<Venta[]>('/Ventas');
        const newVentas = ventas.filter(v => new Date(v.createdAt) > lastCheck);

        if (newVentas.length > 0) {
            newVentas.forEach(v => {
                addNotification({
                    title: "Nueva Venta Registrada",
                    description: `Venta #${v.numeroVenta} por ${v.montoTotal.toLocaleString("es-ES")} €`,
                    type: "success"
                });
            });
            lastCheck = new Date();
        }
      } catch (e) {
        console.error("Error polling notifications", e);
      }
    };

    const interval = setInterval(checkUpdates, 30000); // Check every 30s
    return () => clearInterval(interval);
  }, [addNotification]);

  return {
    notifications,
    unreadCount: notifications.filter(n => !n.read).length,
    addNotification,
    markAsRead,
    markAllAsRead
  };
}
