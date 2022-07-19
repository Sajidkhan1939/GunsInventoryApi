
using InventoryApi.Model;
using InventoryApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace InventoryApi.Middleware
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(5);
        public SigningCredentials SigningCredentials { get; set; }
    }
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        //private readonly JsonSerializerSettings _serializerSettings;
        private readonly IOptions<Settings> _Settings;
        IUserRepository _UserRepository;
        public TokenProviderMiddleware(RequestDelegate next, IOptions<TokenProviderOptions> options, IOptions<Settings> Settings, IUserRepository UserRepository)
        {
            _next = next;
            this._options = options.Value;
            this._Settings = Settings;
            this._UserRepository = UserRepository;
        }

        public Task Invoke(HttpContext context)
        {

            if (context.Request.Method.Equals("OPTIONS"))
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                return _next(context);
            }
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                return _next(context);
            }

            //Request must be POST with Content-Type: application / x - www - form - urlencoded
            if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }


            return GenerateToken(context);
        }
        public async Task GenerateToken(HttpContext context)
        {
            try
            {
                var username = context.Request.Form["Username"];
                var password = context.Request.Form["Password"];
                Result IdentityResult = _UserRepository.Login(username, password);
                if (IdentityResult.Status == false || IdentityResult.Data == null)
                {

                }
                if (IdentityResult != null && IdentityResult.Status == true && IdentityResult.Data != null)
                {
                    User ObjUser = (User)IdentityResult.Data;
                    //UserAccountLog ObjLog = new UserAccountLog();
                    //ObjLog.UserAccountId = identity.Id;
                    //ObjLog.AccountType = identity.AccountType;
                    //ObjLog.UserName = identity.UserName;
                    //ObjLog.ActionType = "Login";
                    //ObjLog.LogDate = DateTime.Now;
                    //ObjLog.IpAddress = context.Connection.LocalIpAddress?.ToString() + " , " + context.Connection.RemoteIpAddress?.ToString();

                    //ObjLog.Comments = "Successfully Login";
                    //ObjLog.UserAgent = context.Request.Headers["User-Agent"];
                    //ObjLog.ClientUrl = context.Request.Headers["Referer"];
                    //DbHelper.Context.UserAccountLog.InsertOne(ObjLog);

                    var nowDate = DateTime.UtcNow;
                    var expDate = nowDate.Add(_options.Expiration);
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                       issuer: _options.Issuer,
                       audience: _options.Audience,
                       claims: new System.Collections.Generic.List<Claim>()
                       {
                            new Claim(JwtRegisteredClaimNames.Sub, ObjUser.Id),
                            new Claim(JwtRegisteredClaimNames.UniqueName, ObjUser.Id),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(nowDate).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                            new Claim(ClaimTypes.Role,"GunAppUser",ClaimValueTypes.String),
                       },
                       notBefore: nowDate,
                       expires: expDate,
                       signingCredentials: _options.SigningCredentials));



                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    // Serialize and return the response
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Message = "",
                        Access_token = encodedJwt,
                        Status = true,
                        UserId = ObjUser.Id,
                        Email = ObjUser.Email,
                        FirstName = ObjUser.FirstName,
                        LastName = ObjUser.LastName,
                        Expireon = expDate,
                    }, new JsonSerializerOptions { PropertyNamingPolicy = null }));
                }
                else
                {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(IdentityResult, new JsonSerializerOptions { PropertyNamingPolicy = null }));
                    return;
                }
            }
            catch (Exception ex)
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new Result { Message = ex.ToString(), Status = false }, new JsonSerializerOptions { PropertyNamingPolicy = null }));
                return;
            }

        }
    }
   
}
