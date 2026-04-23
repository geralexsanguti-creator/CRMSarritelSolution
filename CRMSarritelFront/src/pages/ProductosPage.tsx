import { useState, useMemo, useCallback } from "react";
import { motion, AnimatePresence } from "framer-motion";
import { Search, Plus, Filter, Package, AlertTriangle, Hash, Tag, Trash2, Edit, Upload, MoreHorizontal, DollarSign, File as FileIcon, CheckSquare, LayoutList, Columns3, FilterX, ArrowRight, Infinity, Calendar as CalendarIcon } from "lucide-react";
import { KpiCard } from "@/components/shared/KpiCard";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { StatusBadge } from "@/components/shared/StatusBadge";
import { Badge } from "@/components/ui/badge";
import { ResponsiveModal } from "@/components/shared/ResponsiveModal";
import { EmptyState } from "@/components/shared/EmptyState";
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { ProductosService } from '@/services/api/productos.service';
import { type Producto } from '@/types';
import { useIsMobile } from "@/hooks/use-mobile";
import { Separator } from "@/components/ui/separator";
import { EntityFilesModal } from "@/components/shared/EntityFilesModal";
import { ProductoFormModal } from "@/components/productos/ProductoFormModal";
import { BulkImportModal } from "@/components/shared/BulkImportModal";
import { toast } from "sonner";
import { Checkbox } from "@/components/ui/checkbox";
import { BulkActionsBar } from "@/components/shared/BulkActionsBar";
import { getInsensitive } from "@/lib/import-utils";
import { cn } from "@/lib/utils";
import { UnifiedSearchBar } from "@/components/shared/UnifiedSearchBar";
import { FilterField, type FilterOption } from "@/components/shared/FilterField";
import { useProductos } from "@/hooks/useProductos";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import React from 'react';
import { useAuth } from "@/hooks/useAuth";
import { format } from "date-fns";
import { es } from "date-fns/locale";

// Memoized table row for Products
const ProductRow = React.memo(({ 
  producto, 
  index, 
  selectionMode, 
  isSelected, 
  onSelect, 
  onClick, 
  isLargeList,
  formatAmount
}: { 
  producto: Producto, 
  index: number, 
  selectionMode: boolean, 
  isSelected: boolean, 
  onSelect: (id: number, selected: boolean) => void,
  onClick: (p: Producto) => void,
  isLargeList: boolean,
  formatAmount: (n: number) => string
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
      onClick={() => selectionMode ? null : onClick(producto)}
    >
      {!producto.activo && (
        <div className="absolute inset-0 bg-background/50 backdrop-blur-[1px] pointer-events-none" />
      )}
      {selectionMode && (
        <td className="p-4" onClick={(e) => e.stopPropagation()}>
          <Checkbox 
            checked={isSelected}
            onCheckedChange={(checked) => onSelect(producto.id, !!checked)}
          />
        </td>
      )}
      <td className="p-4 font-medium text-foreground">
        <div className="flex flex-col">
          <span>{producto.nombre}</span>
          {producto.productoCarpetas && producto.productoCarpetas.length > 0 && (
            <div className="flex flex-wrap gap-1 mt-1">
              {producto.productoCarpetas.slice(0, 2).map((pc, idx) => (
                <Badge key={idx} variant="outline" className="text-[8px] px-1 py-0 border-primary/20 text-primary bg-primary/5 uppercase font-black tracking-tighter">
                  {pc.carpetaReglas?.nombre || "Paquete"}
                </Badge>
              ))}
              {producto.productoCarpetas.length > 2 && (
                <span className="text-[8px] text-muted-foreground font-bold">+{producto.productoCarpetas.length - 2}</span>
              )}
            </div>
          )}
        </div>
      </td>
      <td className="p-4 text-muted-foreground">{producto.tipoVenta?.nombre || "Sin tipo de venta"}</td>
      <td className="p-4 text-muted-foreground text-xs">{producto.proveedor?.nombre || "Sin proveedor"}</td>
      <td className="p-4 text-right">
        {producto.productoCarpetas && producto.productoCarpetas.length > 0 ? (
          <Badge variant="outline" className="border-primary/30 text-primary bg-primary/5 font-bold uppercase text-[10px]">
            {producto.productoCarpetas[0].carpetaReglas?.nombre || "Comisión Estándar"}
          </Badge>
        ) : (
          <span className="text-[10px] text-muted-foreground italic">Sin regla</span>
        )}
      </td>
      <td className="p-4 text-right">
        {!producto.activo ? (
          <Badge variant="outline" className="bg-destructive/10 text-destructive border-destructive/20 uppercase text-[10px] font-black">Inactivo</Badge>
        ) : producto.esInfinito ? (
          <div className="flex items-center justify-end text-primary" title="Stock Ilimitado">
            <Infinity className="h-4 w-4 mr-1" />
            <span className="text-xs font-bold uppercase tracking-tighter">Ilimitado</span>
          </div>
        ) : (
          <span className={cn(
            "text-xs px-2 py-1 rounded-full font-medium", 
            producto.cantidad > 0 ? "bg-primary/10 text-primary" : "bg-destructive/10 text-destructive"
          )}>
            {producto.cantidad}
          </span>
        )}
      </td>
    </motion.tr>
  );
});

