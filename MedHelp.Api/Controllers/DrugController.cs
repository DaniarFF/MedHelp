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

  [HttpPost("add")]
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

  [HttpPost("delete")]
  public async Task<IActionResult> DeleteDrug([FromBody] DeleteByIdRequest idRequest)
  {
    await drugService.Delete(idRequest.Id);
    return Ok();
  }

  [HttpPost("update")]
  public async Task<IActionResult> UpdateDrug([FromBody] AddDrugRequest request)
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
}
