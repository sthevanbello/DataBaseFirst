using DataBaseFirst.Data;
using DataBaseFirst.Interfaces;
using DataBaseFirst.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataBaseFirst.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        // Injeção ode dependência
        public ClienteRepository(DatabaseFirstDBContext context)
        {
            Context = context;
        }
        private DatabaseFirstDBContext Context { get; set; }

        public void AlterarCliente(Cliente cliente)
        {
            Context.Entry(cliente).State = EntityState.Modified;

            Context.SaveChanges();
        }

        public void AlterarClienteParcialmente(JsonPatchDocument patchCliente, Cliente cliente)
        {
            patchCliente.ApplyTo(cliente);
            Context.Entry(cliente).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public Cliente BuscarPorId(int id)
        {
            //return Context.Clientes.FirstOrDefault(c => c.Id == id);
            return Context.Clientes.Find(id);
        }

        public void ExcluirCliente(Cliente cliente)
        {

            Context.Clientes.Remove(cliente);

            Context.SaveChanges();
        }

        public Cliente Inserir(Cliente cliente)
        {
            Context.Clientes.Add(cliente);
            Context.SaveChanges();
            var clienteInserido = Context.Clientes.Where(c => c.Cpf == cliente.Cpf).FirstOrDefault();
            return clienteInserido;
        }

        public ICollection<Cliente> ListarTodos()
        {
            return Context.Clientes.ToList();
        }
    }
}
