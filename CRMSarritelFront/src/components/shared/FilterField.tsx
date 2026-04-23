import React from "react";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Input } from "@/components/ui/input";
import { X } from "lucide-react";
import { Button } from "@/components/ui/button";
import { DatePicker } from "@/components/ui/date-picker";

export interface FilterOption {
  value: string;
  label: string;
}

interface FilterFieldProps {
  columnLabel: string;
  type: "select" | "text" | "number" | "date";
  options?: FilterOption[];
  value: string;
  onChange: (value: string) => void;
  onRemove?: () => void;
  placeholder?: string;
}

export const FilterField: React.FC<FilterFieldProps> = ({
  columnLabel,
  type,
  options,
  value,
  onChange,
  onRemove,
  placeholder = "Seleccionar...",
}) => {
  return (
    <div className="flex flex-col gap-1.5 min-w-[180px] group relative pb-2 sm:pb-0">
      <div className="flex items-center justify-between px-1">
        <span className="text-[9px] uppercase tracking-wider text-muted-foreground font-bold">{columnLabel}</span>
        {onRemove && (
          <button 
            onClick={onRemove}
            className="opacity-0 group-hover:opacity-100 p-0.5 hover:bg-destructive/10 rounded transition-all"
          >
            <X className="h-2.5 w-2.5 text-destructive" />
          </button>
        )}
      </div>
      
      {type === "select" && options ? (
        <Select value={value} onValueChange={onChange}>
          <SelectTrigger className="h-9 bg-background border-border/60 text-xs focus:ring-primary/20 transition-all">
            <SelectValue placeholder={placeholder} />
          </SelectTrigger>
          <SelectContent className="glass border-border/50">
            <SelectItem value="all">Ver todos</SelectItem>
            {options.map((opt) => (
              <SelectItem key={opt.value} value={opt.value}>
                {opt.label}
              </SelectItem>
            ))}
          </SelectContent>
        </Select>
      ) : type === "date" ? (
        <DatePicker
          value={value}
          onChange={onChange}
          placeholder={placeholder}
          className="h-9 text-xs"
        />
      ) : (
        <Input 
          type={type}
          value={value}
          onChange={(e) => onChange(e.target.value)}
          placeholder={placeholder}
          className="h-9 bg-background border-border/60 text-xs focus-visible:ring-primary/20 transition-all"
        />
      )}
    </div>
  );
};
