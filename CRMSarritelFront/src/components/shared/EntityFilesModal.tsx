import { useState } from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogDescription } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Trash2, File as FileIcon, UploadCloud, Loader2, Eye } from "lucide-react";
import { ArchivoAdjunto } from "@/types";
import { archivosService } from "@/services/archivosService";
import { useToast } from "@/hooks/use-toast";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { FileViewerModal } from "./FileViewerModal";

interface EntityFilesModalProps {
  isOpen: boolean;
  onClose: () => void;
  entidadTipo: string;
  entidadId: number | null;
  entidadNombre?: string;
}

export const EntityFilesModal = ({ isOpen, onClose, entidadTipo, entidadId, entidadNombre }: EntityFilesModalProps) => {
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [fileToView, setFileToView] = useState<ArchivoAdjunto | null>(null);
  const { toast } = useToast();
  const queryClient = useQueryClient();

  const { data: archivos = [], isLoading } = useQuery({
    queryKey: ['archivos', entidadTipo, entidadId],
    queryFn: () => archivosService.getArchivosPorEntidad(entidadTipo, entidadId!),
    enabled: isOpen && !!entidadId,
  });

  const uploadMutation = useMutation({
    mutationFn: (file: File) => archivosService.subirArchivo(entidadTipo, entidadId!, file),
    onSuccess: () => {
      toast({ title: "Éxito", description: "Archivo subido correctamente." });
      queryClient.invalidateQueries({ queryKey: ['archivos', entidadTipo, entidadId] });
      setSelectedFile(null);
      // Reset input localmente usando el ID
      const fileInput = document.getElementById("file-upload-input") as HTMLInputElement;
      if (fileInput) fileInput.value = "";
    },
    onError: () => {
      toast({ variant: "destructive", title: "Error", description: "No se pudo subir el archivo." });
    }
  });

  const deleteMutation = useMutation({
    mutationFn: (id: number) => archivosService.eliminarArchivo(id),
    onSuccess: () => {
      toast({ title: "Éxito", description: "Archivo eliminado." });
      queryClient.invalidateQueries({ queryKey: ['archivos', entidadTipo, entidadId] });
    },
    onError: () => {
      toast({ variant: "destructive", title: "Error", description: "No se pudo eliminar el archivo." });
    }
  });

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files.length > 0) {
      setSelectedFile(e.target.files[0]);
    }
  };

  const handleUpload = () => {
    if (selectedFile && entidadId) {
      uploadMutation.mutate(selectedFile);
    }
  };

  return (
    <>
      <Dialog open={isOpen} onOpenChange={(open) => !open && onClose()}>
        <DialogContent className="max-w-3xl">
          <DialogHeader>
            <DialogTitle>Archivos Adjuntos</DialogTitle>
            <DialogDescription>
              Gestiona los documentos para {entidadTipo} {entidadNombre ? `"${entidadNombre}"` : `#${entidadId}`}.
            </DialogDescription>
          </DialogHeader>

          <div className="flex flex-col gap-4 py-4">
            <div className="flex items-center gap-2">
              <Input
                id="file-upload-input"
                type="file"
                className="flex-1"
                onChange={handleFileChange}
                disabled={uploadMutation.isPending}
              />
              <Button onClick={handleUpload} disabled={!selectedFile || uploadMutation.isPending}>
                {uploadMutation.isPending ? <Loader2 className="h-4 w-4 mr-2 animate-spin" /> : <UploadCloud className="h-4 w-4 mr-2" />}
                Subir
              </Button>
            </div>

            <div className="border rounded-md divide-y overflow-hidden mt-2">
              {isLoading ? (
                <div className="p-8 flex justify-center text-muted-foreground">
                  <Loader2 className="h-6 w-6 animate-spin" />
                </div>
              ) : archivos.length === 0 ? (
                <div className="p-8 flex flex-col items-center justify-center text-muted-foreground text-sm text-center">
                  <FileIcon className="h-10 w-10 mb-2 opacity-50" />
                  No hay archivos adjuntos.
                </div>
              ) : (
                archivos.map((archivo) => (
                  <div key={archivo.id} className="flex items-center justify-between p-3 hover:bg-muted/50 transition-colors">
                    <div className="flex items-center gap-3 overflow-hidden">
                      <div className="bg-primary/10 p-2 rounded-md shrink-0">
                        <FileIcon className="h-5 w-5 text-primary" />
                      </div>
                      <div className="flex flex-col overflow-hidden">
                        <span className="font-medium text-sm truncate" title={archivo.nombreArchivo}>
                          {archivo.nombreArchivo}
                        </span>
                        <span className="text-xs text-muted-foreground">
                          {new Date(archivo.fechaCreacion).toLocaleDateString()} &bull; {(archivo.tamanoBytes / 1024).toFixed(1)} KB
                        </span>
                      </div>
                    </div>
                    <div className="flex items-center gap-1 shrink-0">
                      <Button variant="ghost" size="icon" onClick={() => setFileToView(archivo)} title="Visualizar">
                        <Eye className="h-4 w-4" />
                      </Button>
                      <Button variant="ghost" size="icon" className="text-destructive hover:bg-destructive/10 hover:text-destructive" onClick={() => deleteMutation.mutate(archivo.id)} disabled={deleteMutation.isPending}>
                        {deleteMutation.isPending ? <Loader2 className="h-4 w-4 animate-spin" /> : <Trash2 className="h-4 w-4" />}
                      </Button>
                    </div>
                  </div>
                ))
              )}
            </div>
          </div>
        </DialogContent>
      </Dialog>

      <FileViewerModal
        isOpen={!!fileToView}
        onClose={() => setFileToView(null)}
        archivo={fileToView}
      />
    </>
  );
};
