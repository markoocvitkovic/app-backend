using AstraLicenceManager.Dto;
using AstraLicenceManager.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AstraLicenceManager.Service
{

    public interface ICompanyService
    {
        Task<List<CompanyDto>> DeleteCompany(long id);
        Task<CompanyDto> GetCompany(long id);
        Task<List<CompanyDto>> GetCompanies();
        Task<CompanyDto> SaveCompany(CompanyDto company);
    }
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyService> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public CompanyService(
            ILogger<CompanyService> logger,
            IMapper mapper,
            ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CompanyDto> GetCompany(long id)
        {
            var data = await _applicationDbContext.Companies.Include(a=>a.User)
                                                            .FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<CompanyDto>(data);
        }

        public async Task<List<CompanyDto>> GetCompanies()
        {
            var data = await _applicationDbContext.Companies.Include(a=>a.User)
                                                            .ToListAsync();

            return _mapper.Map<List<CompanyDto>>(data);
        }

        public async Task<CompanyDto> SaveCompany(CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);

            if (companyDto.Id == 0)
            {
                _applicationDbContext.Companies.Add(company);

                await _applicationDbContext.SaveChangesAsync();

                company.InsertDate = DateTime.Now;
                
                companyDto.Id = company.Id;
            }
            else
            {
                var companyUpdate = await _applicationDbContext.Companies.FirstOrDefaultAsync(a => a.Id == companyDto.Id);

                if (companyUpdate != null)
                {
                    companyUpdate.Name = companyDto.Name;

                    companyUpdate.Address = companyDto.Address;

                    companyUpdate.City = companyDto.City;

                    companyUpdate.ZipCode = companyDto.ZipCode;

                    companyUpdate.Country = companyDto.Country;

                    companyUpdate.Email = companyDto.Email;

                    companyUpdate.Director = companyDto.Director;

                    companyUpdate.UpdateDate = DateTime.Now;

                    companyUpdate.InsertUserId = companyDto.InsertUserId;

                    companyUpdate.UpdateUserId = companyDto.UpdateUserId;

                    _applicationDbContext.Companies.Update(companyUpdate);

                    await _applicationDbContext.SaveChangesAsync();

                }
            }

            var companySave = await GetCompany(companyDto.Id);

            return _mapper.Map<CompanyDto>(companySave);
        }

        public async Task<List<CompanyDto>> DeleteCompany(long id)
        {
            var company = await _applicationDbContext.Companies.FirstOrDefaultAsync(a => a.Id == id);

            if (company == null)
            {
                return _mapper.Map<List<CompanyDto>>(company);
            }
            else
            {
                _applicationDbContext.Companies.Remove(company);

                await _applicationDbContext.SaveChangesAsync();
            }

            return _mapper.Map<List<CompanyDto>>(company);
        }
    }
}
