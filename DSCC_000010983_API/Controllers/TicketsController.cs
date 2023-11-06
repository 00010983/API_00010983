using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DSCC_000010983_API.Data;
using DSCC_000010983_API.Models;

namespace DSCC_000010983_API.Controllers
{
    // Define the route and indicate that this class is an API controller.
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicketsContoller : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor that receives an instance of the ApplicationDbContext.
        public TicketsContoller(ApplicationDbContext context)
        {
            _context = context;
        }

        // This action retrieves and returns a list of all tickets.
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var tickets = _context.Tickets.ToList();
                if (tickets.Count == 0)
                {
                    // Return a "Not Found" response if no tickets are available.
                    return NotFound("Tickets not available");
                }
                // Return a successful response with the list of tickets.
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                // Return a "Bad Request" response if an exception occurs.
                return BadRequest(ex.Message);
            }
        }

        // This action retrieves and returns a ticket by its ID.
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var ticket = _context.Tickets.Find(id);
                if (ticket == null)
                {
                    // Return a "Not Found" response if the ticket is not found.
                    return NotFound("Ticket not found");
                }
                // Return a successful response with the ticket data.
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                // Return a "Bad Request" response if an exception occurs.
                return BadRequest(ex.Message);
            }
        }

        // This action creates a new ticket.
        [HttpPost]
        public IActionResult Post(Ticket ticket)
        {
            try
            {
                _context.Add(ticket);
                _context.SaveChanges();
                // Return a successful response after creating the ticket.
                return Ok("Ticket Created");
            }
            catch (Exception ex)
            {
                // Return a "Bad Request" response if an exception occurs.
                return BadRequest(ex.Message);
            }
        }

        // This action updates an existing ticket.
        [HttpPut]
        public IActionResult Put(Ticket model)
        {
            try
            {
                var ticket = _context.Tickets.Find(model.TicketId);
                if (ticket == null)
                {
                    // Return a "Not Found" response if the ticket is not found by ID.
                    return NotFound("Ticket not found with id " + model.TicketId);
                }
                // Update the ticket's properties and save changes.
                ticket.Title = model.Title;
                ticket.Departure = model.Departure;
                ticket.Arrival = model.Arrival;
                ticket.Priority = model.Priority;
                ticket.DueDate = model.DueDate;
                ticket.Duration = model.Duration;
                ticket.CustomerId = model.CustomerId;
                _context.SaveChanges();
                // Return a successful response after updating the ticket.
                return Ok("Ticket updated");
            }
            catch (Exception ex)
            {
                // Return a "Bad Request" response if an exception occurs.
                return BadRequest(ex.Message);
            }
        }

        // This action deletes a ticket by ID.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ticket = _context.Tickets.Find(id);
                if (ticket == null)
                {
                    // Return a "Not Found" response if the ticket is not found by ID.
                    return NotFound($"Ticket not found with id {id}");
                }
                // Remove the ticket and save changes.
                _context.Tickets.Remove(ticket);
                _context.SaveChanges();
                // Return a successful response after deleting the ticket.
                return Ok("Ticket deleted");
            }
            catch (Exception ex)
            {
                // Return a "Bad Request" response if an exception occurs.
                return BadRequest(ex.Message);
            }
        }
    }
}

