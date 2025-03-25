using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtShare.Data.Models.Shared
{
    public class BaseModelExtended : BaseModel, IHasCreationDate
    {
        public required string Name { get; set; }

        public DateTime? CreationDate { get; set; } = DateTime.UtcNow;
    }
}
