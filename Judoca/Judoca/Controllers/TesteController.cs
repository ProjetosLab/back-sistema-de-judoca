using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Judoca.Services;
using Judoca.ModelsEF;

namespace Judoca.Controllers
{

    
    [Route("api/[controller]")]
    public class TesteController : ControllerBase
    {
        private Services.ITesteFacade _teste;

        public TesteController(Services.ITesteFacade _teste)
        {
            this._teste = _teste;
        }

        [HttpGet("NA")]
        public ActionResult<bool> retornasaida()
        {
            // nome,cbj,niver,tel1,tel2,email,cpf,rg,org,ob
            _teste.retorno();
            return true;
        }

        [HttpGet("NA/aluno/{nome}/{niver}/{cbj}/{tel1}/{tel2}/{email}/{cpf}/{rg}/{org}/{ob?}")]
        public ActionResult<TblFiliado> cadastra(string nome,string  niver ,string cbj, string tel1,string tel2,string email, string cpf, string rg, string org, string ob)
        {
            int dia = int.Parse(niver.Split('-')[2]);
            int mes = int.Parse(niver.Split('-')[1]);
            int ano = int.Parse(niver.Split('-')[0]);
            DateTime niverFormat = new DateTime(ano, mes, dia);
            //http://sistema-de-judoca.herokuapp.com/api/Teste/NA/professor/{nome}/{niver}/{cbj}/{tel1}/{tel2}/{email}/{cpf}/{rg}/{org}/{ob?}
            // nome,cbj,niver,tel1,tel2,email,cpf,rg,org,ob
            //api/Teste/NA/aluno/João Vitor Pessini/2000-07-13/na/989613959/36997436/teste@gmail.com/48927640861/38057603X/ssp/teste
            //Response.Headers.Add("Access-Control-Allow-Origin","*");
            return _teste.cadastro(nome, niverFormat, cbj, tel1, tel2, email, cpf, rg, org, ob, "A");
        }

        [HttpGet("NA/professor/{nome}/{niver}/{cbj}/{tel1}/{tel2}/{email}/{cpf}/{rg}/{org}/{ob?}")]
        public ActionResult<TblFiliado> professor(string nome, string niver, string cbj, string tel1, string tel2, string email, string cpf, string rg, string org, string ob)
        {
            int dia = int.Parse(niver.Split('-')[2]);
            int mes = int.Parse(niver.Split('-')[1]);
            int ano = int.Parse(niver.Split('-')[0]);
            DateTime niverFormat = new DateTime(ano, mes, dia);
            //api/Teste/NA/professor/João Vitor Pessini/2000-07-13/na/989613959/36997436/teste@gmail.com/48927640861/38057603X/ssp/teste
            return _teste.cadastro(nome, niverFormat, cbj, tel1, tel2, email, cpf, rg, org, ob, "P");
        }



        [HttpGet("NA/pesquisa/{cpf}")]
        public ActionResult<List<TblFiliado>> Pesquisa(string cpf)
        {
            return _teste.Busca(cpf);
            //api/Teste/NA/pesquisa/João
        }

        //------------- Data de modificação 03/11 por Joao Pessini ----------
        // atualiza dados do filiado
        [HttpGet("NA/atualizar/{id}/{nome}/{niver}/{cbj}/{tel1}/{tel2}/{email}/{cpf}/{rg}/{org}/{ob?}/{tipo}")]
        public ActionResult<TblFiliado> atualiza(int id, string nome, string niver, string cbj, string tel1, string tel2, string email, string cpf, string rg, string org, string ob, string tipo)
        {
            int dia = int.Parse(niver.Split('-')[2]);
            int mes = int.Parse(niver.Split('-')[1]);
            int ano = int.Parse(niver.Split('-')[0]);
            DateTime niverFormat = new DateTime(ano, mes, dia);
            //api/Teste/NA/atualizar/33/João Vitor Pessini/2000-07-13/na/989613959/36997436/teste@gmail.com/48927640861/38057603X/ssp/teste/P
            return _teste.Att(id, nome, niverFormat, cbj, tel1, tel2, email, cpf, rg, org, ob, tipo);
        }


        //------------- Data de modificação 03/11 por Joao Pessini ----------
        // cadastra entidade
        [HttpGet("NA/entidade/{nome}/{cnpj}")]
        public ActionResult<TblEntidade> CadastroEntidade(string nome,string cnpj)
        {
            //api/Teste/NA/entidade/Pichau/09376495000112
            return _teste.cadastro_entidade(nome,cnpj);
        }

