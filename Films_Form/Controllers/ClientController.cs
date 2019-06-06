using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Films_Form
{
    [Route("api/Client")]
    [ApiController]
    public class ClientController : Controller
    {
        private Films1Context db;
        public ClientController(Films1Context context)
        {
            db = context;
        }
       // Films1Context db = new Films1Context();

        // GET api/Client/IsRegister
        [HttpGet("IsRegister")]
        public IActionResult GetIsRegister(string login, string password)
        {
            Client t = db.Client.Where(s => s.CLogin == login && s.CPassword == password).FirstOrDefault();
            if (t != null)
            {
                return Ok();
            }
            else
                    {
                return NotFound();

            }
        }
        
        // POST api/Client
        [HttpPost("Register")]
        public async Task<IActionResult> PostRegister([FromBody]Client c)
        {

            Client t = db.Client.Where(s => s.CLogin == c.CLogin && s.CPassword == c.CPassword).FirstOrDefault();
            if (t != null)
            {
                return BadRequest();
            }
            else
            {
                await db.Client.AddAsync(c);
                await db.SaveChangesAsync(); ;
                return Ok();
            }
        }
        // GET api/Client/PData
        [HttpGet("PData")]
        public IActionResult GetPData(string login, string password)
        {

            Client t = db.Client.Where(s => s.CLogin == login && s.CPassword == password).FirstOrDefault();
            if (t != null)
            {
                var cl = db.Client.Where(s => s.CLogin == login && s.CPassword == password).FirstOrDefault();
                return Ok(cl);
            }
            else
            {
                return BadRequest();

            }
        }


        // PUT api/Client/Chandge
        [HttpPut("Chandge")]
        public async Task<IActionResult> PutChandge([FromBody]Client c)
        {

            Client t = db.Client.Where(s => s.CId == c.CId).FirstOrDefault();
            if (t == null) { 
                
               return BadRequest();
            }

            t.CCardNumber = c.CCardNumber;
                t.CCvv = c.CCvv;
                t.CDate = c.CDate;
                t.CFio = c.CFio;
                t.CLogin = c.CLogin;
                t.CPassword = c.CPassword;
            // db.Update(c);
            await db.SaveChangesAsync(); ;
            return Ok();
                

            
        }

        // POST api/Client/Buy
        [HttpPost("Buy")]
        public async Task<IActionResult> PostBuy([FromBody]string json)
        {
            JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            string login = obj["login"].Value<string>();
            string password = obj["password"].Value<string>();
            List<int> F = obj["F"].Value<JArray>().ToObject<List<int>>();
            double Cost = obj["Cost"].Value<double>();
            Client t = db.Client.Where(s => s.CLogin == login && s.CPassword == password).FirstOrDefault();
            if (t != null)
            {
                Client c = db.Client.Where(s => s.CLogin == login && s.CPassword == password).FirstOrDefault();
                Buy B = new Buy { CId = c.CId, BPrice = Convert.ToInt32(Cost) };
                await db.Buy.AddAsync(B);
               await db.SaveChangesAsync();
                List<Film> f = new List<Film>();
                foreach(int p in F)
                {
                    int id = p;
                    Film film = db.Film.Where(x => x.FId == id).FirstOrDefault();
                    f.Add(film);
                }
                foreach (Film l in f)
                {
                    Purchase P = new Purchase { BId = B.BId, FId = l.FId };
                    await db.Purchase.AddAsync(P);
                }
               await  db.SaveChangesAsync();
                return Ok();
            }
            else
            {

                return BadRequest();
            }
        }
        // GET api/Client/Histrory
        [HttpGet("History")]
        public IActionResult GetHistory(string login, string password)
        {
            Client t = db.Client.Where(s => s.CLogin == login && s.CPassword == password).FirstOrDefault();
            if (t != null)
            {
                var cl = db.Client.Where(s => s.CLogin == login && s.CPassword == password).FirstOrDefault();
                var B = db.Buy.Where(s => s.CId == cl.CId);
                if (B != null)
                {

                    List<Purchase> Pu = new List<Purchase>();
                    foreach (Buy b in B)
                    {
                         Pu.AddRange( db.Purchase.Where(s => s.BId == b.BId).ToList());
                        
                    }
                    var Pp = Pu.Select(s => new
                        {
                            BId = s.BId,
                            FId = s.FId
                        }).ToList();
                    if (Pu != null)
                        return Ok(Pp);
                    else return NotFound();
                }
                else return NotFound(); ;
            }
            else
            {
                return BadRequest();

            }
        }
    }
}
