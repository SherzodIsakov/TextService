using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TextService.Entities.Models;

namespace TextService.Services.Interfaces
{
    public interface ITextService
    {
        Task<TextModel> AddTextAsync(string text);
        Task<string> UploadFileFormDataAsync(HttpRequest httpRequest);
        Task<string> UploadFileFormDataAsync(IFormFile file);
        Task<string> UploadFileBinaryAsync(HttpRequest httpRequest);
        Task<string> UploadFileStreamAsync(Stream stream);
        Task<string> UploadFileFromUriAsync(string uriValue);

        Task<TextModel> GetTextByIdAsync(Guid id);
        Task<IEnumerable<TextModel>> GetAllTextAsync();
    }
}