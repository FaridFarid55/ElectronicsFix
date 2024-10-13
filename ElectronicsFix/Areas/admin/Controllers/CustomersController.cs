namespace ElectronicsFix.Areas.admin.Controllers
{
    // Specifies that this controller belongs to the "admin" area
    [Area("admin")]
    public class CustomersController : Controller
    {
        // Field for database context
        private readonly DepiProjectContext _context;

        // Constructor initializes the database context
        public CustomersController(DepiProjectContext context)
        {
            _context = context;
        }
        //This  Method Customers
        #region Method
        // GET: Customers
        public async Task<IActionResult> Index()
        {
            try
            {
                // Returns the view with the list of all customers from the database
                return View(await _context.Customers.ToListAsync());
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework)
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving customers.");

                // Return an empty list or handle accordingly
                return View(new List<Customer>());
            }
        }

        // Method to display details of a specific customer
        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Returns NotFound if the ID is null
            if (id == null) return NotFound();

            try
            {
                // Retrieves the customer by ID
                var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);

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

        // Method to render the customer creation form
        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // Method to handle the form submission for creating a new customer
        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FirstName,LastName,Phone,Address,Email,Password,ConfirmPassword")] Customer customer)
        {
            // Check if passwords match
            if (customer.Password != customer.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return View(customer);
            }

            // Check if the email is already taken
            if (_context.Customers.Any(c => c.Email == customer.Email))
            {
                ModelState.AddModelError("Email", "Email address is already taken.");
                return View(customer);
            }

            // Hash the password before saving
            customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            customer.ConfirmPassword = customer.Password;  // Ensure confirm password matches the hashed password

            // If the model state is invalid, return the view with the submitted data
            if (!ModelState.IsValid) return View(customer);

            try
            {
                // Add the new customer to the database
                _context.Add(customer);
                await _context.SaveChangesAsync();

                // Redirect to the Index action after successful creation
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Adds an error message if the customer creation fails
                ModelState.AddModelError(string.Empty, "An error occurred while creating the customer.");
                return View(customer);
            }
        }

        // Method to render the edit form for an existing customer
        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Returns NotFound if the ID is null
            if (id == null) return NotFound();

            try
            {
                // Retrieves the customer by ID
                var customer = await _context.Customers.FindAsync(id);

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

        // Method to handle the form submission for editing a customer
        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FirstName,LastName,Phone,Address,Email,Password,ConfirmPassword")] Customer customer)
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
            if (!ModelState.IsValid)
            {
                return View("Edit", customer);
            }

            try
            {
                // Updates the customer details in the database
                _context.Update(customer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Returns NotFound if the customer no longer exists
                if (!await CustomerExists(customer.CustomerId)) return NotFound();
                else throw; // Rethrow any other exception
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

        // Method to render the delete confirmation form for a customer
        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Returns NotFound if the ID is null
            if (id == null) return NotFound();

            try
            {
                // Retrieves the customer by ID
                var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);

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

        // Method to handle the form submission for deleting a customer
        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Finds the customer by ID
                var customer = await _context.Customers.FindAsync(id);

                // Removes the customer from the database if found
                if (customer != null) _context.Customers.Remove(customer);

                // Save changes to the database
                await _context.SaveChangesAsync();

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
            return await _context.Customers.AnyAsync(c => c.CustomerId == id);
        }

        // Method to check if an email already exists (for remote validation in forms)
        public IActionResult CheckEmailExists(string email)
        {
            // Checks if the email already exists in the database
            var emailExists = _context.Customers.Any(c => c.Email == email);

            // Return JSON result (true if email is unique, false if taken)
            return Json(!emailExists);
        }

        #endregion
    }
}
