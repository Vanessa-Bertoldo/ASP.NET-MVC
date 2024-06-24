using web.students.Data.Repository;
using web.students.Models;

namespace web.students.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ClienteModel> ListarClientes() => _repository.GetAll();

        public ClienteModel ObterClientePorId(int id) => _repository.GetById(id);

        public void CriarCliente(ClienteModel cliente) => _repository.Add(cliente);        

        public void AtualizarCliente(ClienteModel cliente) => _repository.Update(cliente);

        public void DeletarCliente(int id)
        {
            var cliente = _repository.GetById(id);
            if (cliente != null)
            {
                _repository.Delete(cliente);
            }
        }

    }
}
