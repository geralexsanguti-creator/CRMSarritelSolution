# 🚀 PROTOCOLO DE GOBERNANZA Y EJECUCIÓN: SUPREMO AGENT-ARCHITECT (ANTIGRAVITY EDITION)

Este documento es el **Master Plan 360°** definitivo para la integración y escalabilidad del CRM Sarritel. 

---

# 🏆 RESUMEN DE IDEAS E IMPLEMENTACIONES (COMPLETADAS)

### FASE 1: Autenticación, Roles y UX Global
- **Handshake .NET (Login DTO)**: Autenticación robusta usando JWT para aislar el acceso.
- **Auth Guards en React**: Expulsadores de rutas nativos que protegen contra FOUC y previenen acceso sin `localStorage`.
- **Rotación de Sesión Segura**: Interceptores globales de Fetching vía Axios preparados para gestionar tokens caducados (Refresh Token en background).
- **Temas Adaptativos "Lovable"**: Engine nativo inyectado en `index.css` y manejado por `next-themes` (Dark/Light estético) además del widget animado de `FloatingLines` para una bienvenida de lujo.

### FASE 2: KPIs & Tableros (TanStack Query y Skeletons)
- **Extracción Analítica (.NET)**: Configuración asíncrona robusta con `DashboardController` y Repositorios integrados a BDD.
- **Ghost UX Recreada**: Skeletons Pixel-Perfect importados a "SalesPage" y "DashboardPage" que bloquean los Layout Shifts de React.
- **Smart Filtering Múltiple**: Los hooks custom (`useSales`, `useComerciales`) escuchan cambios de variables para aplicar debounced search, filtrado de fecha y estatus inmutables sincronizados al endpoint de C#.

### FASE 3: Reconciliación Master (Gestor de Archivos)

- Depuración limpia de todos los modelos "Zombie" sobrantes de la iteraciones pasadas (`Departamento`, `Empleado`).
- Migración `Phase4Reconciliation` efectiva agregando la tabla crítica **`Contratos`**.
- Motor Universal para Subidas `multipart/form-data`: La creación de un EndPoint unificado `ArchivosController` para guardar byte asincrónicos hacia PostgresSQL. 
- La estructuración Front-End con Componentes Modales versátiles, polimórficos como el **FileViewerModal** (para PDFs/PNGs) y el **EntityFilesModal** incrustado en cada perfil individual (Usuario, Cliente, Venta, Contrato y Producto).

---

### 🛠️ FASE 5: Gestión de Recursos Humanos y Seguridad Extendida
1. **Refactorización Estructural**: Renombrar la tabla de `Comerciales` a `Usuarios`. Agregar columnas críticas de RRHH: *Departamento, Puesto, Fecha de Contratación, Salario Base (0 si es autónomo),comisiones(total mensual etc...)*.
2. **Módulo de Fichajes (Time Tracking)**: Desarrollar lógica de base de datos y UI (cronómetro en tiempo real) para registrar entradas y salidas, horas extra, vacaciones, festivos, bajas y permisos. Todo respaldado con un historial inmutable visible para el empleado o administradores.
3. **Control de Acceso (Matriz de Roles y Permisos)**: 
   - Implementar roles base: `Admin (Todo)`, `Backoffice (Gestión de usuarios y ventas paramétrica)`, `Director (Equipos y Ventas globales)`, `Jefe de Equipo (Comerciales a cargo)`, `Colaborador (Ventas e hipotéticos sub-equipos)` y `Autónomo`.
   - Desarrollar interfaz visual para que el Admin manipule estos permisos con granulación.
4. **Vistas Integradas y Limpieza Sidebar**:
   - Crear páginas CRUD completas para la gestión de usuarios, roles, equipos e historiales de fichajes.
   - Todo debe insertarse de manera armónica en el Sidebar actual conservando el estilo "Glassmorphism" Lovable.
   - **Fix**: *Eliminar temporal o permanentemente el acceso directo "Contratos" del frontend ya que genera bugs y ha perdido su propósito actual en el panel*.
5. **Impactar DB y Seguridad Backend**: Actualizar el esquema de PostgreSQL a través de Entity Framework con los nuevos campos, y blindar cada endpoint tras reglas de permisos por Claims (roles).

