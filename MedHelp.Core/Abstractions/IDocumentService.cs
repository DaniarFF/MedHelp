using iText.Kernel.Pdf;
using MedHelp.Core.Models;

namespace MedHelp.Core.Abstractions;

public interface IDocumentService
{
  /// <summary>
  ///  Создать PDF-файл медицинского документа.
  /// </summary>
  /// <param name="medDoc"></param>
  void CreatePDFFile(MedicalDocument medDoc);

  /// <summary>
  /// Заполнить медицинский документ.
  /// </summary>
  /// <param name="pdfDoc"></param>
  /// <param name="doc"></param>
  /// <param name="pDFDocument"></param>
  void FillDocument(PdfDocument pdfDoc, iText.Layout.Document doc, MedicalDocument pDFDocument);
}