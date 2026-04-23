export interface User {
  id: string;
  email: string;
  nombre: string;
  roles: string[];
  permissions: string[];
  canViewAllCommissions?: boolean;
  isManager?: boolean;
  managedEquipoIds?: number[];
  imagen_Url?: string; // Profile image from backend
  rol_Nombre?: string; // Display role name
}

export interface LoginRequest {
  username: string; // The backend expects 'username' according to UsuarioDto
  password: string; // The backend expects 'password' according to UsuarioDto
}

export interface AuthResponse {
  token: string;
  usuario: User;
  canViewAllCommissions: boolean;
  isManager: boolean;
  managedEquipoIds: number[];
}

export interface AuthContextType {
  user: User | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  login: (credentials: LoginRequest) => Promise<void>;
  logout: () => void;
}
