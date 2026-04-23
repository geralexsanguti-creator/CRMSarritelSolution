import { useState, useMemo, useEffect, useCallback } from "react";
import { motion } from "framer-motion";
import { Clock, Play, Square, Coffee, UserMinus, Plus, Calendar, AlertTriangle, FileText, Trash2, Edit, Save } from "lucide-react";
import { Button } from "@/components/ui/button";
import { KpiCard } from "@/components/shared/KpiCard";
import { useFichajes } from "@/hooks/useFichajes";
import { useAuth } from "@/hooks/useAuth";
import { EmptyState } from "@/components/shared/EmptyState";
import { StatusBadge } from "@/components/shared/StatusBadge";
import { toast } from "sonner";
import { UnifiedSearchBar } from "@/components/shared/UnifiedSearchBar";
import { FilterField } from "@/components/shared/FilterField";
import { Checkbox } from "@/components/ui/checkbox";
import { cn } from "@/lib/utils";
import React from 'react';
import { FichajeFormModal } from "@/components/fichajes/FichajeFormModal";
import { Fichaje } from "@/types";


// Memoized Fichaje Row
const FichajeRow = React.memo(({
    f,
    index,
    isAdmin,
    selectionMode,
    isSelected,
    onSelect,
    onEdit,
    isLargeList
}: {
    f: Fichaje,
    index: number,
    isAdmin: boolean,
    selectionMode: boolean,
    isSelected: boolean,
    onSelect: (id: number, selected: boolean) => void,
    onEdit: (f: Fichaje) => void,
    isLargeList: boolean
}) => {
    const isWorking = !f.horaSalida;
    const pausas: any[] = f.pausas || [];
    const totalPausas = Math.round(f.totalPausasMinutos || 0);
    return (
        <motion.tr
        initial={isLargeList ? false : { opacity: 0 }}
        animate={isLargeList ? false : { opacity: 1 }}
        transition={isLargeList ? { duration: 0 } : { delay: Math.min(index * 0.01, 0.5) }}
        className={cn(
            "hover:bg-muted/50 transition-colors border-b border-border/40",
            isSelected && "bg-primary/5"
        )}
        >
        {selectionMode && (
            <td className="p-4">
            <Checkbox
                checked={isSelected}
                onCheckedChange={(checked) => onSelect(f.id, !!checked)}
            />
            </td>
        )}
        {isAdmin && <td className="p-4 font-medium text-foreground">{f.usuarioNombre || "ID " + f.usuarioId}</td>}
        <td className="p-4">
            <div className="flex items-center gap-2">
            {f.tipoRegistro === "Vacaciones" ? <Calendar className="h-4 w-4 text-sky-500" /> : f.tipoRegistro === "Baja Medica" ? <AlertTriangle className="h-4 w-4 text-rose-500" /> : f.tipoRegistro === "Permiso" ? <Coffee className="h-4 w-4 text-amber-500" /> : <Clock className="h-4 w-4 text-emerald-500" />}
            <span className="font-medium">{f.tipoRegistro}</span>
            </div>
            {f.notas && <p className="text-[10px] text-muted-foreground mt-1 flex items-center gap-1 opacity-70"><FileText className="h-2.5 w-2.5" /> {f.notas}</p>}
        </td>
        <td className="p-4 font-mono text-xs opacity-80">{new Date(f.horaEntrada).toLocaleString("es-ES")}</td>
        <td className="p-4 font-mono text-xs opacity-80">{f.horaSalida ? new Date(f.horaSalida).toLocaleString("es-ES") : "—"}</td>
        <td className="p-4">
            {pausas.length > 0 ? (
                <div className="space-y-1">
                    {pausas.map((p: any, idx: number) => (
                        <div key={p.id ?? idx} className="text-[10px] font-mono text-amber-600 dark:text-amber-400 flex items-center gap-1">
                            <Coffee className="h-2.5 w-2.5 shrink-0" />
                            <span>{new Date(p.horaInicio).toLocaleTimeString("es-ES", { hour: "2-digit", minute: "2-digit" })}</span>
                            <span className="opacity-50">→</span>
                            <span>{p.horaFin ? new Date(p.horaFin).toLocaleTimeString("es-ES", { hour: "2-digit", minute: "2-digit" }) : "activa"}</span>
                        </div>
                    ))}
                    {totalPausas > 0 && (
                        <span className="text-[9px] text-muted-foreground opacity-60">{totalPausas} min total</span>
                    )}
                </div>
            ) : (
                <span className="text-muted-foreground opacity-30 text-xs">—</span>
            )}
        </td>
        <td className="p-4">
            {f.horasExtra > 0 ? (
            <span className="bg-destructive/10 text-destructive font-mono px-2 py-1 rounded-full text-[10px] font-bold">+{f.horasExtra} h</span>
            ) : (
            <span className="text-muted-foreground opacity-40">—</span>
            )}
        </td>
        <td className="p-4 text-right">
            <div className="flex items-center justify-end gap-2">
                {!isWorking && isAdmin && (
                    <Button 
                        variant="ghost" 
                        size="icon" 
                        className="h-7 w-7 text-primary hover:bg-primary/10"
                        onClick={() => onEdit(f)}
                    >
                        <Edit className="h-3.5 w-3.5" />
                    </Button>
                )}
                {isWorking ? (
                    <StatusBadge status="En Curso" />
                ) : (
                    <span className="text-[10px] bg-muted px-2 py-0.5 rounded-full text-muted-foreground uppercase font-bold tracking-tighter">Completado</span>
                )}
            </div>
        </td>
        </motion.tr>
    );
});


