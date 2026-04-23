import { useState, useMemo } from "react";
import { motion } from "framer-motion";
import { FileSignature, Search, Plus, FileCheck, FileClock, FileX } from "lucide-react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { StatusBadge } from "@/components/shared/StatusBadge";
import { ResponsiveModal } from "@/components/shared/ResponsiveModal";
import { EmptyState } from "@/components/shared/EmptyState";
import { useIsMobile } from "@/hooks/use-mobile";
import { Separator } from "@/components/ui/separator";
import { useContratos } from "@/hooks/useContratos";
import { Contrato } from "@/types";
import { EntityFilesModal } from "@/components/shared/EntityFilesModal";
import { File as FileIcon, Trash2, CheckSquare } from "lucide-react";
import { useAuth } from "@/hooks/useAuth";
import { UnifiedSearchBar } from "@/components/shared/UnifiedSearchBar";

export default function ContratosPage() {
  const { hasPermission } = useAuth();
  const { data: contratos = [], isLoading } = useContratos();
  const [search, setSearch] = useState("");
  const [selectedContrato, setSelectedContrato] = useState<Contrato | null>(null);
  const [filesModalOpen, setFilesModalOpen] = useState(false);
  const isMobile = useIsMobile();

  const filtered = useMemo(() => {
    return contratos.filter((c) => {
      // With real data, we usually search by string fields.
      return c.nombreArchivo?.toLowerCase().includes(search.toLowerCase()) || false;
    });
  }, [search, contratos]);

  const firmados = contratos.filter(c => c.estado === "Firmado").length;
  const pendientes = contratos.filter(c => c.estado === "Pendiente Firma").length;
  const borradores = contratos.filter(c => c.estado === "Borrador").length;

  const kpis = [
    { title: "Firmados", value: String(firmados), icon: FileCheck, sparkData: [2, 3, 3, 4, 4, 5, 5] },
    { title: "Pendiente Firma", value: String(pendientes), icon: FileClock, sparkData: [1, 2, 1, 2, 1, 1, 2] },
    { title: "Borradores", value: String(borradores), icon: FileX, sparkData: [1, 1, 2, 1, 1, 2, 1] },
    { title: "Total", value: String(contratos.length), icon: FileSignature, sparkData: [3, 4, 5, 5, 6, 6, 7] },
  ];

  const formatSize = (bytes: number | null) => {
    if (!bytes) return "—";
    return `${(bytes / 1024).toFixed(0)} KB`;
  };

  if (isLoading) {
    return <div className="p-8 text-center text-muted-foreground animate-pulse">Cargando contratos...</div>;
  }

  return (
    <div className="space-y-6">
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl font-bold text-foreground">Contratos</h1>
          <p className="text-sm text-muted-foreground">{filtered.length} contratos registrados</p>
        </div>
        {hasPermission("contratos:create") && (
          <Button className="bg-primary text-primary-foreground hover:bg-primary/90 gap-2 w-full sm:w-auto font-bold shadow-glow">
            <Plus className="h-4 w-4" /> Nuevo Contrato
          </Button>
        )}
      </div>

      <div className={`grid gap-4 ${isMobile ? "grid-cols-2" : "grid-cols-4"}`}>
        {kpis.map((kpi, i) => (<KpiCard key={kpi.title} {...kpi} index={i} />))}
      </div>

      <UnifiedSearchBar
        selectionMode={false} // Currently not implemented for Contratos
        searchValue={search}
        onSearchChange={setSearch}
        searchPlaceholder="Buscar por nombre de archivo..."
        onClearFilters={() => setSearch('')}
      />

      {filtered.length === 0 ? (
        <EmptyState icon={FileSignature} title="Sin contratos" description="No hay contratos que coincidan con tu búsqueda." />
      ) : isMobile ? (
        <div className="grid gap-3">
          {filtered.map((contrato, i) => {
            return (
              <motion.div key={contrato.id} initial={{ opacity: 0, y: 10 }} animate={{ opacity: 1, y: 0 }} transition={{ delay: i * 0.05 }} className="glass glass-hover rounded-xl p-4 cursor-pointer" onClick={() => setSelectedContrato(contrato)}>
                <div className="flex justify-between items-start mb-2">
                  <div>
                    <p className="font-semibold text-sm text-foreground">Cliente ID {contrato.idCliente}</p>
                  </div>
                  <StatusBadge status={contrato.estado || "Borrador"} />
                </div>
                <div className="flex justify-between items-end text-xs text-muted-foreground">
                  <span>{contrato.fecha || "Sin fecha"}</span>
                  <span>{contrato.checkContrato ? "✅ Verificado" : "⏳ Sin verificar"}</span>
                </div>
              </motion.div>
            );
          })}
        </div>
      ) : (
        <div className="glass rounded-xl overflow-hidden">
          <table className="w-full text-sm">
            <thead>
              <tr className="border-b border-border">
                <th className="text-left p-4 font-medium text-muted-foreground">Cliente</th>
                <th className="text-left p-4 font-medium text-muted-foreground">Comercial</th>
                <th className="text-left p-4 font-medium text-muted-foreground">Producto</th>
                <th className="text-left p-4 font-medium text-muted-foreground">Archivo</th>
                <th className="text-left p-4 font-medium text-muted-foreground">Fecha</th>
                <th className="text-center p-4 font-medium text-muted-foreground">Check</th>
                <th className="text-left p-4 font-medium text-muted-foreground">Estado</th>
              </tr>
            </thead>
            <tbody>
              {filtered.map((contrato, i) => {
                return (
                  <motion.tr key={contrato.id} initial={{ opacity: 0 }} animate={{ opacity: 1 }} transition={{ delay: i * 0.03 }} className="border-b border-border hover:bg-muted/50 cursor-pointer transition-colors" onClick={() => setSelectedContrato(contrato)}>
                    <td className="p-4 font-medium text-foreground">ID {contrato.idCliente}</td>
                    <td className="p-4 text-muted-foreground">ID {contrato.idComercial}</td>
                    <td className="p-4 text-muted-foreground">ID {contrato.idProducto}</td>
                    <td className="p-4 text-muted-foreground text-xs font-mono">{contrato.nombreArchivo || "Sin archivo"}</td>
                    <td className="p-4 text-muted-foreground">{contrato.fecha || "—"}</td>
                    <td className="p-4 text-center">{contrato.checkContrato ? "✅" : "—"}</td>
                    <td className="p-4"><StatusBadge status={contrato.estado || "Borrador"} /></td>
                  </motion.tr>
                );
              })}
            </tbody>
          </table>
        </div>
      )}

      <ResponsiveModal open={!!selectedContrato} onOpenChange={(o) => !o && setSelectedContrato(null)} title="Detalle del Contrato">
        {selectedContrato && (() => {
          return (
            <div className="space-y-5">
              <div className="flex items-center gap-4">
                <div className="h-14 w-14 rounded-xl bg-primary/10 flex items-center justify-center">
                  <FileSignature className="h-7 w-7 text-primary" />
                </div>
                <div>
                  <h3 className="font-bold text-lg text-foreground">Cliente ID {selectedContrato.idCliente}</h3>
                  <StatusBadge status={selectedContrato.estado || "Borrador"} />
                </div>
              </div>
              <div className="space-y-3 text-sm">
                <div className="flex justify-between"><span className="text-muted-foreground">Comercial</span><span className="text-foreground">{selectedContrato.idComercial || "—"}</span></div>
                <div className="flex justify-between"><span className="text-muted-foreground">Producto</span><span className="text-foreground">{selectedContrato.idProducto || "—"}</span></div>
                <div className="flex justify-between"><span className="text-muted-foreground">Fecha</span><span className="text-foreground">{selectedContrato.fecha || "—"}</span></div>
                <div className="flex justify-between"><span className="text-muted-foreground">Verificado</span><span className="text-foreground">{selectedContrato.checkContrato ? "Sí" : "No"}</span></div>
                <div className="flex justify-between"><span className="text-muted-foreground">Versión</span><span className="text-foreground font-mono">v{selectedContrato.versionDocumento}</span></div>
              </div>
              {selectedContrato.nombreArchivo && (
                <>
                  <Separator className="bg-white/10" />
                  <div className="space-y-2 text-sm">
                    <h4 className="text-xs font-medium text-muted-foreground uppercase tracking-wider">Archivo</h4>
                    <div className="glass rounded-lg p-3 space-y-1">
                      <p className="text-foreground font-mono text-xs">{selectedContrato.nombreArchivo}</p>
                      <p className="text-muted-foreground text-xs">{selectedContrato.tipoMime} · {formatSize(selectedContrato.tamanoArchivo)}</p>
                    </div>
                  </div>
                </>
              )}
              {selectedContrato.comentariosArchivo && (
                <div><h4 className="text-xs font-medium text-muted-foreground uppercase tracking-wider mb-2">Comentarios</h4><p className="text-sm text-foreground">{selectedContrato.comentariosArchivo}</p></div>
              )}
              <div className="pt-4 border-t border-border mt-4 flex flex-col gap-2">
                {hasPermission("contratos:update") && (
                  <Button variant="outline" className="w-full gap-2 border-primary/20 text-primary hover:bg-primary/10 font-bold shadow-sm" onClick={() => setFilesModalOpen(true)}>
                    <FileIcon className="h-4 w-4" />
                    Gestionar Archivos Adjuntos
                  </Button>
                )}
              </div>
            </div>
          );
        })()}
      </ResponsiveModal>

      <EntityFilesModal
        isOpen={filesModalOpen}
        onClose={() => setFilesModalOpen(false)}
        entidadTipo="Contrato"
        entidadId={selectedContrato?.id || null}
        entidadNombre={`Contrato ${selectedContrato?.id || ''}`}
      />
    </div>
  );
}
