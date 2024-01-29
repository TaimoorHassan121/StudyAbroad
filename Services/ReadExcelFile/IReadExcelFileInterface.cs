using Microsoft.AspNetCore.Http;
using Study_Abroad.DTO;
using Study_Abroad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Abroad.Services.ReadExcelFile
{
    public interface IReadExcelFileInterface
    {
        Task<List<string>> ReadDisiplineExcelFile(IFormFile file);
        Task<List<string>> ReadCountryExcelFile(IFormFile file);
        Task<List<ExcelStateDto>> ReadStateExcelFile(IFormFile file);
        Task<List<ExcelUniDto>> ReadUniExcelFile(IFormFile file);
        Task<List<ExcelUniDto>> ReadUniCampusExcelFile(IFormFile file);
        Task<List<Course>> ReadCoursesExcelFile(IFormFile file);
    }
}
