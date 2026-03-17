using CrystalService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CrystalService.Services
{
    public static class NotaCreditoDataSetBuilder
    {
        /// <summary>
        /// Llena un DataSet (ya creado por ti) con la data del request.
        /// No crea el esquema: solo llena las tablas.
        ///
        /// Tablas esperadas (nombres exactos):
        /// - InfoTributaria
        /// - infoNotaCredito
        /// - TotalConImpuestos
        /// - Pagos
        /// - Detalles
        /// - DetallesAdicionales
        /// - DetallesImpuestos
        /// - Retenciones
        /// - InfoAdicional
        /// - Reembolsos
        /// - ReembolsoDetalleImpuestos
        /// - CamposAdicionales
        ///
        /// Relaciones recomendadas (si las tienes):
        /// - Detalles(detalleId) -> DetallesAdicionales(detalleId)
        /// - Detalles(detalleId) -> DetallesImpuestos(detalleId)
        /// - Reembolsos(reembolsoId) -> ReembolsoDetalleImpuestos(reembolsoId)
        /// </summary>
        public static DataSet FillNotaCreditoDataSet(DataSet ds, NotaCreditoRenderRequest req)
        {
            if (ds == null) throw new ArgumentNullException(nameof(ds));
            if (req == null) throw new ArgumentNullException(nameof(req));

            // Limpia tablas si quieres comportamiento "fresh" por request
            ClearIfExists(ds, "InfoTributaria");
            ClearIfExists(ds, "InfoNotaCredito");
            ClearIfExists(ds, "TotalConImpuestos");
            ClearIfExists(ds, "Detalles");
            ClearIfExists(ds, "DetallesAdicionales");
            ClearIfExists(ds, "DetallesImpuestos");
            ClearIfExists(ds, "InfoAdicional");
            ClearIfExists(ds, "CamposAdicionales");

            // 1) InfoTributaria (1 fila)
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
                Set(r, "fechaAuto", req.infoTributaria.fechaAuto);
                Set(r, "horaAuto", req.infoTributaria.horaAuto);

                t.Rows.Add(r);
            }

            // 2) infoNotaCredito (1 fila) + TotalConImpuestos + Pagos + Reembolsos
            if (req.infoNotaCredito != null)
            {
                // infoNotaCredito (1)
                var tInfo = RequireTable(ds, "InfoNotaCredito");
                var rInfo = tInfo.NewRow();

                Set(rInfo, "fechaEmision", req.infoNotaCredito.fechaEmision);
                Set(rInfo, "dirEstablecimiento", req.infoNotaCredito.dirEstablecimiento);
                Set(rInfo, "tipoIdentificacionComprador", req.infoNotaCredito.tipoIdentificacionComprador);
                Set(rInfo, "razonSocialComprador", req.infoNotaCredito.razonSocialComprador);
                Set(rInfo, "identificacionComprador", req.infoNotaCredito.identificacionComprador);
                Set(rInfo, "contribuyenteEspecial", req.infoNotaCredito.contribuyenteEspecial);
                Set(rInfo, "obligadoContabilidad", req.infoNotaCredito.obligadoContabilidad);
                Set(rInfo, "rise", req.infoNotaCredito.rise);
                Set(rInfo, "direccionComprador", req.infoNotaCredito.direccionComprador);
                Set(rInfo, "codDocModificado", req.infoNotaCredito.codDocModificado);
                Set(rInfo, "numDocModificado", req.infoNotaCredito.numDocModificado);
                Set(rInfo, "fechaEmisionDocSustento", req.infoNotaCredito.fechaEmisionDocSustento);
                Set(rInfo, "totalSinImpuestos", req.infoNotaCredito.totalSinImpuestos);
                Set(rInfo, "valorModificacion", req.infoNotaCredito.valorModificacion);
                Set(rInfo, "moneda", req.infoNotaCredito.moneda);
                Set(rInfo, "motivo", req.infoNotaCredito.motivo);
                tInfo.Rows.Add(rInfo);

                // TotalConImpuestos (N)
                if (req.infoNotaCredito.totalConImpuestos != null)
                {
                    var t = RequireTable(ds, "TotalConImpuestos");
                    foreach (var x in req.infoNotaCredito.totalConImpuestos)
                    {
                        if (x == null) continue;
                        var r = t.NewRow();
                        Set(r, "codigo", x.codigo);
                        Set(r, "codigoPorcentaje", x.codigoPorcentaje);
                        Set(r, "baseImponible", x.baseImponible);
                        Set(r, "valor", x.valor);
                        t.Rows.Add(r);
                    }
                }
            }

            // 3) Detalles (N) + hijos
            var detalleId = 0;
            if (req.detalles != null)
            {
                var tDet = RequireTable(ds, "Detalles");
                var tDetAdd = RequireTable(ds, "DetallesAdicionales");
                var tDetImp = RequireTable(ds, "DetallesImpuestos");

                foreach (var d in req.detalles)
                {
                    if (d == null) continue;
                    detalleId++;

                    var r = tDet.NewRow();
                    Set(r, "detalleId", detalleId);
                    Set(r, "codigoPrincipal", d.codigoPrincipal);
                    Set(r, "codigoAuxiliar", d.codigoAuxiliar);
                    Set(r, "descripcion", d.descripcion);
                    Set(r, "cantidad", d.cantidad); // decimal
                    Set(r, "precioUnitario", d.precioUnitario);
                    Set(r, "descuento", d.descuento);
                    Set(r, "precioTotalSinImpuesto", d.precioTotalSinImpuesto);

                    var detallesAdicionales = (d.detallesAdicionales ?? new List<DetalleAdicionalNc>())
                        .Where(x => x != null)
                        .Take(3)
                        .ToList();

                    SetDetalleAdicionalColumnas(r, 1, detallesAdicionales.ElementAtOrDefault(0));
                    SetDetalleAdicionalColumnas(r, 2, detallesAdicionales.ElementAtOrDefault(1));
                    SetDetalleAdicionalColumnas(r, 3, detallesAdicionales.ElementAtOrDefault(2));

                    tDet.Rows.Add(r);

                    if (detallesAdicionales.Any())
                    {
                        var ra = tDetAdd.NewRow();
                        Set(ra, "detalleId", detalleId);
                        Set(ra, "nombre", detallesAdicionales[0].nombre);
                        Set(ra, "valor", detallesAdicionales[0].valor);
                        tDetAdd.Rows.Add(ra);
                    }

                    if (d.impuestos != null)
                    {
                        foreach (var i in d.impuestos)
                        {
                            if (i == null) continue;
                            var ri = tDetImp.NewRow();
                            Set(ri, "detalleId", detalleId);
                            Set(ri, "codigo", i.codigo);
                            Set(ri, "codigoPorcentaje", i.codigoPorcentaje);
                            Set(ri, "baseImponible", i.baseImponible);
                            Set(ri, "valor", i.valor);
                            Set(ri, "tarifa", i.tarifa);
                            tDetImp.Rows.Add(ri);
                        }
                    }
                }
            }

            
            // 5) InfoAdicional
            if (req.infoAdicional != null)
            {
                var t = RequireTable(ds, "InfoAdicional");
                foreach (var x in req.infoAdicional)
                {
                    if (x == null) continue;
                    var r = t.NewRow();
                    Set(r, "nombre", x.nombre);
                    Set(r, "valor", x.valor);
                    t.Rows.Add(r);
                }
            }

            // 6) CamposAdicionales (1 fila)
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

        // ----------------- Helpers -----------------

        private static void SetDetalleAdicionalColumnas(DataRow detalleRow, int indice, DetalleAdicionalNc adicional)
        {
            Set(detalleRow, $"adicional{indice}Nombre", adicional?.nombre);
            Set(detalleRow, $"adicional{indice}Valor", adicional?.valor);
        }

        private static void ClearIfExists(DataSet ds, string tableName)
        {
            var t = ds.Tables.Contains(tableName) ? ds.Tables[tableName] : null;
            t?.Rows.Clear();
        }

        private static DataTable RequireTable(DataSet ds, string tableName)
        {
            if (!ds.Tables.Contains(tableName))
                throw new InvalidOperationException($"El DataSet no contiene la tabla requerida: '{tableName}'.");
            return ds.Tables[tableName];
        }

        private static void Set(DataRow row, string column, object value)
        {
            if (!row.Table.Columns.Contains(column))
                throw new InvalidOperationException($"La tabla '{row.Table.TableName}' no contiene la columna: '{column}'.");

            row[column] = value ?? DBNull.Value;
        }
    }
}