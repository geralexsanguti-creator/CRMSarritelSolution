import { useState, useEffect, useMemo } from 'react';
import { motion } from 'framer-motion';
import {
  Building2, Plus, Edit, Trash2, Globe, Mail, Phone, Tag,
  MoreVertical, AlertCircle, Check, LayoutGrid, List as ListIcon,
  ArrowRight, CheckCircle2, Percent, Coins, Layers, Trash, Search, Folder
} from 'lucide-react';
import { RolesService } from '@/services/api/roles.service';
import type { Rol } from '@/types';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import { Badge } from '@/components/ui/badge';
import { Separator } from '@/components/ui/separator';
import { CarpetasReglasPanel } from '@/components/proveedores/CarpetasReglasPanel';
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from '@/components/ui/dialog';
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from '@/components/ui/dropdown-menu';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { KpiCard } from '@/components/shared/KpiCard';
import { UnifiedSearchBar } from '@/components/shared/UnifiedSearchBar';
import { ResponsiveModal } from '@/components/shared/ResponsiveModal';
import { cn, getUploadUrl } from '@/lib/utils';
import { glass, card, flexCenter } from '@/lib/styles';
import { useAuth } from '@/hooks/useAuth';
import { toast } from 'sonner';
import { ProveedoresService, TiposVentaService, ReglasComisionService } from '@/services/api/proveedores.service';
import { ReglaComisionWizard } from '@/components/proveedores/ReglaComisionWizard';
import { ImageUpload } from '@/components/shared/ImageUpload';
import type { Proveedor, TipoVenta, ReglaComision, ProveedorCreateDto, ReglaComisionCreateDto, ReparticionComision, ReglaComisionTier } from '@/types/proveedor.types';

// ─── Constants ───────────────────────────────────────────────────────────────
const OPERADORES = [
  { value: '>', label: 'Mayor que (>)' },
  { value: '<', label: 'Menor que (<)' },
  { value: '>=', label: 'Mayor o igual (≥)' },
  { value: '<=', label: 'Menor o igual (≤)' },
  { value: '=', label: 'Igual a (=)' },
  { value: 'between', label: 'Entre (rango)' },
  { value: 'any', label: 'Cualquier valor' },
];

const operadorLabel = (op: string) => OPERADORES.find(o => o.value === op)?.label ?? op;

const getVariableLabel = (regla: ReglaComision, tiposVenta: TipoVenta[]) => {
  if (regla.variable === 'total_ventas') return 'Nº Total de Ventas';
  const tv = tiposVenta.find(x => x.id === regla.tipoVentaId);
  if (tv?.esquemaVariablesJson) {
     try {
       const schema = JSON.parse(tv.esquemaVariablesJson);
       if (Array.isArray(schema)) {
         const v = schema.find((x: any) => x.nombre === regla.variable);
         if (v) return v.etiqueta;
       }
     } catch (e) {}
  }
  return regla.variable;
};

const getRuleRoles = (regla: ReglaComision) => {
  const roles = new Set<string>();
  // Base splits
  regla.reparticionesBase?.forEach(r => {
    if (r.rol?.nombre) roles.add(r.rol.nombre);
    if (r.equipo?.nombre) roles.add(`Líder ${r.equipo.nombre}`);
  });
  // Tier splits
  regla.tiers?.forEach(t => {
    t.reparticiones?.forEach(r => {
      if (r.rol?.nombre) roles.add(r.rol.nombre);
      if (r.equipo?.nombre) roles.add(`Líder ${r.equipo.nombre}`);
    });
  });
  return Array.from(roles);
};

const getRuleFolder = (regla: ReglaComision) => {
  // Support both camelCase and PascalCase just in case or null values
  const list = regla.carpetaReglasComision || (regla as any).CarpetaReglasComision;
  if (!list || !Array.isArray(list)) return null;

  const folders = list.filter(crc => {
    const folder = crc.carpetaReglas || crc.CarpetaReglas;
    return folder && (folder.activo !== false);
  });
  
  if (folders.length > 0) {
    const folder = folders[0].carpetaReglas || folders[0].CarpetaReglas;
    return folder?.nombre;
  }
  return null;
};

const CARD_COLORS = [
  'from-primary/80 to-primary',
  'from-violet-500 to-violet-700',
  'from-emerald-500 to-emerald-700',
  'from-amber-500 to-amber-700',
  'from-rose-500 to-rose-700',
];

