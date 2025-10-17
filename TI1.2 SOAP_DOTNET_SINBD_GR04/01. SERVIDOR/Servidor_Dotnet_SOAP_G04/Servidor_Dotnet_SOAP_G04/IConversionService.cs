using System.ServiceModel;

namespace espe.edu.ec.monster.modelo
{
    [ServiceContract]
    public interface IConversionService
    {
        // --- Longitud (del PDF) ---
        [OperationContract] double CentimetersToFeet(double centimeters);   // cm -> ft
        [OperationContract] double FeetToCentimeters(double feet);          // ft -> cm
        [OperationContract] double MetersToYards(double meters);            // m  -> yd
        [OperationContract] double YardsToMeters(double yards);             // yd -> m
        [OperationContract] double InchesToCentimeters(double inches);      // in -> cm
        [OperationContract] double CentimetersToInches(double centimeters); // cm -> in

        // --- Masas (añadido) ---
        [OperationContract] double KilogramsToPounds(double kilograms);     // kg -> lb
        [OperationContract] double PoundsToKilograms(double pounds);        // lb -> kg
        [OperationContract] double GramsToOunces(double grams);             // g  -> oz
        [OperationContract] double OuncesToGrams(double ounces);            // oz -> g

        // --- Temperaturas (añadido) ---
        [OperationContract] double CelsiusToFahrenheit(double celsius);     // °C -> °F
        [OperationContract] double FahrenheitToCelsius(double fahrenheit);  // °F -> °C
        [OperationContract] double CelsiusToKelvin(double celsius);         // °C -> K
        [OperationContract] double KelvinToCelsius(double kelvin);          // K  -> °C
        [OperationContract] double FahrenheitToKelvin(double fahrenheit);   // °F -> K
        [OperationContract] double KelvinToFahrenheit(double kelvin);       // K  -> °F
    }
}
