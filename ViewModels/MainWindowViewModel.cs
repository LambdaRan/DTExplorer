using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DTExplorer.ViewModels;
public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    [ObservableProperty]
    private string _StatusMessage = string.Empty;


    [ObservableProperty]
    private List<TabItemViewModel> _Documents = new() {
			new EmptyViewModel() {
				Header = "New",
				IsClosable = false,
			},
			new DdsViewModel() {
            Header = "DDS Viewer",
            IsClosable = true,
        },
        new TgaViewModel() {
            Header = "Tga Viewer",
            IsClosable = true,
        },
	};

    [ObservableProperty]
    private TabItemViewModel? _SelectedTabBarItem;


    [RelayCommand]
    private async Task NewDocument()
    {
		WorkStartStatusMessage("New Document start");
		await Task.Delay(TimeSpan.FromSeconds(2));
        WorkEndStatusMessage("New Document end");
	}
}
