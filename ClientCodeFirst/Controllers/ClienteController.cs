
using ClientCodeFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientCodeFirst.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    
    [HttpGet]
    [Route("[action]")]
    public IEnumerable<Cliente> GetAll()
    {
        using (var _context = new ClientcodedbContext())
        {
            return _context.Clientes.ToList();
        }
    }
    
    [HttpPost]
    [Route("[action]")]
    public void Insert([FromBody] Cliente cliente)
    {
        using (var _context = new ClientcodedbContext())
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }
    }
}