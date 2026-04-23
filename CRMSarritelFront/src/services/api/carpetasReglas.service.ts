import api from '@/lib/axios';
import { CarpetaReglas, CarpetaReglasCreateDto } from '@/types/proveedor.types';

export const carpetasReglasService = {
  // Obtener todas las carpetas
  getAll: async () => {
    const response = await api.get<CarpetaReglas[]>('/CarpetasReglas');
    return response.data;
  },

  // Obtener una carpeta por ID
  getById: async (id: number) => {
    const response = await api.get<CarpetaReglas>(`/CarpetasReglas/${id}`);
    return response.data;
  },

  // Crear una nueva carpeta — envía solo los campos que el DTO del backend espera
  create: async (data: CarpetaReglasCreateDto) => {
    const payload = {
      nombre: data.nombre,
      activo: data.activo,
      proveedorId: data.proveedorId,
      carpetaReglasComision: data.reglaIds.map(id => ({ reglaComisionId: id }))
    };
    const response = await api.post<CarpetaReglas>('/CarpetasReglas', payload);
    return response.data;
  },

  // Actualizar una carpeta existente
  update: async (id: number, data: CarpetaReglasCreateDto) => {
    const payload = {
      nombre: data.nombre,
      activo: data.activo,
      proveedorId: data.proveedorId,
      carpetaReglasComision: data.reglaIds.map(rid => ({ reglaComisionId: rid }))
    };
    const response = await api.put(`/CarpetasReglas/${id}`, payload);
    return response.data;
  },

  // Eliminar una carpeta
  delete: async (id: number) => {
    const response = await api.delete(`/CarpetasReglas/${id}`);
    return response.data;
  }
};
