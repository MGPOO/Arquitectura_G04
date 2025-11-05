namespace Servidor_Dotnet_REST_GR04.Services;

public class ConversionService
{
    // ======= LONGITUD =======
    public double CentimetersToFeet(double cm) => cm / 30.48;
    public double FeetToCentimeters(double ft) => ft * 30.48;

    public double InchesToCentimeters(double inches) => inches * 2.54;
    public double CentimetersToInches(double cm) => cm / 2.54;

    public double MetersToYards(double m) => m * 1.0936133;
    public double YardsToMeters(double yd) => yd / 1.0936133;

    // ======= MASA =======
    public double KilogramsToPounds(double kg) => kg * 2.2046226218;
    public double PoundsToKilograms(double lb) => lb / 2.2046226218;

    public double GramsToOunces(double g) => g / 28.349523125;
    public double OuncesToGrams(double oz) => oz * 28.349523125;

    // ======= TEMPERATURA =======
    public double CelsiusToFahrenheit(double c) => (c * 9.0 / 5.0) + 32.0;
    public double FahrenheitToCelsius(double f) => (f - 32.0) * 5.0 / 9.0;

    public double CelsiusToKelvin(double c) => c + 273.15;
    public double KelvinToCelsius(double k) => k - 273.15;

    public double FahrenheitToKelvin(double f) => CelsiusToKelvin(FahrenheitToCelsius(f));
    public double KelvinToFahrenheit(double k) => CelsiusToFahrenheit(KelvinToCelsius(k));
}
