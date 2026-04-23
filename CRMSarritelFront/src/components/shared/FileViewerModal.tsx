import { useState, useEffect } from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Download, File, Loader2 } from "lucide-react";
import { ArchivoAdjunto } from "@/types";
import { archivosService } from "@/services/archivosService";
import { useToast } from "@/hooks/use-toast";

interface FileViewerModalProps {
  isOpen: boolean;
  onClose: () => void;
  archivo: ArchivoAdjunto | null;
}

export const FileViewerModal = ({ isOpen, onClose, archivo }: FileViewerModalProps) => {
  const [blobUrl, setBlobUrl] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const { toast } = useToast();

  useEffect(() => {
    if (!isOpen || !archivo) {
      if (blobUrl) {
        URL.revokeObjectURL(blobUrl);
        setBlobUrl(null);
      }
      return;
    }

    const fetchFile = async () => {
      setIsLoading(true);
      try {
        const blob = await archivosService.descargarArchivo(archivo.id);
        const url = URL.createObjectURL(blob);
        setBlobUrl(url);
      } catch (error) {
        console.error("Error al descargar el archivo:", error);
        toast({
          variant: "destructive",
          title: "Error",
          description: "No se pudo cargar el archivo para su visualización.",
        });
      } finally {
        setIsLoading(false);
      }
    };

    fetchFile();
  }, [isOpen, archivo]);

  const handleDownload = () => {
    if (!blobUrl || !archivo) return;
    const a = document.createElement("a");
    a.href = blobUrl;
    a.download = archivo.nombreArchivo;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
  };

  if (!archivo) return null;

  const isImage = archivo.tipoMime.startsWith("image/");
  const isPdf = archivo.tipoMime === "application/pdf";
  const isViewable = isImage || isPdf;

  return (
    <Dialog open={isOpen} onOpenChange={(open) => !open && onClose()}>
      <DialogContent className="max-w-7xl w-[95vw] h-[90vh] flex flex-col p-6 bg-background gap-4">
        <DialogHeader className="flex flex-row items-center justify-between space-y-0 pb-2 shrink-0">
          <div className="flex flex-col space-y-1">
            <DialogTitle className="font-semibold text-xl line-clamp-1" title={archivo.nombreArchivo}>
              {archivo.nombreArchivo}
            </DialogTitle>
            <span className="text-sm text-muted-foreground">
              {(archivo.tamanoBytes / 1024 / 1024).toFixed(2)} MB &bull; {archivo.tipoMime}
            </span>
          </div>
          <div className="flex items-center space-x-2">
            <Button variant="default" size="default" onClick={handleDownload} disabled={!blobUrl || isLoading}>
              <Download className="h-4 w-4 mr-2" />
              Descargar
            </Button>
          </div>
        </DialogHeader>

        <div className="flex-1 bg-muted/30 rounded-md overflow-hidden flex items-center justify-center relative border">
          {isLoading ? (
            <div className="flex flex-col items-center justify-center text-muted-foreground">
              <Loader2 className="h-10 w-10 animate-spin mb-4" />
              <span>Cargando previsualización...</span>
            </div>
          ) : !blobUrl ? (
            <div className="flex flex-col items-center justify-center text-muted-foreground p-8 text-center">
              <File className="h-16 w-16 mb-4 text-destructive/50" />
              <p>El archivo no está disponible o hubo un error al cargar.</p>
            </div>
          ) : isViewable ? (
            isImage ? (
              <img
                src={blobUrl}
                alt={archivo.nombreArchivo}
                className="max-w-full max-h-full object-contain"
              />
            ) : (
              <iframe
                src={blobUrl}
                title={archivo.nombreArchivo}
                className="w-full h-full border-0"
              />
            )
          ) : (
            <div className="flex flex-col items-center justify-center text-muted-foreground p-8 text-center bg-card rounded-lg shadow-sm border max-w-sm">
              <File className="h-16 w-16 mb-4 text-muted-foreground/50" />
              <h3 className="font-semibold text-lg mb-2 text-foreground">Vista previa no soportada</h3>
              <p className="text-sm mb-6">
                Este tipo de archivo no puede visualizarse directamente en el navegador. Por favor, descárgalo para verlo.
              </p>
              <Button onClick={handleDownload} variant="outline" className="w-full">
                <Download className="h-4 w-4 mr-2" />
                Descargar Archivo Localmente
              </Button>
            </div>
          )}
        </div>
      </DialogContent>
    </Dialog>
  );
};
