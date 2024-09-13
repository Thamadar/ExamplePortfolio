using Deck.Client.Extensions;

namespace Deck.Client.Data;
public class Deck : IDeck
{
	public int Id { get; }

	public ICard[] Cards { get; private set; }

	public DeckType DeckType { get; }

	public string Name { get; private set; }

	public ICard LastVisibleCard => Cards?.LastOrDefault();

	public Deck(
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

	public static IDeck CreateDeck(DeckType deckType, int id, bool hasJokers = false)
	{
		var deckSize = GetDeckSize(deckType) + (hasJokers ? 2 : 0);
		var cards    = Card.CreateCards(deckSize, hasJokers);
		return new Deck(cards, deckType, id, $"Колода {id+1}");
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
