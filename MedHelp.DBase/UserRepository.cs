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

    public async Task<UserEntity> Get(long tgId)
    {
      return await appDbContex.Users.FirstOrDefaultAsync(x => x.TelegramId == tgId);
    }

    public async Task<UserEntity> Update(UserEntity entity)
    {
      appDbContex.Users.Update(entity);
      await appDbContex.SaveChangesAsync();
      return entity;
    }

    public Task Delete(UserEntity entity)
    {
      throw new NotImplementedException();
    }

    public UserRepository(AppDbContext appDbContex)
    {
      this.appDbContex = appDbContex;
    }
  }
}
