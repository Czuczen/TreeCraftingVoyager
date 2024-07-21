using System.ComponentModel.DataAnnotations;

namespace TreeCraftingVoyager.Server.Attributes.Validation;

public class MinimumNullableCurrentDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return true;

        if (value is DateTime date)
            return date.Date >= DateTime.UtcNow.Date;

        return false;
    }
}