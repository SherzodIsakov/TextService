using AuthenticationBase;
using AuthenticationBase.Extensions;
using AuthenticationBase.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TextService.Client.Configuration
{
    public static class TextServiceClientConfiguration
    {
        //Работа без авторизации
        public static IServiceCollection AddTextServiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient(_ => RestService.For<ITextClient>(
                new HttpClient
                (
                    new HttpClientHandler { ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true }
                )
                {
                    BaseAddress = new Uri(configuration["ServiceUrls:TextService"]),
                    Timeout = TimeSpan.FromMinutes(5)
                }));

            return services;
        }

        //Работа с токеном
        public static IServiceCollection AddTextServiceTokenClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiClient<ITextClient>(configuration, new RefitSettings(), "ServiceUrls:TextService");

            return services;
        }

        //Получение токена из appsettings
        public static IServiceCollection AddTextServiceGetTokenClient(this IServiceCollection services, IConfiguration configuration)
        {
            var refitSettings = new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(configuration["Token"])
            };
            services.AddApiClient<ITextClient>(configuration, refitSettings, "ServiceUrls:TextService");

            return services;
        }
    }
}
