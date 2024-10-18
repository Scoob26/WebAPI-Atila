using Microsoft.EntityFrameworkCore;
using Projeto_Empresa.Model;
using Projeto_Empresa.ORM;

namespace Projeto_Empresa.Repositorio
{
    public class ProdutoRepositorio
    {
        private ProjetoEmpresaContext _context;
        public ProdutoRepositorio(ProjetoEmpresaContext context)
        {
            _context = context;
        }

        public void Add(Produto produto, IFormFile DocIdentificacao)
        {
            // Verifica se uma foto foi enviada
            byte[] DocIdentificacaoBytes = null;
            if (DocIdentificacao != null && DocIdentificacao.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    DocIdentificacao.CopyTo(memoryStream);
                    DocIdentificacaoBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbProdutos = new TbProduto()
            {
                Nome = produto.Nome,
                Preco = produto.Preco,
                NotaFiscal = DocIdentificacaoBytes // Armazena a foto na entidade
            };

            // Adiciona a entidade ao contexto
            _context.TbProdutos.Add(tbProdutos);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbProdutos = _context.TbProdutos.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbProdutos != null)
            {
                // Remove a entidade do contexto
                _context.TbProdutos.Remove(tbProdutos);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Produtos não encontrado.");
            }

        }

        public List<Produto> GetAll()
        {
            List<Produto> listFun = new List<Produto>();

            var listTb = _context.TbProdutos.ToList();

            foreach (var item in listTb)
            {
                var Produto = new Produto
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Preco = item.Preco,
                    Quant = item.Quant                   

                };

                listFun.Add(Produto);
            }

            return listFun;
        }

        public Produto GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbProdutos.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var produto = new Produto
            {
                Id = item.Id,
                Nome = item.Nome,
                Preco = item.Preco,
                Quant = item.Quant,
                NotaFiscal = item.NotaFiscal,
            };

            return produto; // Retorna o funcionário encontrado
        }

        public void Update(Produto produto, IFormFile DocIdentificacao)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbProdutos = _context.TbProdutos.FirstOrDefault(f => f.Id == produto.Id);

            // Verifica se a entidade foi encontrada
            if (tbProdutos != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbProdutos.Nome = produto.Nome;
                tbProdutos.Preco = produto.Preco;
                tbProdutos.Quant = produto.Quant;

                // Verifica se uma nova foto foi enviada
                if (DocIdentificacao != null && DocIdentificacao.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        DocIdentificacao.CopyTo(memoryStream);
                        tbProdutos.NotaFiscal = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbProdutos.Update(tbProdutos);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Produtos não encontrado.");
            }
        }


    }
}
