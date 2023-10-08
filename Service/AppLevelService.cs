using AstraLicenceManager.Dto;
using AstraLicenceManager.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AstraLicenceManager.Service
{
    public interface IAppLevelService
    {
        Task<List<AppLevelDto>> DeleteAppLevel(long id);
        Task<AppLevelDto> GetAppLevel(long id);
        Task<List<AppLevelDto>> GetAppLevels();
        Task<AppLevelDto> SaveAppLevel(AppLevelDto appLevel);
    }
    public class AppLevelService : IAppLevelService
    {
        private readonly IAppLevelService _appLevelService;
        private readonly ILogger<AppLevelService> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public AppLevelService(
            ILogger<AppLevelService> logger,
            IMapper mapper,
            ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<AppLevelDto> GetAppLevel(long id)
        {
            var data = await _applicationDbContext.AppLevels.Include(a=>a.User)
                                                            .Include(a => a.Company)
                                                            .Include(a=>a.App)
                                                            .FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<AppLevelDto>(data);
        }

        public async Task<List<AppLevelDto>> GetAppLevels()
        {
            var data = await _applicationDbContext.AppLevels.Include(a=>a.User)
                                                            .Include(p => p.Company)
                                                            .Include(p => p.App)
                                                            .ToListAsync();

            return _mapper.Map<List<AppLevelDto>>(data);
        }

        public async Task<AppLevelDto> SaveAppLevel(AppLevelDto appLevelDto)
        {
            var appLevel = _mapper.Map<AppLevel>(appLevelDto);

            if (appLevelDto.Id == 0)
            {
                 appLevel.InsertDate = DateTime.Now;
                
                _applicationDbContext.AppLevels.Add(appLevel);

                await _applicationDbContext.SaveChangesAsync();                
                
                appLevelDto.Id = appLevel.Id;
               
            }
            else
            {
                var appLevelUpdate = await _applicationDbContext.AppLevels.FirstOrDefaultAsync(a => a.Id == appLevelDto.Id);

                if (appLevelUpdate != null)
                {
                    appLevelUpdate.Value = appLevelDto.Value;

                    appLevelUpdate.Description = appLevelDto.Description;

                    appLevelUpdate.AppId= appLevelDto.AppId;

                    appLevelUpdate.CompanyId = appLevelDto.CompanyId;

                    appLevelUpdate.UpdateDate = DateTime.Now;

                    appLevelUpdate.InsertUserId = appLevelDto.InsertUserId;

                    appLevelUpdate.UpdateUserId = appLevelDto.UpdateUserId;

                    _applicationDbContext.AppLevels.Update(appLevelUpdate);

                    await _applicationDbContext.SaveChangesAsync();

                }
            }

            var appLevelSave = await GetAppLevel(appLevelDto.Id);

            return _mapper.Map<AppLevelDto>(appLevelSave);
        }

        public async Task<List<AppLevelDto>> DeleteAppLevel(long id)
        {
            var appLevel = await _applicationDbContext.Apps.FirstOrDefaultAsync(a => a.Id == id);

            if (appLevel == null)
            {
                return _mapper.Map<List<AppLevelDto>>(appLevel);
            }
            else
            {
                _applicationDbContext.Apps.Remove(appLevel);

                await _applicationDbContext.SaveChangesAsync();
            }

            return _mapper.Map<List<AppLevelDto>>(appLevel);
        }
    }
}
