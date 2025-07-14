namespace ElectronicRecyclers.One800Recycling.Application.Common
{
    public static class BoolExtensions
    {
        public static string ToYesNoString(this bool value)
        {
            return value ? "Yes" : "No";
        }
    }
}