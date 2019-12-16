using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class PDF
    {
        #region Declaration
        Document _document;
        Font _fontStyle;
        PdfPTable _pdfTable = new PdfPTable(4);
        MemoryStream _memoryStream = new MemoryStream();
        PaqueteDTO _paq;
        Cliente Remitente, Destinatario;
        string _estado, _origen, _destino;
        iTextSharp.text.Image _imgQR;
        #endregion

        public byte[] PrepararReport(PaqueteDTO paq, iTextSharp.text.Image imgQRCode)
        {
            _paq = paq;
            Remitente = paq.Remitente;
            Destinatario = paq.Destinatario;
            if(_paq.Trayecto.ListaPuntosControl.Count == _paq.PaquetePuntoControl.Count)
            {
                _estado = "Finalizado";
            }
            else
            {
                _estado = "En viaje";
            }
            _imgQR = imgQRCode;

            #region
            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(20f, 20f, 20f, 20f);
            _document.AddTitle("Envio de paquete");
            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();
            _pdfTable.SetWidths(new float[] { 50f, 50f, 50f, 50f });
            #endregion

            this.CrearContenido();
            _pdfTable.HeaderRows = 2;
            _document.Add(_pdfTable);
            _document.Close();
            return _memoryStream.ToArray();
        }

        private void CrearContenido()
        {
            _document.Add(new Paragraph("Paquete"));
            _document.Add(Chunk.NEWLINE);
            _document.Add(new Paragraph("Datos:"));
            _document.Add(new Paragraph("    - Estado: " + _estado));
            _document.Add(Chunk.NEWLINE);
            _document.Add(new Paragraph("Remitente:"));
            _document.Add(new Paragraph("    - Email: " + Remitente.Email));
            _document.Add(new Paragraph("    - Nombre: " + Remitente.NombreCompleto));
            _document.Add(new Paragraph("    - Telefono: " + Remitente.Telefono));
            _document.Add(new Paragraph("    - Tipo de Documento: " + Remitente.TipoDocumento));
            _document.Add(new Paragraph("    - Nro de Documento: " + Remitente.NumeroDocumento));
            _document.Add(Chunk.NEWLINE);
            _document.Add(new Paragraph("Destinatario:"));
            _document.Add(new Paragraph("    - Email: " + Destinatario.Email));
            _document.Add(new Paragraph("    - Nombre: " + Destinatario.NombreCompleto));
            _document.Add(new Paragraph("    - Telefono: " + Destinatario.Telefono));
            _document.Add(Chunk.NEWLINE);
            _document.Add(_imgQR);
        }
    }
}