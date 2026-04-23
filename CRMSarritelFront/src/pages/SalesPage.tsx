import { useState, useMemo, useEffect, useCallback } from "react";
import { useSearchParams } from "react-router-dom";
import { motion, AnimatePresence } from "framer-motion";
import { Search, Plus, LayoutList, Columns3, Calendar, AlertTriangle, ShoppingCart, Tag, FilterX, TrendingUp, DollarSign, BarChart3, Users, Upload, User, ArrowRight, LayoutGrid } from "lucide-react";
import { PermissionGuard } from "@/components/auth/PermissionGuard";
import { KpiCard } from "@/components/shared/KpiCard";
import { BulkImportModal } from "@/components/shared/BulkImportModal";
import { toast } from "sonner";
import { useNavigate } from "react-router-dom";

import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { StatusBadge } from "@/components/shared/StatusBadge";
import { ResponsiveModal } from "@/components/shared/ResponsiveModal";
import { EmptyState } from "@/components/shared/EmptyState";
import { useQueryClient, useQuery } from '@tanstack/react-query';
import { TiposVentaService } from "@/services/api/proveedores.service";
import { ClientesService } from '@/services/api/clientes.service';
import { UsuariosService } from '@/services/api/usuarios.service';
import { type Venta } from '@/types';
import { useIsMobile } from "@/hooks/use-mobile";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Separator } from "@/components/ui/separator";
import { EntityFilesModal } from "@/components/shared/EntityFilesModal";
import { useSales, useCreateVenta, useDeleteVenta, useUpdateVenta } from "@/hooks/useSales";
import { useClientes } from "@/hooks/useClientes";
import { useUsuarios } from "@/hooks/useUsuarios";
import { usePeriodos } from "@/hooks/usePeriodos";
import { useDebounce } from "@/hooks/useDebounce";
import { SalesSkeleton } from "@/components/shared/SalesSkeleton";
import { Checkbox } from "@/components/ui/checkbox";
import { BulkActionsBar } from "@/components/shared/BulkActionsBar";
import { getInsensitive } from "@/lib/import-utils";
import { cn } from "@/lib/utils";
import { UnifiedSearchBar } from "@/components/shared/UnifiedSearchBar";
import { FilterField, type FilterOption } from "@/components/shared/FilterField";
import { SaleWizard } from "@/components/ventas/SaleWizard";
import { SaleEditFormModal } from "@/components/ventas/SaleEditFormModal";
import { File as FileIcon, Trash2, CheckSquare, Settings } from "lucide-react";
import React from 'react';
import { useAuth } from "@/hooks/useAuth";

// Memoized table row for performance
const SaleRow = React.memo(({ 
  sale, 
  index, 
  selectionMode, 
  isSelected, 
  onSelect, 
  onClick, 
  isLargeList,
  clientName,
  sellerName,
  formatAmount
}: { 
  sale: Venta, 
  index: number, 
  selectionMode: boolean, 
  isSelected: boolean, 
  onSelect: (id: number, selected: boolean) => void,
  onClick: (s: Venta) => void,
  onUserClick: (id: number) => void,
  isLargeList: boolean,
  clientName: string,
  sellerName: string,
  formatAmount: (n: number) => string,
  onUserClick: (id: number) => void
}) => {
  return (
    <motion.tr 
      initial={isLargeList ? false : { opacity: 0 }} 
      animate={isLargeList ? false : { opacity: 1 }} 
      transition={isLargeList ? { duration: 0 } : { delay: Math.min(index * 0.01, 0.5) }} 
      className={cn(
        "border-b border-border hover:bg-muted/50 cursor-pointer transition-colors",
        isSelected && "bg-primary/5"
      )}
      onClick={() => selectionMode ? null : onClick(sale)}
    >
      {selectionMode && (
        <td className="p-4" onClick={(e) => e.stopPropagation()}>
          <Checkbox 
            checked={isSelected}
            onCheckedChange={(checked) => onSelect(sale.id, !!checked)}
          />
        </td>
      )}
      <td className="p-4 font-medium text-foreground">
        <span 
          className="hover:text-primary hover:underline transition-colors font-mono decoration-primary/30 underline-offset-4"
          onClick={(e) => {
            e.stopPropagation();
            onClick(sale);
          }}
        >
          {sale.numeroVenta}
        </span>
      </td>
      <td className="p-4 text-foreground">{clientName}</td>
      <td className="p-4 text-muted-foreground">
        <span 
          className="hover:text-primary hover:underline transition-colors cursor-pointer"
          onClick={(e) => {
            e.stopPropagation();
            onUserClick(sale.usuarioId);
          }}
        >
          {sellerName}
        </span>
      </td>
      <td className="p-4 text-muted-foreground text-[11px]">{sale.tipoVenta_Nombre}</td>
      <td className="p-4 text-muted-foreground text-[11px] capitalize">{sale.origenVenta}</td>
      <td className="p-4 text-muted-foreground text-[11px]">{new Date(sale.fechaVenta).toLocaleDateString()}</td>
      <td className="p-4 text-center">
        <StatusBadge status={sale.estado_Nombre} colorClass={sale.estado_Color} />
      </td>
    </motion.tr>
  );
});


