using System;
using System.Threading.Tasks;
using TextService.Services.Models;

namespace TextService.Services.TextDapperService
{
    public interface ITextDapperService
    {
        Task<TextModel> AddFile(string text);
        Task<TextModel> GetById(Guid id);
    }
}