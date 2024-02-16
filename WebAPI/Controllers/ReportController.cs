using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Reflection;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("Report/")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpPost]
        [Route(template: "GetMasterListReport")]
        public IActionResult GetMasterListReport([FromBody] List<StudentModel> departments)
        {

            using var rs = Assembly.GetExecutingAssembly().GetManifestResourceStream("WebAPI.Reports.MasterListReport.rdlc");

            LocalReport report = new();
            report.LoadReportDefinition(rs);
            report.DataSources.Add(new ReportDataSource("masterlist", departments));
            //report.SetParameters(new[] {
            //    new ReportParameter("EmployeeName", filterParameter.PreparedBy.ToUpper()),
            //    new ReportParameter("PreparedByName", filterParameter.PreparedBy.ToUpper()),
            //    new ReportParameter("PreparedByDesignation", filterParameter.PreparedByDesignation.ToUpper()),
            //    new ReportParameter("Headers", GlobalExtension.Headers(filterParameter))
            //});

            return File(report.Render("PDF"), "application/pdf", "report." + "pdf");
        }
    }
}
