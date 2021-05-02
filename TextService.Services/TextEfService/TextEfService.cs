using AutoMapper;
using System;
using System.Threading.Tasks;
using TextService.Repositories.Entities;
using TextService.Repositories.Interfaces;
using TextService.Services.Models;

namespace TextService.Services.TextEfService
{
    public class TextEfService : ITextEfService
    {
        private readonly ITextEfRepository _textRepository;
        private readonly IMapper _mapper;

        public TextEfService(ITextEfRepository textRepository,
        IMapper mapper)
        {
            _textRepository = textRepository;
            _mapper = mapper;
        }

        public async Task<TextModel> AddFile(string text)
        {
            var textFile = new TextEntity();
            textFile.Text = text;

            textFile = await _textRepository.CreateAsync(textFile);
            textFile.Text = null;

            return _mapper.Map<TextModel>(textFile);
        }

        public async Task<TextModel> GetById(Guid id)
        {
            var text = await _textRepository.GetByIdAsync(id);
            return _mapper.Map<TextModel>(text);
        }
    }
}