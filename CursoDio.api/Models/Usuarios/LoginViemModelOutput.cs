using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoDio.api.Models.Usuarios
{
    public class LoginViemModelOutput
    {
        public UsuarioViewModelOutput Usuario { get; set; }
        public string Token { get; set; }
    }
}
