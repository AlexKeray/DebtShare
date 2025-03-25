using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebtShare.Data.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace DebtShare.Data.Models
{
    public class Item : BaseModelExtended
    {
        [Required]
        [Precision(18, 4)]
        public decimal Price { get; set; }

        public int? MerchantId { get; set; }

        [ForeignKey(nameof(MerchantId))]
        public virtual Merchant? Merchant { get; set; }
    }
}
