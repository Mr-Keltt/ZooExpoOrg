using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZooExpoOrg.Common.Enumerables;
using ZooExpoOrg.Services.Accounts;
using ZooExpoOrg.Services.Animals.Achievements;
using ZooExpoOrg.Services.Animals.Animals;
using ZooExpoOrg.Services.Clients;
using ZooExpoOrg.Services.Comments;
using ZooExpoOrg.Services.Expositions;
using ZooExpoOrg.Services.Photos;

namespace ZooExpoOrg.Context;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    }

    private static MainDbContext DbContext(IServiceProvider serviceProvider)
    {
        return ServiceScope(serviceProvider)
            .ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();
    }

    public static void Execute(IServiceProvider serviceProvider)
    {
        Task.Run(async () =>
        {
            await AddDemoData(serviceProvider);
            await AddAdministrator(serviceProvider);
        })
            .GetAwaiter()
            .GetResult();
    }

    private static async Task AddDemoData(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings.Init?.AddDemoData ?? false))
            return;

        await using var context = DbContext(serviceProvider);

        var alise = await context.Users.FirstOrDefaultAsync(x => x.UserName == "alise");
        var bob = await context.Users.FirstOrDefaultAsync(x => x.UserName == "bob");

        if (alise != null || bob != null)
            return;

        var accountService = scope.ServiceProvider.GetService<IAccountService>();
        var achievementsService = scope.ServiceProvider.GetService<IAchievementService>();
        var animalService = scope.ServiceProvider.GetService<IAnimalService>();
        var clientService = scope.ServiceProvider.GetService<IClientService>();
        var commentService = scope.ServiceProvider.GetService<ICommentService>();
        var expositionService = scope.ServiceProvider.GetService<IExpositionService>();
        var photoService = scope.ServiceProvider.GetService<IPhotoService>();

        // Crate users

        var aliseUser = await accountService.Create(new RegisterAccountModel()
        {
            UserName = "alise",
            Email = "alise@mail.com",
            Password = "123"
        });

        var bobUser = await accountService.Create(new RegisterAccountModel()
        {
            UserName = "bob",
            Email = "bob@mail.com",
            Password = "123"
        });

        var calUser = await accountService.Create(new RegisterAccountModel()
        {
            UserName = "cal",
            Email = "cal@mail.com",
            Password = "123"
        });

        // Create clients

        var aliseClient = await clientService.Create(new CreateClientModel()
        {
            UserId = aliseUser.Id,
            Name = "Алиса",
            Surname = "Иванова",
            Patronymic = "Ивановна",
            BirthDate = DateTime.UtcNow,
            Gender = Common.Enumerables.Gender.Female,
        });

        await photoService.Create(new CreatePhotoModel()
        {
            OwnerId = aliseClient.Id,
            LocationId = aliseClient.Id,
            ImageData = new byte[1],
            ImageMimeType = "img"
        });

        var bobClient = await clientService.Create(new CreateClientModel()
        {
            UserId = bobUser.Id,
            Name = "Боб",
            Surname = "Петров",
            BirthDate = DateTime.UtcNow,
            Gender = Common.Enumerables.Gender.Male,
        });

        var calClient = await clientService.Create(new CreateClientModel()
        {
            UserId = calUser.Id,
            Name = "Кэл",
            Surname = "Сергеев",
            BirthDate = DateTime.UtcNow,
            Gender = Common.Enumerables.Gender.Male,
        });

        await photoService.Create(new CreatePhotoModel()
        {
            OwnerId = calClient.Id,
            LocationId = calClient.Id,
            ImageData = new byte[1],
            ImageMimeType = "img"
        });

        // Create animals

        var bobAnimals = new List<AnimalModel>();

        bobAnimals.Add(await animalService.Create(new CreateAnimalModel()
        {
            OwnerId = bobClient.Id,
            Name = "Собака боба №1",
            Description = "Одна из собак боба",
            Type = AnimalType.Dog,
            BirthDate = DateTime.UtcNow,
            Gender = Gender.Male,
            Height = 50,
            Weight = 25
        }));

        for (int i = 0; i < 3; i++)
        {
            await photoService.Create(new CreatePhotoModel()
            {
                OwnerId = bobClient.Id,
                LocationId = bobAnimals[0].Id,
                ImageData = new byte[1],
                ImageMimeType = "img"
            });
        }

        await achievementsService.Create(new CreateAchievementModel()
        {
            AnimalId = bobAnimals[0].Id,
            Name = "Доказательство крутости",
            Description = "Эта собака крутая",
            DateAward = DateTime.UtcNow
        });

        bobAnimals.Add(await animalService.Create(new CreateAnimalModel()
        {
            OwnerId = bobClient.Id,
            Name = "Собака боба №2",
            Description = "Еще одна собака боба",
            Type = AnimalType.Dog,
            BirthDate = DateTime.UtcNow,
            Gender = Gender.Female,
            Height = 30,
            Weight = 15
        }));

        for (int i = 0; i < 5; i++)
        {
            await photoService.Create(new CreatePhotoModel()
            {
                OwnerId = bobClient.Id,
                LocationId = bobAnimals[1].Id,
                ImageData = new byte[1],
                ImageMimeType = "img"
            });
        }

        bobAnimals.Add(await animalService.Create(new CreateAnimalModel()
        {
            OwnerId = bobClient.Id,
            Name = "Попугай боба",
            Description = "У него еще и попугай есть :)",
            Type = AnimalType.Parrot,
            BirthDate = DateTime.UtcNow,
            Gender = Gender.Female,
            Height = -1,
            Weight = -1
        }));

        for (int i = 0; i < 2; i++)
        {
            await photoService.Create(new CreatePhotoModel()
            {
                OwnerId = bobClient.Id,
                LocationId = bobAnimals[2].Id,
                ImageData = new byte[1],
                ImageMimeType = "img"
            });
        }

        var calAnimals = new List<AnimalModel>();

        calAnimals.Add(await animalService.Create(new CreateAnimalModel()
        {
            OwnerId = calClient.Id,
            Name = "Собака Кэла №1",
            Description = "Это собака Кэла",
            Type = AnimalType.Dog,
            BirthDate = DateTime.UtcNow,
            Gender = Gender.Male,
            Height = 50,
            Weight = 25
        }));

        for (int i = 0; i < 3; i++)
        {
            await photoService.Create(new CreatePhotoModel()
            {
                OwnerId = calClient.Id,
                LocationId = calAnimals[0].Id,
                ImageData = new byte[1],
                ImageMimeType = "img"
            });
        }

        calAnimals.Add(await animalService.Create(new CreateAnimalModel()
        {
            OwnerId = calClient.Id,
            Name = "Кошка Кэла",
            Description = "Очень длинное описание кошки: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris quis odio aliquam, rhoncus odio iaculis, scelerisque turpis. Mauris eget eros vitae orci sollicitudin dapibus id sed mi. Sed nisl odio, mollis at nulla ut, tempus maximus leo. Etiam ac sapien faucibus, sodales lectus vitae, pulvinar nisl. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aenean magna nisi, aliquam nec dui ac, varius molestie diam. Aenean pulvinar sodales erat, condimentum eleifend est convallis posuere. Phasellus et erat eu nunc mollis interdum ac a purus. Nulla efficitur aliquet urna, in mollis elit sodales non. Duis eleifend consequat enim, quis bibendum ante. Fusce cursus nisl dui, in sollicitudin dolor sagittis id. Interdum et malesuada fames ac ante ipsum primis in faucibus.",
            Type = AnimalType.Cat,
            BirthDate = DateTime.UtcNow,
            Gender = Gender.Female,
            Height = 30,
            Weight = 15
        }));

        for (int i = 0; i < 5; i++)
        {
            await photoService.Create(new CreatePhotoModel()
            {
                OwnerId = calClient.Id,
                LocationId = calAnimals[1].Id,
                ImageData = new byte[1],
                ImageMimeType = "img"
            });
        }

        // Create expositions

        var aliseExposition = await expositionService.Create(new CreateExpositionModel()
        {
            Title = "Тестовая выста собак",
            OrganizerId = aliseClient.Id,
            Description = "Это тестовая выста собак!",
            ParticipantsType = AnimalType.Dog,
            Country = "Россия",
            City = "Москва",
            Street = "Ленина",
            HouseNumber = "5",
            DateStart = DateTime.UtcNow,
            DateEnd = DateTime.UtcNow
        }); 

        for (int i = 0; i < 10; i++)
        {
            await photoService.Create(new CreatePhotoModel()
            {
                OwnerId = aliseClient.Id,
                LocationId = aliseExposition.Id,
                ImageData = new byte[1],
                ImageMimeType = "img"
            });
        }

        await expositionService.Subscribe(
            aliseExposition.Id,
            bobClient.Id
            );

        await expositionService.Subscribe(
            aliseExposition.Id,
            calClient.Id
            );

        await expositionService.AddParticipant(
            aliseExposition.Id,
            bobAnimals[1].Id
            );

        await expositionService.AddParticipant(
            aliseExposition.Id,
            bobAnimals[0].Id
            );

        // Create commets
        await commentService.Create(new CreateCommentModel()
        {
            AuthorId = aliseClient.Id,
            LocationId = aliseExposition.Id,
            DateWriting = DateTime.UtcNow,
            Text = "Я создатель этой выстовки!"
        });

        await commentService.Create(new CreateCommentModel()
        {
            AuthorId = bobClient.Id,
            LocationId = aliseExposition.Id,
            DateWriting = DateTime.UtcNow,
            Text = "Я обычный коментатор"
        });

        await commentService.Create(new CreateCommentModel()
        {
            AuthorId = calClient.Id,
            LocationId = aliseExposition.Id,
            DateWriting = DateTime.UtcNow,
            Text = "Я тоже обычный коментатор"
        });

        await commentService.Create(new CreateCommentModel()
        {
            AuthorId = bobClient.Id,
            LocationId = bobAnimals[0].Id,
            DateWriting = DateTime.UtcNow,
            Text = "Это моя собака!"
        });

        await commentService.Create(new CreateCommentModel()
        {
            AuthorId = calClient.Id,
            LocationId = bobAnimals[0].Id,
            DateWriting = DateTime.UtcNow,
            Text = "Я обычный коментатор"
        });
    }

    private static async Task AddAdministrator(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings.Init?.AddAdministrator ?? false))
            return;

        var accountService = scope.ServiceProvider.GetService<IAccountService>();

        var accounts = await accountService.GetAll();

        if (accounts.FirstOrDefault(x => x.UserName == settings.Init.Administrator.UserName) != null)
            return;

        await accountService.Create(new RegisterAccountModel()
        {
            UserName = settings.Init.Administrator.UserName,
            Email = settings.Init.Administrator.Email,
            Password = settings.Init.Administrator.Password,
        });
    }
}
