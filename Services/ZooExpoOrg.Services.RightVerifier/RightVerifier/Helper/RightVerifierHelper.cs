using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;

namespace ZooExpoOrg.Services.RightVerifier.Helper;

public class RightVerifierHelper
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly DbSettings dbSettings;

    public RightVerifierHelper(
        IDbContextFactory<MainDbContext> dbContextFactory,
        DbSettings dbSettings)
    {
        this.dbContextFactory = dbContextFactory;
        this.dbSettings = dbSettings;
    }

    public bool EqualsId(Guid id1, Guid id2)
    {
        if (id1 == id2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<Guid> GetAdminId()
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var admin = await db.Users.FirstOrDefaultAsync(x => x.UserName == dbSettings.Init.Administrator.UserName);

        if (admin == null)
        {
            if (dbSettings.Init.AddAdministrator)
            {
                throw new ProcessException("Administrator not found.");
            }
            else
            {
                return Guid.NewGuid();
            }
        }

        return admin.Id;
    }

    public async Task<Guid> GetUserId(string jwtToken)
    {
        string validJwtToken = jwtToken.Replace("bearer ", "");

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadToken(validJwtToken) as JwtSecurityToken;

        var userIdString = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;

        if (Guid.TryParse(userIdString, out Guid userId))
        {
            return userId;
        }
        else
        {
            throw new ProcessException("Invalid user ID format.");
        }
    }

    public async Task<Guid> GetClientId(string jwtToken)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var userId = await GetUserId(jwtToken);

        var client = await db.Clients.FirstOrDefaultAsync(x => x.UserId == userId);

        if (client == null)
        {
            throw new ProcessException($"Client (By jwt = {jwtToken}) not found.");
        }

        return client.Uid;
    }

    public async Task<Guid> GetClientIdByAchievementId(Guid achievementId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var achievement = await db.Achievements
            .Include(x => x.Animal)
            .FirstOrDefaultAsync(x => x.Uid == achievementId);

        if (achievement == null)
        {
            throw new ProcessException($"Achievement (Id = {achievementId}) not found.");
        }

        return await GetClientIdByAnimalId(achievement.Animal.Uid);
    }

    public async Task<Guid> GetClientIdByAnimalId(Guid animalId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var animal = await db.Animals
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Uid == animalId);

        if (animal == null)
        {
            throw new ProcessException($"Animal (Id = {animalId}) not found.");
        }

        return animal.Owner.Uid;
    }

    public async Task<Guid> GetClientIdByCommentId(Guid commentId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var comment = await db.Comments
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Uid == commentId);

        if (comment == null)
        {
            throw new ProcessException($"Comment (Id = {commentId}) not found.");
        }

        return comment.Author.Uid;
    }

    public async Task<Guid> GetClientIdByExpositionId(Guid expositionId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var exposition = await db.Expositions
            .Include(x => x.Organizer)
            .FirstOrDefaultAsync(x => x.Uid == expositionId);

        if (exposition == null)
        {
            throw new ProcessException($"Exposition (Id = {expositionId}) not found.");
        }

        return exposition.Organizer.Uid;
    }

    public async Task<Guid> GetClientIdByPhotoId(Guid photoId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var photo = await db.Photos
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Uid == photoId);

        if (photo == null)
        {
            throw new ProcessException($"Photo (Id = {photoId}) not found.");
        }

        return photo.Owner.Uid;
    }

    public async Task<Guid> GetOwnerByLocationId(Guid locationId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var client = db.Clients.FirstOrDefault(x => x.Uid == locationId);
        var animal = db.Animals.FirstOrDefault(x => x.Uid == locationId);
        var exposition = db.Expositions.FirstOrDefault(x => x.Uid == locationId);

        if (client != null)
        {
            return client.Uid;
        }
        else if (animal != null)
        {
            return await GetClientIdByAnimalId(animal.Uid);
        }
        else if (exposition != null)
        {
            return await GetClientIdByExpositionId(exposition.Uid);
        }
        else
        {
            throw new ProcessException($"Location (Id = {locationId}) not found.");
        }
    }

    public async Task<Guid> GetOwnerByNotificationId(Guid notificationId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var notification = db.Notifications
            .Include(x => x.Sender)
            .FirstOrDefault(x => x.Uid == notificationId);

        if (notification == null)
        {
            throw new ProcessException($"Notification (Id = {notificationId}) not found.");
        }

        return await GetClientIdByExpositionId(notification.Sender.Uid);
    }
}
