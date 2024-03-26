namespace BusinessLogic.Helpers
{
    public class FileHelper
    {
        public static bool IsImage(string fileName)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".svg" };

            string fileExtension = Path.GetExtension(fileName)?.ToLower();

            return Array.Exists(imageExtensions, ext => ext == fileExtension);
        }

        public static bool IsPDF(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName)?.ToLower();

            return fileExtension == ".pdf";
        }
    }
}
