using KF.Core.Data;
using System;
using System.Collections.Generic;

namespace KF.Core.DomainModels
{
    public partial class Product : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string Producer { get; set; } = null!;

        public virtual Stock Stock { get; set; } = null!;
    }
}
