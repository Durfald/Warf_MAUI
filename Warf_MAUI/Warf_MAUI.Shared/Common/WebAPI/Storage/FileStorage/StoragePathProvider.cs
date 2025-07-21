namespace Warf_MAUI.Shared.Common.WebAPI.Storage.FileStorage
{
    public static class StoragePathProvider
    {
        private static readonly string _folderName = "Data";
        private static readonly string _folderOrderName = "Orders";
        private static readonly string _folderStatisticsName = "Statistics";
        private static readonly string _whiteDirectoryName = "WhiteList";
        private static readonly string _blackDirectoryName = "BlackList";
        private static readonly string _folderItemDetailsName = "ItemDetails";
        private static readonly string _90DaysWhiteListDirectoryName = "90DaysWhiteList";
        private static readonly string _48HoursWhiteListDirectoryName = "48HoursWhiteList";
        private static readonly string _90DaysBlackListDirectoryName = "90DaysBlackList";
        private static readonly string _48HoursBlackListDirectoryName = "48HoursBlackList";


        public static readonly string PathDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _folderName);
        public static readonly string PathOrderDirectory = Path.Combine(PathDirectory, _folderOrderName);
        public static readonly string PathStatisticsDirectory = Path.Combine(PathDirectory, _folderStatisticsName);
        public static readonly string PathWhiteDirectory = Path.Combine(PathDirectory, _whiteDirectoryName);
        public static readonly string PathBlackDirectory = Path.Combine(PathDirectory, _blackDirectoryName);
        public static readonly string PathItemDetailsDirectory = Path.Combine(PathDirectory, _folderItemDetailsName);

        public static readonly string Path90DaysWhiteListDirectory = Path.Combine(PathWhiteDirectory, _90DaysWhiteListDirectoryName);
        public static readonly string Path48HoursWhiteListDirectory = Path.Combine(PathWhiteDirectory, _48HoursWhiteListDirectoryName);
        public static readonly string Path90DaysBlackListDirectory = Path.Combine(PathBlackDirectory, _90DaysBlackListDirectoryName);
        public static readonly string Path48HoursBlackListDirectory = Path.Combine(PathBlackDirectory, _48HoursBlackListDirectoryName);
    }
}
