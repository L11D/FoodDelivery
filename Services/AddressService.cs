using FoodDelivery.Models;
using FoodDelivery.Models.Address;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static FoodDelivery.Models.Enums;

namespace FoodDelivery.Services
{
    public interface IAddressService
    {
        Task<List<SearchAddressModel>> Search(int objectId, string? query);
        Task<List<SearchAddressModel>> GetChain(Guid bjectGuid);
    }

    public class AddressService : IAddressService
    {
        private readonly FDDbContext _context;
        private readonly AppSettings _appSettings;


        public AddressService(FDDbContext context, AppSettings appSettings)
        {
            _context = context;
            _appSettings = appSettings;
        }
        public async Task<List<SearchAddressModel>> GetChain(Guid bjectGuid)
        {
            List<SearchAddressModel> searchedAdresses = new List<SearchAddressModel>();
            await chainRecursion(searchedAdresses, bjectGuid);
            return searchedAdresses;
        }

        private async Task chainRecursion(List<SearchAddressModel> searchedAdresses, Guid bjectGuid)
        {
            HouseModel? house = await _context.AsHouses.FirstOrDefaultAsync(h => h.ObjectGuid == bjectGuid);
            AddressElementModel? address = await _context.AddressElements.FirstOrDefaultAsync(a => a.ObjectGuid == bjectGuid);

            if (house != null)
            {
                searchedAdresses.Insert(0,
                    new SearchAddressModel(
                        house.ObjectId,
                        house.ObjectGuid,
                        buildHouseText(house),
                        ObjectLevelControll.GetGarAddressLevel("10"),
                        ObjectLevelControll.GetGarAddressLevelText("10"))
                    );

                HierarchyModel? hierarchyModel = await _context.Hierarchies.FirstOrDefaultAsync(h => h.ObjectId == house.ObjectId);
                if (hierarchyModel != null)
                {
                    HouseModel? nextHouse = await _context.AsHouses.FirstOrDefaultAsync(h => h.ObjectId == hierarchyModel.ParentObjId);
                    AddressElementModel? nextAddress = await _context.AddressElements.FirstOrDefaultAsync(a => a.ObjectId == hierarchyModel.ParentObjId);
                    if (nextHouse != null)
                    {
                        await chainRecursion(searchedAdresses, nextHouse.ObjectGuid);
                    }
                    else if (nextAddress != null)
                    {
                        await chainRecursion(searchedAdresses, nextAddress.ObjectGuid);
                    }
                }
            }
            else if (address != null)
            {
                searchedAdresses.Insert(0,
                       new SearchAddressModel(
                       address.ObjectId,
                       address.ObjectGuid,
                       address.TypeName + " " + address.Name,
                       ObjectLevelControll.GetGarAddressLevel(address.Level),
                       ObjectLevelControll.GetGarAddressLevelText(address.Level)));

                HierarchyModel? hierarchyModel = await _context.Hierarchies.FirstOrDefaultAsync(h => h.ObjectId == address.ObjectId);
                if (hierarchyModel != null)
                {
                    HouseModel? nextHouse = await _context.AsHouses.FirstOrDefaultAsync(h => h.ObjectId == hierarchyModel.ParentObjId);
                    AddressElementModel? nextAddress = await _context.AddressElements.FirstOrDefaultAsync(a => a.ObjectId == hierarchyModel.ParentObjId);
                    if (nextHouse != null)
                    {
                        await chainRecursion(searchedAdresses, nextHouse.ObjectGuid);
                    }
                    else if (nextAddress != null)
                    {
                        await chainRecursion(searchedAdresses, nextAddress.ObjectGuid);
                    }
                }
            }
            else
            {
                //throw new ExceptionWithStatusCode(404, "Object with uuid not found");
                throw new ExceptionWithStatusCode(_appSettings.Exeptions[3].Code, _appSettings.Exeptions[3].Message);
            }
        }

        public async Task<List<SearchAddressModel>> Search(int objectId, string? query)
        {
            List<HierarchyModel> hierarchyModels = await _context.Hierarchies.Where(h => h.ParentObjId == objectId && h.IsActiveInt == 1).ToListAsync();
            List<AddressElementModel> addresses = await _context.AddressElements.Where(a => hierarchyModels.Select(h => h.ObjectId).Contains(a.ObjectId) && a.IsActiveInt == 1).ToListAsync();
            List<HouseModel> houses = await _context.AsHouses.Where(x => hierarchyModels.Select(h => h.ObjectId).Contains(x.ObjectId) && x.IsActiveInt == 1).ToListAsync();

            List<SearchAddressModel> searchedAdresses = new List<SearchAddressModel>();

            foreach (var item in addresses)
            {
                searchedAdresses.Add(new SearchAddressModel(
                        item.ObjectId,
                        item.ObjectGuid,
                        item.TypeName + " " + item.Name,
                        ObjectLevelControll.GetGarAddressLevel(item.Level),
                        ObjectLevelControll.GetGarAddressLevelText(item.Level)));
            }

            foreach (var item in houses)
            {
                searchedAdresses.Add(new SearchAddressModel(
                        item.ObjectId,
                        item.ObjectGuid,
                        buildHouseText(item),
                        ObjectLevelControll.GetGarAddressLevel("10"),
                        ObjectLevelControll.GetGarAddressLevelText("10")));
            }

            if (query != null)
            {
                searchedAdresses = searchedAdresses.Where(a => a.Text.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return searchedAdresses;
        }

        private string buildHouseText(HouseModel house)
        {
            string ans = house.HouseNum;
            if (house.AddNum1 != null && house.AddType1 != null)
            {
                ans += " " + ObjectLevelControll.GetAddTypesText((int)house.AddType1) + " " + house.AddNum1;
            }
            if (house.AddNum2 != null && house.AddType2 != null)
            {
                ans += " " + ObjectLevelControll.GetAddTypesText((int)house.AddType2) + " " + house.AddNum2;
            }

            return ans;
        }
    }
}
