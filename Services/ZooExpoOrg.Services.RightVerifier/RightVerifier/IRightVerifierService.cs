namespace ZooExpoOrg.Services.RightVerifier;

public interface IRightVerifierService
{
    Task<Guid> GetClientId(string jwtToken);

    Task<bool> VerifiRightToAnAnimal(string jwtToken, Guid animalId);
}