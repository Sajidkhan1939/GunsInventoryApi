using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InventoryApi.Repositories
{
    public class Common
    {
        public static string RegexRplace(string Text)
        {
            if (!string.IsNullOrWhiteSpace(Text))
                Text = Text.Replace("+", "\\+").Replace("$", "\\$").Replace(".", "\\.").Replace("*", "\\*").Replace("?", "\\?").Replace("=", "\\=").Replace("^", "\\^")
                    .Replace("!", "\\!").Replace(":", "\\:").Replace("{", "\\{").Replace("}", "\\}").Replace("(", "\\(").Replace(")", "\\)").Replace("|", "\\|")
                    .Replace("[", "\\[").Replace("]", "\\]").Replace("'", "\\'").Replace("\"", "\\\"");
            return Text;
        }
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
        /* public static string UploadUserFile(IFormFile objFile, string RootPath, string UserId, string Id, int Type)
         {
             try
             {
                 if (objFile != null && !string.IsNullOrWhiteSpace(objFile.FileName))
                 {
                     if (Type == 0)
                     {

                         if (objFile.ContentType.Contains("image", StringComparison.OrdinalIgnoreCase))
                         {
                             return UploadFile(objFile, RootPath, UserId + "/Gun/" + Id + "/Images");
                         }
                     }
                     else if (Type == 1)
                     {
                         if (objFile.ContentType.Contains("video", StringComparison.OrdinalIgnoreCase))
                         {
                             return UploadFile(objFile, RootPath, UserId + "/Gun/" + Id + "/Video");
                         }
                     }
                     else if (Type == 2)
                     {
                         var fileExtension = Path.GetExtension(objFile.FileName).ToLower();
                         if (objFile.ContentType.Contains("image", StringComparison.OrdinalIgnoreCase) || fileExtension == ".pdf" || fileExtension == ".doc" || fileExtension == ".docx")
                         {
                             return UploadFile(objFile, RootPath, UserId + "/Gun/" + Id + "/Receipt");
                         }
                     }
                 }
                 return null;
             }
             catch (System.Exception ex)
             {
                 throw new Exception(ex.Message, ex);

             }
         }

         public static bool DeleteUserFile(string FileName, string RootPath, string UserId, string Id, int Type)
         {
             if (Type == 0)
             {
                 return DeleteFile(FileName, RootPath, UserId + "/Gun/" + Id + "/Images", true);
             }
             else if (Type == 1) { return DeleteFile(FileName, RootPath, UserId + "/Gun/" + Id + "/Video", true); }
             else if (Type == 2) { return DeleteFile(FileName, RootPath, UserId + "/Gun/" + Id + "/Receipt", true); }
             return false;
         }

         private static string UploadFile(IFormFile objFile, string RootPath, string FilePath = "")
         {
             try
             {
                 if (objFile != null && !string.IsNullOrWhiteSpace(objFile.FileName))
                 {
                     var uploadPath = Path.Combine(RootPath, "UserUpload", FilePath);
                     if (!Directory.Exists(uploadPath))
                     {
                         Directory.CreateDirectory(uploadPath);
                     }
                     var completePicPath = Path.Combine(uploadPath, objFile.FileName);
                     using (System.IO.FileStream fs = new System.IO.FileStream(completePicPath, System.IO.FileMode.Create))
                     {
                         objFile.CopyTo(fs);
                     }
                     return objFile.FileName;
                 }
                 else
                 {
                     return null;
                 }
             }
             catch (System.Exception ex)
             {
                 throw new Exception(ex.Message, ex);

             }

         }
         private static bool DeleteFile(string FileName, string RootPath, string FilePath = "", bool IsImageOnly = false)
         {
             try
             {
                 if (!string.IsNullOrWhiteSpace(FileName))
                 {
                     File.Delete(Path.Combine(RootPath, "UserUpload", FilePath, FileName));
                     return true;

                 }
                 else
                 {
                     return false;
                 }
             }
             catch (System.Exception ex)
             {
                 throw new Exception(ex.Message, ex);
             }
         }*/

    }
}
