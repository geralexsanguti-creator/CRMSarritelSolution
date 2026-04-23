export interface EstadoVentaConfig {
  codigo: string;
  nombre: string;
  color: string;
  icono: string;
  orden: number;
  esInicial: boolean;
  esFinal: boolean;
  esGanada: boolean;
}

export interface VariableDefinicion {
  nombre: string;
  etiqueta: string;
  tipo: 'numero' | 'texto' | 'booleano' | 'fecha';
  requerido: boolean;
}

export interface TipoVenta {
  id: number;
  nombre: string;
  codigo?: string;
  descripcion?: string;
  activo: boolean;
  esquemaVariablesJson?: string; // JSON string with variable definitions
  estadosVentaJson?: string; // JSON string with states configuration
  origenesJson?: string; // JSON string with sales origins (e.g. ["presencial", "web"])
}

export interface ReparticionComision {
  id?: number;
  rolId?: number;
  rolNombre?: string;
  rol?: any;
  equipoId?: number;
  equipoNombre?: string;
  equipo?: any;
  tipoCalculo: 'fijo' | 'porcentaje';
  valor: number;
}

export interface ReglaComisionTier {
  id?: number;
  nombre: string;
  valorMin: number;
  valorMax?: number;
  tipoRemuneracionGross?: 'fijo' | 'porcentaje';
  valorRemuneracionGross?: number;
  reparticiones: ReparticionComision[];
}

export interface ReglaComision {
  id: number;
  nombre: string;
  descripcion?: string;
  variable: string;
  operador: '>' | '<' | '>=' | '<=' | '=' | 'between' | 'any';
  valorMin?: number;
  valorMax?: number;
  
  tipoRemuneracionGross: 'fijo' | 'porcentaje';
  valorRemuneracionGross: number;
  
  reparticionesBase: ReparticionComision[];
  tiers: ReglaComisionTier[];
  valorVenta: number; // Mandatory sale value

  proveedorId?: number;
  proveedor?: Proveedor;
  tipoVentaId?: number;
  tipoVenta?: TipoVenta;
  activa: boolean;
  prioridad: number;
  carpetaReglasComision?: CarpetaReglaComision[];
}

export interface Proveedor {
  id: number;
  nombre: string;
  nombrePlataforma?: string;
  cif?: string;
  web?: string;
  emailContacto?: string;
  telefono?: string;
  logoUrl?: string;
  activo: boolean;
  tiposVenta?: TipoVenta[];
  reglas?: ReglaComision[];
  totalProductos?: number;
  totalReglas?: number;
  tipoVentaIds?: number[];
}

export interface ProveedorCreateDto {
  nombre: string;
  nombrePlataforma?: string;
  cif?: string;
  web?: string;
  emailContacto?: string;
  telefono?: string;
  logoUrl?: string;
  activo: boolean;
  tipoVentaIds?: number[];
}

export interface ReglaComisionCreateDto {
  id?: number;
  nombre: string;
  descripcion?: string;
  variable: string;
  operador: string;
  valorMin?: number;
  valorMax?: number;
  tipoRemuneracionGross: string;
  valorRemuneracionGross: number;
  reparticionesBase: ReparticionComision[];
  tiers: ReglaComisionTier[];
  proveedorId: number;
  tipoVentaId?: number;
  activa: boolean;
  prioridad: number;
  valorVenta: number;
}

export interface TipoVentaCreateDto {
  nombre: string;
  codigo?: string;
  descripcion?: string;
  activo: boolean;
  esquemaVariablesJson?: string;
  estadosVentaJson?: string;
  origenesJson?: string;
}

export interface CarpetaReglaComision {
  carpetaReglasId: number;
  carpetaReglas?: CarpetaReglas;
  reglaComisionId: number;
  reglaComision?: ReglaComision;
}

export interface CarpetaReglas {
  id: number;
  nombre: string;
  activo: boolean;
  proveedorId?: number;
  proveedor?: Proveedor;
  carpetaReglasComision?: CarpetaReglaComision[];
}

export interface CarpetaReglasCreateDto {
  id?: number;
  nombre: string;
  activo: boolean;
  proveedorId?: number;
  reglaIds: number[];
}


