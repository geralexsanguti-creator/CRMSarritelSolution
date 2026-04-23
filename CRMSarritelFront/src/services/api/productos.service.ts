import apiClient from '@/lib/axios';
import { Producto } from '@/types';

export const ProductosService = {
  getAll: async () => {
    const response = await apiClient.get<Producto[]>('/Productos');
    return response.data;
  },
  
  getById: async (id: number) => {
    const response = await apiClient.get<Producto>(`/Productos/${id}`);
    return response.data;
  },
  
  create: async (producto: Partial<Producto>) => {
    // Solo enviar propiedades primitivas para evitar TypeErrors y problemas de tracking EF
    const payload = {
      nombre: producto.nombre,
      descripcion: producto.descripcion,
      precio: producto.precio,
      precioOferta: producto.precioOferta,
      cantidad: producto.cantidad,
      esInfinito: producto.esInfinito,
      fechaLimite: producto.fechaLimite,
      imagen: producto.imagen,
      tipoVentaId: producto.tipoVentaId,
      proveedorId: producto.proveedorId,
      carpetaIds: producto.carpetaIds,
      activo: producto.activo,
      fechaActivacion: producto.fechaActivacion,
      fechaBaja: producto.fechaBaja
    };
    const response = await apiClient.post<Producto>('/Productos', payload);
    return response.data;
  },
  
  update: async (id: number, producto: Partial<Producto>) => {
    // Solo enviar propiedades primitivas para evitar TypeErrors y problemas de tracking EF
    const payload = {
      id: id,
      nombre: producto.nombre,
      descripcion: producto.descripcion,
      precio: producto.precio,
      precioOferta: producto.precioOferta,
      cantidad: producto.cantidad,
      esInfinito: producto.esInfinito,
      fechaLimite: producto.fechaLimite,
      imagen: producto.imagen,
      tipoVentaId: producto.tipoVentaId,
      proveedorId: producto.proveedorId,
      carpetaIds: producto.carpetaIds,
      activo: producto.activo,
      fechaActivacion: producto.fechaActivacion,
      fechaBaja: producto.fechaBaja
    };
    const response = await apiClient.put(`/Productos/${id}`, payload);
    return response.data;
  },
  
  delete: async (id: number) => {
    const response = await apiClient.delete(`/Productos/${id}`);
    return response.data;
  }
};
