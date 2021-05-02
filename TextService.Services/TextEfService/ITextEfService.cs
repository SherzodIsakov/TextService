using System;
using System.Threading.Tasks;
using TextService.Services.Models;

namespace TextService.Services.TextEfService
{
    public interface ITextEfService
    {
        Task<TextModel> AddFile(string text);
        Task<TextModel> GetById(Guid id);
    }
}