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
