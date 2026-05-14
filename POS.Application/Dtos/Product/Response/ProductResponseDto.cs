namespace POS.Application.Dtos.Product.Response
{
    public class ProductResponseDto
    {
        public int ProductId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public string? Image { get; set; }
        public decimal SellPrice { get; set; }


        // Propiedades de la Categoría
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; } // Para mostrar en la tabla


        // Propiedades del Proveedor
        public int ProviderId { get; set; }
        public string? ProviderName { get; set; } // Para mostrar en la tabla


        public DateTime? AuditCreateDate { get; set; }
        public int State { get; set; }
        public string? StateProduct { get; set; } // Ej: "Activo" o "Inactivo"
    }
}