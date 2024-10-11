using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public interface Icustomer
    {
        public List<Customer> GetAll();
        public Customer GetById(int id);
        public bool Save(Customer i_customer);
        public bool Delete(int Id);


    }

    

    public class ClsCustomer : Icustomer 

    {
        private readonly DepiProjectContext _Context;

        // constrictor
        public ClsCustomer(DepiProjectContext Context)
        {
            _Context = Context;
        }
        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAll()
        {
            try
            {
                var list_Customer = _Context.Customers.ToList();
                return list_Customer;
            }
            catch
            {
               return new List<Customer>();
            }


        }

        public Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Customer i_customer)
        {
            throw new NotImplementedException();
        }



    }
}
