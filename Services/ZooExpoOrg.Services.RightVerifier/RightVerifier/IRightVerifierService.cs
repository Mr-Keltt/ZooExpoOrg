namespace ZooExpoOrg.Services.RightVerifier;

public interface IRightVerifierService
{
    Task<Guid> GetUserId(string jwtToken);
    Task<Guid> GetClientId(string jwtToken);
    Task<Guid> GetClientIdByAnimalId(Guid animalId);
    Task<bool> VerifRightsOfManagAnimal(string jwtToken, Guid animalId);
    Task<bool> VerifRightsOfManagAchievement(string jwtToken, Guid achievementId);
}