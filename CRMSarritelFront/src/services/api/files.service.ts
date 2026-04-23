import apiClient from '@/lib/axios';

export const FilesService = {
  uploadImage: async (file: File): Promise<string> => {
    const formData = new FormData();
    formData.append('file', file);
    
    // As per ASP.NET typical multipart upload.
    const response = await apiClient.post('/Files/upload', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });

    return response.data.url; // We return { url: "filename.jpg" } from backend
  }
};
