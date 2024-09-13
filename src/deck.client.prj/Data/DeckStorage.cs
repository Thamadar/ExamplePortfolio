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
			   Deck.CreateDeck(DeckType.Default, 0),
			   Deck.CreateDeck(DeckType.Default, 1, true),
			   Deck.CreateDeck(DeckType.Default, 2),
			   Deck.CreateDeck(DeckType.Large,   3, true),
			   Deck.CreateDeck(DeckType.Default, 4, true),
		   };
	}

	public void SaveDecks()
	{
		//TO DO: загрузка в конфиг
	}
}
