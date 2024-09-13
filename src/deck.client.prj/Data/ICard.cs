namespace Deck.Client.Data;
public interface ICard
{
	/// <summary>
	/// Масть карты.
	/// </summary>
	Suit Suit { get; }

	/// <summary>
	/// Сила карты.
	/// </summary>
	int PowerValue { get; } 
}
