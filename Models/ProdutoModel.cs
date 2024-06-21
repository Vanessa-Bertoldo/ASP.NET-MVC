namespace web.students.Models
{
    public class ProdutoModel
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco {  get; set; }
        public string Descricao { get; set; }
        //Relacionamento com fornecedor
        public int FornecedorId { get; set; }
        public FornecedorModel Fornecedor { get; set; }
        //Reloacionamento com Pedido
        public List<PedidoProdutoModel> PedidoProdutos { get; set; }
    }
}