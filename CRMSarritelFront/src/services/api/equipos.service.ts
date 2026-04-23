import axios from '@/lib/axios';
import { Equipo } from '@/types';

export const equiposService = {
  getAll: async () => {
    const response = await axios.get<Equipo[]>('/Equipos');
    return response.data;
  },

  getById: async (id: number) => {
    const response = await axios.get<Equipo>(`/Equipos/${id}`);
    return response.data;
  },

  create: async (data: Partial<Equipo>) => {
    const response = await axios.post<Equipo>('/Equipos', data);
    return response.data;
  },

  update: async (id: number, data: Partial<Equipo>) => {
    const response = await axios.put(`/Equipos/${id}`, data);
    return response.data;
  },

  delete: async (id: number) => {
    const response = await axios.delete(`/Equipos/${id}`);
    return response.data;
  },

  syncUsuarios: async (equipoId: number, participantes: { usuarioId: number, esManager: boolean }[]) => {
    const response = await axios.post(`/Equipos/${equipoId}/Usuarios`, participantes);
    return response.data;
  }
};
