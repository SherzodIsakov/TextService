﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TextService.Services.Interfaces;
using TextService.Services.Models;

namespace TextService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextEfController : ControllerBase
    {
        private readonly ITextService _textService;
        private readonly ILogger<TextEfController> _logger;

        public TextEfController(ITextService textService, ILogger<TextEfController> logger)
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
        public async Task<IEnumerable<TextModel>> GetAll()
        {
            var result = await _textService.GetAllTextAsync();
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<TextModel>> Post([FromBody] string text)
        {
            var textFile = await _textService.AddTextAsync(text);
            return new OkObjectResult(textFile);
        }

        [HttpPost("file/{streamTextFile}")]
        public async Task<ActionResult<string>> PostFile(Stream streamTextFile)
        {
            var textFile = await _textService.UploadFileStreamAsync(streamTextFile);
            return new OkObjectResult(textFile);
        }

        [HttpPost("url/{fileUrl}")]
        public async Task<ActionResult<string>> PostFileUrl(string fileUrl)
        {
            var textFile = await _textService.UploadFileFromUriAsync(fileUrl);
            return new OkObjectResult(textFile);
        }
    }
}