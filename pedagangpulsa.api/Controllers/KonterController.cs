using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pedagangpulsa.api.Context;
using pedagangpulsa.api.Domain.Entities;
using pedagangpulsa.api.DTO.Request;
using pedagangpulsa.api.DTO.Respond;
using pedagangpulsa.api.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace pedagangpulsa.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KonterController : Controller
    {

        private dbPedagangPulsaContext dbPedagangPulsa;
        private KonterService konterSvc; 
        private IConfiguration config;
        public KonterController(dbPedagangPulsaContext context, KonterService _konterSvc, IConfiguration _config)
        {
            dbPedagangPulsa = context;
            konterSvc = _konterSvc;
            config = _config;
        }

        // GET: KonterController
        //[Route("AllKonter")]
        //[HttpGet]
        //public IEnumerable<dynamic> Get()
        //{
        //    var data = dbPedagangPulsa.Konter.ToList();
        //    return data;
        //}

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
            var konterData = konterSvc.login(data);
            ResponsTokenDto respond = new ResponsTokenDto();
            respond.Status = false;
            respond.Message = "Login gagal";
            
            if (konterData != null) {
                respond.Token = Generate(konterData);
                respond.Status = true;
                respond.Message = "Login sukses";
            }
            
            return Ok(respond);
        }

        private string Generate(konter data)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, data.Email),
                new Claim(ClaimTypes.Name, data.Nama),
                new Claim(ClaimTypes.Role, "konter")
            };

            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
