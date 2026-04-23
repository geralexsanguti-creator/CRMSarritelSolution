import apiClient from '@/lib/axios';
import type { Proveedor, ProveedorCreateDto, TipoVenta, TipoVentaCreateDto, ReglaComision, ReglaComisionCreateDto } from '@/types/proveedor.types';

export const ProveedoresService = {
  getAll: async (): Promise<Proveedor[]> => {
    const response = await apiClient.get<Proveedor[]>('/Proveedores');
    return response.data;
  },

  getById: async (id: number): Promise<Proveedor> => {
    const response = await apiClient.get<Proveedor>(`/Proveedores/${id}`);
    return response.data;
  },

  create: async (proveedor: ProveedorCreateDto): Promise<Proveedor> => {
    const response = await apiClient.post<Proveedor>('/Proveedores', proveedor);
    return response.data;
  },

  update: async (id: number, proveedor: Partial<ProveedorCreateDto>): Promise<Proveedor> => {
    const response = await apiClient.put<Proveedor>(`/Proveedores/${id}`, proveedor);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/Proveedores/${id}`);
  },
};

export const TiposVentaService = {
  getAll: async (): Promise<TipoVenta[]> => {
    const response = await apiClient.get<TipoVenta[]>('/TiposVenta');
    return response.data;
  },

  getById: async (id: number): Promise<TipoVenta> => {
    const response = await apiClient.get<TipoVenta>(`/TiposVenta/${id}`);
    return response.data;
  },

  create: async (tipoVenta: TipoVentaCreateDto): Promise<TipoVenta> => {
    const response = await apiClient.post<TipoVenta>('/TiposVenta', tipoVenta);
    return response.data;
  },

  update: async (id: number, tipoVenta: Partial<TipoVentaCreateDto>): Promise<TipoVenta> => {
    const response = await apiClient.put<TipoVenta>(`/TiposVenta/${id}`, tipoVenta);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/TiposVenta/${id}`);
  },
};

export const ReglasComisionService = {
  getAll: async (): Promise<ReglaComision[]> => {
    const response = await apiClient.get<ReglaComision[]>('/ReglasComision');
    return response.data;
  },

  getByProveedor: async (proveedorId: number): Promise<ReglaComision[]> => {
    const response = await apiClient.get<ReglaComision[]>(`/ReglasComision/proveedor/${proveedorId}`);
    return response.data;
  },

  create: async (regla: ReglaComisionCreateDto): Promise<ReglaComision> => {
    const response = await apiClient.post<ReglaComision>('/ReglasComision', regla);
    return response.data;
  },

  update: async (id: number, regla: Partial<ReglaComisionCreateDto>): Promise<ReglaComision> => {
    const response = await apiClient.put<ReglaComision>(`/ReglasComision/${id}`, regla);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/ReglasComision/${id}`);
  },

  checkCommissions: async (id: number): Promise<{ hasCommissions: boolean }> => {
    const response = await apiClient.get<{ hasCommissions: boolean }>(`/ReglasComision/${id}/has-commissions`);
    return response.data;
  },
};
