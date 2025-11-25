using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using FitLifeAPI.Models;

namespace FitLifeAPI.Data
{
    /// <summary>
    /// Classe responsável pela persistência de dados em arquivo JSON
    /// Simula um banco de dados simples
    /// </summary>
    public class DataContext
    {
        private readonly string _dataFilePath;
        private AppData _data = new AppData();

        public DataContext()
        {
            _dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "data.json");
            CarregarDados();
        }

        /// <summary>
        /// Carrega os dados do arquivo JSON
        /// </summary>
        private void CarregarDados()
        {
            try
            {
                if (File.Exists(_dataFilePath))
                {
                    string jsonString = File.ReadAllText(_dataFilePath);
                    
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters = { new TreinoJsonConverter() }
                    };
                    
                    _data = JsonSerializer.Deserialize<AppData>(jsonString, options) ?? new AppData();
                }
                else
                {
                    _data = new AppData();
                    SalvarDados();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados: {ex.Message}");
                _data = new AppData();
            }
        }

        /// <summary>
        /// Salva os dados no arquivo JSON
        /// </summary>
        public void SalvarDados()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new TreinoJsonConverter() }
                };

                string jsonString = JsonSerializer.Serialize(_data, options);
                
                // Garante que o diretório existe
                string? directory = Path.GetDirectoryName(_dataFilePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(_dataFilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar dados: {ex.Message}");
            }
        }

        // Propriedades para acessar os dados
        public List<Usuario> Usuarios => _data.Usuarios;
        public List<Treino> Treinos => _data.Treinos;
        public List<Alimentacao> Alimentacoes => _data.Alimentacoes;
        public List<Habito> Habitos => _data.Habitos;
    }

    /// <summary>
    /// Classe que representa a estrutura de dados da aplicação
    /// </summary>
    public class AppData
    {
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public List<Treino> Treinos { get; set; } = new List<Treino>();
        public List<Alimentacao> Alimentacoes { get; set; } = new List<Alimentacao>();
        public List<Habito> Habitos { get; set; } = new List<Habito>();
    }

    /// <summary>
    /// Conversor JSON customizado para lidar com herança de Treino
    /// </summary>
    public class TreinoJsonConverter : JsonConverter<Treino>
    {
        public override Treino? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                
                // Determina o tipo baseado nas propriedades
                if (root.TryGetProperty("DistanciaKm", out _) || 
                    root.TryGetProperty("distanciaKm", out _))
                {
                    return JsonSerializer.Deserialize<TreinoCardio>(root.GetRawText(), options);
                }
                else if (root.TryGetProperty("Series", out _) || 
                         root.TryGetProperty("series", out _))
                {
                    return JsonSerializer.Deserialize<TreinoMusculacao>(root.GetRawText(), options);
                }
                else if (root.TryGetProperty("TipoFuncional", out _) || 
                         root.TryGetProperty("tipoFuncional", out _))
                {
                    return JsonSerializer.Deserialize<TreinoFuncional>(root.GetRawText(), options);
                }
                
                return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, Treino value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
