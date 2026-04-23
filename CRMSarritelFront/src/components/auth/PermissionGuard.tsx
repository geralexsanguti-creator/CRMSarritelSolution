import React from 'react';
import { useAuth } from '@/contexts/AuthContext';

interface PermissionGuardProps {
  permission: string;
  children: React.ReactNode;
  fallback?: React.ReactNode;
}

/**
 * Componente para proteger elementos de la UI basados en permisos.
 * Si el usuario no tiene el permiso, muestra el fallback o nada.
 */
export const PermissionGuard: React.FC<PermissionGuardProps> = ({ 
  permission, 
  children, 
  fallback = null 
}) => {
  const { hasPermission } = useAuth();

  if (hasPermission(permission)) {
    return <>{children}</>;
  }

  return <>{fallback}</>;
};

export default PermissionGuard;
