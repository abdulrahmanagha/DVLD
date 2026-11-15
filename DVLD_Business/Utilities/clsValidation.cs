using System;
using System.Linq;

namespace DVLD.People
{
    public static class clsValidation
    {
        static string[] ExtentionsToEmail = { ".com", ".net", ".org", ".edu", ".gov", ".mil" };
        static string[] MailToEmail = { "gmail", "yahoo" };

        static public bool IsValidUserNameStart(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && (Char.IsLetter(email[0]) || char.IsDigit(email[0]));
        }

        static public bool IsSingleAtSymbol(string email)
        {
            int posAt = email.IndexOf('@');

            return (posAt != -1) && posAt == email.LastIndexOf('@');
        }

        static public bool IsValidTLD(string email)
        {
            foreach (string extention in ExtentionsToEmail)
            {
                if (email.EndsWith(extention, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        static public string GetDomainName(string email)
        {
            int atIndex = email.IndexOf('@');
            if (atIndex == -1 || atIndex >= email.Length - 1)
                return null;

            string domain = email.Substring(atIndex + 1);
            string[] parts = domain.Split('.');

            return parts.Length > 0 ? parts[0].ToLower() : null;
        }

        static public bool IsKnownDomainName(string email)
        {
            if (!IsValidTLD(email))
                return false;

            string domainName = GetDomainName(email);

            return (domainName != null && MailToEmail.Contains(domainName));
        }
    }
}
