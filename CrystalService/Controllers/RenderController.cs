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
        //private static readonly string BaseDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PlantillasPDF");
        //private static readonly string BaseDir = Path.Combine("C:/Users/WILLIAMC/Desktop", "PlantillasPDF");
        private static readonly string BaseDir = Path.Combine("C:\\API-Facturacion\\FACTURACION_ELECTRONICA_SRI", "PlantillasPDF");
        //private static readonly string BaseDir = @"C:/API-Facturacion/FACTURACION_ELECTRONICA_SRI/PlantillasPDF/";
        //private static readonly string RptPath = Path.Combine(BaseDir, "Factura01.rpt");
        //private static readonly string AssetsDir = Path.Combine(BaseDir, "Assets");
        //private static readonly string TempDir = Path.Combine(BaseDir, "Temp");

        [HttpPost]
        [Route("render/factura01")]
        public HttpResponseMessage Factura01([FromBody] FacturaRenderRequest request)
        {
            try
            {
                string nombreRpt = request.campoAdicional2;
                //string RptPath = Path.Combine(BaseDir, request.infoTributaria.ruc, "Factura01.rpt");
                string RptPath = Path.Combine(BaseDir, request.infoTributaria.ruc, nombreRpt);
                string AssetsDir = Path.Combine(BaseDir, request.infoTributaria.ruc, "Assets");
                string TempDir = Path.Combine(BaseDir, request.infoTributaria.ruc, "Temp");

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

        [HttpPost]
        [Route("render/notacredito")]
        public HttpResponseMessage NotaCredito([FromBody] NotaCreditoRenderRequest request)
        {
            try
            {
                string nombreRpt = request.campoAdicional2;
                //string RptPath = Path.Combine(BaseDir, request.infoTributaria.ruc, "Factura01.rpt");
                string RptPath = Path.Combine(BaseDir, request.infoTributaria.ruc, nombreRpt);
                string AssetsDir = Path.Combine(BaseDir, request.infoTributaria.ruc, "Assets");
                string TempDir = Path.Combine(BaseDir, request.infoTributaria.ruc, "Temp");

                var renderer = new CrystalNotaCreditoRenderer(RptPath, AssetsDir, TempDir);
                byte[] pdf = renderer.RenderNotaCredito(request);

                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(pdf)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                resp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = "notacredito.pdf"
                };

                return resp;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("render/notadebito")]
        public HttpResponseMessage NotaDebito([FromBody] NotaDebitoRenderRequest request)
        {
            try
            {
                string nombreRpt = request.campoAdicional2;
                //string RptPath = Path.Combine(BaseDir, request.infoTributaria.ruc, "Factura01.rpt");
                string RptPath = Path.Combine(BaseDir, request.infoTributaria.ruc, nombreRpt);
                string AssetsDir = Path.Combine(BaseDir, request.infoTributaria.ruc, "Assets");
                string TempDir = Path.Combine(BaseDir, request.infoTributaria.ruc, "Temp");

                var renderer = new CrystalNotaDebitoRenderer(RptPath, AssetsDir, TempDir);
                byte[] pdf = renderer.RenderNotaDebito(request);

                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(pdf)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                resp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = "notadebito.pdf"
                };

                return resp;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("render/guiaremision")]
        public HttpResponseMessage GuiaRemision([FromBody] GuiaRemisionRenderRequest request)
        {
            try
            {
                string nombreRpt = request.campoAdicional2;
                //string RptPath = Path.Combine(BaseDir, request.infoTributaria.ruc, "Factura01.rpt");
                string RptPath = Path.Combine(BaseDir, request.infoTributaria.ruc, nombreRpt);
                string AssetsDir = Path.Combine(BaseDir, request.infoTributaria.ruc, "Assets");
                string TempDir = Path.Combine(BaseDir, request.infoTributaria.ruc, "Temp");

                var renderer = new CrystalGuiaRemisionRenderer(RptPath, AssetsDir, TempDir);
                byte[] pdf = renderer.RenderGuiaRemision(request);

                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(pdf)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                resp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = "guiaremision.pdf"
                };

                return resp;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("render/retencion")]
        public HttpResponseMessage Retencion([FromBody] RetencionRenderRequest request)
        {
            try
            {
                string nombreRpt = request.campoAdicional2;
                //string RptPath = Path.Combine(BaseDir, request.infoTributaria.ruc, "Factura01.rpt");
                string RptPath = Path.Combine(BaseDir, request.infoTributaria.ruc, nombreRpt);
                string AssetsDir = Path.Combine(BaseDir, request.infoTributaria.ruc, "Assets");
                string TempDir = Path.Combine(BaseDir, request.infoTributaria.ruc, "Temp");

                var renderer = new CrystalRetencionRenderer(RptPath, AssetsDir, TempDir);
                byte[] pdf = renderer.RenderRetencion(request);

                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(pdf)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                resp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = "retencion.pdf"
                };

                return resp;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("render/liquidacioncompra")]
        public HttpResponseMessage LiquidacionCompra([FromBody] LiquidacionCompraRenderRequest request)
        {
            try
            {
                string nombreRpt = request.campoAdicional2;
                //string RptPath = Path.Combine(BaseDir, request.infoTributaria.ruc, "Factura01.rpt");
                string RptPath = Path.Combine(BaseDir, request.infoTributaria.ruc, nombreRpt);
                string AssetsDir = Path.Combine(BaseDir, request.infoTributaria.ruc, "Assets");
                string TempDir = Path.Combine(BaseDir, request.infoTributaria.ruc, "Temp");

                var renderer = new CrystalLiquidacionCompraRenderer(RptPath, AssetsDir, TempDir);
                byte[] pdf = renderer.RenderLiquidacionCompra(request);

                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(pdf)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                resp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = "liquidacioncompra.pdf"
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
