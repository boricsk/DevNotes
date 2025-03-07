using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNotes.Model
{
    public class DevNoteDataModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Project name")]
        public string? DevProjectName { get; set; }
        [BsonElement("Note name")]
        public string? NoteName { get; set; }
        [BsonElement("Note")]
        public byte[]? DevNote { get; set; }
        [BsonElement("Issue date")]
        public DateTime NoteCreatedDate { get; set; }
    }
}
