using pedagangpulsa.api.Context;
using pedagangpulsa.api.Domain.Entities;
using pedagangpulsa.api.DTO.Request;
using Serilog;
using AutoMapper;
using pedagangpulsa.api.DTO.Respond;
using System.Security.Cryptography;
using System.Text;


namespace pedagangpulsa.api.Service
{
    public class KonterService
    {
        private dbPedagangPulsaContext dbPpContext;
        ILogger<KonterService> log; 
        private readonly IMapper _mapper;

        public KonterService(dbPedagangPulsaContext _dbPpContext, ILogger<KonterService> _log, IMapper mapper) {
            log = _log;
            dbPpContext = _dbPpContext;
            _mapper = mapper;

        }

        public BaseRespond register(RegisterDTO dt) {
            string result = "";
            BaseRespond baseRespond = new BaseRespond();
            try {
                var data = _mapper.Map<konter>(dt);
                data.Password = CalculateMD5Hash(data.Password);
                dbPpContext.Add(data);
                dbPpContext.SaveChanges();
                baseRespond.Status = true;
                baseRespond.Message = "register sukses";
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                baseRespond.Message = ex.Message + " " + ex.Data.ToString + " " + ex.Source + " " + ex.InnerException;
            }

            return baseRespond;  
        }

        public konter login(LoginDTO dt)
        {
            string result = "";
            konter respond = new konter();
            try
            {
                string hashPassword = CalculateMD5Hash(dt.Password);
                respond = dbPpContext.Konter.Where(a => a.PhoneNumber == dt.PhoneNumber && hashPassword == a.Password).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }

            return respond;
        }

        private static string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

    }
}
