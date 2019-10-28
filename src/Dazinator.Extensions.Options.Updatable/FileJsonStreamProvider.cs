﻿using System.IO;

namespace Dazinator.Extensions.WritableOptions
{
    public class FileJsonStreamProvider<TOptions> : Dazinator.Extensions.WritableOptions.IJsonStreamProvider<TOptions>
       where TOptions : class, new()
    {
        private readonly string _baseDirectory;
        private readonly string _filePath;
        private readonly string _fullFilePath;

        public FileJsonStreamProvider(string baseDirectory, string filePath)
        {
            _baseDirectory = baseDirectory;
            _filePath = filePath;
            _fullFilePath = System.IO.Path.Combine(_baseDirectory, _filePath);
        }

        public Stream OpenReadStream()
        {
            if (File.Exists(_fullFilePath))
            {
                return File.OpenRead(_fullFilePath);
            }
            else
            {
                var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("{}")); // empty json object.
                return memoryStream;
            }
        }

        public Stream OpenWriteStream()
        {
            var filePath = System.IO.Path.Combine(_baseDirectory, _filePath);
            return File.OpenWrite(filePath);
        }
    }
}
