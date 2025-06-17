
#region MB25Info
/* TODO: Перенеси эту хуиту в XML комментарии над методами. И в Trie тоже.*/
/*
🧠 Общая цель класса BM25Searcher
BM25Searcher — это полнотекстовый поисковый движок, реализующий алгоритм BM25 для ранжирования релевантности документов (в данном случае — item.Name). Также включает:

Обратный индекс — для быстрого поиска по словам.
TF / DF индексы — для расчёта BM25.
Trie-дерево — для автодополнения.

Левенштейн-исправление — для корректировки ошибок в запросах (например, опечаток).
    📦 
    Поля и их назначение:
    Поле	                Тип	                                        Назначение
    items	                List<dynamic>	                            Список всех добавленных элементов (документов)
    invertedIndex	        Dictionary<string, List<int>>	            Обратный индекс: слово → список itemId, где оно встречается
    termFrequencies	        Dictionary<int, Dictionary<string, int>>	Слово → частота в конкретном документе
    documentFrequencies	    Dictionary<string, int>	                    Слово → количество документов, где оно встречается
    avgDocLength	        double	                                    Средняя длина документа (по количеству слов)
    k1, b	                double	                                    Параметры BM25
    autocompleteTrie	    Trie	                                    Trie-дерево для автодополнения


📚 Основные методы
> void Clear()                // Очищает все индексы и хранилища, сбрасывая внутреннее состояние.

> void AddItem(dynamic item)  // Добавляет один документ (item) в индекс:
                                - Разбивает item.Name на слова.
                                - Обновляет:
                                    invertedIndex
                                    termFrequencies
                                    documentFrequencies
                                    autocompleteTrie
                                - Перерасчитывает avgDocLength.

> void AddItems(IEnumerable<dynamic> items) // Добавляет сразу несколько документов через вызов AddItem для каждого.

> List<dynamic> Search(string query) // Основной метод поиска:
                                        - Разбивает строку запроса на слова.
                                        - Исправляет опечатки через FindClosestWord.
                                        - Для каждого слова вычисляет BM25-рейтинг для всех релевантных документов.
                                        - Сортирует документы по убыванию суммарного рейтинга и возвращает их.

> double CalculateBM25(int itemId, string term) // Вычисляет значение BM25 по формуле:
                                                    - [ BM25 = IDF(term) * TF(term) ], 
                                                        где IDF(term) = логарифм отношения общего числа документов к количеству документов с этим словом,
                                                            TF(term) = частотная формула с учётом длины документа (k1, b).

> string FindClosestWord(string word) // Исправление опечаток по алгоритму Левенштейна:
                                        - Перебирает все слова в invertedIndex.
                                        - Возвращает ближайшее слово по расстоянию.


> int LevenshteinDistance(string s1, string s2)           // Реализация алгоритма вычисления расстояния Левенштейна (редакционного расстояния) между двумя строками.

> List<string> GetAutocompleteSuggestions(string prefix)  // Возвращает список слов, начинающихся с prefix, используя Trie.

*/
#endregion MB25Info

using Warf_MAUI.Shared.Common.Mock;

namespace Warf_MAUI.Shared.Common.BM25;

public class BM25Searcher<T> where T : class, ISearchableItem
{
    private readonly Dictionary<int, Dictionary<string, int>> termFrequencies = [];
    private readonly Dictionary<string, List<int>> invertedIndex = []; // Обратный индекс
    private readonly Dictionary<string, int> documentFrequencies = [];
    private readonly List<T> items = [];

    private readonly Trie autocompleteTrie = new(); // Trie для автодополнения
    private readonly double k1 = 1.5;
    private readonly double b = 0.75;
    private double avgDocLength;

    public event Action<T>? OnItemAdded;
    public event Action? OnManyItemsAdded;
    public event Action? OnAllCleared;

    #region private