### 🛠️ FASE 6: Ecosistema Formular y Formularios Dinámicos
1. **Cobertura CRUD Multientidad**: Diseñar todos los formularios (Crear, Editar, Borrar, Visualizar/Cronómetro) para las secciones de Usuarios, Equipos, Fichajes, Roles y Permisos.
2. **Builder Dinámico de "Tipos de Venta"**: 
   - Panel administrador accesible desde `Productos` para agregar Tipos de Venta dinámicos con Categoría y Descripción.
   - Formulario que permita generar llaves-variables `JSON` asimilables (ej: numérico, texto, booleano) que luego puedan inyectarse dinámicamente como requisitos para cualquier vendedor haciendo un alta de venta. 
   - Visualización y filtro de estas configuraciones dinámicas dentro de las estadísticas e histórico.
3. **Pipeline Engine (Estados Modificables)*(CREAR FORMULARIO DE ESTADOS DE VENTAS, dentro del formulario de tipos de ventas)*
   - en la vista de tipos de venta debemos poder modificar los estados de las ventas y que se reflejen en la vista de ventas. asi como el json que se ha creado para el tipo de venta. advirtiendo de que se pueden perder datos si se modifica el json.  
4. **Flujo "Wizard" Mejorado de Ventas**:
   - Agregar el botón "Nuevo Cliente" al inicio del formulario de Ventas para abrir el modal, crear el cliente en caliente y seguir el proceso sin perder progreso.
   - (ya esta creado es el unico creado de la app no lo dupliquemos) Encapsular la venta en Steps funcionales: 1) Definición de Cliente y Tipo Venta; 2) Parseo y llenado interactivo del JSON paramétrico configurado en el paso 2 de esta fase; 3) Datos adicionales de venta y vinculación directa al **Gestor de Archivos Adjuntos**.

### 🛠️ FASE 6.1: Correcciones Mayores y Vistas Faltantes
1. **Sidebar y Forms de Entidades**: Renombrar en Sidebar "Usuarios e estatus" a "Usuarios y roles". Crear formularios funcionales e interactivos para crear y editar `Clientes` y `Usuarios` en sus respectivas tablas.
2. **Fix UX Formularios Extensos**: Corregir el Modal de Productos (hacerlo scrollable / autoajustable a altura del contenido para evitar roturas visuales en pantallas pequeñas).
3. **Módulo de Fichaje Avanzado**: Que el fichaje permita no solo el cronómetro, sino elegir introducir **un rango de fechas y horas** manuales (ideal para Vacaciones o Bajas).
4. **Nueva Vista de Equipos**: Proveer ruta, tabla y un formulario para los `Equipos`, extrayéndolos lógicamente y dotándoles de administración visual propia.
5. **Configuración Tipos de Venta y Estados**: Formularios para administrar los Tipos de Venta y configurar los Estados Inmutables, advirtiendo sobre pérdidas de datos si el JSON cambia. Actualizar UX de Venta para reflejar estas integraciones.
6. **Gestión de Comisiones**: Pantalla exclusiva y formulario para registrar tablas relacionales de Comisiones según la venta / usuario.

### 🛠️ FASE 7: Notificaciones Móviles y Smart Alerts
1. **Sistema de Campana Activa (NavBar)**: Diseñar popovers de notificaciones globales al lado del selector "Día/Noche".
2. **Triggers Operativos**: Disparar alertas inmediatas hacia responsables ("Backoffice" o Superiores en el "Equipo") cada vez que un integrante inicie Fichaje o cree un nuevo lead de Venta.
3. **Triggers Funcionales (Vendedor Real-time)**: Notificar de inmediato a un vendedor titular cuando una de sus ventas cambie de estatus o alguien añada nuevas "notas/comentarios".
4. **SLA (Alertas de Estancamiento)**: Regla Cron enviada al vendedor detallando que si una venta inicial no progresa de su etapa ni se cierra tras más de 20 días hábiles, se catalogue preventivamente o notifique como `Cancelada`.
5. **Pipeline Engine (Estados Modificables)*(CREAR FORMULARIO DE ESTADOS DE VENTAS, dentro del de tipo de venta)*:
   - Aplicar lógica dinámica que siempre obligue a disponer un estado inicial y otro final.
   - Sugerencia base: `🔍 REVISANDO, 🟢 ACTIVO, ⏳ ACTIVO PENDIENTE, 📅 PENDIENTE CITA, ✍️ PENDIENTE FIRMA, ❌ VOID (CANCELACION CONFIG), ⚙️ PENDIENTE ADMINISTRACION, 📊 SCORING, 🚫 CANCELADA`.
   - Persistir en un Log de Entidad quién, cuándo y por qué mutó cada estado de la tabla Ventas.

