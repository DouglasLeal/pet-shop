using PetShop.Models;

namespace PetShop.Utils
{
    public class ImageUtil
    {
        public static async Task<bool> Upload(IFormFile file, string path)
        {
            using var fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return true;
        }

        public static async Task<bool> Delete(string path)
        {
            System.IO.File.Delete(path);

            return true;
        }
    }
}
