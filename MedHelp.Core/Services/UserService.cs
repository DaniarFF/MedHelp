using MedHelp.Core.Abstractions;
using MedHelp.Core.Entities;
using MedHelp.Core.Models;
using MedHelp.DBase;
using Microsoft.Extensions.Logging;

namespace MedHelp.Core.Services
{
  public class UserService : IUserService
  {
    private readonly IUserRepository userRepository;
    private readonly ILogger logger;

    public async Task<User> Get(long tgId)
    {
      User user = new User();

      var userEntity = await userRepository.Get(tgId);

      if (userEntity != null)
      {
        user.Id = userEntity.Id;
        user.Name = userEntity.Name;
        user.TelegramId = userEntity.TelegramId;
        user.TelegramUserName = userEntity.TelegramUserName;  
      };

      return user;
    }

  public async Task Add(User user)
  {
    UserEntity entity = new UserEntity()
    {
      Name = user.Name,
      TelegramId = user.TelegramId,
      TelegramUserName = user.TelegramUserName,
    };

    await userRepository.Add(entity);
  }

  public UserService(IUserRepository userRepository, ILogger<UserService> logger)
  {
    this.userRepository = userRepository;
    this.logger = logger;
  }
}
}
