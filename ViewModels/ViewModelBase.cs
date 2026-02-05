using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using DTExplorer.Services;
using System.Linq;
using System.Threading.Tasks;

namespace DTExplorer.ViewModels;

public class ViewModelBase : ObservableObject
{
	public ViewModelBase()
	{
		MessageService = Ioc.Default.GetService<MessageService>()!;
	}
	public MessageService MessageService { get; private set; }
	
	#region Notification message
	public void NotificationInfomation(string title, string message)
		 => MessageService.NotificationInfomation(title, message);
	public void NotificationSuccess(string title, string message)
		=> MessageService.NotificationSuccess(title, message);
	public void NotificationWarning(string title, string message)
		=> MessageService.NotificationWarning(title, message);
	public void NotificationError(string title, string message)
		=> MessageService.NotificationError(title, message);
	#endregion

	#region Status
	public void WorkStartStatusMessage(string message)
	{
		MessageService.WorkStartStatusMessage(message);
	}
	public void WorkEndStatusMessage(string message)
	{
		MessageService.WorkEndStatusMessage(message);
	}
	public void ShowStatusReady()
	{
		MessageService.ShowStatusNormalMsg("Ready");
	}
	public void ShowStatusNormalMsg(string message)
	{
		MessageService.ShowStatusNormalMsg(message);
	}
	#endregion

	#region File/Folder Dialog Service
	public async ValueTask<string> OpenFilePickerAsync(Control control, string title = "")
	{
		var topLevel = TopLevel.GetTopLevel(control);
		if (topLevel == null) {
			return string.Empty;
		}
		var files = await topLevel.StorageProvider.OpenFilePickerAsync(
						new FilePickerOpenOptions()
						{
							AllowMultiple = false,
							Title = title,
						}).ConfigureAwait(false);
		if (files is null || files.Count == 0) {
			return string.Empty;
		}
		return files[0].Path.LocalPath ?? string.Empty;
	}
	public async ValueTask<string[]?> OpenFilesPickerAsync(Control control, string title = "")
	{
		var topLevel = TopLevel.GetTopLevel(control);
		if (topLevel == null) {
			return null;
		}
		var files = await topLevel.StorageProvider.OpenFilePickerAsync(
						new FilePickerOpenOptions()
						{
							AllowMultiple = true,
							Title = title,
						}).ConfigureAwait(false);
		return files?.Select(x => x.Path.LocalPath)?.ToArray();
	}

	public async ValueTask<string> OpenFolderPickerAsync(Control control, string title = "")
	{
		var topLevel = TopLevel.GetTopLevel(control);
		if (topLevel == null) {
			return string.Empty;
		}
		var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(
						new FolderPickerOpenOptions()
						{
							AllowMultiple = false,
							Title = title,
						}).ConfigureAwait(false);
		if (folders is null || folders.Count == 0) {
			return string.Empty;
		}
		return folders[0].Path.LocalPath! ?? string.Empty;
	}
	public async ValueTask<string[]?> OpenFoldersPickerAsync(Control control, string title = "")
	{
		var topLevel = TopLevel.GetTopLevel(control);
		if (topLevel == null) {
			return null;
		}
		var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(
						new FolderPickerOpenOptions()
						{
							AllowMultiple = true,
							Title = title,
						}).ConfigureAwait(false);
		return folders?.Select(x => x.Path.LocalPath)?.ToArray();
	}
	#endregion
}
