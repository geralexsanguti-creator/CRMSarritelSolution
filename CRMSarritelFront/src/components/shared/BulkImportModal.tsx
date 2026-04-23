import { useState, useRef } from "react";
import { motion, AnimatePresence } from "framer-motion";
import { ResponsiveModal } from "@/components/shared/ResponsiveModal";
import { Button } from "@/components/ui/button";
import { Upload, FileText, CheckCircle2, AlertCircle, Trash2, Database } from "lucide-react";
import { ScrollArea } from "@/components/ui/scroll-area";
import * as XLSX from "xlsx";
import { toast } from "sonner";
import { cn } from "@/lib/utils";

export function BulkImportModal({ open, onClose, entityName, onImport, requiredFields = [], optionalFields = [] }: { 
    open: boolean; 
    onClose: () => void; 
    entityName: string;
    onImport: (data: any[], onProgress: (p: number) => void) => Promise<void>;
    requiredFields?: string[];
    optionalFields?: string[];
}) {
  const [data, setData] = useState<any[]>([]);
  const [headers, setHeaders] = useState<string[]>([]);
  const [isProcessing, setIsProcessing] = useState(false);
  const [progress, setProgress] = useState(0);
  const [loadingFile, setLoadingFile] = useState(false);
  const fileInputRef = useRef<HTMLInputElement>(null);

  const handleFileUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;

    setLoadingFile(true);
    const reader = new FileReader();
    reader.onload = (evt) => {
      try {
        const bstr = evt.target?.result;
        const wb = XLSX.read(bstr, { type: "binary" });
        const wsname = wb.SheetNames[0];
        const ws = wb.Sheets[wsname];
        const json = XLSX.utils.sheet_to_json(ws);
        
        if (json.length > 0) {
          setData(json);
          setHeaders(Object.keys(json[0] as object));
          toast.success(`Se han cargado ${json.length} filas del archivo`);
        } else {
          toast.error("El archivo parece estar vacío");
        }
      } catch (err) {
        toast.error("Error al procesar el archivo Excel/CSV");
        console.error(err);
      } finally {
        setLoadingFile(false);
      }
    };
    reader.readAsBinaryString(file);
  };

  const handleImport = async () => {
    if (data.length === 0) return;
    setIsProcessing(true);
    setProgress(0);
    try {
      await onImport(data, (p) => setProgress(p));
      toast.success(`Importación de ${entityName} completada con éxito`);
      onClose();
      setData([]);
    } catch (error) {
      toast.error("Error durante la importación masiva");
    } finally {
      setIsProcessing(false);
      setProgress(0);
    }
  };

  const clearData = () => {
    setData([]);
    setHeaders([]);
    if (fileInputRef.current) fileInputRef.current.value = "";
  };

  return (
    <ResponsiveModal 
      open={open} 
      onOpenChange={(o) => !o && onClose()} 
      title={`Importador Masivo de ${entityName}`}
    >
      <div className="space-y-6">
        {data.length === 0 ? (
          <div 
            onClick={() => fileInputRef.current?.click()}
            className="border-2 border-dashed border-border rounded-2xl p-12 text-center hover:border-primary/50 hover:bg-primary/5 cursor-pointer transition-all group"
          >
            <input type="file" ref={fileInputRef} onChange={handleFileUpload} accept=".xlsx, .xls, .csv" className="hidden" />
            <div className="flex flex-col items-center">
              <div className="h-16 w-16 rounded-full bg-primary/10 flex items-center justify-center mb-4 group-hover:scale-110 transition-transform">
                <Upload className="h-8 w-8 text-primary" />
              </div>
              <h3 className="text-lg font-semibold text-foreground">Selecciona tu archivo</h3>
              <p className="text-sm text-muted-foreground mt-1 max-w-xs mx-auto">
                Arrastra y suelta tu archivo Excel (.xlsx) o CSV aquí para comenzar la gestión de ingesta.
              </p>
              
              <div className="mt-8 w-full max-w-md glass p-4 rounded-xl border border-border/50 text-left">
                <p className="text-[10px] font-bold text-muted-foreground uppercase tracking-wider mb-2 flex items-center gap-2">
                  <Database className="h-3 w-3" /> Esquema de Ingesta Sugerido
                </p>
                <div className="space-y-3">
                  {requiredFields.length > 0 && (
                    <div className="flex flex-wrap gap-1.5 items-center">
                      <span className="text-[10px] text-destructive font-semibold mr-1 shrink-0">OBLIGATORIO:</span>
                      {requiredFields.map(f => <Badge key={f} className="bg-destructive/10 text-destructive border-destructive/20">{f}</Badge>)}
                    </div>
                  )}
                  {optionalFields.length > 0 && (
                    <div className="flex flex-wrap gap-1.5 items-center">
                      <span className="text-[10px] text-muted-foreground font-semibold mr-1 shrink-0">OPCIONAL:</span>
                      {optionalFields.map(f => <Badge key={f} variant="outline" className="opacity-80">{f}</Badge>)}
                    </div>
                  )}
                </div>
              </div>

              <div className="mt-6 flex gap-2">
                 <Badge variant="outline" className="bg-muted/50">.xlsx</Badge>
                 <Badge variant="outline" className="bg-muted/50">.csv</Badge>
                 <Badge variant="outline" className="bg-muted/50">.xls</Badge>
              </div>
            </div>
          </div>
        ) : (
          <div className="space-y-4">
            <div className="flex items-center justify-between">
              <div className="flex items-center gap-2">
                <div className="h-8 w-8 rounded-lg bg-success/10 flex items-center justify-center">
                  <FileText className="h-4 w-4 text-success" />
                </div>
                <div>
                  <p className="text-sm font-bold text-foreground">Vista previa de datos</p>
                  <p className="text-xs text-muted-foreground">{data.length} registros listos para procesar</p>
                </div>
              </div>
              <Button variant="ghost" size="sm" onClick={clearData} className="text-destructive h-8 px-2">
                <Trash2 className="h-4 w-4 mr-2" /> Limpiar
              </Button>
            </div>

            <div className="glass rounded-xl overflow-hidden border border-border">
              <ScrollArea className="h-64">
                <table className="w-full text-xs">
                  <thead className="sticky top-0 bg-background/95 backdrop-blur-sm z-10">
                    <tr className="border-b border-border">
                      {headers.map(h => (
                        <th key={h} className="text-left p-2 font-medium text-muted-foreground bg-muted/30">{h}</th>
                      ))}
                    </tr>
                  </thead>
                  <tbody>
                    {data.slice(0, 10).map((row, i) => (
                      <tr key={i} className="border-b border-border/50 hover:bg-muted/30">
                        {headers.map(h => (
                          <td key={h} className="p-2 text-foreground/80 max-w-[150px] truncate">{row[h]?.toString()}</td>
                        ))}
                      </tr>
                    ))}
                  </tbody>
                </table>
                {data.length > 10 && (
                  <div className="p-3 text-center text-[10px] text-muted-foreground border-t border-border/50">
                    ... y {data.length - 10} filas más
                  </div>
                )}
              </ScrollArea>
            </div>

            <div className="bg-warning/10 border border-warning/20 rounded-lg p-3 flex gap-3">
              <AlertCircle className="h-5 w-5 text-warning shrink-0" />
              <div className="space-y-1">
                <p className="text-xs font-bold text-warning uppercase tracking-wider">Aviso de Ingesta</p>
                <p className="text-[11px] text-muted-foreground leading-relaxed">
                  Asegúrate de que las columnas coincidan con el esquema mandatorio (Nombre, Email, Movil). Las filas duplicadas o vacías serán descartadas automáticamente ante la presión del DB Context.
                </p>
              </div>
            </div>

            {isProcessing && (
              <div className="space-y-2 pt-2">
                <div className="flex justify-between items-center text-[10px] uppercase font-bold tracking-widest text-primary">
                  <span>Procesando registros...</span>
                  <span>{progress}%</span>
                </div>
                <div className="h-1.5 w-full bg-primary/10 rounded-full overflow-hidden">
                  <motion.div 
                    initial={{ width: 0 }}
                    animate={{ width: `${progress}%` }}
                    className="h-full bg-primary"
                  />
                </div>
              </div>
            )}
          </div>
        )}

        <div className="flex justify-end gap-2 pt-4 border-t border-border">
          <Button variant="ghost" onClick={onClose} disabled={isProcessing}>Cancelar</Button>
          <Button 
            onClick={handleImport} 
            disabled={data.length === 0 || isProcessing}
            className="gap-2 min-w-[140px]"
          >
            {isProcessing ? "Procesando..." : <><Database className="h-4 w-4" /> Ejecutar Ingesta</>}
          </Button>
        </div>
      </div>
    </ResponsiveModal>
  );
}

function Badge({ children, variant, className }: any) {
  return (
    <span className={cn("px-2 py-0.5 rounded text-[10px] font-bold uppercase", variant === "outline" ? "border border-border text-muted-foreground" : "bg-primary text-primary-foreground", className)}>
      {children}
    </span>
  );
}
