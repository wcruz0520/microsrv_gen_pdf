using CrystalService.Models;
using CrystalService.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CrystalService.Controllers
{
    public class RenderController : ApiController
    {
        private static readonly string BaseDir = @"C:\CrystalService";
        private static readonly string RptPath = Path.Combine(BaseDir, "Reportes", "Factura01.rpt");
        private static readonly string AssetsDir = Path.Combine(BaseDir, "Assets");
        private static readonly string TempDir = Path.Combine(BaseDir, "Temp");

        [HttpPost]
        [Route("render/factura01")]
        public HttpResponseMessage Factura01([FromBody] FacturaRenderRequest request)
        {
            try
            {
                var renderer = new CrystalFacturaRenderer(RptPath, AssetsDir, TempDir);
                byte[] pdf = renderer.RenderFactura01(request);

                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(pdf)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                resp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = "factura.pdf"
                };

                return resp;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("health")]
        public IHttpActionResult Health() => Ok("OK");
    }
}
