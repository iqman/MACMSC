using System;
using System.Collections.Generic;

namespace Shared.Dto
{
    public class DataEntity
    {
        /// <summary>
        /// Randomly generated at user to uniquely identify this data.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Symmetrically encrypted data payload.
        /// </summary>
        public FilePayload Payload { get; set; }

        /// <summary>
        /// Collection of attributes, each encrypted with user public key
        /// </summary>
        public IList<Attribute> Attributes { get; set; }

        /// <summary>
        /// Info used for the symmetrically encrypted payload, encrypted with user public key
        /// </summary>
        public AesEncryptionInfo AesInfo { get; set; }

        /// <summary>
        /// The author's signature of the data entity.
        /// </summary>
        public Signature Signature { get; set; }

        /// <summary>
        /// Used for integrity checks
        /// </summary>
        public override int GetHashCode()
        {
            int hash = Id.GetHashCode();

            foreach (Attribute attribute in Attributes)
            {
                hash ^= attribute.GetHashCode();
            }
            hash ^= Payload.GetHashCode();
            hash ^= AesInfo.GetHashCode();

            return hash;
        }
    }
}