## 🛠️ FASE 8: Gestor de Importaciones Masivas (Data Importer)

# 📝 DICCIONARIO BASE DE DATOS E IDENTIDAD ACTUAL 

La siguiente es la representación vigente oficial (tras la reconciliación Phase 4) que debe ser acatada por cualquier controlador.

#,Nombre de la Tabla,Descripción Sugerida
1,__EFMigrationsHistory,Historial de migraciones de Entity Framework.
2,Categorias,Categorías de productos o servicios.
3,Clientes,Información de los clientes.
4,CodigosPostales,Catálogo de códigos postales.
5,Comerciales,Datos de agentes de ventas o comerciales.
6,Comisiones,Registro de comisiones por ventas.
7,Contratos,Contratos legales o de servicio.
8,DetalleVentas,Líneas de detalle de cada venta realizada.
9,Municipios,Catálogo de municipios/ciudades.
10,Productos,Catálogo de productos disponibles.
11,Proveedores,Información de los proveedores de mercancía.
12,Provincias,Catálogo de estados o provincias.
13,Roles,"Definición de roles de usuario (Admin, User, etc.)."
14,Sucursales,Ubicaciones físicas de la empresa.
15,UsuarioRoles,Tabla intermedia (N:N) entre Usuarios y Roles.
16,Usuarios,Registro de usuarios del sistema.
17,Ventas,Cabecera de las transacciones de venta.
18,ArchivosAdjuntos,Archivos transversales para adjuntos (Polimórfica).

---

# 🚀 ESTRUCTURA DE PENDIENTES (LO QUE QUEDA POR HACER)


1. **Bulk Uploading Funcional**: Crear una vista centralizada que permita a Super-Usuarios o Administradores importar datos históricos desde `.xlsx` y `.csv`.
2. **Data-Dictionary UX**: Plantear dentro de esta página un "Manual de mapeo" que instruya visualmente al importador sobre los campos obligatorios per entidad en BD para evitar la caída/rechazo de los bulks (Manejo asíncrono limpio previniendo fallos en la persistencia transaccional de PostgreSQL).


## 🛠️ FASE 9:

### RECURSOS HUMANOS

1. Crear un apartado en la gestion de usuarios y roles, donde podemos primero definir cierto tipo de cosas, analiza primero bien como esta hecha la relacion de comisiones para despues hacer esto a la perfeccion, debemos hacer un panel interactivo con valores importantes para el usuario, como su salario base, comisiones, etc tienes esta info de ayuda y usa todas las skills posibles para ejecutarlo


Bloque Optimizado: Módulo de RRHH y Liquidación Foral (Navarra 2025-2026)
1. Configuración de Identidad y Régimen
El sistema debe clasificar a cada usuario bajo uno de los siguientes perfiles para determinar su lógica de cálculo:

Asalariado (Régimen General): Cálculo de nómina con Seguridad Social, MEI (0,80%) y escala progresiva de IRPF Navarra.

Autónomo/Profesional: Generación de factura con IVA (21%) y retención fija (15% o 7% para nuevos).

Colaborador Ocasional: Liquidación mediante recibo con retención fija (15%) sin cuotas de SS.

2. Gestión de Intervalos Dinámicos (Admin)
Implementar una función en el sidebar inferior donde el administrador define el Intervalo de Liquidación:

Selección en Calendario: El administrador elige una fecha de inicio y una fecha/hora de cierre.

Sincronización de Devengo: El sistema debe capturar automáticamente todas las ventas/comisiones cerradas estrictamente dentro de ese marco temporal.

Ajuste de Cotización: Si el intervalo es parcial, el sistema debe calcular el salario base por días naturales, pero ajustar la base de cotización a la norma de "30 días" para evitar errores de validación con la Seguridad Social.

