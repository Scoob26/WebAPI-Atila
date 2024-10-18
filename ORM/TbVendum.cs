using System;
using System.Collections.Generic;

namespace Projeto_Empresa.ORM;

public partial class TbVendum
{
    public int Id { get; set; }

    public decimal Valor { get; set; }

    public byte[]? NotaF { get; set; }

    public int Fkproduto { get; set; }

    public int Fkcliente { get; set; }

    public virtual TbCliente FkclienteNavigation { get; set; } = null!;

    public virtual TbProduto FkprodutoNavigation { get; set; } = null!;
}
