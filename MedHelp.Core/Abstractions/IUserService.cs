using MedHelp.Core.Models;

namespace MedHelp.Core.Abstractions
{
  /// <summary>
  /// Сервис по работе с пользователями.
  /// </summary>
  public interface IUserService
  {
    /// <summary>
    /// Добавить пользователя.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task Add(User user);

    /// <summary>
    /// Получить пользователя по идентификатору аккаунта в телеграмм.
    /// </summary>
    /// <param name="telegramId">Идентификатор аккаунта телеграм</param>
    /// <returns></returns>
    Task<User> Get(long telegramId);
    
    /// <summary>
    /// Обновить данные пользователя.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
     Task Update(User user);
  }
}
