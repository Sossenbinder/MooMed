﻿using Autofac;
using JetBrains.Annotations;
using MooMed.Core.Code.Configuration;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Helper.Crypto;
using MooMed.Core.Code.Helper.Crypto.Interface;
using MooMed.Core.Code.Helper.Email;
using MooMed.Core.Code.Helper.Email.Interface;
using MooMed.Core.Code.Logging.Loggers;
using MooMed.Core.Code.Logging.Loggers.Interface;
using MooMed.Core.Code.Logging.LogManagement;
using MooMed.Core.Code.Logging.LogManagement.Interface;

namespace MooMed.Core
{
    public class CoreModule : Module
    {
        protected override void Load([NotNull] ContainerBuilder builder)
        {
	        builder.RegisterType<Config>()
                .As<IConfig>()
                .SingleInstance();

	        builder.RegisterType<MainConfigSettingsProvider>()
		        .As<IConfigSettingsProvider>()
		        .SingleInstance();

            builder.RegisterType<SettingsCrypto>()
                .As<ISettingsCrypto>()
                .SingleInstance();

            builder.RegisterType<SettingsCryptoProvider>()
	            .As<ICryptoProvider>()
	            .SingleInstance();

            builder.RegisterType<MainLogger>()
                .As<IMainLogger>()
                .SingleInstance();

            builder.RegisterType<StaticLogger>()
	            .AsSelf()
	            .AutoActivate()
	            .SingleInstance();

            builder.RegisterType<LogFileManager>()
                .As<LogFileManager>()
                .SingleInstance();

            builder.RegisterType<LogPathProvider>()
                .As<ILogPathProvider>()
                .SingleInstance();

            builder.RegisterType<EmailManager>()
                .As<IEmailManager>()
                .SingleInstance();
        }
    }
}
