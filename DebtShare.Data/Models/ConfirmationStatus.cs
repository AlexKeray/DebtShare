using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebtShare.Data.Models.Shared;

namespace DebtShare.Data.Models
{
    public class ConfirmationStatus : BaseModelExtended
    {
        public virtual ICollection<GroupUserExpenseConfirmation>? GroupUserExpenseConfirmation { get; set; }
    }
}
