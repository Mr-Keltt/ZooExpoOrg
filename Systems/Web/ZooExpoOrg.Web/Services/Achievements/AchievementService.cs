using System.Net.Http;
using System.Net.Http.Json;
using ZooExpoOrg.Web.Services.Expositions;

namespace ZooExpoOrg.Web.Services.Achievements;

public class AchievementService : IAchievementService
{
    private readonly HttpClient httpClient;

    public AchievementService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<VueAchievementModel>> GetAchievementsOwned(Guid ownerId)
    {
        var response = await httpClient.GetAsync($"v1/achievement/owned/{ownerId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<VueAchievementModel>>() ?? new List<VueAchievementModel>();
    }

    public async Task<VueAchievementModel> GetAchievement(Guid achievementId)
    {
        var response = await httpClient.GetAsync($"v1/achievement/{achievementId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<VueAchievementModel>() ?? new();
    }

    public async Task AddAchievement(VueCreateAchievementModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/achievemen", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteAchievement(Guid achievementId)
    {
        var response = await httpClient.DeleteAsync($"v1/achievemen/{achievementId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}