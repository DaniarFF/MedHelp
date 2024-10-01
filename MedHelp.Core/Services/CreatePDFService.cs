using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MedHelp.Core.Models;

namespace MedHelp.Core.Services
{
  /// <summary>
  /// Сервис для формирования и создания PDF документа.
  /// </summary>
  public class CreatePDFService
  {
    public static readonly string dest = "example.pdf";

    /// <summary>
    /// Создание PDF файла.
    /// </summary>
    /// <param name="medDoc"></param>
    public void CreatePDF(MedicalDocument medDoc)
    {
      FileInfo file = new FileInfo(dest);
      file.Directory.Create();

      PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
      var size = new PageSize(874, 1240);
      var doc = new iText.Layout.Document(pdfDoc, size);

      ManipulatePDF(pdfDoc, doc, medDoc);

      pdfDoc.Close();
    }

    /// <summary>
    /// Заполнение и формирование PDF файла.
    /// </summary>
    private void ManipulatePDF(PdfDocument pdfDoc, iText.Layout.Document doc, MedicalDocument pDFDocument) 
    {
      //Шрифт необходим для правильного отображения кириллицы.
      var font = PdfFontFactory.CreateFont(
      System.IO.Path.Combine("C:\\Users\\Данияр\\source\\repos\\MedHelpBot\\MedHelp.Core\\Services\\", "FreeSans.ttf"),
      PdfEncodings.IDENTITY_H);

      pdfDoc.AddFont(font);

      DateTime dateTime = DateTime.Now;

      var recipeStyle = new Style()
       .SetFont(font)
       .SetFontSize(15)
       .SetPaddingLeft(15);

      doc.Add(new Paragraph($"Рецепт")
        .SetFont(font).SetFontSize(32)
        .SetTextAlignment(TextAlignment.CENTER)
        .SetMultipliedLeading(0.5f));

      doc.Add(new Paragraph($"Дата {dateTime.ToString("D")}")
        .SetPaddingBottom(100)
        .SetFont(font).SetFontSize(20)
        .SetTextAlignment(TextAlignment.CENTER));

      doc.Add(new Paragraph()
        .AddTabStops(new TabStop(250, TabAlignment.LEFT))
        .SetFixedLeading(20)
        .Add("Имя пациента").Add(new Tab()).Add(pDFDocument.PatientName)
        .SetFont(font).SetFontSize(15));

      doc.Add(new Paragraph()
         .AddTabStops(new TabStop(250, TabAlignment.LEFT))
         .SetFixedLeading(20)
         .Add("Возраст").Add(new Tab()).Add(pDFDocument.Age)
         .SetFont(font).SetFontSize(15));

      if(pDFDocument.DrugList != null && pDFDocument.DrugList.Count != 0) 
      {
        for (int j = 0; j < pDFDocument.DrugList.Count; j++)
        {
          doc.Add(new Paragraph($"{j + 1}. {pDFDocument.DrugList[j].Recipe}\n")
            .AddStyle(recipeStyle)
            .SetFixedLeading(20));
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
        .Add("Врач").Add(new Tab())
        .Add(new Text(pDFDocument.DoctorName)
        .SetBold())
        .SetFont(font)
        .SetFontSize(15)
        .SetPaddingTop(100));
    }

    public CreatePDFService()
    {

    }
  }
}
