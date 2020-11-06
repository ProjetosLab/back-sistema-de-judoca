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
        public ActionResult<TblFiliado> Pesquisa(string cpf)
        {
            return _teste.Busca(cpf);
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



    }
}
