using System;
using NXOpen;
using NXOpen.UIStyler;

public class Program
{
    // Punto de entrada principal para la ejecución del programa NXOpen
    public static int Main(string[] args)
    {
        try
        {
            // Crear una instancia de la sesión de NX
            Session theSession = Session.GetSession();

            // Cargar el archivo DLX, asegurándose de que la ruta es correcta
            string dlxPath = "C:\\path_to_your_dlx_file\\YourDialog.dlx";
            Dialog myDialog = theSession.Styler.LoadStylerDialog(dlxPath);

            // Muestra el diálogo y espera a que el usuario interactúe
            NXOpen.UIStyler.DialogResponse response = myDialog.Show();

            // Aquí puedes manejar la respuesta del diálogo, por ejemplo:
            if (response == NXOpen.UIStyler.DialogResponse.Ok)
            {
                Console.WriteLine("El usuario presionó OK.");
            }
            else if (response == NXOpen.UIStyler.DialogResponse.Cancel)
            {
                Console.WriteLine("El usuario canceló o cerró el diálogo.");
            }

            // Asegúrate de llamar a Dispose para liberar recursos
            myDialog.Dispose();
        }
        catch (Exception ex)
        {
            // Manejo de errores general
            Console.WriteLine("Error: " + ex.Message);
        }

        return 0; // Indica terminación exitosa del programa
    }

    // Destructor para manejar la finalización adecuada
    public static void MainDestructor()
    {
        try
        {
            // Asegúrate de realizar cualquier limpieza aquí, si es necesario
        }
        finally
        {
            NXOpen.Utilities.Routines.SendFinalize();
        }
    }
}