export default function FichajesPage() {
  // 1. Auth & Data Hooks
  // 1. Context & Filters
  const { user, hasPermission } = useAuth();
  const isAdmin = useMemo(() => 
    user?.roles?.includes("Admin") || user?.roles?.includes("SuperAdmin")
  , [user]);

  const [dateRange, setDateRange] = useState({
    desde: "",
    hasta: ""
  });

  const { 
    data: rawFichajes, 
    isLoading, 
    isError, 
    startFichaje, 
    stopFichaje, 
    updateFichaje,
    deleteFichaje,
    pauseFichaje,
    resumeFichaje,
    isStarting, 
    isStopping,
    isUpdating,
    isPausing,
    isResuming 
  } = useFichajes({
     fechaInicio: dateRange.desde || undefined,
     fechaFin: dateRange.hasta || undefined
  });

  // Defensive check for data
  const fichajes = useMemo(() => rawFichajes || [], [rawFichajes]);
  
  // 2. State Hooks
  const [search, setSearch] = useState("");
  const [usuarioFilter, setUsuarioFilter] = useState("all");
  const [tipoFilter, setTipoFilter] = useState("all");
  const [selectionMode, setSelectionMode] = useState(false);
  const [selectedIds, setSelectedIds] = useState<Set<number>>(new Set());
  const [displayLimit, setDisplayLimit] = useState(100);
  const [activeDuration, setActiveDuration] = useState("00:00:00");
  const [modalOpen, setModalOpen] = useState(false);
  const [modalMode, setModalMode] = useState<"create" | "edit">("create");
  const [editingFichaje, setEditingFichaje] = useState<Fichaje | null>(null);

  // 3. Memoized Data (Data Transformations)
  const filteredFichajes = useMemo(() => {
    return fichajes.filter(f => {
      const matchSearch = search === "" || 
        (f.usuarioNombre || "").toLowerCase().includes(search.toLowerCase()) ||
        new Date(f.horaEntrada).toLocaleDateString().includes(search);
      
      const matchUsuario = usuarioFilter === "all" || f.usuarioNombre === usuarioFilter;
      const matchTipo = tipoFilter === "all" || f.tipoRegistro === tipoFilter;

      return matchSearch && matchUsuario && matchTipo;
    });
  }, [search, fichajes, usuarioFilter, tipoFilter]);

  const uniqueUsuarios = useMemo(() => {
    const s = new Set(fichajes.map(f => f.usuarioNombre).filter(Boolean));
    return Array.from(s).sort();
  }, [fichajes]);

  const uniqueTipos = useMemo(() => {
    const s = new Set(fichajes.map(f => f.tipoRegistro).filter(Boolean));
    return Array.from(s).sort();
  }, [fichajes]);

  const myActiveSession = useMemo(() => {
    if (!user) return null;
    return fichajes.find(f => f.usuarioId === Number(user.id) && !f.horaSalida) || null;
  }, [fichajes, user]);

  const activeCount = useMemo(() => fichajes.filter(f => !f.horaSalida).length, [fichajes]);
  const overtimeCount = useMemo(() => fichajes.filter(f => f.horasExtra > 0).length, [fichajes]);
  const currentMonthEntries = useMemo(() => fichajes.filter(f => {
    const date = new Date(f.horaEntrada);
    const now = new Date();
    return date.getMonth() === now.getMonth() && date.getFullYear() === now.getFullYear();
  }).length, [fichajes]);

  const kpis = useMemo(() => [
    { title: "Personal Activo", value: String(activeCount), icon: Clock, sparkData: [1, 2, 3, 5, 4, 3, activeCount] },
    { title: "Turnos este Mes", value: String(currentMonthEntries), icon: Calendar, sparkData: [10, 15, 20, 18, 25, 22, 30] },
    { title: "Turnos c/ Horas Extra", value: String(overtimeCount), icon: AlertTriangle, sparkData: [2, 1, 0, 1, 3, 2, 4] },
    { title: "Permisos/Ausencias", value: String(fichajes.filter(f => f.tipoRegistro !== "Trabajando").length), icon: UserMinus, sparkData: [0, 1, 1, 0, 0, 1, 2] },
  ], [activeCount, currentMonthEntries, overtimeCount, fichajes]);

  // 4. Callbacks (Actions)
  const handleSelect = useCallback((id: number, selected: boolean) => {
    setSelectedIds(prev => {
      const next = new Set(prev);
      if (selected) next.add(id);
      else next.delete(id);
      return next;
    });
  }, []);

  const handleSelectAll = useCallback((checked: boolean) => {
    if (checked) setSelectedIds(new Set(filteredFichajes.map(f => f.id)));
    else setSelectedIds(new Set());
  }, [filteredFichajes]);

  const handleBulkDelete = useCallback(async () => {
    if (selectedIds.size === 0) return;
    if (!confirm(`¿Estás seguro de que deseas eliminar ${selectedIds.size} registros?`)) return;

    const idsArray = Array.from(selectedIds);
    const toastId = toast.loading(`Eliminando ${idsArray.length} fichajes... (0%)`);
    try {
      const chunkSize = 15;
      const total = idsArray.length;
      
      for (let i = 0; i < total; i++) {
        await deleteFichaje(idsArray[i]);
        const progress = Math.min(100, Math.round(((i + 1) / total) * 100));
        toast.loading(`Eliminando ${idsArray.length} fichajes... (${progress}%)`, { id: toastId });
      }
      
      setSelectedIds(new Set());
      setSelectionMode(false);
      toast.success("Fichajes eliminados correctamente", { id: toastId });
    } catch (error) {
      toast.error("Error en la eliminación masiva", { id: toastId });
    }
  }, [selectedIds, deleteFichaje]);

  const handleAction = useCallback(async (data: any) => {
    if (!user) return;
    
    // Sanitize data
    const sanitizedData = Object.fromEntries(
      Object.entries(data).filter(([_, v]) => v !== "")
    );
    
    if (modalMode === "create") {
        await startFichaje({ usuarioId: Number(user.id), ...sanitizedData });
    } else if (editingFichaje) {
        await updateFichaje({ id: editingFichaje.id, data: sanitizedData });
    }
    
    setModalOpen(false);
    setEditingFichaje(null);
  }, [user, modalMode, editingFichaje, startFichaje, updateFichaje]);

  const handleEdit = useCallback((f: Fichaje) => {
    setEditingFichaje(f);
    setModalMode("edit");
    setModalOpen(true);
  }, []);

  // 5. Effects (Side Effects)
  useEffect(() => {
    let interval: ReturnType<typeof setInterval>;
    if (myActiveSession?.horaEntrada) {
      const startTime = new Date(myActiveSession.horaEntrada).getTime();
      interval = setInterval(() => {
        const now = new Date().getTime();
        const diff = Math.max(0, now - startTime);
        const hrs = Math.floor(diff / (1000 * 60 * 60));
        const mins = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
        const secs = Math.floor((diff % (1000 * 60)) / 1000);
        setActiveDuration(
          `${hrs.toString().padStart(2, '0')}:${mins.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`
        );
      }, 1000);
    } else {
      setActiveDuration("00:00:00");
    }
    return () => clearInterval(interval);
  }, [myActiveSession]);

  if (isLoading) {
    return (
      <div className="space-y-6">
         <div className="flex justify-between items-center h-12">
            <div className="h-8 w-64 bg-muted animate-pulse rounded-lg" />
            <div className="h-10 w-40 bg-muted animate-pulse rounded-lg" />
         </div>
         <div className="grid grid-cols-2 lg:grid-cols-4 gap-4">
            {[1,2,3,4].map(i => <div key={i} className="h-32 glass animate-pulse rounded-xl" />)}
         </div>
         <div className="h-96 glass animate-pulse rounded-xl" />
      </div>
    );
  }

  if (isError) {
    return (
      <div className="p-12 text-center glass rounded-2xl border border-destructive/20 max-w-2xl mx-auto my-12">
        <AlertTriangle className="h-12 w-12 text-destructive mx-auto mb-4" />
        <h3 className="text-lg font-bold text-foreground">Error de carga</h3>
        <p className="text-muted-foreground mt-2">No se pudieron recuperar los registros del servidor central.</p>
        <Button variant="outline" className="mt-6" onClick={() => window.location.reload()}>Reintentar</Button>
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-20 sm:pb-6">
      <div className="flex flex-col xl:flex-row xl:items-center justify-between gap-4">
        <div className="flex flex-col sm:flex-row sm:items-center gap-4">
          <h1 className="text-2xl font-bold text-foreground">Control de Tiempos</h1>
          <p className="text-sm text-muted-foreground">Monitoreo de asistencia y ausencias</p>
        </div>
        
        {/* Timer Panel - Permitted only if can create fichajes */}
        {hasPermission("fichajes:create") && (
          <div className="glass rounded-xl p-2 px-4 flex items-center justify-between gap-6 border border-primary/20 shadow-glow">
            <div className="flex flex-col">
              <span className="text-[10px] text-muted-foreground uppercase tracking-widest font-bold">
                {myActiveSession?.isPausado ? "En Pausa" : "Sesión en Curso"}
              </span>
              {myActiveSession ? (
                <div className="flex items-center gap-2">
                   <span className={cn("h-2 w-2 rounded-full", myActiveSession.isPausado ? "bg-amber-500" : "bg-emerald-500 animate-pulse")} />
                   <span className={cn("font-bold font-mono tracking-widest text-lg", myActiveSession.isPausado ? "text-amber-500" : "text-primary")}>
                      {activeDuration}
                   </span>
                   {myActiveSession.totalPausasMinutos > 0 && (
                     <span className="text-[10px] text-muted-foreground ml-2 font-medium">
                       (Pausa: {Math.round(myActiveSession.totalPausasMinutos)}m)
                     </span>
                   )}
                </div>
              ) : (
                <span className="text-muted-foreground font-bold text-sm opacity-50">SISTEMA EN ESPERA</span>
              )}
            </div>
            
            <div className="shrink-0 flex items-center gap-2">
              {myActiveSession ? (
                <>
                  {myActiveSession.isPausado ? (
                    <Button variant="outline" size="sm" className="gap-2 border-primary/30 text-primary hover:bg-primary/10 transition-all font-bold h-10 px-4 rounded-lg shadow-sm" onClick={() => resumeFichaje(myActiveSession.id)} disabled={isResuming}>
                      <Play className="h-3.5 w-3.5 fill-current" /> {isResuming ? "Reanudando..." : "Reanudar"}
                    </Button>
                  ) : (
                    <Button variant="outline" size="sm" className="gap-2 border-amber-500/30 text-amber-500 hover:bg-amber-500/10 transition-all font-bold h-10 px-4 rounded-lg shadow-sm" onClick={() => pauseFichaje(myActiveSession.id)} disabled={isPausing}>
                      <Coffee className="h-3.5 w-3.5" /> {isPausing ? "Pausando..." : "Pausa"}
                    </Button>
                  )}
                  <Button variant="destructive" size="sm" className="gap-2 shadow-glow hover:shadow-destructive/40 transition-all font-bold h-10 px-6 rounded-lg" onClick={() => stopFichaje(myActiveSession.id)} disabled={isStopping}>
                    <Square className="h-3.5 w-3.5 fill-current" /> {isStopping ? "Deteniendo..." : "Finalizar Jornada"}
                  </Button>
                </>
              ) : (
                <Button size="sm" className="gap-2 bg-primary text-primary-foreground hover:bg-primary/90 shadow-glow hover:shadow-primary/40 transition-all font-bold h-10 px-6 rounded-lg" onClick={() => { setModalMode("create"); setModalOpen(true); }} disabled={isStarting}>
                  <Play className="h-3.5 w-3.5 fill-current" /> {isStarting ? "Validando..." : "Comenzar Turno"}
                </Button>
              )}
            </div>
          </div>
        )}
      </div>

      {isAdmin && (
        <div className="grid grid-cols-2 lg:grid-cols-4 gap-4">
          {kpis.map((kpi, i) => (
            <KpiCard key={kpi.title} {...kpi} index={i} />
          ))}
        </div>
      )}

      <UnifiedSearchBar
        selectionMode={selectionMode}
        onSelectionModeToggle={hasPermission("fichajes:delete") ? () => {
          setSelectionMode(!selectionMode);
          if (selectionMode) setSelectedIds(new Set());
        } : undefined}
        bulkActions={
          hasPermission("fichajes:delete") && (
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
        searchValue={search}
        onSearchChange={setSearch}
        searchPlaceholder="Filtrar por empleado o fecha..."
        onClearFilters={() => {
          setSearch('');
          setUsuarioFilter('all');
          setTipoFilter('all');
          setDateRange({ desde: "", hasta: "" });
        }}
        showFilterBadge={usuarioFilter !== 'all' || tipoFilter !== 'all' || dateRange.desde !== "" || dateRange.hasta !== ""}
      >
        <div className="flex flex-wrap items-end gap-4 w-full">
          {isAdmin && (
            <FilterField 
              columnLabel="Empleado"
              type="select"
              value={usuarioFilter}
              onChange={setUsuarioFilter}
              options={uniqueUsuarios.map(u => ({ value: u!, label: u! }))}
              placeholder="Todos los empleados"
            />
          )}

          <FilterField 
            columnLabel="Tipo de Registro"
            type="select"
            value={tipoFilter}
            onChange={setTipoFilter}
            options={uniqueTipos.map(t => ({ value: t!, label: t! }))}
            placeholder="Todos los tipos"
          />

          <FilterField 
            columnLabel="Desde"
            type="date"
            value={dateRange.desde}
            onChange={(v) => setDateRange(prev => ({ ...prev, desde: v }))}
          />

          <FilterField 
            columnLabel="Hasta"
            type="date"
            value={dateRange.hasta}
            onChange={(v) => setDateRange(prev => ({ ...prev, hasta: v }))}
          />
        </div>
      </UnifiedSearchBar>

      {fichajes.length === 0 ? (
        <EmptyState icon={Clock} title="Sin Registros" description="No se encontró historial de actividad para este periodo." />
      ) : (
        <div className="glass rounded-xl overflow-hidden border border-border/50">
          <div className="overflow-x-auto scrollbar-thin scrollbar-thumb-primary/20 scrollbar-track-transparent">
            <table className="w-full text-sm min-w-[900px]">
              <thead>
                <tr className="border-b border-border bg-muted/20 text-muted-foreground uppercase text-[10px] tracking-widest font-bold">
                  {selectionMode && (
                    <th className="p-4 w-10">
                      <Checkbox 
                        checked={selectedIds.size === filteredFichajes.length && filteredFichajes.length > 0}
                        onCheckedChange={handleSelectAll}
                      />
                    </th>
                  )}
                  {isAdmin && <th className="text-left p-4">Empleado</th>}
                  <th className="text-left p-4">Tipo de Turno</th>
                  <th className="text-left p-4">Entrada</th>
                  <th className="text-left p-4">Salida</th>
                  <th className="text-left p-4">Pausas</th>
                  <th className="text-left p-4">Horas Extra</th>
                  <th className="text-right p-4 pr-8">Estado</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-border/30 text-foreground">
                {filteredFichajes.slice(0, displayLimit).map((f, i) => (
                    <FichajeRow 
                        key={f.id}
                        f={f}
                        index={i}
                        isAdmin={isAdmin}
                        selectionMode={selectionMode}
                        isSelected={selectedIds.has(f.id)}
                        onSelect={handleSelect}
                        onEdit={handleEdit}
                        isLargeList={filteredFichajes.length > 80}
                    />
                ))}
              </tbody>
            </table>
            {fichajes.length > displayLimit && (
                <div className="p-4 text-center border-t border-border bg-muted/5">
                    <Button variant="ghost" size="sm" onClick={() => setDisplayLimit(prev => prev + 100)} className="text-primary hover:bg-primary/5">
                        Mostrando {displayLimit} de {fichajes.length} - Cargar más...
                    </Button>
                </div>
            )}
          </div>
        </div>
      )}

      <FichajeFormModal 
        open={modalOpen} 
        onClose={() => setModalOpen(false)} 
        onSubmit={handleAction} 
        isLoading={modalMode === "create" ? isStarting : isUpdating} 
        mode={modalMode}
        initialData={editingFichaje}
      />
    </div>
  );
}
