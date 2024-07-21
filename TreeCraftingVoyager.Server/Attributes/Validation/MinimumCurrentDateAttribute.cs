using System.ComponentModel.DataAnnotations;

namespace TreeCraftingVoyager.Server.Attributes.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class MinimumCurrentDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateTime date)
            return date.Date >= DateTime.UtcNow.Date;

        return false;
    }
}
