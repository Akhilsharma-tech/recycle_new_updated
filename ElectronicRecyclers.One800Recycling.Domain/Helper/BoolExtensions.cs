namespace ElectronicRecyclers.One800Recycling.Domain.Common.Helper
{
    public static class BoolExtensions
    {
        public static string ToYesNoString(this bool value)
        {
            return value ? "Yes" : "No";
        }
    }
}