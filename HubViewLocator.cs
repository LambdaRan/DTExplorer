using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using DTExplorer.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DTExplorer;

public class HubViewLocator
{
	private static readonly Dictionary<Type, Type> _ViewModelViewMap = new();
	public static void AddView<TViewModel, TView>()
		where TViewModel : ObservableObject
		where TView : ContentControl
	{
		if (!_ViewModelViewMap.TryAdd(typeof(TViewModel), typeof(TView))) {
			throw new InvalidOperationException($"View for {typeof(TViewModel).FullName} is already registered.");
		}
	}
	public static object GetViewInstance<TViewModel>()
		where TViewModel : ObservableObject
	{
		return GetViewInstance(typeof(TViewModel));
	}
	public static object GetViewInstance(Type viewModel)
	{
		if (viewModel == null) {
			return CreateText("ViewModel was null!");
		}
		if (_ViewModelViewMap.TryGetValue(viewModel, out Type? viewType)) {
			return Ioc.Default.GetService(viewType) ?? CreateText($"View not found for '{viewModel.FullName}'");
		}
		return CreateText($"Viewmodel not found for '{viewModel.FullName}'");
	}
	private static TextBlock CreateText(string text) => new TextBlock { Text = text };
}

public static class HubViewLocatorServiceCollectionExtensions
{
	public static IServiceCollection AddTabView<TViewModel, TView>(this IServiceCollection services)
		where TViewModel : TabItemViewModel
		where TView : ContentControl
	{
		services.AddTransient<TViewModel>();
		services.AddTransient<TView>();
		HubViewLocator.AddView<TViewModel, TView>();
		return services;
	}

	public static IServiceCollection AddNormalView<TViewModel, TView>(this IServiceCollection services)
		where TViewModel : ViewModelBase
		where TView : ContentControl
	{
		services.AddTransient<TViewModel>();
		services.AddTransient<TView>();
		HubViewLocator.AddView<TViewModel, TView>();
		return services;
	}
}
