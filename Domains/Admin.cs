namespace Domains
{
    public class Admin
    {
        public int Id { get; set; } // معرف فريد للإداري
        public string Email { get; set; } // البريد الإلكتروني للإداري
        public string FirstName { get; set; } // الاسم الأول
        public string LastName { get; set; } // الاسم الأخير
        public string Phone { get; set; } // رقم الهاتف
        public string Address { get; set; } // العنوان
                                            // يمكنك إضافة المزيد من الخصائص حسب الحاجة
    }

}
