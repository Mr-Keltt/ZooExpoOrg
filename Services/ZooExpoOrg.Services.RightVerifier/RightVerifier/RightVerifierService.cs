using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Services.RightVerifier.Helper;

namespace ZooExpoOrg.Services.RightVerifier;

public class RightVerifierService : IRightVerifierService
{
    private readonly RightVerifierHelper helper;

    public RightVerifierService(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.helper = new RightVerifierHelper(dbContextFactory);
    }

    public async Task<bool> VerifRightsOfCreateAchievement(string jwtToken, Guid animalId)
    {
        return await VerifRightsOfManagAnimal(jwtToken, animalId);
    }

    public async Task<bool> VerifRightsOfManagAchievement(string jwtToken, Guid achievementId)
    {
        Guid jwtClientId = await helper.GetClientId(jwtToken);
        Guid requestClientId = await helper.GetClientIdByAchievementId(achievementId);

        return helper.EqualsClientId(jwtClientId, requestClientId);
    }

    public async Task<bool> VerifRightsOfCreateAnimal(string jwtToken, Guid clientId)
    {
        Guid jwtClientId = await helper.GetClientId(jwtToken);

        return helper.EqualsClientId(jwtClientId, clientId);
    }

    public async Task<bool> VerifRightsOfManagAnimal(string jwtToken, Guid animalId)
    {
        Guid jwtClientId = await helper.GetClientId(jwtToken);
        Guid requestClientId = await helper.GetClientIdByAnimalId(animalId);

        return helper.EqualsClientId(jwtClientId, requestClientId);
    }

    public async Task<bool> VerifRightsOfCreateClient(string jwtToken, Guid userId)
    {
        Guid jwtUserId = await helper.GetUserId(jwtToken);

        return helper.EqualsClientId(jwtUserId, userId);
    }

    public async Task<bool> VerifRightsOfManagClient(string jwtToken, Guid clientId)
    {
        Guid jwtClientId = await helper.GetClientId(jwtToken);

        return helper.EqualsClientId(jwtClientId, clientId);
    }
}