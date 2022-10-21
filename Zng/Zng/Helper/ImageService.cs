
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Zng.Helper
{
    public class ImageService
    {
        public static Bitmap ResizeImageTest(string sourceImage, int desiredWidth, int desiredHeight)
        {
            //var original = Bitmap.FromFile(sourceImage.FullName);
            var original = new Bitmap(sourceImage);

            //store image widths in variable for easier use
            var oW = (decimal)original.Width;
            var oH = (decimal)original.Height;
            var dW = (decimal)desiredWidth;
            var dH = (decimal)desiredHeight;

            //check for double squares
            if (oW == dW && oH == dH)
            {
                return original;
            }
            else
            {
                var resized = new Bitmap(original, (int)dW, (int)dH);
                original.Dispose();
                return resized;
            }

        }

        public static Bitmap ResizeImage(Stream sourceImage, int desiredWidth, int desiredHeight)
        {
            //var original = Bitmap.FromFile(sourceImage.FullName);
            var original = new Bitmap(sourceImage);

            //store image widths in variable for easier use
            var oW = (decimal)original.Width;
            var oH = (decimal)original.Height;
            var dW = (decimal)desiredWidth;
            var dH = (decimal)desiredHeight;

            //check for double squares
            if (oW == dW && oH == dH)
            {
                return original;
            }
            else
            {
                var resized = new Bitmap(original, (int)dW, (int)dH);
                original.Dispose();
                return resized;
            }
        }

        public static void SaveImage(Bitmap image, string outputFileName)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    image.Save(memory, ImageFormat.Png);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }

        public static void SaveFile(HttpPostedFileBase file, string outputFileName)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    file.SaveAs(outputFileName);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }


        public static void DeleteImageFromFolder(string imagePath)
        {
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}
