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
    public class ItemExpense : BaseModel
    {
        [Required]
        public int ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual required Item Item { get; set; }

        [Required]
        public int ExpenseId { get; set; }

        [ForeignKey(nameof(ExpenseId))]
        public virtual required Expense Expense { get; set; }
    }
}
