using System;
using System.Collections.Generic;

namespace Projeto_Empresa.ORM;

public partial class TbProduto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public int Quant { get; set; }

    public byte[]? NotaFiscal { get; set; }

    public virtual ICollection<TbVendum> TbVenda { get; set; } = new List<TbVendum>();
}
