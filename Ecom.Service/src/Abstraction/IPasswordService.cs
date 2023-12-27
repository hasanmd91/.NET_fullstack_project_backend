using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.Service.src.Abstraction
{

    public interface IPasswordService
    {
        void HashPassword(string originalPassword, out string hashedPassword, out byte[] salt);
        bool VerifyPassword(string originalPassword, string hashedPassword, byte[] salt);
    }

}