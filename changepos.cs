using System;
using NXOpen;
using NXOpen.Features;

public class ModifyExtrudePosition
{
    public static void Main(string[] args)
    {
        // Inicializa la sesión de NX
        Session theSession = Session.GetSession();
        Part workPart = theSession.Parts.Work;

        // Llama a la función para modificar la posición del extrude
        string extrudeName = "Extrude(1)";
        NXOpen.Point3d newPosition = new NXOpen.Point3d(100.0, 50.0, 20.0);
        ModifyExtrudePositionByName(workPart, extrudeName, newPosition);

        // Guarda los cambios
        workPart.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
    }

    public static void ModifyExtrudePositionByName(Part workPart, string extrudeName, NXOpen.Point3d newPosition)
    {
        // Encuentra el objeto "Extrude" por su nombre
        Features.Extrude extrudeFeature = null;
        foreach (Feature feature in workPart.Features.ToArray())
        {
            if (feature.Name == extrudeName && feature is Features.Extrude)
            {
                extrudeFeature = (Features.Extrude)feature;
                break;
            }
        }

        if (extrudeFeature == null)
        {
            Console.WriteLine("Extrude feature not found.");
            return;
        }

        // Crea un vector de transformación
        NXOpen.Vector3d translationVector = new NXOpen.Vector3d(newPosition.X, newPosition.Y, newPosition.Z);

        // Aplica la transformación de traslación al "Extrude"
        NXOpen.Matrix3x3 identityMatrix = NXOpen.Matrix3x3.Identity();
        NXOpen.Transform newTransform = workPart.MatrixManager.NewTransform(identityMatrix, translationVector);

        // Asigna la nueva transformación al "Extrude"
        extrudeFeature.Transform(newTransform);

        Console.WriteLine("Extrude feature position modified successfully.");
    }
}
