using innovation4austria.web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace innovation4austria.web.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            /// zeige bereits abgerechnete monate an

            /// monatsauswahl?
            /// anzeige daneben rechnungen wenn schon abgerechnet 
            /// oder eben button zum abrechnen .
            /// 
            List<InvoiceModel> model = new List<InvoiceModel>();
            for (int i = 0; i < 5; i++)
            {
                model.Add(new InvoiceModel()
                {
                    CompanyName = "abc" + i,
                    ID = i,
                    InvoicePeriod = "1.1.2017",
                    Sum = 1000+i * 100
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string date)
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Show(string date)
        {


            return View();
        }

        public ActionResult Download(int id)
        {
            /// see: http://stackoverflow.com/questions/564650/convert-html-to-pdf-in-net
            /// Download HtmlRenderer.PdfSharp nuget package!!
            /// 
            object model = null;
            ViewDataDictionary viewData = new ViewDataDictionary(model);

            // The string writer where to render the HTML code of the view
            StringWriter stringWriter = new StringWriter();

            // Render the Index view in a HTML string
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, "Invoice", null);
            ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    viewData,
                    new TempDataDictionary(),
                    stringWriter
                    );
            viewResult.View.Render(viewContext, stringWriter);

            // Get the view HTML string
            string htmlToConvert = stringWriter.ToString();

            // Convert the HTML string to a PDF document in a memory buffer            
            byte[] outPdfBuffer = PdfSharpConvert(htmlToConvert);

            // Send the PDF file to browser
            FileResult fileResult = new FileContentResult(outPdfBuffer, "application/pdf");
            fileResult.FileDownloadName = "Rechnung.pdf";

            return fileResult;
        }
        public static Byte[] PdfSharpConvert(String html)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator
                    .GeneratePdf(html, PdfSharp.PageSize.A4);
                
                pdf.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }

    } 

}