using Deck.Client.Views.Panels;

namespace Deck.Client.Data;

public class DeckStorage : IDeckStorage
{
	public List<IDeck> Decks { get; private set; }

	public DeckStorage()
	{
		LoadDecks();
	}

	public void LoadDecks()
	{
		//TO DO: вытяжка из конфига.
		Decks = new List<IDeck>()
		   {
			   DeckCollectionItem.CreateDeck(DeckType.Default, 0),
			   DeckCollectionItem.CreateDeck(DeckType.Default, 1, true),
			   DeckCollectionItem.CreateDeck(DeckType.Default, 2),
			   DeckCollectionItem.CreateDeck(DeckType.Large,   3, true),
			   DeckCollectionItem.CreateDeck(DeckType.Default, 4, true),
		   };
	}

	public void SaveDecks()
	{
		//TO DO: загрузка в конфиг
	}
}