// ─── Proveedor Form Modal ─────────────────────────────────────────────────────
function ProveedorModal({ proveedor, tiposVenta, onClose, onSave }: {
  proveedor?: Proveedor | null;
  tiposVenta: TipoVenta[];
  onClose: () => void;
  onSave: (data: ProveedorCreateDto) => Promise<void>;
}) {
  const [form, setForm] = useState<ProveedorCreateDto>({
    nombre: proveedor?.nombre ?? '',
    nombrePlataforma: proveedor?.nombrePlataforma ?? '',
    cif: proveedor?.cif ?? '',
    web: proveedor?.web ?? '',
    emailContacto: proveedor?.emailContacto ?? '',
    telefono: proveedor?.telefono ?? '',
    logoUrl: proveedor?.logoUrl ?? '',
    activo: proveedor?.activo ?? true,
    tipoVentaIds: proveedor?.tiposVenta?.map(tv => tv.id) ?? [],
  });

  useEffect(() => {
    if (proveedor) {
      setForm({
        nombre: proveedor.nombre ?? '',
        nombrePlataforma: proveedor.nombrePlataforma ?? '',
        cif: proveedor.cif ?? '',
        web: proveedor.web ?? '',
        emailContacto: proveedor.emailContacto ?? '',
        telefono: proveedor.telefono ?? '',
        logoUrl: proveedor.logoUrl ?? '',
        activo: proveedor.activo ?? true,
        tipoVentaIds: proveedor.tiposVenta?.map(tv => tv.id) ?? [],
      });
    } else {
      setForm({
        nombre: '',
        nombrePlataforma: '',
        cif: '',
        web: '',
        emailContacto: '',
        telefono: '',
        logoUrl: '',
        activo: true,
        tipoVentaIds: [],
      });
    }
  }, [proveedor]);

  const [saving, setSaving] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSaving(true);
    try { await onSave(form); } finally { setSaving(false); }
  };

  const toggleTipoVenta = (id: number) => {
    setForm(f => {
      const ids = f.tipoVentaIds || [];
      const newIds = ids.includes(id) ? ids.filter(i => i !== id) : [...ids, id];
      return { ...f, tipoVentaIds: newIds };
    });
  };

  return (
    <DialogContent className="glass border-white/10 max-w-lg">
      <DialogHeader>
        <DialogTitle className="text-lg font-bold">
          {proveedor ? 'Editar Proveedor' : 'Nuevo Proveedor'}
        </DialogTitle>
      </DialogHeader>
      <form onSubmit={handleSubmit} className="space-y-4 pt-2 overflow-y-auto max-h-[80vh] px-1">
        <div className="grid grid-cols-2 gap-4">
          <div className="col-span-2 space-y-2">
            <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Nombre *</Label>
            <Input required value={form.nombre} onChange={e => setForm(f => ({...f, nombre: e.target.value}))}
              placeholder="Ej: Movistar" className="bg-background/40 border-border/40 h-10 font-medium" />
          </div>

          <div className="col-span-2 flex gap-4 items-center">
            <div className="w-24 h-24 shrink-0">
              <ImageUpload 
                value={form.logoUrl} 
                onChange={(val) => setForm(f => ({...f, logoUrl: val || 'default.png'}))} 
                placeholder="Subir Logo"
                className="rounded-lg"
              />
            </div>
            <div className="flex-1 space-y-2">
              <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">URL del Logo (Opcional)</Label>
              <Input value={form.logoUrl === 'default.png' ? '' : form.logoUrl} onChange={e => setForm(f => ({...f, logoUrl: e.target.value}))}
                className="bg-background/40 border-border/40 h-10" placeholder="Pega una URL o usa el botón para subir" />
            </div>
          </div>
          
          <div className="col-span-2 space-y-3 p-3 rounded-lg border border-border/40 bg-muted/20">
            <Label className="text-[10px] font-black uppercase tracking-widest text-primary flex items-center gap-2">
              <Layers className="w-3 h-3" /> Tipos de Venta Operados
            </Label>
            <div className="grid grid-cols-2 gap-2">
              {tiposVenta.map(tv => (
                <div 
                  key={tv.id} 
                  onClick={() => toggleTipoVenta(tv.id)}
                  className={cn(
                    "flex items-center gap-2 p-2 rounded-md border cursor-pointer transition-all text-xs font-medium",
                    form.tipoVentaIds?.includes(tv.id) 
                      ? "bg-primary/20 border-primary text-primary" 
                      : "bg-background/40 border-border/40 text-muted-foreground hover:bg-background/60"
                  )}
                >
                  <div className={cn(
                    "w-3 h-3 rounded-sm border flex items-center justify-center",
                    form.tipoVentaIds?.includes(tv.id) ? "bg-primary border-primary" : "border-muted-foreground"
                  )}>
                    {form.tipoVentaIds?.includes(tv.id) && <Check className="w-2.5 h-2.5 text-white" />}
                  </div>
                  {tv.nombre}
                </div>
              ))}
            </div>
            {(!form.tipoVentaIds || form.tipoVentaIds.length === 0) && (
              <p className="text-[10px] text-amber-500 font-medium italic">Debe seleccionar al menos un tipo de venta para operar.</p>
            )}
          </div>

          <div className="space-y-2">
            <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Plataforma</Label>
            <Input value={form.nombrePlataforma} onChange={e => setForm(f => ({...f, nombrePlataforma: e.target.value}))}
              placeholder="Ej: Salesforce" className="bg-background/40 border-border/40 h-10" />
          </div>
          <div className="space-y-2">
            <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">CIF</Label>
            <Input value={form.cif} onChange={e => setForm(f => ({...f, cif: e.target.value}))}
              className="bg-background/40 border-border/40 h-10" />
          </div>
          <div className="space-y-2">
            <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Email contacto</Label>
            <Input type="email" value={form.emailContacto} onChange={e => setForm(f => ({...f, emailContacto: e.target.value}))}
              className="bg-background/40 border-border/40 h-10" />
          </div>
          <div className="space-y-2">
            <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Teléfono</Label>
            <Input type="tel" value={form.telefono} onChange={e => setForm(f => ({...f, telefono: e.target.value}))}
              className="bg-background/40 border-border/40 h-10" />
          </div>
          <div className="space-y-2">
            <Label className="text-xs font-bold uppercase tracking-wider text-muted-foreground">Sitio Web</Label>
            <Input value={form.web} onChange={e => setForm(f => ({...f, web: e.target.value}))}
              placeholder="https://..." className="bg-background/40 border-border/40 h-10" />
          </div>
          <div className="col-span-2 flex items-center gap-3 pt-1">
            <input type="checkbox" id="activo-prov" checked={form.activo}
              onChange={e => setForm(f => ({...f, activo: e.target.checked}))}
              className="h-4 w-4 rounded border-border text-primary" />
            <Label htmlFor="activo-prov" className="text-sm font-medium cursor-pointer">Proveedor activo</Label>
          </div>
        </div>
        <DialogFooter className="mt-6 flex gap-2">
          <Button type="button" variant="ghost" onClick={onClose}>Cancelar</Button>
          <Button type="submit" disabled={saving} className="bg-primary hover:bg-primary/90 shadow-glow px-6 font-bold gap-2">
            {saving
              ? <span className="animate-spin w-4 h-4 border-2 border-white/30 border-t-white rounded-full" />
              : <Check className="w-4 h-4" />}
            {proveedor ? 'Guardar cambios' : 'Crear proveedor'}
          </Button>
        </DialogFooter>
      </form>
    </DialogContent>
  );
}

