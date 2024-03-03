using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using System.Text;

namespace bookstore_backend.Services
{
    public static class Helper
    {

        public static SymmetricSecurityKey TokenHelper ()
        {
            var secret = Encoding.ASCII.GetBytes("this_is_a_very_strong_key_sovidinjso;diifh");
            return new SymmetricSecurityKey(secret);
        }
       
    }
}
