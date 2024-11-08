using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using MedHelp.Core.Services;
using MedHelp.DBase;
using Moq;
using NUnit.Framework;

namespace MedHelp.Tests.Services;

/// <summary>
///  Тесты для сервиса пользователей.
/// </summary>
[TestFixture]
public class UserServiceTest
{
  private Mock<IUserRepository>? mockUserRepository;
  private UserService? userService;

  [OneTimeSetUp]
  public void OneTimeSetUp()
  {
    mockUserRepository = new Mock<IUserRepository>();
    userService = new UserService(mockUserRepository.Object);
  }
  
  [OneTimeTearDown]
  public void OneTimeTearDown()
  {
    mockUserRepository = null;
    userService = null;
  }
  
  [TestCase(123123,  "Bob")]
  [TestCase(332223,  "Bop")]
  public async Task Get_GetUserByTelegramId_ReturnsUser(long telegramId, string expectedResult)
  {
    var users = new List<UserEntity>()
    {
      new UserEntity() { TelegramId = 123123, Name = "Bob" },
      new UserEntity() { TelegramId = 332223, Name = "Bop" }
    };

    mockUserRepository.Setup(repo => repo.Get(telegramId))
      .ReturnsAsync(users.FirstOrDefault(u => u.TelegramId == telegramId));

    var result = await userService.Get(telegramId);
    
    Assert.That(result.Name, Is.EqualTo(expectedResult));
  }
  
  [TestCase(123123,  "Bob")]
  public void Add_NewUser_UserAddedSuccessfully(long telegramId, string expectedResult)
  {
    var user = new UserEntity()
    {
      TelegramId = 232323,
      Name = "Bob"
    };

    mockUserRepository.Setup(repo => repo.Add(user));
  }
}
