namespace MedHelp.Api.Contracts;

/// <summary>
///   Запрос на добавление нового лекарства
/// </summary>
public class AddDrugRequest
{
  public string Name { get; set; }

  public string Recipe { get; set; }

  public string RlsLink { get; set; }

  public int? GroupId { get; set; }
}
