using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using visual_studio_psicanalista.Models;
using visual_studio_psicanalista.Repositorio;
using Microsoft.AspNetCore.Authorization;
using visual_studio_psicanalista.ORM;

namespace SiteAgendamento.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly RelatorioRepositorio _relatorioRepositorio;
        private readonly ILogger<RelatorioController> _logger;
        public RelatorioController(RelatorioRepositorio relatorioRepositorio, ILogger<RelatorioController> logger)
        {
            _relatorioRepositorio = relatorioRepositorio;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAgendamentos([FromQuery] string campo1, [FromQuery] string campo2, [FromQuery] string campo3, [FromQuery] string valor1, [FromQuery] string valor2, [FromQuery] string valor3)
        {
            // Chama o m�todo da service para obter os agendamentos filtrados
            List<ViewAgendamento> agendamentos = _relatorioRepositorio.GetAgendamentos(
                campo1, campo2, campo3, valor1, valor2, valor3);

            // Retorna os agendamentos em formato JSON
            return Ok(agendamentos);
        }

    }
}