    private double CalculateBM25(int itemId, string term)
    {
        if (!termFrequencies[itemId].ContainsKey(term)) return 0;

        int termFrequency = termFrequencies[itemId][term];
        int docLength = items[itemId].Name.Split(' ').Length;
        int numDocs = items.Count;
        int docFrequency = documentFrequencies.ContainsKey(term) ? documentFrequencies[term] : 0;

        double idf = Math.Log(1 + (numDocs - docFrequency + 0.5) / (docFrequency + 0.5));
        double tf = termFrequency * (k1 + 1) / (termFrequency + k1 * (1 - b + b * (docLength / avgDocLength)));

        return idf * tf;
    }

    // Левенштейн для исправления ошибок
    private string FindClosestWord(string word)
    {
        int minDistance = int.MaxValue;
        string closestWord = word;

        foreach (var indexedWord in invertedIndex.Keys)
        {
            int distance = LevenshteinDistance(word, indexedWord);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestWord = indexedWord;
            }
        }
        return closestWord;
    }

    private int LevenshteinDistance(string s1, string s2)
    {
        int[,] dp = new int[s1.Length + 1, s2.Length + 1];

        for (int i = 0; i <= s1.Length; i++)
            for (int j = 0; j <= s2.Length; j++)
                if (i == 0) dp[i, j] = j;
                else if (j == 0) dp[i, j] = i;
                else
                    dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                                        dp[i - 1, j - 1] + (s1[i - 1] == s2[j - 1] ? 0 : 1));

        return dp[s1.Length, s2.Length];
    }

    #endregion private

    #region public 

    /// <summary>
    /// 
    /// </summary>
    public void Clear()
    {
        items.Clear();
        invertedIndex.Clear();
        termFrequencies.Clear();
        documentFrequencies.Clear();
        autocompleteTrie.Clear();
        avgDocLength = 0;

        OnAllCleared?.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    public Task AddItems(IEnumerable<T> items, CancellationToken? token = null)
    {
        foreach (var item in items)
        {
            if (token.HasValue && token.Value.IsCancellationRequested)
                return Task.CompletedTask;
            AddItem(item);
            OnItemAdded?.Invoke(item);
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(T item)
    {
        int itemId = items.Count;
        items.Add(item);
        var words = item.Name.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var termCount = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (!termCount.ContainsKey(word)) termCount[word] = 0;
            termCount[word]++;

            if (!invertedIndex.ContainsKey(word)) invertedIndex[word] = new List<int>();
            if (!invertedIndex[word].Contains(itemId)) invertedIndex[word].Add(itemId);

            autocompleteTrie.Insert(word); // Добавляем слово в Trie
        }

        termFrequencies[itemId] = termCount;

        foreach (var word in termCount.Keys)
        {
            if (!documentFrequencies.ContainsKey(word)) documentFrequencies[word] = 0;
            documentFrequencies[word]++;
        }

        avgDocLength = items.Average(d => d.Name.Split(' ').Length);
        OnItemAdded?.Invoke(item);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public Task<List<T>> Search(string query, CancellationToken? token = null)
    {
        var queryWords = query.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var correctedWords = queryWords.Select(FindClosestWord).ToList(); // Исправляем ошибки
        var scores = new Dictionary<int, double>();

        foreach (var word in correctedWords)
        {
            if (!invertedIndex.ContainsKey(word)) continue;
            foreach (var itemId in invertedIndex[word])
            {
                if (token.HasValue && token.Value.IsCancellationRequested)
                    return Task.FromResult(new List<T>());

                double score = CalculateBM25(itemId, word);
                if (!scores.ContainsKey(itemId)) scores[itemId] = 0;
                scores[itemId] += score;
            }
        }

        return Task.FromResult(
            scores.OrderByDescending(s => s.Value)
                .Select(s => (T)items[s.Key])
                .ToList()
        );
    }

    /// <summary>
    /// Автодополнение
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public List<string> GetAutocompleteSuggestions(string prefix)
    {
        return autocompleteTrie.GetWordsWithPrefix(prefix.ToLower());
    }

    #endregion public
}
