using System.Net.Http;
using System.Net.Http.Json;
using ZooExpoOrg.Web.Services.Clients;
using ZooExpoOrg.Web.Services.Expositions;
using ZooExpoOrg.Web.Services.GetRsultHelper;

namespace ZooExpoOrg.Web.Services.Achievements;

public class AchievementService : IAchievementService
{
    private readonly HttpClient httpClient;

    public AchievementService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<GetModelResult<List<VueAchievementModel>>> GetAchievementsOwned(Guid ownerId)
    {
        var response = await httpClient.GetAsync($"v1/achievement/owned/{ownerId}");

        var getResultHelper = new GetResultHelper<List<VueAchievementModel>>();

        return await getResultHelper.GetGetModelResult(response, "Achievements");
    }

    public async Task<GetModelResult<VueAchievementModel>> GetAchievement(Guid achievementId)
    {
        var response = await httpClient.GetAsync($"v1/achievement/{achievementId}");

        var getResultHelper = new GetResultHelper<VueAchievementModel>();

        return await getResultHelper.GetGetModelResult(response, "Achievement");
    }

    public async Task<ManageModelResult<VueAchievementModel>> AddAchievement(VueCreateAchievementModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/achievemen", requestContent);

        var getResultHelper = new GetResultHelper<VueAchievementModel>();

        return await getResultHelper.GetManageModelResult(response, "Achievement add");
    }

    public async Task<DeleteModelResult> DeleteAchievement(Guid achievementId)
    {
        var response = await httpClient.DeleteAsync($"v1/achievemen/{achievementId}");

        var getResultHelper = new GetResultHelper<VueAchievementModel>();

        return await getResultHelper.GetDeleteModelResult(response, "Achievement");
    }
}