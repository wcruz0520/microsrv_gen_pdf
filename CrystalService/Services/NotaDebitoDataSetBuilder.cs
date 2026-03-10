using CrystalService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CrystalService.Services
{
    public static class NotaDebitoDataSetBuilder
    {
        /// <summary>
        /// Llena un DataSet (ya creado por ti) con la data del request.
        /// No crea el esquema: solo llena las tablas.
        ///
        /// Tablas esperadas (nombres exactos):
        /// - InfoTributaria
        /// - infoNotaDebito
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
        public static DataSet FillNotaDebitoDataSet(DataSet ds, NotaDebitoRenderRequest req)
        {
            if (ds == null) throw new ArgumentNullException(nameof(ds));
            if (req == null) throw new ArgumentNullException(nameof(req));

            // Limpia tablas si quieres comportamiento "fresh" por request
            ClearIfExists(ds, "InfoTributaria");
            ClearIfExists(ds, "InfoNotaDebito");
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

            // 2) infoNotaDebito (1 fila) + TotalConImpuestos + Pagos + Reembolsos
            if (req.infoNotaDebito != null)
            {
                // infoNotaDebito (1)
                var tInfo = RequireTable(ds, "InfoNotaDebito");
                var rInfo = tInfo.NewRow();

                Set(rInfo, "fechaEmision", req.infoNotaDebito.fechaEmision);
                Set(rInfo, "dirEstablecimiento", req.infoNotaDebito.dirEstablecimiento);
                Set(rInfo, "tipoIdentificacionComprador", req.infoNotaDebito.tipoIdentificacionComprador);
                Set(rInfo, "razonSocialComprador", req.infoNotaDebito.razonSocialComprador);
                Set(rInfo, "identificacionComprador", req.infoNotaDebito.identificacionComprador);
                Set(rInfo, "contribuyenteEspecial", req.infoNotaDebito.contribuyenteEspecial);
                Set(rInfo, "obligadoContabilidad", req.infoNotaDebito.obligadoContabilidad);
                Set(rInfo, "direccionComprador", req.infoNotaDebito.direccionComprador);
                Set(rInfo, "codDocModificado", req.infoNotaDebito.codDocModificado);
                Set(rInfo, "numDocModificado", req.infoNotaDebito.numDocModificado);
                Set(rInfo, "fechaEmisionDocSustento", req.infoNotaDebito.fechaEmisionDocSustento);
                Set(rInfo, "totalSinImpuestos", req.infoNotaDebito.totalSinImpuestos);
                Set(rInfo, "valorTotal", req.infoNotaDebito.valorTotal);
                tInfo.Rows.Add(rInfo);

                // TotalConImpuestos (N)
                if (req.infoNotaDebito.totalConImpuestos != null)
                {
                    var t = RequireTable(ds, "TotalConImpuestos");
                    foreach (var x in req.infoNotaDebito.totalConImpuestos)
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

                if (req.infoNotaDebito.pagos != null)
                {
                    var t = RequireTable(ds, "Pagos");
                    foreach (var p in req.infoNotaDebito.pagos)
                    {
                        if (p == null) continue;
                        var r = t.NewRow();
                        Set(r, "formaPago", p.formaPago);
                        Set(r, "total", p.total);
                        Set(r, "plazo", p.plazo);
                        Set(r, "unidadTiempo", p.unidadTiempo);
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
                    tDet.Rows.Add(r);

                    if (d.detallesAdicionales != null)
                    {
                        foreach (var a in d.detallesAdicionales)
                        {
                            if (a == null) continue;
                            var ra = tDetAdd.NewRow();
                            Set(ra, "detalleId", detalleId);
                            Set(ra, "nombre", a.nombre);
                            Set(ra, "valor", a.valor);
                            tDetAdd.Rows.Add(ra);
                        }
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