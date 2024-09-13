namespace Deck.Client.Data;
public interface IDeckStorage
{
	/// <summary>
	/// Хранилище колод.
	/// </summary>
	List<IDeck> Decks { get; }

	/// <summary>
	/// Подгрузка колод при инициализации.
	/// </summary>
	void LoadDecks();

	/// <summary>
	/// Сохранение колод при завершении.
	/// </summary>
	void SaveDecks();
}
