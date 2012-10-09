namespace Shared.Dto
{
    public class KeyPair
    {
        public KeyPair(byte[] @public, byte[] @private)
        {
            Public = @public;
            Private = @private;
        }

        public KeyPair()
        {
            
        }

        public byte[] Public { get; set; }
        public byte[] Private { get; set; }
    }
}
