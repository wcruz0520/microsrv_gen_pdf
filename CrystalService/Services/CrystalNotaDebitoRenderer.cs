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
    public class CrystalNotaDebitoRenderer
    {
        private readonly string _rptPath;
        private readonly string _assetsDir;
        private readonly string _tempDir;

        public CrystalNotaDebitoRenderer(string rptPath, string assetsDir, string tempDir)
        {
            _rptPath = rptPath;
            _assetsDir = assetsDir;
            _tempDir = tempDir;
        }

        public byte[] RenderNotaDebito(NotaDebitoRenderRequest req)
        {
            if (!File.Exists(_rptPath))
                throw new FileNotFoundException("No existe el .rpt", _rptPath);

            DataSet dsNc = new DataSet();
            dsNc.ReadXmlSchema(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fomatos", "NotaDebito.xsd"));

            // 1) Dataset
            DataSet ds = NotaDebitoDataSetBuilder.FillNotaDebitoDataSet(dsNc, req);

            // 2) Imágenes
            string logoPath = !string.IsNullOrWhiteSpace(req?.LogoPathOverride)
                ? req.LogoPathOverride
                : Path.Combine(_assetsDir, req.campoAdicional1);

            // QR y barcode con clave de acceso
            string clave = req?.infoTributaria?.claveAcceso ?? "";
            string qrPath = CodeImageService.GenerateQrPng(clave, _tempDir);
            string barPath = CodeImageService.GenerateCode128Png(clave, _tempDir);

            //if (!File.Exists(logoPath))
            //    throw new FileNotFoundException("No existe el logo", logoPath);

            //if (!File.Exists(qrPath))
            //    throw new FileNotFoundException("No existe el qr", qrPath);

            if (!File.Exists(barPath))
                throw new FileNotFoundException("No existe el codigo de barras", barPath);

            // 3) Crystal
            using (var report = new ReportDocument())
            {
                //Carga RPT
                report.Load(_rptPath);

                //Asigna dataset
                report.SetDataSource(ds);

                //asigna dataset para subreporte de infoAdicional
                ReportDocument subinfoAdicional = report.Subreports["infoAdicional"];
                subinfoAdicional.SetDataSource(ds.Tables["InfoAdicional"]);

                //asigna dataset para subreporte de totalConImpuesto
                ReportDocument subtotalConImpuesto = report.Subreports["totalConImpuesto"];
                subinfoAdicional.SetDataSource(ds.Tables["totalConImpuesto"]);

                ////asigna dataset para subreporte de pagos
                //ReportDocument subpagos = report.Subreports["pagos"];
                //subpagos.SetDataSource(ds.Tables["Pagos"]);

                ////asigna dataset para subreporte de reembolso
                //ReportDocument subreembolso = report.Subreports["reembolso"];
                //subpagos.SetDataSource(ds);

                // Parámetros en el .rpt:
                TrySetParam(report, "pLogoPath", logoPath);
                TrySetParam(report, "pQRPath", qrPath);
                TrySetParam(report, "pBarPath", barPath);

                using (var stream = report.ExportToStream(ExportFormatType.PortableDocFormat))
                using (var ms = new MemoryStream())
                {
                    if (File.Exists(qrPath))
                    {
                        File.Delete(qrPath);
                    }

                    if (File.Exists(barPath))
                    {
                        File.Delete(barPath);
                    }

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