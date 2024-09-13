using Deck.Client.Extensions; 

namespace Deck.Client.Data;
public class DeckRepository : IDeckRepository
{
	private readonly IDeckStorage _deckStorage;

	public event EventHandler DecksChanged;

	public DeckRepository(IDeckStorage deckStorage)
	{
		_deckStorage = deckStorage;
	}

	public void AddDeck(IDeck deck)
	{
		_deckStorage.Decks.Add(deck);
		DecksChanged?.Invoke(this, EventArgs.Empty);
	}

	public bool RemoveDeck(int id)
	{
		var deck = _deckStorage.Decks.Where(deck => deck.Id == id).FirstOrDefault();
		if(deck != null)
		{
			_deckStorage.Decks.Remove(deck);
			DecksChanged?.Invoke(this, EventArgs.Empty);
			return true;
		}
		return false;
	}

	public bool RenameDeck(int id, string name)
	{
		var deck = _deckStorage.Decks.Where(deck => deck.Id == id).FirstOrDefault();
		if(deck != null)
		{
			deck.RenameDeck(name);
			DecksChanged?.Invoke(this, EventArgs.Empty);
			return true;
		}
		return false;
	}


	public List<IDeck> GetDecks() => _deckStorage.Decks;

	public IDeck GetDeck(int id) => _deckStorage.Decks.Where(deck => deck.Id == id).FirstOrDefault();

	public bool ShaffleDeck(
		int id,
		int[] newPositions = null)
	{
		var deck     = _deckStorage.Decks.Where(deck => deck.Id == id).FirstOrDefault();
		var cards    = deck?.Cards;
			newPositions = newPositions == null ?
			RandomGeneratePositions(cards?.Count()) :
			newPositions;
		 
		deck?.ShaffleDeck(newPositions);
		DecksChanged?.Invoke(this, EventArgs.Empty);
		return true;
	}

	private int[] RandomGeneratePositions(int? cards)
	{ 
		if(cards != null)
		{
			var newPositions = GetIntArray(cards.Value);
			var random = new Random();
			for(int i = newPositions.Length - 1; i >= 1; i--)
			{
				int j = random.Next(i + 1);
				// обменять значения data[j] и data[i]
				var temp = newPositions[j];
				newPositions[j] = newPositions[i];
				newPositions[i] = temp;
			}
			return newPositions;
		}
		return new int[0];
	}

	private int[] GetIntArray(int? cards)
	{
		if(cards != null)
		{
			var intArray = new int[cards.Value];
			for(int i = 0; i < intArray.Length - 1; i++)
			{
				intArray[i] = i;
			}
			return intArray;
		}
		return new int[0];
	}
}
