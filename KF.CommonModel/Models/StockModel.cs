using KF.Core.Data;
using KF.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KF.CommonModel.Models
{
    public class StockModel : BaseEntity
    {
        
        public Guid StockId { get; set; }
        public Guid ProductId { get; set; }
        public double Quantity { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
