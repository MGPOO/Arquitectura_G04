using espe.edu.ec.monster.modelo;

namespace espe.edu.ec.monster.controlador
{
    public class ConversionService : IConversionService
    {
        // ---------- Longitud ----------
        public double CentimetersToFeet(double centimeters) => centimeters * 0.0328084;
        public double FeetToCentimeters(double feet) => feet / 0.0328084;
        public double MetersToYards(double meters) => meters * 1.09361;
        public double YardsToMeters(double yards) => yards / 1.09361;
        public double InchesToCentimeters(double inches) => inches * 2.54;
        public double CentimetersToInches(double centimeters) => centimeters / 2.54;

        // ---------- Masas ----------
        public double KilogramsToPounds(double kilograms) => kilograms * 2.2046226218;
        public double PoundsToKilograms(double pounds) => pounds / 2.2046226218;
        public double GramsToOunces(double grams) => grams / 28.349523125;
        public double OuncesToGrams(double ounces) => ounces * 28.349523125;

        // ---------- Temperaturas ----------
        public double CelsiusToFahrenheit(double celsius) => (celsius * 9.0 / 5.0) + 32.0;
        public double FahrenheitToCelsius(double fahrenheit) => (fahrenheit - 32.0) * 5.0 / 9.0;
        public double CelsiusToKelvin(double celsius) => celsius + 273.15;
        public double KelvinToCelsius(double kelvin) => kelvin - 273.15;
        public double FahrenheitToKelvin(double fahrenheit) => CelsiusToKelvin(FahrenheitToCelsius(fahrenheit));
        public double KelvinToFahrenheit(double kelvin) => CelsiusToFahrenheit(KelvinToCelsius(kelvin));
    }
}
