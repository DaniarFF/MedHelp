using MedHelp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedHelp.DBase
{
  public class UserRepository : IUserRepository
  {
    private readonly AppDbContext appDbContex;
    private readonly ILogger<UserRepository> logger;

    public async Task Add(UserEntity entity)
    {
      try
      {
        await appDbContex.Users.AddAsync(entity);
        await appDbContex.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw;
      }
    }

    public async Task<UserEntity> Get(long tgId)
    {
      return await appDbContex.Users.FirstOrDefaultAsync(x => x.TelegramId == tgId);
    }


    public async Task<UserEntity> Update(UserEntity entity)
    {
      try
      {
        appDbContex.Users.Update(entity);
        await appDbContex.SaveChangesAsync();
        return entity;
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Ошибка обновления информации пользователя");
        throw;
      }
    }

    public UserRepository(AppDbContext appDbContex, ILogger<UserRepository> logger)
    {
      this.appDbContex = appDbContex;
      this.logger = logger;
    }
  }
}
