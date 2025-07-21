using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Warf_MAUI.Shared.Common.WebAPI.Interfaces;

namespace Warf_MAUI.Shared.Common.WebAPI.Storage.FileStorage
{
    public class FileDataStorage : IDataStorage
    {
        private ILogger<FileDataStorage> _logger;

        //private bool AreEqual<T>(T obj1, T obj2)
        //{
        //    if (obj1 == null)
        //        throw new ArgumentNullException(nameof(obj1));
        //    if (obj2 == null)
        //        throw new ArgumentNullException(nameof(obj2));
        //    var json1 = JToken.FromObject(obj1);
        //    var json2 = JToken.FromObject(obj2);
        //    return JToken.DeepEquals(json1, json2);
        //}

        //private async Task WriteAllTextSafe(string path, string content)
        //{
        //    var dir = Path.GetDirectoryName(path);
        //    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        //        Directory.CreateDirectory(dir);

        //    File.WriteAllText(path, content);
        //}

        public Task<IEnumerable<T>> GetFilteredItems<T>(string language = "ru")
        {
            throw new NotImplementedException();
        }

        public Task SaveFilteredItems<T>(IEnumerable<T> items, string language = "ru")
        {
            throw new NotImplementedException();
        }


        public Task DeleteAllItems(string language = "ru")
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllFilteredItems(string language = "ru")
        {
            throw new NotImplementedException();
        }


        public bool HasItems(string language)
        {
            string path = GetItemsFilePath(language);
            return File.Exists(path);
        }

        public async Task SaveJsonAsync<T>(string path, T data)
        {
            try
            {
                var dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                await File.WriteAllTextAsync(path, json);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Ошибка при записи файла: {path}");
            }
        }

        public async Task<T?> LoadJsonAsync<T>(string path)
        {
            try
            {
                if (!File.Exists(path)) return default;
                var json = await File.ReadAllTextAsync(path);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Ошибка при чтении файла: {path}");
                return default;
            }
        }

        //private async Task DeleteAllFilesInDirectory(string dir, string searchPattern = "*.json")
        //{
        //    if (!Directory.Exists(dir))
        //        return;

        //    var files = Directory.GetFiles(dir, searchPattern, SearchOption.AllDirectories);

        //    var tasks = files
        //        .Where(file =>
        //        {
        //            var lastWrite = File.GetLastWriteTime(file);
        //            var created = File.GetCreationTime(file);
        //            var relevant = lastWrite > created ? lastWrite : created;
        //            return relevant < DateTime.Now.AddHours(-4);
        //        })
        //        .Select(file => Task.Run(() =>
        //        {
        //            try { File.Delete(file); }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError(ex, $"Ошибка при удалении файла: {file}");
        //            }
        //        }));

        //    await Task.WhenAll(tasks);
        //}

        public async Task SaveItems<T>(IEnumerable<T> items, string language = "ru")
        {
            string path = GetItemsFilePath(language);
            await SaveJsonAsync(path, items);
            //FileHelper.WriteAllTextSafe(path, JsonConvert.SerializeObject(items, Formatting.Indented));
        }

        public async Task<IEnumerable<T>> GetItems<T>(string language = "ru")
        {
            string path = GetItemsFilePath(language);
            var data = await LoadJsonAsync<List<T>>(path);
            return data ?? [];
            //if (File.Exists(path))
            //{

            //    var data = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(path));
            //    return data ?? throw new Exception("Failed to get items");
            //}
            //return new List<Item>(); // Если файла нет, возвращаем пустой список
        }

        private string GetItemsFilePath(string language = "ru") => Path.Combine(StoragePathProvider.PathDirectory, $"Items_{language}.json");


        //public async Task SaveOrders<T>(IEnumerable<T> orders, string urlItem)
        //{
        //    var orderpath = Path.Combine(StoragePathProvider.PathOrderDirectory, $"{urlItem}.json");
        //    await SaveJsonAsync(orderpath, orders);
        //    //FileHelper.WriteAllTextSafe(orderpath, JsonConvert.SerializeObject(orders, Formatting.Indented));
        //}

        //public Task DeleteAllOrders() => DeleteAllFilesInDirectory(StoragePathProvider.PathOrderDirectory);

        //public async Task SaveItemDetails<T>(T itemDetails, string urlName)
        //{
        //    var detailsPath = Path.Combine(StoragePathProvider.PathItemDetailsDirectory, $"{urlName}.json");

        //    await SaveJsonAsync(detailsPath, itemDetails);
        //}

        //public Task DeleteAllItemDetails() => DeleteAllFilesInDirectory(StoragePathProvider.PathItemDetailsDirectory);

        //public async Task<T?> GetItemDetails<T>(string urlName)
        //{
        //    var detailsPath = Path.Combine(StoragePathProvider.PathItemDetailsDirectory, $"{urlName}.json");
        //    return await LoadJsonAsync<T>(detailsPath);
        //}

        //public async Task<IEnumerable<T>> GetItemDetails<T>()
        //{
        //    var files = Directory.GetFiles(StoragePathProvider.PathItemDetailsDirectory);
        //    var items = new List<T>();
        //    foreach (var file in files)
        //    {
        //        var json = await LoadJsonAsync<T>(file);
        //        if (json != null)
        //            items.Add(json);
        //    }
        //    return items;
        //}

        //public async Task<IEnumerable<T>> GetOrders<T>(string urlItem)
        //{
        //    var orderpath = Path.Combine(StoragePathProvider.PathOrderDirectory, $"{urlItem}.json");

        //    var data = await LoadJsonAsync<List<T>>(orderpath);

        //    return data ?? [];
        //}

        //public async Task SaveStatistics<T>(T statistics, string urlItem)
        //{
        //    var statisticsPath = Path.Combine(StoragePathProvider.PathStatisticsDirectory, $"{urlItem}.json");

        //    await SaveJsonAsync(statisticsPath, statistics);
        //}

        //public Task DeleteAllStatistics() => DeleteAllFilesInDirectory(StoragePathProvider.PathStatisticsDirectory);

        //public async Task<T?> GetStatistics<T>(string urlItem)
        //{
        //    var statisticsPath = Path.Combine(StoragePathProvider.PathStatisticsDirectory, $"{urlItem}.json");

        //    return await LoadJsonAsync<T>(statisticsPath);
        //}

        //private DateTime GetLatestFileWriteTime(string folderPath)
        //{
        //    var directoryInfo = new DirectoryInfo(folderPath);
        //    if (!directoryInfo.Exists)
        //        throw new DirectoryNotFoundException($"Папка не найдена: {folderPath}");

        //    var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
        //    DateTime latest = directoryInfo.LastWriteTime;

        //    foreach (var file in files)
        //    {
        //        if (file.LastWriteTime > latest)
        //            latest = file.LastWriteTime;
        //    }

        //    return latest;
        //}

        //public DateTime GetWhiteListUpdateTime() => GetLatestFileWriteTime(StoragePathProvider.PathWhiteDirectory);

        public FileDataStorage(ILogger<FileDataStorage> logger)
        {
            _logger = logger;
        }
    }
}
