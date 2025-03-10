﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UsersCrud.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")] // Ensure field name matches MongoDB
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }
    }

}

// Mongo DB - MyAppDB -> Users
// CREATE DATABASE MyAppDB;
//  USE MyAppDB;

