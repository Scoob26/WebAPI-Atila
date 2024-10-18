using System.Text.Json.Serialization;

namespace Projeto_Empresa.Model
{
    public class Venda
    {
        public int Id { get; set; }

        public decimal Valor { get; set; }       

        public int Fkproduto { get; set; }

        public int Fkcliente { get; set; }

        public byte[]? NotaF { get; set; }

        [JsonIgnore] // Ignora a serialização deste campo
        public string? NotaFBase64 => NotaF != null ? Convert.ToBase64String(NotaF) : null;

        public string UrlNotaF { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}
