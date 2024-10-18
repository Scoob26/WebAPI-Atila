namespace Projeto_Empresa.Model
{
    public class Endereco
    {
        public int Id { get; set; }

        public string Logradouro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Cep { get; set; }

        public string PontoReferencia { get; set; }

        public int Nº { get; set; } 

        public int FkCliente { get; set; }


    }
}