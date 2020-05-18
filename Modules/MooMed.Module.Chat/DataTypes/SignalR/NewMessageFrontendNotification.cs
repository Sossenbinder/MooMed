using System.Diagnostics.CodeAnalysis;

namespace MooMed.Module.Chat.DataTypes.SignalR
{
	public class NewMessageFrontendNotification
	{
		public int SenderId { get; }

		[NotNull]
		public string Message { get; }

		public NewMessageFrontendNotification(int senderId, [NotNull] string message)
		{
			SenderId = senderId;
			Message = message;
		}
	}
}
