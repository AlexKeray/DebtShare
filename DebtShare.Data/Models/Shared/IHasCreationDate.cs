using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtShare.Data.Models.Shared
{
    public interface IHasCreationDate
    {
        DateTime? CreationDate { get; set; }
    }
}