export default function SalesPage() {
  const { user, hasPermission } = useAuth();
  const navigate = useNavigate();
  const [selectedIds, setSelectedIds] = useState<Set<number>>(new Set());
  const [selectionMode, setSelectionMode] = useState(false);
  const [searchParams, setSearchParams] = useSearchParams();
  const [view, setView] = useState<"table" | "kanban" | "grid">("table");
  const [searchInfo, setSearchInfo] = useState(searchParams.get("q") || "");
  const [selectedSaleId, setSelectedSaleId] = useState<number | null>(null);
  const [wizardOpen, setWizardOpen] = useState(false);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [filesModalOpen, setFilesModalOpen] = useState(false);
  const [importModalOpen, setImportModalOpen] = useState(false);
  const [displayLimit, setDisplayLimit] = useState(50);
  const isMobile = useIsMobile();

  // Queries
  const debouncedSearch = useDebounce(searchInfo, 300);
  const currentStatus = searchParams.get("status") || "all";
  const currentSeller = searchParams.get("vendedor") || (hasPermission("ventas:view_all") ? "all" : String(user?.id));
  const currentClient = searchParams.get("clienteId") || "all";
  const currentPeriod = searchParams.get("periodoId") || "all";

  const { data: sales = [], isLoading: loadingSales, isError: errorSales } = useSales({
    q: debouncedSearch,
    status: currentStatus,
    vendedorId: currentSeller !== "all" ? Number(currentSeller) : undefined,
    clienteId: currentClient !== "all" ? Number(currentClient) : undefined,
  });

  const selectedSale = useMemo(() => sales.find(s => s.id === selectedSaleId) || null, [sales, selectedSaleId]);

  const { periodos = [] } = usePeriodos();

  const kanbanColumns = useMemo(() => {
    // Generate unique kanban columns based on current filtered sales
    const uniqueStates = new Map<string, { codigo: string, nombre: string, color: string }>();
    sales.forEach(s => {
      if (s.estado_Codigo && !uniqueStates.has(s.estado_Codigo)) {
        uniqueStates.set(s.estado_Codigo, { codigo: s.estado_Codigo, nombre: s.estado_Nombre, color: s.estado_Color || '' });
      }
    });

    if (uniqueStates.size === 0) return [{ codigo: "NUEVO", nombre: "Nuevas (0)" }];
    return Array.from(uniqueStates.values());
  }, [sales]);

  const { clientes: clients = [] } = useClientes();
  const { data: users = [] } = useUsuarios();

  const { data: tiposVenta = [] } = useQuery({
    queryKey: ['tiposVenta'],
    queryFn: async () => {
      const res = await TiposVentaService.getAll();
      return Array.isArray(res) ? res : [];
    }
  });

  const ganadaCodes = useMemo(() => {
    const codes = new Set<string>();
    tiposVenta.forEach(tv => {
      if (tv.estadosVentaJson) {
        try {
          const estados = JSON.parse(tv.estadosVentaJson);
          estados.forEach((st: any) => {
            if (st.esGanada === true && st.codigo) {
              codes.add(st.codigo);
            }
          });
        } catch {}
      }
    });
    return codes;
  }, [tiposVenta]);


  // Optimization: Maps for O(1) lookup
  const clientMap = useMemo(() => {
    const map = new Map();
    clients.forEach(c => map.set(c.id, c.nombre));
    return map;
  }, [clients]);

  const filteredSellers = useMemo(() => {
    if (hasPermission("ventas:view_all")) return users;
    if (user?.isManager && user.managedEquipoIds) {
      return users.filter(u => 
        String(u.id) === user.id || 
        (u.equipoIds && u.equipoIds.some(id => user.managedEquipoIds?.includes(id)))
      );
    }
    return users.filter(u => String(u.id) === user?.id);
  }, [users, user, hasPermission]);

  const userMap = useMemo(() => {
    const map = new Map();
    users.forEach(u => map.set(u.id, u.nombre));
    return map;
  }, [users]);

  const getClientName = useCallback((id: number) => clientMap.get(id) || `ID: ${id}`, [clientMap]);
  const getUserName = useCallback((id: number) => userMap.get(id) || `ID: ${id}`, [userMap]);

  // Sync URL with input field changes
  useEffect(() => {
    const params = new URLSearchParams(searchParams);
    if (debouncedSearch) params.set("q", debouncedSearch);
    else params.delete("q");
    setSearchParams(params, { replace: true });
  }, [debouncedSearch, setSearchParams]);

  const formatAmount = useCallback((n: number) => `${n?.toLocaleString("es-ES") || '0'} €`, []);

  const filtered = useMemo(() => {
    let result = sales;

    // Filter by Period (Installation Date)
    if (currentPeriod !== "all") {
      const periodo = periodos.find(p => p.id === Number(currentPeriod));
      if (periodo) {
        const start = new Date(periodo.fechaInicio);
        const end = new Date(periodo.fechaFin);
        result = result.filter(v => {
          if (!v.fechaInstalacionReal) return false;
          const instDate = new Date(v.fechaInstalacionReal);
          return instDate >= start && instDate <= end;
        });
      }
    }

    if (debouncedSearch === "") return result;
    const searchLower = debouncedSearch.toLowerCase();
    return result.filter((v) => {
      const clientName = (clientMap.get(v.clienteId) || "").toLowerCase();
      return clientName.includes(searchLower) || v.numeroVenta.toLowerCase().includes(searchLower);
    });
  }, [debouncedSearch, sales, clientMap, currentPeriod, periodos]);

  const kpis = useMemo(() => {
    const ventasTotales = filtered.length;
    const cerradas = filtered.filter(v => ganadaCodes.has(v.estado_Codigo || '') || v.estado_EsGanada).length;
    const cerradasPercentage = ventasTotales > 0 ? Math.round((cerradas / ventasTotales) * 100) : 0;

    return [
      { 
        title: "Ventas Totales", 
        value: String(ventasTotales), 
        icon: TrendingUp, 
        sparkData: [5, 10, 8, 15, 12, 18, ventasTotales] 
      },
      { 
        title: "Ventas Cerradas", 
        value: String(cerradas), 
        icon: CheckSquare, 
        sparkData: [0, 2, 5, 8, Math.max(0, cerradas - 2), cerradas] 
      },
      { 
        title: "Tasa de Cierre", 
        value: `${cerradasPercentage}%`, 
        icon: BarChart3, 
        sparkData: [20, 30, 45, 40, Math.max(10, cerradasPercentage - 10), cerradasPercentage] 
      },
    ];
  }, [filtered]);

  const { mutateAsync: createMutation } = useCreateVenta();
  const { mutateAsync: deleteVenta } = useDeleteVenta();
  const queryClient = useQueryClient();

  const handleSelect = useCallback((id: number, selected: boolean) => {
    setSelectedIds(prev => {
      const next = new Set(prev);
      if (selected) next.add(id);
      else next.delete(id);
      return next;
    });
  }, []);

  const handleSelectAll = useCallback((checked: boolean) => {
    if (checked) setSelectedIds(new Set(filtered.map(s => s.id)));
    else setSelectedIds(new Set());
  }, [filtered]);

  const handleBulkDelete = async () => {
    if (selectedIds.size === 0) return;
    if (!confirm(`¿Estás seguro de que deseas eliminar ${selectedIds.size} ventas?`)) return;

    const idsArray = Array.from(selectedIds);
    const toastId = toast.loading(`Eliminando ${idsArray.length} ventas... (0%)`);
    try {
      const chunkSize = 15;
      const total = idsArray.length;
      
      for (let i = 0; i < total; i += chunkSize) {
        const chunk = idsArray.slice(i, i + chunkSize);
        await Promise.all(chunk.map(id => deleteVenta(id)));
        const progress = Math.min(100, Math.round(((i + chunkSize) / total) * 100));
        toast.loading(`Eliminando ${idsArray.length} ventas... (${progress}%)`, { id: toastId });
      }
      
      queryClient.invalidateQueries({ queryKey: ['ventas'] });
      setSelectedIds(new Set());
      setSelectionMode(false);
      toast.success("Ventas eliminadas correctamente", { id: toastId });
    } catch (error) {
      toast.error("Error en la eliminación masiva", { id: toastId });
    }
  };

  const handleImport = async (data: any[], onProgress: (p: number) => void) => {
    let successCount = 0;
    let errorCount = 0;
    const total = data.length;

    for (let i = 0; i < total; i++) {
      const row = data[i];
      try {
        const ventaData = {
          clienteId: Number(getInsensitive(row, ["clienteid", "cliente", "id_cliente", "client_id", "idcliente"]) || 1),
          usuarioId: Number(getInsensitive(row, ["usuarioid", "vendedor", "id_usuario", "user_id", "idusuario", "empleado"]) || 1),
          montoTotal: Number(getInsensitive(row, ["total", "monto", "importe", "amount", "montototal"]) || 0),
          tipoVenta: {
            nombre: getInsensitive(row, ["tipo", "tipoventa", "modalidad", "service"]) || "SARRITEL",
            codigo: getInsensitive(row, ["codigo", "code", "sku"]) || "SAR",
            descripcion: getInsensitive(row, ["descripcion", "details", "notas", "comentarios"]) || "",
            activo: true
          },
          origenVenta: getInsensitive(row, ["origen", "origenventa", "source", "canal"]) || "Directa",
          notas: getInsensitive(row, ["notas", "comentarios", "observaciones", "comments"]) || "",
          fechaVenta: getInsensitive(row, ["fecha", "fechaventa", "date", "created_at"]) || new Date().toISOString(),
          estado: {
            codigo: getInsensitive(row, ["estado", "status", "stage", "cod_estado"]) || "NUEVO",
            nombre: getInsensitive(row, ["estado_nombre", "estado", "nombre_estado"]) || "Nuevo Lead",
            icono: "📋",
            color: "badge-neutral",
            permiteEdicion: true,
            permiteEliminar: true,
            esFinal: false,
            activo: true,
            esInicial: true
          },
          etapaActual: String(getInsensitive(row, ["etapa", "stage", "paso", "phase"]) || "Prospecto"),
          fechaCierreEstimada: new Date().toISOString()
        };
        await createMutation(ventaData as any);
        successCount++;
      } catch (err) {
        console.error(`Error importando venta fila ${i + 1}:`, err);
        errorCount++;
      }
      onProgress(Math.round(((i + 1) / total) * 100));
    }
    
    if (errorCount > 0) {
      toast.warning(`Importación parcial: ${successCount} exitosos, ${errorCount} fallidos. Revisa la consola.`);
    } else {
      toast.success(`Se han importado ${successCount} ventas correctamente`);
    }
  };

  const handleSaleClick = useCallback((s: Venta) => setSelectedSaleId(s.id), []);

  if (loadingSales) return (
    <div className="space-y-6">
      <div>
        <h1 className="text-2xl font-bold text-foreground">Ventas</h1>
        <p className="text-sm text-muted-foreground">Cargando datos...</p>
      </div>
      <div className="glass rounded-xl p-4 flex gap-3 h-16 animate-pulse" />
      <SalesSkeleton view={view} isMobile={isMobile} />
    </div>
  );
  
  if (errorSales) return <div className="h-60 flex flex-col items-center justify-center glass rounded-xl text-destructive gap-2 text-sm"><AlertTriangle /> Error al obtener ventas del servidor.</div>;

  return (
    <div className="space-y-6 pb-20 sm:pb-6">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl font-bold text-foreground">Ventas</h1>
          <p className="text-sm text-muted-foreground">{filtered.length} ventas registradas</p>
        </div>
        <div className="flex items-center gap-2">
          {hasPermission("ventas:create") && (
            <Button variant="outline" size="icon" className="border-border bg-background" onClick={() => setImportModalOpen(true)} title="Importar CSV/Excel">
              <Upload className="h-4 w-4" />
            </Button>
          )}
          <div className="hidden sm:flex items-center bg-muted/30 p-1 rounded-lg border border-border mr-2">
            <Button 
              variant={view === 'table' ? 'secondary' : 'ghost'} 
              size="icon" 
              className="h-7 w-7 rounded-sm" 
              onClick={() => setView('table')}
              title="Vista de Tabla"
            >
              <LayoutList className="h-4 w-4" />
            </Button>
            <Button 
              variant={view === 'grid' ? 'secondary' : 'ghost'} 
              size="icon" 
              className="h-7 w-7 rounded-sm" 
              onClick={() => setView('grid')}
              title="Vista de Widgets"
            >
              <Columns3 className="h-4 w-4" />
            </Button>
            <Button 
              variant={view === 'kanban' ? 'secondary' : 'ghost'} 
              size="icon" 
              className="h-7 w-7 rounded-sm" 
              onClick={() => setView('kanban')}
              title="Vista Kanban"
            >
              <BarChart3 className="h-4 w-4" />
            </Button>
          </div>
          <Button variant="outline" size="icon" className="border-border bg-background" onClick={() => setFilesModalOpen(true)} title="Documentos Globales">
            <FileIcon className="h-4 w-4" />
          </Button>
          {hasPermission("ventas:create") && (
            <Button className="bg-primary text-primary-foreground hover:bg-primary/90 gap-2 flex-1 sm:flex-none font-bold shadow-glow" onClick={() => setWizardOpen(true)}>
              <Plus className="h-4 w-4" /> {isMobile ? "Nueva" : "Nueva Venta"}
            </Button>
          )}
        </div>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {kpis.map((kpi, i) => (
          <KpiCard key={kpi.title} {...kpi} index={i} />
        ))}
      </div>
      <UnifiedSearchBar
        selectionMode={selectionMode}
        onSelectionModeToggle={hasPermission("ventas:delete") ? () => {
          setSelectionMode(!selectionMode);
          if (selectionMode) setSelectedIds(new Set());
        } : undefined}
        bulkActions={
          hasPermission("ventas:delete") && (
            <div className="flex items-center gap-3 w-full">
              <span className="text-xs font-bold text-primary bg-primary/10 px-2 py-1 rounded-md border border-primary/20">
                {selectedIds.size} seleccionados
              </span>
              <div className="h-4 w-px bg-border/40 mx-1" />
              <Button 
                variant="destructive" 
                size="sm" 
                className="h-8 gap-2 shadow-sm font-bold" 
                disabled={selectedIds.size === 0}
                onClick={handleBulkDelete}
              >
                <Trash2 className="h-3.5 w-3.5" /> Eliminar Registros
              </Button>
            </div>
          )
        }
        searchValue={searchInfo}
        onSearchChange={setSearchInfo}
        searchPlaceholder="Buscar por cliente, nro. venta..."
        onClearFilters={() => {
          setSearchInfo("");
          const params = new URLSearchParams(searchParams);
          params.delete("status");
          params.delete("vendedor");
          params.delete("clienteId");
          params.delete("periodoId");
          setSearchParams(params);
        }}
        showFilterBadge={currentStatus !== "all" || currentSeller !== "all" || currentClient !== "all" || currentPeriod !== "all"}
      >
        <div className="flex flex-wrap items-end gap-4 w-full">
          <FilterField 
            columnLabel="Estado de Venta"
            type="select"
            value={currentStatus}
            onChange={(val) => {
              const params = new URLSearchParams(searchParams);
              if (val === "all") params.delete("status");
              else params.set("status", val);
              setSearchParams(params);
            }}
            options={kanbanColumns.map(col => ({ value: col.nombre, label: col.nombre }))}
            placeholder="Todos los estados"
          />

          {(hasPermission("ventas:view_all") || user?.isManager) && (
            <FilterField 
              columnLabel="Vendedor / Usuario"
              type="select"
              value={currentSeller}
              onChange={(val) => {
                const params = new URLSearchParams(searchParams);
                if (val === "all") params.delete("vendedor");
                else params.set("vendedor", val);
                setSearchParams(params);
              }}
              options={filteredSellers.map(u => ({ value: String(u.id), label: u.nombre }))}
              placeholder="Todos los vendedores"
            />
          )}

          <FilterField 
            columnLabel="Cliente"
            type="select"
            value={currentClient}
            onChange={(val) => {
              const params = new URLSearchParams(searchParams);
              if (val === "all") params.delete("clienteId");
              else params.set("clienteId", val);
              setSearchParams(params);
            }}
            options={clients.map(c => ({ value: String(c.id), label: c.nombre }))}
            placeholder="Todos los clientes"
          />

          <FilterField 
            columnLabel="Periodo / Mes"
            type="select"
            value={currentPeriod}
            onChange={(val) => {
              const params = new URLSearchParams(searchParams);
              if (val === "all") params.delete("periodoId");
              else params.set("periodoId", val);
              setSearchParams(params);
            }}
            options={periodos.map(p => ({ value: String(p.id), label: p.nombre }))}
            placeholder="Todos los periodos"
          />
        </div>
      </UnifiedSearchBar>

      {filtered.length === 0 ? (
        <EmptyState icon={ShoppingCart} title="Sin ventas" description="No hay ventas que coincidan con tu búsqueda." />
      ) : view === "kanban" ? (
        <div className="overflow-x-auto scrollbar-hide">
            <div className={cn("inline-flex gap-4 p-1 min-w-full", isMobile ? "flex-col" : "flex-row")}>
                {kanbanColumns.map((col) => {
                    const colSales = filtered.filter((v) => v.estado_Codigo === col.codigo);
                    return (
                    <div key={col.codigo} className={cn("space-y-3 shrink-0", isMobile ? "w-full" : "w-[300px]")}>
                        <div className="flex items-center gap-2 mb-2 p-1 glass rounded-lg bg-muted/40 justify-between">
                            <h4 className="text-xs font-bold px-2 flex items-center gap-2">
                                <span className={cn("w-2 h-2 rounded-full", col.color ? col.color.split(" ")[0].replace("/20","") : "bg-primary")} />
                                {col.nombre}
                            </h4>
                            <span className="text-[10px] text-muted-foreground font-mono bg-background/50 px-1.5 rounded">{colSales.length}</span>
                        </div>
                        <div className="space-y-2">
                            {colSales.slice(0, 50).map((venta, i) => (
                                <motion.div key={venta.id} initial={{ opacity: 0, y: 10 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: Math.min(i * 0.05, 0.5) }} className="glass glass-hover rounded-xl p-4 cursor-pointer relative" onClick={() => selectionMode ? null : setSelectedSale(venta)}>
                                    {selectionMode && (
                                    <div className="absolute top-2 right-2 z-10">
                                        <Checkbox 
                                        checked={selectedIds.has(venta.id)} 
                                        onCheckedChange={(checked) => handleSelect(venta.id, !!checked)}
                                        />
                                    </div>
                                    )}
                                    <p className="text-[9px] text-muted-foreground font-mono mb-1 uppercase tracking-tighter">{venta.numeroVenta}</p>
                                    <p className="font-bold text-sm text-foreground line-clamp-1 mb-1">{getClientName(venta.clienteId)}</p>
                                    <div className="flex items-center gap-2 mt-3 pt-3 border-t border-white/5 text-[10px] text-muted-foreground">
                                        <Calendar className="h-3 w-3" /> {new Date(venta.fechaVenta).toLocaleDateString()}
                                    </div>
                                </motion.div>
                            ))}
                            {colSales.length > 50 && (
                                <p className="text-center text-[10px] text-muted-foreground py-2 italic">Mostrando 50 de {colSales.length}... Usa filtros para refinar.</p>
                            )}
                        </div>
                    </div>
                    );
                })}
            </div>
        </div>
      ) : (view === "grid" || isMobile) ? (
        <div className="space-y-4">
            <div className="grid gap-3 sm:grid-cols-2 lg:grid-cols-3">
            {filtered.slice(0, displayLimit).map((venta, i) => (
                <motion.div key={venta.id} initial={{ opacity: 0, y: 10 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: Math.min(i * 0.05, 0.5) }} className="glass glass-hover rounded-xl p-4 cursor-pointer relative flex flex-col group" onClick={() => selectionMode ? null : handleSaleClick(venta)}>
                <div className="flex justify-between items-start mb-3">
                    <div className="flex items-center gap-2">
                    {selectionMode && (
                        <Checkbox 
                        checked={selectedIds.has(venta.id)} 
                        onCheckedChange={(checked) => handleSelect(venta.id, !!checked)}
                        onClick={(e) => e.stopPropagation()}
                        />
                    )}
                    <div>
                        <p className="text-[10px] text-muted-foreground font-mono mb-0.5">{venta.numeroVenta}</p>
                        <p className="font-bold text-sm text-foreground leading-tight">{getClientName(venta.clienteId)}</p>
                    </div>
                    </div>
                    <StatusBadge status={venta.estado_Nombre} colorClass={venta.estado_Color} />
                </div>
                
                <div className="space-y-1.5 mb-4 opacity-80">
                    <div 
                      className="flex items-center gap-2 text-[11px] text-muted-foreground hover:text-primary cursor-pointer"
                      onClick={(e) => { e.stopPropagation(); navigate(`/perfil/${venta.usuarioId}`); }}
                    >
                        <Users className="h-3 w-3" /> {getUserName(venta.usuarioId)}
                    </div>
                    <div className="flex items-center gap-2 text-[11px] text-muted-foreground">
                        <Tag className="h-3 w-3" /> {venta.tipoVenta_Nombre} · {venta.origenVenta}
                    </div>
                </div>

                <div className="flex justify-between items-end pt-3 border-t border-white/5 mt-auto">
                    <div></div>
                    <div className="flex items-center gap-2">
                        <span className="text-[10px] text-muted-foreground bg-background/50 px-2 py-0.5 rounded-md border border-white/5">{new Date(venta.fechaVenta).toLocaleDateString()}</span>
                        <div className="h-7 w-7 rounded-full bg-primary/10 flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity">
                            <ArrowRight className="h-3 w-3 text-primary" />
                        </div>
                    </div>
                </div>
                </motion.div>
            ))}
            </div>
            {filtered.length > displayLimit && (
                <div className="flex justify-center pt-4">
                    <Button variant="outline" size="sm" onClick={() => setDisplayLimit(prev => prev + 50)} className="gap-2">
                        Cargar más ventas ({filtered.length - displayLimit} restantes)
                    </Button>
                </div>
            )}
        </div>
      ) : (
        <div className="glass rounded-xl overflow-hidden relative border border-border/50">
          <div className="overflow-x-auto scrollbar-thin scrollbar-thumb-primary/20 scrollbar-track-transparent">
            <table className="w-full text-sm min-w-[1000px]">
                <thead>
                <tr className="border-b border-border bg-muted/20">
                    {selectionMode && (
                    <th className="p-4 w-10">
                        <Checkbox 
                        checked={selectedIds.size === filtered.length && filtered.length > 0}
                        onCheckedChange={handleSelectAll}
                        />
                    </th>
                    )}
                    <th className="text-left p-4 font-medium text-muted-foreground">Nro.</th>
                    <th className="text-left p-4 font-medium text-muted-foreground">Cliente</th>
                    <th className="text-left p-4 font-medium text-muted-foreground">Usuario</th>
                    <th className="text-left p-4 font-medium text-muted-foreground">Tipo</th>
                    <th className="text-left p-4 font-medium text-muted-foreground">Origen</th>
                    <th className="text-left p-4 font-medium text-muted-foreground">Fecha</th>
                    <th className="text-center p-4 font-medium text-muted-foreground pr-6">Estado</th>
                </tr>
                </thead>
                <tbody>
                {filtered.slice(0, displayLimit).map((venta, i) => (
                    <SaleRow 
                    key={venta.id}
                    sale={venta}
                    index={i}
                    selectionMode={selectionMode}
                    isSelected={selectedIds.has(venta.id)}
                    isLargeList={filtered.length > 80}
                    clientName={getClientName(venta.clienteId)}
                    sellerName={getUserName(venta.usuarioId)}
                    formatAmount={formatAmount}
                    onSelect={handleSelect}
                    onClick={handleSaleClick}
                    onUserClick={(userId) => navigate(`/perfil/${userId}`)}
                    />
                ))}
                </tbody>
            </table>
            {filtered.length > displayLimit && (
                <div className="p-4 text-center border-t border-border bg-muted/5">
                    <Button variant="ghost" size="sm" onClick={() => setDisplayLimit(prev => prev + 100)} className="text-primary hover:bg-primary/5">
                        Mostrando {displayLimit} de {filtered.length} - Cargar más...
                    </Button>
                </div>
            )}
          </div>
        </div>
      )}
      <ResponsiveModal 
        open={!!selectedSaleId && !editModalOpen} 
        onOpenChange={(open) => !open && setSelectedSaleId(null)} 
        title="Detalle de Venta"
      >
        {selectedSale && (
          <div className="space-y-5">
            <div className="glass rounded-lg p-4 space-y-2">
              <div className="flex justify-between items-center">
                <span className="text-xs text-muted-foreground font-mono">{selectedSale.numeroVenta}</span>
                <StatusBadge status={selectedSale.estado_Nombre} />
              </div>
              <div className="flex justify-between items-center mt-2">
                <span className="text-sm font-medium text-muted-foreground">Nivel de Venta</span>
                <span className="text-foreground capitalize">{selectedSale.origenVenta}</span>
              </div>
            </div>
            <div className="space-y-3 text-sm">
              <div className="flex justify-between"><span className="text-muted-foreground">Cliente</span><span className="text-foreground">{getClientName(selectedSale.clienteId)}</span></div>
              <div className="flex justify-between"><span className="text-muted-foreground">Usuario (Vendedor)</span><span className="text-foreground">{getUserName(selectedSale.usuarioId)}</span></div>
              <div className="flex justify-between"><span className="text-muted-foreground">Tipo</span><span className="text-foreground">{selectedSale.tipoVenta_Nombre}</span></div>
              <div className="flex justify-between"><span className="text-muted-foreground">Origen</span><span className="text-foreground capitalize">{selectedSale.origenVenta}</span></div>
              <div className="flex justify-between"><span className="text-muted-foreground">Fecha Venta</span><span className="text-foreground">{new Date(selectedSale.fechaVenta).toLocaleDateString()}</span></div>
              <div className="flex justify-between"><span className="text-muted-foreground">Etapa Actual</span><span className="text-foreground font-mono">{selectedSale.etapaActual}</span></div>
            </div>
            <Separator className="bg-white/10" />
            <div className="space-y-2 text-sm">
              <h4 className="text-xs font-medium text-muted-foreground uppercase tracking-wider">Instalación</h4>
              <div className="flex justify-between"><span className="text-muted-foreground">Prevista</span><span className="text-foreground">{selectedSale.fechaInstalacionPrevista ? new Date(selectedSale.fechaInstalacionPrevista).toLocaleDateString() : 'No asignada'}</span></div>
              <div className="flex justify-between"><span className="text-muted-foreground">Real</span><span className="text-foreground">{selectedSale.fechaInstalacionReal ? new Date(selectedSale.fechaInstalacionReal).toLocaleDateString() : 'Pendiente'}</span></div>
            </div>
            {selectedSale.notas && (
              <div><h4 className="text-xs font-medium text-muted-foreground uppercase tracking-wider mb-2">Notas</h4><p className="text-sm text-foreground">{selectedSale.notas}</p></div>
            )}
            <div className="pt-4 border-t border-border mt-4 flex flex-col gap-2">
              <Button variant="outline" className="w-full gap-2 border-primary/20 text-primary hover:bg-primary/10 font-bold" onClick={() => setFilesModalOpen(true)}>
                <FileIcon className="h-4 w-4" />
                Gestionar Archivos Adjuntos
              </Button>
              {hasPermission("ventas:update") && (
                <Button variant="secondary" className="w-full gap-2 border border-border font-bold bg-primary/10 text-primary hover:bg-primary/20" onClick={() => {
                  setEditModalOpen(true);
                }}>
                  <LayoutGrid className="h-4 w-4" /> Vista 360 de Venta
                </Button>
              )}
            </div>
          </div>
        )}
      </ResponsiveModal>

      <EntityFilesModal
        isOpen={filesModalOpen}
        onClose={() => setFilesModalOpen(false)}
        entidadTipo="Venta"
        entidadId={selectedSale?.id || null}
        entidadNombre={selectedSale?.numeroVenta}
      />

      <SaleWizard open={wizardOpen} onClose={() => setWizardOpen(false)} />
      
      {selectedSale && (
        <SaleEditFormModal
          open={editModalOpen}
          onClose={() => {
            setEditModalOpen(false);
            setSelectedSaleId(null);
          }}
          sale={selectedSale}
          clients={clients}
          users={users}
        />
      )}

      <BulkImportModal 
        open={importModalOpen} 
        onClose={() => setImportModalOpen(false)} 
        entityName="Ventas" 
        onImport={handleImport}
        requiredFields={["ClienteId", "UsuarioId"]}
        optionalFields={["Total", "Origen", "Notas", "Fecha", "TipoVentaId"]}
      />

      {isMobile && !loadingSales && !errorSales && hasPermission("ventas:create") && (
        <motion.button whileTap={{ scale: 0.9 }} className="fixed bottom-6 right-6 h-14 w-14 rounded-full bg-primary text-primary-foreground shadow-glow flex items-center justify-center z-40" onClick={() => setWizardOpen(true)}>
          <Plus className="h-6 w-6" />
        </motion.button>
      )}
    </div>
  );
}
