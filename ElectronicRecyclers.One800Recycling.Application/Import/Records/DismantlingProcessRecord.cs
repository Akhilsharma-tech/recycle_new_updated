using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Records
{
    [DelimitedRecord("\t"), IgnoreEmptyLines, IgnoreFirst]
    public class DismantlingProcessRecord
    {
        [FieldTrim(TrimMode.Both, '"')]
        public string Name;

        [FieldTrim(TrimMode.Both, '"')]
        public string Type;

        public string LossPercentageDuringRecycling;

        public string ClimateChangeImpact;

        public string ResourceDepletionImpact;

        public string WaterWithdrawalImpact;
    }
}