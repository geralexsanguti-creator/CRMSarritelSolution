import apiClient from '@/lib/axios';
import { LoginRequest, AuthResponse } from '@/types/auth.types';

export const authService = {
  login: async (credentials: LoginRequest): Promise<AuthResponse> => {
    const response = await apiClient.post<any>('/Auth/login', credentials);
    const data = response.data;
    return {
      token: data.token,
      canViewAllCommissions: data.canViewAllCommissions || false,
      usuario: {
        id: data.id.toString(),
        email: data.email,
        nombre: data.nombre,
        roles: data.roles || [],
        permissions: data.permissions || [],
        canViewAllCommissions: data.canViewAllCommissions || false
      }
    };
  },

  // Mock checking if the token is valid, or retrieving user profile if needed in the future
  getCurrentUser: async () => {
     // A real implementation might hit a /me endpoint
     // For now, we rely on the data stored during login
     return Promise.resolve(JSON.parse(localStorage.getItem('user') || 'null'));
  }
};
