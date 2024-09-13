using Deck.Client.Data; 

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
		 
		DeckCollectionViewModel.PropertyChanged += OnSelectedDeckChanged;
		DeckCollectionViewModel.Decks = _deckRepository.GetDecks(); //Костыль для обновления SelectedDeckViewModel.SelectedDesk.
	}

	private void OnSelectedDeckChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		if(e.PropertyName == nameof(DeckCollectionViewModel.SelectedDeck))
		{
			SelectedDeckViewModel.SetSelectedDeck(DeckCollectionViewModel.SelectedDeck);
		}
	}
}
