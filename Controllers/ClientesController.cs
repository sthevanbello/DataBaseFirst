using DataBaseFirst.Interfaces;
using DataBaseFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DataBaseFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        public IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        /// <summary>
        /// Cadastrar Cliente no banco de dados
        /// </summary>
        /// <param name="cliente">Cliente a ser cadastrado</param>
        /// <returns>Cliente cadastrado</returns>
        [HttpPost]
        public IActionResult InsertCliente(Cliente cliente)
        {
            try
            {
                var clienteInserido = _clienteRepository.Inserir(cliente);
                return Ok(clienteInserido);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os clientes",
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Exibir todos os clientes
        /// </summary>
        /// <returns>List de clientes</returns>
        [HttpGet]
        public IActionResult GetAllClientes()
        {
            try
            {
                var clientes = _clienteRepository.ListarTodos();
                return Ok(clientes);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os clientes",
                    ex.Message
                });
            }
        }
        /// <summary>
        /// Exibir um único cliente de acordo com o id fornecido
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <returns>Retorna uma Cliente</returns>
        [HttpGet("{id}")]
        public IActionResult GetClienteById(int id)
        {
            try
            {
                var cliente = _clienteRepository.BuscarPorId(id);
                if (cliente is null)
                {
                    return NotFound(new {msg="Cliente não encontrado. Conferir o Id informado"});
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao listar os clientes",
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Atualizar Cliente no banco de dados
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <param name="cliente">Cliente a ser atualizado</param>
        /// <returns>Cliente atualizado</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateCliente(int id, Cliente cliente)
        {
            try
            {
                if (id != cliente.Id)
                {
                    return BadRequest(new { msg= "Os ids não são correspondentes" });    
                }
                var clienteRetorno = _clienteRepository.BuscarPorId(id);

                if (clienteRetorno is null)
                {
                    return NotFound(new { msg = "Cliente não encontrado. Conferir o Id informado" });
                }

                _clienteRepository.AlterarCliente(cliente);

                return Ok(new {msg="Cliente alterado", cliente });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar o cliente",
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Atualizar parte das informações do cliente
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <param name="patchCliente">informações a serem alteradas</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult PatchCliente(int id, [FromBody] JsonPatchDocument patchCliente)
        {
            try
            {
                if (patchCliente is null)
                {
                    return BadRequest(new { msg = "Insira os dados novos" });
                }

                var cliente = _clienteRepository.BuscarPorId(id);
                if (cliente is null)
                {
                    return NotFound(new { msg = "Cliente não encontrado. Conferir o Id informado" });
                }

                _clienteRepository.AlterarClienteParcialmente(patchCliente, cliente);

                return Ok(new { msg = "Cliente alterado", cliente });
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar o cliente",
                    ex.Message
                });
            }
        }

        /// <summary>
        /// Excluir Cliente do banco de dados
        /// </summary>
        /// <param name="id">Id do cliente a ser excluído</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            try
            {
                var clienteRetorno = _clienteRepository.BuscarPorId(id);

                if (clienteRetorno is null)
                {
                    return NotFound(new { msg = "Cliente não encontrado. Conferir o Id informado" });
                }

                _clienteRepository.ExcluirCliente(clienteRetorno);

                return Ok(new {msg="Cliente removido com sucesso"});
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    msg = "Falha ao alterar os clientes",
                    ex.Message
                });
            }
        }
    }
}
