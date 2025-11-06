namespace ConUni_Soap_Dotnet_CliMov_G04.Controllers
{
    public static class ConversionController
    {
        // Longitud
        public static double CentimetersToFeet(double centimeters) => centimeters * 0.0328084;
        public static double FeetToCentimeters(double feet) => feet / 0.0328084;
        public static double MetersToYards(double meters) => meters * 1.09361;
        public static double YardsToMeters(double yards) => yards / 1.09361;
        public static double InchesToCentimeters(double inches) => inches * 2.54;
        public static double CentimetersToInches(double centimeters) => centimeters / 2.54;

        // Masa
        public static double KilogramsToPounds(double kilograms) => kilograms * 2.2046226218;
        public static double PoundsToKilograms(double pounds) => pounds / 2.2046226218;
        public static double GramsToOunces(double grams) => grams / 28.349523125;
        public static double OuncesToGrams(double ounces) => ounces * 28.349523125;

        // Temperatura
        public static double CelsiusToFahrenheit(double celsius) => (celsius * 9.0 / 5.0) + 32.0;
        public static double FahrenheitToCelsius(double fahrenheit) => (fahrenheit - 32.0) * 5.0 / 9.0;
        public static double CelsiusToKelvin(double celsius) => celsius + 273.15;
        public static double KelvinToCelsius(double kelvin) => kelvin - 273.15;
        public static double FahrenheitToKelvin(double fahrenheit) => CelsiusToKelvin(FahrenheitToCelsius(fahrenheit));
        public static double KelvinToFahrenheit(double kelvin) => CelsiusToFahrenheit(KelvinToCelsius(kelvin));
    }
}
