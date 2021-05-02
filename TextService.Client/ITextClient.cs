using Refit;
using System;
using System.IO;
using System.Threading.Tasks;
using TextService.Services.Models;

namespace TextService.Client
{
    public interface ITextClient
    {
        [Get("/text/{id}")]
        Task<TextModel> GetById(Guid id);
        [Post("/text")]
        Task<TextModel> Post([Body] string text);
        [Post("/text/file/{streamTextFile}")]
        Task<TextModel> PostFile(Stream streamTextFile);
        [Post("/text/url/{fileUrl}")]
        Task<TextModel> PostFileUrl([Body] string fileUrl);
    }
}
