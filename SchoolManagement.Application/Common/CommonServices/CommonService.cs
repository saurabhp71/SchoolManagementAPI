using SchoolManagement.Application.Common.CommonInterface;
using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.Common.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Common.CommonServices
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepositore _commonRepository;
        public CommonService(ICommonRepositore commonRepositore)
        {
            _commonRepository = commonRepositore;
        }

        public async Task<ApiResponse<List<CountryDropdownResponce>>> GetCountriesForDropdownAsync()
        {
            try
            {
                return await _commonRepository.GetCountriesForDropdownAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<List<StateDropdownResponce>>> GetStateForDropdownAsync(int CountryId)
        {
            try
            {
                return await _commonRepository.GetStateForDropdownAsync(CountryId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ApiResponse<List<CItyDropdownResponce>>> GetCityForDropdownAsync(int StateId)
        {
            try
            {
                return await _commonRepository.GetCityForDropdownAsync(StateId);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
