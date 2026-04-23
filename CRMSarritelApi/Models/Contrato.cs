namespace CRMSarritelApi.Models
{
    /*public class Contrato : BaseEntity<int>
    {
            public int? IdCliente { get; set; }
            public int? IdComercial { get; set; }
            public int? IdProducto { get; set; }
            public DateTime? Fecha { get; set; }
            public string? Estado { get; set; }
            public string? UrlContrato { get; set; }
            public bool CheckContrato { get; set; } = false;
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            // Relaciones
            public virtual Cliente? Cliente { get; set; }
            public virtual Comercial? Comercial { get; set; }
            public virtual Producto? Producto { get; set; }
        }
    }*/

    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;

    public class Contrato : BaseEntity<int>
    {
        public int? IdCliente { get; set; }
        public int? IdComercial { get; set; }
        public int? IdProducto { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Estado { get; set; }
        public string? UrlContrato { get; set; }
        public bool CheckContrato { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // NUEVOS CAMPOS - Almacenamiento directo en PostgreSQL con bytea
        [Column(TypeName = "bytea")]
        public byte[]? ArchivoBytes { get; set; }

        public string? NombreArchivo { get; set; }
        public string? TipoMime { get; set; }
        public long? TamanoArchivo { get; set; }

        // Versión del documento
        public int VersionDocumento { get; set; } = 1;

        // Fecha de última modificación
        public DateTime? FechaModificacionArchivo { get; set; }

        // Usuario que subió/modificó el archivo
        public int? IdUsuarioSubida { get; set; }

        // Comentarios sobre el archivo
        public string? ComentariosArchivo { get; set; }

        // Relaciones
        public virtual Cliente? Cliente { get; set; }
        public virtual Usuario? Comercial { get; set; }
        public virtual Producto? Producto { get; set; }
    }
}
