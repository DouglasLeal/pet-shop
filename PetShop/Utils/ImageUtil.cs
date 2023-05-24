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
    }
}
