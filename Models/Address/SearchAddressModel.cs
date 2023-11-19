using System.ComponentModel.DataAnnotations.Schema;
using static FoodDelivery.Models.Enums;

namespace FoodDelivery.Models.Address
{
    public class SearchAddressModel
    {
        public int ObjectId { get; set; }
        public Guid ObjectGuid { get; set; }
        public string Text { get; set; }
        public GarAddressLevel ObjectLevel { get; set; }
        public string ObjectLevelText { get; set; }

        public SearchAddressModel(int objectId, Guid objectGuid, string text, GarAddressLevel objectLevel, string objectLevelText)
        {
            ObjectId = objectId;
            ObjectGuid = objectGuid;
            Text = text;
            ObjectLevel = objectLevel;
            ObjectLevelText = objectLevelText;
        }
    }
}
