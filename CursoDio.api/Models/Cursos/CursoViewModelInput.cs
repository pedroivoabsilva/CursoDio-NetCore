using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursoDio.api.Models.Cursos
{
    public class CursoViewModelInput
    {
        [Required(ErrorMessage ="O nome do curso é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="A descrição do curso é obrigatória")]
        public string Descricao { get; set; }
    }
}
