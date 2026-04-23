import { Toaster } from "@/components/ui/toaster";
import { Toaster as Sonner } from "@/components/ui/sonner";
import { TooltipProvider } from "@/components/ui/tooltip";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { AppLayout } from "@/components/layout/AppLayout";
import { AuthProvider } from "@/contexts/AuthContext";
import { ProtectedRoute } from "@/components/layout/ProtectedRoute";
import DashboardPage from "./pages/DashboardPage";
import CustomersPage from "./pages/CustomersPage";
import SalesPage from "./pages/SalesPage";
import CommissionsPage from "./pages/CommissionsPage";
import ProductosPage from "./pages/ProductosPage";
import ProveedoresPage from "./pages/ProveedoresPage";
import FichajesPage from "./pages/FichajesPage";
import ContratosPage from "./pages/ContratosPage";
import UsuariosPage from "./pages/UsuariosPage";
import EquiposPage from "./pages/EquiposPage";
import RolesPage from "./pages/RolesPage";
import TiposVentaPage from "./pages/TiposVentaPage";
import LoginPage from "./pages/LoginPage";
import ProfilePage from "./pages/ProfilePage";
import NotFound from "./pages/NotFound";

import { ThemeProvider } from "@/components/theme-provider";

const queryClient = new QueryClient();

const App = () => (
  <QueryClientProvider client={queryClient}>
    <ThemeProvider attribute="class" defaultTheme="system" storageKey="vite-ui-theme">
      <TooltipProvider>
        <Toaster />
        <Sonner />
        <BrowserRouter future={{ v7_startTransition: true, v7_relativeSplatPath: true }}>
          <AuthProvider>
            <Routes>
              <Route path="/login" element={<LoginPage />} />
              
              {/* Protected Routes encapsulated in AppLayout */}
              <Route path="/" element={
                <ProtectedRoute>
                  <AppLayout>
                    <DashboardPage />
                  </AppLayout>
                </ProtectedRoute>
              } />
              <Route path="/clientes" element={
                <ProtectedRoute requiredPermission="clientes:read">
                  <AppLayout>
                    <CustomersPage />
                  </AppLayout>
                </ProtectedRoute>
              } />
              <Route path="/usuarios" element={
                <ProtectedRoute requiredPermission="usuarios:read">
                  <AppLayout>
                    <UsuariosPage />
                  </AppLayout>
                </ProtectedRoute>
              } />
              <Route path="/equipos" element={
                <ProtectedRoute requiredPermission="equipos:read">
                  <AppLayout>
                    <EquiposPage />
                  </AppLayout>
                </ProtectedRoute>
              } />
              <Route path="/roles" element={
                <ProtectedRoute requiredPermission="roles:read">
                  <AppLayout>
                    <RolesPage />
                  </AppLayout>
                </ProtectedRoute>
              } />
              <Route path="/fichajes" element={
                <ProtectedRoute requiredPermission="fichajes:read">
                  <AppLayout>
                    <FichajesPage />
                  </AppLayout>
                </ProtectedRoute>
              } />
              <Route path="/productos" element={
                <ProtectedRoute requiredPermission="productos:read">
                  <AppLayout>
                    <ProductosPage />
                  </AppLayout>
                </ProtectedRoute>
              } />
              <Route path="/proveedores" element={
                <ProtectedRoute requiredPermission="proveedores:read">
                  <AppLayout>
                  <ProveedoresPage />
                </AppLayout>
              </ProtectedRoute>
            } />
            <Route path="/tipos-venta" element={
              <ProtectedRoute requiredPermission="tipos-venta:read">
                <AppLayout>
                  <TiposVentaPage />
                </AppLayout>
              </ProtectedRoute>
            } />
              <Route path="/ventas" element={
                <ProtectedRoute requiredPermission="ventas:read">
                  <AppLayout>
                    <SalesPage />
                  </AppLayout>
                </ProtectedRoute>
              } />
              <Route path="/perfil" element={
                <ProtectedRoute>
                  <AppLayout>
                    <ProfilePage />
                  </AppLayout>
                </ProtectedRoute>
              } />
              <Route path="/perfil/:id" element={
                <ProtectedRoute>
                  <AppLayout>
                    <ProfilePage />
                  </AppLayout>
                </ProtectedRoute>
              } />
              {/* Route Removed temporarily per Phase 5 Requirements 
                <Route path="/contratos" element={
                <ProtectedRoute>
                  <AppLayout>
                    <ContratosPage />
                  </AppLayout>
                </ProtectedRoute>
              } />
              */}
              <Route path="/comisiones" element={
                <ProtectedRoute requiredPermission="comisiones:read">
                  <AppLayout>
                    <CommissionsPage />
                  </AppLayout>
                </ProtectedRoute>
              } />
              
              {/* 404 Route */}
              <Route path="*" element={<NotFound />} />
            </Routes>
          </AuthProvider>
        </BrowserRouter>
      </TooltipProvider>
    </ThemeProvider>
  </QueryClientProvider>
);

export default App;
