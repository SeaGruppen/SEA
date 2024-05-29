using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.UserValidation
{
    public interface ISuperUserValidator
    {
        bool ValidateSuperUser(string username, string password);

        void AddSuperUserCredentials(string username, string password);

        void RemoveSuperUserCredentials(string username);
    }
}
