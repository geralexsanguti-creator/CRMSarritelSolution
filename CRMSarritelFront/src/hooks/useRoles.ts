import { useQuery } from '@tanstack/react-query';
import { RolesService } from '@/services/api/roles.service';

export function useRoles() {
  const query = useQuery({
    queryKey: ['roles'],
    queryFn: RolesService.getAll,
    staleTime: 1000 * 60 * 60, // 1 hour cache since roles rarely change
  });

  return {
    data: query.data || [],
    isLoading: query.isLoading,
    isError: query.isError,
  };
}
