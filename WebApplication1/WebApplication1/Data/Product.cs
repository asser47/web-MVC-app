using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class Product
    {
        public required int Id { get; set; }
        //[Required(ErrorMessage = "Please enter your name")]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "Name must between 2 and 50 letters")]
        public required string Name { get; set; }
        //[Required(ErrorMessage = "Please enter SKU")]
        //[RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Please enter valid sku")]
        public required string Sku { get; set; }

    }
}
