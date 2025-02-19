using Moq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using visual_studio_psicanalista.Repositorio;
using Assert = Xunit.Assert;
using visual_studio_psicanalista.ORM;


namespace SiteAgendamento.Tests
{
    public class UsuarioRepositorioTests
    {
        private readonly Mock<AndrezaMeloNeuropsicanalistaContext> _mockContext;
        private readonly UsuarioRepositorio _repositorio;

        public UsuarioRepositorioTests()
        {
            // Criando um Mock para o DbSet de TbUsuario
            _mockContext = new Mock<AndrezaMeloNeuropsicanalistaContext>();
            _repositorio = new UsuarioRepositorio(_mockContext.Object);
        }

        [Fact]
        public void InserirUsuario_DeveRetornarTrue_QuandoInserirComSucesso()
        {
            // Arrange
            var nome = "João";
            var email = "joao@exemplo.com";
            var telefone = "123456789";
            var senha = "senha123";
            var tipoUsuario = 1;

            var mockDbSet = new Mock<DbSet<TbUsuario>>();
            _mockContext.Setup(m => m.TbUsuarios).Returns(mockDbSet.Object);

            // Act
            var resultado = _repositorio.InserirUsuario(nome, email, telefone, senha, tipoUsuario);

            // Assert
            Xunit.Assert.True(resultado);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void ListarUsuarios_DeveRetornarListaDeUsuarios()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<TbUsuario>>();
            var usuarios = new List<TbUsuario>
            {
                new TbUsuario { Id = 1, Nome = "João", Email = "joao@exemplo.com", Telefone = "123456789", Senha = "senha123", TipoUsuario = 1 },
                new TbUsuario { Id = 2, Nome = "Maria", Email = "maria@exemplo.com", Telefone = "987654321", Senha = "senha123", TipoUsuario = 2 }
            }.AsQueryable();

            mockDbSet.As<IQueryable<TbUsuario>>().Setup(m => m.Provider).Returns(usuarios.Provider);
            mockDbSet.As<IQueryable<TbUsuario>>().Setup(m => m.Expression).Returns(usuarios.Expression);
            mockDbSet.As<IQueryable<TbUsuario>>().Setup(m => m.ElementType).Returns(usuarios.ElementType);
            mockDbSet.As<IQueryable<TbUsuario>>().Setup(m => m.GetEnumerator()).Returns(usuarios.GetEnumerator());

            _mockContext.Setup(m => m.TbUsuarios).Returns(mockDbSet.Object);

            // Act
            var resultado = _repositorio.ListarUsuarios();

            // Assert
            Assert.Equal(2, resultado.Count);
            Assert.Equal("João", resultado[0].Nome);
            Assert.Equal("Maria", resultado[1].Nome);
        }

        [Fact]
        public void AtualizarUsuario_DeveRetornarTrue_QuandoAtualizacaoForBemSucedida()
        {
            // Arrange
            var id = 1;
            var nome = "João Atualizado";
            var email = "joao_atualizado@exemplo.com";
            var telefone = "987654321";
            var senha = "senha123";
            var tipoUsuario = 1;

            // Criando o mock do DbSet como IQueryable
            var usuario = new TbUsuario { Id = id, Nome = "João", Email = "joao@exemplo.com", Telefone = "123456789", Senha = "senha123", TipoUsuario = 1 };
            var listaUsuarios = new List<TbUsuario> { usuario }.AsQueryable();

            var mockDbSet = new Mock<DbSet<TbUsuario>>();

            // Configurando o mock do DbSet para se comportar como IQueryable
            mockDbSet.As<IQueryable<TbUsuario>>()
                     .Setup(m => m.Provider).Returns(listaUsuarios.Provider);
            mockDbSet.As<IQueryable<TbUsuario>>()
                     .Setup(m => m.Expression).Returns(listaUsuarios.Expression);
            mockDbSet.As<IQueryable<TbUsuario>>()
                     .Setup(m => m.ElementType).Returns(listaUsuarios.ElementType);
            mockDbSet.As<IQueryable<TbUsuario>>()
                     .Setup(m => m.GetEnumerator()).Returns(listaUsuarios.GetEnumerator());

            // Configurando o mock do contexto
            _mockContext.Setup(m => m.TbUsuarios).Returns(mockDbSet.Object);

            // Act
            var resultado = _repositorio.AtualizarUsuario(id, nome, email, telefone, senha, tipoUsuario);

            // Assert
            Assert.True(resultado);
            Assert.Equal(nome, usuario.Nome);
            Assert.Equal(email, usuario.Email);
            Assert.Equal(telefone, usuario.Telefone);
            Assert.Equal(senha, usuario.Senha);
            Assert.Equal(tipoUsuario, usuario.TipoUsuario);

            // Verificando que SaveChanges foi chamado uma vez
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }


        [Fact]
        public void ExcluirUsuario_DeveRetornarTrue_QuandoExcluirComSucesso()
        {
            // Arrange
            var id = 12;
            var usuario = new TbUsuario
            {
                Id = id,
                Nome = "João",
                Email = "joao@exemplo.com",
                Telefone = "123456789",
                Senha = "senha123",
                TipoUsuario = 1
            };

            // Lista de usuários simulada
            var usuarios = new List<TbUsuario>
    {
        new TbUsuario { Id = 1, Nome = "João", Email = "joao@exemplo.com" },
        new TbUsuario { Id = 2, Nome = "Maria", Email = "maria@exemplo.com" },
        usuario // Usuário que será excluído
    }.AsQueryable(); // Converte para IQueryable

            // Mock do DbSet<TbUsuario>
            var mockDbSet = new Mock<DbSet<TbUsuario>>();

            // Configura o mock do DbSet para retornar o IQueryable
            mockDbSet.As<IQueryable<TbUsuario>>()
                     .Setup(m => m.Provider).Returns(usuarios.Provider);
            mockDbSet.As<IQueryable<TbUsuario>>()
                     .Setup(m => m.Expression).Returns(usuarios.Expression);
            mockDbSet.As<IQueryable<TbUsuario>>()
                     .Setup(m => m.ElementType).Returns(usuarios.ElementType);
            mockDbSet.As<IQueryable<TbUsuario>>()
                     .Setup(m => m.GetEnumerator()).Returns(usuarios.GetEnumerator());

            // Configura o contexto para retornar o mock do DbSet
            _mockContext.Setup(m => m.TbUsuarios).Returns(mockDbSet.Object);

            // Act
            var resultado = _repositorio.ExcluirUsuario(id);

            // Assert
            Assert.True(resultado);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);  // Verifica que SaveChanges foi chamado
        }

    }
}
