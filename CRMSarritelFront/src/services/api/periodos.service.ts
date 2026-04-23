import apiClient from "@/lib/axios";

import { PeriodoFacturacion } from "@/types";

export const PeriodosService = {
  getAll: async () => {
    const response = await apiClient.get<PeriodoFacturacion[]>("/Periodos");
    return response.data;
  },
  getById: async (id: number) => {
    const response = await apiClient.get<PeriodoFacturacion>(`/Periodos/${id}`);
    return response.data;
  },
  create: async (data: Partial<PeriodoFacturacion>) => {
    const response = await apiClient.post<PeriodoFacturacion>("/Periodos", data);
    return response.data;
  },
  update: async (id: number, data: Partial<PeriodoFacturacion>) => {
    const response = await apiClient.put(`/Periodos/${id}`, data);
    return response.data;
  },
  delete: async (id: number) => {
    const response = await apiClient.delete(`/Periodos/${id}`);
    return response.data;
  },
  setPrincipal: async (id: number) => {
    const response = await apiClient.patch(`/Periodos/${id}/set-principal`);
    return response.data;
  },
  recalculate: async (id: number) => {
    const response = await apiClient.post(`/Periodos/${id}/recalculate`);
    return response.data;
  }
};
