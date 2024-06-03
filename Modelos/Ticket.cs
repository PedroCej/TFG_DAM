using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
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
        [BsonElement("FechaInicio")]
        public DateTime FechaInicio { get; set; }
        [BsonElement("FechaFin")]
        public DateTime FechaFin { get; set; }
        [BsonElement("Estado")]

        public string Estado { get; set; }
        
        [BsonElement("Comentarios")]
        public List<string> Comentarios { get; set; }
        [BsonElement("Usuario")]
        public User Usuario { get; set; }
        [BsonElement("Asignado")]
        public User Asignado { get; set; }

        public Ticket()
        {
        }

        public Ticket(string titulo, string descripcion, string prioridad, DateTime fechaInicio, DateTime fechaFin, string estado, List<string> comentarios, User usuario, User asignado)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Prioridad = prioridad;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Estado = estado;
            Comentarios = comentarios;
            Usuario = usuario;
            Asignado = asignado;
        }



    }
}
