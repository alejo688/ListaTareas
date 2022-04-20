using BD;
using Microsoft.EntityFrameworkCore;

static void ShowMenu()
{
    Console.WriteLine("Bienvenidos al programa de lista de tareas");
    Console.WriteLine("1. Mostrar");
    Console.WriteLine("2. Crear");
    Console.WriteLine("3. Editar");
    Console.WriteLine("4. Eliminar");
    Console.WriteLine("5. Salir");
}

static void GetAllData(DbContextOptionsBuilder<TareasContext> optionsBuilder)
{
    Console.Clear();
    Console.WriteLine("Listado de tareas");

    using (var context = new TareasContext(optionsBuilder.Options))
    {
        List<Tarea> tareas = (from tarea in context.Tareas
                              orderby tarea.FechaInicio
                              select tarea).ToList();

        if (tareas.Count() == 0)
        {
            Console.WriteLine("No se encuentra tareas pendientes");
        }
        else
        {
            foreach (var tarea in tareas)
            {
                Console.WriteLine($"{tarea.Id} | {tarea.Nombre} | {tarea.FechaInicio} | {tarea.FechaFinal}");
            }
        }
    }
}

static void Add(DbContextOptionsBuilder<TareasContext> optionsBuilder)
{
    Console.Clear();
    Console.WriteLine("Agregar nueva tarea");
    Console.WriteLine("Nombre de la tareas");
    string Nombre = Console.ReadLine();
    Console.WriteLine("Fecha de inicio");
    DateTime FechaInicio = DateTime.Parse(Console.ReadLine());

    while (FechaInicio < DateTime.Now)
    {
        Console.WriteLine("Fecha no valida");
        Console.WriteLine("Fecha de inicio");
        FechaInicio = DateTime.Parse(Console.ReadLine());
    }

    Console.WriteLine("Fecha final");
    DateTime fechaFinal = DateTime.Parse(Console.ReadLine());
    while (fechaFinal < FechaInicio)
    {
        Console.WriteLine("Fecha no valida");
        Console.WriteLine("Fecha final");
        fechaFinal = DateTime.Parse(Console.ReadLine());
    }

    string Descripcion = null;
    Console.WriteLine("Desea agregar una descripcion a la tareas?");
    Console.WriteLine("1. Si");
    Console.WriteLine("2. No");
    int OptionDescription = int.Parse(Console.ReadLine());

    if (OptionDescription == 1)
    {
        Console.WriteLine("Descripcion de la tarea");
        Descripcion = Console.ReadLine();
    }

    using (var context = new TareasContext(optionsBuilder.Options))
    {
        Tarea tarea = new Tarea()
        {
            Nombre = Nombre,
            FechaInicio = FechaInicio,
            FechaFinal = fechaFinal,
            Descripcion = Descripcion
        };
        context.Add(tarea);
        context.SaveChanges();
    }
    Console.Clear();
}

static void Edit(DbContextOptionsBuilder<TareasContext> optionsBuilder)
{
    Console.Clear();
    Console.WriteLine("Editar Tareas");
    GetAllData(optionsBuilder);
    Console.WriteLine("Escribe el id de la tarea:");
    int id = int.Parse(Console.ReadLine());

    using (var context = new TareasContext(optionsBuilder.Options))
    {
        Tarea tarea = context.Tareas.Find(id);
        if (tarea == null)
        {
            Console.WriteLine("La tarea no existe");
        }
        else
        {
            Console.WriteLine("Nombre de la tareas");
            string Nombre = Console.ReadLine();
            Console.WriteLine("Fecha de inicio");
            DateTime FechaInicio = DateTime.Parse(Console.ReadLine());

            while (FechaInicio < DateTime.Now)
            {
                Console.WriteLine("Fecha no valida");
                Console.WriteLine("Fecha de inicio");
                FechaInicio = DateTime.Parse(Console.ReadLine());
            }

            Console.WriteLine("Fecha final");
            DateTime fechaFinal = DateTime.Parse(Console.ReadLine());
            while (fechaFinal < FechaInicio)
            {
                Console.WriteLine("Fecha no valida");
                Console.WriteLine("Fecha final");
                fechaFinal = DateTime.Parse(Console.ReadLine());
            }

            string Descripcion = null;

            if (tarea.Descripcion == null)
                Console.WriteLine("Desea agregar una descripcion a la tareas?");
            else
                Console.WriteLine("Desea Editar una descripcion a la tareas?");

            Console.WriteLine("1. Si");
            Console.WriteLine("2. No");
            int OptionDescription = int.Parse(Console.ReadLine());

            if (OptionDescription == 1)
            {
                Console.WriteLine("Descripcion de la tarea");
                Descripcion = Console.ReadLine();
            }

            tarea.Nombre = Nombre;
            tarea.FechaInicio = FechaInicio;
            tarea.FechaFinal = fechaFinal;
            tarea.Descripcion = (Descripcion == null) ? tarea.Descripcion : Descripcion;

            context.Entry(tarea).State = EntityState.Modified;
            context.SaveChanges();
        }
    }

    Console.Clear();
}

static void Delete(DbContextOptionsBuilder<TareasContext> optionsBuilder)
{
    Console.Clear();
    Console.WriteLine("Editar Tareas");
    GetAllData(optionsBuilder);
    Console.WriteLine("Escribe el id de la tarea:");
    int id = int.Parse(Console.ReadLine());

    using (var context = new TareasContext(optionsBuilder.Options))
    {
        Tarea tarea = context.Tareas.Find(id);
        if (tarea == null)
        {
            Console.WriteLine("La tarea no existe");
        }
        else
        {
            context.Tareas.Remove(tarea);
            context.SaveChanges();
        }
    }

    Console.Clear();
}

DbContextOptionsBuilder<TareasContext> optionsBuilder = 
    new DbContextOptionsBuilder<TareasContext>();

bool Again = false; 
int Option = 0;

do
{
    ShowMenu();
    Console.WriteLine("Selecciona una opción: ");
    
    if (int.TryParse(Console.ReadLine(), out Option))
    {
        switch (Option)
        {
            case 1:
                GetAllData(optionsBuilder);
                break;
            case 2:
                Add(optionsBuilder);
                break;
            case 3:
                Edit(optionsBuilder);
                break;
            case 4:
                Delete(optionsBuilder);
                break;
            case 5:
                Again = true;
                break;
            default:
                Console.Clear();
                break;
        }
    }
    else
    {
        Console.Clear();
    }
}
while (!Again);

