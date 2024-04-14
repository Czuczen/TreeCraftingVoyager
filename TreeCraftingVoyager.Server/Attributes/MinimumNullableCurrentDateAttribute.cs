using System.ComponentModel.DataAnnotations;

namespace TreeCraftingVoyager.Server.Attributes
{
    public class MinimumNullableCurrentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            if (value is DateTime date)
                return date.Date >= DateTime.Now.Date;

            return false;
        }
    }
}
