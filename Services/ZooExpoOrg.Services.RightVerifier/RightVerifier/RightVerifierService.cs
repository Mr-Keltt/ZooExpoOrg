using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using ZooExpoOrg.Common.Exceptions;
using ZooExpoOrg.Context;
using ZooExpoOrg.Services.RightVerifier.Helper;

namespace ZooExpoOrg.Services.RightVerifier;

public class RightVerifierService : IRightVerifierService
{
    private readonly RightVerifierHelper helper;

    public RightVerifierService(
        IDbContextFactory<MainDbContext> dbContextFactory,
        DbSettings dbSettings)
    {
        this.helper = new RightVerifierHelper(dbContextFactory, dbSettings);
    }

    private async Task<bool> VerifAdminRights(string jwtToken)
    {
        return helper.EqualsId(
            (await helper.GetUserId(jwtToken)), 
            (await helper.GetAdminId())
            );
    }

    public async Task<bool> VerifRightsOfCreateAchievement(string jwtToken, Guid animalId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);
        Guid requestClientId = await helper.GetClientIdByAnimalId(animalId);

        return helper.EqualsId(jwtClientId, requestClientId);
    }

    public async Task<bool> VerifRightsOfManagAchievement(string jwtToken, Guid achievementId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);
        Guid requestClientId = await helper.GetClientIdByAchievementId(achievementId);

        return helper.EqualsId(jwtClientId, requestClientId);
    }

    public async Task<bool> VerifRightsOfCreateAnimal(string jwtToken, Guid clientId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);

        return helper.EqualsId(jwtClientId, clientId);
    }

    public async Task<bool> VerifRightsOfManagAnimal(string jwtToken, Guid animalId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);
        Guid requestClientId = await helper.GetClientIdByAnimalId(animalId);

        return helper.EqualsId(jwtClientId, requestClientId);
    }

    public async Task<bool> VerifRightsOfCreateClient(string jwtToken, Guid userId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtUserId = await helper.GetUserId(jwtToken);

        return helper.EqualsId(jwtUserId, userId);
    }

    public async Task<bool> VerifRightsOfManagClient(string jwtToken, Guid clientId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);

        return helper.EqualsId(jwtClientId, clientId);
    }

    public async Task<bool> VerifRightsOfCreateComment(string jwtToken, Guid clientId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);

        return helper.EqualsId(jwtClientId, clientId);
    }

    public async Task<bool> VerifRightsOfManagComment(string jwtToken, Guid commentId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);
        Guid requestClientId = await helper.GetClientIdByCommentId(commentId);

        return helper.EqualsId(jwtClientId, requestClientId);
    }

    public async Task<bool> VerifRightsOfCreateExposition(string jwtToken, Guid clientId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);

        return helper.EqualsId(jwtClientId, clientId);
    }

    public async Task<bool> VerifRightsOfManagExposition(string jwtToken, Guid expositionId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);
        Guid requestClientId = await helper.GetClientIdByExpositionId(expositionId);

        return helper.EqualsId(jwtClientId, requestClientId);
    }

    public async Task<bool> VerifRightsOfCreatePhoto(string jwtToken, Guid clientId, Guid LocationId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);
        Guid requestOwnerId = await helper.GetOwnerByLocationId(LocationId);

        return 
            helper.EqualsId(jwtClientId, clientId) &&
            helper.EqualsId(jwtClientId, requestOwnerId);
    }

    public async Task<bool> VerifRightsOfManagPhoto(string jwtToken, Guid photoId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);
        Guid requestClientId = await helper.GetClientIdByPhotoId(photoId);

        return helper.EqualsId(jwtClientId, requestClientId);
    }

    public async Task<bool> VerifRightsOfCreateNotification(string jwtToken, Guid expositionId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);
        Guid requestClientId = await helper.GetClientIdByExpositionId(expositionId);

        return helper.EqualsId(jwtClientId, requestClientId);
    }

    public async Task<bool> VerifRightsOfManagNotification(string jwtToken, Guid notificationId)
    {
        if (await VerifAdminRights(jwtToken))
        {
            return true;
        }

        Guid jwtClientId = await helper.GetClientId(jwtToken);
        Guid requestClientId = await helper.GetOwnerByNotificationId(notificationId);

        return helper.EqualsId(jwtClientId, requestClientId);
    }
}