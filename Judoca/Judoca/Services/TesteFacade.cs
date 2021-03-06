﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Judoca.ModelsEF;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Judoca.Services
{
    public interface ITesteFacade
    {
        TblFiliado Att(int id, string nome, DateTime niver, string cbj, string tel1, string tel2, string email, string cpf, string rg, string org, string ob, string tipo);
        string retorno();
        List<TblFiliado> Busca(string cpf);
        TblFiliado cadastro(string nome, DateTime niver, string cbj, string tel1, string tel2, string email, string cpf, string rg, string org, string ob,string tipo);
        TblEntidade cadastro_entidade(string nome, string cnpj);
        List<TblEntidade> busca_entidade();
        List<TblFiliado> busca_filiado();
        bool matricula(int filiado, int entidade, int teste);
        List<Matricula> busca_matricula(int filiado);
        Matricula unica_matricula(int filiado, int entidade);
        Matricula renova_matricula(int id, int mes);
        List<TblFiliado> busca_professor();
        TblEntidade Att_Entidade(int id, string nome, DateTime data, string cnpj);
        List<TblEntidade> BuscaEntidade(string nome);
        TblEntidade EntidadeCNPJ(string cnpj);
        TblFiliado FiliadoCPF(string cpf);

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

        public TblFiliado Att(int id,string nome, DateTime niver, string cbj, string tel1, string tel2, string email, string cpf, string rg, string org, string ob, string tipo)
        {
            TblFiliado atuacao = new TblFiliado();
            atuacao = (from vali in _context.TblFiliado
                             where vali.Id == id
                             select vali).First();



            atuacao.Nome = nome;
            atuacao.RegistroCbj = cbj;
            atuacao.Aniversario = niver;
            atuacao.Telefone1 = tel1;
            atuacao.Telefone2 = tel2;
            atuacao.Email = email;
            atuacao.Cpf = cpf;
            atuacao.Rg = rg;
            atuacao.OrgaoExp = org;
            atuacao.Observacao = ob;
            atuacao.Tipo = tipo;
            _context.TblFiliado.Update(atuacao);
            _context.SaveChanges();


            atuacao = (from vali in _context.TblFiliado
                       where vali.Id == id
                       select vali).First();

            return atuacao;


        }

        public List<TblFiliado> Busca(string cpf)
        {

            /*
               var registro = (from vali in _context.TblFiliado
                                    where cpf.Contains(vali.Nome)
                                    select vali).ToList();
            */

            var registro = (from vali in _context.TblFiliado
                            where vali.Nome.Contains(cpf)
                            select vali).ToList();


            return registro;

        }

        public TblEntidade cadastro_entidade(string nome,string cnpj)
        {

            
            var validacao = (from vali in _context.TblEntidade
                             where cnpj.Contains(vali.Cnpj)
                             select vali).ToList();


            if (validacao.Count() == 0)
            {
                TblEntidade a = new TblEntidade();
                a.Nome = nome;
                a.Cnpj = cnpj;
                a.DataCadastro = DateTime.Now;
                _context.TblEntidade.Add(a);
                _context.SaveChanges();

                var tag = (from vali in _context.TblEntidade
                           where cnpj.Contains(vali.Cnpj)
                           select vali).First();

                return tag;

            }

            else
            {
                TblEntidade falha = new TblEntidade();
                falha.Id = -1;
                return falha;
            }
        }


        public List<TblEntidade> busca_entidade()
        {
            var validacao = (from vali in _context.TblEntidade
                             select vali).ToList();

            return validacao;
        }

        public List<TblFiliado> busca_filiado()
        {
            var validacao = (from vali in _context.TblFiliado
                             where vali.Tipo == "A"
                             select vali).ToList();

            return validacao;
        }

        public List<TblFiliado> busca_professor()
        {
            var validacao = (from vali in _context.TblFiliado
                             where vali.Tipo == "P"
                             select vali).ToList();

            return validacao;
        }

        // Faz o vinculo entre filiado e entidade
        public bool matricula(int filiado, int entidade, int teste)
        {
            DateTime cadastro = DateTime.Now;
            DateTime final = DateTime.Now.AddMonths(teste);

            TblCarteira a = new TblCarteira();
            a.DataInicio = cadastro;
            a.DataFinal = final;
            a.IdFiliado = filiado;
            a.IdEntidade = entidade;

            _context.TblCarteiras.Add(a);
            _context.SaveChanges();

            return true;
        }

        public List<Matricula> busca_matricula(int filiado)
        {
            var retorno = (from a in _context.Matricula
                           where a.ID_FILIADO == filiado
                           select a).ToList();

            return retorno;
        }

        public Matricula unica_matricula(int filiado, int entidade)
        {
            

            var teste = (from a in _context.Matricula
                        where a.ID_ENTIDADE == entidade && a.ID_FILIADO == filiado
                        select a).First();
            
            return teste;
        }

        public Matricula renova_matricula(int id,int mes)
        {

            var teste = (from a in _context.TblCarteiras
                         where a.Id == id
                         select a).First();

            DateTime var = new DateTime();
            if (teste.DataFinal < DateTime.Now)
            {
                var = DateTime.Now.AddMonths(mes);
            }
            else
            {
                var = DateTime.Parse(teste.DataFinal.ToString()).AddMonths(mes);
            }
            
            teste.DataFinal = var;
            teste.DataInicio = DateTime.Now;
            _context.TblCarteiras.Update(teste);
            _context.SaveChanges();

            var retorno = (from a in _context.Matricula
                           where a.ID == id
                           select a).First();

            return retorno;

        }

        public TblEntidade Att_Entidade(int id, string nome, DateTime data,string cnpj)
        {
            TblEntidade atuacao = new TblEntidade();
            atuacao = (from vali in _context.TblEntidade
                       where vali.Id == id
                       select vali).First();



            atuacao.Nome = nome;
            atuacao.DataCadastro = data;
            atuacao.Cnpj = cnpj;
            _context.TblEntidade.Update(atuacao);
            _context.SaveChanges();


            atuacao = (from vali in _context.TblEntidade
                       where vali.Id == id
                       select vali).First();

            return atuacao;


        }

        public List<TblEntidade> BuscaEntidade(string nome)
        {

            var registro = (from vali in _context.TblEntidade
                            where vali.Nome.Contains(nome)
                            select vali).ToList();


            return registro;

        }

        public TblEntidade EntidadeCNPJ(string cnpj)
        {

            var registro = (from vali in _context.TblEntidade
                            where cnpj.Contains(vali.Cnpj)
                            select vali).First();

            return registro;
        }

        public TblFiliado FiliadoCPF(string cpf)
        {

            var registro = (from vali in _context.TblFiliado
                            where cpf.Contains(vali.Cpf)
                            select vali).First();

            return registro;
        }
    }
}