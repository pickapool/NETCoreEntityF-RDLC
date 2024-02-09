using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebAPI.DBContexts;
using WebAPI.Extension;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("global/")]
    [ApiController]
    public class GlobalController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public GlobalController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("QRCode")]
        [Obsolete]
        public async Task<ActionResult<string>> ExtractQR(CamImage imageData)
        {
            try
            {
                var splitObject = imageData.imageDataBase64.Split(',');
                byte[] imagebyteData = Convert.FromBase64String((splitObject.Length > 1) ? splitObject[1] : splitObject[0]);
                IronBarCode.License.LicenseKey = "IRONSUITE.CYRILLECONM0817.GMAIL.COM.22495-EC4A10B306-EPVKW-QKGICOSY5BHM-YGKV6BCIXJJW-AVJ73E7L7GHB-MYUPZXQOAMBF-K5FE4U2HKABY-AKW2KIC4LHQF-5PNKKK-TZ2XNEN22NKLEA-DEPLOYMENT.TRIAL-6YUAP3.TRIAL.EXPIRES.05.DEC.2023";
                using (var ms = new MemoryStream(imagebyteData))
                {
                    System.Drawing.Image barcodeImage = System.Drawing.Image.FromStream(ms);
                    var result = IronBarCode.BarcodeReader.QuicklyReadOneBarcode(barcodeImage);
                    if (result == null || result.Value == null)
                    {
                        return "";
                    }
                    return result.Value;
                }
            }
            catch (Exception ex)
            {
                return $"Exception is {ex.Message}";
            }
        }
        [HttpGet]
        [Route("GetStudentByQR/{qr}")]
        public async Task<ActionResult<StudentModel>> GetStudentByQR(string qr)
        {
            return await _context.Students
                .Include(c => c.Course)
                .Include(d => d.Department)
                .Include(us => us.Sanctions).ThenInclude(s => s.Sanction)
                .Include(s => s.Section).FirstAsync(s => s.QRCode == qr);
        }
        [HttpGet]
        [Route("AndroidGetStudentByQR/{qr}")]
        public async Task<ActionResult<StudentModel>> AndroidGetStudentByQR(string qr)
        {
            return await _context.Students
                .Include(c => c.Course)
                .Include(d => d.Department)
                .Include(s => s.Section).FirstAsync(s => s.QRCode == qr);
        }
    }
}
