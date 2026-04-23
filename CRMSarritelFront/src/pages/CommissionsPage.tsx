import { useState, useMemo, useCallback, useEffect } from "react";
import { motion } from "framer-motion";
import {
  DollarSign,
  TrendingUp,
  Clock,
  CheckCircle,
  Percent,
  Plus,
  Edit,
  Upload,
  Trash2,
  Search,
  LayoutList,
  Columns3,
  Calendar,
  ArrowRight,
  Building2,
  RefreshCw,
  User,
  Users,
  Shield,
  Briefcase,
  AlertTriangle
} from "lucide-react";
import { AnimatePresence } from "framer-motion";

import { KpiCard } from "@/components/shared/KpiCard";
import { StatusBadge } from "@/components/shared/StatusBadge";
import { EmptyState } from "@/components/shared/EmptyState";
import { useIsMobile } from "@/hooks/use-mobile";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
import { useComisiones } from "@/hooks/useComisiones";
import { usePeriodos } from "@/hooks/usePeriodos";
import { useUsuarios } from "@/hooks/useUsuarios";

import { Comision } from "@/types";
import { ComisionFormModal } from "@/components/comisiones/ComisionFormModal";
import { PeriodoFormModal } from "@/components/comisiones/PeriodoFormModal";
import { BulkImportModal } from "@/components/shared/BulkImportModal";
import { Checkbox } from "@/components/ui/checkbox";
import { Badge } from "@/components/ui/badge";
import { getInsensitive } from "@/lib/import-utils";
import { cn } from "@/lib/utils";
import { UnifiedSearchBar } from "@/components/shared/UnifiedSearchBar";
import { FilterField } from "@/components/shared/FilterField";
import React from 'react';
import { useAuth } from "@/hooks/useAuth";
import { toast } from "sonner";
import { PermissionGuard } from "@/components/auth/PermissionGuard";

// Memoized table row for Commissions
const CommissionRow = React.memo(({
  commission,
  index,
  selectionMode,
  isSelected,
  onSelect,
  onClick,
  isLargeList,
  formatAmount
}: {
  commission: Comision,
  index: number,
  selectionMode: boolean,
  isSelected: boolean,
  onSelect: (id: number, selected: boolean) => void,
  onClick: (c: Comision) => void,
  isLargeList: boolean,
  formatAmount: (n: number) => string
}) => {
  return (
    <tr
      className={cn(
        "border-b border-border hover:bg-muted/50 cursor-pointer transition-colors",
        isSelected && "bg-primary/5"
      )}
      onClick={() => selectionMode ? null : onClick(commission)}
    >
      {selectionMode && (
        <td className="p-4" onClick={(e) => e.stopPropagation()}>
          <Checkbox
            checked={isSelected}
            onCheckedChange={(checked) => onSelect(commission.id, !!checked)}
          />
        </td>
      )}
      <td className="p-4 font-mono text-xs text-muted-foreground">
        <div className="flex items-center gap-2">
          {commission.tipo_Codigo === "MANUAL" && (
            <Badge className="bg-amber-500/10 text-amber-500 border-amber-500/20 px-1.5 h-4 text-[9px] font-black" variant="outline">M</Badge>
          )}
          <span>{commission.venta_Numero || "—"}</span>
        </div>
      </td>
      <td className="p-4 text-foreground">
        <div className="flex flex-col gap-1.5">
          <span className="text-[11px] font-bold uppercase tracking-tight text-foreground/90">
            {commission.proveedorNombre || "Sin Proveedor"}
          </span>
          {commission.productoNombre ? (
            <div className="w-fit px-1.5 py-0.5 rounded bg-primary/10 border border-primary/20 text-[9px] font-black text-primary uppercase shadow-sm">
              {commission.productoNombre}
            </div>
          ) : (
            <span className="text-[9px] text-muted-foreground uppercase font-mono">
              {commission.detalleVentaId ? `Línea #${commission.detalleVentaId}` : (commission.proveedorId ? "Cargo a Proveedor" : "Ajuste Directo")}
            </span>
          )}
        </div>
      </td>
      <td className="p-4 font-medium text-foreground">
        <div className="flex flex-col">
          <span>{commission.vendedorNombre || "—"}</span>
          <span className="text-[10px] text-muted-foreground">Vendedor</span>
        </div>
      </td>
      <td className="p-4 font-medium text-foreground">
        <div className="flex flex-col">
          <span>{commission.empleadoNombre || "ID: " + commission.empleadoId}</span>
          <span className="text-[10px] text-muted-foreground">Beneficiario</span>
        </div>
      </td>
      <td className="p-4 text-muted-foreground">{commission.periodo}</td>
      <td className="p-4 font-mono font-bold text-primary">{formatAmount(commission.montoComision)}</td>
      <td className="p-4"><StatusBadge status={commission.estado_Nombre || 'Inactiva'} /></td>
      <td className="p-4 text-muted-foreground text-[10px]">{new Date(commission.createdAt).toLocaleDateString()}</td>
    </tr>
  );
});

