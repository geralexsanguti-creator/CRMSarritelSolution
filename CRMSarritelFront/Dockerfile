# ================== BUILD STAGE ==================
FROM node:20-alpine AS builder
WORKDIR /app

# Copiar package files primero (mejor cache)
COPY package*.json ./
COPY package-lock.json* ./

# Instalar dependencias
RUN npm ci --legacy-peer-deps || npm install --legacy-peer-deps

# Copiar el resto del código
COPY . .

# Construir la aplicación
RUN npm run build

# ================== PRODUCTION STAGE ==================
FROM nginx:alpine AS production
WORKDIR /usr/share/nginx/html

# Copiar los archivos compilados
COPY --from=builder /app/dist ./

# Copiar configuración de Nginx
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]