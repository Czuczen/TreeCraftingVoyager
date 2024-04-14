using System.ComponentModel.DataAnnotations;

namespace TreeCraftingVoyager.Server.Attributes;

public class MinimumNullableCurrentDateTimeAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
            if (value == null) return true;

            if (value is DateTime date)
                return date >= DateTime.UtcNow;

            return false;
        }
}