function LiquidationModal({
  commission,
  open,
  onClose,
  onLiquidar,
  hasPermission,
  onEdit,
  activeTab
}: {
  commission: Comision | null;
  open: boolean;
  onClose: () => void;
  onLiquidar: () => void;
  hasPermission: (p: string) => boolean;
  onEdit?: () => void;
  activeTab: string;
}) {
  if (!commission) return null;
  const formatAmount = (n: number) => `${n.toLocaleString("es-ES")} €`;

  return (
    <Dialog open={open} onOpenChange={(o) => !o && onClose()}>
      <DialogContent className="glass border-border max-w-md">
        <DialogHeader><DialogTitle className="text-foreground">Liquidación de Comisión</DialogTitle></DialogHeader>
        <div className="space-y-4 mt-2">
          <div className="glass rounded-lg p-5 border border-dashed border-border space-y-4">
            <div className="text-center">
              <p className="text-xs text-muted-foreground uppercase tracking-widest">CRMSarritel</p>
              <p className="text-xs text-muted-foreground">Recibo de Comisión</p>
            </div>
            <Separator className="bg-white/10" />
            <div className="space-y-2 text-sm">
              <div className="flex justify-between"><span className="text-muted-foreground">Beneficiario</span><span className="text-foreground font-medium">{commission.empleadoNombre || "Empleado ID " + commission.empleadoId}</span></div>
              <div className="flex justify-between"><span className="text-muted-foreground">Venta Ref.</span><span className="text-foreground font-mono">{commission.venta_Numero || "—"}</span></div>
              <div className="flex justify-between"><span className="text-muted-foreground">Periodo</span><span className="text-foreground">{commission.periodo || "—"}</span></div>
              <div className="flex justify-between"><span className="text-muted-foreground">Tipo</span><span className="text-foreground">{commission.tipo_Nombre || "Estándar"}</span></div>
            </div>
            <Separator className="bg-white/10" />
            <div className="space-y-2 text-sm">
              {activeTab === "admin" && (
                <>
                  <div className="flex justify-between"><span className="text-muted-foreground">Base Cálculo</span><span className="text-foreground font-mono">{formatAmount(commission.baseCalculo)}</span></div>
                  {commission.tasaPorcentaje && <div className="flex justify-between"><span className="text-muted-foreground">Porcentaje</span><span className="text-foreground font-mono">{commission.tasaPorcentaje}%</span></div>}
                </>
              )}
              {commission.montoFijo && <div className="flex justify-between"><span className="text-muted-foreground">Monto Fijo</span><span className="text-foreground font-mono">{formatAmount(commission.montoFijo)}</span></div>}
              <div className="flex justify-between text-lg font-bold"><span className="text-foreground">Comisión</span><span className="text-primary font-mono">{formatAmount(commission.montoComision)}</span></div>
            </div>
            <Separator className="bg-white/10" />
            <div className="flex justify-between text-xs">
              <span className="text-muted-foreground">Estado</span>
              <StatusBadge status={commission.estado_Nombre || "Pendiente"} />
            </div>
          </div>
          <div className="flex gap-2">
            {commission.estado_EsPagable && hasPermission("comisiones:update") && (
              <Button
                className="flex-1 bg-primary text-primary-foreground hover:bg-primary/90 font-bold gap-2 shadow-glow"
                onClick={onLiquidar}
              >
                <CheckCircle className="h-4 w-4" /> Liquidar
              </Button>
            )}
            {hasPermission("comisiones:update") && onEdit && (
              <Button variant="outline" className="flex-1 border-primary/20 text-primary font-bold gap-2" onClick={onEdit}>
                <Edit className="h-4 w-4" /> Editar
              </Button>
            )}
          </div>
        </div>
      </DialogContent>
    </Dialog>
  );
}

