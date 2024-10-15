namespace Deck.Client.Data;
public class Card : ICard
{ 
	public Suit Suit { get; }

	public int PowerValue { get; }
	 
	public Card(
		Suit suit,
		int powerValue)
	{
		Suit       = suit;
		PowerValue = powerValue;
	}

	public static ICard[] CreateCards(
		int size,
		bool hasJokers)
	{
		var resultCards = new ICard[size];
		if(hasJokers)
		{
			resultCards[0] = new Card(Suit.JokerDark,  size);
			resultCards[1] = new Card(Suit.JokerLight, size);
		} 

		var powerValue       = (size - (hasJokers ? 2 : 0)) / 4; 
		var currentSuitIndex = 0; 
		for(int i = 0 + (hasJokers ? 2 : 0); i < size; i++)
		{
			if(currentSuitIndex > (int)Suit.Diamonds)
			{
				currentSuitIndex = 0;
				powerValue--;
			}
			resultCards[i] = new Card((Suit)currentSuitIndex++, powerValue); 
		}

		return resultCards;
	} 
}