        //------------- Data de modificação 04/11 por Joao Pessini ----------
        // busca entidades
        [HttpGet("NA/entidade/busca")]
        public ActionResult<List<TblEntidade>> BuscaEntidade(string nome, string cnpj)
        {
            //api/Teste/NA/entidade/busca
            return _teste.busca_entidade();
        }


        //------------- Data de modificação 19/11 por Joao Pessini ----------
        // busca entidades
        [HttpGet("NA/filiado/aluno/busca")]
        public ActionResult<List<TblFiliado>> BuscaAluno()
        {
            //api/Teste/NA/filiado/aluno/busca
            return _teste.busca_filiado();
        }
        [HttpGet("NA/filiado/professor/busca")]
        public ActionResult<List<TblFiliado>> BuscaProfessor()
        {
            //api/Teste/NA/filiado/professor/busca
            return _teste.busca_professor();
        }

        //------------- Data de modificação 20/11 por Joao Pessini ----------
        // cadastro de filiado a entidade
        [HttpGet("NA/Carteira/{filiado}/{entidade}/{teste}")]
        public ActionResult<bool> CadastraCarteira(int filiado, int entidade,int teste)
        {
            //api/Teste/NA/Carteira/33/4/1
            return _teste.matricula(filiado,entidade,teste);
        }

        //------------- Data de modificação 20/11 por Joao Pessini ----------
        // Retorna uma lista de associado
        [HttpGet("NA/Carteira/lista/{filiado}")]
        public ActionResult<List<Matricula>> BuscaMatricula(int filiado)
        {
            //api/Teste/NA/Carteira/lista/33
            return _teste.busca_matricula(filiado);
        }

        //------------- Data de modificação 20/11 por Joao Pessini ----------
        // RETORNA APENAS UM ASSOCIADO
        [HttpGet("NA/Carteira/busca/{filiado}/{entidade}")]
        public ActionResult<Matricula> UnicaMatricula(int filiado, int entidade)
        {
            //api/Teste/NA/Carteira/busca/33/4
            return _teste.unica_matricula(filiado, entidade);
        }

        //------------- Data de modificação 20/11 por Joao Pessini ----------
        // Renova Matricula
        [HttpGet("NA/Carteira/renova/{id}/{mes}")]
        public ActionResult<Matricula> RenovaMatricula(int id, int mes)
        {
            //api/Teste/NA/Carteira/renova/14/2
            return _teste.renova_matricula(id, mes);
        }

        //------------- Data de modificação 20/11 por Joao Pessini ----------
        // Atualiza Entidade
        [HttpGet("NA/entidade/atualizar/{id}/{nome}/{data}/{cnpj}")]
        public ActionResult<TblEntidade> AttEntidade(int id, string nome, DateTime data, string cnpj)
        {
            //api/Teste/NA/entidade/atualizar/6/Kabum/2020-11-05/093764950
            return _teste.Att_Entidade(id,nome,data,cnpj);
        }
        //------------- Data de modificação 20/11 por Joao Pessini ----------
        // Busca entidade pelo Cnpj
        [HttpGet("NA/entidade/busca/cnpj/{cnpj}")]
        public ActionResult<TblEntidade> BuscaCNPJ(string cnpj)
        {
            //api/Teste/NA/entidade/busca/cnpj/09376495000112
            return _teste.EntidadeCNPJ(cnpj);
        }

        //------------- Data de modificação 20/11 por Joao Pessini ----------
        // Busca filiado pelo CPF
        [HttpGet("NA/pesquisa/cliente/{cpf}")]
        public ActionResult<TblFiliado> BuscaCPF(string cpf)
        {
            //api/Teste/NA/pesquisa/cliente/48927640861
            return _teste.FiliadoCPF(cpf);
        }

        //------------- Data de modificação 20/11 por Joao Pessini ----------
        // Busca entidades pelo nome
        [HttpGet("NA/entidade/busca/nome/{nome}")]
        public ActionResult<List<TblEntidade>> EntidadeNome(string nome)
        {
            //api/Teste/NA/entidade/busca/nome/pichau
            return _teste.BuscaEntidade(nome);
        }




    }
}

/*
rota para atualizar cadastro:
api/Teste/NA/atualizar/{id}/{nome}/{niver}/{cbj}/{tel1}/{tel2}/{email}/{cpf}/{rg}/{org}/{ob?}/{tipo}

rota para cadastro de entidade:
api/Teste/NA/entidade/{nome}/{cnpj}

rota para trazer lista de entidades cadastradas:
api/Teste/NA/entidade/busca

 */
