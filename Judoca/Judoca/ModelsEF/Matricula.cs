using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Judoca.ModelsEF
{
    public partial class Matricula
    {
        public int? ID { get; set; }
        public DateTime? DATA_INICIO { get; set; }
        public DateTime? DATA_FINAL { get; set; }
        public string EMPRESA { get; set; }
        public string NOME { get; set; }
        public int? ID_ENTIDADE { get; set; }
        public int? ID_FILIADO { get; set; }
    }
}
