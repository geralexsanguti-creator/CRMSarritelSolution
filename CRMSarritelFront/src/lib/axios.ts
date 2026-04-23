import axios from 'axios';

// Utilizando la variable de entorno, o un fallback
const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5066/api';

const apiClient = axios.create({
    baseURL: API_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

// Interceptor de peticiones (Request)
apiClient.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token');
        if (token) {
          config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Interceptor de respuestas (Response)
apiClient.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        // Manejo global de errores, como 401 Unauthorized
        if (error.response?.status === 401) {
            // Removemos el token para forzar la salida en caso de token inválido o caducado
            localStorage.removeItem('token');
            localStorage.removeItem('user');
            
            // Dispatch a custom event to notify the application (e.g., AuthContext) that the session has expired
            window.dispatchEvent(new Event('session_expired'));
        }
        return Promise.reject(error);
    }
);

export default apiClient;
