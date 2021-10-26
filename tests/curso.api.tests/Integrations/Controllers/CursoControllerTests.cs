using AutoBogus;
using CursoDio.api;
using CursoDio.api.Models.Cursos;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace curso.api.tests.Integrations.Controllers
{
    public class CursoControllerTests : UsuarioControllerTests
    {
        public CursoControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper outputHelper)
            : base(factory, outputHelper)
        {
        }

        [Fact]
        public async Task Registrar_InformadoDadosDeUmCursoValidoEUmUsuárioAutenticado_DeverRetornarSusesso()
        {
            //AAA

            ///Arrange
            var cursoViewModelInput = new AutoFaker<CursoViewModelInput>();
            
            StringContent content = new StringContent(JsonConvert.SerializeObject(cursoViewModelInput.Generate()), Encoding.UTF8, "application/json");

            //Act(actions)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginViemModelOutput.Token);
            var httpClientRequest = await _httpClient.PostAsync("api/v1/cursos", content);

            //Assert
            _output.WriteLine($"{nameof(CursoControllerTests)}_{nameof(Registrar_InformadoDadosDeUmCursoValidoEUmUsuárioAutenticado_DeverRetornarSusesso)} = {await httpClientRequest.Content.ReadAsStringAsync()}");
            Assert.Equal(HttpStatusCode.Created, httpClientRequest.StatusCode);

        }

        [Fact]
        public async Task Registrar_InformadoDadosDeUmCursoValidoEUmUsuárioNãoAutenticado_DeverRetornarSusesso()
        {
            //AAA

            ///Arrange
            var cursoViewModelInput = new AutoFaker<CursoViewModelInput>();

            StringContent content = new StringContent(JsonConvert.SerializeObject(cursoViewModelInput.Generate()), Encoding.UTF8, "application/json");

            //Act(actions)
            
            var httpClientRequest = await _httpClient.PostAsync("api/v1/cursos", content);

            //Assert
            _output.WriteLine($"{nameof(CursoControllerTests)}_{nameof(Registrar_InformadoDadosDeUmCursoValidoEUmUsuárioNãoAutenticado_DeverRetornarSusesso)} = {await httpClientRequest.Content.ReadAsStringAsync()}");
            Assert.Equal(HttpStatusCode.Unauthorized, httpClientRequest.StatusCode);

        }

        [Fact]
        public async Task Obter_InformadoDadosDeUmUsuarioAutenticado_DeverRetornarSusesso()
        {
            //AAA

            ///Arrange
            await Registrar_InformadoDadosDeUmCursoValidoEUmUsuárioAutenticado_DeverRetornarSusesso();

            //Act(actions)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginViemModelOutput.Token);
            var httpClientRequest = await _httpClient.GetAsync("api/v1/cursos");

            //Assert
            _output.WriteLine($"{nameof(CursoControllerTests)}_{nameof(Obter_InformadoDadosDeUmUsuarioAutenticado_DeverRetornarSusesso)} = {await httpClientRequest.Content.ReadAsStringAsync()}");
            var cursos = JsonConvert.DeserializeObject<List<CursoViewModelOutput>>(await httpClientRequest.Content.ReadAsStringAsync());

            Assert.NotEmpty(cursos);
            Assert.Equal(HttpStatusCode.OK, httpClientRequest.StatusCode);

        }
    }
}
