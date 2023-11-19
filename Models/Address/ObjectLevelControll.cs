using System.Net;
using static FoodDelivery.Models.Enums;

namespace FoodDelivery.Models.Address
{
    static class ObjectLevelControll
    {
        private static List<string> objectLevelTextes = new List<string>
        {
            "Субъект РФ",
            "Административный район",
            "Муниципальный район",
            "Сельское/городское поселение",
            "Город",
            "Населенный пункт",
            "Элемент планировочной структуры",
            "Элемент улично-дорожной сети",
            "Земельный участок",
            "Здание",
            "Помещение",
            "Помещения в пределах помещения",
            "Уровень автономного округа",
            "Уровень внутригородской территории",
            "Уровень дополнительных территорий",
            "Уровень объектов на дополнительных территориях",
            "Машиноместо"
        };

        private static List<string> addTypes = new List<string>
        {
            "к.",
            "стр.",
            "соор.",
            " "
        };

        public static string GetAddTypesText(int level)
        {
            return addTypes[level-1];
        }

        public static GarAddressLevel GetGarAddressLevel(string level)
        {
            return (GarAddressLevel)Enum.Parse(typeof(GarAddressLevel), (Int32.Parse(level) - 1).ToString());
        }

        public static string GetGarAddressLevelText(string level)
        {
            return objectLevelTextes[Int32.Parse(level) - 1];
        }
    }
}
