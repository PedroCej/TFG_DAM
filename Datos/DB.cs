using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ProyectoTFG.Modelos;

namespace ProyectoTFG.Datos
{
    public class DB
    {
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<User> usersCollection;
        private readonly IMongoCollection<Ticket> ticketsCollection;

        public DB()
        {
            //const string connectionUri = "mongodb+srv://admin:IoRjIGht5MgXss4z@cluster0.oz2yuw8.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            const string connectionUri = "mongodb://admin:IoRjIGht5MgXss4z@ac-ly1dpis-shard-00-00.oz2yuw8.mongodb.net:27017,ac-ly1dpis-shard-00-01.oz2yuw8.mongodb.net:27017,ac-ly1dpis-shard-00-02.oz2yuw8.mongodb.net:27017/?ssl=true&replicaSet=atlas-qsszly-shard-0&authSource=admin&retryWrites=true&w=majority&appName=Cluster0";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            database = client.GetDatabase("ticketing");
            usersCollection = database.GetCollection<User>("users");
            ticketsCollection = database.GetCollection<Ticket>("tickets");
        }

        //
        //  Metodos de User   -----------------
        //
        public bool ExisteUser(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email) | Builders<User>.Filter.Eq("Apodo", email);
            var user = usersCollection.Find(filter).FirstOrDefault();

            if (user == null)
            {
                return false; // Usuario no encontrado
            }

            return true; // Usuario encontrado
        }

        public void MeterUser(string nombre, string apellidos, string email, string apodo, string pass, string rol)
        {
            if (!ExisteUser(email)&& !ExisteUser(apodo)) // Si no hay ningun user con ese apodo o email
            {
                User user = new User();
                string hashedPassword = user.HashPassword(pass);

                if (apodo != null)
                {
                    user = new User { Nombre = nombre, Apellidos = apellidos, Email = email, Apodo = apodo, Pass = hashedPassword, Rol = rol };
                }
                else
                {
                    user = new User { Nombre = nombre, Apellidos = apellidos, Email = email, Pass = hashedPassword, Rol = rol };
                }

                usersCollection.InsertOne(user);
            }
            
        }
        public bool LoginUser(string email, string password)
        {
            var filter = Builders<User>.Filter.Eq("Email", email) | Builders<User>.Filter.Eq("Apodo", email);
            var user = usersCollection.Find(filter).FirstOrDefault();

            if (user == null)
            {
                return false; // Usuario no encontrado
            }

            // Verifica la contraseña
            return user.VerifyPassword(user.Pass, password);
        }

        public User GetUser(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email) | Builders<User>.Filter.Eq("Apodo", email);
            var user = usersCollection.Find(filter).FirstOrDefault();

            return user;
        }


        //
        //  Metodos de Ticket   -----------------
        //
        public void UserMeterTicket(string titulo, string descripcion, string categoria, string prioridad, DateTime fechaInicio, string emailUser, byte[] imagen)
        {
            Ticket ticket = new Ticket { Titulo = titulo, Descripcion = descripcion, Categoria = categoria, Prioridad = prioridad, FechaInicio = fechaInicio, FechaUltimaModificacion = fechaInicio, Estado = "Sin seguimiento", Usuario = emailUser, Imagen=imagen};
            ticketsCollection.InsertOne(ticket);
        }

        public List<Ticket> GetTickets()
        {
            var filter = Builders<Ticket>.Filter.Empty;
            return ticketsCollection.Find(filter).ToList();
        }

        public List<Ticket> GetTickets(string email, bool oculto)
        {
            var filter = Builders<Ticket>.Filter.Eq("Usuario", email) & Builders<Ticket>.Filter.Eq("Borrado", oculto);
            return ticketsCollection.Find(filter).ToList();
        }

        public List<Ticket> GetTickets(string email, string estado)
        {
            var filter = Builders<Ticket>.Filter.Eq("Usuario", email) & Builders<Ticket>.Filter.Eq("Estado", estado);
            return ticketsCollection.Find(filter).ToList();
        }

        public bool UpdateTicket(Ticket ticket)
        {
            var filter = Builders<Ticket>.Filter.Eq("_id", ticket.Id);
            var update = Builders<Ticket>.Update
                .Set("Titulo", ticket.Titulo)
                .Set("Descripcion", ticket.Descripcion)
                .Set("Categoria", ticket.Categoria)
                .Set("Prioridad", ticket.Prioridad)
                .Set("FechaInicio", ticket.FechaInicio)
                .Set("Estado", ticket.Estado)
                .Set("Usuario", ticket.Usuario);
            var result = ticketsCollection.UpdateOne(filter, update);

            return result.IsAcknowledged;
        }

        public bool OcultarTicket(Ticket ticket)
        {
            var filter = Builders<Ticket>.Filter.Eq("_id", ticket.Id);
            var update = Builders<Ticket>.Update
                .Set("Borrado", true);
            var result = ticketsCollection.UpdateOne(filter, update);

            return result.IsAcknowledged;
        }

        public bool DeleteTicket(Ticket ticket)
        {
            var filter = Builders<Ticket>.Filter.Eq("_id", ticket.Id);
            var result = ticketsCollection.DeleteOne(filter);

            return result.IsAcknowledged;
        }






    }

}
