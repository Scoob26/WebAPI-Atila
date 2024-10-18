using Microsoft.EntityFrameworkCore;
using Projeto_Empresa.Model;
using Projeto_Empresa.ORM;

namespace Projeto_Empresa.Repositorio
{
    public class EnderecoRepositorio
    {
        private ProjetoEmpresaContext _context;
        public EnderecoRepositorio(ProjetoEmpresaContext context)
        {
            _context = context;
        }


        public void Add(Endereco endereco)
        {
           

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbEndereco = new TbEndereco()
            {
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoReferencia = endereco.PontoReferencia,
                Nº = endereco.Nº,
                FkCliente = endereco.FkCliente,
                
            };

            // Adiciona a entidade ao contexto
            _context.TbEnderecos.Add(tbEndereco);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEnderecos = _context.TbEnderecos.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbEnderecos != null)
            {
                // Remove a entidade do contexto
                _context.TbEnderecos.Remove(tbEnderecos);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereços não encontrado.");
            }

        }

        public List<Endereco> GetAll()
        {
            List<Endereco> listFun = new List<Endereco>();

            var listTb = _context.TbEnderecos.ToList();

            foreach (var item in listTb)
            {
                var Endereco = new Endereco
                {                   
                    Id = item.Id,
                    Logradouro = item.Logradouro,
                    Cidade = item.Cidade,
                    Estado = item.Estado,
                    Cep = item.Cep,
                    PontoReferencia = item.PontoReferencia,
                    Nº = item.Nº,
                    FkCliente = item.FkCliente,
                    
                    
                };

                listFun.Add(Endereco);
            }

            return listFun;
        }

        public Endereco GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbEnderecos.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var endereco = new Endereco
            {
               Id=item.Id,
                Logradouro = item.Logradouro,
                Cidade = item.Cidade,
                Estado = item.Estado, // Mantém o campo Foto como byte[]
                Cep = item.Cep,
                PontoReferencia= item.PontoReferencia,
                Nº = item.Nº,
                FkCliente= item.FkCliente,
            };

            return endereco; // Retorna o funcionário encontrado
        }

        public void Update(Endereco endereco)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEnderecos = _context.TbEnderecos.FirstOrDefault(f => f.Id == endereco.Id);

            // Verifica se a entidade foi encontrada
            if (tbEnderecos != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbEnderecos.Logradouro = endereco.Logradouro;
                tbEnderecos.Cidade = endereco.Cidade;
                tbEnderecos.Estado = endereco.Estado;
                tbEnderecos.Cep = endereco.Cep;
                tbEnderecos.PontoReferencia = endereco.PontoReferencia;
                tbEnderecos.Nº = endereco.Nº;
                tbEnderecos .FkCliente = endereco.FkCliente;

               

                // Atualiza as informações no contexto
                _context.TbEnderecos.Update(tbEnderecos);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Clientes não encontrado.");
            }
        }



    }

 

}
