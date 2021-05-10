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
        [Get("/textservice/{id}")]
        Task<TextModel> GetById(Guid id);

        [Get("/textservice")]
        Task<IEnumerable<TextModel>> GetAllTexts();

        [Post("/textservice/text")]
        Task<TextModel> Post([Body] string text);

        [Post("/textservice/file")]
        Task<TextModel> PostFile(IFormFile streamTextFile);        

        [Post("/textservice/url/{fileUrl}")]
        Task<TextModel> PostFileUrl([Body] string fileUrl);

        [Post("/textservice/file/{streamTextFile}")]
        Task<TextModel> PostFile(Stream streamTextFile);
    }
}
