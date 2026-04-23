import apiClient from '../lib/axios';
import { ArchivoAdjunto } from '../types';

export const archivosService = {
  // Obtener la lista de archivos para una entidad específica
  getArchivosPorEntidad: async (entidadTipo: string, entidadId: number): Promise<ArchivoAdjunto[]> => {
    const response = await apiClient.get(`/Archivos/entidad/${entidadTipo}/${entidadId}`);
    return response.data;
  },

  // Subir un nuevo archivo
  subirArchivo: async (entidadTipo: string, entidadId: number, file: File, descripcion?: string): Promise<ArchivoAdjunto> => {
    const formData = new FormData();
    formData.append('entidadTipo', entidadTipo);
    formData.append('entidadId', String(entidadId));
    formData.append('file', file);
    if (descripcion) {
      formData.append('descripcion', descripcion);
    }

    const response = await apiClient.post('/Archivos', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  },

  // Descargar un archivo como Blob (para URL.createObjectURL)
  descargarArchivo: async (id: number): Promise<Blob> => {
    const response = await apiClient.get(`/Archivos/descargar/${id}`, {
      responseType: 'blob', // Importante para obtener el binario
    });
    return response.data;
  },

  // Eliminar un archivo
  eliminarArchivo: async (id: number): Promise<void> => {
    await apiClient.delete(`/Archivos/${id}`);
  }
};
