
using System;
using System.Runtime.Serialization;

namespace Shared.Dto
{
    [DataContract]
    public class FilePayload
    {
        [DataMember]
        public byte[] Name { get; set; }

        [DataMember]
        public byte[] Content { get; set; }

        [DataMember]
        public int Size { get; set; }

        public FilePayload()
        {
        }

        public FilePayload(byte[] name)
        {
            if (name == null) throw new ArgumentNullException("name");
            Name = name;
        }

        public FilePayload(byte[] name, int size)
            : this(name)
        {
            this.Size = size;
        }

        public FilePayload(byte[] name, byte[] content)
            : this(name)
        {
            if (content == null) throw new ArgumentNullException("content");

            Content = content;
            Size = content.Length;
        }

        public override int GetHashCode()
        {
            if (Content != null)
            {
                return Name.ComputeContentHash() ^ Content.ComputeContentHash();
            }
            return Name.ComputeContentHash();
        }
    }
}
