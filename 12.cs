using System;
using NXOpen;
using NXOpen.Features;
using NXOpen.GeometricUtilities;
using NXOpen.UF;

public class CreateRectangle
{
    public static void Main(string[] args)
    {
        // Obtén la sesión actual de NX
        Session theSession = Session.GetSession();
        UFSession theUFSession = UFSession.GetUFSession();
        Part workPart = theSession.Parts.Work;

        // Define las coordenadas del punto donde se creará el rectángulo
        double x = 100.0;
        double y = 100.0;
        double z = 0.0;
        Point3d startPoint = new Point3d(x, y, z);

        // Define las dimensiones del rectángulo
        double width = 50.0;
        double height = 30.0;

        // Crear un sketch en el plano XY
        SketchInPlaceBuilder2 sketchInPlaceBuilder = workPart.Sketches.CreateSketchInPlaceBuilder2(null);
        sketchInPlaceBuilder.Origin = startPoint;
        sketchInPlaceBuilder.PlaneOption = Sketch.PlaneOptionType.XyPlane;

        Sketch sketch = (NXOpen.Sketch)sketchInPlaceBuilder.Commit();

        // Crear los cuatro puntos del rectángulo
        Point3d point1 = new Point3d(0.0, 0.0, 0.0);
        Point3d point2 = new Point3d(width, 0.0, 0.0);
        Point3d point3 = new Point3d(width, height, 0.0);
        Point3d point4 = new Point3d(0.0, height, 0.0);

        // Crear líneas que conecten los puntos
        Line line1 = workPart.Curves.CreateLine(point1, point2);
        Line line2 = workPart.Curves.CreateLine(point2, point3);
        Line line3 = workPart.Curves.CreateLine(point3, point4);
        Line line4 = workPart.Curves.CreateLine(point4, point1);

        // Añadir las líneas al sketch
        sketch.AddGeometry(new NXOpen.Curve[] { line1, line2, line3, line4 }, Sketch.ConstraintMode.Auto);

        // Terminar el sketch
        sketchInPlaceBuilder.Destroy();
    }
}
