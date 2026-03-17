using CrystalService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CrystalService.Services
{
    public static class LiquidacionCompraDataSetBuilder
    {
        public static DataSet FillLiquidacionCompraDataSet(DataSet ds, LiquidacionCompraRenderRequest req)
        {
            if (ds == null) throw new ArgumentNullException(nameof(ds));
            if (req == null) throw new ArgumentNullException(nameof(req));

            ClearIfExists(ds, "InfoTributaria");
            ClearIfExists(ds, "infoLiquidacionCompra");
            ClearIfExists(ds, "TotalConImpuestos");
            ClearIfExists(ds, "Pagos");
            ClearIfExists(ds, "Detalles");
            ClearIfExists(ds, "DetallesAdicionales");
            ClearIfExists(ds, "Impuestos");
            ClearIfExists(ds, "Reembolsos");
            ClearIfExists(ds, "DetalleImpuestosReembolso");
            ClearIfExists(ds, "InfoAdicional");
            ClearIfExists(ds, "CamposAdicionales");

            // 1) InfoTributaria
            if (req.infoTributaria != null)
            {
                var t = RequireTable(ds, "InfoTributaria");
                var r = t.NewRow();

                Set(r, "ambiente", req.infoTributaria.ambiente);
                Set(r, "tipoEmision", req.infoTributaria.tipoEmision);
                Set(r, "claveAcceso", req.infoTributaria.claveAcceso);
                Set(r, "razonSocial", req.infoTributaria.razonSocial);
                Set(r, "nombreComercial", req.infoTributaria.nombreComercial);
                Set(r, "ruc", req.infoTributaria.ruc);
                Set(r, "codDoc", req.infoTributaria.codDoc);
                Set(r, "estab", req.infoTributaria.estab);
                Set(r, "ptoEmi", req.infoTributaria.ptoEmi);
                Set(r, "secuencial", req.infoTributaria.secuencial);
                Set(r, "dirMatriz", req.infoTributaria.dirMatriz);
                Set(r, "diaEmission", req.infoTributaria.diaEmission);
                Set(r, "mesEmission", req.infoTributaria.mesEmission);
                Set(r, "anioEmission", req.infoTributaria.anioEmission);

                t.Rows.Add(r);
            }

            // 2) InfoLiquidacionCompra
            if (req.infoLiquidacionCompra != null)
            {
                var t = RequireTable(ds, "InfoLiquidacionCompra");
                var r = t.NewRow();

                Set(r, "fechaEmision", req.infoLiquidacionCompra.fechaEmision);
                Set(r, "dirEstablecimiento", req.infoLiquidacionCompra.dirEstablecimiento);
                Set(r, "contribuyenteEspecial", req.infoLiquidacionCompra.contribuyenteEspecial);
                Set(r, "obligadoContabilidad", req.infoLiquidacionCompra.obligadoContabilidad);
                Set(r, "tipoIdentificacionProveedor", req.infoLiquidacionCompra.tipoIdentificacionProveedor);
                Set(r, "razonSocialProveedor", req.infoLiquidacionCompra.razonSocialProveedor);
                Set(r, "identificacionProveedor", req.infoLiquidacionCompra.identificacionProveedor);
                Set(r, "direccionProveedor", req.infoLiquidacionCompra.direccionProveedor);
                Set(r, "totalSinImpuestos", req.infoLiquidacionCompra.totalSinImpuestos);
                Set(r, "totalDescuento", req.infoLiquidacionCompra.totalDescuento);
                Set(r, "codDocReembolso", req.infoLiquidacionCompra.codDocReembolso);
                Set(r, "totalComprobantesReembolso", req.infoLiquidacionCompra.totalComprobantesReembolso);
                Set(r, "totalBaseImponibleReembolso", req.infoLiquidacionCompra.totalBaseImponibleReembolso);
                Set(r, "totalImpuestoReembolso", req.infoLiquidacionCompra.totalImpuestoReembolso);
                Set(r, "importeTotal", req.infoLiquidacionCompra.importeTotal);
                Set(r, "moneda", req.infoLiquidacionCompra.moneda);

                t.Rows.Add(r);
            }

            // 3) TotalConImpuestos
            if (req.infoLiquidacionCompra != null && req.infoLiquidacionCompra.totalConImpuestos != null)
            {
                var t = RequireTable(ds, "TotalConImpuestos");

                foreach (var item in req.infoLiquidacionCompra.totalConImpuestos)
                {
                    if (item == null) continue;

                    var r = t.NewRow();
                    Set(r, "codigo", item.codigo);
                    Set(r, "codigoPorcentaje", item.codigoPorcentaje);
                    Set(r, "baseImponible", item.baseImponible);
                    Set(r, "valor", item.valor);
                    Set(r, "tarifa", item.tarifa);
                    Set(r, "descuentoAdicional", item.descuentoAdicional);
                    t.Rows.Add(r);
                }
            }

            // 4) Pagos
            if (req.infoLiquidacionCompra != null && req.infoLiquidacionCompra.pagos != null)
            {
                var t = RequireTable(ds, "Pagos");

                foreach (var pago in req.infoLiquidacionCompra.pagos)
                {
                    if (pago == null) continue;

                    var r = t.NewRow();
                    Set(r, "formaPago", pago.formaPago);
                    Set(r, "total", pago.total);
                    Set(r, "plazo", pago.plazo);
                    Set(r, "unidadTiempo", pago.unidadTiempo);
                    t.Rows.Add(r);
                }
            }

            // 5) Detalles + hijos
            int detalleId = 0;

            if (req.detalles != null)
            {
                var tDet = RequireTable(ds, "Detalles");
                var tDetAdd = RequireTable(ds, "DetallesAdicionales");
                var tImp = RequireTable(ds, "Impuestos");

                foreach (var det in req.detalles)
                {
                    if (det == null) continue;

                    detalleId++;

                    var rDet = tDet.NewRow();
                    Set(rDet, "detalleId", detalleId);
                    Set(rDet, "codigoPrincipal", det.codigoPrincipal);
                    Set(rDet, "codigoAuxiliar", det.codigoAuxiliar);
                    Set(rDet, "descripcion", det.descripcion);
                    Set(rDet, "cantidad", det.cantidad);
                    Set(rDet, "precioUnitario", det.precioUnitario);
                    Set(rDet, "descuento", det.descuento);
                    Set(rDet, "precioTotalSinImpuesto", det.precioTotalSinImpuesto);
                    Set(rDet, "unidadMedida", det.unidadMedida);

                    var detallesAdicionales = (det.detallesAdicionales ?? new List<DetalleAdicionalLq>())
                        .Where(x => x != null)
                        .Take(3)
                        .ToList();

                    SetDetalleAdicionalColumnas(rDet, 1, detallesAdicionales.ElementAtOrDefault(0));
                    SetDetalleAdicionalColumnas(rDet, 2, detallesAdicionales.ElementAtOrDefault(1));
                    SetDetalleAdicionalColumnas(rDet, 3, detallesAdicionales.ElementAtOrDefault(2));

                    tDet.Rows.Add(rDet);

                    if (detallesAdicionales.Any())
                    {
                        var rAdd = tDetAdd.NewRow();
                        Set(rAdd, "detalleId", detalleId);
                        Set(rAdd, "nombre", detallesAdicionales[0].nombre);
                        Set(rAdd, "valor", detallesAdicionales[0].valor);
                        tDetAdd.Rows.Add(rAdd);
                    }

                    if (det.impuestos != null)
                    {
                        foreach (var imp in det.impuestos)
                        {
                            if (imp == null) continue;

                            var rImp = tImp.NewRow();
                            Set(rImp, "detalleId", detalleId);
                            Set(rImp, "codigo", imp.codigo);
                            Set(rImp, "codigoPorcentaje", imp.codigoPorcentaje);
                            Set(rImp, "baseImponible", imp.baseImponible);
                            Set(rImp, "valor", imp.valor);
                            Set(rImp, "tarifa", imp.tarifa);
                            tImp.Rows.Add(rImp);
                        }
                    }
                }
            }

            // 6) Reembolsos + detalle impuestos reembolso
            int reembolsoId = 0;

            if (req.reembolsos != null)
            {
                var tReemb = RequireTable(ds, "Reembolsos");
                var tDetImpReemb = RequireTable(ds, "DetalleImpuestosReembolso");

                foreach (var reemb in req.reembolsos)
                {
                    if (reemb == null) continue;

                    reembolsoId++;

                    var rReemb = tReemb.NewRow();
                    Set(rReemb, "reembolsoId", reembolsoId);
                    Set(rReemb, "tipoIdentificacionProveedorReembolso", reemb.tipoIdentificacionProveedorReembolso);
                    Set(rReemb, "identificacionProveedorReembolso", reemb.identificacionProveedorReembolso);
                    Set(rReemb, "codPaisPagoProveedorReembolso", reemb.codPaisPagoProveedorReembolso);
                    Set(rReemb, "tipoProveedorReembolso", reemb.tipoProveedorReembolso);
                    Set(rReemb, "codDocReembolso", reemb.codDocReembolso);
                    Set(rReemb, "estabDocReembolso", reemb.estabDocReembolso);
                    Set(rReemb, "ptoEmiDocReembolso", reemb.ptoEmiDocReembolso);
                    Set(rReemb, "secuencialDocReembolso", reemb.secuencialDocReembolso);
                    Set(rReemb, "fechaEmisionDocReembolso", reemb.fechaEmisionDocReembolso);
                    Set(rReemb, "numeroautorizacionDocReemb", reemb.numeroautorizacionDocReemb);
                    tReemb.Rows.Add(rReemb);

                    if (reemb.detalleImpuestos != null)
                    {
                        foreach (var imp in reemb.detalleImpuestos)
                        {
                            if (imp == null) continue;

                            var rImp = tDetImpReemb.NewRow();
                            Set(rImp, "reembolsoId", reembolsoId);
                            Set(rImp, "codigo", imp.codigo);
                            Set(rImp, "codigoPorcentaje", imp.codigoPorcentaje);
                            Set(rImp, "tarifa", imp.tarifa);
                            Set(rImp, "baseImponibleReembolso", imp.baseImponibleReembolso);
                            Set(rImp, "impuestoReembolso", imp.impuestoReembolso);
                            tDetImpReemb.Rows.Add(rImp);
                        }
                    }
                }
            }

            // 7) InfoAdicional
            if (req.infoAdicional != null)
            {
                var t = RequireTable(ds, "InfoAdicional");

                foreach (var item in req.infoAdicional)
                {
                    if (item == null) continue;

                    var r = t.NewRow();
                    Set(r, "nombre", item.nombre);
                    Set(r, "valor", item.valor);
                    t.Rows.Add(r);
                }
            }

            // 8) CamposAdicionales
            if (ds.Tables.Contains("CamposAdicionales"))
            {
                var t = RequireTable(ds, "CamposAdicionales");
                var r = t.NewRow();

                Set(r, "campoAdicional1", req.campoAdicional1);
                Set(r, "campoAdicional2", req.campoAdicional2);

                t.Rows.Add(r);
            }

            ds.AcceptChanges();
            return ds;
        }

        private static void SetDetalleAdicionalColumnas(DataRow detalleRow, int indice, DetalleAdicionalLq adicional)
        {
            Set(detalleRow, $"adicional{indice}Nombre", adicional?.nombre);
            Set(detalleRow, $"adicional{indice}Valor", adicional?.valor);
        }

        private static void ClearIfExists(DataSet ds, string tableName)
        {
            if (ds.Tables.Contains(tableName))
                ds.Tables[tableName].Rows.Clear();
        }

        private static DataTable RequireTable(DataSet ds, string tableName)
        {
            if (!ds.Tables.Contains(tableName))
                throw new InvalidOperationException(
                    $"El DataSet no contiene la tabla requerida: '{tableName}'.");

            return ds.Tables[tableName];
        }

        private static void Set(DataRow row, string column, object value)
        {
            if (!row.Table.Columns.Contains(column))
                throw new InvalidOperationException(
                    $"La tabla '{row.Table.TableName}' no contiene la columna '{column}'.");

            row[column] = value ?? DBNull.Value;
        }
    }
}