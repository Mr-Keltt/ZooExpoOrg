﻿using Microsoft.EntityFrameworkCore;
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

    public RightVerifierHelper(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public bool EqualsClientId(Guid jwtClientId, Guid requestClientId)
    {
        if (jwtClientId == requestClientId)
        {
            return true;
        }
        else
        {
            return false;
        }
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

        var client = await db.Clients.FirstOrDefaultAsync(x => x.UserId == clienId);

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

    public async Task<Guid> GetClientIdByExpositionsId(Guid expositionId)
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
}
