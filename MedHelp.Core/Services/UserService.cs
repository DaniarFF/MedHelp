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

    public async Task<User> Get(long tgId)
    {
      var userEntity = await userRepository.Get(tgId);

      if (userEntity != null)
      {
        User user = new User() 
        { 
        Id = userEntity.Id,
        Name = userEntity.Name,
        TelegramId = userEntity.TelegramId,        
        };
        return user;
      };

      return null;
    }

    public async Task Add(User user)
    {
      UserEntity entity = new UserEntity()
      {
        Name = user.Name,
        TelegramId = user.TelegramId,
      };

      await userRepository.Add(entity);
    }

    public UserService(IUserRepository userRepository)
    {
      this.userRepository = userRepository;
    }
  }
}
