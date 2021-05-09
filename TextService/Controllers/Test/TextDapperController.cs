//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Threading.Tasks;
//using TextService.Entities.Models;
//using TextService.Services.Interfaces;

//namespace TextService.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TextDapperController : ControllerBase
//    {
//        private readonly ITextService _textService;
//        private readonly ILogger<TextDapperController> _logger;

//        public TextDapperController(ITextService textService, ILogger<TextDapperController> logger)
//        {
//            _textService = textService;
//            _logger = logger;
//        }

//        [HttpGet("GetById/{id}")]
//        public async Task<ActionResult<TextModel>> GetById(Guid id)
//        {
//            var result = await _textService.GetTextByIdAsync(id);
//            return result;
//        }

//        [HttpGet("GetAll")]
//        public async Task<IEnumerable<TextModel>> GetAll()
//        {
//            var result = await _textService.GetAllTextAsync();
//            return result;
//        }

//        [HttpPost("text")]
//        public async Task<ActionResult<TextModel>> Post([FromBody] string text)
//        {
//            var textFile = await _textService.AddTextAsync(text);
//            return new OkObjectResult(textFile);
//        }

//        [HttpPost("file")]
//        public async Task<ActionResult<string>> PostFile(IFormFile streamTextFile)
//        {
//            var textFile = await _textService.UploadFileFormDataAsync(streamTextFile);
//            return new OkObjectResult(textFile);
//        }

//        [HttpPost("url/{fileUrl}")]
//        public async Task<ActionResult<string>> PostFileUrl(string fileUrl)
//        {
//            var textFile = await _textService.UploadFileFromUriAsync(fileUrl);
//            return new OkObjectResult(textFile);
//        }

//        [HttpPost("file/{streamTextFile}")]
//        [Consumes("multipart/form-data")]
//        public async Task<ActionResult<string>> PostFile(Stream streamTextFile)
//        {
//            var textFile = await _textService.UploadFileStreamAsync(streamTextFile);
//            return new OkObjectResult(textFile);
//        }

//    }
//}
