using CursoDio.api.Models.Cursos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CursoDio.api.Business.Repositories;
using CursoDio.api.Business.Entities;
using System.Linq;

namespace CursoDio.api.Controllers
{
    [ApiController]
    [Route("api/v1/cursos")]
    [Authorize ]
    public class CursoController : Controller
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        /// <summary>
        /// Este serviço permite cadastrar o curso para o usuário.
        /// </summary>
        /// <param name="cursoViewModelInput"></param>
        /// <returns>Retorna status 201 e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao Cadastrar um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            Curso curso = new Curso();
            curso.Nome = cursoViewModelInput.Nome;
            curso.Descricao = cursoViewModelInput.Descricao;
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            curso.CodigoUsuario = codigoUsuario;
            _cursoRepository.Adicionar(curso);
            _cursoRepository.Commit();
            return Created("", cursoViewModelInput);
        }

        /// <summary>
        /// Este serviço permite obter todos os cursos do usuário
        /// </summary>
        /// <returns>Retorna ok e dados do curso do usuário</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao Obter um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            

            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var cursos = _cursoRepository.ObterPorUsuario(codigoUsuario)
                .Select(s => new CursoViewModelOutput()
                {
                    Nome = s.Nome,
                    Descricao=s.Descricao,
                    Login = s.Usuario.Login
                }); 
            
            return Ok(cursos);
        }
    }

}
