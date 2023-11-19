using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models.Address
{
    [Table("as_adm_hierarchy")]
    public class HierarchyModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("objectid")]
        public int ObjectId { get; set; }

        [Column("parentobjid")]
        public int ParentObjId { get; set; }

        [Column("isactive")]
        public int IsActiveInt { get; set; }

        public HierarchyModel(int id, int objectId, int parentObjId, int isActiveInt)
        {
            Id = id;
            ObjectId = objectId;
            ParentObjId = parentObjId;
            IsActiveInt = isActiveInt;
        }
    }
}
