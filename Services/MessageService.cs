using Avalonia.Controls.Notifications;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;

namespace DTExplorer.Services;

public sealed class MessageService
{
	private Stopwatch _stopwatch = new Stopwatch();
	public MessageService()
	{
		Messenger = WeakReferenceMessenger.Default;
	}
	private WeakReferenceMessenger Messenger { get; }

	public void Register<TRecipient, TMessage>(TRecipient recipient, MessageHandler<TRecipient, TMessage> handler)
		where TRecipient : class
		where TMessage : class
	{
		Guard.IsNotNull(recipient);
		Guard.IsNotNull(handler);
		if (!Messenger.IsRegistered<TMessage>(recipient)) {
			Messenger.Register(recipient, handler);
		}
	}
	public void Unregister<TMessage>(object recipient)
		where TMessage : class
	{
		Guard.IsNotNull(recipient);
		Messenger.Unregister<TMessage>(recipient);
	}
	public void UnregisterAll(object recipient)
	{
		Guard.IsNotNull(recipient);
		Messenger.UnregisterAll(recipient);
	}

	public TMessage Send<TMessage>(TMessage message)
		where TMessage : class
	{
		Guard.IsNotNull(message);
		return Messenger.Send(message);
	}


	#region StatuMessage
	public void WorkStartStatusMessage(string message)
	{
		ShowStatusNormalMsg(message);
		_stopwatch.Reset();
		_stopwatch.Start();
	}
	public string WorkEndStatusMessage(string message)
	{
		_stopwatch.Stop();
		var msg = $"{message} (用时 {_stopwatch.Elapsed.TotalSeconds:#0.000} 秒)";
		ShowStatusNormalMsg(msg);
		return msg;
	}
	public void ShowStatusNormalMsg(string message)
	{
		Messenger.Send(StatusMessage.Normal(message));
	}
	public void ShowStatusErrorMsg(string message)
	{
		Messenger.Send(StatusMessage.Error(message));
	}
	#endregion

	#region NotificationMessage
	public void NotificationInfomation(string title, string message)
	{
		Messenger.Send(new Notification(title, message, NotificationType.Information));
	}
	public void NotificationSuccess(string title, string message)
	{
		Messenger.Send(new Notification(title, message, NotificationType.Success));
	}
	public void NotificationWarning(string title, string message)
	{
		Messenger.Send(new Notification(title, message, NotificationType.Warning));
	}
	public void NotificationError(string title, string message)
	{
		Messenger.Send(new Notification(title, message, NotificationType.Error));
	}
	#endregion
}
