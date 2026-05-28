using NUnit.Framework;
using GestionReadify;
[TestFixture]
public class LibroAlquiladoTest
{
    [Test]
    public void CalcularPrecio_LibroAlquilado_CalculaPrecioCorrectamente()
    {
        // Arrange
        string nombre = "Libro de Alquiler";
        decimal precioBase = 100m;
        string codigo = "AAAA";
        int diasAlquiler = 5;
        LibroAlquilado libroAlquilado = new LibroAlquilado(nombre, precioBase, codigo, diasAlquiler);

        decimal precioFinal = libroAlquilado.CalcularPrecio();

        Assert.AreEqual(425m, precioFinal);
    }
}