3. Motor de Cálculo "Navarra-Specific"
El motor no debe usar tablas estatales, sino la Escala de la Hacienda Foral de Navarra 2025:



🏗️ Especificación Técnica: Arquitectura de Liquidación Foral Navarra (v.2025-2026)
1. Motor de Filtrado de Datos (Hard-Filter)
Para garantizar una experiencia de usuario limpia y sin ambigüedades, el sistema implementará una exclusión lógica por estado:

Gatekeeper de Estados: El motor de cálculo solo consultará objetos de la base de datos cuyo atributo status sea estrictamente TERMINADO o FINALIZADO.

Exclusión Silenciosa: Cualquier registro en estados intermedios (Pending, Processing, Validating) será omitido del flujo de datos (query level).

Objetivo: Eliminar el ruido visual y mensajes de error preventivos. Si no está terminado, es invisible para el intervalo actual.

2. Refactorización de Perfiles y Herencia de Equipos
Persistencia de Roles: Optimización del método de actualización de usuarios para asegurar la integridad referencial en el cambio de roles, evitando excepciones de servidor.

Módulo de Equipos: * Asignación: Vinculación dinámica a equipos existentes.

Contexto de Equipo: El usuario hereda la metadata del equipo (Nombre, Departamento, Centro de Coste) para el encabezado de la nómina/factura.

Autonomía de Liquidación: La herencia es informativa; el cálculo financiero permanece indexado exclusivamente al rendimiento individual (comisiones terminadas del usuario).

Higiene de Datos: Depuración total de la base de datos eliminando el campo comision_historica y sus dependencias.

3. Diccionario de Configuración (JSON Maestro)
Este objeto define las reglas fiscales y de negocio. Su estructura permite cambios rápidos en tipos impositivos sin modificar la lógica del core.

JSON
{
  "version_protocol": "Navarra_Foral_2025_Final",
  "business_logic": {
    "valid_commission_status": ["TERMINADO", "FINALIZADO"],
    "strict_mode": true,
    "payroll_day_basis": 30
  },
  "tax_engine_navarra": {
    "irpf_scaling_table": [
      { "limit": 14500, "rates": [0, 0, 0, 0] },
      { "limit": 28250, "rates": [15.8, 14.5, 13.7, 12.4] },
      { "limit": 35750, "rates": [18.1, 17.1, 16.5, 14.7] },
      { "limit": 62000, "rates": [26.1, 25.5, 24.5, 24.0] },
      { "limit": "infinity", "rates": [43.0, 42.9, 42.8, 42.5] }
    ],
    "social_security_rules": {
      "mei_rate": 0.008,
      "max_base_cap": 4909.50,
      "solidarity_tier_one": 0.0092
    }
  }
}
4. Prompt de Ejecución Final para Antigravity
"Genera una actualización del módulo de RRHH centrada en la eficiencia operativa en Navarra.

Primero: Corrige la lógica de edición de roles para evitar errores de sistema y habilita la asignación a equipos existentes, asegurando que el usuario herede la información del equipo en sus documentos de pago. Elimina definitivamente el campo 'Comisión Histórica'.

Segundo: Implementa en el sidebar un selector de intervalos de tiempo. Al activarse, el sistema debe ejecutar una consulta filtrada que solo incluya comisiones TERMINADAS. Cualquier otra comisión debe ser ignorada sin generar logs o avisos al administrador.

Tercero: Integra el motor de cálculo foral utilizando el JSON de Navarra 2025.

Para Asalariados: Calcular Salario Base + Comisiones Terminadas - (IRPF Navarra + SS + MEI).

Para Autónomos: Generar factura con Base (Comisiones) + IVA 21% - Retención 15%.

Cuarto: El resultado debe ser una pre-visualización simplificada de la 'Nómina Neta' o 'Factura Neta' con un botón de descarga directa a PDF profesional, optimizado para impresión inmediata."




### Roles, permisos 

      🏗️ Especificación Técnica: Panel de Roles Dinámico y Piramidal
1. El Concepto: Matriz de Permisos 3D
Para que sea "super variable" como pides, el sistema debe cruzar tres ejes en cada funcionalidad:

El Módulo: (Ventas, Clientes, Facturas, RRHH).

La Funcionalidad (Acción): (Ver, Crear, Editar, Eliminar, Llamar, Exportar).

