using MedHelp.Core.Abstractions;
using MedHelp.Core.Models;
using MedHelp.Core.Services;
using MedHelp.TelegramBot.Cache;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PRTelegramBot.Attributes;
using PRTelegramBot.Extensions;
using PRTelegramBot.Interfaces;
using PRTelegramBot.Models;
using PRTelegramBot.Models.InlineButtons;
using PRTelegramBot.Utils;
using System.ComponentModel;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Message = PRTelegramBot.Helpers.Message;

namespace MedHelp
{
  [BotHandler]
  public class BotController
  {
    private readonly ILogger<BotController> logger;
    private readonly IServiceScopeFactory serviceScopeFactory;

    /// <summary>
    /// Перечисление необходимое для создания inline menu в <see cref="DiagnosisStepTwo"/>. 
    /// </summary>
    [InlineCommand]
    public enum InlineCommands
    {
      [Description("Получить рецепт")]
      StartMakingRecipe = 500,
      [Description("Посмотреть особенности каждого заболевания")]
      GetDistinctiveSigns,
      [Description("Получить рецепт")]
      GetRecipe
    }

    /// <summary>
    /// Перечисление необходимое для создания inline menu выбора группы препаратов в <see cref="StartMakeRecipe"/>.
    /// </summary>
    [InlineCommand]
    public enum DrugsGroup
    {
      Antibiotic = 600,
      NSAIDs,
      Antihistamines,
      Bronchodilators,
      Mucolytics,
      Antispasmodics,
      Adsorbents,
      SpraysAndDrops,
      DropsInTheEar,
      ForTheThroat
    }
    
    [InlineCommand]
    public enum ProfileMenu
    {
      Profile = 700,
      TreatmentTemplates,
      UpdateProfile,
    }
    
