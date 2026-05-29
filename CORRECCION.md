# Corrección — FrancoRey

> **Aviso importante:** Las soluciones se evalúan exclusivamente con los conceptos vistos en clase. Si se utilizan conceptos que no fueron parte del programa (frameworks externos, técnicas avanzadas no vistas, etc.), esas partes no serán tenidas en cuenta en la corrección.

## Nota general
**No aprobado** — Puntaje: 46/100

| Área | Obtenido | Máximo |
|---|---|---|
| Jerarquía de herencia | 13 | 25 |
| Clase Orden | 7 | 15 |
| BibliotecaService | 7 | 25 |
| Validaciones y excepciones | 10 | 15 |
| Tests unitarios | 5 | 15 |
| Estructura de proyecto | 4 | 5 |
| **Total** | **46** | **100** |

## Correcciones de evaluación

### 1. Jerarquía de herencia — 13/25

- `Libro` es abstracta correctamente. Tiene `Nombre`, `PrecioBase` y `Codigo` (equivalentes a `Titulo`, `PrecioBase` e `Isbn`). Constructor con validaciones. (3/5 — diferencias de nombres)
- El método abstracto se llama `public abstract decimal CalcularPrecio()` en lugar de `CalcularPrecioFinal()`. El cambio de nombre rompe el contrato esperado del sistema. (3/5)
- `LibroComprado` hereda de `Libro`. En lugar de `Formato` (string) tiene `public decimal Peso {get; set;}` — la propiedad `Formato` está **ausente** y fue reemplazada por otra propiedad diferente. Override de `CalcularPrecio()`: `PrecioBase * 1.1m`. Correcto en fórmula. (4/7)
- `LibroAlquilado` hereda de `Libro`, tiene `DiasAlquiler` (int). Override con **fórmula incorrecta**: `PrecioBase * DiasAlquiler / 0.85m`. Dividir por 0.85 es lo **opuesto** a multiplicar por 0.85. Para precio=100, días=5: 100×5÷0.85 ≈ 588.24 en lugar del correcto 425. Error grave. (3/8)

### 2. Clase Orden — 7/15

- El constructor recibe un `id` como parámetro, pero internamente lo sobreescribe: `Id = Guid.NewGuid().GetHashCode()`. El Id se auto-genera internamente (aunque con `GetHashCode()` sobre un Guid en lugar de el Guid en sí), pero la firma del constructor exige un `id` externo que luego se ignora. Diseño confuso. (2/3)
- `Item` de tipo `Libro` (polimorfismo correcto). (4/4)
- `TotalaPagar` (nótese la 'a' minúscula) es propiedad que delega: `public decimal TotalaPagar => Item.CalcularPrecio()`. Correcto en delegación, aunque el nombre difiere del esperado `TotalAPagar`. (4/5)
- Constructor con diseño inconsistente (recibe e ignora el parámetro `id`). (−2)

### 3. BibliotecaService — 7/25

- Lista `private List<Orden> ordenes` correctamente encapsulada. (5/5)
- `AgregarOrden(Orden orden)` agrega correctamente. (5/5)
- `BuscarOrdenesPorUsuario`: **no implementado** en `BibliotecaService`. (0/8)
- `ObtenerTotalRecaudado`: **no implementado** en `BibliotecaService`. (0/7)

Solo se implementaron `AgregarOrden` y la lista privada.

### 4. Validaciones y excepciones — 10/15

- ISBN: `string.IsNullOrEmpty(codigo)` — valida nulo y vacío pero **no whitespace** (un string de solo espacios pasaría). (3/5)
- `PrecioBase <= 0` → `ArgumentException`. Correcto. (5/5)
- `DiasAlquiler <= 0` → `ArgumentException`. Correcto. (5/5)

### 5. Tests unitarios — 5/15

- `LibroAlquiladoTest` — `PrecioAlquiler_ConDescuento`: crea `LibroAlquilado` con precio=100 y días=5, espera `CalcularPrecio() == 425m`. Con `[Test]`. La estructura del test es correcta, **pero la implementación de `CalcularPrecio()` devolvería ≈588.24 en lugar de 425** (fórmula con división por 0.85 en lugar de multiplicación). El test fallará en ejecución. Se otorga crédito parcial por la estructura correcta del test. (3/5)
- `UnitTest1.cs` con `Assert.Pass()`: no cuenta.
- Tests de búsqueda y total recaudado: ausentes. (0/5 + 0/5)

### 6. Estructura de proyecto — 4/5

- Archivo `GestionReadify.sln` presente: correcto. (2/2)
- Proyecto classlib `GestionReadify` (todos los archivos de clases están en `Catalogo.cs` — un solo archivo con múltiples clases: `Libro`, `LibroComprado`, `LibroAlquilado`, `Orden`). Presente pero con organización deficiente. (2/2)
- Proyecto NUnit `GestionReadifyTest` con referencia: presente. (0/1 — el proyecto de tests contiene solo un test parcialmente funcional y un placeholder)

## Observaciones importantes

- **Error crítico de fórmula**: `LibroAlquilado` usa `PrecioBase * DiasAlquiler / 0.85m` en lugar de `(PrecioBase * DiasAlquiler) * 0.85m`. Dividir por 0.85 da el inverso del descuento. Para precio=100 y días=5: debería dar 425, pero da ≈588.24.
- `BibliotecaService` está muy incompleto: solo tiene la lista y `AgregarOrden`. Faltan los métodos de búsqueda y total recaudado, que representan 15 de los 25 puntos del service.
- El método abstracto se llama `CalcularPrecio` en lugar de `CalcularPrecioFinal`.
- `LibroComprado` tiene `Peso` en lugar de `Formato`.
- La validación de ISBN usa `IsNullOrEmpty` en lugar de `IsNullOrWhiteSpace`.
