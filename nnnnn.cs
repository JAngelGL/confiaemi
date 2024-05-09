using NXOpen;
using NXOpen.UF;

public class FacePositionFinder
{
    public static void Main()
    {
        Session theSession = Session.GetSession();
        Part workPart = theSession.Parts.Work;

        // Asumiendo que tienes una referencia a un FaceCollector llamado faceCollector
        FaceCollector faceCollector; // Asegúrate de inicializar este objeto correctamente

        // Obtener las caras del FaceCollector
        NXObject[] faces = faceCollector.GetEntities();

        foreach (Face face in faces)
        {
            UFSession ufSession = UFSession.GetUFSession();
            
            // Obtener el tag de la cara y calcular el centroide
            Tag faceTag = face.Tag;
            double area, centroidX, centroidY, centroidZ;
            ufSession.Modl.AskFaceArea(faceTag, out area, out centroidX, out centroidY, out centroidZ);

            // Imprimir la posición del centroide de la cara
            System.Console.WriteLine($"Centroide de la cara: ({centroidX}, {centroidY}, {centroidZ})");
        }
    }

    // Método necesario para que NXOpen pueda ejecutar este programa
    public static int GetUnloadOption(string dummy) => (int)Session.LibraryUnloadOption.Immediately;
}
