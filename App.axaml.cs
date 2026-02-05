using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.DependencyInjection;
using DTExplorer.Services;
using DTExplorer.ViewModels;
using DTExplorer.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DTExplorer
{
    public partial class App : Application
    {
		private static readonly ServiceCollection _serviceCollection = new ServiceCollection();
		public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
		}

        public override void OnFrameworkInitializationCompleted()
        {
            Name = "DTExplorer";
            InitApp();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
        public void InitApp()
        {
            _serviceCollection.AddSingleton<MessageService>();

            _serviceCollection.AddNormalView<MainWindowViewModel, MainWindow>();
            _serviceCollection.AddTabView<DdsViewModel, DdsView>();
            _serviceCollection.AddTabView<TgaViewModel, TgaView>();
            _serviceCollection.AddTabView<EmptyViewModel, EmptyView>();

			Ioc.Default.ConfigureServices(_serviceCollection.BuildServiceProvider());
		}
	}
}