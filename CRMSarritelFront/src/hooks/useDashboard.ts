import { useQuery } from '@tanstack/react-query';
import apiClient from '@/lib/axios';

// Interfaces matching the new Backend DTOs structure
export interface KpisDto {
  totalClientes: number;
  empresas: number;
  totalSalesMonto: number;
  pendingCommissions: number;
  activeProducts: number;
  totalProductos: number;
}

export interface RecentSaleDto {
  id: number;
  numeroVenta: string;
  montoTotal: number;
  fechaVenta: string;
  clienteNombre: string;
  usuarioNombre: string;
  estado_Nombre: string;
  estado_Color: string;
  tipoVenta_Nombre: string;
  origenVenta: string;
}

export interface TopSellerDto {
  usuarioId: number;
  nombre: string;
  email: string;
  totalVendido: number;
  count: number;
}

export interface DashboardDto {
  kpis: KpisDto;
  recentSales: RecentSaleDto[];
  topSellers: TopSellerDto[];
}

export function useDashboard() {
  return useQuery({
    queryKey: ['dashboard_metrics'],
    queryFn: async () => {
      // Call actual endpoint
      const response = await apiClient.get<DashboardDto>('/Dashboard');
      return response.data;
    },
    staleTime: 1000 * 60 * 5, // 5 minutes
  });
}
