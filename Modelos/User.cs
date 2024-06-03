using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTFG.Modelos
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Nombre")]
        public string Nombre { get; set; }
        [BsonElement("Apellidos")]
        public string Apellidos { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Apodo")]
        public string Apodo { get; set; }
        [BsonElement("Pass")]
        public string Pass { get; set; }
        [BsonElement("Rol")]
        public string Rol { get; set; }
        [BsonElement("Opciones")]
        public Opciones Opciones { get; set; }

        public User()
        {
        }

        public User(string nombre, string apellidos, string email, string pass, string rol, string tema, string idioma, string tamLetra)
        {
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.Email = email;
            this.Pass = pass;
            this.Rol = rol;
            Opciones opciones = new Opciones(tema, idioma, tamLetra);
            this.Opciones = opciones;
        }

        public User(string nombre, string apellidos, string email, string pass, string rol)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Email = email;
            Pass = pass;
            Rol = rol;
        }

        public string HashPassword(string pass)
        {
            // Genera el hash de la contraseña con bcrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(pass);
            return hashedPassword;
        }

        public bool VerifyPassword(string pass, string hashedPassword)
        {
            
            // Verifica la contraseña con el hash almacenado
            if (hashedPassword!= null && pass != null)
            return BCrypt.Net.BCrypt.Verify(hashedPassword, pass);
            else return false;
        }
    }
}
