
using Autofac;
using Deck.Client.Views.Panels;
using Deck.Client.Views;
using Deck.Client.Data;

namespace Deck.Client.Modules;
public class RepositoriesModule : Autofac.Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder
			.RegisterType<DeckStorage>()
			.As<IDeckStorage>()
			.SingleInstance();

		builder
			.RegisterType<DeckRepository>()
			.As<IDeckRepository>()
			.SingleInstance();
	}
}
