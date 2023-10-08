using AstraLicenceManager.Dto;
using AstraLicenceManager.Entities;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace AstraLicenceManager.Service
{
    public interface ILicenceService
    {
            Task<List<LicenceDto>> DeleteLicence(long id);
            Task<LicenceDto> GetLicence(long id);
            Task<List<LicenceDto>> GetLicences();
            Task<LicenceDto> SaveLicence(LicenceDto licence);
            Task<LicenceInfoDto> Check(long appId, long id , string deviceId);

    }
    public class LicenceService : ILicenceService
    {

        private readonly ILicenceService _licenceService;
        private readonly ILogger<LicenceService> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public LicenceService(
            ILogger<LicenceService> logger,
            IMapper mapper,
            ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<LicenceDto> GetLicence(long id)
        {
            var data = await _applicationDbContext.Licences.Include(a=>a.User)
                                                           .Include(p => p.Company)
                                                           .Include(p => p.App)
                                                           .FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<LicenceDto>(data);
        }

        public async Task<List<LicenceDto>> GetLicences()
        {
            var data = await _applicationDbContext.Licences.Include(a => a.User)
                                                           .Include(p => p.Company)
                                                           .Include(p => p.App)
                                                           .ToListAsync();

            return _mapper.Map<List<LicenceDto>>(data);
        }

        public async Task<LicenceDto> SaveLicence(LicenceDto licenceDto)
        {
            var licence = _mapper.Map<Licence>(licenceDto);

            if (licenceDto.Id == 0)
            {
                _applicationDbContext.Licences.Add(licence);

                await _applicationDbContext.SaveChangesAsync();

                licence.InsertDate = DateTime.Now;
                
                licenceDto.Id = licence.Id;
            }
            else
            {
                var licenceUpdate = await _applicationDbContext.Licences.FirstOrDefaultAsync(a => a.Id == licenceDto.Id);

                if (licenceUpdate != null)
                {
                    licenceUpdate.Code = licenceDto.Code;

                    licenceUpdate.Description = licenceDto.Description;

                    licenceUpdate.Active = licenceDto.Active;

                    licenceUpdate.Update = licenceDto.Update;

                    licenceUpdate.Permanent = licenceDto.Permanent;

                    licenceUpdate.FirstCheck =licenceDto.FirstCheck;                  

                    licenceUpdate.UpdateDate = DateTime.Now;

                    licenceUpdate.InsertUserId = licenceDto.InsertUserId;

                    licenceUpdate.UpdateUserId = licenceDto.UpdateUserId;

                    _applicationDbContext.Licences.Update(licenceUpdate);

                    await _applicationDbContext.SaveChangesAsync();

                }
            }

            var licenceSave = await GetLicence(licenceDto.Id);

            return _mapper.Map<LicenceDto>(licenceSave);
        }

        public async Task<List<LicenceDto>> DeleteLicence(long id)
        {
            var licence = await _applicationDbContext.Licences.FirstOrDefaultAsync(a => a.Id == id);

            if (licence == null)
            {
                return _mapper.Map<List<LicenceDto>>(licence);
            }
            else
            {
                _applicationDbContext.Licences.Remove(licence);

                await _applicationDbContext.SaveChangesAsync();
            }

            return _mapper.Map<List<LicenceDto>>(licence);
        }

        public async Task<LicenceInfoDto> Check(long appId, long id , string deviceId )
        {     
                
            var licEntity = await GetLicence(id);

            var lic = new LicenceInfoDto(); 

            if (licEntity == null)
            {
                lic.Status = LicenceStatus.NotExisting;
                return lic; 
            }

            if (licEntity.AppId != appId)
            {
                lic.Status = LicenceStatus.WrongApp;
                return lic; 
            }
            else if (licEntity.DeviceId.ToUpper() != deviceId.ToUpper())
            {
                lic.Status = LicenceStatus.WrongDevice;
                return lic;
            }
            else if(licEntity.Active == false)
            {
                lic.Status = LicenceStatus.NotActive;
                return lic;
            }
            else
            {
                lic.Status = LicenceStatus.Ok;                
                return lic;
            }
            
        }

    }
}
