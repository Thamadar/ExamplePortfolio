using Avalonia.Controls;
using Deck.Client.Views.Panels;
using DynamicData;

namespace Deck.Client.Data;
public class DeckRepository : IDeckRepository
{ 
	private readonly SourceList<IDeck> _decks = new(); 

	public DeckRepository()
	{
		LoadDesk();
	}

	/// <inheritdoc/>
	public IObservable<IChangeSet<IDeck>> ConnectToDecks()
	{
		return _decks.Connect();
	}

	/// <inheritdoc/>
	public void AddDeck(IDeck deck)
	{
		_decks.Add(deck); 
	}

	/// <inheritdoc/>
	public bool RemoveDeck(int id)
	{
		var deck = _decks.Items.Where(deck => deck.Id == id).FirstOrDefault();
		if(deck != null)
		{
			_decks.Remove(deck); 
			return true;
		}
		return false;
	}

	/// <inheritdoc/>
	public bool RenameDeck(int id, string name)
	{
		if(name == null || name == "")
		{
			return false;
		}
		var deck = _decks.Items.Where(deck => deck.Id == id).FirstOrDefault();
		if(deck != null)
		{
			deck.RenameDeck(name); 
			return true;
		}
		return false;
	} 

	/// <inheritdoc/>
	public IDeck? GetDeck(int id) => _decks.Items.Where(deck => deck.Id == id).FirstOrDefault() ?? null;

	/// <inheritdoc/>
	public bool ShaffleDeck(
		int id,
		int[] newPositions = null)
	{
		var deck     = _decks.Items.Where(deck => deck.Id == id).FirstOrDefault();
		var cards    = deck?.Cards;

		newPositions = newPositions == null ?
					   RandomGeneratePositions(cards?.Count()) :
					   newPositions;
		 
		deck?.ShaffleDeck(newPositions); 
		return true;
	}

	/// <summary>
	/// Допустим, загрузка колоды откуда-то (TCP, DB).
	/// </summary>
	private void LoadDesk()
	{
		var decks = new List<IDeck>()
		   {
			   DeckCollectionItem.CreateDeck(DeckType.Default, 0),
			   DeckCollectionItem.CreateDeck(DeckType.Default, 1, true),
			   DeckCollectionItem.CreateDeck(DeckType.Default, 2),
			   DeckCollectionItem.CreateDeck(DeckType.Large,   3, true),
			   DeckCollectionItem.CreateDeck(DeckType.Default, 4, true),
		   };

		_decks.Clear();
		_decks.AddRange(decks);
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
