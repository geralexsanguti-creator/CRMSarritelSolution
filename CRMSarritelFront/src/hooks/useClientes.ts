import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { ClientesService } from '@/services/api/clientes.service';
import { Cliente } from '@/types';
import { toast } from 'sonner';

export function useClientes() {
  const queryClient = useQueryClient();

  const query = useQuery({
    queryKey: ['clientes'],
    queryFn: ClientesService.getAll,
    staleTime: 1000 * 60 * 5, // 5 minutes
  });

  const createCliente = useMutation({
    mutationFn: (data: Omit<Cliente, 'id' | 'createdAt' | 'updatedAt'>) => ClientesService.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['clientes'] });
      toast.success('Cliente creado correctamente');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al crear el cliente');
    }
  });

  const updateCliente = useMutation({
    mutationFn: ({ id, data }: { id: number; data: Partial<Cliente> }) => 
      ClientesService.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['clientes'] });
      toast.success('Cliente actualizado correctamente');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al actualizar el cliente');
    }
  });

  const deleteCliente = useMutation({
    mutationFn: (id: number) => ClientesService.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['clientes'] });
      toast.success('Cliente eliminado correctamente');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al eliminar el cliente');
    }
  });

  return {
    clientes: query.data || [],
    isLoading: query.isLoading,
    isError: query.isError,
    createCliente: createCliente.mutateAsync,
    isCreating: createCliente.isPending,
    updateCliente: updateCliente.mutateAsync,
    isUpdating: updateCliente.isPending,
    deleteCliente: deleteCliente.mutateAsync,
    isDeleting: deleteCliente.isPending,
    refetch: query.refetch
  };
}
