using MedHelp.Core.Abstractions;
using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using MedHelp.DBase;

namespace MedHelp.Core.Services;

public class UserService : IUserService
{
  private readonly IUserRepository userRepository;

  public UserService(IUserRepository userRepository)
  {
    this.userRepository = userRepository;
  }

  public async Task<User> Get(long telegramId)
  {
    var userEntity = await userRepository.Get(telegramId);

    if (userEntity != null)
    {
      var user = new User
      {
        Id = userEntity.Id,
        Name = userEntity.Name,
        TelegramId = userEntity.TelegramId,
        TelegramUserName = userEntity.TelegramUserName
      };
      return user;
    }

    return null;
  }

  public async Task Update(User user)
  {
    var userEntity = new UserEntity
    {
      Name = user.Name,
      TelegramId = user.TelegramId,
      TelegramUserName = user.TelegramUserName
    };

    await userRepository.Update(userEntity);
  }

  public async Task Add(User user)
  {
    var entity = new UserEntity
    {
      Name = user.Name,
      TelegramId = user.TelegramId,
      TelegramUserName = user.TelegramUserName
    };

    await userRepository.Add(entity);
  }
}