using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebtShare.Data.Models.Shared;

namespace DebtShare.Data.Models
{
    public class ItemExpenseGrpoupUser : BaseModel
    {
        [Required]
        public int ItemExpenseId { get; set; }

        [ForeignKey(nameof(ItemExpenseId))]
        public virtual ItemExpense ItemExpense { get; set; } = null!;

        [Required]
        public int GroupUserId { get; set; }

        [ForeignKey(nameof(GroupUserId))]
        public virtual GroupUser GroupUser { get; set; } = null!;
    }
}
