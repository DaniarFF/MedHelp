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
public class DiseasesController : ControllerBase
{
  private readonly IDiseaseService diseaseService;

  public DiseasesController(IDiseaseService diseaseService)
  {
    this.diseaseService = diseaseService;
  }

  [HttpGet]
  public async Task<ActionResult<List<DiseaseResponse>>> GetAll()
  {
    var diseases = await diseaseService.GetAll();
    var response = diseases
      .Select(d => new DiseaseResponse(d.Name, d.Symptoms, d.Recomendations, d.DistinctiveSigns));

    return Ok(response);
  }

  [HttpPost("add")]
  public async Task<IActionResult> AddDrug([FromBody] AddDiseaseRequest request)
  {
    var newDisease = new Disease
    {
      Name = request.Name,
      Symptoms = request.Symptoms,
      Recomendations = request.Recomendations,
      DistinctiveSigns = request.DistinctiveSigns
    };

    await diseaseService.Add(newDisease);
    return Ok();
  }

  [HttpPost("delete")]
  public async Task<IActionResult> DeleteDrug([FromBody] DeleteByIdRequest request)
  {
    await diseaseService.Delete(request.Id);
    return Ok();
  }

  [HttpPost("update")]
  public async Task<IActionResult> UpdateDrug([FromBody] AddDiseaseRequest request)
  {
    var disease = new Disease
    {
      Id = request.Id,
      Name = request.Name,
      Symptoms = request.Symptoms,
      Recomendations = request.Recomendations,
      DistinctiveSigns = request.DistinctiveSigns
    };

    await diseaseService.Update(disease);
    return Ok();
  }
}
