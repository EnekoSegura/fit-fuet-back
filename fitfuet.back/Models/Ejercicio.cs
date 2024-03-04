using System;

namespace fitfuet.back.Models
{
    public enum TipoEjercicio
    {
        Cardio,
        Fuerza
    }

    public class Ejercicio : EntidadBase
    {
        public string Nombre { get; set; }
        public string MusculoEjercitado { get; set; }
        public TipoEjercicio Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Explicacion { get; set; }
        public float? Met { get; set; }
        public string Imagen { get; set; }

        public Ejercicio(string nombre, string musculo, TipoEjercicio tipo, string descripcion, string explicacion, float met, string imagen)
        {
            Nombre = nombre;
            MusculoEjercitado = musculo;
            Tipo = tipo;
            Descripcion = descripcion;
            Explicacion = explicacion;
            Met = met;
            Imagen = imagen;
        }

        public Ejercicio(string nombre, string musculo, TipoEjercicio tipo, string descripcion, string explicacion, float met)
        {
            Nombre = nombre;
            MusculoEjercitado = musculo;
            Tipo = tipo;
            Descripcion = descripcion;
            Explicacion = explicacion;
            Met = met;
            Imagen = "";
        }

        public Ejercicio()
        {
            // Parameterless constructor
        }
    }
}