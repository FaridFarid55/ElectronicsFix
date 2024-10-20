// Ignore Spelling: Cls

namespace Bl
{
    public interface ICustomers
    {
        public IEnumerable<Customer> GetAll();
        public bool Save(Customer item);
        public bool CheckEmail(string email);
        public bool CheckCustomer(int Id);
        public Customer GetById(int Id);
        public bool Delete(int Id);
    }
    // Form Farid Farid
    public class ClsCustomers : ICustomers
    {
        // Filed
        private readonly DepiProjectContext _Context;

        // constrictor
        public ClsCustomers(DepiProjectContext Context)
        {
            _Context = Context;
        }
        public bool CheckEmail(string email)
        {
            return _Context.Customers.Any(c => c.Email == email);
        }
        public bool CheckCustomer(int Id)
        {
            return _Context.Customers.Any(c => c.CustomerId == Id);
        }

        public IEnumerable<Customer> GetAll()
        {
            try
            {
                var ListCustomer = _Context.Customers.ToList();
                return ListCustomer;
            }
            catch (Exception)
            {

                return new List<Customer>();
            }
        }

        public Customer GetById(int Id)
        {
            try
            {
                var item = _Context.Customers.FirstOrDefault(m => m.CustomerId == Id);
                return item;
            }
            catch (Exception)
            {
                return new Customer();
            }
        }
        public bool Save(Customer item)
        {
            try
            {
                if (item.CustomerId == 0) _Context.Customers.Add(item);
                else _Context.Entry(item).State = EntityState.Modified;

                // save
                _Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var Items = GetById(Id);
                _Context.Remove(Items);
                _Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