// ─── Main Page ────────────────────────────────────────────────────────────────
export default function ProveedoresPage() {
  const { hasPermission } = useAuth();
  const canCreate = hasPermission('proveedores:create');
  const canEdit   = hasPermission('proveedores:update');
  const canDelete = hasPermission('proveedores:delete');

  const [proveedores, setProveedores] = useState<Proveedor[]>([]);
  const [tiposVenta, setTiposVenta] = useState<TipoVenta[]>([]);
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState('');
  const [viewMode, setViewMode] = useState<'grid' | 'table'>('grid');
  const [selected, setSelected] = useState<Proveedor | null>(null);
  const [showProveedorModal, setShowProveedorModal] = useState(false);
  const [editingProveedor, setEditingProveedor] = useState<Proveedor | null>(null);

  // Detail panel — rule management state
  const [reglas, setReglas] = useState<ReglaComision[]>([]);
  const [reglaLoading, setReglaLoading] = useState(false);
  const [showReglaModal, setShowReglaModal] = useState(false);
  const [editingRegla, setEditingRegla] = useState<ReglaComision | null>(null);
  const [selectionMode, setSelectionMode] = useState(false);
  const [selectedIds, setSelectedIds] = useState<Set<number>>(new Set());
  const [ruleSearch, setRuleSearch] = useState('');

  const loadData = async () => {
    try {
      const [prov, tv] = await Promise.all([
        ProveedoresService.getAll(),
        TiposVentaService.getAll(),
      ]);
      setProveedores(prov);
      setTiposVenta(tv);
    } catch {
      toast.error('Error al cargar proveedores');
    } finally {
      setLoading(false);
    }
  };

  const loadReglas = async (proveedorId: number) => {
    setReglaLoading(true);
    try {
      const data = await ReglasComisionService.getByProveedor(proveedorId);
      setReglas(data);
    } catch { 
      toast.error('Error al cargar reglas de comisión');
    } finally { setReglaLoading(false); }
  };

  useEffect(() => { loadData(); }, []);
  useEffect(() => {
    if (selected) { 
      loadReglas(selected.id); 
      setRuleSearch('');
    } else { 
      setReglas([]); 
    }
  }, [selected?.id]);

  const filtered = useMemo(() => {
    if (!search) return proveedores;
    const q = search.toLowerCase();
    return proveedores.filter(p =>
      p.nombre.toLowerCase().includes(q) ||
      (p.nombrePlataforma ?? '').toLowerCase().includes(q) ||
      (p.emailContacto ?? '').toLowerCase().includes(q)
    );
  }, [proveedores, search]);

  const kpis = useMemo(() => [
    { title: 'Total Proveedores', value: String(proveedores.length), icon: Building2, sparkData: [2, 3, 3, 4, 5, 5, 6] },
    { title: 'Activos', value: String(proveedores.filter(p => p.activo).length), icon: CheckCircle2, sparkData: [1, 2, 2, 3, 4, 4, 5] },
    { title: 'Tipos de venta', value: String(tiposVenta.length), icon: Tag, sparkData: [0, 1, 1, 2, 2, 3, 4] },
  ], [proveedores, tiposVenta]);

  const handleSaveProveedor = async (data: ProveedorCreateDto) => {
    if (editingProveedor) {
      await ProveedoresService.update(editingProveedor.id, data);
      toast.success('Proveedor actualizado');
    } else {
      await ProveedoresService.create(data);
      toast.success('Proveedor creado');
    }
    setShowProveedorModal(false);
    setEditingProveedor(null);
    await loadData();
  };

  const handleDelete = async (id: number) => {
    if (!confirm('¿Eliminar este proveedor y todos sus datos?')) return;
    try {
      await ProveedoresService.delete(id);
      toast.success('Proveedor eliminado');
      if (selected?.id === id) setSelected(null);
      setProveedores(ps => ps.filter(p => p.id !== id));
    } catch {
      toast.error('Error al eliminar el proveedor');
    }
  };

  const handleSaveRegla = async (data: ReglaComisionCreateDto) => {
    if (editingRegla) {
      // Validar inmutabilidad
      const { hasCommissions } = await ReglasComisionService.checkCommissions(editingRegla.id);
      if (hasCommissions) {
        toast.info('Esta regla ya tiene historial financiero. Se ha creado una versión clonada automáticamente.', { duration: 5000 });
        // Crear como nueva (clon)
        const dtoclone = { ...data, id: undefined, nombre: `${data.nombre} (v2)` };
        await ReglasComisionService.create(dtoclone);
        
        // Opcional: Podríamos desactivar la vieja, pero es más seguro dejarla "activa" si sigue operando 
        // o simplemente la dejamos. De momento clonamos.
      } else {
        await ReglasComisionService.update(editingRegla.id, { ...data, id: editingRegla.id });
        toast.success('Regla actualizada');
      }
    } else {
      await ReglasComisionService.create(data);
      toast.success('Regla creada');
    }
    setShowReglaModal(false);
    setEditingRegla(null);
    if (selected) loadReglas(selected.id);
  };

  const handleDeleteRegla = async (id: number) => {
    if (!confirm('¿Eliminar esta regla?')) return;
    await ReglasComisionService.delete(id);
    toast.success('Regla eliminada');
    setReglas(r => r.filter(x => x.id !== id));
  };

  return (
    <div className="space-y-6 animate-in fade-in duration-500 pb-20 sm:pb-6">
      {/* Header */}
      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl font-bold text-foreground">Gestión de Proveedores</h1>
          <p className="text-sm text-muted-foreground">Administra tus proveedores y sus reglas de retribución</p>
        </div>
        <div className="flex items-center gap-2">
          <div className="flex items-center glass rounded-lg p-1 border border-border/50">
            <Button variant={viewMode === 'grid' ? 'secondary' : 'ghost'} size="icon"
              onClick={() => setViewMode('grid')} className="h-8 w-8 px-0" title="Vista cuadrícula">
              <LayoutGrid className="h-4 w-4" />
            </Button>
            <Button variant={viewMode === 'table' ? 'secondary' : 'ghost'} size="icon"
              onClick={() => setViewMode('table')} className="h-8 w-8 px-0" title="Vista lista">
              <ListIcon className="h-4 w-4" />
            </Button>
          </div>
          {canCreate && (
            <Button onClick={() => { setEditingProveedor(null); setShowProveedorModal(true); }}
              className="bg-primary text-primary-foreground hover:bg-primary/90 gap-2 font-bold px-6 shadow-glow">
              <Plus className="h-4 w-4" /> Nuevo Proveedor
            </Button>
          )}
        </div>
      </div>

      {/* KPI Cards */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {kpis.map((kpi, i) => <KpiCard key={kpi.title} {...kpi} index={i} />)}
      </div>

      {/* Search */}
      <UnifiedSearchBar
        selectionMode={selectionMode}
        onSelectionModeToggle={() => setSelectionMode(!selectionMode)}
        searchValue={search}
        onSearchChange={setSearch}
        searchPlaceholder="Buscar proveedores por nombre, plataforma o email..."
        onClearFilters={() => setSearch('')}
      />

      {/* Content */}
      {loading ? (
        <div className="h-64 flex flex-col items-center justify-center space-y-4">
          <div className="w-12 h-12 border-4 border-primary border-t-transparent rounded-full animate-spin" />
          <p className="text-muted-foreground animate-pulse">Cargando proveedores...</p>
        </div>
      ) : filtered.length === 0 ? (
        <div className={cn('h-64', card, flexCenter, 'flex-col space-y-4 text-center')}>
          <div className="h-16 w-16 rounded-full bg-muted/20 flex items-center justify-center">
            <Building2 className="h-8 w-8 text-muted-foreground" />
          </div>
          <div>
            <h3 className="text-lg font-medium">No se encontraron proveedores</h3>
            <p className="text-muted-foreground max-w-xs mx-auto text-sm">
              {search ? 'Prueba con otra búsqueda' : 'Crea tu primer proveedor para empezar.'}
            </p>
          </div>
        </div>
      ) : viewMode === 'grid' ? (
        // Grid view
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {filtered.map((p, i) => (
            <motion.div
              key={p.id}
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: i * 0.05 }}
              onClick={() => setSelected(s => s?.id === p.id ? null : p)}
              className={cn('group transition-all hover:scale-[1.01] cursor-pointer', card,
                selected?.id === p.id ? 'ring-2 ring-primary/50 border-primary/30' : '')}
            >
              <div className="p-6">
                <div className="flex items-start justify-between mb-4">
                  <div className="flex items-center gap-3">
                    {p.logoUrl && p.logoUrl !== "default.png" ? (
                      <div className="h-10 w-10 rounded-xl overflow-hidden bg-primary/10 flex items-center justify-center shrink-0 border border-border/50">
                        <img src={`/uploads/${p.logoUrl}`} alt="logo" className="h-full w-full object-cover" />
                      </div>
                    ) : (
                      <div className={cn('h-10 w-10 rounded-xl bg-gradient-to-br flex items-center justify-center text-white font-bold text-lg', CARD_COLORS[p.id % CARD_COLORS.length])}>
                        {p.nombre[0].toUpperCase()}
                      </div>
                    )}
                    <div>
                      <h3 className="font-bold text-base leading-none mb-1">{p.nombre}</h3>
                      <p className="text-[10px] text-muted-foreground flex items-center gap-1 uppercase tracking-wider font-bold">
                        {p.nombrePlataforma ?? (p.activo ? 'Activo' : 'Inactivo')}
                      </p>
                    </div>
                  </div>
                  <DropdownMenu>
                    <DropdownMenuTrigger asChild>
                      <Button variant="ghost" size="icon" className="h-8 w-8 rounded-full hover:bg-muted opacity-0 group-hover:opacity-100 transition-opacity"
                        onClick={e => e.stopPropagation()}>
                        <MoreVertical className="h-4 w-4" />
                      </Button>
                    </DropdownMenuTrigger>
                    <DropdownMenuContent align="end" className="glass">
                      {canEdit && (
                        <DropdownMenuItem onClick={e => { e.stopPropagation(); setEditingProveedor(p); setShowProveedorModal(true); }}>
                          <Edit className="mr-2 h-4 w-4" /> Editar
                        </DropdownMenuItem>
                      )}
                      {canDelete && (
                        <DropdownMenuItem className="text-destructive font-bold" onClick={e => { e.stopPropagation(); handleDelete(p.id); }}>
                          <Trash2 className="mr-2 h-4 w-4" /> Eliminar
                        </DropdownMenuItem>
                      )}
                    </DropdownMenuContent>
                  </DropdownMenu>
                </div>

                <div className="space-y-1.5 mb-4">
                  {p.emailContacto && (
                    <p className="text-xs text-muted-foreground flex items-center gap-1.5 truncate">
                      <Mail className="h-3 w-3 text-primary shrink-0" /> {p.emailContacto}
                    </p>
                  )}
                  {p.telefono && (
                    <p className="text-xs text-muted-foreground flex items-center gap-1.5">
                      <Phone className="h-3 w-3 text-primary shrink-0" /> {p.telefono}
                    </p>
                  )}
                </div>

                {p.tiposVenta && p.tiposVenta.length > 0 && (
                  <div className="flex flex-wrap gap-1 pt-3 border-t border-white/5">
                    {p.tiposVenta.slice(0, 3).map(tv => (
                      <Badge key={tv.id} variant="secondary" className="font-normal text-[10px] bg-muted/30 border-border/40">
                        {tv.nombre}
                      </Badge>
                    ))}
                    {p.tiposVenta.length > 3 && (
                      <Badge variant="outline" className="text-[10px]">+{p.tiposVenta.length - 3}</Badge>
                    )}
                  </div>
                )}

                <div className="mt-4 pt-2 flex justify-end">
                  <div className="h-7 w-7 rounded-lg bg-background flex items-center justify-center border border-border group-hover:bg-primary group-hover:border-primary transition-all">
                    <ArrowRight className="h-3 w-3 text-muted-foreground group-hover:text-primary-foreground" />
                  </div>
                </div>
              </div>
            </motion.div>
          ))}
        </div>
      ) : (
        // Table view
        <div className={cn('overflow-hidden border border-border/50', glass, 'rounded-xl')}>
          <table className="w-full text-left border-collapse">
            <thead className="bg-muted/30">
              <tr>
                <th className="px-6 py-4 font-bold text-xs uppercase tracking-wider text-muted-foreground">Proveedor</th>
                <th className="px-6 py-4 font-bold text-xs uppercase tracking-wider text-muted-foreground">Contacto</th>
                <th className="px-6 py-4 font-bold text-xs uppercase tracking-wider text-muted-foreground">Tipos de venta</th>
                <th className="px-6 py-4 font-bold text-xs uppercase tracking-wider text-muted-foreground">Estado</th>
                <th className="px-6 py-4 font-bold text-xs uppercase tracking-wider text-muted-foreground text-right">Acciones</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-border/30">
              {filtered.map(p => (
                <tr key={p.id} onClick={() => setSelected(s => s?.id === p.id ? null : p)}
                  className={cn('hover:bg-muted/10 transition-colors group cursor-pointer', selected?.id === p.id && 'bg-primary/5')}>
                  <td className="px-6 py-4">
                    <div className="flex items-center gap-3">
                      {p.logoUrl && p.logoUrl !== "default.png" ? (
                        <div className="h-8 w-8 rounded-lg overflow-hidden border border-border/50 bg-primary/10 shrink-0">
                          <img src={getUploadUrl(p.logoUrl)} alt="logo" className="h-full w-full object-cover" />
                        </div>
                      ) : (
                        <div className={cn('h-8 w-8 rounded-lg bg-gradient-to-br flex items-center justify-center text-white text-xs font-bold shrink-0', CARD_COLORS[p.id % CARD_COLORS.length])}>
                          {p.nombre[0].toUpperCase()}
                        </div>
                      )}
                      <div>
                        <span className="font-bold text-sm tracking-tight">{p.nombre}</span>
                        {p.nombrePlataforma && <p className="text-[10px] text-muted-foreground">{p.nombrePlataforma}</p>}
                      </div>
                    </div>
                  </td>
                  <td className="px-6 py-4">
                    <div className="space-y-0.5">
                      {p.emailContacto && <p className="text-xs text-muted-foreground truncate max-w-xs">{p.emailContacto}</p>}
                      {p.telefono && <p className="text-xs text-muted-foreground">{p.telefono}</p>}
                    </div>
                  </td>
                  <td className="px-6 py-4">
                    <div className="flex flex-wrap gap-1">
                      {(p.tiposVenta ?? []).slice(0, 2).map(tv => (
                        <Badge key={tv.id} variant="secondary" className="font-bold text-[10px] bg-primary/5 text-primary border-primary/10">
                          {tv.nombre}
                        </Badge>
                      ))}
                      {(p.tiposVenta?.length ?? 0) > 2 && <Badge variant="outline" className="text-[10px]">+{(p.tiposVenta?.length ?? 0) - 2}</Badge>}
                    </div>
                  </td>
                  <td className="px-6 py-4">
                    <Badge variant={p.activo ? 'secondary' : 'outline'} className={cn(
                      'font-bold text-[10px]',
                      p.activo ? 'bg-emerald-500/10 text-emerald-600 border-emerald-500/20 dark:text-emerald-400' : 'opacity-60'
                    )}>
                      {p.activo ? 'Activo' : 'Inactivo'}
                    </Badge>
                  </td>
                  <td className="px-6 py-4 text-right">
                    <div className="flex justify-end gap-1 opacity-0 group-hover:opacity-100 transition-opacity">
                      {canEdit && (
                        <Button variant="ghost" size="icon" className="h-8 w-8"
                          onClick={e => { e.stopPropagation(); setEditingProveedor(p); setShowProveedorModal(true); }}>
                          <Edit className="h-4 w-4" />
                        </Button>
                      )}
                      {canDelete && (
                        <Button variant="ghost" size="icon" className="h-8 w-8 text-destructive"
                          onClick={e => { e.stopPropagation(); handleDelete(p.id); }}>
                          <Trash2 className="h-4 w-4" />
                        </Button>
                      )}
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      <Dialog open={showProveedorModal} onOpenChange={v => { if (!v) { setShowProveedorModal(false); setEditingProveedor(null); } }}>
        <ProveedorModal
          proveedor={editingProveedor}
          tiposVenta={tiposVenta}
          onClose={() => { setShowProveedorModal(false); setEditingProveedor(null); }}
          onSave={handleSaveProveedor}
        />
      </Dialog>

      {/* Regla create/edit modal */}
      <Dialog open={showReglaModal} onOpenChange={v => { if (!v) { setShowReglaModal(false); setEditingRegla(null); } }}>
        {selected && (
          <ReglaComisionWizard
            regla={editingRegla}
            proveedorId={selected.id}
            tiposVenta={tiposVenta}
            onClose={() => { setShowReglaModal(false); setEditingRegla(null); }}
            onSave={handleSaveRegla}
          />
        )}
      </Dialog>

      {/* Proveedor detail — ResponsiveModal (Sheet), same as CustomersPage */}
      <ResponsiveModal
        open={!!selected}
        onOpenChange={open => !open && setSelected(null)}
        title="Detalle del Proveedor"
        variant="dialog"
        maxWidth="900px"
      >
        {selected && (
          <div className="space-y-6">
            {/* Avatar + name */}
            <div className="flex items-center gap-4">
              <div className={cn('h-14 w-14 rounded-2xl bg-gradient-to-br flex items-center justify-center text-white font-bold text-2xl uppercase shrink-0', CARD_COLORS[selected.id % CARD_COLORS.length])}>
                {selected.nombre.charAt(0)}
              </div>
              <div className="min-w-0">
                <h3 className="font-bold text-lg text-foreground truncate">{selected.nombre}</h3>
                <div className="flex items-center gap-2 flex-wrap">
                  {selected.nombrePlataforma && (
                    <span className="text-xs text-muted-foreground">{selected.nombrePlataforma}</span>
                  )}
                  <Badge variant={selected.activo ? 'secondary' : 'outline'} className={cn(
                    'font-bold text-[10px]',
                    selected.activo ? 'bg-emerald-500/10 text-emerald-600 border-emerald-500/20 dark:text-emerald-400' : 'opacity-60'
                  )}>
                    {selected.activo ? 'Activo' : 'Inactivo'}
                  </Badge>
                </div>
              </div>
            </div>

            {/* Contact info */}
            <div className="space-y-3">
              {selected.emailContacto && (
                <div className="flex items-center gap-3 text-sm font-medium">
                  <Mail className="h-4 w-4 text-primary opacity-70" />
                  <span className="text-foreground truncate">{selected.emailContacto}</span>
                </div>
              )}
              {selected.telefono && (
                <div className="flex items-center gap-3 text-sm font-medium">
                  <Phone className="h-4 w-4 text-primary opacity-70" />
                  <span className="text-foreground">{selected.telefono}</span>
                </div>
              )}
              {selected.web && (
                <div className="flex items-center gap-3 text-sm font-medium">
                  <Globe className="h-4 w-4 text-primary opacity-70" />
                  <a href={selected.web} target="_blank" rel="noopener noreferrer" className="text-primary hover:underline truncate">{selected.web}</a>
                </div>
              )}
              {selected.cif && (
                <div className="flex items-center gap-3 text-sm font-medium">
                  <Tag className="h-4 w-4 text-primary opacity-70" />
                  <span className="text-foreground font-mono">CIF: {selected.cif}</span>
                </div>
              )}
            </div>

            {/* Sales types */}
            {selected.tiposVenta && selected.tiposVenta.length > 0 && (
              <>
                <Separator className="bg-white/5" />
                <div className="space-y-2">
                  <h4 className="text-[10px] font-bold text-muted-foreground uppercase tracking-widest">Tipos de venta</h4>
                  <div className="flex flex-wrap gap-1.5">
                    {selected.tiposVenta.map(tv => (
                      <Badge key={tv.id} variant="secondary" className="font-normal text-[10px] bg-muted/30 border-border/40">
                        <Tag className="w-2.5 h-2.5 mr-1" />{tv.nombre}
                      </Badge>
                    ))}
                  </div>
                </div>
              </>
            )}

            <Separator className="bg-white/5" />

            <CarpetasReglasPanel proveedorId={selected.id} reglas={reglas} />

            <Separator className="bg-white/5" />

            {/* Commission rules */}
            <div className="space-y-4">
              <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-3">
                <h4 className="text-[10px] font-bold text-muted-foreground uppercase tracking-widest">
                  Reglas de comisión ({reglas.length})
                </h4>
                <div className="flex items-center gap-2">
                  <div className="relative">
                    <Search className="absolute left-2.5 top-1/2 -translate-y-1/2 w-3 h-3 text-muted-foreground" />
                    <Input 
                      placeholder="Filtrar reglas..." 
                      value={ruleSearch}
                      onChange={(e) => setRuleSearch(e.target.value)}
                      className="h-7 w-32 sm:w-48 pl-8 text-[10px] bg-muted/20 border-white/5 focus:ring-primary/20"
                    />
                  </div>
                  <Button size="sm" onClick={() => { setEditingRegla(null); setShowReglaModal(true); }}
                    className="h-7 px-3 text-xs bg-primary hover:bg-primary/90 shadow-glow font-bold gap-1">
                    <Plus className="w-3 h-3" /> Nueva
                  </Button>
                </div>
              </div>

              {reglaLoading ? (
                <div className="h-20 flex items-center justify-center">
                  <div className="w-6 h-6 border-2 border-primary border-t-transparent rounded-full animate-spin" />
                </div>
              ) : reglas.length === 0 ? (
                <div className={cn('h-20', flexCenter, 'flex-col gap-2 text-center')}>
                  <AlertCircle className="w-6 h-6 text-muted-foreground/30" />
                  <p className="text-xs text-muted-foreground">Sin reglas definidas</p>
                </div>
              ) : (
                <div className="space-y-2">
                  {reglas
                    .filter(r => 
                      r.nombre.toLowerCase().includes(ruleSearch.toLowerCase()) || 
                      r.variable.toLowerCase().includes(ruleSearch.toLowerCase())
                    )
                    .map(r => (
                    <div key={r.id} className={cn(
                      'rounded-xl border p-3 transition-all group',
                      r.activa ? 'bg-primary/5 border-primary/20' : 'bg-muted/10 border-border/30 opacity-60'
                    )}>
                      <div className="flex items-start justify-between gap-2">
                        <div className="flex-1 min-w-0">
                          <span className="font-bold text-[13px] leading-tight text-foreground block truncate">{r.nombre}</span>
                          <div className="flex flex-wrap gap-1.5 mt-2">
                            <Badge variant="outline" className="text-[10px] font-bold px-2 py-0.5 bg-primary/5 border-primary/20 text-primary flex items-center gap-1">
                              <Layers className="w-3 h-3" />
                              {getVariableLabel(r, tiposVenta)}
                            </Badge>
                            
                            {getRuleRoles(r).map(role => (
                              <Badge key={role} variant="secondary" className="text-[9px] font-medium bg-muted/40 border-transparent px-2 h-5">
                                {role}
                              </Badge>
                            ))}

                            <Badge variant="secondary" className={cn(
                              'text-[10px] font-black px-2 h-5 ml-auto shadow-sm',
                              r.tipoRemuneracionGross === 'porcentaje'
                                ? 'bg-emerald-500/10 text-emerald-600 dark:text-emerald-400 border-emerald-500/20'
                                : 'bg-amber-500/10 text-amber-600 dark:text-amber-400 border-amber-500/20'
                            )}>
                              {r.tipoRemuneracionGross === 'porcentaje' ? `${r.valorRemuneracionGross}%` : `€${r.valorRemuneracionGross.toFixed(2)}`}
                            </Badge>

                            {getRuleFolder(r) && (
                              <div className="w-full mt-1.5 flex items-center gap-1 text-[9px] text-muted-foreground/60 font-bold uppercase tracking-wider">
                                <Folder className="w-2.5 h-2.5" />
                                <span className="truncate">{getRuleFolder(r)}</span>
                              </div>
                            )}
                          </div>
                        </div>
                        <div className="flex gap-0.5 opacity-0 group-hover:opacity-100 transition-opacity shrink-0">
                          {canEdit && (
                            <Button variant="ghost" size="icon" className="h-7 w-7"
                              onClick={() => { setEditingRegla(r); setShowReglaModal(true); }}>
                              <Edit className="w-3.5 h-3.5" />
                            </Button>
                          )}
                          {canDelete && (
                            <Button variant="ghost" size="icon" className="h-7 w-7 text-destructive"
                              onClick={() => handleDeleteRegla(r.id)}>
                              <Trash2 className="w-3.5 h-3.5" />
                            </Button>
                          )}
                        </div>
                      </div>
                    </div>
                  ))}
                  {reglas.filter(r => 
                    r.nombre.toLowerCase().includes(ruleSearch.toLowerCase()) || 
                    r.variable.toLowerCase().includes(ruleSearch.toLowerCase())
                  ).length === 0 && (
                    <div className="p-6 text-center text-xs text-muted-foreground border border-dashed rounded-lg">
                      {reglas.length === 0 ? "Sin reglas definidas." : "No se encontraron reglas con ese filtro."}
                    </div>
                  )}
                </div>
              )}
            </div>

            {/* Actions */}
            <div className="pt-2 border-t border-border flex flex-col gap-2">
              {selected.emailContacto && (
                <a href={`mailto:${selected.emailContacto}`}
                  className="flex items-center justify-center gap-2 py-2.5 rounded-xl border border-primary/20 text-primary hover:bg-primary/10 transition-colors text-sm font-bold shadow-sm">
                  <Mail className="h-4 w-4" /> Enviar email
                </a>
              )}
              {canEdit && (
                <Button variant="secondary" className="flex-1 gap-2 border border-border font-bold"
                  onClick={() => { setEditingProveedor(selected); setShowProveedorModal(true); setSelected(null); }}>
                  <Edit className="h-4 w-4" /> Editar proveedor
                </Button>
              )}
            </div>
          </div>
        )}
      </ResponsiveModal>
    </div>
  );
}
