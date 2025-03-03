
using MongoDB.Driver;
using MongoDB.Bson; // ✅ Import this for ObjectId
using Microsoft.Extensions.Options;
using UsersCrud.Models;

namespace UsersCrud.Services 
{
    public class UserServices
    {
        private readonly IMongoCollection<User> _users;

        public UserServices(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDB:DatabaseName"]);
            _users = database.GetCollection<User>(config["MongoDB:CollectionName"]);
        }

        // ✅ Get all users
        public List<User> Get() => _users.Find(user => true).ToList();

        // ✅ Get a single user by ID (Convert string to ObjectId)
        public User Get(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return null; // Invalid ObjectId format

            return _users.Find(user => user.Id.Equals(objectId.ToString())).FirstOrDefault();
        }

        // ✅ Create a new user
        public void Create(User user) => _users.InsertOne(user);

        // ✅ Update a user
        public void Update(string id, User userIn)
        {
            if (ObjectId.TryParse(id, out ObjectId objectId))
                _users.ReplaceOne(user => user.Id.Equals(objectId.ToString()), userIn);
        }

        // ✅ Delete a user (Convert id to ObjectId)
        public void Remove(string id)
        {
            if (ObjectId.TryParse(id, out ObjectId objectId))
                _users.DeleteOne(user => user.Id.Equals(objectId.ToString()));
        }
    }
}
