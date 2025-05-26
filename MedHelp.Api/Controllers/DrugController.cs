using MedHelp.Api.Contracts;
using MedHelp.Core.Abstractions;
using MedHelp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedHelp.Api.Api;

/// <summary>
///   Контроллер для работы с лекарствами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DrugsController : ControllerBase
{
  private readonly IDrugService drugService;

  public DrugsController(IDrugService drugService)
  {
    this.drugService = drugService;
  }

  [HttpGet]
  public ActionResult<List<DrugResponse>> GetAll()
  {
    var drugs = drugService.GetAll();
    var response = drugs
      .Select(d => new DrugResponse(d.Id, d.Name, d.Recipe, d.RlsLink, d.GroupId));

    return Ok(response);
  }

  [HttpPost]
  public async Task<IActionResult> AddDrug([FromBody] AddDrugRequest request)
  {
    var newDrug = new Drug
    {
      Name = request.Name,
      Recipe = request.Recipe,
      RlsLink = request.RlsLink,
      GroupId = request.GroupId
    };

    await drugService.Add(newDrug);
    return Ok();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteDrug([FromRoute] int id)
  {
    await drugService.Delete(id);
    return Ok();
  }
}