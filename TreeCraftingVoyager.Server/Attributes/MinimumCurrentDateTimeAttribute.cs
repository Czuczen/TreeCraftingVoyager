using System.ComponentModel.DataAnnotations;

namespace TreeCraftingVoyager.Server.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class MinimumCurrentDateTimeAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
            if (value is DateTime date)
                return date >= DateTime.UtcNow;

            return false;
        }
}