    [ReplyMenuHandler("/start","/menu", "Главное меню")]
    public async Task StartFirstStep(ITelegramBotClient botClient, Update update)
    {     
      try
      {
        using var scope = serviceScopeFactory.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        var existingUser = await userService.Get(update.Message.Chat.Id);

        if (existingUser.TelegramId == 0)
        {
          update.RegisterStepHandler(new StepTelegram(UserRegistration));
          string msg = "Похоже вы до этого не пользовались ботом!\n Напишите ваше имя в формате Фамилия И.О." +
                       "\n (нужно для формирования рецептурных бланков)";
          await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
        else
        {
          string msg = "Выберете пункт меню.";
          var option = new OptionMessage();
          var menuList = new List<KeyboardButton>();
          
          menuList.Add(new KeyboardButton("Диагностика заболевания \U0001FA7A"));
          menuList.Add(new KeyboardButton("Найти препарат по названию \U0001F489"));
          menuList.Add(new KeyboardButton("Сформировать рецепт \U0001F48A"));
          menuList.Add(new KeyboardButton("Мой профиль \U0001F468"));
          
          var menu = MenuGenerator.ReplyKeyboard(2, menuList, true);

          option.MenuReplyKeyboardMarkup = menu;
          await PRTelegramBot.Helpers.Message.Send(botClient, update, msg, option);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Ошибка при выполнении /start", ex.Message);
      }
    }
    
    #region Диагностика заболевания

    /// <summary>
    /// Начало выполнения команды для диагностики заболевания.
    /// Активируется после ввода /diagnosis.
    /// </summary>
    /// <param name="botClient">Экземпляр <see cref="ITelegramBotClient"/></param>
    /// <param name="update">Действие с ботом</param>
    /// <returns></returns>
    [ReplyMenuHandler("/diagnosis", "Диагностика заболевания \U0001FA7A")]
    public async Task DiagnosisStartStep(ITelegramBotClient botClient, Update update)
    {     
      try
      {
        update.CreateCacheData<BotCache>();

        using var scope = serviceScopeFactory.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        var existingUser = await userService.Get(update.Message.Chat.Id);

        if (existingUser.TelegramId == 0)
        {
          update.RegisterStepHandler(new StepTelegram(UserRegistration));
          string msg = "Напишите ваше имя в формате Фамилия И.О.\n (нужно для формирования рецептурных бланков)";
          await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
        else
        {
          update.RegisterStepHandler(new StepTelegram(DiagnosisStepTwo, new BotCache() { DoctorName = existingUser.Name }));
          string msg = "Введите симптомы в формате: Температура 37 кашель насморк сыпь";
          await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Ошибка при выполнении /diagnosis", ex.Message);
      }
    }

    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="botClient">Экземпляр <see cref="ITelegramBotClient"/></param>
    /// <param name="update"></param>
    /// <returns></returns>
    public async Task UserRegistration(ITelegramBotClient botClient, Update update)
    {
      try
      {
        //Обработчик пошагового выполнения команд
        var handler = update.GetStepHandler<StepTelegram>();

        using var scope = serviceScopeFactory.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        string name = update.Message.Text;

        if (name != null)
        {
          Core.Models.User userToAdd = new Core.Models.User
          {
            Name = name,
            TelegramId = update.Message.Chat.Id,
            TelegramUserName = update.Message.From.Username
          };

          await userService.Add(userToAdd);

          handler!.GetCache<BotCache>().DoctorName = update.Message.Text;

          var cache = update.GetCacheData<BotCache>();

          handler?.RegisterNextStep(DiagnosisStepTwo);
          string msg = "Введите симптомы в формате: Температура 37 кашель насморк сыпь";
          await PRTelegramBot.Helpers.Message.Send(botClient, update, msg);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Ошибка регистрации пользователя", ex.Message);
      }
    }

    /// <summary>
    /// Второй шаг команды диагностики заболевания.
    /// </summary>
    /// <returns></returns>
    public async Task DiagnosisStepTwo(ITelegramBotClient botClient, Update update)
    {
      using var scope = serviceScopeFactory.CreateScope();
      var diseaseService = scope.ServiceProvider.GetRequiredService<IDiseaseService>();

      var handler = update.GetStepHandler<StepTelegram>();

      string symptoms = update.Message.Text;

      if (symptoms != null)
      {
        var diseases = (await diseaseService.GetAll()).ToList();

        var closestDeseases = diseaseService.GetClosestDisease(symptoms, diseases);

        string msg = "";
        if (closestDeseases.Count == 0)
        {
          await PRTelegramBot.Helpers.Message.Send(botClient, update,
            $"Слишком мало данных или не удалось найти болезнь с такими странными симптомами");
          return;
        }
        if (closestDeseases.Count == 1)
        {
          msg = $"Наиболее подходящая болезнь - {closestDeseases[0].Name}";
        }
        if (closestDeseases.Count == 2)
        {
          msg = $"Наиболее подходящая болезнь - {closestDeseases[0].Name}, " +
            $"но так же стоит учитываять, что у пациента можеть быть {closestDeseases[1].Name}";
        }

        update.GetCacheData<BotCache>().Diseases = closestDeseases;

        var inlineButtonOne = new InlineCallback($"Получить рецепт для лечения {closestDeseases[0].Name}", InlineCommands.StartMakingRecipe);
        var inlineButtonTwo = new InlineCallback("Посмотреть особенности заболевания", InlineCommands.GetDistinctiveSigns);

        List<IInlineContent> menu = new()
        {
          inlineButtonOne,
          inlineButtonTwo
        };

        var inlineMenu = MenuGenerator.InlineKeyboard(1, menu);

        var option = new OptionMessage();
        option.MenuInlineKeyboardMarkup = inlineMenu;

        var cache = update.GetCacheData<BotCache>();

        await Message.Send(botClient, update, msg, option);
      }
    }

    /// <summary>
    /// Получение особенности заболевания. 
    /// Активируется после нажатия соответствующей кнопки inlineMenu.
    /// </summary>
    /// <returns></returns>
    [InlineCallbackHandler<InlineCommands>(InlineCommands.GetDistinctiveSigns)]
    public async Task GetDistinctiveSigns(ITelegramBotClient botClient, Update update)
    {
      var diseases = update.GetCacheData<BotCache>().Diseases;

      if (diseases.Count == 1)
      {
        await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Наиболее подходящая болезнь - {diseases[0].Name}");
        await Task.Delay(1000);
        await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Особенности этого заболевания:\n {diseases[0].DistinctiveSigns}");
      }
      if (diseases.Count == 2)
      {
        await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Наиболее подходящая болезнь - {diseases[0].Name}, но так же стоит учитываять, что у пациента можеть быть {diseases[1].Name}");
        await Task.Delay(1000);
        await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Особенности первого заболевания:\n {diseases[0].DistinctiveSigns}");
        await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Особенности второго заболевания:\n {diseases[1].DistinctiveSigns}");
      }
    }

    #endregion

    #region Формирования рецепта

    /// <summary>
    /// Начало формирование рецепта. Создает inlineMenu для выбора группы препаратов.
    /// Активируется после нажатия соответствующей кнопки inline меню.
    /// </summary>
    /// <returns></returns>
    [InlineCallbackHandler<InlineCommands>(InlineCommands.StartMakingRecipe)]
    [ReplyMenuHandler("Сформировать рецепт \U0001F48A")]
    public async Task StartMakeRecipe(ITelegramBotClient botClient, Update update)
    {
      var option = new OptionMessage();
      List<IInlineContent> inlineMenuList = new();

      if (update.Message != null && update.Message.Text == "Закончить выбор")
      {
        var inlineButtonO = new InlineCallback($"Получить рецепт", InlineCommands.GetRecipe);
        inlineMenuList.Add(inlineButtonO);
      }
      else
      {
        var inlineButtonOne = new InlineCallback($"Антибиотики", DrugsGroup.Antibiotic);
        var inlineButtonTwo = new InlineCallback($"НПВС", DrugsGroup.NSAIDs);
        var inlineButtonThree = new InlineCallback($"Спреи и капли в нос", DrugsGroup.SpraysAndDrops);
        var inlineButtonFour = new InlineCallback($"Препараты для горла", DrugsGroup.ForTheThroat);
        var inlineButtonFive = new InlineCallback($"Антигистаминные", DrugsGroup.Antihistamines);
        //var inlineButtonOne = new InlineCallback($"Адсорбенты", DrugsGroup.Adsorbents);    
        // var inlineButtonOne = new InlineCallback($"Спазмолитики", DrugsGroup.Antispasmodics);
        // var inlineButtonOne = new InlineCallback($"Капли в ухо", DrugsGroup.DropsInTheEar);
        //var inlineButtonOne = new InlineCallback($"Бронходилятаторы", DrugsGroup.Bronchodilators);

        inlineMenuList.Add(inlineButtonOne);
        inlineMenuList.Add(inlineButtonTwo);
        inlineMenuList.Add(inlineButtonThree);
        inlineMenuList.Add(inlineButtonFour);
        inlineMenuList.Add(inlineButtonFive);
      }

      var inlineMenu = MenuGenerator.InlineKeyboard(1, inlineMenuList);

      option.MenuInlineKeyboardMarkup = inlineMenu;

      var cache = update.GetCacheData<BotCache>();

      await PRTelegramBot.Helpers.Message.Send(botClient, update, "Выберете группы препаратов", option);
    }

    /// <summary>
    /// Выбор препаратов для формирования рецептов. Создает ReplyMenu для выбора препарата.
    /// Активируется после выбора группы препаратов в inline меню.
    /// </summary>
    /// <returns></returns>
    [InlineCallbackHandler<DrugsGroup>(DrugsGroup.Antibiotic, DrugsGroup.DropsInTheEar, DrugsGroup.NSAIDs, DrugsGroup.ForTheThroat, DrugsGroup.Antihistamines)]
    public async Task ChooseDrugs(ITelegramBotClient botClient, Update update)
    {
      update.RegisterStepHandler(new StepTelegram(AddDrugs, new BotCache()));

      using var scope = serviceScopeFactory.CreateScope();
      var drugService = scope.ServiceProvider.GetRequiredService<IDrugService>();

      var command = InlineCallback.GetCommandByCallbackOrNull(update.CallbackQuery.Data).CommandType.ToString();

      DrugsGroup group = (DrugsGroup)Enum.Parse(typeof(DrugsGroup), command);

      int groupNum = (int)group;

      var drugs = drugService.GetAll().Where(d => d.GroupId == groupNum);

      string msg = "Выберете препарат";

      var option = new OptionMessage();
      var menuList = new List<KeyboardButton>();

      foreach (var drug in drugs)
      {
        menuList.Add(new KeyboardButton(drug.Name));
      }

      var cache = update.GetCacheData<BotCache>();

      var menu = MenuGenerator.ReplyKeyboard(1, menuList, true, "Закончить выбор");
      option.MenuReplyKeyboardMarkup = menu;
      await PRTelegramBot.Helpers.Message.Send(botClient, update, msg, option);
    }

    /// <summary>
    /// Добавление препаратов в список перед формированием рецепта.
    /// Активируется после метода ChooseDrugs.
    /// </summary>
    /// <returns></returns>
    public async Task AddDrugs(ITelegramBotClient botClient, Update update)
    {
      using var scope = serviceScopeFactory.CreateScope();
      var drugService = scope.ServiceProvider.GetRequiredService<IDrugService>();

      if (update.Message.Text == "Закончить выбор")
      {
        update.RegisterStepHandler(new StepTelegram(GetRecipe));
        return;
      }

      var cache = update.GetCacheData<BotCache>();

      update.RegisterStepHandler(new StepTelegram(StartMakeRecipe));

      var drug = drugService.Get(update.Message.Text);

      var currentDrugs = update.GetCacheData<BotCache>().Drugs;

      if (currentDrugs == null)
        currentDrugs = new List<Drug>() { drug };
      else
        currentDrugs.Add(drug);

      update.GetCacheData<BotCache>().Drugs = currentDrugs;

      StringBuilder sb = new StringBuilder();
      foreach (var item in currentDrugs)
      {
        sb.Append(item.Name + " ");
      }

      var option = new OptionMessage();
      var menuList = new List<KeyboardButton>() { "Продолжить выбор", "Закончить выбор" };

      var menu = MenuGenerator.ReplyKeyboard(1, menuList, true, "Главное меню");
      option.MenuReplyKeyboardMarkup = menu;

      await PRTelegramBot.Helpers.Message.Send(botClient, update, $"Выбраны препараты: {sb}", option);
    }

    /// <summary>
    /// Создает и отправляет сформированный рецепт.
    /// Активируется после нажатия соответствующей кнопки inlineMenu.
    /// </summary>
    /// <returns></returns>
    [InlineCallbackHandler<InlineCommands>(InlineCommands.GetRecipe)]
    public async Task GetRecipe(ITelegramBotClient botClient, Update update)
    {
      using var scope = serviceScopeFactory.CreateScope();
      var pdfService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
      var userService = scope.ServiceProvider.GetService<IUserService>();

      var cache = update.GetCacheData<BotCache>();
      var user = await userService.Get(update.GetChatId());

      MedicalDocument pDFDocument = new MedicalDocument()
      {
        PatientName = "Сергеев С.С.",
        Age = "5",
        DoctorName = user.Name,
        DrugList = update.GetCacheData<BotCache>().Drugs,
      };

      //TODO Сделать свою уникальную дозировку под каждое заболевание
      pdfService.CreatePDFFile(pDFDocument);

      await using Stream stream = System.IO.File.OpenRead("recipe.pdf");
      var message = await botClient.SendDocumentAsync(update.GetChatId(), document: InputFile.FromStream(stream, "recipe.pdf"),
          caption: "Рецепт");
    } 
    #endregion

    #region Профиль

    [ReplyMenuHandler("/profile", "Мой профиль \U0001F468")]
    public async Task ProfileStartStep(ITelegramBotClient botClient, Update update)
    {     
      try
      {
        update.CreateCacheData<BotCache>();

        using var scope = serviceScopeFactory.CreateScope(); 
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        var currentUser = await userService.Get(update.Message.Chat.Id);
        update.GetCacheData<BotCache>().UserId = currentUser.Id;

        if (currentUser.TelegramId == 0)
        {
          update.RegisterStepHandler(new StepTelegram(UserRegistration));
          string msg = "Напишите ваше имя в формате Фамилия И.О.";
          await Message.Send(botClient, update, msg);
        }
        else
        {
          var inlineButtonOne = new InlineCallback("Изменить имя", ProfileMenu.Profile); 
          var inlineButtonTwo = new InlineCallback("Шаблоны лечения", ProfileMenu.TreatmentTemplates); 

          List<IInlineContent> menu = new()
          {
            inlineButtonOne,
            inlineButtonTwo
          };

          var inlineMenu = MenuGenerator.InlineKeyboard(1, menu);

          var option = new OptionMessage();
          option.MenuInlineKeyboardMarkup = inlineMenu;

          update.GetCacheData<BotCache>().DoctorName = currentUser.Name;  

          var msg = $"{currentUser.Name}, выберите пункт меню"; 

          await Message.Send(botClient, update, msg , option);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Ошибка при выполнении /profile", ex.Message);
      }
    }
    
    [InlineCallbackHandler<ProfileMenu>(ProfileMenu.Profile)]  
    public async Task ShowProfile(ITelegramBotClient botClient, Update update)
    {
      update.RegisterStepHandler(new StepTelegram(UserNameUpdate));
      var msg = $"Напишите ваше имя в формате Фамилия И.О."; 
      await Message.Send(botClient, update, msg);
    }

    public async Task UserNameUpdate(ITelegramBotClient botClient, Update update)
    {
      try
      {
        using var scope = serviceScopeFactory.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        string name = update.Message.Text;

        if (name != null)
        {
          Core.Models.User userToUpdate = new Core.Models.User
          {
            Name = name,
            TelegramId = update.Message.Chat.Id,
            TelegramUserName = update.Message.Chat.Username,  
          };

          await userService.Update(userToUpdate);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Ошибка при выполнении UserNameUpdate", ex.Message);
      }
    }
    
    [InlineCallbackHandler<ProfileMenu>(ProfileMenu.TreatmentTemplates)]  
    public async Task ShowTemplates(ITelegramBotClient botClient, Update update)
    {
      using var scope = serviceScopeFactory.CreateScope(); 
      var templateService = scope.ServiceProvider.GetRequiredService<TreatmentTemplateService>();
      
      var userId = update.GetCacheData<BotCache>().UserId; 
      
      var templates = templateService.GetTemplatesByUserId(userId);

      string msg =  "У вас нет сохраненных шаблонов лечения.";
      if (templates.Any())
      {
        var sb = new StringBuilder();
        var i = 1; 
        foreach (var template in templates)
        {
          sb.AppendLine($"{i}. {template.TemplateName}");
        }
        
        msg = $"Выберите шаблон лечения:\n {sb}";
      }
     
      await Message.Send(botClient, update, msg);
    }

    #endregion

    #region Конструктор
    public BotController(ILogger<BotController> logger, IServiceScopeFactory serviceScopeFactory)
    {
      this.logger = logger;
      this.serviceScopeFactory = serviceScopeFactory;
    } 
    #endregion
  }
}
