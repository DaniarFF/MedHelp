using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MedHelp.Core.Abstractions;
using MedHelp.Core.Models;

namespace MedHelp.Core.Services
{
  /// <summary>
  /// Сервис для формирования и создания PDF документа.
  /// </summary>
  public class DocumentService : IDocumentService
  {
    public static readonly string dest = "recipe.pdf";

    /// <summary>
    /// Создание PDF файла.
    /// </summary>
    /// <param name="medDoc"></param>
    public void CreatePDFFile(MedicalDocument medDoc)
    {
      FileInfo file = new FileInfo(dest);
      file.Directory.Create();

      PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
      var size = new PageSize(874, 1240);
      var doc = new iText.Layout.Document(pdfDoc, size);

      FillDocument(pdfDoc, doc, medDoc);

      pdfDoc.Close();
    }

    /// <summary>
    /// Заполнение и формирование PDF файла.
    /// </summary>
    public void FillDocument(PdfDocument pdfDoc, iText.Layout.Document doc, MedicalDocument pDFDocument) 
    {
      //Шрифт необходим для правильного отображения кириллицы.

      var font = PdfFontFactory.CreateFont(
      System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Services\\FreeSans.ttf"),
      PdfEncodings.IDENTITY_H);

      pdfDoc.AddFont(font);

      var dateTime = DateTime.Now;

      var recipeStyle = new Style()
       .SetFont(font)
       .SetFontSize(15)
       .SetPaddingLeft(15);

      doc.Add(new Paragraph($"Рецепт")
        .SetFont(font)
        .SetFontSize(32)
        .SetTextAlignment(TextAlignment.CENTER)
        .SetMultipliedLeading(0.5f));

      doc.Add(new Paragraph($"Дата {dateTime.ToString("D")}")
        .SetPaddingBottom(100)
        .SetFont(font)
        .SetFontSize(20)
        .SetTextAlignment(TextAlignment.CENTER));

      doc.Add(new Paragraph()
        .AddTabStops(new TabStop(250, TabAlignment.LEFT))
        .SetFixedLeading(20)
        .Add("Имя пациента")
        .Add(new Tab())
        .Add(pDFDocument.PatientName)
        .SetFont(font)
        .SetFontSize(15));

      doc.Add(new Paragraph()
         .AddTabStops(new TabStop(250, TabAlignment.LEFT))
         .SetFixedLeading(20)
         .Add("Возраст")
         .Add(new Tab())
         .Add(pDFDocument.Age)
         .SetFont(font)
         .SetFontSize(15));

      if(pDFDocument.DrugList != null && pDFDocument.DrugList.Any()) 
      {
        int drugNum = 1;

        foreach(var drug in pDFDocument.DrugList) 
        {
          doc.Add(new Paragraph($"{drugNum}. {drug.Recipe}\n")
           .AddStyle(recipeStyle)
           .SetFixedLeading(20));

          drugNum++;
        }
      }
     
      Image image = new Image(ImageDataFactory.Create("pechat.png"))
            .SetHeight(80)
            .SetWidth(80)
            .SetMarginTop(20)   
            .SetMarginLeft(0)   
            .SetMarginRight(25);

      doc.Add(new Paragraph()
        .AddTabStops(new TabStop(250, TabAlignment.LEFT))
        .SetFixedLeading(20)
        .Add("Врач")
        .Add(new Tab())
        .Add(new Text(pDFDocument.DoctorName)
        .SetBold())
        .SetFont(font)
        .SetFontSize(15)
        .SetPaddingTop(100));
    }
  }
}
