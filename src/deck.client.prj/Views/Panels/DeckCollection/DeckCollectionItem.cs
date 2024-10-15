using Deck.Client.Data;
using Deck.Client.Extensions;
using Deck.UI;
using ReactiveUI;

namespace Deck.Client.Views.Panels;
public class DeckCollectionItem : ReactiveViewModelBase, IDeck
{
	private ICard[] _cards;
	private ICard? _lastVisibleCard;
	private string _name;
	private bool _isSelected;

	/// <inheritdoc/>
	public int Id { get; }

	/// <inheritdoc/>
	public ICard[] Cards
	{
		get => _cards;
		set
		{
			this.RaiseAndSetIfChanged(ref _cards, value);
			LastVisibleCard = _cards.FirstOrDefault();
		}
	}

	/// <inheritdoc/>
	public DeckType DeckType { get; }

	/// <inheritdoc/>
	public ICard? LastVisibleCard
	{
		get => _lastVisibleCard;
		set => this.RaiseAndSetIfChanged(ref _lastVisibleCard, value);
	}

	/// <inheritdoc/>
	public string Name
	{
		get => _name;
		set => this.RaiseAndSetIfChanged(ref _name, value);
	}

	/// <inheritdoc/>
	public bool IsSelected
	{
		get => _isSelected;
		set => this.RaiseAndSetIfChanged(ref _isSelected, value);
	}

	public DeckCollectionItem(
		ICard[] cards,
		DeckType deckType,
		int id,
		string name)
	{
		Cards    = cards;
		DeckType = deckType;
		Id       = id;
		Name     = name;
	}

	/// <inheritdoc/>
	public void RenameDeck(string name) => Name = name;

	/// <inheritdoc/>
	public void ShaffleDeck(int[] newPositions)
	{
		if(Cards != null && Cards.Count() > 1)
		{
			for(int i = 0; i < Cards.Count(); i++)
			{
				Cards.Swap(ref Cards[newPositions[i]], ref Cards[newPositions[++i]]);
			}
			Cards.Swap(ref Cards[newPositions.Count() - 1], ref Cards[newPositions[0]]);
		}

		LastVisibleCard = Cards?.FirstOrDefault();
	} 

	public static IDeck CreateDeck(DeckType deckType, int id, bool hasJokers = false)
	{
		var deckSize = GetDeckSize(deckType) + (hasJokers ? 2 : 0);
		var cards = Card.CreateCards(deckSize, hasJokers);
		return new DeckCollectionItem(cards, deckType, id, $"Колода {id + 1}");
	}

	public static int GetDeckSize(DeckType deckType)
	{
		switch(deckType)
		{
			case DeckType.Large:
				return 52;
			case DeckType.Default:
				return 36;
			case DeckType.Small:
				return 32;
			case DeckType.Tiny:
				return 24;
			default: return 0;
		}
	}
}
