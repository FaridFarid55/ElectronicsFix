namespace ElectronicsFix.Areas.admin.Controllers
{
    [Area("admin")]
    public class CustomersController : Controller
    {
        // Field 
        private readonly ICustomers oClsCustomers;

        // Constructor 
        public CustomersController(ICustomers ClsCustomers)
        {
            oClsCustomers = ClsCustomers;
        }
        //This  Method Customers

        #region Method
        // GET: Customers
        public async Task<IActionResult> Index()
        {
            try
            {
                // Returns the view with the list of all customers from the database
                return View(oClsCustomers.GetAll());
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework)
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving customers.");

                // Return an empty list or handle accordingly
                return View(new List<Customer>());
            }
        }

        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            // Returns NotFound if the ID is null
            if (id == null) return NotFound();

            try
            {
                // Retrieves the customer by ID
                var customer = oClsCustomers.GetById((int)id);

                // Returns NotFound if the customer is not found
                if (customer == null) return NotFound();

                // Returns the view with customer details
                return View(customer);
            }
            catch (Exception ex)
            {
                // Adds an error message if details retrieval fails
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving customer details.");

                // Return an empty Customer object or handle accordingly
                return View(new Customer());
            }
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Returns NotFound if the ID is null
            if (id == null) return NotFound();

            try
            {
                // Retrieves the customer by ID
                var customer = oClsCustomers.GetById((int)id);

                // Returns NotFound if the customer is not found
                if (customer == null) return NotFound();

                // Returns the view with the customer data for editing
                return View(customer);
            }
            catch (Exception ex)
            {
                // Adds an error message if retrieval fails
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving customer data for editing.");

                // Return an empty Customer object or handle accordingly
                return View(new Customer());
            }
        }


        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            // Returns NotFound if the ID in the URL doesn't match the customer's ID
            if (id != customer.CustomerId) return NotFound();

            // Check if passwords match
            if (customer.Password != customer.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(customer);
            }

            // If the model state is invalid, return the view with the submitted data
            if (!ModelState.IsValid) return View("Edit", customer);

            try
            {
                // Updates the customer details in the database
                oClsCustomers.Save(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Returns NotFound if the customer no longer exists
                if (!await CustomerExists(customer.CustomerId)) return NotFound();
                else throw; // Re throw any other exception
            }
            catch (Exception ex)
            {
                // Adds an error message if updating fails
                ModelState.AddModelError(string.Empty, "An error occurred while updating the customer.");
                return View(customer);
            }

            // Redirect to the Index action after a successful edit
            return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Returns NotFound if the ID is null
            if (id == null) return NotFound();

            try
            {
                // Retrieves the customer by ID
                var customer = oClsCustomers.GetById((int)id);

                // Returns NotFound if the customer is not found
                if (customer == null) return NotFound();

                // Returns the view with the customer data for deletion confirmation
                return View(customer);
            }
            catch (Exception ex)
            {
                // Adds an error message if retrieval fails
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving customer data for deletion.");

                // Return an empty Customer object or handle accordingly
                return View(new Customer());
            }
        }


        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Finds the customer by ID
                var customer = oClsCustomers.GetById(id);

                // Removes the customer from the database if found
                if (customer != null) oClsCustomers.Delete(id);

                // Redirect to the Index action after deletion
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Adds an error message if deletion fails
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the customer.");
                return RedirectToAction(nameof(Index)); // Optionally redirect with an error message
            }
        }

        // Helper method to check if a customer exists by ID
        private async Task<bool> CustomerExists(int id)
        {
            // Returns true if the customer exists, false otherwise
            return oClsCustomers.CheckCustomer(id);
        }

        // Method to check if an email already exists (for remote validation in forms)
        public IActionResult CheckEmailExists(string email)
        {
            // Checks if the email already exists in the database
            var emailExists = oClsCustomers.CheckEmail(email);

            // Return JSON result (true if email is unique, false if taken)
            return Json(!emailExists);
        }

        #endregion
    }
}
