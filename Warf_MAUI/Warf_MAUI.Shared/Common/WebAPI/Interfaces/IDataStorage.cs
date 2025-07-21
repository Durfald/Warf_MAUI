using System.Collections.Generic;

namespace Warf_MAUI.Shared.Common.WebAPI.Interfaces
{
    public interface IDataStorage
    {
        bool HasItems(string Language = "ru");
        
        Task SaveItems<T>(IEnumerable<T> items, string language = "ru");
        
        Task<IEnumerable<T>> GetItems<T>(string language = "ru");

        Task DeleteAllItems(string language = "ru");


        Task<IEnumerable<T>> GetFilteredItems<T>(string language = "ru");

        Task SaveFilteredItems<T>(IEnumerable<T> items, string language = "ru");

        Task DeleteAllFilteredItems(string language = "ru");

        //Task SaveOrders<T>(IEnumerable<T> orders, string urlItem);
        //Task<IEnumerable<T>> GetOrders<T>(string urlItem);

        //Task SaveItemDetails<T>(T itemDetails, string urlName);
        //Task<T?> GetItemDetails<T>(string urlName);
        //Task<IEnumerable<T>> GetItemDetails<T>();

        //Task SaveStatistics<T>(T statistics, string urlItem);

        //Task<T?> GetStatistics<T>(string urlItem);

        //Task DeleteAllOrders();
        //Task DeleteAllItemDetails();
        //Task DeleteAllStatistics();

        //Task<IEnumerable<T>> GetCachedItems<T>(string language = "ru");
        //Task<T?> GetCachedItemDetails<T>(string urlName);
        //Task<IEnumerable<T>> GetCachedItemDetails<T>();
        //Task<T?> GetCachedStatistics<T>(string urlItem);
        //Task<IEnumerable<T>> GetCachedOrders<T>(string urlItem);
    }
}