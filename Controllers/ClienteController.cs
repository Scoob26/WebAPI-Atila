using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_Empresa.Model;
using Projeto_Empresa.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projeto_Empresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepositorio _ClienteRepositorio; // O repositório que contém GetAll()

        public ClienteController(ClienteRepositorio ClienteRepositorio)
        {
            _ClienteRepositorio = ClienteRepositorio;
        }

        // GET: api/Cliente/{id}/DocIdentificacao
        [HttpGet("{id}/DocIdentificacao")]
        public IActionResult GetDocIdentificacao(int id)
        {
            // Busca o funcionário pelo ID
            var cliente = _ClienteRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (cliente == null || cliente.DocIdentificacao == null)
            {
                return NotFound(new { Mensagem = "Foto não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(cliente.DocIdentificacao, "image/jpeg"); // Ou "image/png" dependendo do formato
        }


        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Cliente>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var cliente = _ClienteRepositorio.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (cliente == null || !cliente.Any())
            {
                return NotFound(new { Mensagem = "Nenhum funcionário encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = cliente.Select(cliente => new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                UrlDocIdentificacao = $"{Request.Scheme}://{Request.Host}/api/Funcionario/{cliente.Id}/DocIdentificacao" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }


        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Cliente> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var cliente = _ClienteRepositorio.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (cliente == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var clienteComUrl = new Cliente
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                UrlDocIdentificacao = $"{Request.Scheme}://{Request.Host}/api/Cliente/{cliente.Id}/DocIdentificacao " // Define a URL completa para a imagem
            };

            // Retorna o funcionário com status 200 OK
            return Ok(clienteComUrl);
        }


        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] ClienteDto novoCliente)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var cliente = new Cliente
            {
                Nome = novoCliente.Nome,
                Telefone = novoCliente.Telefone
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _ClienteRepositorio.Add(cliente, novoCliente.DocIdentificacao);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Nome = cliente.Nome,
                Telefone = cliente.Telefone
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }



        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ClienteDto clienteAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var clienteExistente = _ClienteRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            clienteExistente.Nome = clienteAtualizado.Nome;
            clienteExistente.Telefone = clienteAtualizado.Telefone;

            // Chama o método de atualização do repositório, passando a nova foto
            _ClienteRepositorio.Update(clienteExistente, clienteAtualizado.DocIdentificacao);

            // Cria a URL da foto
            var UrlDocIdentificacao = $"{Request.Scheme}://{Request.Host}/api/Cliente/{clienteExistente.Id}/DocIdentificacao";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Nome = clienteExistente.Nome,
                Telefone = clienteExistente.Telefone,
                UrlDocIdentificacao = UrlDocIdentificacao // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }



        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var clienteExistente = _ClienteRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (clienteExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _ClienteRepositorio.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Nome = clienteExistente.Nome,
                Telefone = clienteExistente.Telefone
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }

}
