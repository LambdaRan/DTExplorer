using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.DependencyInjection;
using DTExplorer.Services;
using DTExplorer.ViewModels;

namespace DTExplorer.Views
{
    public partial class MainWindow : Window
	{
        public MainWindow()
        {
			DataContext = Ioc.Default.GetRequiredService<MainWindowViewModel>();
			Loaded += OnWindowLoaded;
			Unloaded += OnWindowUnloaded;
			InitializeComponent();
			NotificationManager = new WindowNotificationManager(TopLevel.GetTopLevel(this))
			{
				Position = NotificationPosition.TopCenter,
			};
		}

        public MainWindowViewModel? ViewModel => DataContext as MainWindowViewModel;
		public WindowNotificationManager NotificationManager { get; private set; }

		private void OnWindowLoaded(object? sender, RoutedEventArgs e)
		{
			ViewModel?.MessageService.Register<MainWindow, StatusMessage>(this, (r, m) => {
				Dispatcher.UIThread.Post(() => {
					r.ViewModel?.StatusMessage = m.Status;
				});
			});
			ViewModel?.MessageService.Register<MainWindow, Notification>(this, (r, m) => {
				r.NotificationManager?.Show(m);
			});
		}
		private void OnWindowUnloaded(object? sender, RoutedEventArgs e)
		{
			ViewModel?.MessageService.UnregisterAll(this);
		}
	}
}