using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTFG.Modelos
{
    public class Ticket
    {
        

        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Titulo")]
        public string Titulo { get; set; }
        [BsonElement("Descripcion")]
        public string Descripcion { get; set; } 
        [BsonElement("Prioridad")]
        public string Prioridad { get; set; }
        [BsonElement("Categoria")]
        public string Categoria { get; set; }
        [BsonElement("FechaInicio")]
        public DateTime FechaInicio { get; set; }
        [BsonElement("FechaUltimaModificacion")]
        public DateTime FechaUltimaModificacion { get; set; }
        [BsonElement("Estado")]
        public string Estado { get; set; }
        [BsonElement("Comentarios")]
        public List<string> Comentarios { get; set; }
        [BsonElement("Usuario")]
        public string Usuario { get; set; }
        [BsonElement("Asignado")]
        public string AsignadoA { get; set; }
        [BsonElement("Imagen")]
        public byte[] Imagen { get; set; }
        [BsonElement("Borrado")]
        public bool Borrado { get; set; }

        public Ticket()
        {
        }

        public Ticket(string titulo, string descripcion, string prioridad, DateTime fechaInicio, DateTime fechaFin, string estado, List<string> comentarios, string usuario, string asignado)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Prioridad = prioridad;
            FechaInicio = fechaInicio;
            FechaUltimaModificacion = fechaFin;
            Estado = estado;
            Comentarios = comentarios;
            Usuario = usuario;
            AsignadoA = asignado;
            Borrado = false;
        }

        public Ticket(string titulo, string descripcion, string categoria, string prioridad, DateTime fechaInicio, string usuario, byte[] imagen)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Categoria = categoria;
            Prioridad = prioridad;
            FechaInicio = fechaInicio;
            Estado = null;
            Comentarios = null;
            Usuario = usuario;
            AsignadoA = null;
            Borrado = false;
            Imagen = imagen;
        }

        [BsonIgnore]
        public bool IsChecked { get; set; }


    }
}
