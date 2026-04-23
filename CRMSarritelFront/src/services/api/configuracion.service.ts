import apiClient from '../../lib/axios';

export interface TipoVentaConfig {
  codigo: string;
  nombre: string;
}

export interface EstadoVentaConfig {
  codigo: string;
  nombre: string;
  color: string;
  icono: string;
  orden: number;
  esInicial: boolean;
  esFinal: boolean;
}

export interface PipelineConfig {
  tiposVenta: TipoVentaConfig[];
  estadosVenta: EstadoVentaConfig[];
}

export const ConfiguracionService = {
  getPipelineConfig: async (): Promise<PipelineConfig> => {
    const { data } = await apiClient.get<PipelineConfig>('/Configuracion/Pipeline');
    return data;
  },

  savePipelineConfig: async (config: PipelineConfig): Promise<PipelineConfig> => {
    const { data } = await apiClient.post<PipelineConfig>('/Configuracion/Pipeline', config);
    return data;
  }
};
