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
    /// Получить пользователя по идентификатору акканта в телеграмм.
    /// </summary>
    /// <param name="tgId"></param>
    /// <returns></returns>
    Task<User> Get(long tgId);
  }
}