namespace ElectronicsFix.Ui
{
    public static class ClsUiHelper
    {
        // طريقة لتحميل صورة واحدة
        public static async Task<string> UploadImage(IFormFile file, string folderName)
        {
            // تحقق من حجم الملف
            if (file.Length > 0)
            {
                // تغيير كيفية إنشاء اسم الصورة مع تضمين التاريخ بشكل أكثر دقة
                string imageName = Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";

                // تحديد المسار الكامل لرفع الصورة
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads", folderName, imageName);

                // رفع الصورة إلى المسار المحدد
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                // إعادة المسار الكامل للصورة بعد الرفع
                return $"/Uploads/{folderName}/{imageName}";
            }
            // إرجاع فارغ إذا كان الملف غير موجود
            return string.Empty;
        }

        // يمكنك استعادة هذه الطريقة إذا كنت بحاجة إلى تحميل عدة صور
        /*
        public static async Task<string> UploadImage(List<IFormFile> files, string folderName)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string imageName = Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads", folderName, imageName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return $"/Uploads/{folderName}/{imageName}";
                }
            }
            return string.Empty;
        }
        */

        public static string Url(string url)
        {
            // التأكد من أن الرابط غير فارغ
            return string.IsNullOrEmpty(url) ? "~/" : url;
        }
    }
}
