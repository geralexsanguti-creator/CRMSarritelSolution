import { cn } from "@/lib/utils";

const statusColors: Record<string, string> = {
  Lead: "bg-status-lead/20 text-status-lead border-status-lead/30",
  Activo: "bg-emerald-500/20 text-emerald-400 border-emerald-500/30",
  Inactivo: "bg-amber-500/20 text-amber-400 border-amber-500/30",
  Activa: "bg-emerald-500/20 text-emerald-400 border-emerald-500/30",
  Inactiva: "bg-amber-500/20 text-amber-400 border-amber-500/30",
  ACTIVA: "bg-emerald-500/20 text-emerald-400 border-emerald-500/30",
  INACTIVA: "bg-amber-500/20 text-amber-400 border-amber-500/30",
  Pendiente: "bg-status-pending/20 text-status-pending border-status-pending/30",
  Completada: "bg-status-completed/20 text-status-completed border-status-completed/30",
  Cancelada: "bg-status-cancelled/20 text-status-cancelled border-status-cancelled/30",
  "Pendiente de Pago": "bg-status-pending/20 text-status-pending border-status-pending/30",
  Pagada: "bg-status-paid/20 text-status-paid border-status-paid/30",
  Anulada: "bg-status-voided/20 text-status-voided border-status-voided/30",
  "En Proceso": "bg-blue-500/20 text-blue-400 border-blue-500/30",
  EMPRESA: "bg-primary/20 text-primary border-primary/30",
  PARTICULAR: "bg-muted text-muted-foreground border-border",
  Penalizado: "bg-destructive/20 text-destructive border-destructive/30",
  Firmado: "bg-status-completed/20 text-status-completed border-status-completed/30",
  "Pendiente Firma": "bg-status-pending/20 text-status-pending border-status-pending/30",
  Borrador: "bg-muted text-muted-foreground border-border",
};

const dotColors: Record<string, string> = {
  Lead: "bg-status-lead",
  Activo: "bg-emerald-400",
  Inactivo: "bg-amber-400",
  Activa: "bg-emerald-400",
  Inactiva: "bg-amber-400",
  ACTIVA: "bg-emerald-400",
  INACTIVA: "bg-amber-400",
  Pendiente: "bg-status-pending",
  Completada: "bg-status-completed",
  Cancelada: "bg-status-cancelled",
  "Pendiente de Pago": "bg-status-pending",
  Pagada: "bg-status-paid",
  Anulada: "bg-status-voided",
  "En Proceso": "bg-blue-400",
  EMPRESA: "bg-primary",
  PARTICULAR: "bg-muted-foreground",
  Penalizado: "bg-destructive",
  Firmado: "bg-status-completed",
  "Pendiente Firma": "bg-status-pending",
  Borrador: "bg-muted-foreground",
};

export function StatusBadge({ status, colorClass }: { status: string, colorClass?: string }) {
  const finalClass = colorClass || statusColors[status] || "bg-muted text-muted-foreground border-border";
  return (
    <span
      className={cn(
        "inline-flex items-center px-2.5 py-0.5 rounded-full text-[10px] font-bold border uppercase tracking-wider",
        finalClass
      )}
    >
      <span className={cn("w-1.5 h-1.5 rounded-full mr-1.5 opacity-70", dotColors[status] || (colorClass ? "bg-current" : "bg-muted-foreground"))} />
      {status}
    </span>
  );
}
