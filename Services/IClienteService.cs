using web.students.Models;

namespace web.students.Services
{
    public interface IClienteService
    {
        IEnumerable<ClienteModel> ListarClientes();
        ClienteModel ObterClientePorId(int id);
        void CriarCliente(ClienteModel cliente);
        void AtualizarCliente(ClienteModel cliente);
        void DeletarCliente(int id);
    }
}
