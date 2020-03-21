using System;

namespace SimpleMail.Services
{
    public static class StrUtil
    {
        static StrUtil() { }

        public static String removeDomain(String address)
        {
            return address.Split(new char[] { '@' })[0];
        }
    }
}