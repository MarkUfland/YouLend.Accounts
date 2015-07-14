using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouLend.Accounts
{
    public class AccountApplicationService : IAccountApplicationService
    {
        public void NewLoanSetup(NewLoanCommand newLoanCommand)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=YLWIN81DEV1\SQLEXPRESS;Initial Catalog=YouLend.Accounts;Integrated Security=True"))
            { 
                con.Open();
                using( SqlCommand command = new SqlCommand( "INSERT INTO Events (EventProcessingTime) Values( '" + DateTime.UtcNow.ToString() + "')", con ) )
                {
                    command.ExecuteNonQuery();                    
                }

            }
        }
    }
}
