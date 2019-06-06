using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Films_Form
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : Controller
    {
        private Films1Context db;
        public FilmController(Films1Context context)
        {
            db = context;
        }
       // Films1Context db = new Films1Context();
        // GET: api/Film
        [HttpGet]
        public IActionResult Get()
        {
            var f=  db.Film.ToList();
            if (f != null)
                return Ok(f);
            else return NotFound();
        }

        // GET api/Film/Best
        [HttpGet("Best")]
        public IActionResult GetBest(int id)
        {
            
                var films = db.Film.OrderByDescending(s => s.FImdb);
            var F = films.Take(id).ToList();
            if (F != null)
                return Ok(F);
            else return NotFound();
            
        }

        // GET api/Film/5
        [HttpGet("{id}")]
        public IActionResult GetFilm(int id)
        { 
                var films = db.Film.Where(p => p.FId==id);
            if (films != null)
                return Ok(films);
            else return NotFound();
            
        }

        // GET api/Film/"Genre"
        [HttpGet("Genre")]
        public IActionResult GetGenre(int id)
        {
            string genre = "";
            var films = db.Film.Where(p => p.FId == id).FirstOrDefault();
            var FG = db.FilmsToGenre.Where(x => x.FId == films.FId);
            foreach (FilmsToGenre fg in FG) {
                Genre G = db.Genre.Where(x => x.GId == fg.GId).FirstOrDefault();
                genre += G.GName+", ";
                    }
           
           // var genres = db.Genre.ToList();
            if (genre != null)
                return Ok(genre);
            else return NotFound();
        }


        [HttpGet("Pic")]
        public async Task<IActionResult> GetPicture(int id)
        {
            Film F = db.Film.Where(p => p.FId == id).FirstOrDefault();
            if (F != null)
            {
                string s = Environment.CurrentDirectory + "\\Pic\\" + F.FName + ".jpg";
                Byte[] b =await System.IO.File.ReadAllBytesAsync(s);
                return File(b, "image/jpg");
            }
            else return NotFound();
                
            
        }


        [HttpGet("Movie")]
        public IActionResult GetMovie(int id)
        {
            Film F = db.Film.Where(p => p.FId == id).FirstOrDefault();
            if (F != null)
            {
                string s = Environment.CurrentDirectory + "\\Films\\" + "Адвокат" + ".mkv";
                return PhysicalFile(s, "application/octet-stream");
                
            }
            else return NotFound();


        }

    }
}
