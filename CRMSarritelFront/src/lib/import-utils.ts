/**
 * Busca una propiedad en un objeto de forma insensible a mayúsculas
 * @param obj El objeto donde buscar (ej: una fila del CSV)
 * @param keys Una o más claves a buscar (ej: ['nombre', 'name'])
 * @returns El valor encontrado o undefined
 */
export function getInsensitive(obj: any, keys: string | string[]): any {
    if (!obj) return undefined;
    
    // Normalización: quitamos BOM, caracteres no alfanuméricos y pasamos a minúsculas
    const normalize = (s: string) => 
        String(s).replace(/^\uFEFF/, '')
                 .replace(/[^a-z0-9]/gi, '')
                 .toLowerCase()
                 .trim();

    const searchKeys = Array.isArray(keys) 
        ? keys.map(normalize) 
        : [normalize(keys)];
    
    const objKeys = Object.keys(obj);
    const foundKey = objKeys.find(k => {
        const normalizedKey = normalize(k);
        return searchKeys.includes(normalizedKey);
    });
    
    return foundKey ? obj[foundKey] : undefined;
}
