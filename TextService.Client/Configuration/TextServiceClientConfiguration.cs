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

        public static IServiceCollection AddTextServiceTokenClient(this IServiceCollection services, IConfiguration configuration)
        {
            //var toksettingen = new ApiSettings
            //{
            //    Token =
            //    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMTQ4NTk1NjItMmFhMC00ZmFjLTlkZjAtYmNmMTY2MGFlZGM0IiwianRpIjoiZDQ5ZGEwZDEtOGMwOC00ZGEzLWFmNDctZWZiYzM3MDVmODM0IiwiZXhwIjoxNjIxMTg1ODUwLCJpc3MiOiJhdXRoZW50aWNhdGlvbnNlcnZpY2UiLCJhdWQiOiJhdXRoZW50aWNhdGlvbnNlcnZpY2UifQ.Z9oSqZH-jnpRXoZiBeoPqT-u2yxvcU3wy14VIJGHcQ0"
            //};
            //var rs = new RefitSettings()
            //{
            //    //AuthorizationHeaderValueGetter = () => Task.FromResult(toksettingen.Token)
            //};

            services.AddApiClient<ITextClient>(configuration, new RefitSettings(), "ServiceUrls:TextService");

            return services;
        }
    }
}
