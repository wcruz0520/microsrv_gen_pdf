using CrystalService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CrystalService.Services
{
    public static class FacturaDataSetBuilder
    {
        public static DataSet Build(FacturaRenderRequest req)
        {
            var ds = new DataSet("Factura01DS");

            // Cabecera (1 fila)
            var cab = new DataTable("FacturaCab");
            cab.Columns.Add("ClaveAcceso", typeof(string));
            cab.Columns.Add("Ruc", typeof(string));
            cab.Columns.Add("RazonSocial", typeof(string));
            cab.Columns.Add("NombreComercial", typeof(string));
            cab.Columns.Add("Estab", typeof(string));
            cab.Columns.Add("PtoEmi", typeof(string));
            cab.Columns.Add("Secuencial", typeof(string));
            cab.Columns.Add("FechaEmision", typeof(string));
            cab.Columns.Add("DirMatriz", typeof(string));
            cab.Columns.Add("DirEstablecimiento", typeof(string));
            cab.Columns.Add("IdentificacionComprador", typeof(string));
            cab.Columns.Add("RazonSocialComprador", typeof(string));
            cab.Columns.Add("TotalSinImpuestos", typeof(decimal));
            cab.Columns.Add("TotalDescuento", typeof(decimal));
            cab.Columns.Add("ImporteTotal", typeof(decimal));
            cab.Columns.Add("Moneda", typeof(string));
            ds.Tables.Add(cab);

            if (req?.Cabecera != null)
            {
                var r = cab.NewRow();
                r["ClaveAcceso"] = req.Cabecera.ClaveAcceso ?? "";
                r["Ruc"] = req.Cabecera.Ruc ?? "";
                r["RazonSocial"] = req.Cabecera.RazonSocial ?? "";
                r["NombreComercial"] = req.Cabecera.NombreComercial ?? "";
                r["Estab"] = req.Cabecera.Estab ?? "";
                r["PtoEmi"] = req.Cabecera.PtoEmi ?? "";
                r["Secuencial"] = req.Cabecera.Secuencial ?? "";
                r["FechaEmision"] = req.Cabecera.FechaEmision ?? "";
                r["DirMatriz"] = req.Cabecera.DirMatriz ?? "";
                r["DirEstablecimiento"] = req.Cabecera.DirEstablecimiento ?? "";
                r["IdentificacionComprador"] = req.Cabecera.IdentificacionComprador ?? "";
                r["RazonSocialComprador"] = req.Cabecera.RazonSocialComprador ?? "";
                r["TotalSinImpuestos"] = req.Cabecera.TotalSinImpuestos;
                r["TotalDescuento"] = req.Cabecera.TotalDescuento;
                r["ImporteTotal"] = req.Cabecera.ImporteTotal;
                r["Moneda"] = req.Cabecera.Moneda ?? "";
                cab.Rows.Add(r);
            }

            // Detalles
            var det = new DataTable("FacturaDet");
            det.Columns.Add("LineNum", typeof(int));
            det.Columns.Add("CodigoPrincipal", typeof(string));
            det.Columns.Add("Descripcion", typeof(string));
            det.Columns.Add("Cantidad", typeof(decimal));
            det.Columns.Add("PrecioUnitario", typeof(decimal));
            det.Columns.Add("Descuento", typeof(decimal));
            det.Columns.Add("PrecioTotalSinImpuesto", typeof(decimal));
            ds.Tables.Add(det);

            if (req?.Detalles != null)
            {
                foreach (var d in req.Detalles)
                {
                    var dr = det.NewRow();
                    dr["LineNum"] = d.LineNum;
                    dr["CodigoPrincipal"] = d.CodigoPrincipal ?? "";
                    dr["Descripcion"] = d.Descripcion ?? "";
                    dr["Cantidad"] = d.Cantidad;
                    dr["PrecioUnitario"] = d.PrecioUnitario;
                    dr["Descuento"] = d.Descuento;
                    dr["PrecioTotalSinImpuesto"] = d.PrecioTotalSinImpuesto;
                    det.Rows.Add(dr);
                }
            }

            // Totales Impuestos
            var tim = new DataTable("FacturaTotImp");
            tim.Columns.Add("Codigo", typeof(string));
            tim.Columns.Add("CodigoPorcentaje", typeof(string));
            tim.Columns.Add("BaseImponible", typeof(decimal));
            tim.Columns.Add("Valor", typeof(decimal));
            ds.Tables.Add(tim);

            if (req?.TotalesImpuestos != null)
            {
                foreach (var t in req.TotalesImpuestos)
                {
                    var tr = tim.NewRow();
                    tr["Codigo"] = t.Codigo ?? "";
                    tr["CodigoPorcentaje"] = t.CodigoPorcentaje ?? "";
                    tr["BaseImponible"] = t.BaseImponible;
                    tr["Valor"] = t.Valor;
                    tim.Rows.Add(tr);
                }
            }

            // Pagos
            var pagos = new DataTable("FacturaPagos");
            pagos.Columns.Add("FormaPago", typeof(string));
            pagos.Columns.Add("Total", typeof(decimal));
            pagos.Columns.Add("Plazo", typeof(int));
            pagos.Columns.Add("UnidadTiempo", typeof(string));
            ds.Tables.Add(pagos);

            if (req?.Pagos != null)
            {
                foreach (var p in req.Pagos)
                {
                    var pr = pagos.NewRow();
                    pr["FormaPago"] = p.FormaPago ?? "";
                    pr["Total"] = p.Total;
                    pr["Plazo"] = p.Plazo;
                    pr["UnidadTiempo"] = p.UnidadTiempo ?? "";
                    pagos.Rows.Add(pr);
                }
            }

            return ds;
        }
    }
}