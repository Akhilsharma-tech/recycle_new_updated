﻿
using ElectronicRecyclers.One800Recycling.Application.Common;
using ElectronicRecyclers.One800Recycling.Application.ETLProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class CheckIsEnabledForOrganizationImport : AbstractOperation
    {
        private readonly string column;
        private readonly string validationColumn;

        public CheckIsEnabledForOrganizationImport(string column, string validationColumn)
        {
            this.column = column;
            this.validationColumn = validationColumn;
        }

        public override IEnumerable<DynamicReader> Execute(IEnumerable<DynamicReader> rows)
        {
            foreach (var row in rows)
            {

                
                var rawValue = row[column]?.ToString()?.Trim().ToLowerInvariant();

                if (!IsValidBoolean(rawValue))
                {
                    row[validationColumn] = false;
                }
                else
                {
                    row[validationColumn] = true;
                }

                yield return row;
            }
        }
        private bool IsValidBoolean(string value)
        {
            value = value?.Trim().ToLowerInvariant();

            if (value == "1" || value == "true")
                return true;
            else if (value == "0" || value == "false")
                return false;
            else
                return false;  
        }
    }
}