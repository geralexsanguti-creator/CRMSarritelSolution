import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { fichajesService } from '@/services/api/fichajes.service';
import { Fichaje } from '@/types';
import { toast } from 'sonner';

export function useFichajes(params?: { usuarioId?: number; fechaInicio?: string; fechaFin?: string }) {
  const queryClient = useQueryClient();

  const queryParams = { ...params };
  if (!queryParams.usuarioId) delete queryParams.usuarioId;
  if (!queryParams.fechaInicio) delete queryParams.fechaInicio;
  if (!queryParams.fechaFin) delete queryParams.fechaFin;

  const queryKey = ['fichajes', queryParams];

  const fichajesQuery = useQuery({
    queryKey,
    queryFn: () => fichajesService.getAll(queryParams),
    staleTime: 1000 * 60 * 5, // 5 minutos
  });

  const startMutation = useMutation({
    mutationFn: (data: { usuarioId: number; tipoRegistro?: string; notas?: string }) => fichajesService.start(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['fichajes'] });
      toast.success('Turno iniciado correctamente');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al iniciar el turno');
    }
  });

  const stopMutation = useMutation({
    mutationFn: (id: number) => fichajesService.stop(id),
    onSuccess: (data) => {
      queryClient.invalidateQueries({ queryKey: ['fichajes'] });
      toast.success('Turno finalizado: ' + data.message);
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al finalizar el turno');
    }
  });

  const deleteMutation = useMutation({
    mutationFn: (id: number) => fichajesService.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['fichajes'] });
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al eliminar el fichaje');
    }
  });

  const pauseMutation = useMutation({
    mutationFn: (id: number) => fichajesService.pause(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['fichajes'] });
      toast.success('Turno pausado');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al pausar el turno');
    }
  });

  const resumeMutation = useMutation({
    mutationFn: (id: number) => fichajesService.resume(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['fichajes'] });
      toast.success('Turno reanudado');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al reanudar el turno');
    }
  });

  const updateMutation = useMutation({
    mutationFn: ({ id, data }: { id: number; data: Partial<Fichaje> }) => fichajesService.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['fichajes'] });
      toast.success('Registro actualizado correctamente');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al actualizar el registro');
    }
  });

  return {
    data: fichajesQuery.data || [],
    isLoading: fichajesQuery.isLoading,
    isError: fichajesQuery.isError,
    startFichaje: startMutation.mutateAsync,
    isStarting: startMutation.isPending,
    stopFichaje: stopMutation.mutateAsync,
    isStopping: stopMutation.isPending,
    updateFichaje: updateMutation.mutateAsync,
    isUpdating: updateMutation.isPending,
    deleteFichaje: deleteMutation.mutateAsync,
    pauseFichaje: pauseMutation.mutateAsync,
    isPausing: pauseMutation.isPending,
    resumeFichaje: resumeMutation.mutateAsync,
    isResuming: resumeMutation.isPending,
  };
}
