using CursoDio.web.mvc.Models.Cursos;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoDio.web.mvc.Services
{
    public interface ICursoService
    {
        [Post("/api/v1/cursos")]
        [Headers("Authorization: Bearer")]
        Task<CadastrarCursoViewModeOutput> Registrar(CadastrarCursoViewModelInput cadastrarCursoViewModelInput);

        [Get("/api/v1/cursos")]
        [Headers("Authorization: Bearer")]
        Task<IList<ListarCursoViewModelOutput>> Obter();
    }
}
