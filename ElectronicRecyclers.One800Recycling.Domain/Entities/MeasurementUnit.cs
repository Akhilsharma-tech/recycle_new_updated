namespace ElectronicRecyclers.One800Recycling.Domain.Entities
{
    public class MeasurementUnit : Enumeration<MeasurementUnit, int>
    {
        public static readonly MeasurementUnit Gallon = new MeasurementUnit(1, "gal");

        public static readonly MeasurementUnit Mile = new MeasurementUnit(2, "mi");

        public static readonly MeasurementUnit Pound = new MeasurementUnit(3, "lb");

        public static readonly MeasurementUnit Kilowatt = new MeasurementUnit(4, "kWh");

        public static readonly MeasurementUnit Household = new MeasurementUnit(5, "household");

        public static readonly MeasurementUnit Car = new MeasurementUnit(6, "car");

        public static readonly MeasurementUnit Truck = new MeasurementUnit(7, "truck");

        public static readonly MeasurementUnit RailCar = new MeasurementUnit(8, "rail car");

        public static readonly MeasurementUnit PropaneCylinder = new MeasurementUnit(9, "home barbecue propane cylinder");

        public static readonly MeasurementUnit Therm = new MeasurementUnit(10, "therm");

        public static readonly MeasurementUnit Tree = new MeasurementUnit(11, "tree");

        public static readonly MeasurementUnit Acre = new MeasurementUnit(12, "acre");

        protected MeasurementUnit(int value, string displayName) 
            : base(value, displayName) {}
    }
}