namespace POS.Application.Dtos.Sale.Response
{
    public class SaleResponseDto
    {
        public int SaleId { get; set; }

        public string? VoucherDescription { get; set; } 

        public string? VoucherNumber { get; set; }

        public string? Client { get; set; } 
             
        public decimal TotalAmount { get; set; }

        public DateTime DateOfSale { get; set; }

    }
}
