using DynamicData;

namespace Deck.Client.Data;
public interface IDeckRepository
{
	/// <summary>
	/// Получить все колоды из Storage'а.
	/// </summary> 
	IObservable<IChangeSet<IDeck>> ConnectToDecks();

	/// <summary>
	/// Получить колоду из Storage по её ID.
	/// </summary> 
	IDeck GetDeck(int id);

	/// <summary>
	/// Добавить колоду в Storage.
	/// </summary> 
	void AddDeck(IDeck deck);

	/// <summary>
	/// Перетасовать колоду по ID.
	/// </summary> 
	bool ShaffleDeck(int id, int[] newPositions = null);

	/// <summary>
	/// Переименовать колоду в Storage по ID.
	/// </summary> 
	bool RenameDeck(int id, string name);

	/// <summary>
	/// Удалить колоду из Storage по ID.
	/// </summary> 
	bool RemoveDeck(int id);
}
