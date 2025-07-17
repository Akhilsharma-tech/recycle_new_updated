using AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.AutoMapper;
using ElectronicRecyclers.One800Recycling.Application.Logging;
using ElectronicRecyclers.One800Recycling.Application.ViewModels;
using ElectronicRecyclers.One800Recycling.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NHibernate.Mapping.ByCode.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicRecyclers.One800Recycling.Application.Services
{
    public static class SmartyStreetsService
    {
        private const string AuthId = "f6ca4315-ca1d-4ce0-88af-df569533acf4";
        private const string AuthToken = "w3GakWNYSTqXDvz1FZlh";
        private const string Country = "US";
        private static IConfiguration _configuration;
        private static IMapper _mapper;

        public static void Configure(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        private static string SmartyStreetsApiUrl
        {
            get { return _configuration["SmartyStreetsApiUrl"]; }
        }

        private static Uri CreateWebRequestUri()
        {
            return new Uri(string.Format(
                "{0}?auth-id={1}&auth-token={2}",
                SmartyStreetsApiUrl,
                AuthId,
                AuthToken));
        }

        private static string SendWebRequest(string requestPayLoad)
        {
            try
            {
                if (string.IsNullOrEmpty(requestPayLoad))
                    return string.Empty;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                var request = (HttpWebRequest)WebRequest.Create(CreateWebRequestUri());
                request.Method = "POST";

                using (var stream = request.GetRequestStream())
                using (var writer = new StreamWriter(stream))
                    writer.Write(requestPayLoad);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                    if (stream != null)
                        using (var reader = new StreamReader(stream))
                            return reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                LogManager.GetLogger().Error(ex.Message, ex);
            }

            return string.Empty;
        }

        public static IEnumerable<KeyValuePair<int, Address>> VerifyAddress(
            IEnumerable<KeyValuePair<int, Address>> addresses)
        {
            var requestPayLoad = JsonConvert.SerializeObject(
                _mapper.Map<SmartyStreetsRequestJson>(addresses));

            var response = SendWebRequest(requestPayLoad);
            if (string.IsNullOrEmpty(response))
                return new Dictionary<int, Address>();

            var responseAddresses = JsonConvert
            .DeserializeObject<IEnumerable<SmartyStreetsResponseJson>>(response);

            return _mapper.Map<IDictionary<int, Address>>(responseAddresses); 
        }

        public static Address VerifyAddress(Address address)
        {
            var results = VerifyAddress(new List<KeyValuePair<int, Address>>
            {
                new KeyValuePair<int, Address>(1, address)
            });

            return (results.Any()) ? results.First().Value : null;
        }

        public static Address VerifyAddress(string addressLine, string postalCode)
        {
            return VerifyAddress(new Address(addressLine, "", " ", " ", postalCode, Country));
        }

        public static bool IsAddressVerified(Address address)
        {
            return VerifyAddress(address) != null;
        }
    }
}
