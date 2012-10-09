using System;
using System.Xml.Serialization;

namespace S1CloudServices.Persistance
{
    public class EntityAttribute
    {
        [XmlAttribute]
        public Guid Id { get; set; }

        [XmlElement]
        public string Keyword { get; set; }

        public EntityAttribute()
        {
        }

        public EntityAttribute(Guid id, string keyword)
        {
            Id = id;
            Keyword = keyword;
        }
    }
}