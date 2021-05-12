using Microsoft.AspNetCore.Http;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TextService.Entities.Models;

namespace TextService.Client
{
    public interface ITextClient
    {
        [Get("/api/textservice/{id}")]
        Task<TextModel> GetById(Guid id);

        [Get("/api/textservice")]
        Task<IEnumerable<TextModel>> GetAllTexts();

        [Post("/api/textservice/text")]
        Task<TextModel> Post([Body] string text);

        [Post("/api/textservice/file")]
        Task<TextModel> PostFile(IFormFile streamTextFile);        

        [Post("/api/textservice/url/{fileUrl}")]
        Task<TextModel> PostFileUrl([Body] string fileUrl);

        [Post("/api/textservice/file/{streamTextFile}")]
        Task<TextModel> PostFile(Stream streamTextFile);
    }
}
