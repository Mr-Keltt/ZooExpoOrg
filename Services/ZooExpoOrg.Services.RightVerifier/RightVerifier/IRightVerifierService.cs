namespace ZooExpoOrg.Services.RightVerifier;

public interface IRightVerifierService
{
    Task<bool> VerifRightsOfCreateAchievement(string jwtToken, Guid animalId);
    Task<bool> VerifRightsOfManagAchievement(string jwtToken, Guid achievementId);
    Task<bool> VerifRightsOfCreateAnimal(string jwtToken, Guid clientId);
    Task<bool> VerifRightsOfManagAnimal(string jwtToken, Guid animalId);
    Task<bool> VerifRightsOfCreateClient(string jwtToken, Guid userId);
    Task<bool> VerifRightsOfManagClient(string jwtToken, Guid clientId);
    Task<bool> VerifRightsOfCreateComment(string jwtToken, Guid userId);
    Task<bool> VerifRightsOfManagComment(string jwtToken, Guid clientId);
    Task<bool> VerifRightsOfCreateExposition(string jwtToken, Guid clientId);
    Task<bool> VerifRightsOfManagExposition(string jwtToken, Guid expositionId);
    Task<bool> VerifRightsOfCreatePhoto(string jwtToken, Guid clientId, Guid LocationId);
    Task<bool> VerifRightsOfManagPhoto(string jwtToken, Guid clientId);
}