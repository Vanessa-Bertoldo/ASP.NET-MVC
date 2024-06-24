using web.students.Models;

namespace web.students.Data.Repository
{
    public interface IClienteRepository
    {
        IEnumerable<ClienteModel> GetAll();
        ClienteModel GetById(int id);
        void Add(ClienteModel cliente);
        void Update(ClienteModel cliente);
        void Delete(ClienteModel cliente);
    }
}
