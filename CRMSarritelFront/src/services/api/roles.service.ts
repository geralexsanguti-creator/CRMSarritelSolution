import apiClient from '../../lib/axios';
import { Rol, Permiso } from '../../types';

export const RolesService = {
    getAll: async (): Promise<Rol[]> => {
        const response = await apiClient.get<Rol[]>('/Roles');
        return response.data;
    },

    getById: async (id: number): Promise<Rol> => {
        const response = await apiClient.get<Rol>(`/Roles/${id}`);
        return response.data;
    },

    getPermissionsByRolId: async (id: number): Promise<Permiso[]> => {
        const response = await apiClient.get<Permiso[]>(`/Roles/${id}/permissions`);
        return response.data;
    },

    updateRolPermissions: async (rolId: number, permissionIds: number[]): Promise<void> => {
        await apiClient.post(`/Roles/${rolId}/permissions`, permissionIds);
    },

    getAllPermissions: async (): Promise<Permiso[]> => {
        const response = await apiClient.get<Permiso[]>('/Roles/permissions');
        return response.data;
    },

    create: async (rol: Partial<Rol>): Promise<Rol> => {
        const response = await apiClient.post<Rol>('/Roles', rol);
        return response.data;
    },

    update: async (id: number, rol: Partial<Rol>): Promise<Rol> => {
        const response = await apiClient.put<Rol>(`/Roles/${id}`, rol);
        return response.data;
    },

    delete: async (id: number): Promise<void> => {
        await apiClient.delete(`/Roles/${id}`);
    }
};
