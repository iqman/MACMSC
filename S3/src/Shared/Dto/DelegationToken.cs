namespace Shared.Dto
{
    public class DelegationToken
    {
        public byte[] ToUser { get; set; }

        public DelegationToken()
        {
        }

        public DelegationToken(byte[] toUser)
        {
            ToUser = toUser;
        }
    }
}
