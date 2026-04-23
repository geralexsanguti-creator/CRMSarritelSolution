import { motion, AnimatePresence } from "framer-motion";
import { Trash2, Edit3, X, CheckSquare } from "lucide-react";
import { Button } from "@/components/ui/button";

interface BulkActionsBarProps {
  selectedCount: number;
  onClear: () => void;
  onDelete?: () => void;
  onEditStatus?: () => void;
  entityName?: string;
}

export function BulkActionsBar({ selectedCount, onClear, onDelete, onEditStatus, entityName = "registros" }: BulkActionsBarProps) {
  return (
    <AnimatePresence>
      {selectedCount > 0 && (
        <motion.div
          initial={{ y: 100, opacity: 0 }}
          animate={{ y: 0, opacity: 1 }}
          exit={{ y: 100, opacity: 0 }}
          className="fixed bottom-6 left-1/2 -translate-x-1/2 z-50 w-[90%] max-w-2xl"
        >
          <div className="glass border border-primary/20 shadow-2xl rounded-2xl p-4 flex items-center justify-between gap-4">
            <div className="flex items-center gap-3">
              <div className="h-10 w-10 rounded-xl bg-primary/10 flex items-center justify-center text-primary">
                <CheckSquare className="h-5 w-5" />
              </div>
              <div>
                <p className="text-sm font-bold text-foreground">{selectedCount} {entityName} seleccionados</p>
                <button 
                  onClick={onClear}
                  className="text-xs text-muted-foreground hover:text-primary transition-colors flex items-center gap-1"
                >
                  <X className="h-3 w-3" /> Deseleccionar todo
                </button>
              </div>
            </div>

            <div className="flex items-center gap-2">
              {onEditStatus && (
                <Button 
                  variant="outline" 
                  size="sm" 
                  onClick={onEditStatus}
                  className="border-primary/20 text-primary hover:bg-primary/10 gap-2 h-9"
                >
                  <Edit3 className="h-4 w-4" /> Cambiar Estado
                </Button>
              )}
              {onDelete && (
                <Button 
                  variant="destructive" 
                  size="sm" 
                  onClick={onDelete}
                  className="gap-2 h-9 shadow-lg shadow-destructive/20"
                >
                  <Trash2 className="h-4 w-4" /> Eliminar
                </Button>
              )}
            </div>
          </div>
        </motion.div>
      )}
    </AnimatePresence>
  );
}
