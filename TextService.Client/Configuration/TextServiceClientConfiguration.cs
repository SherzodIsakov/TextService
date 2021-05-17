﻿using AuthenticationBase;
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
            services.AddApiClient<ITextClient>(configuration, new RefitSettings(), "ServiceUrls:TextService");

            return services;
        }
    }
}
