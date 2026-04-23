export function SkeletonCard() {
  return (
    <div className="glass rounded-xl p-5 space-y-3">
      <div className="flex items-center gap-3">
        <div className="h-10 w-10 rounded-full bg-muted skeleton-pulse" />
        <div className="space-y-2 flex-1">
          <div className="h-4 w-3/4 rounded bg-muted skeleton-pulse" />
          <div className="h-3 w-1/2 rounded bg-muted skeleton-pulse" />
        </div>
      </div>
      <div className="h-3 w-full rounded bg-muted skeleton-pulse" />
      <div className="h-3 w-2/3 rounded bg-muted skeleton-pulse" />
    </div>
  );
}

export function SkeletonTable() {
  return (
    <div className="glass rounded-xl overflow-hidden">
      <div className="p-4 border-b border-border">
        <div className="h-4 w-48 rounded bg-muted skeleton-pulse" />
      </div>
      {[...Array(5)].map((_, i) => (
        <div key={i} className="flex items-center gap-4 p-4 border-b border-border">
          <div className="h-4 w-4 rounded bg-muted skeleton-pulse" />
          <div className="h-4 flex-1 rounded bg-muted skeleton-pulse" />
          <div className="h-4 w-24 rounded bg-muted skeleton-pulse" />
          <div className="h-6 w-16 rounded-full bg-muted skeleton-pulse" />
        </div>
      ))}
    </div>
  );
}
