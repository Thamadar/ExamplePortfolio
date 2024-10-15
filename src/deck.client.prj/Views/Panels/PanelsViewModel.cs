using Deck.Client.Data;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace Deck.Client.Views.Panels;
public class PanelsViewModel
{
	private readonly IDeckRepository _deckRepository;

	public DeckCollectionViewModel DeckCollectionViewModel { get; }
	public SelectedDeckViewModel SelectedDeckViewModel { get; }

	public PanelsViewModel(
		IDeckRepository deckRepository,
		DeckCollectionViewModel deckCollectionViewModel,
		SelectedDeckViewModel selectedDeckViewModel)
	{
		_deckRepository = deckRepository;

		DeckCollectionViewModel = deckCollectionViewModel;
		SelectedDeckViewModel   = selectedDeckViewModel;

		DeckCollectionViewModel
			.WhenAnyValue(x => x.SelectedDeck)
			.Subscribe(x => { SelectedDeckViewModel.SetSelectedDeck(x); }); 
	}

}
