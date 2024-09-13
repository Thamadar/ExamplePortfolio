using Deck.Client.Data;
using Deck.UI;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck.Client.Views.Panels;
public sealed partial class SelectedDeckViewModel : ReactiveViewModelBase
{
	private readonly IDeckRepository _deckRepository;
	private IDeck _selectedDeck;

	public IDeck SelectedDeck
	{
		get => _selectedDeck;
		set => this.RaiseAndSetIfChanged(ref _selectedDeck, value);
	}

	public SelectedDeckViewModel(
		IDeckRepository deckRepository)
	{
		_deckRepository = deckRepository; 
	}

	public void SetSelectedDeck(IDeck deck) => SelectedDeck = deck;

	public void ShuffleDeck() => _deckRepository.ShaffleDeck(id:SelectedDeck?.Id ?? -1); 
}
