# ================== BUILD STAGE ==================
FROM node:20-alpine AS builder

WORKDIR /app

# Copiar archivos de configuración de paquetes
COPY package*.json ./
COPY package-lock.json* ./
COPY yarn.lock* ./

# Instalar dependencias
RUN npm ci --legacy-peer-deps || npm install --legacy-peer-deps

# Copiar el código fuente
COPY . .

# Variables de entorno para la build (si tu frontend necesita la URL de la API)
ARG API_BASE_URL=http://localhost:8080
ENV VITE_API_URL=$API_BASE_URL
ENV REACT_APP_API_URL=$API_BASE_URL

# Construir la aplicación para producción
RUN npm run build

# ================== PRODUCTION STAGE ==================
FROM nginx:stable-alpine AS production

# Instalar curl para healthcheck
RUN apk add --no-cache curl

# Crear directorio para Nginx
WORKDIR /usr/share/nginx/html

# Copiar archivos compilados desde builder
COPY --from=builder /app/dist ./
# Si tu build genera en otra carpeta (ej: build), ajusta la línea anterior:
# COPY --from=builder /app/build ./

# Copiar configuración personalizada de Nginx
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copiar script de entrada personalizado (opcional)
COPY docker-entrypoint.sh /docker-entrypoint.sh
RUN chmod +x /docker-entrypoint.sh

# Verificar que los archivos se copiaron correctamente
RUN ls -la /usr/share/nginx/html

# Healthcheck
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
    CMD curl -f http://localhost/ || exit 1

EXPOSE 80

ENTRYPOINT ["/docker-entrypoint.sh"]
CMD ["nginx", "-g", "daemon off;"]