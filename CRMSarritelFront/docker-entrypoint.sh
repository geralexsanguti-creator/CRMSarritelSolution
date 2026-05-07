#!/bin/sh
set -e

# Reemplazar variables de entorno en archivos estáticos (si es necesario)
# Esto es útil para configurar la URL de la API en tiempo de ejecución

if [ -n "$API_BASE_URL" ]; then
    echo "Configurando API_BASE_URL: $API_BASE_URL"
    
    # Buscar archivos JS que contengan la variable de entorno
    find /usr/share/nginx/html -type f -name "*.js" -exec sed -i "s|VITE_API_URL_PLACEHOLDER|$API_BASE_URL|g" {} \;
    find /usr/share/nginx/html -type f -name "*.js" -exec sed -i "s|REACT_APP_API_URL_PLACEHOLDER|$API_BASE_URL|g" {} \;
fi

echo "Iniciando Nginx..."
exec "$@"