using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Drive.Client.Extensions {
    internal static class ParseVisionResultExtension {

        public static List<string> ParseVisionResult(this IEnumerable<string> results) {
            List<string> parsedList = new List<string>();

            foreach (string item in results) {
                parsedList.Add(Regex.Replace(item, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]", string.Empty));
            }
            return parsedList;
        }
    }
}

