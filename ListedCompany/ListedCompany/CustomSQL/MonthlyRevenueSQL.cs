using System.Text;

namespace ListedCompany.CustomSQL;

public class MonthlyRevenueSQL
{
    public string GetFilterCommand()
    {
        var command = new StringBuilder();

        command.Append($@"
SELECT a.CompanyID,b.CompanyName, b.Industry , a.RevenueCurrentMonth, 
a.RevenuePreviousMonth, a.RevenueChangePreviousMonth
FROM MON_REVENUE a
INNER JOIN COMPANY_DATA b ON a.CompanyID = b.CompanyID
WHERE a.CompanyID = @CompanyID
");

        return command.ToString();
    }


}
