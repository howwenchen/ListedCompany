using System.Text;

namespace ListedCompany.CustomSQL;

public class MonthlyRevenueSQL
{
    public string GetFilterCommand()
    {
        var command = new StringBuilder();

        command.Append($@"
SELECT a.CompanyID, a.ReportDate, a.DataYearMonth, a.RevenueCurrentMonth, 
a.RevenuePreviousMonth, a.RevenueSameMonthLastYear, a.RevenueChangePreviousMonth,
a.RevenueChangeSameMonthLastYear, a.CumulativeRevenueCurrentMonth, a.CumulativeRevenueLastYear,
a.CumulativeRevenueChangePreviousPeriod, a.Notes, b.CompanyName, b.Industry 
FROM MON_REVENUE a
JOIN COMPANY_DATA b ON a.CompanyID = b.CompanyID
WHERE a.CompanyID = @CompanyID
");

        return command.ToString();
    }


}
