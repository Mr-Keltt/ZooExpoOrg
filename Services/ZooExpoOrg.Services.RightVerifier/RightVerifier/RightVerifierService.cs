using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;

namespace ZooExpoOrg.Services.RightVerifier;

public class RightVerifierService : IRightVerifierService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public RightVerifierService(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
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

        var clienId = await GetUserId(jwtToken);

        var client = await db.Clients.FirstOrDefaultAsync(x =>  x.UserId == clienId);

        if (client == null)
        {
            throw new ProcessException($"Client (By jwt = {jwtToken}) not found.");
        }

        return client.Uid;
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

    public async Task<bool> VerifRightsOfManagAnimal(string jwtToken, Guid animalId)
    {
        if ((await GetClientId(jwtToken)) == (await GetClientIdByAnimalId(animalId))) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> VerifRightsOfManagAchievement(string jwtToken, Guid achievementId)
    {
        var db = await dbContextFactory.CreateDbContextAsync();

        var achievement = await db.Achievements.FirstOrDefaultAsync(x => x.Uid == achievementId);

        if (achievement == null)
        {
            throw new ProcessException($"Achievement (By jwt = {achievementId}) not found.");
        }

        var animal = await db.Animals.FirstOrDefaultAsync(x => x.Id == achievement.AnimalId);

        return await VerifRightsOfManagAnimal(jwtToken, animal.Uid);
    }
}