using MedHelp.Core.Entities;
using MedHelp.Core.Services;
using MedHelp.DBase;
using Moq;
using NUnit.Framework;

namespace MedHelp.Tests.Services;

/// <summary>
///   Тесты для сервиса пользователей.
/// </summary>
[TestFixture]
public class UserServiceTest
{
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

  private Mock<IUserRepository>? mockUserRepository;
  private UserService? userService;

  [TestCase(123123, "Bob")]
  [TestCase(332223, "Bop")]
  public async Task Get_GetUserByTelegramId_ReturnsUser(long telegramId, string expectedResult)
  {
    var users = new List<UserEntity>
    {
      new() { TelegramId = 123123, Name = "Bob" },
      new() { TelegramId = 332223, Name = "Bop" }
    };

    mockUserRepository.Setup(repo => repo.Get(telegramId))
      .ReturnsAsync(users.FirstOrDefault(u => u.TelegramId == telegramId));

    var result = await userService.Get(telegramId);

    Assert.That(result.Name, Is.EqualTo(expectedResult));
  }

  [TestCase(123123, "Bob")]
  public void Add_NewUser_UserAddedSuccessfully(long telegramId, string expectedResult)
  {
    var user = new UserEntity
    {
      TelegramId = 232323,
      Name = "Bob"
    };

    mockUserRepository.Setup(repo => repo.Add(user));
  }
}