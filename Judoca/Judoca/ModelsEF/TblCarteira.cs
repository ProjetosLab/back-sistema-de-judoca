using System;
using System.Collections.Generic;


namespace Judoca.ModelsEF
{
    public partial class TblCarteira
    {
        public int Id { get; set; }
        public int? IdFiliado { get; set; }
        public int? IdEntidade { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}
