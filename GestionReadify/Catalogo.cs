namespace GestionReadify
{

    public abstract class Libro
    {
        public string Nombre{get;set;}
        public decimal PrecioBase{get;set;}
        public string Codigo{get;set;}


        public Libro(string nombre, decimal precioBase, string codigo)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("El nombre no puede ser nulo o vacío.");
            }
            if (precioBase <= 0)
            {
                throw new ArgumentException("El precio base no puede ser menor o igual a cero.");
            }
            if (string.IsNullOrEmpty(codigo))
            {
                throw new ArgumentException("El código no puede ser nulo o vacío.");
            }
            Nombre = nombre;
            PrecioBase = precioBase;
            Codigo = codigo;}

            public abstract decimal CalcularPrecio();
        }

    public class LibroComprado : Libro
    {
        public decimal Peso { get; set; }

        public LibroComprado(string nombre, decimal precioBase, string codigo, decimal peso) : base(nombre, precioBase, codigo)
        {
            if (peso <= 0)
            {
                throw new ArgumentException("El peso no puede ser menor o igual a cero.");
            }
            Peso = peso;
        }
        public override decimal CalcularPrecio()
        {
            return (PrecioBase * 1.1m);
        }  

      
    }

    public class LibroAlquilado : Libro
    {
        public int DiasAlquiler { get; set; }

        public LibroAlquilado(string nombre, decimal precioBase, string codigo, int diasAlquiler) : base(nombre, precioBase, codigo)
        {
            if (diasAlquiler <= 0)
            {
                throw new ArgumentException("Los días de alquiler no pueden ser menores o iguales a cero.");
            }
            DiasAlquiler = diasAlquiler;
        }

        public override decimal CalcularPrecio()
        {
            return (PrecioBase * DiasAlquiler / 0.85m);//
        }
    } 

    public class Orden
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public Libro Item { get; set; }

        public Orden(int id, string nombreUsuario, Libro item)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID no puede ser menor o igual a cero.");
            }
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                throw new ArgumentException("El nombre de usuario no puede ser nulo o vacío.");
            }
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "El libro no puede ser nulo.");
            }
            Id = Guid.NewGuid().GetHashCode(); // Genera un ID único basado en un GUID
            NombreUsuario = nombreUsuario;
            Item = item;

        }
        // Reutilizo CalcularPrecio() para obtener el total a pagar 
        public decimal TotalaPagar=> Item.CalcularPrecio();
    }  
}

