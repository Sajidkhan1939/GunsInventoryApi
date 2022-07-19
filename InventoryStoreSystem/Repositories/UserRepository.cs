using InventoryApi.Model;
using InventoryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InventoryApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        IOptions<Settings> _Settings;
        public UserRepository(IOptions<Settings> settings)
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
        public Result Login(string UserName, string Password)
        {
            if (string.IsNullOrWhiteSpace(UserName))
                return new Result { Status = false, Message = "Invalid username or password" };
            else if (string.IsNullOrWhiteSpace(Password))
                return new Result { Status = false, Message = "Invalid username or password" };
            else
            {
                User ObjUser = DbContextHelper.Context.User.Find(Builders<User>.Filter.Regex("Email", new MongoDB.Bson.BsonRegularExpression("^" + Common.RegexRplace(UserName) + "$", "i"))).FirstOrDefault();
                if (ObjUser != null && ObjUser.Password == Password)
                {
                    ObjUser.Password = "";
                    return new Result { Status = true, Data = ObjUser };
                }
                else
                    return new Result { Status = false, Message = "Invalid username or password" };
            }
        }
        public Result Register(User objRegister)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(objRegister.Email))
                    return new Result { Status = false, Message = "Email requires" };
                else if (string.IsNullOrWhiteSpace(objRegister.FirstName) && string.IsNullOrWhiteSpace(objRegister.LastName))
                    return new Result { Status = false, Message = "Name is Required" };
                else if (isUserNameExist(objRegister.Email))
                    return new Result { Status = false, Message = "Email Already Exist" };
                else
                {
                    objRegister.CreatedDate = DateTime.Now;
                    DbContextHelper.Context.User.InsertOne(objRegister);
                    return new Result { Status = true, Message = "Register Succesfully" };
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        private bool isUserNameExist(string Email)
        {
            var filter = Builders<User>.Filter.Regex(e => e.Email, new MongoDB.Bson.BsonRegularExpression("^" + Common.RegexRplace(Email) + "$", "i"));
            var findinguser = DbContextHelper.Context.User.Find(filter).FirstOrDefault();
            if (findinguser != null)
                return true;
          return false;
        }

        public Result Update(User objRegister, string Id)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    objRegister.ModifiedDate = DateTime.Now;
                    objRegister.ModifiedBy = Id;
                    var filter = Builders<User>.Filter.Eq("Id", Id);
                    var update = Builders<User>.Update.Set(s => s.FirstName, objRegister.FirstName)
                        .Set(s => s.LastName, objRegister.LastName)
                        .Set(s => s.State, objRegister.State)
                        .Set(s => s.City, objRegister.City)
                        .Set(s => s.Country, objRegister.Country)
                         .Set(s => s.ModifiedBy, objRegister.ModifiedBy)
                        .Set(s => s.ModifiedDate, objRegister.ModifiedDate) ;
                    DbContextHelper.Context.User.UpdateOne(filter, update);
                    return new Result { Status = true, Message = "updated successfully" };
                }
                else
                {
                    return new Result { Status = false, Message = "Not Found" };
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message,e);

            }
        }
        public Result ChangePassword(ChangePassword objChangePassword)
        {
            try
            {
                var Message = "";
                var status = false;
                if (objChangePassword != null)
                {
                    var Filter = Builders<User>.Filter.Eq("Id", objChangePassword.Id);
                    User user = DbContextHelper.Context.User.Find(Filter).FirstOrDefault();
                    if (user.Password == objChangePassword.Password)
                    {
                        var update = Builders<User>.Update.Set(s => s.Password, objChangePassword.NewPassword);
                        DbContextHelper.Context.User.UpdateOne(Filter, update);
                        Message = "Success";
                        status = true;
                    }
                    else
                    {
                        Message = "Invalid Password";
                        status = false;
                    }
                }
               
                return new Result { Message=Message,Status=status};
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        public Result ReadByUserId(string Id)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Id))
                {
                    
                    User objuser = DbContextHelper.Context.User.Find(Builders<User>.Filter.Eq("Id",Id)).FirstOrDefault();
                    return new Result
                    {
                        Status = true, Message = "" ,
                        Data=objuser
                    };
                }
                else
                {
                    return new Result { Status = false, Message = "Id Require" };
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
