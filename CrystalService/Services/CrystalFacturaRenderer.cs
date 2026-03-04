using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace CrystalService.Services
{
    public class CrystalFacturaRenderer
    {
        private readonly string _rptPath;
        private readonly string _assetsDir;
        private readonly string _tempDir;

        public CrystalFacturaRenderer(string rptPath, string assetsDir, string tempDir)
        {
            _rptPath = rptPath;
            _assetsDir = assetsDir;
            _tempDir = tempDir;
        }

        public byte[] RenderFactura01(FacturaRenderRequest req)
        {
            if (!File.Exists(_rptPath))
                throw new FileNotFoundException("No existe el .rpt", _rptPath);

            // 1) Dataset
            DataSet ds = FacturaDataSetBuilder.Build(req);

            // 2) Imágenes
            string logoPath = !string.IsNullOrWhiteSpace(req?.LogoPathOverride)
                ? req.LogoPathOverride
                : Path.Combine(_assetsDir, "logo.png");

            // QR y barcode con clave de acceso
            string clave = req?.Cabecera?.ClaveAcceso ?? "";
            string qrPath = CodeImageService.GenerateQrPng(clave, _tempDir);
            string barPath = CodeImageService.GenerateCode128Png(clave, _tempDir);

            // 3) Crystal
            using (var report = new ReportDocument())
            {
                report.Load(_rptPath);

                // IMPORTANTE: el .rpt debe haber sido diseñado con tablas:
                // FacturaCab, FacturaDet, FacturaTotImp, FacturaPagos
                report.SetDataSource(ds);

                // Parámetros en el .rpt:
                // pLogoPath, pQRPath, pBarPath (puedes omitir pBarPath si no lo usas)
                TrySetParam(report, "pLogoPath", logoPath);
                TrySetParam(report, "pQRPath", qrPath);
                TrySetParam(report, "pBarPath", barPath);

                //Console.WriteLine($"Ruta Logo: {logoPath}");
                //Console.WriteLine($"Ruta QR: {qrPath}");
                //Console.WriteLine($"Ruta Cod Barras: {barPath}");

                using (var stream = report.ExportToStream(ExportFormatType.PortableDocFormat))
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        private static void TrySetParam(ReportDocument report, string paramName, object value)
        {
            try
            {
                report.SetParameterValue(paramName, value);
            }
            catch(Exception exprm)
            {
                // Si el parámetro no existe en el .rpt, no reventamos
                // (útil mientras estás iterando)
            }
        }
    }
}