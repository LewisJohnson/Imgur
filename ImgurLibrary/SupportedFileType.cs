using System.Collections.Generic;
using System.Linq;

namespace ImgurLibrary
{
    public static class SupportedFileType
    {
        public static IEnumerable<string> FileFormatList()
        {
            return new List<string>()
            {
                "JPEG",
                "JPG",
                "PNG",
                "GIF",
                "APNG",
                "TIFF",
                "BMP",
                "PDF",
                "XCF",
            };
        }

        public static bool CheckAgainstFormat(string input)
        {
            return FileFormatList().Any(fileFormat => input.ToUpper().Contains(fileFormat));
        }
    }
}
