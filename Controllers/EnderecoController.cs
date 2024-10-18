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
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoRepositorio _enderecoRepositorio;

        public EnderecoController(EnderecoRepositorio enderecoRepositorio)
        {
            _enderecoRepositorio = enderecoRepositorio;
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Endereco>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var enderecos = _enderecoRepositorio.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (enderecos == null || !enderecos.Any())
            {
                return NotFound(new { Mensagem = "Nenhum funcionário encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = enderecos.Select(endereco => new Endereco
            {
               Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Cidade = endereco .Cidade, 
                Estado = endereco .Estado,
                Cep = endereco .Cep,
                PontoReferencia = endereco .PontoReferencia,
                Nº = endereco.Nº,
                FkCliente = endereco .FkCliente,

            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Endereco> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var endereco = _enderecoRepositorio.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (endereco == null)
            {
                return NotFound(new { Mensagem = "Cliente não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var enderecoComUrl = new Endereco
            {
                Id=endereco.Id,
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cidade,
                PontoReferencia = endereco.PontoReferencia,
                Nº = endereco.Nº,
                FkCliente = endereco.FkCliente,
            };

            // Retorna o funcionário com status 200 OK
            return Ok(enderecoComUrl);
        }

        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] EnderecoDto novoEndereco)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var endereco = new Endereco
            {
                Logradouro = novoEndereco.Logradouro,
                Cidade = novoEndereco.Cidade,
                Estado = novoEndereco.Estado,
                Cep= novoEndereco.Cep,
                PontoReferencia = novoEndereco.PontoReferencia,
                Nº = novoEndereco.Nº,
                FkCliente = novoEndereco.FkCliente,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _enderecoRepositorio.Add(endereco);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário cadastrado com sucesso!",
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] EnderecoDto enderecoAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var enderecoExistente = _enderecoRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            enderecoExistente.Logradouro = enderecoAtualizado.Logradouro;
            enderecoExistente.Cidade = enderecoAtualizado.Cidade;
            enderecoExistente.Estado = enderecoAtualizado.Estado;
            enderecoExistente.Cep = enderecoAtualizado.Cep;
            enderecoExistente.PontoReferencia = enderecoAtualizado.PontoReferencia;
            enderecoExistente.Nº = enderecoAtualizado.Nº;
            enderecoExistente.FkCliente = enderecoAtualizado.FkCliente;

            // Chama o método de atualização do repositório, passando a nova foto
            _enderecoRepositorio.Update(enderecoExistente);
                       

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Cidade = enderecoExistente.Cidade,
                Estado = enderecoExistente.Estado,
               
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var enderecoExistente = _enderecoRepositorio.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Funcionário não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _enderecoRepositorio.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário excluído com sucesso!",
                Logradouro = enderecoExistente.Logradouro,
                Estado = enderecoExistente.Estado,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

    }
}
