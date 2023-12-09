using pedagangpulsa.api.Context;
using pedagangpulsa.api.Domain.Entities;
using pedagangpulsa.api.DTO.Request;
using Serilog;
using AutoMapper;
using pedagangpulsa.api.DTO.Respond;


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
            BaseRespond baseRespond = null;
            try {
                var data = _mapper.Map<konter>(dt);
                dbPpContext.Add(data);
                dbPpContext.SaveChanges();
                baseRespond.status = true;
                baseRespond.Message = "register sukses";
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                baseRespond.Message = ex.Message + " " + ex.Data.ToString + " " + ex.Source + " " + ex.InnerException;
            }

            return baseRespond;  
        }

        public BaseRespond login(LoginDTO dt)
        {
            string result = "";
            BaseRespond baseRespond = null;
            try
            {

                var data = _mapper.Map<konter>(dt);
                dbPpContext.Add(data);
                dbPpContext.SaveChanges();
                baseRespond.status = true;
                baseRespond.Message = "register sukses";
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                baseRespond.Message = ex.Message + " " + ex.Data.ToString + " " + ex.Source + " " + ex.InnerException;
            }

            return baseRespond;
        }
    }
}
