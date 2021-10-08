using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoDio.api.Models.Usuarios;
using CursoDio.api.Filters;
using Swashbuckle.AspNetCore.Annotations;
using CursoDio.api.Models;

namespace CursoDio.api.Controllers
{
    [ApiController]
    [Route("api/v1/usuario")]
    public class UsuarioController : Controller
    {
        [SwaggerResponse(statusCode: 200, description:"Sucesso ao autenticar",type:typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description:"Campos obrigatórios",type:typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description:"Erro interno",type:typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            return Ok(loginViewModelInput);
        }
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistroViewModelInput registroViewModelInput)
        {
            return Created("",registroViewModelInput);
        }
    }
}
