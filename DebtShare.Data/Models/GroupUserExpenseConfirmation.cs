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
    public class GroupUserExpenseConfirmation : BaseModel
    {
        [Required]
        public int ExpenseId { get; set; }

        [ForeignKey(nameof(ExpenseId))]
        public virtual Expense Expense { get; set; } = null!;

        [Required]
        public int GroupUserId { get; set; }

        [ForeignKey(nameof(GroupUserId))]
        public virtual GroupUser GroupUser { get; set; } = null!;

        [Required]
        public int ConfirmationStatusId { get; set; }

        [ForeignKey(nameof(ConfirmationStatusId))]
        public virtual ConfirmationStatus ConfirmationStatus { get; set; } = null!;
    }
}
