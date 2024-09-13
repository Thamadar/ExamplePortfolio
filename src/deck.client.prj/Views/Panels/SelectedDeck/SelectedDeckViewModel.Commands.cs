using Avalonia.Controls;

using ReactiveUI;

using System.Reactive;

namespace Deck.Client.Views.Panels;
public sealed partial class SelectedDeckViewModel
{
	public sealed class SelectedDeckViewModelCommands
	{
		public SelectedDeckViewModelCommands(SelectedDeckViewModel vm)
		{
			ShaffleDeck = ReactiveCommand.Create(() =>
			{ 
				vm.ShuffleDeck(); 
			});
		}

		public IReactiveCommand ShaffleDeck { get; }
	}

	private SelectedDeckViewModelCommands _commands;

	public SelectedDeckViewModelCommands Commands => _commands ??= new(this);
}
