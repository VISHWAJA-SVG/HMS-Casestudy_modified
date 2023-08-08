using AutoMapper;
using HotelManagement_backend.DomainModels;
using HotelManagement_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace HotelManagement_backend.Controllers
{
   // [Authorize(Roles ="Receptionist")]
    public class BillController : Controller
    {
        private readonly IBillRepository billRepository;
        private readonly IMapper mapper;
        public BillController(IBillRepository billRepository,IMapper mapper)
        {
            this.billRepository = billRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]/{billId:Guid}"), ActionName("GetBillAsync")]
        public async Task<IActionResult> GetBillAsync([FromRoute] Guid billId)
        {
            var bill = await billRepository.GetBillAsync(billId);
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Bill>(bill));
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddBillDetailsAsync([FromBody] AddBillRequest request)
        {
            var bill = await billRepository.AddBillDetails(mapper.Map<DataModels.Bill>(request));
            return CreatedAtAction(nameof(GetBillAsync), new { billId = bill.BillId },
                mapper.Map<Bill>(bill));
        }

        [HttpGet("generatepdf")]
        public async Task<IActionResult> GeneratePDF(Guid billId)
        {
            var document = new PdfDocument();
            string HtmlContent = "<h1  style=\"text-align: center;\">Welocme to Hotel Carnivals</h1>";
            HtmlContent += "<p style=\"text-align: center;\">20b,Airoli,NaviMumbai-541337,Mumbai</p>";
           var header = await this.billRepository.GetBillAsync(billId);
            
            if (header != null)
            {
                HtmlContent += "<table style=\"border-collapse: collapse;\">";
                HtmlContent += "<tr><th style=\"border: 1px solid black;\">Bill Number</th><th style=\"border: 1px solid black;\">Check-In Date</th><th style=\"border: 1px solid black;\">Check-Out Date</th><th style=\"border: 1px solid black;\">Tax Amount</th><th style=\"border: 1px solid black;\">Total Amount</th></tr>";
                HtmlContent += "<tr>";
                HtmlContent += "<td style=\"border: 1px solid black;\">" + header.BillId + "</td>";
                HtmlContent += "<td style=\"border: 1px solid black;\">" + header.CheckinDate + "</td>";
                HtmlContent += "<td style=\"border: 1px solid black;\">" + header.CheckoutDate + "</td>";
                HtmlContent += "<td style=\"border: 1px solid black;\">" + header.TaxAmount + "</td>";
                HtmlContent += "<td style=\"border: 1px solid black;\">" + header.TotalAmount + "</td>";
                HtmlContent += "</tr>";
                HtmlContent += "</table>";
                HtmlContent += "<p>Please Deposit Room Key Card</p>";
                HtmlContent += "<p>Thank You, Visit Again</p>";
            }

            PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);

            byte[]? response = null;
            using(MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                //PdfGenerator.GeneratePdf(HtmlContent, PdfSharp.PageSize.A4,60);
                response = ms.ToArray();
            }
            string Filename = "Bill_" + billId + ".pdf";
            return File(response, "application/pdf", Filename);
        }
       
    }
}
