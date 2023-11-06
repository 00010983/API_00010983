using DSCC_000010983_API.Data;
using DSCC_000010983_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DSCC_000010983_API.Controllers
{
    // Define the route and indicate that this class is an API controller.
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor that receives an instance of the ApplicationDbContext.
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // This action retrieves and returns a list of all customers.
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var customers = _context.Customers.ToList();
                if (customers.Count == 0)
                {
                    // Return a "Not Found" response if no customers are available.
                    return NotFound("Customers not available");
                }
                // Return a successful response with the list of customers.
                return Ok(customers);
            }
            catch (Exception ex)
            {
                // Return a "Bad Request" response if an exception occurs.
                return BadRequest(ex.Message);
            }
        }

        // This action retrieves and returns a customer by their ID.
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer == null)
                {
                    // Return a "Not Found" response if the customer is not found.
                    return NotFound("Customer not found");
                }
                // Return a successful response with the customer data.
                return Ok(customer);
            }
            catch (Exception ex)
            {
                // Return a "Bad Request" response if an exception occurs.
                return BadRequest(ex.Message);
            }
        }

        // This action creates a new customer.
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            try
            {
                _context.Add(customer);
                _context.SaveChanges();
                // Return a successful response after creating the customer.
                return Ok("Customer Created");
            }
            catch (Exception ex)
            {
                // Return a "Bad Request" response if an exception occurs.
                return BadRequest(ex.Message);
            }
        }

        // This action updates an existing customer.
        [HttpPut]
        public IActionResult Put(Customer model)
        {
            try
            {
                var customer = _context.Customers.Find(model.CustomerId);
                if (customer == null)
                {
                    // Return a "Not Found" response if the customer is not found by ID.
                    return NotFound("Customer not found with id " + model.CustomerId);
                }
                // Update the customer's properties and save changes.
                customer.Name = model.Name;
                customer.Email = model.Email;
                customer.Address = model.Address;
                customer.PhoneNumber = model.PhoneNumber;
                customer.DateOfBirth = model.DateOfBirth;
                _context.SaveChanges();
                // Return a successful response after updating the customer.
                return Ok("Customer updated");
            }
            catch (Exception ex)
            {
                // Return a "Bad Request" response if an exception occurs.
                return BadRequest(ex.Message);
            }
        }

        // This action deletes a customer by ID.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer == null)
                {
                    // Return a "Not Found" response if the customer is not found by ID.
                    return NotFound($"Customer not found with id {id}");
                }
                // Remove the customer and save changes.
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                // Return a successful response after deleting the customer.
                return Ok("Customer deleted");
            }
            catch (Exception ex)
            {
                // Return a "Bad Request" response if an exception occurs.
                return BadRequest(ex.Message);
            }
        }
    }
}

