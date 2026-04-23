import { useQuery } from '@tanstack/react-query';
import { Contrato } from '@/types';

// Contratos has not yet been implemented in the backend schema.
// We return an empty array for now until its backend entity is built.
export function useContratos() {
  return useQuery({
    queryKey: ['contratos'],
    queryFn: async (): Promise<Contrato[]> => {
      return [];
    },
    staleTime: 1000 * 60 * 5,
  });
}
