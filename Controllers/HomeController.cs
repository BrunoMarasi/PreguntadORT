using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06.Models;

namespace TP06.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ConfigurarJuego()
    {
        ViewBag.listaDificultades = Juego.ObtenerDificultades();
        ViewBag.listaCategorias = Juego.ObtenerCategorias();
        return View();
    }

    public IActionResult Comenzar(string username, int idDificultad, int idCategoria)
    {
        ViewBag.username = username;
        ViewBag.idDificultad = idDificultad;
        ViewBag.idCategoria = idCategoria;
        Juego.CargarPartida(username, idDificultad, idCategoria);
        return RedirectToAction("Jugar");
    }

    public IActionResult Jugar() 
    {
        Pregunta siguientePregunta = Juego.ObtenerPregunta();

        if(siguientePregunta != null) 
        {
            ViewBag.siguientePregunta = siguientePregunta;
            ViewBag.listaRespuestas = Juego.ObtenerProximasRespuestas(siguientePregunta.idPregunta);
            return View("Juego");
        }
        else
        {

            return View("Fin");
        }
    }


    [HttpPost] 
        
    public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta) 
    {
        bool esCorrecto = Juego.VerificarRespuesta(idRespuesta);
        ViewBag.esCorrecto = esCorrecto;
        return View("Respuesta");
    }
    

}
