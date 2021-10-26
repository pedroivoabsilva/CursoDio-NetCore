using AutoBogus;
using curso.api.tests.Configurations;
using CursoDio.api;
using CursoDio.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace curso.api.tests.Integrations.Controllers
{
    public class UsuarioControllerTests : IClassFixture<WebApplicationFactory<Startup>>, IAsyncLifetime
    {

        protected readonly HttpClient _httpClient;
        protected readonly ITestLoggerFactory _output;
        protected RegistroViewModelInput RegistroViewModelInput;
        protected LoginViemModelOutput LoginViemModelOutput;

        public UsuarioControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper outputHelper)
        {
            _httpClient = factory.CreateClient();
            _output = new TestLoggerFactory(outputHelper);
        }

        public async Task DisposeAsync()
        {
            _httpClient.Dispose();
        }

        public async Task InitializeAsync()
        {
            await Registrar_InformadoUsuarioESenhaExistetes_DeverRetornarSusesso();
            await Logar_InformadoUsuarioESenhaExistetes_DeverRetornarSusesso();
        }

        //WhenGivenThen
        //Qundo_Dados_EntaoResultadoEsperado

        [Fact]
        public async Task Logar_InformadoUsuarioESenhaExistetes_DeverRetornarSusesso()
        {
            //AAA

            ///Arrange
            var loginWiewModelInput = new usuarioViewModelOutput
            {
                Login = RegistroViewModelInput.Login,
                Senha = RegistroViewModelInput.Senha
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginWiewModelInput), Encoding.UTF8, "application/json");

            //Act(actions)
            var httpClientRequest = await _httpClient.PostAsync("api/v1/usuario/logar", content);
            LoginViemModelOutput = JsonConvert.DeserializeObject<LoginViemModelOutput>(await httpClientRequest.Content.ReadAsStringAsync());

            //Assert
            Assert.Equal(HttpStatusCode.OK, httpClientRequest.StatusCode);
            Assert.Equal(loginWiewModelInput.Login, LoginViemModelOutput.Usuario.Login);
            Assert.NotNull(LoginViemModelOutput.Token);
            _output.WriteLine($"{nameof(UsuarioControllerTests)}_{nameof(Logar_InformadoUsuarioESenhaExistetes_DeverRetornarSusesso)} = {await httpClientRequest.Content.ReadAsStringAsync()}");

        }

        [Fact]
        public async Task Registrar_InformadoUsuarioESenhaExistetes_DeverRetornarSusesso()
        {
            //AAA
            RegistroViewModelInput = new AutoFaker<RegistroViewModelInput>(AutoBogusConfiguration.LOCATE)
                .RuleFor(p => p.Login, faker => faker.Person.UserName)
                .RuleFor(p => p.Email, faker => faker.Person.Email);

            StringContent content = new StringContent(JsonConvert.SerializeObject(RegistroViewModelInput), Encoding.UTF8, "application/json");

            ///Arrange

            var httpClientRequest = await _httpClient.PostAsync("api/v1/usuario/registrar", content);

            //Act(actions)


            //Assert
            _output.WriteLine($"{nameof(UsuarioControllerTests)}_{nameof(Logar_InformadoUsuarioESenhaExistetes_DeverRetornarSusesso)} = {await httpClientRequest.Content.ReadAsStringAsync()}");
            Assert.Equal(HttpStatusCode.Created, httpClientRequest.StatusCode);

        }
    }
}
