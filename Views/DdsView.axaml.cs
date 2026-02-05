using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using DTExplorer.ViewModels;

namespace DTExplorer;

public partial class DdsView : UserControl
{
    public DdsView()
    {
        DataContext = Ioc.Default.GetRequiredService<DdsViewModel>();
		InitializeComponent();

        ViewModel?.NotificationInfomation("Info", "DdsView Initialize");
        ViewModel?.ShowStatusReady();
	}

    public DdsViewModel? ViewModel => DataContext as DdsViewModel;
}