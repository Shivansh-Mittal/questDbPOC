using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using Npgsql;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using QuestDbPOC.Models;

namespace QuestDbPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestEmpController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            string username = "admin";
            string password = "quest";
            string database = "qdb";
            int port = 8812;
            var connectionString = $@"host=localhost;port={port};username={username};password={password};database={database};ServerCompatibilityMode=NoTypeLoading;";
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            var sql = "SELECT * FROM QEmployee;";

            await using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
            DataTable _dt = new DataTable();
            da.Fill(_dt);
            return Ok(_dt);

        }
        [HttpPost]

        public async Task PostEmp(QEmployee model)
        {
            string username = "admin";
            string password = "quest";
            string database = "qdb";
            int port = 8812;
            var connectionString = $@"host=localhost;port={port};username={username};password={password};database={database};ServerCompatibilityMode=NoTypeLoading;";
            await using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            //var address = IPAddress.Loopback.ToString();
            string sql = $"INSERT INTO QEmployee (Id, Name, Email, Phone, Address) VALUES ({model.Id}, '{model.Name}', '{model.Email}', '{model.Phone}', '{model.Address}')";

            await using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);

            DataTable _dt = new DataTable();
            da.Fill(_dt);

        }

    }
}
