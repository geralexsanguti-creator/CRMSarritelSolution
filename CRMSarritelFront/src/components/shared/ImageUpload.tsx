import { useState, useRef } from "react";
import { Button } from "@/components/ui/button";
import { Upload, X, Image as ImageIcon, Loader2, FileText, Video } from "lucide-react";
import { FilesService } from "@/services/api/files.service";
import { toast } from "sonner";
import { cn, getUploadUrl } from "@/lib/utils";

interface ImageUploadProps {
  value?: string;
  onChange: (url: string) => void;
  className?: string;
  placeholder?: string;
}

export function ImageUpload({ value, onChange, className, placeholder = "Subir Imagen" }: ImageUploadProps) {
  const [uploading, setUploading] = useState(false);
  const inputRef = useRef<HTMLInputElement>(null);

  const imageUrl = getUploadUrl(value);

  const isPdf = value?.toLowerCase().includes(".pdf");
  const isVideo = value?.match(/\.(mp4|avi|mov|mkv|webm)/i);

  const handleFileChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;

    if (!file.type.startsWith('image/') && file.type !== 'application/pdf' && !file.type.startsWith('video/')) {
        toast.error("El formato de archivo no está permitido (solo Imágenes, PDFs o Videos).");
        return;
    }

    if (file.size > 50 * 1024 * 1024) {
        toast.error("El archivo supera el límite de 50 MB.");
        return;
    }

    setUploading(true);
    try {
      const fileName = await FilesService.uploadImage(file);
      onChange(fileName);
      toast.success("Archivo subido con éxito");
    } catch (error) {
      toast.error("Error al subir el archivo");
      console.error(error);
    } finally {
      setUploading(false);
      if (inputRef.current) inputRef.current.value = "";
    }
  };

  const clearImage = (e: React.MouseEvent) => {
    e.stopPropagation();
    onChange("default.png");
  };

  return (
    <div className={cn("relative group border-2 border-dashed border-border/50 rounded-xl overflow-hidden hover:border-primary/50 transition-colors bg-background/50 flex flex-col items-center justify-center w-full h-full min-h-[80px]", className)} onClick={() => inputRef.current?.click()}>
      {uploading ? (
         <div className="flex flex-col items-center gap-2 text-muted-foreground p-4">
           <Loader2 className="w-6 h-6 animate-spin text-primary" />
           <span className="text-[10px] uppercase font-bold tracking-wider">Subiendo...</span>
         </div>
      ) : imageUrl ? (
         <>
           {isVideo ? (
               <video src={imageUrl} className="w-full h-full object-cover absolute inset-0 z-0" muted loop autoPlay playsInline />
           ) : isPdf ? (
               <div className="w-full h-full absolute inset-0 z-0 flex flex-col items-center justify-center bg-muted/20 p-2">
                   <FileText className="w-8 h-8 text-destructive mb-1" />
                   <span className="text-[9px] text-muted-foreground break-all text-center line-clamp-2 px-2 max-w-[90%]">{value?.split('_').pop() || "Documento PDF"}</span>
               </div>
           ) : (
               <img src={imageUrl} alt="Upload preview" className="w-full h-full object-cover absolute inset-0 z-0" />
           )}
           <div className="absolute inset-0 bg-black/50 opacity-0 group-hover:opacity-100 transition-opacity z-10 flex items-center justify-center">
             <Button type="button" variant="ghost" size="icon" className="text-white hover:text-white hover:bg-white/20">
               <Upload className="w-5 h-5" />
             </Button>
           </div>
           <Button type="button" variant="destructive" size="icon" className="absolute top-2 right-2 h-6 w-6 rounded-full opacity-0 group-hover:opacity-100 transition-opacity z-20" onClick={clearImage}>
             <X className="w-3 h-3" />
           </Button>
         </>
      ) : (
         <div className="flex flex-col items-center gap-2 p-4 text-muted-foreground hover:text-primary transition-colors cursor-pointer">
           <ImageIcon className="w-6 h-6 opacity-50" />
           <span className="text-[10px] uppercase font-bold tracking-wider">{placeholder}</span>
         </div>
      )}
      <input 
        ref={inputRef}
        type="file" 
        className="hidden" 
        accept="image/*, application/pdf, video/*"
        onChange={handleFileChange}
      />
    </div>
  );
}
