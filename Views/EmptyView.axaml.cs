using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using DTExplorer.ViewModels;

namespace DTExplorer;

public partial class EmptyView : UserControl
{
    public EmptyView()
    {
        DataContext = Ioc.Default.GetService<EmptyViewModel>();
		InitializeComponent();

        ViewModel?.MessageService.NotificationInfomation("Info", "EmptyView Initialize");
	}

    public EmptyViewModel? ViewModel => DataContext as EmptyViewModel;
}