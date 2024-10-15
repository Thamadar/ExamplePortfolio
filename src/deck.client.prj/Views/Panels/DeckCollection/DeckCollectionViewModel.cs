using Deck.Client.Data; 
using Deck.UI;
using DynamicData;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace Deck.Client.Views.Panels;
public sealed partial class DeckCollectionViewModel : ReactiveViewModelBase
{
	private readonly IDeckRepository _deckRepository;

	private ReadOnlyObservableCollection<IDeck> _decks;
	private IDeck _selectedDeck;

	public ReadOnlyObservableCollection<IDeck> Decks => _decks;

	public IDeck SelectedDeck
	{
		get => _selectedDeck;
		set
		{
			this.RaiseAndSetIfChanged(ref _selectedDeck, value);

			foreach(var item in Decks)
			{
				item.IsSelected = false;
			}

			if(_selectedDeck != null)
				_selectedDeck.IsSelected = true;
		}
	} 

	public DeckCollectionViewModel(
		IDeckRepository deckRepository)
	{
		_deckRepository = deckRepository;  

		_deckRepository
			.ConnectToDecks()
			.ObserveOn(RxApp.MainThreadScheduler)
			.Bind(out _decks)
			.Subscribe(x => {
				var deck = _decks?.Where(x => x.Id == SelectedDeck?.Id)?.FirstOrDefault();
				SelectedDeck = deck == null ? _decks.FirstOrDefault() : deck;
			});
	} 

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
