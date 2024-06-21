namespace web.students.Models
{
    public class LojaModel
    {
        public int LojaId { get; set; }
        public string Nome {  get; set; }
        public string Endereco { get; set; }
        //Relacionamento com pedido
        public List<PedidoModel> Pedidos { get; set; }
    }
}