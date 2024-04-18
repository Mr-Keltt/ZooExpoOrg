namespace ZooExpoOrg.Services.RightVerifier;

public interface IRightVerifierService
{
    Task<bool> VerifRightsOfCreateAchievement(string jwtToken, Guid animalId);
    Task<bool> VerifRightsOfManagAchievement(string jwtToken, Guid achievementId);
    Task<bool> VerifRightsOfCreateAnimal(string jwtToken, Guid clientId);
    Task<bool> VerifRightsOfManagAnimal(string jwtToken, Guid animalId);
}