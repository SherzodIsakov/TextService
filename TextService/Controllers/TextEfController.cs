using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using TextService.Services.Models;
using TextService.Services.TextEfService;

namespace TextService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextEfController : ControllerBase
    {
        private readonly ITextEfService _textService;
        private readonly ILogger<TextDapperController> _logger;

        public TextEfController(ITextEfService textService, ILogger<TextDapperController> logger)
        {
            _textService = textService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TextModel>> GetById(Guid id)
        {
            return await _textService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<TextModel>> Post([FromBody] string text)
        {
            var textFile = await _textService.AddFile(text);
            return new OkObjectResult(textFile);
        }

        [HttpPost("file/{streamTextFile}")]
        public ActionResult<TextModel> PostFile(Stream streamTextFile)
        {
            var textFile = new TextModel();
            return new OkObjectResult(textFile);
        }

        [HttpPost("url/{fileUrl}")]
        public ActionResult<TextModel> PostFileUrl(string textFileUrl)
        {
            var textFile = new TextModel();
            return new OkObjectResult(textFile);
        }
    }
}
