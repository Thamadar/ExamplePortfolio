using Avalonia.Controls;
using Deck.Client.Data;
using ReactiveUI;

using System.Reactive;

namespace Deck.Client.Views.Panels;
public sealed partial class DeckCollectionViewModel
{
	public sealed class DeckCollectionViewModelCommands
	{
		public DeckCollectionViewModelCommands(DeckCollectionViewModel vm)
		{
			EditDeckName = ReactiveCommand.Create<Window, Unit>(
			window =>
			{
				var dialogWindow         = new DeckEditDialogWindowView();
				var dialogViewModel      = new DeckEditDialogWindowViewModel(vm.SelectedDeck?.Name ?? "");
				dialogWindow.DataContext = dialogViewModel;
				dialogWindow.Closed += (object? sender, EventArgs e) =>
				{
					if(dialogViewModel.IsSave)
					{
						vm.RenameDeck(dialogViewModel.NewName);
					}
				};
				dialogWindow.Show();
				return Unit.Default;
			});

			SelectDeck = ReactiveCommand.Create<int, Unit>(
			id =>
			{
				vm.SetSelectedDeck(id);
				return Unit.Default;
			});

			Add = ReactiveCommand.Create(
			() =>
			{
				var rnd = new Random();
				var id = vm.Decks.LastOrDefault().Id + 1;
				var deck = DeckCollectionItem.CreateDeck(
					(DeckType)rnd.Next(0,3),
					id,
					rnd.Next(0,3) == 3);

				vm.AddDeck(deck);
				vm.SelectedDeck = vm.Decks.LastOrDefault();
				return Unit.Default;
			});

			Remove = ReactiveCommand.Create(
			() =>
			{
				//Вообще кнопка не должна работать сама по себе. TO DO: отключать её по bool 
				if(vm.Decks != null && vm.Decks.Count > 1)
				{
					vm.RemoveSelectedDeck(); 
				}
				return Unit.Default;
			});
		}
		 

		public ReactiveCommand<Window, Unit> EditDeckName { get; }
		public ReactiveCommand<int, Unit> SelectDeck { get; }
		public IReactiveCommand Add { get; }
		public IReactiveCommand Remove { get; } 
	}

	private DeckCollectionViewModelCommands _commands;

	public DeckCollectionViewModelCommands Commands => _commands ??= new(this);
}
