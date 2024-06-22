namespace web.students.Models
{
    public class PedidoProdutoModel
    {
        public int PedidoId { get; set; }
        public DateTime DataPedido { get; set; }
        //Relacionamento com cliente
        public int ClientId { get; set; }
        public ClienteModel Cliente { get; set; }
        //Relacionamento com loja
        public int LojaId { get; set; }
        public LojaModel Loja { get; set; }
        //Relacionamento com produto
        public List<PedidoProdutoModel> PedidoProdutos { get; set; }
        public object Pedido { get; internal set; }
    }
}