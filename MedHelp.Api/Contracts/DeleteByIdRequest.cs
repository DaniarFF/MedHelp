namespace MedHelp.Api.Contracts;

/// <summary>
///   Запрос на удаление записи по идентификатору.
/// </summary>
public class DeleteByIdRequest
{
  public int Id { get; set; }
}
