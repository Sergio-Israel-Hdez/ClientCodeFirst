
using System.Data;
using ClientCodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
namespace ClientCodeFirst.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{

    private readonly string connString;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ClienteController> _logger;
    public ClienteController(IConfiguration configuration, ILogger<ClienteController> logger)
    {
        _logger = logger;
        var host = _configuration["DBHOST"];
        var port = _configuration["DBPORT"];
        var password = _configuration["MYSQL_PASSWORD"];
        var userid = _configuration["MYSQL_USER"];
        var usersDataBase = _configuration["MYSQL_DATABASE"];

        connString = $"server={host}; userid={userid};pwd={password};port={port};database={usersDataBase}";
    }
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

}