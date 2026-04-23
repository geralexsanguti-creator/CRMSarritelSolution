import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import apiClient from '@/lib/axios';
import { Comision } from '@/types';
import { toast } from 'sonner';

export function useComisiones() {
  const queryClient = useQueryClient();

  const query = useQuery({
    queryKey: ['comisiones'],
    queryFn: async () => {
      const response = await apiClient.get<Comision[]>('/Comisiones');
      return response.data;
    },
    staleTime: 1000 * 60 * 5,
  });

  const createMutation = useMutation({
    mutationFn: (data: Partial<Comision>) => apiClient.post('/Comisiones', data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['comisiones'] });
      toast.success('Comisión creada');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al crear la comisión');
    }
  });

  const updateMutation = useMutation({
    mutationFn: (data: Partial<Comision> & { id: number }) => apiClient.put(`/Comisiones/${data.id}`, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['comisiones'] });
      toast.success('Comisión actualizada');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al actualizar la comisión');
    }
  });

  const liquidarMutation = useMutation({
    mutationFn: (id: number) => apiClient.put(`/Comisiones/${id}`, { id, estado_Codigo: 'PAGADA', estado_Nombre: 'Pagada', fechaPago: new Date().toISOString() }),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['comisiones'] });
      toast.success('Comisión liquidada');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al liquidar la comisión');
    }
  });

  const deleteMutation = useMutation({
    mutationFn: (id: number) => apiClient.delete(`/Comisiones/${id}`),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['comisiones'] });
      toast.success('Comisión eliminada');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al eliminar la comisión');
    }
  });

  const recalculateMutation = useMutation({
    mutationFn: (periodoId: number) => apiClient.post(`/Comisiones/recalculate-period/${periodoId}`),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['comisiones'] });
      toast.success('Periodo recalculado con éxito');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al recalcular comisiones');
    }
  });

  const recalculateAllMutation = useMutation({
    mutationFn: (force?: boolean) => apiClient.post(`/Comisiones/recalculate-all${force ? '?force=true' : ''}`),
    onSuccess: (response) => {
      queryClient.invalidateQueries({ queryKey: ['comisiones'] });
      toast.success(response.data.message || 'Todas las comisiones se han recalculado.');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al realizar el recálculo masivo');
    }
  });


  return {
    comisiones: query.data || [],
    isLoading: query.isLoading,
    isError: query.isError,
    createComision: createMutation.mutateAsync,
    isCreating: createMutation.isPending,
    updateComision: updateMutation.mutateAsync,
    isUpdating: updateMutation.isPending,
    liquidarComision: liquidarMutation.mutateAsync,
    isLiquidating: liquidarMutation.mutateAsync,
    deleteComision: deleteMutation.mutateAsync,
    isDeleting: deleteMutation.isPending,
    recalculatePeriod: recalculateMutation.mutateAsync,
    isRecalculating: recalculateMutation.isPending || recalculateAllMutation.isPending,
    recalculateAll: recalculateAllMutation.mutateAsync,
    isRecalculatingAll: recalculateAllMutation.isPending,
    refetch: query.refetch
  };
}


