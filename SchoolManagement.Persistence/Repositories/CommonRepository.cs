using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.CommonInterface;
using SchoolManagement.Application.Common.Models;
using SchoolManagement.Application.Common.Models.DTO_s;
using SchoolManagement.Application.Employees.Interfaces;
using SchoolManagement.Application.Employees.Models.DTO_s;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Persistence.Repositories
{
    public class CommonRepository : ICommonRepositore
    {
        private readonly SchoolDbContext _context;
        public CommonRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<CountryDropdownResponce>>> GetCountriesForDropdownAsync()
        {
            try
            {
                var countries = await _context.Countries
                     .AsNoTracking()
                     .Where(x => x.IsDeleted != true)
                    .Select(c => new CountryDropdownResponce
                    {
                        CountryId = c.Id,
                        CountryName = c.CountryName
                    }).ToListAsync();


                return new ApiResponse<List<CountryDropdownResponce>>
                {
                    Success = true,
                    Message = "CountryName fetched successfully.",
                    Data = countries,
                };
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return new ApiResponse<List<CountryDropdownResponce>>
                {
                    Success = false,
                    Message = "An error occurred while fetching countries.",
                    Data = null
                };
            }

        }

        public async Task<ApiResponse<List<StateDropdownResponce>>> GetStateForDropdownAsync(int CountryId)
        {
            try
            {
                var states = await _context.States
                     .AsNoTracking()
                     .Where(x => x.IsDeleted != true && x.CountryId == CountryId)
                    .Select(c => new StateDropdownResponce
                    {
                        StateId = c.Id,
                        StateName = c.StateName
                    }).ToListAsync();


                return new ApiResponse<List<StateDropdownResponce>>
                {
                    Success = true,
                    Message = "StateName fetched successfully.",
                    Data = states,
                };
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return new ApiResponse<List<StateDropdownResponce>>
                {
                    Success = false,
                    Message = "An error occurred while fetching states.",
                    Data = null
                };
            }

        }

        public async Task<ApiResponse<List<CItyDropdownResponce>>> GetCityForDropdownAsync(int StateId)
        {
            try
            {
                var cities = await _context.Cities
                     .AsNoTracking()
                     .Where(x => x.IsDeleted != true && x.StateId == StateId)
                    .Select(c => new CItyDropdownResponce
                    {
                        CityId = c.Id,
                        CityName = c.CityName
                    }).ToListAsync();


                return new ApiResponse<List<CItyDropdownResponce>>
                {
                    Success = true,
                    Message = "CityName fetched successfully.",
                    Data = cities,
                };
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return new ApiResponse<List<CItyDropdownResponce>>
                {
                    Success = false,
                    Message = "An error occurred while fetching cities.",
                    Data = null
                };
            }

        }
    }
}
