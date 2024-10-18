using System.Text.Json.Serialization;

namespace Projeto_Empresa.Model
{
    public class ProdutoDto
    {
        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public int Quant { get; set; }
        [JsonIgnore] // Ignora a serialização deste campo
        public IFormFile NotaFiscal { get; set; }
    }
}
