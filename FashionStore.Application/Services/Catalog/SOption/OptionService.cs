using FashionStore.Data.EF;
using FashionStore.Data.Entities;
using FashionStore.ViewModel.Catalog.Option;
using FashionStore.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.Application.Services.Catalog.SOption
{
    public class OptionService : IOptionService
    {
        private readonly FashionStoreDbContext _context;
        public OptionService(FashionStoreDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> CreateOption(CreateOptionRequest request)
        {
            var option = new Option()
            {
                Name = request.Name,
                Value = request.Value
            };

            await _context.Options.AddAsync(option);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<List<OptionVm>>> GetColorOption()
        {
            var listOption = await _context.Options
                .Where(x => x.Name.Equals("Color"))
                .Select(x => new OptionVm()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Value = x.Value
                }).ToListAsync();

            return new ApiSuccessResult<List<OptionVm>>(listOption);
        }

        public async Task<ApiResult<List<OptionVm>>> GetSizeOption()
        {
            var listOption = await _context.Options
                .Where(x => x.Name.Equals("Size"))
                .Select(x => new OptionVm()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Value = x.Value
                }).ToListAsync();

            return new ApiSuccessResult<List<OptionVm>>(listOption);
        }

        public async Task<ApiResult<List<OptionVm>>> GetAllOption()
        {
            var listOption = await _context.Options
                .Select(x => new OptionVm()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Value = x.Value
                }).ToListAsync();

            return new ApiSuccessResult<List<OptionVm>>(listOption);
        }

        public async Task<ApiResult<bool>> UpdateOption(UpdateOptionRequest request)
        {
            var option = await _context.Options.FindAsync(request.Id);
            if (option == null) return new ApiFailedResult<bool>($"Không tồn tại option với Id = {request.Id}");

            option.Value = request.Value;
            option.Name = request.Name;
            _context.Options.Update(option);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }

        public async Task<ApiResult<bool>> DeleteOption(int id)
        {
            var option = await _context.Options.FindAsync(id);
            if (option == null) return new ApiFailedResult<bool>($"Không tồn tại option với Id = {id}");


            _context.Options.Remove(option);
            var result = await _context.SaveChangesAsync() > 0;
            return new ApiSuccessResult<bool>(result);
        }
    }
}
