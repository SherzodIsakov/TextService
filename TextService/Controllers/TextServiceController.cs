using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TextService.Services.Interfaces;
using TextService.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace TextService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TextServiceController : ControllerBase
    {
        private readonly ITextService _textService;
        private readonly ILogger<TextServiceController> _logger;

        public TextServiceController(ITextService textService, ILogger<TextServiceController> logger)
        {            
            _textService = textService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TextModel>> GetById(Guid id)
        {
            var result = await _textService.GetTextByIdAsync(id);
            return result;
        }
        
        [HttpGet]
        public async Task<IEnumerable<TextModel>> GetAllTexts()
        {
            var token = Request.Headers["Authorization"];
            var result = await _textService.GetAllTextAsync();
            return result;
        }

        [HttpPost("text")]
        public async Task<ActionResult<TextModel>> Post([FromBody] string text)
        {
            var textFile = await _textService.AddTextAsync(text);
            return new OkObjectResult(textFile);
        }

        [HttpPost("file")]
        public async Task<ActionResult<string>> PostFile(IFormFile formFile)
        {
            if (formFile != null)
            {
                var result = await _textService.UploadFileFormDataAsync(formFile);
                return new OkObjectResult(result);
            }

            return new OkObjectResult($"formFile Is Empty");
        }

        [HttpPost("files")]
        public async Task<ActionResult<string>> PostFiles(List<IFormFile> formFiles)
        {
            if (formFiles != null && formFiles.Count > 0)
            {
                string textFilesResult = string.Empty;
                foreach (var streamTextFile in formFiles)
                {
                    textFilesResult += await _textService.UploadFileFormDataAsync(streamTextFile) + ", ";
                }

                return new OkObjectResult(textFilesResult);
            }

            return new OkObjectResult($"formFiles Is Empty");
        }

        [HttpPost("url/{fileUrl}")]
        public async Task<ActionResult<string>> PostFileUrl(string fileUrl)
        {
            var textFile = await _textService.UploadFileFromUriAsync(fileUrl);
            return new OkObjectResult(textFile);
        }

        [HttpPost("file/{streamTextFile}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<string>> PostFile(Stream streamTextFile)
        {
            var textFile = await _textService.UploadFileStreamAsync(streamTextFile);
            return new OkObjectResult(textFile);
        }


    }
}
