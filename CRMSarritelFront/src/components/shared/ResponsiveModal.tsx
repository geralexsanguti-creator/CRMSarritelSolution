import { ReactNode } from "react";
import { Sheet, SheetContent, SheetDescription, SheetHeader, SheetTitle } from "@/components/ui/sheet";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { useIsMobile } from "@/hooks/use-mobile";
import { cn } from "@/lib/utils";

interface ResponsiveModalProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  title: string;
  children: ReactNode;
  variant?: "sheet" | "dialog";
  maxWidth?: string;
}

export function ResponsiveModal({ 
  open, 
  onOpenChange, 
  title, 
  children, 
  variant = "sheet",
  maxWidth = "800px" 
}: ResponsiveModalProps) {
  const isMobile = useIsMobile();

  // On mobile, we always use a bottom sheet for better UX
  if (isMobile) {
    return (
      <Sheet open={open} onOpenChange={onOpenChange}>
        <SheetContent
          side="bottom"
          className="glass border-border overflow-y-auto"
          style={{ height: "90vh", borderTopLeftRadius: "1.5rem", borderTopRightRadius: "1.5rem" }}
        >
          <SheetHeader>
            <SheetTitle className="text-foreground">{title}</SheetTitle>
            <SheetDescription className="sr-only">Detalles y configuración para {title}</SheetDescription>
          </SheetHeader>
          <div className="mt-4 pb-20">{children}</div>
        </SheetContent>
      </Sheet>
    );
  }

  // If centered dialog variant is requested
  if (variant === "dialog") {
    return (
      <Dialog open={open} onOpenChange={onOpenChange}>
        <DialogContent 
          className="glass border-border overflow-y-auto max-h-[90vh] my-8 p-0"
          style={{ maxWidth }}
        >
          <div className="p-8">
            <DialogHeader className="mb-6">
              <DialogTitle className="text-2xl font-black tracking-tight text-foreground">{title}</DialogTitle>
              <DialogDescription className="sr-only">Detalles y configuración para {title}</DialogDescription>
            </DialogHeader>
            <div className="space-y-4">{children}</div>
          </div>
        </DialogContent>
      </Dialog>
    );
  }

  // Default is right sheet
  return (
    <Sheet open={open} onOpenChange={onOpenChange}>
      <SheetContent
        side="right"
        className="glass border-border overflow-y-auto"
        style={{ width: "480px" }}
      >
        <SheetHeader>
          <SheetTitle className="text-foreground">{title}</SheetTitle>
          <SheetDescription className="sr-only">Detalles y configuración para {title}</SheetDescription>
        </SheetHeader>
        <div className="mt-4">{children}</div>
      </SheetContent>
    </Sheet>
  );
}

