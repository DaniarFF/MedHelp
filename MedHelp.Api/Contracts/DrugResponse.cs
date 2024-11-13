namespace MedHelp.Api.Contracts;

public class DrugResponse
{
  public DrugResponse(int id, string name, string recipe, string rlsLink, int? groupId)
  {
    Id = id;
    Name = name;
    Recipe = recipe;
    RlsLink = rlsLink;
    GroupId = groupId;
  }

  public int Id { get; set; }

  public string Name { get; set; }

  public string Recipe { get; set; }

  public string RlsLink { get; set; }

  public int? GroupId { get; set; }
}