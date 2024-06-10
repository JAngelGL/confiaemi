using NXOpen;
using NXOpen.Features;
using NXOpen.GeometricUtilities;
using NXOpen.Sketch;
using NXOpen.UF;

public class RectangleCreator
{
    public static void CreateRectangle(Session theSession, Part workPart, Point3d basePoint)
    {
        // Crear el sketch
        DatumPlane datumPlane = workPart.Datums.CreateFixedDatumPlane(basePoint, Vector3d.ZAxis, 0.0);
        SketchInPlaceBuilder2 sketchBuilder = workPart.Sketches.CreateSketchInPlaceBuilder2(null);
        sketchBuilder.Plane = datumPlane;
        sketchBuilder.Origin = workPart.Points.CreatePoint(basePoint);
        sketchBuilder.PlaneReferenceType = Sketch.PlaneReferenceType.WorkPartAbsolute;

        Sketch sketch = (Sketch)sketchBuilder.Commit();
        sketchBuilder.Destroy();

        // Crear los puntos del rectángulo
        Point3d point1 = new Point3d(basePoint.X, basePoint.Y, basePoint.Z);
        Point3d point2 = new Point3d(basePoint.X + 2.110, basePoint.Y, basePoint.Z);
        Point3d point3 = new Point3d(basePoint.X + 2.110, basePoint.Y + 0.466, basePoint.Z);
        Point3d point4 = new Point3d(basePoint.X, basePoint.Y + 0.466, basePoint.Z);

        // Crear las líneas del rectángulo
        Line line1 = workPart.Curves.CreateLine(point1, point2);
        Line line2 = workPart.Curves.CreateLine(point2, point3);
        Line line3 = workPart.Curves.CreateLine(point3, point4);
        Line line4 = workPart.Curves.CreateLine(point4, point1);

        // Añadir las líneas al sketch
        sketch.AddGeometry(new Curve[] { line1, line2, line3, line4 }, Sketch.InferConstraintsOption.InferConstraints);

        // Finalizar el sketch
        sketch.Update();
    }

    public static void Main()
    {
        Session theSession = Session.GetSession();
        Part workPart = theSession.Parts.Work;
        Point3d basePoint = new Point3d(0.0, 0.0, 0.0); // Cambia esto según sea necesario

        CreateRectangle(theSession, workPart, basePoint);
    }
}
