using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.Common.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Common.CommonInterface
{
    public interface ICommonRepositore
    {
        Task<ApiResponse<List<CountryDropdownResponce>>> GetCountriesForDropdownAsync();
        Task<ApiResponse<List<StateDropdownResponce>>> GetStateForDropdownAsync(int CountryId);
        Task<ApiResponse<List<CItyDropdownResponce>>> GetCityForDropdownAsync(int StateId);
    }
}
