//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace ElectronicRecyclers.One800Recycling.Application.Services
//{
//    public static class GoogleGeocodingService
//    {
//        private const string AuthId = "AIzaSyDhKoBPFGMPZ73aMAMEHxS4C4y-nl8Fj_I";

//        private static string GoogleGeocodingApiUrl
//        {
//            get { return ConfigurationManager.AppSettings["GoogleGeocodingApiUrl"]; }
//        }

//        private static string EncodeAddressForUrl(Address address)
//        {
//            return address.ToString().Replace(" ", "+");
//        }

//        private static Uri CreateWebRequestUri(Address address)
//        {
//            return new Uri(string.Format(
//                "{0}?address={1}&components=country:{2}&key={3}",
//                GoogleGeocodingApiUrl,
//                EncodeAddressForUrl(address),
//                address.Country,
//                AuthId));
//        }

//        private static string SendWebRequest(Address address)
//        {
//            try
//            {
//                var request = (HttpWebRequest)WebRequest.Create(CreateWebRequestUri(address));

//                using (var response = request.GetResponse())
//                using (var stream = response.GetResponseStream())
//                    if (stream != null)
//                        using (var reader = new StreamReader(stream))
//                            return reader.ReadToEnd();
//            }
//            catch (WebException ex)
//            {
//                LogManager.GetLogger().Error(ex.Message, ex);
//            }

//            return string.Empty;
//        }

//        private static bool IsWebRequestResultMatchedAddressExactly(GoogleGeocodingResult result)
//        {
//            var locationType = result.AddressGeometry.LocationType;
//            return locationType == "ROOFTOP" || locationType == "RANGE_INTERPOLATED";
//        }

//        public static Address GeocodeAddress(Address address)
//        {
//            dynamic results = JsonConvert.DeserializeObject(SendWebRequest(address));
//            if (results == null)
//                return null;

//            foreach (var result in results["results"])
//            {
//                var geocodingResult = JsonConvert
//                        .DeserializeObject<GoogleGeocodingResult>(result.ToString());

//                if (IsWebRequestResultMatchedAddressExactly(geocodingResult) == false)
//                    continue;

//                var addressResult = AddressResolver.Resolve(geocodingResult);
//                if (addressResult.IsEmpty() == false)
//                    return addressResult;
//            }

//            return null;
//        }

//        public static Address GeocodeAddress(
//            string addressLine,
//            string city,
//            string state,
//            string postalCode,
//            string country)
//        {
//            return GeocodeAddress(new Address(
//                addressLine,
//                string.Empty,
//                city,
//                state,
//                postalCode,
//                country));
//        }
//    }
//}
