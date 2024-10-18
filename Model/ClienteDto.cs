namespace Projeto_Empresa.Model
{
    public class ClienteDto
    {
        public string Nome { get; set; } = null!;

        public string Telefone { get; set; } = null!;

        public IFormFile DocIdentificacao { get; set; }
    }
}
