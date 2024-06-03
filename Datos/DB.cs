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
            const string connectionUri = "mongodb+srv://admin:IoRjIGht5MgXss4z@cluster0.oz2yuw8.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            database = client.GetDatabase("ticketing");
            usersCollection = database.GetCollection<User>("users");
            ticketsCollection = database.GetCollection<Ticket>("tickets");
        }

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

        public void MeterTicket(string titulo, string descripcion, string prioridad, DateTime fechaInicio, DateTime fechaFin, string estado, List<string> comentarios, User usuario, User asignado)
        {
            Ticket ticket = new Ticket { Titulo = titulo, Descripcion = descripcion, Prioridad = prioridad, FechaInicio = fechaInicio, FechaFin = fechaFin, Estado = estado, Comentarios = comentarios, Usuario = usuario, Asignado = asignado };
            ticketsCollection.InsertOne(ticket);
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

    }





    /* var filter = Builders<BsonDocument>.Filter.Eq("Apodo", email);
            var user = usersCollection.Find(filter).FirstOrDefault();

            if (user == null)
            {
                return false; // Usuario no encontrado
            }

            // Verifica la contraseña
            User userModel = BsonSerializer.Deserialize<User>(user);

            return userModel.VerifyPassword(userModel.Pass, password);*/
}
