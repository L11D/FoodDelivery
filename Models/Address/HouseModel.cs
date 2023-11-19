using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models.Address
{
    [Table("as_houses")]
    public class HouseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("objectid")]
        public int ObjectId { get; set; }

        [Column("objectguid")]
        public Guid ObjectGuid { get; set; }

        [Column("housenum")]
        public string HouseNum {  get; set; }

        [Column("addnum1")]
        public string? AddNum1 { get; set; }

        [Column("addnum2")]
        public string? AddNum2 { get; set; }

        [Column("addtype1")]
        public int? AddType1 { get; set; }

        [Column("addtype2")]
        public int? AddType2 { get; set; }

        [Column("isactive")]
        public int IsActiveInt { get; set; }

        public HouseModel(int id, int objectId, Guid objectGuid, string houseNum, string? addNum1, string? addNum2, int? addType1, int? addType2, int isActiveInt)
        {
            Id = id;
            ObjectId = objectId;
            HouseNum = houseNum;
            ObjectGuid = objectGuid;
            AddNum1 = addNum1;
            AddNum2 = addNum2;
            AddType1 = addType1;
            AddType2 = addType2;
            IsActiveInt = isActiveInt;
        }
    }
}
