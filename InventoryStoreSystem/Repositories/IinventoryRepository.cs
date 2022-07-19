using InventoryApi.Model;
namespace InventoryApi.Repositories
{
    public interface IinventoryRepository
    {
        Result Add(Guns objAdd, string UserId);
        Result Update(Guns objAdd, string UserId);
        Result ReadByGunsId(string Id);
        Result ReadGunsByUserId(string UserId);
        Result Delete(string Id);
    }
}
