using Deck.Client.Data; 
using Deck.UI;

using ReactiveUI;
 

namespace Deck.Client.Views.Panels;
public sealed partial class DeckCollectionViewModel : ReactiveViewModelBase
{
	private readonly IDeckRepository _deckRepository;

	private List<IDeck> _decks;
	private IDeck _selectedDeck;

	public List<IDeck> Decks
	{
		get => _decks;
		set
		{
			this.RaiseAndSetIfChanged(ref _decks, value);
			if(_decks != null)
			{
				var deck = _decks?.Where(x => x.Id == SelectedDeck?.Id)?.FirstOrDefault();
				SelectedDeck = deck == null ? _decks.FirstOrDefault() : deck;
			}
		}
	}

	public IDeck SelectedDeck
	{
		get => _selectedDeck;
		set => this.RaiseAndSetIfChanged(ref _selectedDeck, value);
	} 

	public DeckCollectionViewModel(
		IDeckRepository deckRepository)
	{
		_deckRepository = deckRepository;

		_deckRepository.DecksChanged += OnDecksChanged; 
	}

	private void OnDecksChanged(object? sender, EventArgs e)
	{
		UpdateDecks();
	}

	private void UpdateDecks() => Decks = _deckRepository
										  .GetDecks()
										  .Select(deck => new DeckCollectionItem(deck.Cards, deck.DeckType, deck.Id, deck.Name) as IDeck)
										  .ToList();

	public void SetSelectedDeck(IDeck deck) => SelectedDeck = deck;

	public void SetSelectedDeck(int id)
	{
		var deck = Decks?.Where(x => x.Id == id).FirstOrDefault();
		if(deck != null)
		{
			SelectedDeck = deck;
		}
	}

	public void AddDeck(IDeck deck) => _deckRepository.AddDeck(deck);

	public void RemoveSelectedDeck() => _deckRepository.RemoveDeck(SelectedDeck?.Id ?? -1);

	public void RenameDeck(string name) => _deckRepository.RenameDeck(SelectedDeck.Id, name);
}
