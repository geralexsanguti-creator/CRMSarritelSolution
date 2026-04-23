import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { PeriodosService } from "@/services/api/periodos.service";
import { PeriodoFacturacion } from "@/types";
import { toast } from "sonner";

export function usePeriodos() {
  const queryClient = useQueryClient();

  const query = useQuery({
    queryKey: ["periodos"],
    queryFn: PeriodosService.getAll,
  });

  const createMutation = useMutation({
    mutationFn: (data: Partial<PeriodoFacturacion>) => PeriodosService.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["periodos"] });
      toast.success("Periodo creado con éxito");
    },
    onError: () => toast.error("Error al crear el periodo"),
  });

  const updateMutation = useMutation({
    mutationFn: ({ id, data }: { id: number; data: Partial<PeriodoFacturacion> }) =>
      PeriodosService.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["periodos"] });
      toast.success("Periodo actualizado con éxito");
    },
    onError: () => toast.error("Error al actualizar el periodo"),
  });

  const deleteMutation = useMutation({
    mutationFn: (id: number) => PeriodosService.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["periodos"] });
      toast.success("Periodo eliminado");
    },
    onError: () => toast.error("Error al eliminar el periodo"),
  });

  const setPrincipalMutation = useMutation({
    mutationFn: (id: number) => PeriodosService.setPrincipal(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["periodos"] });
      toast.success("Periodo principal actualizado");
    },
    onError: () => toast.error("Error al establecer periodo principal"),
  });

  const recalculateMutation = useMutation({
    mutationFn: (id: number) => PeriodosService.recalculate(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["comisiones"] });
      toast.success("Comisiones recalculadas para el periodo");
    },
    onError: () => toast.error("Error al recalcular comisiones"),
  });

  return {
    periodos: query.data || [],
    isLoading: query.isLoading,
    createPeriodo: createMutation.mutateAsync,
    isCreating: createMutation.isPending,
    updatePeriodo: updateMutation.mutateAsync,
    isUpdating: updateMutation.isPending,
    deletePeriodo: deleteMutation.mutateAsync,
    isDeleting: deleteMutation.isPending,
    setPrincipal: setPrincipalMutation.mutateAsync,
    isSettingPrincipal: setPrincipalMutation.isPending,
    recalculate: recalculateMutation.mutateAsync,
    isRecalculating: recalculateMutation.isPending,
    refetch: query.refetch,
  };
}
