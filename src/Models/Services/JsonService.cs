using AvaloniaCustomTheme.Models;
using AvaloniaCustomTheme.Models.DTOS;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

public class JsonService : IJsonService
{
    public async Task<T> ReadJsonFileAsync<T>(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }

        try
        {
            using (var stream = File.OpenRead(filePath))
            using (var reader = new StreamReader(stream))
            {
                string jsonContent = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(jsonContent);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            throw;
        }
    }

    public async Task WriteJsonFileAsync<T>(string filePath, T data)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
        }

        try
        {
            string jsonContent = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented); // Makes the JSON human-readable

            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream))
            {
                await writer.WriteAsync(jsonContent);
                Console.WriteLine($"JSON file written successfully: {data}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing JSON file: {ex.Message}");
            throw;
        }
    }

    public T ReadJsonFile<T>(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }

        try
        {
            string jsonContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading JSON file: {ex.Message}");
            throw;
        }
    }

    public void WriteJsonFile<T>(string filePath, T data)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));
        }

        try
        {
            string jsonContent = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented); // Makes the JSON human-readable
            File.WriteAllText(filePath, jsonContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing JSON file: {ex.Message}");
            throw;
        }
    }
}
