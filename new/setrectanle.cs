using System;
using NXOpen;
using NXOpen.Features;
using NXOpen.GeometricUtilities;
using NXOpen.UF;

public class CreateRectangle
{
    public static void CreateRectangularSketch(Session session, Part workPart, Point3d origin, Face face, Edge localFaceEdge)
    {
        // Crear un Sketch In Place
        SketchInPlaceBuilder2 sketchBuilder = workPart.Sketches.CreateSketchInPlaceBuilder2(null);
        
        // Establecer la cara de referencia para el Sketch
        sketchBuilder.PlaneOrFace.ReplaceReference(face);

        // Establecer el origen del Sketch
        sketchBuilder.OriginPoint = origin;

        // Establecer una línea de referencia (el borde local de la cara)
        sketchBuilder.XDirection = localFaceEdge;

        // Crear el Sketch
        Sketch sketch = (Sketch)workPart.Sketches.CommitSketchInPlaceBuilder(sketchBuilder);

        // Definir los puntos del rectángulo
        Point3d point1 = new Point3d(origin.X, origin.Y, origin.Z);
        Point3d point2 = new Point3d(origin.X + 0.5, origin.Y, origin.Z);
        Point3d point3 = new Point3d(origin.X + 0.5, origin.Y + 0.2, origin.Z);
        Point3d point4 = new Point3d(origin.X, origin.Y + 0.2, origin.Z);

        // Comenzar a editar el Sketch
        sketch.Activate(Sketch.ViewReorient.True);

        // Crear las líneas del rectángulo en el Sketch
        sketch.CreateLine(point1, point2);
        sketch.CreateLine(point2, point3);
        sketch.CreateLine(point3, point4);
        sketch.CreateLine(point4, point1);

        // Finalizar la edición del Sketch
        sketch.Deactivate(Sketch.ViewReorient.True, Sketch.UpdateLevel.Model);

        // Limpiar recursos
        sketchBuilder.Destroy();
    }

    public static void Main()
    {
        Session theSession = Session.GetSession();
        Part workPart = theSession.Parts.Work;

        // Ejemplo de uso
        Point3d origin = new Point3d(0.0, 0.0, 0.0); // Asegúrate de que este sea el punto correcto
        Face face = /* Tu código para obtener la Face */;
        Edge localFaceEdge = /* Tu código para obtener el Edge */;

        CreateRectangularSketch(theSession, workPart, origin, face, localFaceEdge);
    }
}
