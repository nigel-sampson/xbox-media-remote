using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using XboxMediaRemote.App.Resources;

namespace XboxMediaRemote.App.ViewModels
{
    public static class AlphaNumericGroups
    {
        private static Dictionary<string, Regex> expressions;

        public static IDictionary<string, Regex> GetExpressions()
        {
            if (expressions != null)
                return expressions;

            expressions = new Dictionary<string, Regex>();

            expressions.Add(Strings.GroupsNumberTitle, new Regex(@"^\d"));
            expressions.Add(Strings.GroupsSymbolTitle, new Regex(@"^[\W_]"));

            Enumerable.Range(65, 26)
                .Select(i => (char)i)
                .ForEach(c => expressions.Add(c.ToString(), new Regex(String.Format("^{0}", c), RegexOptions.IgnoreCase)));

            return expressions;
        }
    }
}
