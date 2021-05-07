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
        [Get("/text/GetById/{id}")]
        Task<TextModel> GetById(Guid id);
        [Get("/text/GetAll")]
        Task<IEnumerable<TextModel>> GetAll();

        [Post("/text/text")]
        Task<TextModel> Post([Body] string text);

        [Post("/text/file")]
        Task<TextModel> PostFile(IFormFile streamTextFile);        

        [Post("/text/url/{fileUrl}")]
        Task<TextModel> PostFileUrl([Body] string fileUrl);

        [Post("/text/file/{streamTextFile}")]
        Task<TextModel> PostFile(Stream streamTextFile);
    }
}
