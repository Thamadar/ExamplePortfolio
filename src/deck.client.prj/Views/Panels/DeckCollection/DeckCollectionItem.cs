using Deck.Client.Data;
using Deck.Client.Extensions;
using Deck.UI;
using ReactiveUI;

namespace Deck.Client.Views.Panels;
public class DeckCollectionItem : ReactiveViewModelBase, IDeck
{
	private ICard[] _cards;
	private string _name;

	public int Id { get; }

	public ICard[] Cards
	{
		get => _cards;
		set => this.RaiseAndSetIfChanged(ref _cards, value);
	}

	public DeckType DeckType { get; }

	public ICard LastVisibleCard => Cards?.LastOrDefault();

	public string Name
	{
		get => _name;
		set => this.RaiseAndSetIfChanged(ref _name, value);
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

	public void RenameDeck(string name) => Name = name;

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
	} 
}
