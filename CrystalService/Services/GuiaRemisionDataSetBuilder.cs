using CrystalService.Models;
using System;
using System.Data;

namespace CrystalService.Services
{
    public static class GuiaRemisionDataSetBuilder
    {
        public static DataSet FillGuiaRemisionDataSet(DataSet ds, GuiaRemisionRenderRequest req)
        {
            if (ds == null) throw new ArgumentNullException(nameof(ds));
            if (req == null) throw new ArgumentNullException(nameof(req));

            // Limpieza de tablas realmente usadas por la guía
            ClearIfExists(ds, "InfoTributaria");
            ClearIfExists(ds, "InfoGuiaRemision");
            ClearIfExists(ds, "Destinatarios");
            ClearIfExists(ds, "Detalles");
            ClearIfExists(ds, "DetallesAdicionales");
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
                Set(r, "fechaAuto", req.infoTributaria.fechaAuto);
                Set(r, "horaAuto", req.infoTributaria.horaAuto);

                t.Rows.Add(r);
            }

            // 2) InfoGuiaRemision
            if (req.infoGuiaRemision != null)
            {
                var t = RequireTable(ds, "InfoGuiaRemision");
                var r = t.NewRow();

                Set(r, "dirEstablecimiento", req.infoGuiaRemision.dirEstablecimiento);
                Set(r, "dirPartida", req.infoGuiaRemision.dirPartida);
                Set(r, "razonSocialTransportista", req.infoGuiaRemision.razonSocialTransportista);
                Set(r, "tipoIdentificacionTransportista", req.infoGuiaRemision.tipoIdentificacionTransportista);
                Set(r, "rucTransportista", req.infoGuiaRemision.rucTransportista);
                Set(r, "rise", req.infoGuiaRemision.rise);
                Set(r, "contribuyenteEspecial", req.infoGuiaRemision.contribuyenteEspecial);
                Set(r, "obligadoContabilidad", req.infoGuiaRemision.obligadoContabilidad);
                Set(r, "fechaIniTransporte", req.infoGuiaRemision.fechaIniTransporte);
                Set(r, "fechaFinTransporte", req.infoGuiaRemision.fechaFinTransporte);
                Set(r, "placa", req.infoGuiaRemision.placa);

                t.Rows.Add(r);
            }

            // 3) Destinatarios + Detalles + DetallesAdicionales
            int destinatarioId = 0;
            int detalleId = 0;

            if (req.destinatarios != null)
            {
                var tDest = RequireTable(ds, "Destinatarios");
                var tDet = RequireTable(ds, "Detalles");
                var tDetAdd = RequireTable(ds, "DetallesAdicionales");

                foreach (var dest in req.destinatarios)
                {
                    if (dest == null) continue;

                    destinatarioId++;

                    var rDest = tDest.NewRow();
                    Set(rDest, "destinatarioId", destinatarioId);
                    Set(rDest, "identificacionDestinatario", dest.identificacionDestinatario);
                    Set(rDest, "razonSocialDestinatario", dest.razonSocialDestinatario);
                    Set(rDest, "dirDestinatario", dest.dirDestinatario);
                    Set(rDest, "motivoTraslado", dest.motivoTraslado);
                    Set(rDest, "codEstabDestino", dest.codEstabDestino);
                    Set(rDest, "codDocSustento", dest.codDocSustento);
                    Set(rDest, "numDocSustento", dest.numDocSustento);
                    Set(rDest, "numAutDocSustento", dest.numAutDocSustento);
                    Set(rDest, "fechaEmisionDocSustento", dest.fechaEmisionDocSustento);
                    Set(rDest, "docAduaneroUnico", dest.docAduaneroUnico);
                    Set(rDest, "ruta", dest.ruta);
                    tDest.Rows.Add(rDest);

                    if (dest.detalles != null)
                    {
                        foreach (var det in dest.detalles)
                        {
                            if (det == null) continue;

                            detalleId++;

                            var rDet = tDet.NewRow();
                            Set(rDet, "detalleId", detalleId);
                            Set(rDet, "destinatarioId", destinatarioId);
                            Set(rDet, "codigoPrincipal", det.codigoPrincipal);
                            Set(rDet, "codigoAuxiliar", det.codigoAuxiliar);
                            Set(rDet, "descripcion", det.descripcion);
                            Set(rDet, "cantidad", det.cantidad);
                            tDet.Rows.Add(rDet);

                            if (det.detallesAdicionales != null)
                            {
                                foreach (var add in det.detallesAdicionales)
                                {
                                    if (add == null) continue;

                                    var rAdd = tDetAdd.NewRow();
                                    Set(rAdd, "detalleId", detalleId);
                                    Set(rAdd, "nombre", add.nombre);
                                    Set(rAdd, "valor", add.valor);
                                    tDetAdd.Rows.Add(rAdd);
                                }
                            }
                        }
                    }
                }
            }

            // 4) InfoAdicional
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

            // 5) CamposAdicionales
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

        private static void ClearIfExists(DataSet ds, string tableName)
        {
            if (ds.Tables.Contains(tableName))
                ds.Tables[tableName].Rows.Clear();
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
                throw new InvalidOperationException(
                    $"La tabla '{row.Table.TableName}' no contiene la columna: '{column}'.");

            row[column] = value ?? DBNull.Value;
        }
    }
}