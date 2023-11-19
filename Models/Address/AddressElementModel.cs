using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models.Address
{
    [Table("as_addr_obj")]
    public class AddressElementModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("objectid")]
        public int ObjectId { get; set; }

        [Column("objectguid")]
        public Guid ObjectGuid { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("typename")]
        public string TypeName { get; set; }

        [Column("level")]
        public string Level { get; set; }

        [Column("isactive")]
        public int IsActiveInt { get; set; }

        public AddressElementModel(int id, int objectId, Guid objectGuid, string name, string typeName, string level, int isActiveInt)
        {
            Id = id;
            ObjectId = objectId;
            ObjectGuid = objectGuid;
            Name = name;
            TypeName = typeName;
            Level = level;
            IsActiveInt = isActiveInt;
        }
    }
}
