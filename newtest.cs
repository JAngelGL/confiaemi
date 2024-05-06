using System;
using NXOpen;
using NXOpen.UF;

public class Program
{
    public static void Main()
    {
        try
        {
            Session theSession = Session.GetSession();
            UFSession theUfSession = UFSession.GetUFSession();

            string dlxPath = "C:\\path_to_your_dlx_file\\YourDialog.dlx";
            Tag dialogTag;

            // Cargar el archivo DLX
            theUfSession.Ui.LoadDialog(dlxPath, out dialogTag);

            // Mostrar el diálogo
            int response = 0;
            theUfSession.Ui.DisplayDialog(dialogTag, ref response);

            // Manejar la respuesta del diálogo
            if (response == (int)NXOpen.UF.UFConstants.UF_UI_OK)
            {
                Console.WriteLine("El usuario presionó OK.");
            }
            else if (response == (int)NXOpen.UF.UFConstants.UF_UI_CANCEL)
            {
                Console.WriteLine("El usuario canceló o cerró el diálogo.");
            }

            // Descargar el diálogo para liberar recursos
            theUfSession.Ui.UnloadDialog(dialogTag);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