export default function ProductosPage() {
  const { hasPermission } = useAuth();
  const queryClient = useQueryClient();
  const [selectedIds, setSelectedIds] = useState<Set<number>>(new Set());
  const [selectionMode, setSelectionMode] = useState(false);
  const [view, setView] = useState<"table" | "grid">("table");
  const [search, setSearch] = useState("");
  const [tipoVentaFilter, setTipoVentaFilter] = useState("all");
  const [stockFilter, setStockFilter] = useState("all");
  const [statusFilter, setStatusFilter] = useState("active");
  const [selectedProduct, setSelectedProduct] = useState<Producto | null>(null);
  const [filesModalOpen, setFilesModalOpen] = useState(false);
  const [formModalOpen, setFormModalOpen] = useState(false);
  const [importModalOpen, setImportModalOpen] = useState(false);
  const [editingProduct, setEditingProduct] = useState<Producto | null>(null);
  const [displayLimit, setDisplayLimit] = useState(50);

  const { 
    productos, 
    isLoading, 
    createProducto, 
    updateProducto, 
    deleteProducto 
  } = useProductos();

  const handleBulkDelete = async () => {
    if (selectedIds.size === 0) return;
    if (!confirm(`¿Estás seguro de que deseas eliminar ${selectedIds.size} productos?`)) return;

    const idsArray = Array.from(selectedIds);
    const toastId = toast.loading(`Eliminando ${idsArray.length} productos... (0%)`);
    try {
      const total = idsArray.length;
      
      for (let i = 0; i < total; i++) {
        await deleteProducto(idsArray[i]);
        const progress = Math.min(100, Math.round(((i + 1) / total) * 100));
        toast.loading(`Eliminando ${idsArray.length} productos... (${progress}%)`, { id: toastId });
      }
      
      setSelectedIds(new Set());
      setSelectionMode(false);
      toast.success("Productos eliminados correctamente", { id: toastId });
    } catch (error) {
      toast.error("Error en la eliminación masiva", { id: toastId });
    }
  };


  const isMobile = useIsMobile();
  const formatAmount = useCallback((n: number) => `${n.toLocaleString("es-ES")} €`, []);

  const filtered = useMemo(() => {
    return productos.filter(p => {
      const matchSearch = search === "" || 
        p.nombre.toLowerCase().includes(search.toLowerCase()) || 
        (p.descripcion && p.descripcion.toLowerCase().includes(search.toLowerCase()));
      
      const matchTipoVenta = tipoVentaFilter === "all" || p.tipoVenta?.nombre === tipoVentaFilter;
      const matchStock = stockFilter === "all" || (stockFilter === "in_stock" ? p.cantidad > 0 : p.cantidad <= 0);
      const matchStatus = statusFilter === "all" || (statusFilter === "active" ? p.activo : !p.activo);
      return matchSearch && matchTipoVenta && matchStock && matchStatus;
    });
  }, [search, productos, tipoVentaFilter, stockFilter, statusFilter]);

  const uniqueTiposVenta = useMemo(() => {
    const tvs = new Set(productos.map(p => p.tipoVenta?.nombre).filter(Boolean));
    return Array.from(tvs);
  }, [productos]);

  const kpis = useMemo(() => {
    const totalInventoryValue = productos.reduce((acc, p) => acc + (p.precio * p.cantidad), 0);
    const outOfStockCount = productos.filter(p => p.cantidad === 0).length;
    
    return [
      { title: "Catálogo Total", value: String(productos.length), icon: Package, sparkData: [productos.length - 2, productos.length - 1, productos.length] },
      { title: "Sin Stock", value: String(outOfStockCount), icon: AlertTriangle, sparkData: [5, 4, 3, 2, outOfStockCount] },
      { title: "Valor Inventario", value: formatAmount(totalInventoryValue), icon: DollarSign, sparkData: [totalInventoryValue * 0.9, totalInventoryValue * 1.05, totalInventoryValue] },
    ];
  }, [productos, formatAmount]);

  const handleSelect = useCallback((id: number, selected: boolean) => {
    setSelectedIds(prev => {
      const next = new Set(prev);
      if (selected) next.add(id);
      else next.delete(id);
      return next;
    });
  }, []);

  const handleSelectAll = useCallback((checked: boolean) => {
    if (checked) setSelectedIds(new Set(filtered.map(p => p.id)));
    else setSelectedIds(new Set());
  }, [filtered]);

  const handleProductClick = useCallback((p: Producto) => setSelectedProduct(p), []);

  if (isLoading) {
    return (
      <div className="space-y-6">
        <div>
          <h1 className="text-2xl font-bold text-foreground">Productos</h1>
          <p className="text-sm text-muted-foreground font-medium animate-pulse">Cargando catálogo...</p>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
          {[1, 2, 3].map(i => <div key={i} className="h-32 glass animate-pulse rounded-xl" />)}
        </div>
        <div className="h-64 glass animate-pulse rounded-xl" />
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-20 sm:pb-6">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl font-bold text-foreground">Productos</h1>
          <p className="text-sm text-muted-foreground">{filtered.length} productos en catálogo</p>
        </div>
        <div className="flex items-center gap-2">
          {hasPermission("productos:create") && (
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
          </div>
          {hasPermission("productos:create") && (
            <Button className="bg-primary text-primary-foreground hover:bg-primary/90 gap-2 flex-1 sm:flex-none font-bold shadow-glow" onClick={() => { setEditingProduct(null); setFormModalOpen(true); }}>
              <Plus className="h-4 w-4" /> Nuevo Producto
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
        onSelectionModeToggle={hasPermission("productos:delete") ? () => {
          setSelectionMode(!selectionMode);
          if (selectionMode) setSelectedIds(new Set());
        } : undefined}
        bulkActions={
          hasPermission("productos:delete") && (
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
                <Trash2 className="h-3.5 w-3.5" /> Eliminar Productos
              </Button>
            </div>
          )
        }
        searchValue={search}
        onSearchChange={setSearch}
        searchPlaceholder="Buscar por nombre, código o descripción..."
        onClearFilters={() => {
          setSearch('');
          setTipoVentaFilter('all');
          setStockFilter('all');
        }}
        showFilterBadge={tipoVentaFilter !== 'all' || stockFilter !== 'all'}
      >
        <div className="flex flex-wrap items-end gap-4 w-full">
          <FilterField 
            columnLabel="Tipo de Venta"
            type="select"
            value={tipoVentaFilter}
            onChange={setTipoVentaFilter}
            options={uniqueTiposVenta.map(tv => ({ value: String(tv), label: String(tv) }))}
            placeholder="Todos los tipos"
          />

          <FilterField 
            columnLabel="Estado de Stock"
            type="select"
            value={stockFilter}
            onChange={setStockFilter}
            options={[
              { value: "in_stock", label: "Con Stock" },
              { value: "out_of_stock", label: "Sin Stock / Agotado" }
            ]}
            placeholder="Cualquier estado"
          />

          <FilterField 
            columnLabel="Actividad"
            type="select"
            value={statusFilter}
            onChange={setStatusFilter}
            options={[
              { value: "active", label: "Operativos" },
              { value: "inactive", label: "Baja / Inactivos" }
            ]}
            placeholder="Todos"
          />
        </div>
      </UnifiedSearchBar>

      {filtered.length === 0 ? (
        <EmptyState icon={Package} title="Sin productos" description="No se encontraron productos." />
      ) : (view === "grid" || isMobile) ? (
        <div className="space-y-4">
            <div className="grid gap-3 sm:grid-cols-2 lg:grid-cols-3">
            {filtered.slice(0, displayLimit).map((prod, i) => (
                <motion.div key={prod.id} initial={{ opacity: 0, y: 10 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: Math.min(i * 0.05, 0.5) }} className="glass glass-hover rounded-xl p-4 cursor-pointer relative flex flex-col group" onClick={() => selectionMode ? null : handleProductClick(prod)}>
                <div className="flex justify-between items-start mb-3">
                    <div className="flex items-center gap-2">
                    {selectionMode && (
                        <Checkbox 
                        checked={selectedIds.has(prod.id)} 
                        onCheckedChange={(checked) => handleSelect(prod.id, !!checked)}
                        onClick={(e) => e.stopPropagation()}
                        />
                    )}
                    <div>
                        <h3 className="font-bold text-sm text-foreground leading-tight group-hover:text-primary transition-colors">{prod.nombre}</h3>
                        <p className="text-[10px] text-muted-foreground">{prod.tipoVenta?.nombre || "Sin tipo de venta"}</p>
                    </div>
                    </div>
                    <div className="flex flex-col items-end gap-1">
                      <span className={cn(
                          "text-[10px] px-2 py-0.5 rounded-full font-medium", 
                          !prod.activo ? "bg-destructive/10 text-destructive border border-destructive/20 uppercase" :
                          prod.esInfinito ? "bg-primary/10 text-primary border border-primary/20" :
                          prod.cantidad > 0 ? "bg-primary/10 text-primary" : "bg-destructive/10 text-destructive"
                      )}>
                          {!prod.activo ? "INACTIVO" : prod.esInfinito ? "Ilimitado" : `${prod.cantidad} disp.`}
                      </span>
                    </div>
                </div>
                
                {prod.fechaLimite && (
                    <div className="flex items-center gap-1 mb-2 text-[9px] text-destructive bg-destructive/10 px-2 py-1 rounded-md border border-destructive/20 w-fit font-bold uppercase tracking-tighter shadow-sm shadow-destructive/20">
                        <CalendarIcon className="h-3 w-3" />
                        Límite: {format(new Date(prod.fechaLimite), "dd MMM yyyy", { locale: es })}
                    </div>
                )}
                
                {prod.descripcion && (
                    <p className="text-[11px] text-muted-foreground line-clamp-2 mb-4 flex-1">
                        {prod.descripcion}
                    </p>
                )}

                <div className="flex justify-between items-end pt-3 border-t border-white/5 mt-auto">
                    <div className="flex flex-col">
                        <span className="text-[9px] uppercase tracking-tighter text-muted-foreground mb-0.5">Regla</span>
                        <span className="text-xs font-bold text-primary">
                          {prod.productoCarpetas && prod.productoCarpetas.length > 0 
                            ? (prod.productoCarpetas[0].carpetaReglas?.nombre || "Estándar")
                            : "Sin asignar"}
                        </span>
                    </div>
                    <div className="flex items-center gap-1">
                        {hasPermission("productos:update") && (
                          <Button variant="ghost" size="icon" className="h-8 w-8 text-muted-foreground hover:text-primary transition-colors" onClick={(e) => { e.stopPropagation(); setEditingProduct(prod); setFormModalOpen(true); }}>
                              <Edit className="h-4 w-4" />
                          </Button>
                        )}
                        <div className="h-8 w-8 rounded-full bg-background flex items-center justify-center border border-border opacity-0 group-hover:opacity-100 transition-all transform translate-x-2 group-hover:translate-x-0">
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
                        Cargar más productos ({filtered.length - displayLimit} restantes)
                    </Button>
                </div>
            )}
        </div>
      ) : (
        <div className="glass rounded-xl overflow-hidden relative border border-border/50">
          <div className="overflow-x-auto scrollbar-thin scrollbar-thumb-primary/20 scrollbar-track-transparent">
            <table className="w-full text-sm min-w-[800px]">
              <thead className="sticky top-0 bg-background/95 backdrop-blur-md z-10 border-b border-border shadow-sm">
                <tr className="text-left text-muted-foreground text-xs uppercase font-bold tracking-wider">
                  {selectionMode && (
                    <th className="p-4 w-10">
                      <Checkbox 
                        checked={selectedIds.size === filtered.length && filtered.length > 0}
                        onCheckedChange={(checked) => handleSelectAll(!!checked)}
                      />
                    </th>
                  )}
                  <th className="p-4">Producto</th>
                  <th className="p-4">Tipo de Venta</th>
                  <th className="p-4">Proveedor</th>
                  <th className="p-4 text-right">Regla Aplicada</th>
                  <th className="p-4 text-right w-24">Stock</th>
                </tr>
              </thead>
                <tbody>
                {filtered.slice(0, displayLimit).map((prod, i) => (
                    <ProductRow 
                    key={prod.id}
                    producto={prod}
                    index={i}
                    selectionMode={selectionMode}
                    isSelected={selectedIds.has(prod.id)}
                    isLargeList={filtered.length > 80}
                    formatAmount={formatAmount}
                    onSelect={handleSelect}
                    onClick={handleProductClick}
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
        open={!!selectedProduct} 
        onOpenChange={(o) => !o && setSelectedProduct(null)} 
        title="Detalle del Producto"
        variant="dialog"
        maxWidth="800px"
      >
        {selectedProduct && (
          <div className="space-y-5 max-h-[70vh] overflow-y-auto pr-2 custom-scrollbar">
            <div className="flex items-center gap-4">
              <div className="h-14 w-14 rounded-xl bg-primary/10 flex items-center justify-center shrink-0">
                <Package className="h-7 w-7 text-primary" />
              </div>
              <div className="min-w-0">
                <h3 className="font-bold text-lg text-foreground truncate">{selectedProduct.nombre}</h3>
                <span className="text-xs bg-background px-2 py-1 rounded-full text-muted-foreground border border-border/50">{selectedProduct.tipoVenta?.nombre || "Sin Tipo de Venta"}</span>
              </div>
            </div>
            
            <div className="glass rounded-xl p-4 space-y-3">
                <p className="text-sm text-muted-foreground leading-relaxed">{selectedProduct.descripcion || "Sin descripción adicional."}</p>
                <div className="flex flex-wrap items-center gap-2 text-xs font-medium">
                    <span className={cn(
                        "px-2 py-1 rounded-md", 
                        selectedProduct.esInfinito ? "bg-primary/10 text-primary border border-primary/20" :
                        selectedProduct.cantidad > 0 ? "bg-primary/10 text-primary" : "bg-destructive/10 text-destructive"
                    )}>
                        {selectedProduct.esInfinito ? "Stock Ilimitado (Ilimitado)" : `Stock disponible: ${selectedProduct.cantidad} unidades`}
                    </span>
                    {selectedProduct.fechaLimite && (
                      <span className="bg-destructive/10 text-destructive px-2 py-1 rounded-md border border-destructive/20 font-bold uppercase text-[10px] flex items-center gap-1">
                        <CalendarIcon className="h-3 w-3" />
                        Caduca: {format(new Date(selectedProduct.fechaLimite), "PPPP", { locale: es })}
                      </span>
                    )}
                </div>
            </div>

            <Separator className="bg-white/5" />

            <div className="grid grid-cols-1 gap-4 text-sm font-mono bg-muted/20 p-4 rounded-xl border border-white/5">
              <div className="space-y-1">
                <span className="text-muted-foreground block text-[10px] uppercase tracking-wider font-sans">Regla Aplicable</span>
                <span className="text-primary font-bold text-lg">
                   {selectedProduct.productoCarpetas && selectedProduct.productoCarpetas.length > 0 
                     ? (selectedProduct.productoCarpetas[0].carpetaReglas?.nombre || "Estándar") 
                     : "Sin regla asignada"}
                </span>
              </div>
            </div>

            <div className="pt-4 border-t border-border/50 mt-4 flex gap-2">
              <Button variant="outline" className="flex-1 gap-2 border-primary/20 text-primary hover:bg-primary/10 font-bold shadow-sm" onClick={() => setFilesModalOpen(true)}>
                <FileIcon className="h-4 w-4" /> Archivos
              </Button>
              {hasPermission("productos:update") && (
                <Button variant="secondary" className="flex-1 gap-2 border border-border font-bold" onClick={() => { setSelectedProduct(null); setEditingProduct(selectedProduct); setFormModalOpen(true); }}>
                  <Edit className="h-4 w-4" /> Editar info
                </Button>
              )}
            </div>
          </div>
        )}
      </ResponsiveModal>

      {/* Add/Edit Modal */}
      <ProductoFormModal
        open={formModalOpen}
        onClose={() => { setFormModalOpen(false); setEditingProduct(null); }}
        product={editingProduct}
        onSave={async (data) => {
          try {
            if (editingProduct) {
              await updateProducto({ id: editingProduct.id, data });
              toast.success("Producto actualizado");
            } else {
              await createProducto(data);
              toast.success("Producto creado");
            }
            setFormModalOpen(false);
          } catch (e) {
            toast.error("Error al guardar el producto");
          }
        }}
      />
      
      <EntityFilesModal isOpen={filesModalOpen} onClose={() => setFilesModalOpen(false)} entidadTipo="Producto" entidadId={selectedProduct?.id || null} entidadNombre={selectedProduct?.nombre} />
      
      <BulkImportModal open={importModalOpen} onClose={() => setImportModalOpen(false)} entityName="Productos" onImport={async (data, onProgress) => {
          let successCount = 0;
          let errorCount = 0;
          const total = data.length;
          for (let i = 0; i < total; i++) {
              const row = data[i];
              try {
                  const prodData = {
                      nombre: getInsensitive(row, ["nombre", "producto", "item", "name", "articulo"]) || "Sin Nombre",
                      descripcion: getInsensitive(row, ["descripcion", "description", "detalles", "info"]) || "Sin descripción",
                      precio: Number(getInsensitive(row, ["precio", "price", "costo", "monto"]) || 0),
                      precioOferta: Number(getInsensitive(row, ["oferta", "precio_oferta", "discount_price", "promocion"]) || getInsensitive(row, ["precio", "price"]) || 0),
                      cantidad: Number(getInsensitive(row, ["stock", "cantidad", "quantity", "stock_actual", "inventario"]) || 0),
                      tipoVentaId: Number(getInsensitive(row, ["tipoventaid", "tipo_venta_id", "id_tipoventa", "tipo_venta"]) || 1),
                      imagen: getInsensitive(row, ["imagen", "image", "foto", "url"]) || "default.png",
                      fechaCreation: new Date().toISOString()
                  };
                  await createProducto(prodData as any);
                  successCount++;
              } catch (err) {
                  errorCount++;
              }
              onProgress(Math.round(((i + 1) / total) * 100));
          }
          if (errorCount > 0) toast.warning(`Importación parcial: ${successCount} exitosos, ${errorCount} fallidos.`);
          else toast.success(`Se han importado ${successCount} productos correctamente`);
      }} requiredFields={["Nombre"]} optionalFields={["Descripcion", "Precio", "Oferta", "Stock", "TipoVentaId"]} />
      
      {isMobile && !isLoading && hasPermission("productos:create") && (
        <motion.button whileTap={{ scale: 0.9 }} className="fixed bottom-6 right-6 h-14 w-14 rounded-full bg-primary text-primary-foreground shadow-glow flex items-center justify-center z-40" onClick={() => { setEditingProduct(null); setFormModalOpen(true); }}>
          <Plus className="h-6 w-6" />
        </motion.button>
      )}
    </div>
  );
}
