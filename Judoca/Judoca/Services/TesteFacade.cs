using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Judoca.ModelsEF;

namespace Judoca.Services
{
    public interface ITesteFacade
    {
        string retorno();
        TblFiliado Busca(string cpf);
        TblFiliado cadastro(string nome, DateTime niver, string cbj, string tel1, string tel2, string email, string cpf, string rg, string org, string ob,string tipo);
    }

    public class Teste : ITesteFacade
    {
        public int num { get; set; }
        private readonly ModelsEF.JudocaContext _context;

        public Teste(ModelsEF.JudocaContext context)
        {
            this._context = context;
        }

        public string retorno()
        {
            this.num = 3;
            Console.WriteLine(num);
            return num.ToString();
        }

        public TblFiliado cadastro(string nome,DateTime niver,string cbj,string tel1,string tel2,string email,string cpf,string rg,string org, string ob,string tipo)
        {

            var validacao = (from vali in _context.TblFiliado
                             where cpf.Contains(vali.Cpf)
                             select vali).ToList();

            if (validacao.Count() == 0)
            {
                TblFiliado a = new TblFiliado();
                a.Nome = nome;
                a.RegistroCbj = cbj;
                a.Aniversario = niver;
                a.Telefone1 = tel1;
                a.Telefone2 = tel2;
                a.Email = email;
                a.Cpf = cpf;
                a.Rg = rg;
                a.OrgaoExp = org;
                a.Observacao = ob;
                a.Tipo = tipo;
                a.DataCadastro = DateTime.Now;
                _context.TblFiliado.Add(a);
                _context.SaveChanges();

                var tag = (from vali in _context.TblFiliado
                           where cpf.Contains(vali.Cpf)
                           select vali).First();
                
                return tag;

            }

            else
            {
                TblFiliado falha = new TblFiliado();
                falha.Id = -1;
                return falha;
            }


            

        }

        public TblFiliado Busca(string cpf)
        {
            TblFiliado registro = new TblFiliado();
            try
            {
                registro = (from vali in _context.TblFiliado
                                where cpf.Contains(vali.Cpf)
                                select vali).First();
            }
            catch
            {
                registro.Id = -1;
            }
            return registro;

        }
    }
}