using MedHelp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedHelp.DBase
{
  public class UserRepository : IUserRepository
  {
    private readonly AppDbContext appDbContex;

    public async Task Add(UserEntity entity)
    {
      await appDbContex.Users.AddAsync(entity);
      await appDbContex.SaveChangesAsync();
    }

    public async Task<UserEntity> Get(long telegramId)
    {
      return await appDbContex.Users.FirstOrDefaultAsync(x => x.TelegramId == telegramId);
    }

    public async Task<UserEntity> Update(UserEntity entity)
    {
      appDbContex.Users.Update(entity);
      await appDbContex.SaveChangesAsync();
      return entity;
    }

    public async Task Delete(int id)
    {
      var user = await appDbContex.Users.FindAsync(id);
      if (user == null) return;

      appDbContex.Users.Remove(user);
      await appDbContex.SaveChangesAsync();
    }

    public UserRepository(AppDbContext appDbContex)
    {
      this.appDbContex = appDbContex;
    }
  }
}
