using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TextService.Entities.Models;
using TextService.Repositories.Entities;
using TextService.Repositories.Interfaces;
using TextService.Services.Interfaces;

namespace TextService.Services.TextDapperService
{
    public class TextDapperService : ITextService
    {
        private readonly ITextDapperRepository _textRepository;
        private readonly IMapper _mapper;

        public TextDapperService(ITextDapperRepository textRepository, IMapper mapper)
        {
            _textRepository = textRepository;
            _mapper = mapper;
        }

        public async Task<TextModel> AddTextAsync(string text)
        {
            var textFile = new TextEntity { Text = text };
            textFile = await _textRepository.CreateAsync(textFile);

            return _mapper.Map<TextModel>(textFile);
        }
        public async Task<string> UploadFileFormDataAsync(HttpRequest httpRequest)
        {
            try
            {
                IFormFile file = httpRequest.Form.Files[0];
                if (file.Length > 0)
                {
                    using (var sr = new StreamReader(file.OpenReadStream()))
                    {
                        var body = await sr.ReadToEndAsync();

                        await this.AddTextAsync(body);
                    }
                    return "Файл загружен";
                }
                else
                {
                    return "Файл не загружен";
                }

            }
            catch (Exception ex)
            {
                return $"Файл не загружен {ex.Message}";
            }
        }
        public async Task<string> UploadFileFormDataAsync(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    using (var sr = new StreamReader(file.OpenReadStream()))
                    {
                        var body = await sr.ReadToEndAsync();

                        await this.AddTextAsync(body);
                    }

                    return "Файл загружен";
                }
                else
                {
                    return "Файл не загружен";
                }
            }
            catch (Exception ex)
            {
                return $"Файл не загружен {ex.Message}";
            }
        }
        public async Task<string> UploadFileBinaryAsync(HttpRequest httpRequest)
        {
            try
            {
                var stream = httpRequest.Body;
                using (var sr = new StreamReader(stream))
                {
                    var body = await sr.ReadToEndAsync();

                    await this.AddTextAsync(body);
                }
                return "Файл загружен";
            }
            catch (Exception ex)
            {
                return $"Файл не загружен {ex.Message}";
            }
        }
        public async Task<string> UploadFileStreamAsync(Stream stream)
        {
            try
            {
                using (var sr = new StreamReader(stream))
                {
                    var body = await sr.ReadToEndAsync();

                    await this.AddTextAsync(body);
                }
                return "Файл загружен";
            }
            catch (Exception ex)
            {
                return $"Файл не загружен {ex.Message}";
            }
        }
        public async Task<string> UploadFileFromUriAsync(string uriValue)
        {
            try
            {
                Uri filePath;
                if (Uri.TryCreate(uriValue, UriKind.Absolute, out filePath))
                {
                    filePath = new Uri(uriValue);

                    string filename = System.IO.Path.GetFileName(filePath.AbsolutePath);
                    string ex = System.IO.Path.GetExtension(filename);

                    if (ex == ".txt")
                    {
                        using (System.Net.WebClient wc = new System.Net.WebClient())
                        {
                            var body = wc.DownloadString(filePath);

                            await this.AddTextAsync(body);
                        }
                        return "Файл успешно загружен";
                    }

                    return $"Не корректный формат файла {filename}";

                }
                else
                {
                    return $"Не корректный Uri {uriValue}";
                }
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<TextModel> GetTextByIdAsync(Guid id)
        {
            var text = await _textRepository.GetByIdAsync(id);

            return _mapper.Map<TextModel>(text);
        }
        public async Task<IEnumerable<TextModel>> GetAllTextAsync()
        {
            var text = await _textRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<TextEntity>, IEnumerable<TextModel>>(text);
        }
    }
}