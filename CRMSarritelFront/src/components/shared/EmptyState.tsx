import { LucideIcon } from "lucide-react";
import { motion } from "framer-motion";

interface EmptyStateProps {
  icon: LucideIcon;
  title: string;
  description: string;
}

export function EmptyState({ icon: Icon, title, description }: EmptyStateProps) {
  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      className="flex flex-col items-center justify-center py-16 px-4"
    >
      <div className="rounded-full bg-background p-6 mb-4">
        <Icon className="h-12 w-12 text-muted-foreground/40" />
      </div>
      <h3 className="text-lg font-semibold text-foreground/80 mb-1">{title}</h3>
      <p className="text-sm text-muted-foreground text-center max-w-sm">{description}</p>
    </motion.div>
  );
}
