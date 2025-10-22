using System.ServiceModel;

namespace ec.edu.espe.monster.modelo
{
    [ServiceContract]
    public interface IConversionService
    {
        // Longitud
        [OperationContract] double CentimetersToFeet(double centimeters);
        [OperationContract] double FeetToCentimeters(double feet);
        [OperationContract] double MetersToYards(double meters);
        [OperationContract] double YardsToMeters(double yards);
        [OperationContract] double InchesToCentimeters(double inches);
        [OperationContract] double CentimetersToInches(double centimeters);

        // Masas
        [OperationContract] double KilogramsToPounds(double kilograms);
        [OperationContract] double PoundsToKilograms(double pounds);
        [OperationContract] double GramsToOunces(double grams);
        [OperationContract] double OuncesToGrams(double ounces);

        // Temperaturas
        [OperationContract] double CelsiusToFahrenheit(double celsius);
        [OperationContract] double FahrenheitToCelsius(double fahrenheit);
        [OperationContract] double CelsiusToKelvin(double celsius);
        [OperationContract] double KelvinToCelsius(double kelvin);
        [OperationContract] double FahrenheitToKelvin(double fahrenheit);
        [OperationContract] double KelvinToFahrenheit(double kelvin);
    }
}
