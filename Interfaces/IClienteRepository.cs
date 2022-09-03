using DataBaseFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace DataBaseFirst.Interfaces
{
    public interface IClienteRepository
    {
        // CRUD

        public Cliente Inserir(Cliente cliente);
        public ICollection<Cliente> ListarTodos();
        public Cliente BuscarPorId(int id);
        public void AlterarCliente(Cliente cliente);
        public void AlterarClienteParcialmente(JsonPatchDocument patchCliente, Cliente cliente);
        public void ExcluirCliente(Cliente cliente);
    }
}
