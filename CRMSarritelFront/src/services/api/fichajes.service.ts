import apiClient from '../../lib/axios';
import { Fichaje } from '../../types';

export const fichajesService = {
  // Obtener todos los fichajes, opcionalmente filtrados por usuario o fecha
  getAll: async (params?: { usuarioId?: number; fechaInicio?: string; fechaFin?: string }): Promise<Fichaje[]> => {
    const query = new URLSearchParams();
    if (params?.usuarioId) query.append('usuarioId', params.usuarioId.toString());
    if (params?.fechaInicio) query.append('fechaInicio', params.fechaInicio);
    if (params?.fechaFin) query.append('fechaFin', params.fechaFin);
    
    const response = await apiClient.get<Fichaje[]>(`/Fichajes?${query.toString()}`);
    return response.data;
  },

  // Obtener un fichaje por ID
  getById: async (id: number): Promise<Fichaje> => {
    const response = await apiClient.get<Fichaje>(`/Fichajes/${id}`);
    return response.data;
  },

  // Iniciar un turno (Start)
  start: async (data: { usuarioId: number; tipoRegistro?: string; notas?: string }): Promise<Fichaje> => {
    const response = await apiClient.post<Fichaje>(`/Fichajes/Start`, data);
    return response.data;
  },

  // Detener un turno (Stop)
  stop: async (id: number): Promise<{ message: string; horaSalida: string; horasExtra: number }> => {
    const response = await apiClient.post<{ message: string; horaSalida: string; horasExtra: number }>(`/Fichajes/Stop/${id}`);
    return response.data;
  },

  // Actualizar un fichaje manualmente (Solo Admin)
  update: async (id: number, data: Partial<Fichaje>): Promise<void> => {
    await apiClient.put(`/Fichajes/${id}`, data);
  },

  // Eliminar un fichaje (Solo Admin)
  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/Fichajes/${id}`);
  },

  // Pausar un turno
  pause: async (id: number): Promise<{ message: string; horaInicio: string }> => {
    const response = await apiClient.post<{ message: string; horaInicio: string }>(`/Fichajes/Pause/${id}`);
    return response.data;
  },

  // Reanudar un turno
  resume: async (id: number): Promise<{ message: string; horaFin: string }> => {
    const response = await apiClient.post<{ message: string; horaFin: string }>(`/Fichajes/Resume/${id}`);
    return response.data;
  }
};
