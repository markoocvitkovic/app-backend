using AstraLicenceManager.Dto;
using AstraLicenceManager.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AstraLicenceManager.Service
{
    public interface IAppService
    {
        Task<List<AppDto>> DeleteApp(long id);
        Task<AppDto> GetApp(long id);
        Task<List<AppDto>> GetApps();
        Task<AppDto> SaveApp(AppDto app);
    }
    public class AppService : IAppService
    {
        private readonly IAppService _appService;
        private readonly ILogger<AppService> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public AppService(
            ILogger<AppService> logger,
            IMapper mapper,
            ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<AppDto> GetApp(long id)
        {
            var data = await _applicationDbContext.Apps.Include(a=>a.Company)
                                                       .Include(a=>a.User)
                                                       .FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<AppDto>(data);
        }

        public async Task<List<AppDto>> GetApps()
        {
            var data = await _applicationDbContext.Apps.Include(_a => _a.Company)
                                                       .Include(a=>a.User)
                                                       .ToListAsync();

            return _mapper.Map<List<AppDto>>(data);
        }

        public async Task<AppDto> SaveApp(AppDto appDto)
        {
            var app = _mapper.Map<App>(appDto);

            if (appDto.Id == 0)
            {
                _applicationDbContext.Apps.Add(app);              
               
                await _applicationDbContext.SaveChangesAsync();

                app.InsertDate = DateTime.Now;                
                
                appDto.Id = app.Id;                
                
            }
            else
            {
                var appUpdate = await _applicationDbContext.Apps.FirstOrDefaultAsync(a => a.Id == appDto.Id);

                if (appUpdate != null)
                {
                    appUpdate.Name = appDto.Name;

                    appUpdate.Description = appDto.Description;

                    appUpdate.Type = appDto.Type;                   

                    appUpdate.UpdateDate = DateTime.Now;  
                    
                    appUpdate.InsertUserId = appDto.InsertUserId;

                    appUpdate.UpdateUserId = appDto.UpdateUserId;

                    _applicationDbContext.Apps.Update(appUpdate);

                    await _applicationDbContext.SaveChangesAsync();

                }
            }

            var appSave = await GetApp(appDto.Id);

            return _mapper.Map<AppDto>(appSave);
        }

        public async Task<List<AppDto>> DeleteApp(long id)
        {
            var app = await _applicationDbContext.Apps.FirstOrDefaultAsync(a => a.Id == id);

            if (app == null)
            {
                return _mapper.Map<List<AppDto>>(app);
            }
            else
            {
                _applicationDbContext.Apps.Remove(app);

                await _applicationDbContext.SaveChangesAsync();
            }

            return _mapper.Map<List<AppDto>>(app);
        }
    }
}
