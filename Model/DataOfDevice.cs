using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Tivit.WebApi.VO;

namespace Tivit.WebApi.Models
{
    public class DataOfDevice : Entity
    {
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [BsonElement]
        [JsonProperty]
        public string Latitude { get; set; }

        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [BsonElement]
        [JsonProperty]
        public string Longitude { get; set; }

        [BsonElement]
        [JsonProperty]
        public DateTime CreationDate { get; set; }
        
        [BsonElement]
        [JsonProperty]
        public Device Device { get; set; }
    }
}