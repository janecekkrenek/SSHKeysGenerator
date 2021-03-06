using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Krenek.SSHKeysGenerator.Generators
{
    /// <summary>
    /// RSA Cipher
    /// </summary>
    public class RSAGenerator : GenericGenerator
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public RSAGenerator()
        {
            _key = RSA.Create(4096);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size">Size</param>
        public RSAGenerator(int size)
        {
            if (size % 1024 != 0)
            {
                throw new ArgumentException("Key Size of RSA cypher should be n * 1024!");
            }

            _key = RSA.Create(size);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size">Size</param>
        /// <param name="comment">Comment</param>
        public RSAGenerator(int size, string comment)
        {
            if (size % 1024 != 0)
            {
                throw new ArgumentException("Key Size of RSA cypher should be n * 1024!");
            }

            _key = RSA.Create(size);
            _publicComment = comment;
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Returns the SSH private key
        /// </summary>
        /// <returns></returns>
        public override string ToPrivateKey()
        {
            var builder = new StringBuilder();
            builder.AppendLine("-----BEGIN RSA PRIVATE KEY-----");
            var privateKeyBytes = Convert.ToBase64String(((RSA)_key).ExportRSAPrivateKey()).ToCharArray();
            for (var i = 0; i < privateKeyBytes.Length; i += 64)
            {
                builder.AppendLine(new String(privateKeyBytes, i, Math.Min(64, privateKeyBytes.Length - i)));
            }
            builder.AppendLine("-----END RSA PRIVATE KEY-----");
            return builder.ToString();
        }

        /// <summary>
        /// Returns the SSH public key
        /// </summary>
        /// <returns></returns>
        public override string ToPublicKey()
        {
            var builder = new StringBuilder();
            builder.AppendLine("-----BEGIN PUBLIC KEY-----");
            var publicKeyBytes = Convert.ToBase64String(((RSA)_key).ExportRSAPublicKey()).ToCharArray();
            for (var i = 0; i < publicKeyBytes.Length; i += 64)
            {
                builder.AppendLine(new String(publicKeyBytes, i, Math.Min(64, publicKeyBytes.Length - i)));
            }
            builder.AppendLine("-----END PUBLIC KEY-----");
            return builder.ToString();
        }

        /// <summary>
        /// Returns the SSH public key in PuTTY format
        /// </summary>
        /// <returns></returns>
        public override string ToPuTTYPublicKey()
        {
            byte[] sshrsaBytes = Encoding.Default.GetBytes("ssh-rsa");
            var parameters = ((RSA)_key).ExportParameters(false);
            byte[] n = parameters.Modulus;
            byte[] e = parameters.Exponent;
            string publicBase64Key;
            using (var ms = new MemoryStream())
            {
                ms.Write(ToBytes(sshrsaBytes.Length), 0, 4);
                ms.Write(sshrsaBytes, 0, sshrsaBytes.Length);

                ms.Write(ToBytes(e.Length), 0, 4);
                ms.Write(e, 0, e.Length);

                ms.Write(ToBytes(n.Length + 1), 0, 4);
                ms.Write(new byte[] { 0 }, 0, 1);
                ms.Write(n, 0, n.Length);

                ms.Flush();
                publicBase64Key = Convert.ToBase64String(ms.ToArray());
            }

            return $"ssh-rsa {publicBase64Key} {_publicComment}";
        }
        #endregion
    }
}
