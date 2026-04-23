import apiClient from '../../lib/axios';
import { Usuario } from '../../types';

export const UsuariosService = {
    getAll: async (): Promise<Usuario[]> => {
        const response = await apiClient.get<Usuario[]>('/Usuarios');
        return response.data;
    },

    getById: async (id: number): Promise<Usuario> => {
        const response = await apiClient.get<Usuario>(`/Usuarios/${id}`);
        return response.data;
    },

    create: async (data: any): Promise<Usuario> => {
        const response = await apiClient.post<Usuario>('/Usuarios', data);
        return response.data;
    },

    update: async (id: number, data: any): Promise<Usuario> => {
        const response = await apiClient.put<Usuario>(`/Usuarios/${id}`, data);
        return response.data;
    },

    delete: async (id: number): Promise<void> => {
        await apiClient.delete(`/Usuarios/${id}`);
    }
};
