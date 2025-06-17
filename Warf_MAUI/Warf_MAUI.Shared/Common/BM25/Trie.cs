#region TrieInfo
/*
🌳 Вспомогательный класс Trie
Простое Trie-дерево (префиксное дерево), позволяющее быстро:
        - Вставлять слова (Insert)
        - Получать все слова по префиксу (GetWordsWithPrefix)

Внутренности Trie:
        - TrieNode — вершина дерева:
        - Children — дочерние символы.
        - IsEndOfWord — флаг конца слова.

Методы:
> Insert(string word) // Добавляет слово посимвольно в дерево.

> GetWordsWithPrefix(string prefix) // Находит поддерево, соответствующее префиксу, и возвращает все возможные окончания (полные слова).

📈 Алгоритм BM25 (подробнее)
Параметры:
    k1 — степень насыщения по частоте терма (обычно 1.2–2.0)
    b — насколько сильно учитывать длину документа (0 = игнорировать, 1 = учитывать полностью)

Формула:
[ BM25 = idf * ((tf * (k1 + 1)) / (tf + k1 * (1 - b + b * (dl / avgdl)))) ]
    tf      — частота слова в документе
    dl      — длина документа
    avgdl   — средняя длина всех документов
    idf     — логарифмическое значение обратной частоты документа

✅ Преимущества реализации
        - Поддержка нечёткого поиска (ошибки, опечатки).
        - Поддержка автодополнения по первым буквам.
        - Высокое качество ранжирования за счёт BM25.
        - Простота расширения под кастомные поля (можно заменить item.Name на item.Description, item.Tags и т. д.).

🔧 Возможные улучшения
        - Кеширование расстояний Левенштейна — особенно для больших словарей.
        - Поддержка мультиязычности — токенизация по языковым правилам.
        - Параллельный поиск — особенно при большом количестве документов.
        - Разделение полей (Name, Tags, Type и т.д.) с весами.
        - Фильтрация по дополнительным условиям (например, по цене, рангу и т. д.).
*/
#endregion TrieInfo

namespace Warf_MAUI.Shared.Common.BM25;

// Trie-дерево для автодополнения
public class Trie
{
    private class TrieNode
    {
        public Dictionary<char, TrieNode> Children = new();
        public bool IsEndOfWord;
    }
    public void Clear()
    {
        root.Children.Clear();
    }

    private readonly TrieNode root = new();

    public void Insert(string word)
    {
        var node = root;
        foreach (char ch in word)
        {
            if (!node.Children.ContainsKey(ch))
                node.Children[ch] = new TrieNode();
            node = node.Children[ch];
        }
        node.IsEndOfWord = true;
    }

    public List<string> GetWordsWithPrefix(string prefix)
    {
        var node = root;
        foreach (char ch in prefix)
        {
            if (!node.Children.ContainsKey(ch))
                return new List<string>();
            node = node.Children[ch];
        }
        List<string> results = new();
        FindWords(node, prefix, results);
        return results;
    }

    private void FindWords(TrieNode node, string prefix, List<string> results)
    {
        if (node.IsEndOfWord) results.Add(prefix);
        foreach (var child in node.Children)
        {
            FindWords(child.Value, prefix + child.Key, results);
        }
    }
}
