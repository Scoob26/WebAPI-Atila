using Projeto_Empresa.Model;
using Projeto_Empresa.ORM;

namespace Projeto_Empresa.Repositorio
{
    public class VendaRepositorio
    {
        private ProjetoEmpresaContext _context;
        public VendaRepositorio(ProjetoEmpresaContext context)
        {
            _context = context;
        }

        public void Add(Venda venda, IFormFile Notaf)
        {
            // Verifica se uma foto foi enviada
            byte[] NotafBytes = null;
            if (Notaf != null && Notaf.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    Notaf.CopyTo(memoryStream);
                    NotafBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbvenda = new TbVendum()
            {
                Valor = venda.Valor,
                Fkproduto = venda.Fkproduto,
                NotaF = NotafBytes, // Armazena a foto na entidade
                Fkcliente = venda.Fkcliente,
            };

            // Adiciona a entidade ao contexto
            _context.TbVenda.Add(tbvenda);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVenda = _context.TbVenda.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbVenda != null)
            {
                // Remove a entidade do contexto
                _context.TbVenda.Remove(tbVenda);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Venda não encontrado.");
            }

        }

        public List<Venda> GetAll()
        {
            List<Venda> listFun = new List<Venda>();

            var listTb = _context.TbVenda.ToList();

            foreach (var item in listTb)
            {
                var Venda = new Venda
                {
                    Id = item.Id,
                    Valor = item.Valor,
                    Fkproduto = item.Fkproduto,
                    Fkcliente = item.Fkcliente,
                    NotaF = item.NotaF,
                };

                listFun.Add(Venda);
            }

            return listFun;
        }

        public Venda GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbVenda.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var venda = new Venda
            {
                Id = item.Id,
                Valor = item.Valor,
               Fkproduto  = item.Fkproduto,
               Fkcliente= item.Fkcliente,
               NotaF = item.NotaF,
            };

            return venda ; // Retorna o funcionário encontrado
        }

        public void Update(Venda venda, IFormFile NotaF)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVenda = _context.TbVenda.FirstOrDefault(f => f.Id == venda.Id);

            // Verifica se a entidade foi encontrada
            if (tbVenda != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbVenda.Id = venda.Id;
                tbVenda.Valor = venda.Valor;
                tbVenda.Fkproduto = venda.Fkproduto;
                tbVenda.Fkcliente = venda.Fkcliente;

                // Verifica se uma nova foto foi enviada
                if (NotaF != null && NotaF.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        NotaF.CopyTo(memoryStream);
                        tbVenda.NotaF = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbVenda.Update(tbVenda);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Venda não encontrado.");
            }
        }



    }
}
