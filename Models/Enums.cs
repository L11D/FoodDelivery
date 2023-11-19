namespace FoodDelivery.Models
{
    public class Enums
    {
        public enum UserGender
        {
            Male,
            Female
        }

        public enum DishCategory
        {
            WOK,
            Pizza,
            Soup,
            Desert,
            Drink
        }

        public enum SortingTypes
        {
            NameAsc,
            NameDesc,
            PriceAsc,
            PriceDesc,
            RatingAsc,
            RatingDesc
        }

        public enum OrderStatus
        {
            InProgress,
            Delivered
        }

        public enum GarAddressLevel
        {
            Region, 
            AdministrativeArea, 
            MunicipalArea, 
            RuralUrbanSettlement, 
            City, 
            Locality, 
            ElementOfPlanningStructure, 
            ElementOfRoadNetwork, 
            Land, 
            Building, 
            Room, 
            RoomInRooms, 
            AutonomousRegionLevel, 
            IntracityLevel, 
            AdditionalTerritoriesLevel, 
            LevelOfObjectsInAdditionalTerritories, 
            CarPlace
        }
    }
}
