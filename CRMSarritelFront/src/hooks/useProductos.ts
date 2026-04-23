import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { ProductosService } from '@/services/api/productos.service';
import { Producto } from '@/types';
import { toast } from 'sonner';

export function useProductos() {
  const queryClient = useQueryClient();

  const query = useQuery({
    queryKey: ['productos'],
    queryFn: ProductosService.getAll,
    staleTime: 1000 * 60 * 5, // 5 minutes
  });

  const createMutation = useMutation({
    mutationFn: (data: Partial<Producto>) => ProductosService.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['productos'] });
      toast.success('Producto creado correctamente');
    },
    onError: (error: any) => {
      console.error("Update Error:", error.response?.data);
      const errors = error.response?.data?.errors;
      const errorMsg = errors ? JSON.stringify(errors) : (error.response?.data?.message || 'Error al actualizar el producto');
      toast.error(`Error: ${errorMsg}`);
    }
  });

  const updateMutation = useMutation({
    mutationFn: ({ id, data }: { id: number; data: Partial<Producto> }) => 
      ProductosService.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['productos'] });
      toast.success('Producto actualizado correctamente');
    },
    onError: (error: any) => {
      console.error("Update Error:", error.response?.data);
      const errors = error.response?.data?.errors;
      const errorMsg = errors ? JSON.stringify(errors) : (error.response?.data?.message || 'Error al actualizar el producto');
      toast.error(`Error: ${errorMsg}`);
    }
  });

  const deleteMutation = useMutation({
    mutationFn: (id: number) => ProductosService.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['productos'] });
      toast.success('Producto eliminado correctamente');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al eliminar el producto');
    }
  });

  return {
    productos: query.data || [],
    isLoading: query.isLoading,
    isError: query.isError,
    createProducto: createMutation.mutateAsync,
    isCreating: createMutation.isPending,
    updateProducto: updateMutation.mutateAsync,
    isUpdating: updateMutation.isPending,
    deleteProducto: deleteMutation.mutateAsync,
    isDeleting: deleteMutation.isPending,
    refetch: query.refetch
  };
}
