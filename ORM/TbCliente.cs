using System;
using System.Collections.Generic;

namespace Projeto_Empresa.ORM;

public partial class TbCliente
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public byte[]? DocIdentificacao { get; set; }

    public virtual ICollection<TbEndereco> TbEnderecos { get; set; } = new List<TbEndereco>();

    public virtual ICollection<TbVendum> TbVenda { get; set; } = new List<TbVendum>();
}
