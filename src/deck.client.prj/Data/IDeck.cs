namespace Deck.Client.Data;
public interface IDeck
{
	/// <summary>
	/// Идентификатор колоды.
	/// </summary>
	int Id { get; }

	/// <summary>
	/// Массив карт.
	/// </summary>
	ICard[] Cards { get; }

	/// <summary>
	/// Тип колоды. (24, 32, 36, 52)
	/// </summary>
	DeckType DeckType { get; }

	/// <summary>
	/// Имя колоды.
	/// </summary>
	string Name { get; }

	/// <summary>
	/// Самая верхняя карта.
	/// </summary>
	public ICard LastVisibleCard { get; }

	/// <summary>
	/// Переименовать колоду.
	/// </summary> 
	public void RenameDeck(string name);

	/// <summary>
	/// Перетасовать колоду.
	/// </summary> 
	public void ShaffleDeck(int[] newPositions);
}
