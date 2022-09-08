using KF.Core.Data;
using KF.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KF.CommonModel.Models
{
    public class StockModel : BaseEntity
    {
        
        public Guid StockId { get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public double Quantity { get; set; }
        [JsonIgnore]
        public virtual Product? Product { get; set; } = null!;
    }
}
