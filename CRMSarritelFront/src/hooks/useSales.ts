import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import apiClient from '@/lib/axios';
import { Venta } from '@/types';
import { VentasService } from '@/services/api/ventas.service';
import { toast } from 'sonner';

interface UseSalesOptions {
  q?: string;
  status?: string;
  vendedorId?: number;
  clienteId?: number;
}

export function useSales(options?: UseSalesOptions) {
  return useQuery({
    queryKey: ['ventas', options],
    queryFn: async () => {
      const response = await apiClient.get<Venta[]>('/Ventas');
      const data = response.data || [];
      
      let filteredData = data;

      if (options?.status && options.status !== 'all') {
        filteredData = filteredData.filter(v => v.estado_Codigo === options.status);
      }

      if (options?.vendedorId) {
        filteredData = filteredData.filter(v => v.usuarioId === options.vendedorId);
      }
      
      if (options?.clienteId) {
        filteredData = filteredData.filter(v => v.clienteId === options.clienteId);
      }

      return filteredData;
    },
    staleTime: 1000 * 60 * 5,
  });
}

export function useCreateVenta() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (data: Partial<Venta>) => VentasService.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['ventas'] });
      queryClient.invalidateQueries({ queryKey: ['sale-history'] });
      queryClient.invalidateQueries({ queryKey: ['comisiones'] }); // Sincronización fluida
      toast.success('Venta creada correctamente');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al crear la venta');
    }
  });
}

export function useUpdateVenta() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: Partial<Venta> }) => VentasService.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['ventas'] });
      queryClient.invalidateQueries({ queryKey: ['sale-history'] });
      queryClient.invalidateQueries({ queryKey: ['comisiones'] }); // Sincronización fluida
      toast.success('Venta actualizada correctamente');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al actualizar la venta');
    }
  });
}

export function useDeleteVenta() {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: number) => VentasService.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['ventas'] });
      queryClient.invalidateQueries({ queryKey: ['comisiones'] }); // Sincronización fluida
      toast.success('Venta eliminada correctamente');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al eliminar la venta');
    }
  });
}

