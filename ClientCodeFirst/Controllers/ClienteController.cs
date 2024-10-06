
using System.Data;
using ClientCodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
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
    [HttpGet]
    [Route("[action]")]
    public IEnumerable<Cliente> GetAllMySql()
    {
        string connString = "server=mysql-service; userid=userinfo;pwd=admin;port=3311;database=clientcodedb";
        List<Cliente> clientes = new List<Cliente>();
        using (var conn = new MySqlConnection(connString))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("select * from Cliente",conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente(){
                            IdCliente = (int)reader["IdCliente"],
                            Nombre = (string)reader["Nombre"],
                            CorreoElectronico = (string)reader["CorreoElectronico"],
                            Telefono = (string)reader["Telefono"]
                        };
                        clientes.Add(cliente);
                    }
                }
            }
        }
        return clientes.AsEnumerable();
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
    [HttpGet]
    [Route("[action]")]
    public string hola1()
    {
        return "hola este es un nuevo endpoint hola1";
    }
    [HttpGet]
    [Route("[action]")]
    public string hola2()
    {
        return "hola este es un nuevo endpoint hola2";
    }
}