using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Judoca.Services;
/*
http://localhost:5001/api/Teste/NA
*/
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

        [HttpGet("NA/{nome}/{niver}/{cbj}/{tel1}/{tel2}/{email}/{cpf}/{rg}/{org}/{ob?}")]
        public ActionResult<string> cadastra(string nome,string  niver ,string cbj, string tel1,string tel2,string email, string cpf, string rg, string org, string ob)
        {
            int dia = int.Parse(niver.Split('-')[2]);
            int mes = int.Parse(niver.Split('-')[1]);
            int ano = int.Parse(niver.Split('-')[0]);
            DateTime niverFormat = new DateTime(ano, mes, dia);
            //DateTime niver = DateTime.Parse("13/07/2000");
            // nome,cbj,niver,tel1,tel2,email,cpf,rg,org,ob
            //NA/João Vitor Pessini/2000-07-13/na/989613959/36997436/teste@gmail.com/48927640861/38057603X/ssp/teste
            return _teste.cadastro(nome, niverFormat, cbj, tel1, tel2, email, cpf, rg, org, ob);
        }





    }
}