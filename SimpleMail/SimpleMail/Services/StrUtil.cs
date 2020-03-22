using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleMail.Services
{
    public static class StrUtil
    {
        static StrUtil() { }

        public static String removeDomain(String address)
        {
            return address.Split(new char[] { '@' })[0];
        }

        
        async public static Task<bool> ValidateAddress(String address)
        {
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            // Alternative (more complex) regex: ^([a - zA - Z0 - 9_\-\.] +)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a - zA - Z0 - 9\-]+\.)+))([a - zA - Z]{2,4}|[0-9]{1,3})(\]?)$
            // Regexes from: http://regexlib.com/Search.aspx?k=email&AspxAutoDetectCookieSupport=1
            Match m = Regex.Match(address, pattern);
            return m.Success;
        }
        

    }
}