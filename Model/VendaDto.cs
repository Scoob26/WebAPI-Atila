namespace Projeto_Empresa.Model
{
    public class VendaDto
    {
        public decimal Valor { get; set; }

        public int Fkproduto { get; set; }

        public int Fkcliente { get; set; }

        public IFormFile NotaF { get; set; }
    }
}
