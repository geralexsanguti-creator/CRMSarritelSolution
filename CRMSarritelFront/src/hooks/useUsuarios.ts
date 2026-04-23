import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import apiClient from '@/lib/axios';
import { Usuario } from '@/types';
import { UsuariosService } from '@/services/api/usuarios.service';
import { toast } from 'sonner';

export function useUsuarios() {
  const queryClient = useQueryClient();

  const query = useQuery({
    queryKey: ['usuarios'],
    queryFn: UsuariosService.getAll,
    staleTime: 1000 * 60 * 5,
  });

  const createUsuarioMutation = useMutation({
    mutationFn: (data: any) => UsuariosService.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['usuarios'] });
      toast.success('Usuario creado correctamente');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al crear el usuario');
    }
  });

  const updateUsuarioMutation = useMutation({
    mutationFn: (param: { id: number, data: Partial<Usuario> }) => UsuariosService.update(param.id, param.data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['usuarios'] });
      toast.success('Usuario actualizado correctamente');
    },
    onError: (error: any) => {
      console.error('Update error:', error);
      const msg = error.response?.data?.message || 'Error al actualizar el usuario';
      const detail = error.response?.data?.errors?.join(', ');
      toast.error(detail ? `${msg}: ${detail}` : msg);
    }
  });

  const deleteUsuarioMutation = useMutation({
    mutationFn: (id: number) => UsuariosService.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['usuarios'] });
      toast.success('Usuario eliminado correctamente');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.message || 'Error al eliminar el usuario');
    }
  });

  return {
    data: query.data || [],
    isLoading: query.isLoading,
    isError: query.isError,
    createUsuario: createUsuarioMutation.mutateAsync,
    updateUsuario: updateUsuarioMutation.mutateAsync,
    deleteUsuario: deleteUsuarioMutation.mutateAsync,
  };
}

export function useUsuario(id: number | null) {
  return useQuery({
    queryKey: ['usuario', id],
    queryFn: () => UsuariosService.getById(id!),
    enabled: !!id,
    staleTime: 1000 * 60 * 5,
  });
}
