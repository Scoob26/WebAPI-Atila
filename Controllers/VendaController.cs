using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_Empresa.Model;
using Projeto_Empresa.ORM;
using Projeto_Empresa.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projeto_Empresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VendaController : ControllerBase
    {
        private readonly VendaRepositorio _VendaRepositorio; // O repositório que contém GetAll()
        public VendaController(VendaRepositorio VendaRepositorio)
        {
            _VendaRepositorio = VendaRepositorio;
        }

        // GET: api/Cliente/{id}/Notaf
        [HttpGet("{id}/Notaf")]
        public IActionResult GetDocNotaf(int id)
        {
            // Busca o funcionário pelo ID
            var venda = _VendaRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (venda == null || venda.NotaF == null)
            {
                return NotFound(new { Mensagem = "Foto não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(venda.NotaF, "image/jpeg"); // Ou "image/png" dependendo do formato
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Cliente>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var venda = _VendaRepositorio.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (venda == null || !venda.Any())
            {
                return NotFound(new { Mensagem = "Nenhum venda encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = venda.Select(venda => new Venda
            {
                Id = venda.Id,
                Valor = venda.Valor,
                Fkproduto = venda.Fkproduto,
                UrlNotaF = $"{Request.Scheme}://{Request.Host}/api/Funcionario/{venda.Id}/Notaf" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Venda> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var venda = _VendaRepositorio.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (venda == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var vendaComUrl = new Venda
            {
                Id = venda.Id,
                Valor = venda.Valor,
                Fkproduto = venda.Fkproduto,
                UrlNotaF = $"{Request.Scheme}://{Request.Host}/api/Cliente/{venda.Id}/DocIdentificacao ", // Define a URL completa para a imagem
                Fkcliente = venda.Fkcliente,
            };

            // Retorna o funcionário com status 200 OK
            return Ok(vendaComUrl);
        }

        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] VendaDto novoVenda)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var venda = new Venda
            {
                Valor = novoVenda.Valor,
                Fkproduto = novoVenda.Fkproduto,
                Fkcliente = novoVenda.Fkcliente,
                
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _VendaRepositorio.Add(venda, novoVenda.NotaF);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Venda cadastrado com sucesso!",
                Valor = venda.Valor,
                Fkproduto = venda.Fkproduto
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] VendaDto VendaAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var vendaExistente = _VendaRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (vendaExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            vendaExistente.Valor = VendaAtualizado.Valor;
            vendaExistente.Fkproduto = VendaAtualizado.Fkproduto;

            // Chama o método de atualização do repositório, passando a nova foto
            _VendaRepositorio.Update(vendaExistente, VendaAtualizado.NotaF);

            // Cria a URL da foto
            var UrlNotaF = $"{Request.Scheme}://{Request.Host}/api/Cliente/{vendaExistente.Id}/DocIdentificacao";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Venda atualizado com sucesso!",
                Valor = vendaExistente.Valor,
                Fkproduto = vendaExistente.Fkproduto,
                UrlNotaF = UrlNotaF // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var vendaExistente = _VendaRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (vendaExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _VendaRepositorio.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto excluído com sucesso!",
                Valor = vendaExistente.Valor,
                Fkproduto = vendaExistente.Fkproduto
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }




    }   
}
