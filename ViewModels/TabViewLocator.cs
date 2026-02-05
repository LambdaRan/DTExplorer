using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace DTExplorer.ViewModels;

public class TabViewLocator : IDataTemplate
{
	public Control Build(object? param)
	{
		if (param is null) {
			return CreateText("Data is null.");
		}
		var control = HubViewLocator.GetViewInstance(param.GetType()) as Control;
		control?.DataContext = param;
		return control ?? CreateText($"Not Find View:{param.GetType().FullName}");
	}

	public bool Match(object? data) => data is TabItemViewModel;

	private static TextBlock CreateText(string text) => new TextBlock { Text = text };
}