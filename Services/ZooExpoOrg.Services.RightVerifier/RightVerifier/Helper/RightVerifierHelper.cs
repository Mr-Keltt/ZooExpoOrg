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
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

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

        var achievement = await db.Achievements.FirstOrDefaultAsync(x => x.Uid == achievementId);

        if (achievement == null)
        {
            throw new ProcessException($"Achievement (Id = {achievementId}) not found.");
        }

        var animal = await db.Animals.FirstOrDefaultAsync(x => x.Id == achievement.AnimalId);

        return await GetClientIdByAnimalId(animal.Uid);
    }

    public async Task<Guid> GetClientIdByAnimalId(Guid animalId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var animal = await db.Animals.FirstOrDefaultAsync(x => x.Uid == animalId);

        if (animal == null)
        {
            throw new ProcessException($"Animal (Id = {animalId}) not found.");
        }

        var client = await db.Clients.FirstOrDefaultAsync(x => x.Id == animal.OwnerId);

        return client.Uid;
    }

    public async Task<Guid> GetClientIdByCommentId(Guid commentId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var comment = await db.Comments.FirstOrDefaultAsync(x => x.Uid == commentId);

        if (comment == null)
        {
            throw new ProcessException($"Comment (Id = {commentId}) not found.");
        }

        var client = await db.Clients.FirstOrDefaultAsync(x => x.Id == comment.AuthorId);

        return client.Uid;
    }

    public async Task<Guid> GetClientIdByExpositionId(Guid expositionId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var exposition = await db.Expositions.FirstOrDefaultAsync(x => x.Uid == expositionId);

        if (exposition == null)
        {
            throw new ProcessException($"Exposition (Id = {expositionId}) not found.");
        }

        var client = await db.Clients.FirstOrDefaultAsync(x => x.Id == exposition.OrganizerId);

        return client.Uid;
    }

    public async Task<Guid> GetClientIdByPhotoId(Guid photoId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var photo = await db.Photos.FirstOrDefaultAsync(x => x.Uid == photoId);

        if (photo == null)
        {
            throw new ProcessException($"Photo (Id = {photoId}) not found.");
        }

        var client = await db.Clients.FirstOrDefaultAsync(x => x.Id == photo.OwnerId);

        return client.Uid;
    }

    public async Task<Guid> GetOwnerByLocationId(Guid LocationId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var client = db.Clients.FirstOrDefault(x => x.Uid == LocationId);
        var animal = db.Animals.FirstOrDefault(x => x.Uid == LocationId);
        var exposition = db.Expositions.FirstOrDefault(x => x.Uid == LocationId);

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
            throw new ProcessException($"Location (Id = {LocationId}) not found.");
        }
    }
}
