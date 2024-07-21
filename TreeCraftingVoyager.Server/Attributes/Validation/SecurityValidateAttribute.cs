using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TreeCraftingVoyager.Server.Attributes.Validation;

public class SecurityValidateAttribute : ValidationAttribute
{
    public static readonly IEnumerable<string> IllegalNames = new List<string>
    {
        "admin",

        "kurwa",
        "chuj",
        "pizda",
        "pierdol",
        "jebany",
        "skurwiel",

        "fuck",
        "shit",
        "bitch",
        "asshole",
        "bastard",
        "cunt",
        "dick",
        "douche",
        "fucker",
        "motherfucker",
        "nigger",
        "pussy",
        "slut",
        "whore",
    };

    public static readonly IEnumerable<string> IllegalRegexes = new List<string>
    {
        @"WHERE\s+ID",           // WHERE Id
        @"SELECT\s+.*\s+FROM",   // SELECT ... FROM
        @"INSERT\s+INTO",        // INSERT INTO
        @"UPDATE\s+.*\s+SET",    // UPDATE ... SET
        @"DELETE\s+FROM",        // DELETE FROM
        @"DROP\s+TABLE",         // DROP TABLE
        @"TRUNCATE\s+TABLE",     // TRUNCATE TABLE
        @"ALTER\s+TABLE",        // ALTER TABLE
        @"CREATE\s+TABLE",       // CREATE TABLE
        @"EXEC\s+",              // EXEC
        @"UNION\s+SELECT",       // UNION SELECT
        @"xp_",                  // Extended stored procedures
        @"sp_",                  // System stored procedures
    };

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        if (value is not string valueAsString)
            return new ValidationResult("Nieprawidłowa wartość.");

        if (valueAsString.StartsWith(" ") || valueAsString.EndsWith(" "))
            return new ValidationResult("Wartość nie może zaczynać się ani kończyć spacją.");

        if (valueAsString.Contains("  "))
            return new ValidationResult("Wartość może posiadać tylko pojedyncze spacje.");

        if (!Regex.IsMatch(valueAsString, @"^[a-zA-Z0-9\s\.,;:\-_\+!'" + "\"" + @"“?()\nąćęłńóśźżĄĆĘŁŃÓŚŹŻäöüÄÖÜß]+$"))
            return new ValidationResult("Wartość zawiera niedozwolone znaki.");

        var valueAsStringLower = valueAsString.ToLowerInvariant();
        if (IllegalNames.Any(n => valueAsStringLower.Contains(n.ToLowerInvariant())))
            return new ValidationResult("Wartość zawiera nieakceptowane słowo.");

        foreach (var pattern in IllegalRegexes)
            if (Regex.IsMatch(valueAsString, pattern, RegexOptions.IgnoreCase))
                return new ValidationResult("Wartość zawiera nieakceptowane słowo.");

        return ValidationResult.Success;
    }
}
