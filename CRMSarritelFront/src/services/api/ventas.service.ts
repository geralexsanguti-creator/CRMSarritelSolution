import apiClient from '../../lib/axios';
import { Venta } from '../../types';

export const VentasService = {
    // Obtener todas las ventas
    getAll: async (): Promise<Venta[]> => {
        const response = await apiClient.get<Venta[]>('/Ventas');
        return response.data;
    },

    // Obtener una venta por ID
    getById: async (id: number): Promise<Venta> => {
        const response = await apiClient.get<Venta>(`/Ventas/${id}`);
        return response.data;
    },

    // Crear una nueva venta
    create: async (data: Partial<Venta>): Promise<Venta> => {
        const response = await apiClient.post<Venta>('/Ventas', data);
        return response.data;
    },

    // Actualizar una venta existente
    update: async (id: number, data: Partial<Venta>): Promise<void> => {
        await apiClient.put(`/Ventas/${id}`, data);
    },

    // Eliminar una venta
    delete: async (id: number): Promise<void> => {
        await apiClient.delete(`/Ventas/${id}`);
    },
};
