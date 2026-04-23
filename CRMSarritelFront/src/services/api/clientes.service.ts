import apiClient from '../../lib/axios';
import { Cliente } from '../../types';

export const ClientesService = {
    // Obtener todos los clientes
    getAll: async (): Promise<Cliente[]> => {
        const response = await apiClient.get<Cliente[]>('/Clientes');
        return response.data;
    },

    // Obtener un cliente por ID
    getById: async (id: number): Promise<Cliente> => {
        const response = await apiClient.get<Cliente>(`/Clientes/${id}`);
        return response.data;
    },

    // Crear un nuevo cliente
    create: async (data: Omit<Cliente, 'id' | 'createdAt' | 'updatedAt'>): Promise<Cliente> => {
        const response = await apiClient.post<Cliente>('/Clientes', data);
        return response.data;
    },

    // Actualizar un cliente existente
    update: async (id: number, data: Partial<Cliente>): Promise<void> => {
        await apiClient.put(`/Clientes/${id}`, data);
    },

    // Eliminar un cliente
    delete: async (id: number): Promise<void> => {
        await apiClient.delete(`/Clientes/${id}`);
    },
};
