using FluentAssertions;

namespace AjusteTexto;

public class AjusteTextoTest
{
    [Fact]
    public void a()
    {
        var result = Wrap("", 1);

        result.Should().Be("");
    }
    
    [Fact]
    public void b()
    {
        var result = Wrap("this", 10);

        result.Should().Be("this");
    } 
    
    [Fact]
    public void c()
    {
        var result = Wrap("word", 2);

        result.Should().Be("wo\nrd");
    } 
    
    [Fact]
    public void d()
    {
        var result = Wrap("abcdefghij", 3);

        result.Should().Be("abc\ndef\nghi\nj");
    }
    
    [Fact]
    public void e()
    {
        var result = Wrap("word word", 3);

        result.Should().Be("wor\nd\nwor\nd");
    }

    [Fact]
    public void f()
    {
        var result = Wrap("word word", 6);

        result.Should().Be("word\nword");
    }   
    
    [Fact]
    public void f2()
    {
        var result = Wrap("word word", 5);

        result.Should().Be("word\nword");
    }
    
    [Fact]
    public void g()
    {
        var result = Wrap("word word word", 6);

        result.Should().Be("word\nword\nword");
    }
    
    [Fact]
    public void h()
    {
        var result = Wrap("word word word", 11);

        result.Should().Be("word word\nword");
    }
    
    private static string Wrap(string text, int col)
    {
        const string saltoLinea = "\n";
        
        if (TextoEstaVacio(text))
            return "";

        return ContieneEspacios(text) ? DividirTextoConEspacios(text , col, saltoLinea) : DividirPalabra(text,col, saltoLinea);
    }

    private static string DividirTextoConEspacios(string texto, int cantidadMaximaCaracteres, string saltoLinea)
    {
        string textoDivido = string.Empty;

        foreach (var palabraOriginal in texto.Split(' '))
        {
            string palabra = DividirPalabra(palabraOriginal, cantidadMaximaCaracteres, saltoLinea);

            if (TextoEstaVacio(textoDivido))
            {
                textoDivido = palabra;
                continue;
            }

            textoDivido += PalabraMenorOIgualACantidadMaxima(cantidadMaximaCaracteres, palabra, textoDivido,saltoLinea)
                ? " "
                : saltoLinea;

            textoDivido += palabra;
        }

        return textoDivido;
    }

    private static string DividirPalabra(string texto, int cantidadMaximaCaracteres, string saltoLinea)
    {
        var palabras = new List<string>();
        for (int posicionCaracter = 0; posicionCaracter < texto.Length; posicionCaracter += cantidadMaximaCaracteres)
        {
            int cantidadCaracteresRestantes = texto.Length - posicionCaracter;
            int cantidadCaracteresDividir = Math.Min(cantidadMaximaCaracteres, cantidadCaracteresRestantes);
            
            palabras.Add(texto.Substring(posicionCaracter, cantidadCaracteresDividir));
        }
        
        return string.Join(saltoLinea, palabras);
    }
   
    private static bool ContieneEspacios(string text) => text.Contains(' ');
    private static bool TextoEstaVacio(string textoDivido) => string.IsNullOrEmpty(textoDivido);
    private static bool ContieneSaltoDeLinea(string textoDivido, string saltoLinea) => textoDivido.Contains(saltoLinea);
    private static string ObtenerUltimaPalabraPorSaltoDeLinea(string textoDivido, string saltoLinea) => textoDivido.Substring(textoDivido.LastIndexOf(saltoLinea) + 1);
    private static string ObtenerUltimaPalabra(string textoDivido, string saltoLinea) =>
        ContieneSaltoDeLinea(textoDivido, saltoLinea)
            ? ObtenerUltimaPalabraPorSaltoDeLinea(textoDivido, saltoLinea)
            : textoDivido;
    private static bool PalabraMenorOIgualACantidadMaxima(int cantidadMaximaCaracteres, string palabra, string textoDivido, string saltoLinea)
    {
        string ultimaPalabra = ObtenerUltimaPalabra(textoDivido, saltoLinea);
        
        return ultimaPalabra.Length + 1 + palabra.Length <= cantidadMaximaCaracteres;
    }
}