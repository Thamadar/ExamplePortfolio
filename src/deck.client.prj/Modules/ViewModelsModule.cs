using Autofac; 

using Deck.Client.Views;
using Deck.Client.Views.Panels; 

namespace Deck.Client.Modules;

public class ViewModelsModule : Autofac.Module
{ 
	protected override void Load(ContainerBuilder builder)
	{
		builder
			.RegisterType<MainWindowView>() 
			.AsSelf()
			.SingleInstance();

		builder
			.RegisterType<MainWindowViewModel>() 
			.AsSelf()
			.SingleInstance();

		#region Panels

		builder
			.RegisterType<PanelsViewModel>()
			.AsSelf()
			.SingleInstance();
		 
		builder
			.RegisterType<DeckCollectionViewModel>()
			.AsSelf()
			.SingleInstance();

		builder
			.RegisterType<SelectedDeckViewModel>()
			.AsSelf()
			.SingleInstance();

		#endregion

	}
}
