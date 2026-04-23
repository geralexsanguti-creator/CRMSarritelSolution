import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { ConfiguracionService, PipelineConfig } from '@/services/api/configuracion.service';

export function usePipelineConfig() {
  const queryClient = useQueryClient();

  const query = useQuery({
    queryKey: ['pipeline-config'],
    queryFn: ConfiguracionService.getPipelineConfig,
  });

  const updateMutation = useMutation({
    mutationFn: (data: PipelineConfig) => ConfiguracionService.savePipelineConfig(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['pipeline-config'] });
    },
  });

  return {
    ...query,
    updateConfig: updateMutation.mutateAsync,
    isUpdating: updateMutation.isPending,
  };
}
