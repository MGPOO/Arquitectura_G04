namespace ConUni_Soap_Dotnet_CliMov_G04.Core
{
    internal class ConversionEngine
    {
        public double Convert(Models.ConversionRequest req)
        {
            return req.Category switch
            {
                "Longitud" => ConvertLength(req.From, req.To, req.Value),
                "Masa" => ConvertMass(req.From, req.To, req.Value),
                "Temperatura" => ConvertTemperature(req.From, req.To, req.Value),
                _ => 0
            };
        }

        private double ConvertLength(string from, string to, double v)
        {
            if (from == to) return v;
            if (from == "Centímetros" && to == "Pies") return v * 0.0328084;
            if (from == "Pies" && to == "Centímetros") return v / 0.0328084;
            if (from == "Metros" && to == "Yardas") return v * 1.09361;
            if (from == "Yardas" && to == "Metros") return v / 1.09361;
            if (from == "Pulgadas" && to == "Centímetros") return v * 2.54;
            if (from == "Centímetros" && to == "Pulgadas") return v / 2.54;
            return v;
        }

        private double ConvertMass(string from, string to, double v)
        {
            if (from == to) return v;
            if (from == "Kilogramos" && to == "Libras") return v * 2.20462;
            if (from == "Libras" && to == "Kilogramos") return v / 2.20462;
            if (from == "Gramos" && to == "Onzas") return v / 28.3495;
            if (from == "Onzas" && to == "Gramos") return v * 28.3495;
            return v;
        }

        private double ConvertTemperature(string from, string to, double v)
        {
            if (from == to) return v;
            if (from == "Celsius" && to == "Fahrenheit") return (v * 9 / 5) + 32;
            if (from == "Fahrenheit" && to == "Celsius") return (v - 32) * 5 / 9;
            if (from == "Celsius" && to == "Kelvin") return v + 273.15;
            if (from == "Kelvin" && to == "Celsius") return v - 273.15;
            if (from == "Fahrenheit" && to == "Kelvin") return (v - 32) * 5 / 9 + 273.15;
            if (from == "Kelvin" && to == "Fahrenheit") return (v - 273.15) * 9 / 5 + 32;
            return v;
        }
    }
}
