export function SalesSkeleton({ view, isMobile }: { view: "table" | "kanban" | "grid", isMobile: boolean }) {
  if (view === "kanban" && !isMobile) {
    return (
      <div className="grid grid-cols-4 gap-4 animate-pulse">
        {[1, 2, 3, 4].map((col) => (
          <div key={col} className="space-y-3">
            <div className="flex items-center gap-2 mb-2">
              <div className="h-5 w-20 bg-muted rounded-full" />
              <div className="h-4 w-6 bg-muted rounded-md" />
            </div>
            {[1, 2, 3].map((item) => (
              <div key={item} className="glass rounded-xl p-4 h-32 space-y-3">
                <div className="h-3 w-16 bg-muted rounded" />
                <div className="h-4 w-32 bg-muted rounded" />
                <div className="h-6 w-24 bg-muted rounded mt-2" />
                <div className="flex gap-2">
                  <div className="h-3 w-16 bg-muted rounded" />
                  <div className="h-3 w-16 bg-muted rounded" />
                </div>
              </div>
            ))}
          </div>
        ))}
      </div>
    );
  }

  if (isMobile) {
    return (
      <div className="grid gap-3 animate-pulse">
        {[1, 2, 3, 4, 5].map((i) => (
          <div key={i} className="glass rounded-xl p-4 h-28 space-y-3">
            <div className="flex justify-between">
              <div className="h-4 w-32 bg-muted rounded" />
              <div className="h-5 w-20 bg-muted rounded-full" />
            </div>
            <div className="h-3 w-16 bg-muted rounded" />
            <div className="flex justify-between items-end mt-4">
              <div className="h-3 w-24 bg-muted rounded" />
              <div className="h-6 w-24 bg-muted rounded" />
            </div>
          </div>
        ))}
      </div>
    );
  }

  return (
    <div className="glass rounded-xl overflow-hidden animate-pulse">
      <div className="w-full text-sm">
        <div className="flex border-b border-border p-4 gap-4">
          <div className="h-4 w-12 bg-muted rounded" />
          <div className="h-4 w-32 bg-muted rounded" />
          <div className="h-4 w-32 bg-muted rounded" />
          <div className="h-4 w-16 bg-muted rounded" />
          <div className="h-4 w-16 bg-muted rounded" />
          <div className="h-4 w-24 bg-muted rounded ml-auto" />
          <div className="h-4 w-24 bg-muted rounded ml-4" />
          <div className="h-4 w-20 bg-muted rounded ml-4" />
        </div>
        {[1, 2, 3, 4, 5].map((i) => (
          <div key={i} className="flex border-b border-border p-4 gap-4 items-center">
            <div className="h-4 w-12 bg-muted/50 rounded" />
            <div className="h-4 w-32 bg-muted/50 rounded" />
            <div className="h-4 w-32 bg-muted/50 rounded" />
            <div className="h-5 w-16 bg-muted/50 rounded-full" />
            <div className="h-4 w-16 bg-muted/50 rounded" />
            <div className="h-4 w-24 bg-muted/50 rounded ml-auto" />
            <div className="h-4 w-24 bg-muted/50 rounded ml-4" />
            <div className="h-5 w-20 bg-muted/50 rounded-full ml-4" />
          </div>
        ))}
      </div>
    </div>
  );
}
