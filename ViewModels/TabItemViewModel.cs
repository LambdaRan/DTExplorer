using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;

namespace DTExplorer.ViewModels;

public partial class TabItemViewModel: ViewModelBase
{
	[ObservableProperty]
	private string _Header = "New";

	public IconSource? Icon { get; set; }

	public bool IsClosable { get; set; }
}
