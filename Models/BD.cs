using System.Data.SqlClient;
using Dapper;

namespace TP06.Models;

public class BD
{
    private static string _ConnectionString = "Server=localhost;DataBase=BDPreguntadORT;Trusted_Connection=true;";

    public static List<Categoria> ObtenerCategorias()
    {
        List<Categoria> listaCategorias = new List<Categoria>();
        using(SqlConnection conn = new SqlConnection(_ConnectionString)){
            string sql="SELECT * FROM Categorias";
            listaCategorias = conn.Query<Categoria>(sql).ToList();
        }
        return listaCategorias;
    }

    public static List<Dificultad> ObtenerDificulades()
    {
        List<Dificultad> listaDificultades = new List<Dificultad>();
        using(SqlConnection conn = new SqlConnection(_ConnectionString)){
            string sql="SELECT * FROM Dificultades";
            listaDificultades = conn.Query<Dificultad>(sql).ToList();
        }
        return listaDificultades;
    }

    public static List<Pregunta> ObtenerPreguntas(int idDificultad, int idCategoria)
    {
        if (idDificultad != -1 && idCategoria != -1)
        {
            List<Pregunta> listaPreguntas = new List<Pregunta>();
            using(SqlConnection conn = new SqlConnection(_ConnectionString)){
                string sql="SELECT * FROM Preguntas WHERE idDificultad = @idDificultad AND idCategoria = @idCategoria";
                listaPreguntas = conn.Query<Pregunta>(sql, new { idDificultad, idCategoria }).ToList();
            }
            return listaPreguntas;
        }
        else if (idDificultad != -1 && idCategoria == -1)
        {
            List<Pregunta> listaPreguntas = new List<Pregunta>();
            using(SqlConnection conn = new SqlConnection(_ConnectionString)){
                string sql="SELECT * FROM Preguntas WHERE idDificultad = @idDificultad";
                listaPreguntas = conn.Query<Pregunta>(sql, new { idDificultad }).ToList();
            }
            return listaPreguntas;
        }
        else if (idDificultad == -1 && idCategoria != -1) 
        {
            List<Pregunta> listaPreguntas = new List<Pregunta>();
            using(SqlConnection conn = new SqlConnection(_ConnectionString)){
                string sql="SELECT * FROM Preguntas WHERE idCategoria = @idCategoria";
                listaPreguntas = conn.Query<Pregunta>(sql, new { idCategoria }).ToList();
            }
            return listaPreguntas;
        }
        else
        {
            List<Pregunta> listaPreguntas = new List<Pregunta>();
            using(SqlConnection conn = new SqlConnection(_ConnectionString)){
                string sql="SELECT * FROM Preguntas";
                listaPreguntas = conn.Query<Pregunta>(sql).ToList();
            }
            return listaPreguntas;
        }
    }

    public static List<Respuesta> ObtenerRespuestas(int idPregunta)
    {
        List<Respuesta> listaRespuestas = new List<Respuesta>();
        using(SqlConnection conn = new SqlConnection(_ConnectionString)){
            string sql="SELECT * FROM Respuestas WHERE idPregunta = @idPregunta";
            listaRespuestas = conn.Query<Respuesta>(sql, new { idPregunta }).ToList();
        }
        return listaRespuestas;
    }


}