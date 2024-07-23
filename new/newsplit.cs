using System;

class Program
{
    static void Main()
    {
        string cadena = "hola como estas(2)";
        string ultimaParte = ObtenerUltimaParte(cadena);
        Console.WriteLine("La última parte es: " + ultimaParte);
    }

    static string ObtenerUltimaParte(string cadena)
    {
        if (string.IsNullOrWhiteSpace(cadena))
        {
            return string.Empty;
        }
        
        string[] partes = cadena.Split(' ');
        return partes[^1];
    }
}
