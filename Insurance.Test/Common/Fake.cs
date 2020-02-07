using System;

namespace Insurance.Test.Mocks
{
    internal static class Fake
    {
        public static string GetKey() => Guid.NewGuid().ToString();

        internal static Guid GetId(string key = null)
        {
            if (Guid.TryParse(key, out var result))
                return result;

            return Guid.Empty;
        }

        internal static string GetName(string key = null) => $"{key} FirstName";

        internal static string GetLastName(string key = null) => $"{key} LastName";

        internal static string GetPhone() => "+1 (999) 999-9999";

        internal static string GetAddress(string key = null) => $"{key} Street _Toronto, ON M9Z 9Z9";
    }
}