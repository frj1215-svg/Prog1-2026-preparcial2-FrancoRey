//namespace GestionReadify;
using System.Collections.Generic;
using System.Linq;

namespace GestionReadify
{

public class BibliotecaService
{
    private List<Orden> ordenes= new List<Orden>();
    
    public void AgregarOrden(Orden orden)
    {
        ordenes.Add(orden);
    }
}
}