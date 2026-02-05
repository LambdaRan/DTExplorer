using Avalonia.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using DTExplorer.ViewModels;

namespace DTExplorer;

public partial class TgaView : UserControl
{
    public TgaView()
    {
        DataContext = Ioc.Default.GetService<TgaViewModel>();
		InitializeComponent();
        ViewModel?.MessageService.NotificationInfomation("Info", "TgaView Initialize");
	}

    public TgaViewModel? ViewModel => DataContext as TgaViewModel;
}