El Alcance (Scope Piramidal):

Nivel 1 (Individual): Solo registros creados por el propio usuario.

Nivel 2 (Equipo): Registros propios + de los miembros de sus equipos.

Nivel 3 (Global): Acceso total a todos los usuarios de la organización.

2. Interfaz de Usuario "Drag & Drop" o "Toggle"
El panel debe permitir al administrador:

Encendido/Apagado de Funciones: Activar o recoger permisos específicos como "Botón de Llamada" o "Botón de Exportación" por cada rol.

Selector de Visibilidad (Pirámide): Un menú desplegable o selector de tres niveles para decidir qué registros ve ese rol dentro de cada módulo.

3. Estructura JSON de Permisos (Ejemplo de Flexibilidad)
Este esquema permite que el rol sea totalmente maleable:

JSON
{
  "rol_id": "team_leader_ventas",
  "permisos": [
    {
      "modulo": "ventas",
      "acciones": {
        "ver": { "acceso": true, "alcance": "EQUIPO" },
        "editar": { "acceso": true, "alcance": "PROPIO" },
        "eliminar": { "acceso": false },
        "llamar": { "acceso": true }
      }
    },
    {
      "modulo": "rrhh",
      "acciones": {
        "ver": { "acceso": true, "alcance": "PROPIO" },
        "imprimir_nomina": { "acceso": true }
      }
    }
  ]
}
4. Prompt Final Optimizado para Antigravity
"Desarrolla un panel de configuración de roles basado en una jerarquía piramidal flexible. El sistema debe permitir asignar o recoger permisos de forma atómica para cada función de la aplicación.

Lógica de Configuración:

Funcionalidades Variables: Para cada módulo (Ventas, Usuarios, Equipos, RRHH), el administrador debe poder activar o desactivar acciones específicas (Ej: Botón de Llamar, Editar, Añadir, Eliminar, Ver campos sensibles).

Esquema Piramidal de Visibilidad: Implementa un selector de 'Alcance de Datos' para cada permiso de visualización:

Vista Propia: El usuario solo interactúa con sus propios datos.

Vista de Equipo: El usuario accede a sus datos y a los de los miembros de los equipos donde esté asignado.

Vista General: Acceso a toda la base de datos de la organización.

Interactividad: El panel debe ser una lista de módulos expandibles donde, al abrir un módulo, aparezcan todas sus herramientas disponibles con su respectivo selector de nivel de acceso.

Herencia Dinámica: Asegúrate de que, al asignar un usuario a un equipo, el sistema aplique inmediatamente la lógica de visibilidad de 'Equipo' definida en su rol."

## 🛠️ FASE 10: Integración del Motor de IA y Chatbot Híbrido "SarriChat"

### 1. Arquitectura de Mensajería Tripartita
Debes desarrollar un motor de chat con tres canales diferenciados:
- **Canal IA (@sarriAI)**: Interfaz directa con el modelo de lenguaje. Debe tener memoria de contexto (historial de la sesión).0.
- **Canal P2P (Privado)**: Chat cifrado cliente-a-cliente.
- **Canal Grupal (Teams)**: Espacios colaborativos por departamento o equipo.

### 2. UI/UX "WhatsApp Premium" & Framer Motion
- Botón flotante animado con indicador de notificaciones. Efectos de rebote, doble check y status in-time de conexión.
> **Skill Discovery Obligatorio**: `npx skills find framer-motion-chat-ui`.

### 3. WebSockets e Inteligencia Contextual
- **Context Aware IA**: El chatbot lee el entorno. *"Analiza las ventas de esta tabla"*.
- **SignalR en .NET**: Tiempo real bi-direccional instantáneo en la BD.
- **Motor File-Sharing**: Compatibilidad con el `ArchivosController` para previsualizaciones in-chat.

---

## 📏 PROTOCOLO DE ACTUALIZACIÓN CONSTANTE
Al realizar un paso estructural de la FASE 5 en adelante:
1. Actualizar Código.
2. Actualizar `database.md` según se afecte.
3. Marcar los checkbox en `task.md`.
3. Modificar en `guia.md`.
4. Pasar al siguiente micro-paso.

***El Backend es Inmutable. La Identidad Lovable es Sagrada.***
