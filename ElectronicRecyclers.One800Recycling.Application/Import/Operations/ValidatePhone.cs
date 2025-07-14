using System.Collections.Generic;




namespace ElectronicRecyclers.One800Recycling.Application.Import.Operations
{
    public class ValidatePhone 
    {
        private readonly string column;
        private readonly string countryColumn;
        private readonly string validationColumn;
       
        public ValidatePhone(string column, string countryColumn, string validationColumn)
        {
            this.column = column;
            this.countryColumn = countryColumn;
            this.validationColumn = validationColumn;
        }

        public override IEnumerable<Dictionary<string,object>> Execute(IEnumerable<Dictionary<string,object>> rows)
        {
            foreach (var row in rows)
            {
                row[validationColumn] = true;
                var phone = Phone.EmptyPhone();
                var number = row[column] as string;

                if (string.IsNullOrWhiteSpace(number) == false &&
                    number != "0" &&
                    Phone.TryParse(row[countryColumn] as string, number, out phone) == false)
                {
                    row[validationColumn] = false;
                }

                row[column] = phone;

                yield return row;
            }
        }
    }
}