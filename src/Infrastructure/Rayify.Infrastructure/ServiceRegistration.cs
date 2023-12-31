﻿using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Rayify.Application.Abstractions.ConvertVideo;
using Rayify.Application.Abstractions.ICronJob;
using Rayify.Application.Abstractions.Music;
using Rayify.Application.Abstractions.Storage.Main;
using Rayify.Application.Abstractions.Token;
using Rayify.Application.Abstractions.YoutubeDownload;
using Rayify.Infrastructure.Services.ConvertVideo;
using Rayify.Infrastructure.Services.CronJob;
using Rayify.Infrastructure.Services.Music;
using Rayify.Infrastructure.Services.Storage;
using Rayify.Infrastructure.Services.Token;
using Rayify.Infrastructure.Services.YoutubeDownload;

namespace Rayify.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IMusic, MusicService>();
            services.AddScoped<IYoutubeDownload, YoutubeDownload>();
            services.AddScoped<IConvertVideo, ConvertVideo>();
            services.AddQuartz(o =>
            {
                o.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = JobKey.Create(nameof(CronJob));
                o.AddJob<CronJob>(jobKey);
                o.AddTrigger(trigger => 
                    trigger.ForJob(jobKey)
                    .WithCronSchedule("0 0 00 1/1 * ? *") // her gece 12 de 
                );
            });
            services.AddQuartzHostedService(o =>
            {
                o.WaitForJobsToComplete = true;
            });
            
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}   
