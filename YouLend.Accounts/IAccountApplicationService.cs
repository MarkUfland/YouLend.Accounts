using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouLend.Accounts
{
    public interface IAccountApplicationService
    {
        void NewLoanSetup(NewLoanCommand newLoanCommand);
    }
}
