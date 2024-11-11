using MedHelp.Core.Entities;

namespace MedHelp.DBase
{
  /// <summary>
  /// Репозиторий пользователей.
  /// </summary>
  public interface IUserRepository
  {
    /// <summary>
    /// Получить пользователя по идентификатору аккаунта в телеграмм.
    /// </summary>
    /// <param name="telegramId">Идентификатор аккаунта телеграм</param>
    /// <returns><see cref="UserEntity"/></returns>
    Task<UserEntity> Get(long telegramId);

    /// <summary>
    /// Добавить пользователя.
    /// </summary>
    /// <param name="entity"></param>
    Task Add(UserEntity entity);

    /// <summary>
    /// Обновить информацию о пользователе.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Измененного пользователя <see cref="UserEntity"/></returns>
    Task<UserEntity> Update(UserEntity entity);
    
    /// <summary>
    /// Удалить пользователя.
    /// </summary>
    /// <param name= "id"></param>
    /// <returns></returns>
    Task Delete(int id); 
  }
}
