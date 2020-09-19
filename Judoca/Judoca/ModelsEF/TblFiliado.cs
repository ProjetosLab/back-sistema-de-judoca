using System;
using System.Collections.Generic;

namespace Judoca.ModelsEF
{
    public partial class TblFiliado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? Aniversario { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string RegistroCbj { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string OrgaoExp { get; set; }
        public string Observacao { get; set; }
        public string Tipo { get; set; }
    }
}
