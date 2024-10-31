using AvaloniaCustomTheme.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaCustomTheme.Models
{
    public interface IJsonService
    {
        Task<T> ReadJsonFileAsync<T>(string filePath);
        Task WriteJsonFileAsync<T>(string filePath, T data);
        public T ReadJsonFile<T>(string filePath);
        public void WriteJsonFile<T>(string filePath, T data);

    }
}