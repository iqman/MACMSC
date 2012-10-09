namespace Shared.Dto
{
    public class SignKeys
    {
        public SignKeys(byte[] publicAndPrivate, byte[] publicOnly)
        {
            PublicAndPrivate = publicAndPrivate;
            PublicOnly = publicOnly;
        }

        public SignKeys()
        {
            
        }

        public byte[] PublicAndPrivate { get; set; }
        public byte[] PublicOnly { get; set; }
    }
}
