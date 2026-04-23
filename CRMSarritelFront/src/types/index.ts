export interface Direccion {
    calle?: string;
    codigoPostal?: string;
    poblacion?: string;
    provincia?: string;
}

export interface Cliente {
    id: number;
    nombre: string;
    dni?: string;
    tipo: string;
    email?: string;
    movil?: string;
    numeroCuenta?: string;
    penalizado: boolean;
    notaPublica?: string;
    notaPrivada?: string;
    createdAt: string;
    updatedAt: string;
    direccion: Direccion;
    
    // Legacy support for flat properties if needed, but we'll try to migrate
    direccion_Calle?: string;
    direccion_CodigoPostal?: string;
    direccion_Poblacion?: string;
    direccion_Provincia?: string;
}

export interface Usuario {
    id: number;
    nombre: string;
    email?: string;
    activo: boolean;
    rol_Nombre?: string;
    departamento?: string;
    puesto?: string;
    fechaContratacion?: string;
    salarioBase?: number;
    comisiones?: number;
    createdAt: string;
    equipos?: UsuarioEquipo[];
    superiorId?: number;
    fotoPerfil?: string;

    // UI Helpers (Match Backend NotMapped properties)
    rolId?: number;
    equipoIds?: number[];
    password?: string;
    usuarioRoles?: UsuarioRol[];
}

export interface UsuarioRol {
    usuarioId: number;
    rolId: number;
    rol?: Rol;
    usuario?: Usuario;
}

export interface Producto {
    id: number;
    nombre: string;
    descripcion: string;
    precio: number;
    precioOferta: number;
    cantidad: number;
    esInfinito?: boolean;
    fechaLimite?: string;
    imagen: string;
    fechaCreation?: string;
    activo?: boolean;
    fechaActivacion?: string;
    fechaBaja?: string;
    tipoVentaId?: number;
    tipoVenta?: {
        id: number;
        nombre: string;
    };
    proveedorId?: number;
    proveedor?: {
        id: number;
        nombre: string;
    };
    carpetaIds?: number[];
    productoCarpetas?: Array<{
        carpetaReglasId: number;
        carpetaReglas?: import('./proveedor.types').CarpetaReglas;
    }>;
}


export interface DetalleVenta {
    id: number;
    cantidad: number;
    total: number;
    datosConfiguracion?: string;
    idVenta?: number;
    idProducto?: number;
    productoNombre?: string;
}

export interface Venta {
    id: number;
    ventaId: number;
    numeroVenta: string;
    clienteId: number;
    usuarioId: number;
    productoPrincipalId?: number;
    creadoPorId?: number;
    fechaCreacion: string;
    fechaVenta: string;
    fechaInicio?: string;
    fechaFin?: string;
    fechaInstalacionPrevista?: string;
    fechaInstalacionReal?: string;
    etapaActual: number;
    montoVenta: number;
    descuentoPorcentaje: number;
    descuentoMonto: number;
    montoTotal: number;
    notas?: string;
    archivoContrato?: string;
    origenVenta: string;
    tipoVentaId?: number;
    createdAt: string;
    updatedAt: string;
    detalles?: DetalleVenta[];

    // Tipos Enbedidos
    tipoVenta_Nombre?: string;
    tipoVenta_Codigo?: string;
    tipoVenta_Descripcion?: string;
    tipoVenta_Activo?: boolean;

    estado_Codigo?: string;
    estado_Nombre?: string;
    estado_Icono?: string;
    estado_Color?: string;
    estado_Orden?: number;
    estado_PermiteEdicion?: boolean;
    estado_PermiteEliminar?: boolean;
    estado_EsFinal?: boolean;
    estado_EsGanada?: boolean;
    estado_Activo?: boolean;
    estado_EsInicial?: boolean;
}

export interface Comision {
    id: number;
    ventaId?: number;
    detalleVentaId?: number;
    proveedorId?: number;
    empleadoId: number;
    periodo?: string;
    tasaPorcentaje?: number;
    montoFijo?: number;
    baseCalculo: number;
    montoComision: number;
    notas?: string;
    fechaCalculo: string;
    fechaPago?: string;
    createdAt: string;
    updatedAt: string;
    vendedorId?: number;
    tipo?: { codigo: string; nombre: string };
    estado?: { codigo: string; nombre: string; color?: string; icono?: string; esPagable?: boolean };

    // Embebidos / DTO fields
    empleadoNombre?: string;
    vendedorNombre?: string;
    venta_Numero?: string;
    productoNombre?: string;
    productoIcono?: string;
    proveedorNombre?: string;
    tipo_Codigo?: string;
    tipo_Nombre?: string;
    estado_Codigo?: string;
    estado_Nombre?: string;
    estado_Color?: string;
    estado_Icono?: string;
    estado_EsPagable?: boolean;
    estado_EsFinal?: boolean;
    estado_Orden?: number;

    appliedReglaNombre?: string;
    appliedTierNombre?: string;
    appliedTierId?: number;
    esExtra?: boolean;
}


export interface Contrato {
    id: number;
    idCliente?: number;
    idUsuario?: number;
    idProducto?: number;
    fecha?: string;
    estado?: string;
    urlContrato?: string;
    checkContrato: boolean;
    nombreArchivo?: string;
    tipoMime?: string;
    tamanoArchivo?: number;
    versionDocumento: number;
    fechaModificacionArchivo?: string;
    idUsuarioSubida?: number;
    comentariosArchivo?: string;
    createdAt: string;
}

export interface ArchivoAdjunto {
    id: number;
    entidadTipo: string;
    entidadId: number;
    nombreArchivo: string;
    tipoMime: string;
    tamanoBytes: number;
    descripcion?: string;
    fechaCreacion: string;
    creadoPorId?: number;
}

export interface Pausa {
    id: number;
    horaInicio: string;
    horaFin?: string;
    motivo?: string;
}

export interface Fichaje {
    id: number;
    usuarioId: number;
    usuarioNombre?: string;
    horaEntrada: string;
    horaSalida?: string;
    tipoRegistro: string;
    horasExtra: number;
    notas?: string;
    createdAt: string;
    updatedAt?: string;
    isPausado?: boolean;
    totalPausasMinutos: number;
    pausas: Pausa[];
}

export interface Rol {
    id: number;
    nombre: string;
}

export interface Permiso {
    id: number;
    nombre: string;
    descripcion?: string;
    modulo: string;
}

export interface Equipo {
    id: number;
    nombre: string;
    descripcion?: string;
    logoUrl?: string;
    jefeEquipoId?: number;
    jefeEquipo?: Usuario;
    usuarioEquipos?: UsuarioEquipo[];
}

export interface UsuarioEquipo {
    usuarioId: number;
    equipoId: number;
    equipo?: Equipo;
    usuario?: Usuario;
    fechaAsignacion: string;
    esManager: boolean;
}

export interface PeriodoFacturacion {
    id: number;
    nombre: string;
    fechaInicio: string;
    fechaFin: string;
    estaCerrado: boolean;
    esPrincipal: boolean;
}

export interface BeneficiarySummary {
    id: number;
    detalleVentaId?: number;
    empleadoId: number;
    nombre: string;
    monto: number;
    tipo: string;
    estado: string;
}

export interface SaleCommissionSummary {
    ventaId: number;
    detalleVentaId?: number;
    numeroVenta: string;
    baseBruta: number;
    totalComisionado: number;
    remanente: number;
    beneficiarios: BeneficiarySummary[];
}

