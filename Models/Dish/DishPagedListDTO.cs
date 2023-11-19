namespace FoodDelivery.Models.Dish
{
    public class DishPagedListDTO
    {
        public List<DishDTO>? Dishes { get; set; }

        public PageInfoModel Pagination { get; set; }

        public DishPagedListDTO (List<DishDTO>? dishes, PageInfoModel pagination)
        {
            Dishes = dishes;
            Pagination = pagination;
        }
    }
}
