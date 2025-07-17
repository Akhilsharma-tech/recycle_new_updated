﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using ElectronicRecyclers.One800Recycling.Application.Logging;



namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class FormatLookupCodes : AbstractOperation
    {
        private static string CleanString(string str)
        {
            try
            {
                return Regex.Replace(str, @"[^\w\.@-]", "", RegexOptions.None, TimeSpan.FromSeconds((1.5)));
            }
            catch (RegexMatchTimeoutException e)
            {
                LogManager.GetLogger().Error(e.Message, e);
                return string.Empty;
            }
        }

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            foreach (var row in rows)
            {
                row["Name"] = row["Name"].ToString().Trim().ToTitleCase();
                row["Code"] = CleanString(row["Code"].ToString().Trim()).ToUpper();
                row["Description"] = row["Description"].ToString().Trim();

                yield return row;
            }
        }
    }
}