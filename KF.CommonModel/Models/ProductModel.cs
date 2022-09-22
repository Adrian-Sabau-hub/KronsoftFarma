using KF.Core.Data;

namespace KF.CommonModel.Models
{
    public class ProductModel : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string Producer { get; set; } = null!;

    }
}
