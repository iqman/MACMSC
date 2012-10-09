using System;
using System.Xml.Serialization;

namespace S3CloudServices.Persistance
{
    public class ChildId
    {
        [XmlAttribute]
        public Guid Id { get; set; }

        public ChildId()
        {
        }

        public ChildId(Guid id)
        {
            Id = id;
        }
    }
}