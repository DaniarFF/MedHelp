using System.ComponentModel.DataAnnotations;

namespace MedHelp.Api.Contracts;

/// <summary>
///   Запрос на добавление нового лекарства
/// </summary>
public class AddDrugRequest
{
  [Required] public string Name { get; set; }

  [Required] public string Recipe { get; set; }

  [Url] public string RlsLink { get; set; }

  [Range(600, 610)] public int? GroupId { get; set; }
}