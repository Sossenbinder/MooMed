using System;
using Autofac;
using MooMed.Common.Database.Context;
using MooMed.Common.Database.Converter;
using MooMed.Common.Definitions.Models.Chat;
using MooMed.Common.Definitions.UiModels.Chat;
using MooMed.Module.Chat.Converters;
using MooMed.Module.Chat.Database;
using MooMed.Module.Chat.DataTypes.Entity;
using MooMed.Module.Chat.Helper;
using MooMed.Module.Chat.Repository;
using MooMed.Module.Chat.Repository.Interface;
using MooMed.Module.Chat.Service;
using MooMed.Module.Chat.Service.Interface;

namespace MooMed.Module.Chat.Module
{
	public class InternalChatModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<ChatDbContextFactory>()
				.As<ChatDbContextFactory, AbstractDbContextFactory<ChatDbContext>>()
				.SingleInstance();

			builder.RegisterType<ChatMessageRepository>()
				.As<IChatMessageRepository>()
				.SingleInstance();

			builder.RegisterType<MessageService>()
				.As<IMessageService>()
				.SingleInstance();

			builder.RegisterType<ChatMessageDbConverter>()
				.As<IBiDirectionalDbConverter<ChatMessage, ChatMessageEntity, Guid>>()
				.SingleInstance();

			builder.RegisterType<ChatMessageEncodingHelper>()
				.AsSelf()
				.SingleInstance();

			builder.RegisterType<ChatMessageModelToUiConverter>()
				.As<IModelToUiModelConverter<ChatMessage, ChatMessageUiModel>>()
				.SingleInstance();
		}
	}
}