namespace ClienteConversionConsoleMvc.Models
{
    public class ConversionModel
    {
        // Longitud
        public double CentimetersToFeet(double v) => v / 30.48;
        public double FeetToCentimeters(double v) => v * 30.48;
        public double MetersToYards(double v) => v * 1.09361;
        public double YardsToMeters(double v) => v / 1.09361;
        public double InchesToCentimeters(double v) => v * 2.54;
        public double CentimetersToInches(double v) => v / 2.54;

        // Masa
        public double KilogramsToPounds(double v) => v * 2.20462;
        public double PoundsToKilograms(double v) => v / 2.20462;
        public double GramsToOunces(double v) => v / 28.3495;
        public double OuncesToGrams(double v) => v * 28.3495;

        // Temperatura
        public double CelsiusToFahrenheit(double v) => v * 9 / 5 + 32;
        public double FahrenheitToCelsius(double v) => (v - 32) * 5 / 9;
        public double CelsiusToKelvin(double v) => v + 273.15;
        public double KelvinToCelsius(double v) => v - 273.15;
        public double FahrenheitToKelvin(double v) => (v - 32) * 5 / 9 + 273.15;
        public double KelvinToFahrenheit(double v) => (v - 273.15) * 9 / 5 + 32;
    }
}
