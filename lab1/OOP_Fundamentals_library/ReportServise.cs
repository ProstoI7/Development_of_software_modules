using OOP_Fundamentals_library;

namespace OOP_Fundamentals_Library
{
    public class ReportService
    {
        public void GenerateReport(IReport report)
        {
            report.GenerateReport();
        }
    }
}
