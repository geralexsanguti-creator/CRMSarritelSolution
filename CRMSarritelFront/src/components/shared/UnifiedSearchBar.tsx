import React, { useState } from "react";
import { Search, Filter, CheckSquare, X } from "lucide-react";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { motion, AnimatePresence } from "framer-motion";
import { cn } from "@/lib/utils";

interface UnifiedSearchBarProps {
  selectionMode: boolean;
  onSelectionModeToggle: () => void;
  searchValue: string;
  onSearchChange: (val: string) => void;
  searchPlaceholder?: string;
  children?: React.ReactNode; // Advanced filters
  onClearFilters?: () => void;
  showFilterBadge?: boolean;
  bulkActions?: React.ReactNode;
}

export const UnifiedSearchBar: React.FC<UnifiedSearchBarProps> = ({
  selectionMode,
  onSelectionModeToggle,
  searchValue,
  onSearchChange,
  searchPlaceholder = "Buscar...",
  children,
  onClearFilters,
  showFilterBadge = false,
  bulkActions,
}) => {
  const [isExpanded, setIsExpanded] = useState(false);

  return (
    <div className="w-full space-y-3">
      {/* Main Unified Bar */}
      <div className="glass rounded-xl p-2 sm:p-3 flex flex-col sm:flex-row gap-3 items-center shadow-lg border border-border/50 transition-all">
        {/* Selection Toggle */}
        <Button 
          variant={selectionMode ? "secondary" : "outline"} 
          size="sm" 
          className={cn(
            "h-10 px-4 shrink-0 gap-2 border-border/60 font-semibold transition-all w-full sm:w-auto",
            selectionMode ? "bg-primary/20 text-primary border-primary/30" : "bg-background hover:bg-muted/50"
          )}
          onClick={onSelectionModeToggle}
        >
          <CheckSquare className={cn("h-4 w-4", selectionMode ? "text-primary" : "text-muted-foreground")} />
          <span>{selectionMode ? "Cancelar" : "Seleccionar"}</span>
        </Button>

        {/* Unified Search Input or Bulk Actions */}
        <div className="relative flex-1 w-full min-h-[40px] flex items-center">
          <AnimatePresence mode="wait">
            {selectionMode && bulkActions ? (
              <motion.div
                key="bulk-actions"
                initial={{ opacity: 0, x: 20 }}
                animate={{ opacity: 1, x: 0 }}
                exit={{ opacity: 0, x: -20 }}
                className="w-full h-full flex items-center"
              >
                {bulkActions}
              </motion.div>
            ) : (
              <motion.div
                key="search-input"
                initial={{ opacity: 0, x: -20 }}
                animate={{ opacity: 1, x: 0 }}
                exit={{ opacity: 0, x: 20 }}
                className="w-full relative"
              >
                <Search className="absolute left-3.5 top-1/2 -translate-y-1/2 h-4 w-4 text-muted-foreground/60" />
                <Input 
                  placeholder={searchPlaceholder} 
                  value={searchValue} 
                  onChange={(e) => onSearchChange(e.target.value)} 
                  className="pl-10 h-10 bg-muted/20 border-border/40 w-full rounded-lg focus-visible:ring-primary/20 focus-visible:border-primary/30 text-sm placeholder:text-muted-foreground/50 transition-all font-medium" 
                />
                {searchValue && (
                  <button 
                    onClick={() => onSearchChange("")}
                    className="absolute right-3 top-1/2 -translate-y-1/2 p-1 hover:bg-muted rounded-full transition-colors"
                  >
                    <X className="h-3 w-3 text-muted-foreground" />
                  </button>
                )}
              </motion.div>
            )}
          </AnimatePresence>
        </div>

        {/* Filter Toggle Button */}
        <div className="flex items-center gap-2 w-full sm:w-auto">
          <Button 
            variant={isExpanded ? "secondary" : "outline"} 
            size="icon" 
            className={cn(
              "h-10 w-10 shrink-0 border-border/60 relative transition-all",
              isExpanded ? "bg-primary/10 border-primary/20 text-primary" : "bg-background hover:bg-muted/50"
            )}
            onClick={() => setIsExpanded(!isExpanded)}
          >
            <Filter className="h-4 w-4" />
            {showFilterBadge && (
              <span className="absolute -top-1 -right-1 h-3 w-3 bg-primary rounded-full border-2 border-background shadow-glow" />
            )}
          </Button>
          
          {/* Quick Clear (Mobile/Visible if filters match) */}
          {(searchValue || showFilterBadge) && onClearFilters && (
            <Button variant="ghost" size="sm" onClick={onClearFilters} className="h-10 px-2 text-muted-foreground hover:text-destructive transition-colors">
              <span className="hidden sm:inline text-xs mr-2">Limpiar</span>
              <X className="h-4 w-4" />
            </Button>
          )}
        </div>
      </div>

      {/* Advanced Filters Panel (Expandable) */}
      <AnimatePresence>
        {isExpanded && children && (
          <motion.div 
            initial={{ height: 0, opacity: 0, y: -10 }}
            animate={{ height: "auto", opacity: 1, y: 0 }}
            exit={{ height: 0, opacity: 0, y: -10 }}
            transition={{ type: "spring", stiffness: 300, damping: 30 }}
            className="overflow-hidden"
          >
            <div className="glass rounded-xl p-4 border border-border/40 shadow-xl bg-muted/5">
              <div className="flex flex-wrap items-center gap-3">
                <div className="text-[10px] font-bold text-muted-foreground uppercase tracking-widest w-full mb-1">
                  Filtros Avanzados
                </div>
                {children}
              </div>
            </div>
          </motion.div>
        )}
      </AnimatePresence>
    </div>
  );
};
