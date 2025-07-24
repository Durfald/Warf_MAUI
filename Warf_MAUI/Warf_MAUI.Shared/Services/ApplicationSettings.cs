using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using Warf_MAUI.Shared.Common.WebAPI.WebClients.MyWarframeApiClient;

namespace Warf_MAUI.Shared.Services
{
    [XmlRoot]
    public class ApplicationSettings
    {
        // for serializers
        public ApplicationSettings() { }


        public ApplicationSettings(Action<ApplicationSettingsOverrides> optionsBuilder)
        {
            ApplicationSettingsOverrides overrides = new();
            optionsBuilder.Invoke(overrides);
            GetLocalSettings(this, overrides);
            GetNetworkSettings(this, overrides);
        }

        public GeneralSettings General { get; set; } = new();
        public NetworkSettings Network { get; set; } = new();
        public NotificationSettings Notifications { get; set; } = new();

        private const string SettingsDirectoryName = "./config";
        private const string SettingsFileRelativePath = SettingsDirectoryName + "/settings.xml";

        #region GetSettings

        private static void GetLocalSettings(
            ApplicationSettings settings,
            ApplicationSettingsOverrides overrides)
        {
            ApplicationSettings loadedSettings = new();

            if (OperatingSystem.IsWindows())
            {
                if (File.Exists(SettingsFileRelativePath))
                {
                    using (var file = File.Open(SettingsFileRelativePath, FileMode.Open))
                    {
                        var serializer = new XmlSerializer(typeof(ApplicationSettings));
                        if (serializer.CanDeserialize(XmlReader.Create(file)))
                        {
                            file.Seek(0, SeekOrigin.Begin);

                            var deserialized = serializer.Deserialize(file);
                            if (deserialized is ApplicationSettings deserializedSettings)
                                loadedSettings = deserializedSettings;
                        }
                    }
                }
            }
            else if (OperatingSystem.IsBrowser())
            {

            }

            settings.General = loadedSettings.General;
            settings.Network = loadedSettings.Network;
            settings.Notifications = loadedSettings.Notifications;

            if (overrides.DoBeFurry)
                Console.WriteLine("[Local] : You gay!");

        }

        private static void GetNetworkSettings(
            ApplicationSettings settings,
            ApplicationSettingsOverrides overrides)
        {
#if WINDOWS

#elif NET

#endif
            if (overrides.DoBeFurry)
                Console.WriteLine("[Network] : Now all your friends also knows dat you are g4y, sir!");
        }

        #endregion GetSettings

        #region SaveSettings

        public static void SaveSettings(ApplicationSettings instance)
        {
            if (OperatingSystem.IsWindows())
            {
                var serializer = new XmlSerializer(typeof(ApplicationSettings));

                if (!Directory.Exists(SettingsDirectoryName))
                    Directory.CreateDirectory(SettingsDirectoryName);

                // игнорить варн о юзинге
                using (var file = File.Open(SettingsFileRelativePath, FileMode.Create))
                {
                    var writer = XmlWriter.Create(file);
                    serializer.Serialize(writer, instance);

                    writer.Flush();
                    writer.Close();
                }
            }
            else if (OperatingSystem.IsBrowser())
            {

            }
        }

        #endregion SaveSettings
    }


    public class ApplicationSettingsOverrides
    {
        public bool DoBeFurry { get; set; }
    }

    #region SettingsTypes

    [XmlType]
    public class GeneralSettings
    {
        [DisplayName("Минимальный объём продаж за 2 дня")]
        public int TwoDaySellVolume { get; set; } = 60;

        [DisplayName("Минимальная разница цены между покупкой и продажей")]
        public float MinPriceDifference { get; set; } = 15;

        private string _language = string.Empty;
        [DisplayName("Язык")]
        public string Language
        {
            get
            {
                if (string.IsNullOrEmpty(_language))
                    _language = CultureInfo.CurrentCulture.Name;
                return _language;
            }
            set => _language = value;
        }
    }

    [XmlType]
    public class NetworkSettings
    {
        private const string EmailRegularExpression = @"^([0-9a-zA-Z.*_-]+)@([0-9a-zA-Z.*_-]+)\.([0-9a-zA-Z.*_-]+)$";
        private const string PasswordRegularExpression = @"^[0-9a-zA-Z.*_-]+$";

        private const string EmailErrorMessage =
            "Почта должна подходить под схему *@*.* м должна состоять только из латинских букв, цифр от " +
            "0 до 9 и может содержать символы *_-.";
        private const string PasswordErrorMessage = 
            "Пароль должен состоять только из латинских букв, цифр от 0 до 9 и может содержать символы *_-.";


        public SignInForm LoginForm { get; set; } = new();


        [XmlType]
        public class SignInForm
        {
            [DisplayName("Электронная почта")]
            [RegularExpression(EmailRegularExpression, ErrorMessage = EmailErrorMessage)]
            public string Email { get; set; } = string.Empty;

            [DisplayName("Пароль")]
            [RegularExpression(PasswordRegularExpression, ErrorMessage = PasswordErrorMessage)]
            public string Password { get; set; } = string.Empty;
        }
    }

    [XmlType]
    public class NotificationSettings
    {
        public bool EnableSendNotificationsThroughApplication { get; set; }

        public bool EnableSendNotificationsThroughWindows { get; set; }

        public bool EnableSendNotificationsThroughDiscord { get; set; }

        public bool EnableSendNotificationsThroughTelegram { get; set; }

        public bool TelegramNotificationsEnabled { get; }
    }

    #endregion SettingsTypes
}
