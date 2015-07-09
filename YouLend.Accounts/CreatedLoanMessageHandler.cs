using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shuttle.ESB.Core;
using YouLend.Loans.Messages;

namespace YouLend.Accounts
{
    public class CreatedLoanMessageHandler : IMessageHandler<LoanCreatedExternalEvent>
    {
        private IAccountApplicationService accountApplicationService;

        public CreatedLoanMessageHandler(IAccountApplicationService accountApplicationService)
        {
            this.accountApplicationService = accountApplicationService;
        }

        public void ProcessMessage(HandlerContext<LoanCreatedExternalEvent> context)
        {
            NewLoanCommand newLoanCommand = new NewLoanCommand() { LoanAmount = context.Message.LoanAmount };

            this.accountApplicationService.NewLoanSetup(newLoanCommand);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
