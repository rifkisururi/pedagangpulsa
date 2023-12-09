using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pedagangpulsa.api.Context;
using pedagangpulsa.api.Domain.Entities;
using pedagangpulsa.api.DTO.Request;
using pedagangpulsa.api.Service;

namespace pedagangpulsa.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KonterController : Controller
    {

        private dbPedagangPulsaContext dbPedagangPulsa;
        private KonterService konterSvc;
        public KonterController(dbPedagangPulsaContext context, KonterService _konterSvc)
        {
            dbPedagangPulsa = context;
            konterSvc = _konterSvc;
        }

        // GET: KonterController
        [Route("AllKonter")]
        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            var data = dbPedagangPulsa.Konter.ToList();
            return data;
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterDTO data)
        {
            var register = konterSvc.register(data);
            return Ok(register);
        }

        [Route("login")]
        [HttpPost]
        public IActionResult login(LoginDTO data)
        {
            var register = konterSvc.register(data);
            return Ok(register);
        }


    }
}