function PeriodoManagerModal({
  open,
  onClose,
  periodos,
  onSetPrincipal,
  onRecalculate,
  onRecalculateAll,
  isRecalculating,
  isSettingPrincipal,
  onNewPeriod
}: {
  open: boolean;
  onClose: () => void;
  periodos: any[];
  onSetPrincipal: (id: number) => void;
  onRecalculate: (id: number) => void;
  onRecalculateAll: () => void;
  isRecalculating: boolean;
  isSettingPrincipal: boolean;
  onNewPeriod: () => void;
}) {
  return (
    <Dialog open={open} onOpenChange={(o) => !o && onClose()}>
      <DialogContent className="glass border-border max-w-2xl p-0 overflow-hidden">
        <DialogHeader className="p-6 pb-2">
          <div className="flex justify-between items-center w-full">
            <DialogTitle className="text-foreground flex items-center gap-2">
              <div className="h-8 w-8 rounded-lg bg-primary/10 flex items-center justify-center">
                <Calendar className="h-5 w-5 text-primary" />
              </div>
              Gestión de Marcos Temporales
            </DialogTitle>
            <div className="flex gap-2">
              <Button size="sm" variant="outline" onClick={onRecalculateAll} className="gap-2 font-bold border-primary/20 text-primary" disabled={isRecalculating}>
                <RefreshCw className={cn("h-4 w-4", isRecalculating && "animate-spin")} />
                Recalcular Todo
              </Button>
              <Button size="sm" onClick={onNewPeriod} className="gap-2 font-bold shadow-glow">
                <Plus className="h-4 w-4" /> Nuevo Periodo
              </Button>
            </div>
          </div>
        </DialogHeader>

        <div className="p-6 pt-2 space-y-6">
          <div className="glass rounded-xl border border-border/50 overflow-hidden shadow-xl">
            <table className="w-full text-sm">
              <thead>
                <tr className="border-b border-border bg-muted/30 text-muted-foreground uppercase tracking-widest text-[10px] font-black">
                  <th className="p-4 text-left">Periodo</th>
                  <th className="p-4 text-left">Rango</th>
                  <th className="p-4 text-center">Estado Principal</th>
                  <th className="p-4 text-right">Mantenimiento</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-border/20">
                {periodos.map((p) => (
                  <tr key={p.id} className="hover:bg-primary/5 transition-all group">
                    <td className="p-4">
                      <div className="flex flex-col">
                        <span className="font-bold text-foreground">{p.nombre}</span>
                        <span className="text-[10px] text-muted-foreground uppercase tracking-tighter">ID: {p.id}</span>
                      </div>
                    </td>
                    <td className="p-4">
                      <div className="flex items-center gap-2 text-xs text-muted-foreground font-mono">
                        {new Date(p.fechaInicio).toLocaleDateString()}
                        <ArrowRight className="h-3 w-3 opacity-30" />
                        {new Date(p.fechaFin).toLocaleDateString()}
                      </div>
                    </td>
                    <td className="p-4">
                      <div className="flex justify-center">
                        {p.esPrincipal ? (
                          <Badge className="bg-primary/20 text-primary border-primary/30 hover:bg-primary/30 gap-1.5 px-3 py-1">
                            <span className="w-1.5 h-1.5 rounded-full bg-primary animate-pulse" />
                            ACTIVO
                          </Badge>
                        ) : (
                          <Button
                            variant="ghost"
                            size="sm"
                            className="h-8 px-4 text-[10px] font-bold border border-transparent hover:border-primary/20 hover:bg-primary/5 opacity-40 hover:opacity-100"
                            onClick={() => onSetPrincipal(p.id)}
                            disabled={isSettingPrincipal}
                          >
                            MARCAR PRINCIPAL
                          </Button>
                        )}
                      </div>
                    </td>
                    <td className="p-4 text-right">
                      <Button
                        variant="secondary"
                        size="sm"
                        className={cn(
                          "h-8 gap-2 font-bold shadow-sm transition-all border border-border/50",
                          isRecalculating ? "opacity-50" : "hover:bg-primary hover:text-primary-foreground hover:border-primary"
                        )}
                        onClick={() => onRecalculate(p.id)}
                        disabled={isRecalculating}
                      >
                        <RefreshCw className={cn("h-3.5 w-3.5", isRecalculating && "animate-spin")} />
                        Recalcular
                      </Button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          <div className="flex justify-between items-center bg-muted/10 p-4 rounded-xl border border-border/50">
            <p className="text-[10px] text-muted-foreground max-w-[300px] leading-relaxed">
              * El periodo marcado como <strong>Principal</strong> sirve de filtro predeterminado para todos los cálculos de comisiones y KPIs globales.
            </p>
            <Button variant="outline" onClick={onClose} className="border-border shadow-sm font-bold bg-background/50">Cerrar Gestión</Button>
          </div>
        </div>
      </DialogContent>
    </Dialog>
  );
}

export default function CommissionsPage() {
  const { user, hasPermission } = useAuth();
  const isAdmin = hasPermission("comisiones:view_all");

  const {
    comisiones = [],
    isLoading,
    liquidarComision,
    createComision,
    deleteComision,
    recalculatePeriod,
    isRecalculating,
    recalculateAll
  } = useComisiones();

  const {
    periodos = [],
    setPrincipal,
    isSettingPrincipal,
    recalculate
  } = usePeriodos();

  const { data: users = [] } = useUsuarios();

  const [periodoManagerOpen, setPeriodoManagerOpen] = useState(false);

  const handleRecalculateAll = async () => {
    if (!confirm("¿Deseas realizar una LIMPIEZA TOTAL y recálculo? Esto borrará todas las comisiones actuales (incluyendo ajustes manuales) para regenerarlas desde cero según las reglas actuales. Esta acción no se puede deshacer.")) return;
    await recalculateAll(true);
  };
  const [newPeriodoOpen, setNewPeriodoOpen] = useState(false);
  const [selectedIds, setSelectedIds] = useState<Set<number>>(new Set());
  const [selectionMode, setSelectionMode] = useState(false);
  const [view, setView] = useState<"table" | "grid">("table");
  const [selectedCommission, setSelectedCommission] = useState<Comision | null>(null);
  const [formOpen, setFormOpen] = useState(false);
  const [importModalOpen, setImportModalOpen] = useState(false);
  const [editData, setEditData] = useState<Comision | undefined>();
  const [displayLimit, setDisplayLimit] = useState(50);

  // --- NEW: Granular Permissions for Views ---
  const canViewPersonal = hasPermission("comisiones:view_personal") || isAdmin;
  const canViewGeneral = hasPermission("comisiones:view_users") || isAdmin;
  const canViewAdmin = hasPermission("comisiones:view_admin") || isAdmin;

  // Helper status checkers
  const isActive = useCallback((c: Comision) => c.estado_Nombre?.toUpperCase() === "ACTIVA" || c.estado_Nombre?.toUpperCase() === "PAGADA", []);
  const isInactive = useCallback((c: Comision) => !isActive(c), [isActive]);

  const [activeTab, setActiveTab] = useState<"personal" | "general" | "admin">("personal");
  const [search, setSearch] = useState("");
  const principalPeriodo = useMemo(() => periodos.find(p => p.esPrincipal), [periodos]);
  const [periodoFilter, setPeriodoFilter] = useState("all");
  const rawUserId = (user as any)?.id || (user as any)?.Id;
  const [empleadoFilter, setEmpleadoFilter] = useState(isAdmin ? "all" : String(rawUserId || ""));
  const [estadoFilter, setEstadoFilter] = useState("all");

  // Reset filters on tab switch to isolate views
  useEffect(() => {
    setSearch("");
    setEstadoFilter("all");
    setPeriodoFilter("all");

    if (activeTab === "personal") {
      setEmpleadoFilter(String(rawUserId || ""));
    } else {
      setEmpleadoFilter("all");
    }
  }, [activeTab, principalPeriodo?.nombre, rawUserId]);

  // Auto-select the best available tab on mount
  useEffect(() => {
    if (canViewAdmin) setActiveTab("admin");
    else if (canViewGeneral) setActiveTab("general");
    else if (canViewPersonal) setActiveTab("personal");
  }, [canViewAdmin, canViewGeneral, canViewPersonal]);

  const isMobile = useIsMobile();
  const formatAmount = useCallback((n: number) => `${n.toLocaleString("es-ES")} €`, []);

  // Auto-switch tab if user is not admin but somehow reached here (guards)
  useEffect(() => {
    if (!isAdmin && activeTab !== "personal") {
      setActiveTab("personal");
    }
  }, [isAdmin, activeTab]);

  const filtered = useMemo(() => {
    if (!Array.isArray(comisiones)) return [];
    
    // Extraer el ID de forma robusta (PascalCase o camelCase)
    const rawId = (user as any)?.id || (user as any)?.Id;
    const myId = rawId ? Number(rawId) : NaN;

    return comisiones.filter(c => {
      // 1. Tab Filtering (Core Logic)
      if (activeTab === "personal") {
        if (c.empleadoId === 99) return false;

        const isMine = c.empleadoId === myId;

        // En personal, siempre aplicamos el filtro de empleado si está activo
        if (isAdmin || hasPermission("comisiones:view_all")) {
          const beneficiary = users.find(u => u.id === c.empleadoId);
          const isSubordinate = beneficiary?.superiorId === myId;

          if (!isMine && !isSubordinate) return false;
        } else {
          if (!isMine) return false;
        }
      } else if (activeTab === "admin") {
        // En Admin permitimos ver todo para que los KPIs de Payout vs Profit funcionen
      } else if (activeTab === "general") {
        if (c.empleadoId === 99) return false;
      }

      const matchSearch = search === "" ||
        (c.venta_Numero || "").toLowerCase().includes(search.toLowerCase()) ||
        (c.empleadoNombre || "").toLowerCase().includes(search.toLowerCase()) ||
        (c.periodo || "").toLowerCase().includes(search.toLowerCase());

      const matchPeriodo = periodoFilter === "all" || c.periodo === periodoFilter;

      // El match de empleado solo se aplica en General o Admin (en Personal ya está filtrado arriba)
      const matchEmpleado = activeTab === "personal" ? true : (empleadoFilter === "all" || String(c.empleadoId) === empleadoFilter);

      let matchEstado = true;
      if (estadoFilter === "ACTIVA") {
        matchEstado = isActive(c);
      } else if (estadoFilter === "INACTIVA") {
        matchEstado = isInactive(c);
      } else if (estadoFilter !== "all") {
        matchEstado = c.estado_Nombre === estadoFilter;
      }

      return matchSearch && matchPeriodo && matchEmpleado && matchEstado;
    });
  }, [comisiones, search, periodoFilter, empleadoFilter, estadoFilter, activeTab, users, isAdmin, user?.id, hasPermission]);

  const totalGenerated = useMemo(() => filtered.reduce((s, c) => s + (c?.montoComision || 0), 0), [filtered]);
  const totalGrossOrg = useMemo(() => filtered.reduce((s, c) => s + (c?.baseCalculo || 0), 0), [filtered]);
  const avgMargin = useMemo(() => totalGrossOrg > 0 ? ((totalGrossOrg - totalGenerated) / totalGrossOrg) * 100 : 0, [totalGrossOrg, totalGenerated]);

  const kpis = useMemo(() => {
    // isActive ya está definido fuera

    // Si estamos en la vista de Admin (Empresa), mostramos Profit Organizational
    if (activeTab === "admin") {
      // Usamos ventas únicas para calcular la base bruta total real (para no triplicar si hay splits)
      const uniqueSalesKeys = Array.from(new Set(filtered.map(c => 
          c.detalleVentaId ? `DV_${c.detalleVentaId}` : (c.ventaId ? `V_${c.ventaId}` : `E_${c.id}`)
      )));
      
      let totalGrossBase = 0;
      let activeGrossBase = 0;
      uniqueSalesKeys.forEach(uKey => {
          const sample = filtered.find(c => (c.detalleVentaId ? `DV_${c.detalleVentaId}` : (c.ventaId ? `V_${c.ventaId}` : `E_${c.id}`)) === uKey);
          if (sample) {
              totalGrossBase += (sample.baseCalculo || 0);
              if (isActive(sample)) activeGrossBase += (sample.baseCalculo || 0);
          }
      });

      const beneficioOrg = filtered
        .filter(c => c.empleadoId === 99 && isActive(c))
        .reduce((s, c) => s + (c.montoComision || 0), 0);

      // Comisiones activas debidas a comerciales (lo que realmente se ha pagado o se va a pagar)
      const activeComercialLiability = filtered
        .filter(c => c.empleadoId !== 99 && isActive(c))
        .reduce((s, c) => s + (c.montoComision || 0), 0);
        
      const comisionesExtra = filtered
        .filter(c => c.esExtra && isActive(c))
        .reduce((s, c) => s + (c.montoComision || 0), 0);

      const totalInactivasOrg = filtered
        .filter(c => isInactive(c) && c.empleadoId === 99)
        .reduce((s, c) => s + (c.montoComision || 0), 0);

      const performanceRatioOrg = totalGrossBase > 0 ? (activeGrossBase / totalGrossBase) * 100 : 0;
      
      const totalActivoComisiones = beneficioOrg + activeComercialLiability;

      const margenNeto = totalActivoComisiones > 0 
        ? (beneficioOrg / totalActivoComisiones) * 100 
        : 0;

      return [
        { title: "Beneficio Org.", value: formatAmount(beneficioOrg), icon: Building2, change: "Activo Sistema" },
        { title: "Comisiones Pagadas", value: formatAmount(activeComercialLiability), icon: DollarSign, change: "Comerciales Activos" },
        { title: "Comisiones Extra", value: formatAmount(comisionesExtra), icon: Plus, change: "Extras Activos" },
        { title: "Margen Bruto", value: `${margenNeto.toFixed(1)}%`, icon: TrendingUp, change: "Ratio Bnf/Total" },
        { title: "Pendiente Org.", value: formatAmount(totalInactivasOrg), icon: Clock, change: "Por activar" },
        { title: "% Éxito Org.", value: `${performanceRatioOrg.toFixed(1)}%`, icon: Percent, change: "Ratio Global" },
      ];
    }

    // --- VISTAS PERSONAL / GENERAL ---
    const myId = rawUserId ? Number(rawUserId) : NaN;
    const targetUserId = empleadoFilter !== "all" && empleadoFilter !== "" ? Number(empleadoFilter) : myId;

    // Segmentamos las comisiones del set filtrado actual
    const currentComms = filtered.filter(c => c.empleadoId !== 99);

    // Si el filtro es 'all' (vista general), sumamos TODO el set filtrado actual
    const isGeneralView = empleadoFilter === "all" || empleadoFilter === "";

    const personalDirectComms = currentComms.filter(c =>
      (isGeneralView ? true : c.empleadoId === targetUserId) &&
      c.vendedorId === c.empleadoId &&
      c.tipo_Codigo !== "GESTION_EQUIPO"
    );

    const personalTeamComms = currentComms.filter(c =>
      (isGeneralView ? true : (c.empleadoId === targetUserId && (c.vendedorId !== c.empleadoId || c.tipo_Codigo === "GESTION_EQUIPO"))) &&
      (c.vendedorId !== c.empleadoId || c.tipo_Codigo === "GESTION_EQUIPO")
    );

    const activePersonal = personalDirectComms.filter(isActive);
    const inactivePersonal = personalDirectComms.filter(isInactive);
    const inactiveTeam = personalTeamComms.filter(isInactive);

    const sumActiveP = activePersonal.reduce((s, c) => s + (c.montoComision || 0), 0);
    const sumInactiveP = inactivePersonal.reduce((s, c) => s + (c.montoComision || 0), 0);
    const sumActiveTeam = personalTeamComms.filter(isActive).reduce((s, c) => s + (c.montoComision || 0), 0);
    const sumInactiveTeam = inactiveTeam.reduce((s, c) => s + (c.montoComision || 0), 0);

    // Cálculo de Rendimiento Global del Filtro: Usamos ventas únicas para no triplicar la base si hay splits
    const uniqueSales = Array.from(new Set(filtered.map(c =>
      c.detalleVentaId ? `DV_${c.detalleVentaId}` : (c.ventaId ? `V_${c.ventaId}` : `E_${c.id}`)
    )));

    let totalBaseFilter = 0;
    let activeBaseFilter = 0;

    uniqueSales.forEach(uKey => {
      // Buscamos una comisión de este grupo (todas comparten la misma base y estado de venta)
      const sample = filtered.find(c => {
        const currentKey = c.detalleVentaId ? `DV_${c.detalleVentaId}` : (c.ventaId ? `V_${c.ventaId}` : `E_${c.id}`);
        return currentKey === uKey;
      });
      if (sample) {
        totalBaseFilter += (sample.baseCalculo || 0);
        if (isActive(sample)) {
          activeBaseFilter += (sample.baseCalculo || 0);
        }
      }
    });

    const performanceRatio = totalBaseFilter > 0 ? (activeBaseFilter / totalBaseFilter) * 100 : 0;

    const isCurrentMe = targetUserId === myId;

    return [
      {
        title: "Comisiones Activas",
        value: formatAmount(sumActiveP + sumActiveTeam),
        icon: DollarSign,
        change: `Total del filtro actual`
      },
      {
        title: "Pendiente Activar",
        value: formatAmount(sumInactiveP + sumInactiveTeam),
        icon: Clock,
        change: "En revisión / No activas"
      },
      {
        title: "% Éxito Ventas",
        value: `${performanceRatio.toFixed(1)}%`,
        icon: Percent,
        change: "Ratio de activación"
      },
      {
        title: "Monto Gestión",
        value: formatAmount(sumActiveTeam),
        icon: Users,
        change: "Derivadas de equipo / gestión"
      },
    ];
  }, [filtered, activeTab, empleadoFilter, user?.id, formatAmount]);

  const handleSelect = useCallback((id: number, selected: boolean) => {
    setSelectedIds(prev => {
      const next = new Set(prev);
      if (selected) next.add(id);
      else next.delete(id);
      return next;
    });
  }, []);

  const displayCommissions = useMemo(() => {
    if (activeTab === "admin" && empleadoFilter === "all") {
        return filtered.filter(c => c.empleadoId === 99);
    }
    return filtered;
  }, [filtered, activeTab, empleadoFilter]);

  const handleSelectAll = useCallback((checked: boolean) => {
    if (checked) setSelectedIds(new Set(displayCommissions.map(c => c.id)));
    else setSelectedIds(new Set());
  }, [displayCommissions]);

  const handleBulkDelete = async () => {
    if (selectedIds.size === 0) return;
    if (!confirm(`¿Estás seguro de que deseas eliminar ${selectedIds.size} comisiones?`)) return;

    const idsArray = Array.from(selectedIds);
    const toastId = toast.loading(`Eliminando...`);
    try {
      for (const id of idsArray) {
        await deleteComision(id);
      }
      setSelectedIds(new Set());
      setSelectionMode(false);
      toast.success("Comisiones eliminadas", { id: toastId });
    } catch {
      toast.error("Error al eliminar", { id: toastId });
    }
  };

  const handleLiquidar = async () => {
    if (!selectedCommission) return;
    try {
      await liquidarComision(selectedCommission.id);
      toast.success("Comisión liquidada");
      setSelectedCommission(null);
    } catch {
      toast.error("Error al liquidar");
    }
  };


  if (isLoading) return <div className="p-12 text-center animate-pulse">Cargando financiera...</div>;

  return (
    <div className="space-y-6 pb-20 sm:pb-6">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl font-bold text-foreground">Financiera</h1>
          <p className="text-sm text-muted-foreground">
            {activeTab === "personal" && "Mis ingresos y rendimiento del equipo"}
            {activeTab === "general" && "Auditoría general de comisiones y usuarios"}
            {activeTab === "admin" && "Rendimiento organizacional (Gross vs Profit)"}
          </p>
        </div>
        <div className="flex items-center gap-2">
          {hasPermission("comisiones:update") && (
            <>
              <Button 
                variant="outline" 
                className="border-border gap-2 text-amber-500 hover:text-amber-600" 
                onClick={handleRecalculateAll}
                disabled={isRecalculating}
              >
                <RefreshCw className={cn("h-4 w-4", isRecalculating && "animate-spin")} />
                {isRecalculating ? "Recalculando..." : "Recalcular Todo"}
              </Button>
              <Button variant="outline" className="border-border gap-2" onClick={() => setPeriodoManagerOpen(true)}>
                <Calendar className="h-4 w-4" /> Gestión Periodos
              </Button>
            </>
          )}
          {hasPermission("comisiones:create") && (
            <Button className="bg-primary shadow-glow font-bold" onClick={() => setFormOpen(true)}>
              <Plus className="h-4 w-4" /> Nueva
            </Button>
          )}
        </div>
      </div>

      {/* Vistas Tab Switcher */}
      {/* Vistas Tab Switcher - Now with granular permissions */}
      {(canViewPersonal || canViewGeneral || canViewAdmin) && (
        <div className="flex p-1 bg-muted/30 border border-border/50 rounded-xl w-fit">
          {canViewPersonal && (
            <Button
              variant={activeTab === "personal" ? "secondary" : "ghost"}
              size="sm"
              onClick={() => setActiveTab("personal")}
              className={cn("gap-2 h-9 px-4 rounded-lg font-bold transition-all", activeTab === "personal" && "shadow-sm bg-background")}
            >
              <User className="h-4 w-4" /> Personal / Equipo
            </Button>
          )}
          {canViewGeneral && (
            <Button
              variant={activeTab === "general" ? "secondary" : "ghost"}
              size="sm"
              onClick={() => setActiveTab("general")}
              className={cn("gap-2 h-9 px-4 rounded-lg font-bold transition-all", activeTab === "general" && "shadow-sm bg-background")}
            >
              <Users className="h-4 w-4" /> General
            </Button>
          )}
          {canViewAdmin && (
            <Button
              variant={activeTab === "admin" ? "secondary" : "ghost"}
              size="sm"
              onClick={() => setActiveTab("admin")}
              className={cn("gap-2 h-9 px-4 rounded-lg font-bold transition-all", activeTab === "admin" && "shadow-sm bg-background text-primary")}
            >
              <Shield className="h-4 w-4" /> Admin (Empresa)
            </Button>
          )}
        </div>
      )}

      <div className="grid gap-4 grid-cols-2 md:grid-cols-4">
        {kpis.map((kpi, i) => (
          <KpiCard key={kpi.title} {...kpi} index={i} />
        ))}
      </div>

      <UnifiedSearchBar
        selectionMode={selectionMode}
        onSelectionModeToggle={hasPermission("comisiones:delete") ? () => setSelectionMode(!selectionMode) : undefined}
        searchValue={search}
        onSearchChange={setSearch}
        onClearFilters={() => {
          setSearch("");
          setPeriodoFilter(principalPeriodo?.nombre || "all");
          setEmpleadoFilter(isAdmin ? "all" : String(user?.id));
          setEstadoFilter("all");
        }}
        showFilterBadge={periodoFilter !== "all" || empleadoFilter !== (isAdmin ? "all" : String(user?.id)) || estadoFilter !== "all"}
        bulkActions={
          selectionMode && (
            <Button variant="destructive" size="sm" onClick={handleBulkDelete} disabled={selectedIds.size === 0}>
              Eliminar ({selectedIds.size})
            </Button>
          )
        }
      >
        <div className="flex flex-wrap items-end gap-4 w-full">
          <FilterField
            columnLabel="Periodo / Mes"
            type="select"
            value={periodoFilter}
            onChange={setPeriodoFilter}
            options={periodos.map(p => ({ value: p.nombre, label: p.nombre }))}
          />

          {(activeTab === "general" || activeTab === "admin") && (
            <PermissionGuard permission="comisiones:view_all">
              <FilterField
                columnLabel="Vendedor / Beneficiario"
                type="select"
                value={empleadoFilter}
                onChange={setEmpleadoFilter}
                options={users
                  .filter(u => activeTab === 'admin' ? true : u.id !== 99)
                  .map(u => ({ value: String(u.id), label: u.nombre }))}
              />
            </PermissionGuard>
          )}

          <FilterField
            columnLabel="Estado"
            type="select"
            value={estadoFilter}
            onChange={setEstadoFilter}
            options={[
              { value: "ACTIVA", label: "Activas (Pagadas)" },
              { value: "INACTIVA", label: "Inactivas (Pendientes)" }
            ]}
          />
        </div>
      </UnifiedSearchBar>

      <div className="glass rounded-xl overflow-hidden border border-border/50">
        <div className="overflow-x-auto">
          <table className="w-full text-sm min-w-[900px]">
            <thead className="bg-muted/20 border-b border-border">
              <tr>
                {selectionMode && <th className="p-4 w-10"><Checkbox checked={selectedIds.size === displayCommissions.length && displayCommissions.length > 0} onCheckedChange={handleSelectAll} /></th>}
                <th className="text-left p-4">Ref. Venta</th>
                <th className="text-left p-4">Proveedor / Producto</th>
                <th className="text-left p-4">Vendedor</th>
                <th className="text-left p-4">Beneficiario</th>
                <th className="text-left p-4">Periodo</th>
                <th className="text-right p-4">Monto</th>
                <th className="text-left p-4">Estado</th>
                <th className="text-left p-4">Fecha</th>
              </tr>
            </thead>
            <tbody>
              {displayCommissions.slice(0, displayLimit).map((c, i) => (
                <CommissionRow
                  key={`${c.id}-${i}`}
                  commission={c}
                  index={i}
                  selectionMode={selectionMode}
                  isSelected={selectedIds.has(c.id)}
                  onSelect={handleSelect}
                  onClick={setSelectedCommission}
                  formatAmount={formatAmount}
                  isLargeList={filtered.length > 50}
                />
              ))}
            </tbody>
          </table>
        </div>
      </div>

      <LiquidationModal
        commission={selectedCommission}
        open={!!selectedCommission && !formOpen}
        onClose={() => setSelectedCommission(null)}
        onLiquidar={handleLiquidar}
        hasPermission={hasPermission}
        activeTab={activeTab}
        onEdit={() => {
          setEditData(selectedCommission);
          setFormOpen(true);
          setSelectedCommission(null);
        }}
      />

      <ComisionFormModal open={formOpen} onClose={() => { setFormOpen(false); setEditData(undefined); }} initialData={editData} />
      <PeriodoManagerModal 
        open={periodoManagerOpen} 
        onClose={() => setPeriodoManagerOpen(false)} 
        periodos={periodos} 
        onSetPrincipal={setPrincipal} 
        onRecalculate={recalculatePeriod} 
        onRecalculateAll={handleRecalculateAll}
        isRecalculating={isRecalculating} 
        isSettingPrincipal={isSettingPrincipal} 
        onNewPeriod={() => {
          setPeriodoManagerOpen(false);
          setNewPeriodoOpen(true);
        }} 
      />
      <PeriodoFormModal open={newPeriodoOpen} onClose={() => setNewPeriodoOpen(false)} />
    </div>
  );
}
