namespace Warf_MAUI.Shared.Common.BM25
{
    /// <summary>
    /// Интерфейс для элементов, которые могут быть поиском.
    /// </summary>
    public interface ISearchableItem
    {
        /// <summary>
        /// Возвращает имя элемента, используемое для поиска.
        /// </summary>
        public string Name { get; init; }
    }
}
