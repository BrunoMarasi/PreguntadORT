using TP06.Models;

public static class Juego {
    public static string username;
    public static int puntajeActual;
    private static int cantidadPreguntasCorrectas;
    public static int contadorNroPreguntaActual;
    private static Pregunta preguntaActual;
    private static List<Pregunta> listaPreguntas = new List<Pregunta>();
    private static List<Respuesta> listaRespuestas = new List<Respuesta>();

    public static void InicializarJuego()
    {
        username = string.Empty;
        puntajeActual = 0;
        cantidadPreguntasCorrectas = 0;
        contadorNroPreguntaActual = 0;
        preguntaActual = null;
        listaPreguntas = new List<Pregunta>();
        listaRespuestas = new List<Respuesta>();
    }

    public static List<Categoria> ObtenerCategorias()
    {
        return BD.ObtenerCategorias();
    }

    public static List<Dificultad> ObtenerDificultades()
    {
        return BD.ObtenerDificulades();
    }

    public static void CargarPartida(string usuario, int idDificultad, int idCategoria)
    {
        InicializarJuego();
        username = usuario;
        listaPreguntas = BD.ObtenerPreguntas(idDificultad, idCategoria);
    }

    public static Pregunta ObtenerPregunta()
    {
        if (listaPreguntas.Count > contadorNroPreguntaActual)
        {
            preguntaActual = listaPreguntas[contadorNroPreguntaActual];
            return preguntaActual;
        }
        return null;
    }

    public static List<Respuesta> ObtenerProximasRespuestas(int idPregunta) 
    {
        listaRespuestas = BD.ObtenerRespuestas(idPregunta);
        return listaRespuestas;
    }


    public static bool VerificarRespuesta(int idRespuesta)
    {
        var respuesta = listaRespuestas.FirstOrDefault(r => r.idRespuesta == idRespuesta);
        if (respuesta != null && respuesta.correcta)
        {
            if (preguntaActual.idDificultad == 1)
            {
                puntajeActual += 10;
            }
            else if (preguntaActual.idDificultad == 2)
            {
                puntajeActual += 20;
            }
            else
            {
                puntajeActual += 30;
            }
            cantidadPreguntasCorrectas++;
            contadorNroPreguntaActual++;
            return true;
        }
        contadorNroPreguntaActual++;
        return false;
    }

}