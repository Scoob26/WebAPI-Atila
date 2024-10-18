using System.Text.Json.Serialization;

namespace Projeto_Empresa.Model
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public int Quant { get; set; }
        [JsonIgnore] // Ignora a serialização deste campo
        public byte[]? NotaFiscal { get; set; }
        [JsonIgnore] // Ignora a serialização deste campo
        public string? NotaFiscalBase64 => NotaFiscal != null ? Convert.ToBase64String(NotaFiscal) : null;

        public string UrlNotaFiscal { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}
