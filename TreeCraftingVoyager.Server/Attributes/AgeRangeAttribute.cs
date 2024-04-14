using System.ComponentModel.DataAnnotations;

namespace TreeCraftingVoyager.Server.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AgeRangeAttribute : ValidationAttribute
{
    private readonly int _minAge;
    private readonly int _maxAge;
    private readonly bool _allowNull;

    public AgeRangeAttribute(int minAge, int maxAge, bool allowNull = false)
    {
        _minAge = minAge;
        _maxAge = maxAge;
        _allowNull = allowNull;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null)
        {
            if (_allowNull)
                return ValidationResult.Success;

            return new ValidationResult("Pole jest wymagane.", new[] { validationContext.MemberName });
        }

        if (!DateTime.TryParse(value.ToString(), out var dateOfBirth))
            return new ValidationResult("Nieprawidłowa data urodzenia.", new[] { validationContext.MemberName });

        var age = CalculateAge(dateOfBirth);

        if (age < _minAge || age > _maxAge)
            return new ValidationResult($"Proszę wprowadzić wiek między {_minAge} a {_maxAge}.", new[] { validationContext.MemberName });

        return ValidationResult.Success;
    }

    private static int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age))
            age--;

        return age;
    }
}