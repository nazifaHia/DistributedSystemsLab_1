using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography.Interfaces
{
    interface ICryptographyService
    {
        bool DecryptAndCheck(string clearText, string salt, string encryptedSecret);
        string EncryptSecret(string secretSalt, string secret);
        string GetSecretSalt();
       
    }
}
