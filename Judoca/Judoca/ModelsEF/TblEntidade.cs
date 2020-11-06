using System;
using System.Collections.Generic;

namespace Judoca.ModelsEF
{
    public partial class TblEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string Cnpj { get; set; }
    }
}
