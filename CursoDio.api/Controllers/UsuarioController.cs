using CursoDio.api.Business.Entities;
using CursoDio.api.Business.Repositories;
using CursoDio.api.Configurations;
using CursoDio.api.Filters;
using CursoDio.api.Models;
using CursoDio.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace CursoDio.api.Controllers
{
    [ApiController]
    [Route("api/v1/usuario")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationService _authenticationService;

        public UsuarioController(
            IUsuarioRepository usuarioRepository,
            IAuthenticationService authenticationService)
        {
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;
        }


        /// <summary>
        /// Este serviço pertime autenticar um usuário cadastrado e ativo.
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns>Retorna status ok, dados do usuário e token em caso de uso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", type: typeof(usuarioViewModelOutput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", type: typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", type: typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Logar(usuarioViewModelOutput loginViewModelInput)
        {
            var usuario = await _usuarioRepository.ObterUsuarioAsync(loginViewModelInput.Login);

            if (usuario == null)
            {
                return BadRequest("Houve um erro ao tentar acessar.");
            }

            if (usuario.Senha != loginViewModelInput.Senha)
            {
                return BadRequest("Usuário ou senha inválido");
            }
            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = usuario.Codigo,
                Login = loginViewModelInput.Login,
                Email = usuario.Email
            };

            var token = _authenticationService.GerarToken(usuarioViewModelOutput);


            return Ok(new LoginViemModelOutput
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            }
                );
        }

        /// <summary>
        /// Este serviço permite cadastrar um usuário cadastrado não existente
        /// </summary>
        /// <param name="registroViewModelInput"> View Model de registro de login</param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", type: typeof(usuarioViewModelOutput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", type: typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", type: typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Registrar(RegistroViewModelInput registroViewModelInput)
        {


            //var migracoesPendentes = context.Database.GetPendingMigrations();
            //if (migracoesPendentes.Count() > 0)
            //{
            //    context.Database.Migrate();
            //}

            var usuario = await _usuarioRepository.ObterUsuarioAsync(registroViewModelInput.Login);

            if (usuario != null) 
            {
                return BadRequest("Usuário já cadastrado.");
            }

            usuario = new Usuario();
            usuario.Login = registroViewModelInput.Login;
            usuario.Senha = registroViewModelInput.Senha;
            usuario.Email = registroViewModelInput.Email;


            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();
            return Created("", registroViewModelInput);
        }
    }
}
