

using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;

namespace ElectronicRecyclers.One800Recycling.Application.AutoMapper.Profiles.Resolvers
{
    public class PhoneResolver
    {
        public static Phone Resolve(string countryCodeSource, string number)
        {
            Phone phone;
            return string.IsNullOrEmpty(number) || !Phone.TryParse(
                                                        countryCodeSource,
                                                        number, 
                                                        out phone)
                ? Phone.EmptyPhone()
                : phone;
        }
    }
}