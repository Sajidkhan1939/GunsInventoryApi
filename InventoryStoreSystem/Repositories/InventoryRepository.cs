using InventoryApi.Model;
using InventoryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InventoryApi.Repositories
{
    public class InventoryRepository : IinventoryRepository
    {
        IOptions<Settings> _Settings;
        public InventoryRepository(IOptions<Settings> settings)
        {
            try
            {
                DbContextHelper.Context = new DbContext(settings);
                _Settings = settings;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public Result Add(Guns objAdd, string UserId)
        {
            try
            {
                objAdd.UserId = UserId;
                objAdd.CreatedBy= UserId;
                objAdd.CreatedDate = DateTime.Now;
                DbContextHelper.Context.Gun.InsertOne(objAdd);
                return new Result
                {
                    Status = true,
                    Message = "Added Succecfully",
                    Data = objAdd
                };
            }
            catch
            {
                throw new Exception();
            }
        }
        public Result Update(Guns objAdd,string UserId)
        {
            try
            {
                if (!string.IsNullOrEmpty(objAdd.Id))
                {
                    var filter = Builders<Guns>.Filter.Eq("Id", objAdd.Id) & Builders<Guns>.Filter.Eq("UserId", UserId);
                    objAdd.ModifiedBy= UserId;
                    objAdd.ModifiedDate= DateTime.Now;
                    var update = Builders<Guns>.Update.Set(s => s.Name, objAdd.Name)
                          .Set(s => s.FirearmType, objAdd.FirearmType)
                        .Set(s => s.Manufacturer, objAdd.Manufacturer)
                        .Set(s => s.Model, objAdd.Model)
                        .Set(s => s.Caliber, objAdd.Caliber)
                        .Set(s => s.CountryOfOrigin, objAdd.CountryOfOrigin)
                        .Set(s => s.DateOfPurchase, objAdd.DateOfPurchase)
                        .Set(s => s.PurchasedStoreName, objAdd.PurchasedStoreName)
                        .Set(s => s.SerialNo, objAdd.SerialNo)
                        .Set(s => s.Codition, objAdd.Codition)
                        .Set(s => s.StoredLocation, objAdd.StoredLocation)
                        .Set(s => s.PurchasePrice, objAdd.PurchasePrice)
                        .Set(s => s.InsuranceValue, objAdd.InsuranceValue)
                        .Set(s => s.Action, objAdd.Action)
                        .Set(s => s.OAL, objAdd.OAL)
                        .Set(s => s.Weight, objAdd.Weight)
                        .Set(s => s.TwistRate, objAdd.TwistRate)
                        .Set(s => s.LengthOfPull, objAdd.LengthOfPull)
                        .Set(s => s.BarrelLength, objAdd.BarrelLength)
                        .Set(s => s.BarrelFinish, objAdd.BarrelFinish)
                        .Set(s => s.GunFinish, objAdd.GunFinish)
                        .Set(s => s.AmmoTypeUsed, objAdd.AmmoTypeUsed)
                        .Set(s => s.LastMaintenanceDate, objAdd.LastMaintenanceDate)
                        .Set(s => s.GeneralNotes, objAdd.GeneralNotes)
                        .Set(s => s.Sights, objAdd.Sights)
                        .Set(s => s.Scope, objAdd.Scope)
                        .Set(s => s.TypeOfScope, objAdd.TypeOfScope)
                        .Set(s => s.Laser, objAdd.Laser)
                        .Set(s => s.TypeOfLaser, objAdd.TypeOfLaser)
                        .Set(s => s.StockType, objAdd.StockType)
                        .Set(s => s.TriggerType, objAdd.TriggerType)
                        .Set(s => s.TriggerPull, objAdd.TriggerPull)
                        .Set(s => s.CostOfFirearm, objAdd.CostOfFirearm)
                        .Set(s => s.AnyOtherAccessories, objAdd.AnyOtherAccessories)
                        .Set(s => s.PreBan, objAdd.PreBan)
                        .Set(s => s.NFAApprovalDate, objAdd.NFAApprovalDate)
                        .Set(s => s.NFAPaperworkScanned, objAdd.NFAPaperworkScanned)
                        .Set(s => s.NameOfTrustOrCorporation, objAdd.NameOfTrustOrCorporation)
                        .Set(s => s.LocalPoliceApproval, objAdd.LocalPoliceApproval)
                        .Set(s => s.LocalPoliceStationName, objAdd.LocalPoliceStationName)
                        .Set(s => s.ModifiedBy, objAdd.ModifiedBy)
                        .Set(s => s.ModifiedDate, objAdd.ModifiedDate);
                    DbContextHelper.Context.Gun.UpdateOne(filter,update);
                    return new Result { Status=true,Data = objAdd};
                }
                else
                {
                    return new Result { Status=false,Message="id require"};
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Result Delete(string Id)
        {
            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    var filter = Builders<Guns>.Filter.Eq("Id",Id);
                    DbContextHelper.Context.Gun.DeleteOne(filter);
                    return new Result { Status = true };
                }
                else
                {
                    return new Result { Status = false, Message = "id require" };
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Result ReadByGunsId(string Id)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    var filter = Builders<Guns>.Filter.Eq("Id", Id);
                    Guns objGuns = DbContextHelper.Context.Gun.Find(filter).FirstOrDefault();
                    return new Result
                    {
                        Message="Success",
                        Status = true,
                        Data = objGuns
                    };
                }
                else
                {
                    return new Result 
                    { Status = false, Message = "id require" };
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public Result ReadGunsByUserId(string UserId)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(UserId))
                {
                    var filter = Builders<Guns>.Filter.Eq("UserId", UserId);
                    IList<Guns> listGuns = DbContextHelper.Context.Gun.Find(filter).ToList();
                    return new Result
                    { Message="Success",Status = true, Data = listGuns };
                }
                else
                {
                    return new Result { Status = false, Message = "id required" };
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
