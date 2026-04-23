import { LucideIcon } from "lucide-react";
import { motion } from "framer-motion";
import { Area, AreaChart, ResponsiveContainer } from "recharts";

interface KpiCardProps {
  title: string;
  value: string;
  change?: string;
  changePositive?: boolean;
  icon: LucideIcon;
  sparkData?: number[];
  index?: number;
}

export function KpiCard({ title, value, change, changePositive, icon: Icon, sparkData, index = 0 }: KpiCardProps) {
  const chartData = sparkData?.map((v, i) => ({ v, i })) || [];

  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ delay: index * 0.1 }}
      className="glass glass-hover rounded-xl p-5 relative overflow-hidden group"
    >
      <div className="flex items-start justify-between mb-3">
        <div>
          <p className="text-xs font-medium text-muted-foreground uppercase tracking-wider">{title}</p>
          <p className="text-2xl font-bold text-foreground mt-1">{value}</p>
        </div>
        <div className="rounded-lg bg-primary/10 p-2.5">
          <Icon className="h-5 w-5 text-primary" />
        </div>
      </div>
      {change && (
        <p className="text-[10px] font-bold text-primary/80 uppercase tracking-tighter">
          {change}
        </p>
      )}
      {chartData.length > 0 && (
        <div className="absolute bottom-0 left-0 right-0 h-12 opacity-30 group-hover:opacity-50 transition-opacity">
          <ResponsiveContainer width="100%" height="100%">
            <AreaChart data={chartData}>
              <defs>
                <linearGradient id={`grad-${index}`} x1="0" y1="0" x2="0" y2="1">
                  <stop offset="0%" stopColor="hsl(173 80% 50%)" stopOpacity={0.4} />
                  <stop offset="100%" stopColor="hsl(173 80% 50%)" stopOpacity={0} />
                </linearGradient>
              </defs>
              <Area type="monotone" dataKey="v" stroke="hsl(173 80% 50%)" fill={`url(#grad-${index})`} strokeWidth={1.5} dot={false} />
            </AreaChart>
          </ResponsiveContainer>
        </div>
      )}
    </motion.div>
  );
}
