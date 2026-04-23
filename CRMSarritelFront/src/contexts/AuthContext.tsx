import React, { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import { User, LoginRequest } from '@/types/auth.types';
import { authService } from '@/services/api/auth.service';
import { useNavigate } from 'react-router-dom';
import { useQueryClient } from '@tanstack/react-query';
import { useToast } from '@/hooks/use-toast';

export interface AuthContextType {
  user: User | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  login: (credentials: LoginRequest) => Promise<void>;
  logout: () => void;
  hasPermission: (permission: string) => boolean;
}

export const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { toast } = useToast();

  useEffect(() => {
    // Check for existing session on mount
    const initAuth = () => {
      try {
        const token = localStorage.getItem('token');
        const savedUser = localStorage.getItem('user');
        
        if (token && savedUser && savedUser !== 'undefined') {
          const parsedUser = JSON.parse(savedUser);
          // Normalizar por si acaso hay datos antiguos en localStorage
          const normalized = {
            id: String(parsedUser.id || parsedUser.Id || ""),
            email: parsedUser.email || parsedUser.Email || "",
            nombre: parsedUser.nombre || parsedUser.Nombre || "",
            roles: parsedUser.roles || parsedUser.Roles || [],
            permissions: parsedUser.permissions || parsedUser.Permissions || [],
            canViewAllCommissions: parsedUser.canViewAllCommissions ?? parsedUser.CanViewAllCommissions ?? false,
            isManager: parsedUser.isManager ?? parsedUser.IsManager ?? false,
            managedEquipoIds: parsedUser.managedEquipoIds || parsedUser.ManagedEquipoIds || []
          };
          setUser(normalized as User);
          setIsAuthenticated(true);
        } else if (savedUser === 'undefined') {
          // Clean up bad state from previous bug
          localStorage.removeItem('user');
          localStorage.removeItem('token');
        }
      } catch (e) {
        console.error("Failed to parse user from local storage", e);
        localStorage.removeItem('user');
        localStorage.removeItem('token');
      } finally {
        setIsLoading(false);
      }
    };

    initAuth();

    // Listen to session expiry event
    const handleSessionExpired = () => {
      logout();
      toast({
        title: "Sesión expirada",
        description: "Tu sesión ha expirado, por favor inicia sesión nuevamente.",
        variant: "destructive",
      });
    };

    window.addEventListener('session_expired', handleSessionExpired);
    return () => {
      window.removeEventListener('session_expired', handleSessionExpired);
    };
  }, []);

  const login = async (credentials: LoginRequest) => {
    try {
      setIsLoading(true);
      const data = await authService.login(credentials);
      
      // Store token and user
      localStorage.setItem('token', data.token);
      
      // Normalizar el usuario (el backend a veces devuelve PascalCase y el frontend espera camelCase)
      const normalizedUser = {
        id: String((data.usuario as any).id || (data.usuario as any).Id || ""),
        email: (data.usuario as any).email || (data.usuario as any).Email || "",
        nombre: (data.usuario as any).nombre || (data.usuario as any).Nombre || "",
        roles: (data.usuario as any).roles || (data.usuario as any).Roles || [],
        permissions: (data.usuario as any).permissions || (data.usuario as any).Permissions || [],
        canViewAllCommissions: (data.usuario as any).canViewAllCommissions ?? (data.usuario as any).CanViewAllCommissions ?? data.canViewAllCommissions ?? false,
        isManager: (data.usuario as any).isManager ?? (data.usuario as any).IsManager ?? data.isManager ?? false,
        managedEquipoIds: (data.usuario as any).managedEquipoIds || (data.usuario as any).ManagedEquipoIds || data.managedEquipoIds || []
      };
      
      localStorage.setItem('user', JSON.stringify(normalizedUser));
      
      // Update state
      setUser(normalizedUser as User);
      setIsAuthenticated(true);
      
      toast({
        title: "Bienvenido",
        description: `Has iniciado sesión como ${data.usuario.nombre}`,
      });
      
      // Redirect to dashboard
      navigate('/');
    } catch (error) {
      console.error('Login error:', error);
      throw error;
    } finally {
      setIsLoading(false);
    }
  };

  const logout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    setUser(null);
    setIsAuthenticated(false);
    
    // Clear React Query cache
    queryClient.clear();
    
    // Redirect to login
    navigate('/login');
  };

  const hasPermission = (permission: string): boolean => {
    if (!user) return false;
    
    // Admin role has all permissions by default
    if (user.roles.some(role => role.toLowerCase() === 'admin')) {
      return true;
    }

    // Handle legacy view_all_commissions which mapped to DB column conceptually
    // Also support modern standard permission:
    if ((permission === 'view_all_commissions' || permission.toLowerCase() === 'comisiones:view_all') && user.canViewAllCommissions) {
      return true;
    }
    return user.permissions?.some(p => p.toLowerCase() === permission.toLowerCase()) || false;
  };

  return (
    <AuthContext.Provider value={{ user, isAuthenticated, isLoading, login, logout, hasPermission }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};
