
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;
using System.Text.Json;
using ZooExpoOrg.Web.Services.Accounts;
using ZooExpoOrg.Web.Services.GetRsultHelper.Models;

namespace ZooExpoOrg.Web.Services.GetRsultHelper;

public class GetResultHelper<T> where T : new()
{
    public async Task<GetModelResult<T>> GetGetModelResult(HttpResponseMessage response, string modelName)
    {
        var getModelResult = new GetModelResult<T>();

        if (!response.IsSuccessStatusCode)
        {
            getModelResult.Successful = false;
            getModelResult.ErrorMesage = $"{modelName} not found.";
            return getModelResult;
        }

        getModelResult.Successful = true;
        getModelResult.Result = await response.Content.ReadFromJsonAsync<T>() ?? new T();

        return getModelResult;
    }

    public async Task<ManageModelResult<T>> GetManageModelResult(HttpResponseMessage response, string perationName)
    {
        var manageModelResult = new ManageModelResult<T>();

        if (response.IsSuccessStatusCode)
        {
            manageModelResult.Successful = true;

            return manageModelResult;
        }

        var res = await response.Content.ReadAsStringAsync();
        Console.WriteLine(res);

        manageModelResult.FieldErrors = await response.Content.ReadFromJsonAsync<List<ManageModelError>>() ?? new List<ManageModelError>();

        if (manageModelResult.FieldErrors.IsNullOrEmpty())
        {
            manageModelResult = await response.Content.ReadFromJsonAsync<ManageModelResult<T>>() ?? new ManageModelResult<T>();

            for (int i = 0; i < manageModelResult.FieldErrors.Count; i++)
            {
                manageModelResult.FieldErrors[i].ErrorMessage = manageModelResult.FieldErrors[i].Message;
            }
        }

        manageModelResult.Successful = false;
        manageModelResult.ErrorMesage = $"{perationName} error.";

        return manageModelResult;
    }

    public async Task<DeleteModelResult> GetDeleteModelResult(HttpResponseMessage response, string modelName)
    {
        var deleteModelResult = new DeleteModelResult();

        if (!response.IsSuccessStatusCode)
        {
            deleteModelResult.Successful = false;
            deleteModelResult.ErrorMesage = $"{modelName} not found.";

            return deleteModelResult;
        }

        deleteModelResult.Successful = true;

        return deleteModelResult;
    }
}