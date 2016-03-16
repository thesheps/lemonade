using System.Collections.Generic;
using System.Linq;
using Lemonade.Web.Core.Services;

namespace Lemonade.Web.Services
{
    public class PseudoResourceTranslator : ITranslateResource
    {
        public string Translate(string resource, string locale, string targetLocale)
        {
            return $"[{targetLocale} - {string.Join("", resource.Select(GetTranslatedCharacter))}]";
        }

        private char GetTranslatedCharacter(char c)
        {
            char result;
            return _replacements.TryGetValue(c, out result) ? result : c;
        }

        private readonly Dictionary<char, char> _replacements = new Dictionary<char, char>
        {
            {'A', 'Å'},
            {'B', 'ß'},
            {'C', 'C'},
            {'D', 'Đ'},
            {'E', 'Ē'},
            {'F', 'F'},
            {'G', 'Ğ'},
            {'H', 'Ħ'},
            {'I', 'Ĩ'},
            {'J', 'Ĵ'},
            {'K', 'Ķ'},
            {'L', 'Ŀ'},
            {'M', 'M'},
            {'N', 'Ń'},
            {'O', 'Ø'},
            {'P', 'P'},
            {'Q', 'Q'},
            {'R', 'Ŗ'},
            {'S', 'Ŝ'},
            {'T', 'Ŧ'},
            {'U', 'Ů'},
            {'V', 'V'},
            {'W', 'Ŵ'},
            {'X', 'X'},
            {'Y', 'Ÿ'},
            {'Z', 'Ż'},
            {'a', 'ä'},
            {'b', 'þ'},
            {'c', 'č'},
            {'d', 'đ'},
            {'e', 'ę'},
            {'f', 'ƒ'},
            {'g', 'ģ'},
            {'h', 'ĥ'},
            {'i', 'į'},
            {'j', 'ĵ'},
            {'k', 'ĸ'},
            {'l', 'ľ'},
            {'m', 'm'},
            {'n', 'ŉ'},
            {'o', 'ő'},
            {'p', 'p'},
            {'q', 'q'},
            {'r', 'ř'},
            {'s', 'ş'},
            {'t', 'ŧ'},
            {'u', 'ū'},
            {'v', 'v'},
            {'w', 'ŵ'},
            {'x', 'χ'},
            {'y', 'y'},
            {'z', 'ž'}
        };
    }
}