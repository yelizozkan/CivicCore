using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AssociationMembership.Application.Common.Helpers
{
    public static class SlugHelper
    {
        public static string Generate(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;


            text = text.ToLowerInvariant().Trim();


            text = text
                .Replace("ç", "c")
                .Replace("ğ", "g")
                .Replace("ı", "i")
                .Replace("ö", "o")
                .Replace("ş", "s")
                .Replace("ü", "u")
                .Replace("Ç", "c")
                .Replace("Ğ", "g")
                .Replace("İ", "i")
                .Replace("Ö", "o")
                .Replace("Ş", "s")
                .Replace("Ü", "u");


            text = text.Replace(" ", "-");

            text = Regex.Replace(text, @"[^a-z0-9\-]", "");

            text = Regex.Replace(text, @"-+", "-");

            text = text.Trim('-');

            return text;
        }
    }
}
