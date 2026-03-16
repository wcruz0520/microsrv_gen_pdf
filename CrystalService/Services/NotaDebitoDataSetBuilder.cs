using CrystalService.Models;
using System;
using System.Data;

namespace CrystalService.Services
{
    public static class NotaDebitoDataSetBuilder
    {
        /// <summary>
        /// Llena un DataSet existente con la data del request de Nota de Débito.
        ///
        /// Tablas esperadas:
        /// - InfoTributaria
        /// - InfoNotaDebito
        /// - Impuestos
        /// - Pagos
        /// - Motivos
        /// - InfoAdicional
        /// - CamposAdicionales
        ///
        /// Relaciones esperadas:
        /// - InfoNotaDebito(notadebitorowId) -> Impuestos(notadebitorowId)
        /// - InfoNotaDebito(notadebitorowId) -> Pagos(notadebitorowId)
        /// </summary>
        public static DataSet FillNotaDebitoDataSet(DataSet ds, NotaDebitoRenderRequest req)
        {
            if (ds == null) throw new ArgumentNullException(nameof(ds));
            if (req == null) throw new ArgumentNullException(nameof(req));

            ClearIfExists(ds, "InfoTributaria");
            ClearIfExists(ds, "InfoNotaDebito");
            ClearIfExists(ds, "Impuestos");
            ClearIfExists(ds, "Pagos");
            ClearIfExists(ds, "Motivos");
            ClearIfExists(ds, "InfoAdicional");
            ClearIfExists(ds, "CamposAdicionales");

            // 1) InfoTributaria
            if (req.infoTributaria != null)
            {
                DataTable t = RequireTable(ds, "InfoTributaria");
                DataRow r = t.NewRow();

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

                if (t.Columns.Contains("fechaAuto"))
                    Set(r, "fechaAuto", req.infoTributaria.fechaAuto);

                if (t.Columns.Contains("horaAuto"))
                    Set(r, "horaAuto", req.infoTributaria.horaAuto);

                t.Rows.Add(r);
            }

            // 2) InfoNotaDebito + hijos
            if (req.infoNotaDebito != null)
            {
                const int notaDebitoRowId = 1;

                DataTable tInfo = RequireTable(ds, "InfoNotaDebito");
                DataRow rInfo = tInfo.NewRow();

                Set(rInfo, "notadebitorowId", notaDebitoRowId);
                Set(rInfo, "fechaEmision", req.infoNotaDebito.fechaEmision);
                Set(rInfo, "dirEstablecimiento", req.infoNotaDebito.dirEstablecimiento);
                Set(rInfo, "tipoIdentificacionComprador", req.infoNotaDebito.tipoIdentificacionComprador);
                Set(rInfo, "razonSocialComprador", req.infoNotaDebito.razonSocialComprador);
                Set(rInfo, "identificacionComprador", req.infoNotaDebito.identificacionComprador);
                Set(rInfo, "contribuyenteEspecial", req.infoNotaDebito.contribuyenteEspecial);
                Set(rInfo, "obligadoContabilidad", req.infoNotaDebito.obligadoContabilidad);
                Set(rInfo, "codDocModificado", req.infoNotaDebito.codDocModificado);
                Set(rInfo, "numDocModificado", req.infoNotaDebito.numDocModificado);
                Set(rInfo, "fechaEmisionDocSustento", req.infoNotaDebito.fechaEmisionDocSustento);
                Set(rInfo, "totalSinImpuestos", req.infoNotaDebito.totalSinImpuestos);
                Set(rInfo, "valorTotal", req.infoNotaDebito.valorTotal);

                if (tInfo.Columns.Contains("direccionComprador"))
                    Set(rInfo, "direccionComprador", req.infoNotaDebito.direccionComprador);

                tInfo.Rows.Add(rInfo);

                // 3) Impuestos
                if (req.infoNotaDebito.impuestos != null)
                {
                    DataTable tImp = RequireTable(ds, "Impuestos");

                    foreach (ImpuestoNotaDebitoNd imp in req.infoNotaDebito.impuestos)
                    {
                        if (imp == null) continue;

                        DataRow rImp = tImp.NewRow();
                        Set(rImp, "notadebitorowId", notaDebitoRowId);
                        Set(rImp, "codigo", imp.codigo);
                        Set(rImp, "codigoPorcentaje", imp.codigoPorcentaje);
                        Set(rImp, "baseImponible", imp.baseImponible);
                        Set(rImp, "valor", imp.valor);

                        if (tImp.Columns.Contains("tarifa"))
                            Set(rImp, "tarifa", imp.tarifa);

                        tImp.Rows.Add(rImp);
                    }
                }

                // 4) Pagos
                if (req.infoNotaDebito.pagos != null)
                {
                    DataTable tPag = RequireTable(ds, "Pagos");

                    foreach (PagoNotaDebitoNd pag in req.infoNotaDebito.pagos)
                    {
                        if (pag == null) continue;

                        DataRow rPag = tPag.NewRow();
                        Set(rPag, "notadebitorowId", notaDebitoRowId);
                        Set(rPag, "formaPago", pag.formaPago);
                        Set(rPag, "total", pag.total);
                        Set(rPag, "plazo", pag.plazo);
                        Set(rPag, "unidadTiempo", pag.unidadTiempo);
                        tPag.Rows.Add(rPag);
                    }
                }
            }

            // 5) Motivos
            if (req.motivos != null)
            {
                DataTable tMot = RequireTable(ds, "Motivos");

                foreach (MotivoNd mot in req.motivos)
                {
                    if (mot == null) continue;

                    DataRow rMot = tMot.NewRow();
                    Set(rMot, "razon", mot.razon);
                    Set(rMot, "valor", mot.valor);
                    tMot.Rows.Add(rMot);
                }
            }

            // 6) InfoAdicional
            if (req.infoAdicional != null)
            {
                DataTable tAd = RequireTable(ds, "InfoAdicional");

                foreach (InfoAdicionalItemNd item in req.infoAdicional)
                {
                    if (item == null) continue;

                    DataRow rAd = tAd.NewRow();
                    Set(rAd, "nombre", item.nombre);
                    Set(rAd, "valor", item.valor);
                    tAd.Rows.Add(rAd);
                }
            }

            // 7) CamposAdicionales
            {
                DataTable tCampos = RequireTable(ds, "CamposAdicionales");
                DataRow rCampos = tCampos.NewRow();

                Set(rCampos, "campoAdicional1", req.campoAdicional1);
                Set(rCampos, "campoAdicional2", req.campoAdicional2);

                tCampos.Rows.Add(rCampos);
            }

            ds.AcceptChanges();
            return ds;
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
                    "El DataSet no contiene la tabla requerida: '" + tableName + "'.");

            return ds.Tables[tableName];
        }

        private static void Set(DataRow row, string column, object value)
        {
            if (!row.Table.Columns.Contains(column))
                throw new InvalidOperationException(
                    "La tabla '" + row.Table.TableName + "' no contiene la columna: '" + column + "'.");

            row[column] = value ?? DBNull.Value;
        }
    }
}