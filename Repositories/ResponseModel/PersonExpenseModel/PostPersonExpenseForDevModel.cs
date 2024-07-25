using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ResponseModel.PersonExpenseModel
{
    public class PostPersonExpenseForDevModel
    {
        public string ExpenseId { get; set; } = null!;
        public string PersonId { get; set; } = null!;
        public double Amount { get; set; }
        public bool IsShared { get; set; }
        public string? ReportId { get; set; }

    }
}
