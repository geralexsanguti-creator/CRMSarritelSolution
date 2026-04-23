import { motion } from "framer-motion";
import { Users, ShoppingCart, DollarSign, TrendingUp, ArrowUpRight, Package, Medal } from "lucide-react";
import { KpiCard } from "@/components/shared/KpiCard";
import { StatusBadge } from "@/components/shared/StatusBadge";
import { useNavigate } from "react-router-dom";
import { useDashboard } from "@/hooks/useDashboard";
import { DashboardSkeleton } from "@/components/shared/DashboardSkeleton";
import { AlertTriangle } from "lucide-react";

export default function DashboardPage() {
  const navigate = useNavigate();
  const formatAmount = (n: number) => `${n.toLocaleString("es-ES")} €`;

  const { data, isLoading, isError } = useDashboard();

  if (isLoading) return <DashboardSkeleton />;
  if (isError || !data) return (
    <div className="h-[60vh] flex flex-col items-center justify-center glass rounded-xl text-destructive gap-4">
      <AlertTriangle className="h-10 w-10" />
      <p>Error al cargar el dashboard.</p>
    </div>
  );

  const { kpis, recentSales, topSellers } = data;

  const kpiCards = [
    { title: "Clientes", value: String(kpis.totalClientes), icon: Users, sparkData: [3, 5, 4, 6, 5, 7, 8], change: `${kpis.empresas} empresas` },
    { title: "Ventas Totales", value: formatAmount(kpis.totalSalesMonto), icon: ShoppingCart, sparkData: [20, 40, 35, 55, 50, 60, 75], change: "+12% este mes", changePositive: true },
    { title: "Comisiones Pendientes", value: formatAmount(kpis.pendingCommissions), icon: DollarSign, sparkData: [10, 15, 20, 18, 25, 22, 30] },
    { title: "Productos Activos", value: String(kpis.activeProducts), icon: Package, sparkData: [50, 55, 60, 58, 65, 70, 68], change: `${kpis.totalProductos} total` },
  ];

  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Dashboard</h1>
        <p className="text-sm text-muted-foreground">Resumen general de CRMSarritel</p>
      </div>

      <div className="grid grid-cols-2 lg:grid-cols-4 gap-4">
        {kpiCards.map((kpi, i) => (
          <KpiCard key={kpi.title} {...kpi} index={i} />
        ))}
      </div>

      <div className="grid lg:grid-cols-2 gap-6">
        {/* Ventas Recientes */}
        <motion.div initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: 0.3 }} className="glass rounded-xl overflow-hidden">
          <div className="flex items-center justify-between p-4 border-b border-border">
            <h2 className="font-semibold text-foreground text-sm">Ventas Recientes</h2>
            <button onClick={() => navigate("/ventas")} className="text-xs text-primary flex items-center gap-1 hover:underline">
              Ver todas <ArrowUpRight className="h-3 w-3" />
            </button>
          </div>
          <div className="divide-y divide-border">
            {recentSales.map((venta) => {
              return (
                <div key={venta.id} className="flex items-center justify-between p-4 hover:bg-muted/50 transition-colors">
                  <div className="flex items-center gap-3">
                    <div className="h-9 w-9 rounded-lg bg-primary/10 flex items-center justify-center text-primary text-xs font-bold">
                      {venta.clienteNombre?.charAt(0) || "-"}
                    </div>
                    <div>
                      <p className="text-sm font-medium text-foreground">{venta.clienteNombre || `ID: ${venta.id}`}</p>
                      <p className="text-xs text-muted-foreground">{venta.usuarioNombre} · {new Date(venta.fechaVenta).toLocaleDateString()}</p>
                    </div>
                  </div>
                  <div className="text-right">
                    <p className="text-sm font-mono font-semibold text-foreground">{formatAmount(venta.montoTotal)}</p>
                    <StatusBadge status={venta.estado_Nombre || 'Pendiente'} />
                  </div>
                </div>
              );
            })}
          </div>
        </motion.div>

        {/* Top Vendedores */}
        <motion.div initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: 0.4 }} className="glass rounded-xl overflow-hidden">
          <div className="flex items-center justify-between p-4 border-b border-border">
            <h2 className="font-semibold text-foreground text-sm">Top Vendedores</h2>
            <button onClick={() => navigate("/usuarios")} className="text-xs text-primary flex items-center gap-1 hover:underline">
              Ver equipo <ArrowUpRight className="h-3 w-3" />
            </button>
          </div>
          <div className="divide-y divide-border">
            {topSellers.map((seller, idx) => (
              <div key={seller.usuarioId} className="flex items-center justify-between p-4 hover:bg-muted/50 transition-colors">
                <div className="flex items-center gap-3">
                  <div className={`h-9 w-9 rounded-full flex items-center justify-center ${idx === 0 ? 'bg-amber-100 text-amber-600 dark:bg-amber-900/30 dark:text-amber-500' : idx === 1 ? 'bg-slate-100 text-slate-500 dark:bg-slate-800 dark:text-slate-400' : idx === 2 ? 'bg-orange-50 text-orange-700 dark:bg-orange-900/30 dark:text-orange-500' : 'bg-muted text-muted-foreground'}`}>
                    <Medal className="h-4 w-4" />
                  </div>
                  <div>
                    <p className="text-sm font-medium text-foreground">{seller.nombre}</p>
                    <p className="text-xs text-muted-foreground">{seller.count} ventas cerradas</p>
                  </div>
                </div>
                <div className="text-right">
                  <p className="text-sm font-bold font-mono text-primary">{formatAmount(seller.totalVendido)}</p>
                </div>
              </div>
            ))}
            {topSellers.length === 0 && (
              <div className="p-8 text-center text-muted-foreground text-sm">
                No hay ventas registradas aún.
              </div>
            )}
          </div>
        </motion.div>
      </div>
    </div>
  );
}
