namespace ZooExpoOrg.Api.Configuration;

using ZooExpoOrg.Common.Helpers;
using ZooExpoOrg.Common.Validator;
using FluentValidation.AspNetCore;

public static class ValidatorConfiguration
{
    public static IServiceCollection AddAppValidator(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(opt => { opt.DisableDataAnnotationsValidation = true; });

        ValidatorsRegisterHelper.Register(services);

        services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return services;
    }
}