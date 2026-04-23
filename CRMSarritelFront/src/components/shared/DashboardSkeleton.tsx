export function DashboardSkeleton() {
  return (
    <div className="space-y-6 animate-pulse">
      <div>
        <div className="h-8 w-48 bg-muted rounded mb-2" />
        <div className="h-4 w-64 bg-muted rounded" />
      </div>

      <div className="grid grid-cols-2 lg:grid-cols-4 gap-4">
        {[1, 2, 3, 4].map((i) => (
          <div key={i} className="glass rounded-xl p-4 h-32 flex flex-col justify-between">
            <div className="flex justify-between items-start">
              <div className="h-4 w-24 bg-muted rounded" />
              <div className="h-8 w-8 bg-muted rounded-full" />
            </div>
            <div className="h-8 w-32 bg-muted rounded mb-2" />
            <div className="h-3 w-40 bg-muted rounded" />
          </div>
        ))}
      </div>

      <div className="grid lg:grid-cols-2 gap-6">
        <div className="glass rounded-xl overflow-hidden h-[400px]">
          <div className="p-4 border-b border-border flex justify-between items-center">
            <div className="h-5 w-32 bg-muted rounded" />
            <div className="h-4 w-16 bg-muted rounded" />
          </div>
          <div className="p-4 space-y-4">
            {[1, 2, 3, 4, 5].map((i) => (
              <div key={i} className="flex justify-between items-center">
                <div className="flex items-center gap-3">
                  <div className="h-9 w-9 bg-muted rounded-lg" />
                  <div className="space-y-2">
                    <div className="h-4 w-32 bg-muted rounded" />
                    <div className="h-3 w-24 bg-muted rounded" />
                  </div>
                </div>
                <div className="space-y-2 text-right">
                  <div className="h-4 w-24 bg-muted rounded ml-auto" />
                  <div className="h-5 w-20 bg-muted rounded-full ml-auto" />
                </div>
              </div>
            ))}
          </div>
        </div>

        <div className="glass rounded-xl overflow-hidden h-[400px]">
          <div className="p-4 border-b border-border flex justify-between items-center">
            <div className="h-5 w-32 bg-muted rounded" />
            <div className="h-4 w-16 bg-muted rounded" />
          </div>
          <div className="p-4 space-y-4">
            {[1, 2, 3, 4, 5].map((i) => (
              <div key={i} className="flex justify-between items-center">
                <div className="flex items-center gap-3">
                  <div className="h-9 w-9 bg-muted rounded-full" />
                  <div className="space-y-2">
                    <div className="h-4 w-24 bg-muted rounded" />
                    <div className="h-3 w-40 bg-muted rounded" />
                  </div>
                </div>
                <div className="h-5 w-16 bg-muted rounded-full" />
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
