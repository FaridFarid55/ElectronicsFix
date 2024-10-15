namespace ElectronicsFix.Ui
{
    public static class ClsUiHelper
    {
        public static async Task<string> UploadImage(List<IFormFile> files, string folderName)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +
                        DateTime.Now.Hour + +DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Microsecond +
                         "Fr" + ".jpg";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads/" + folderName, ImageName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await file.CopyToAsync(stream);
                        return ImageName;
                    }

                }
            }
            return string.Empty;
        }
        public static async Task<string> UploadImage(IFormFile file, string folderName)
        {
            if (file.Length > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +
                    DateTime.Now.Hour + +DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Microsecond +
                     "Fr" + ".jpg";
                var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads/" + folderName, ImageName);
                using (var stream = System.IO.File.Create(filePaths))
                {
                    await file.CopyToAsync(stream);
                    return ImageName;
                }

            }
            return string.Empty;
        }
        public static string Url(string url)
        {
            return string.IsNullOrEmpty(url) ? "~/" : url;
        }
    }
}
