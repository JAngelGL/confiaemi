Aquí tienes un ejemplo de una función en C# que crea un prisma rectangular en un punto específico (pasado como parámetro) utilizando la API de NXOpen. La función acepta un `Point3d` y crea un prisma rectangular con dimensiones fijas.

```csharp
using NXOpen;
using NXOpen.UF;
using NXOpen.Features;
using NXOpen.Sketch;
using NXOpen.GeometricUtilities;

public class CreateRectangularPrism
{
    public static void Main(string[] args)
    {
        Session theSession = Session.GetSession();
        Part workPart = theSession.Parts.Work;

        // Define the base corner point of the prism
        Point3d baseCornerPoint = new Point3d(10.0, 20.0, 0.0);

        // Call the function to create the prism
        CreatePrismAtPoint(workPart, baseCornerPoint);
    }

    public static void CreatePrismAtPoint(Part workPart, Point3d baseCornerPoint)
    {
        // Define the dimensions of the prism
        double length = 20.0;
        double width = 10.0;
        double height = 30.0;

        // Create the sketch in the work part
        SketchInPlaceBuilder2 sketchBuilder = workPart.Sketches.CreateSketchInPlaceBuilder2(null);
        sketchBuilder.Plane = workPart.Planes.CreatePlane(new Point3d(0.0, 0.0, 0.0), new Vector3d(0.0, 0.0, 1.0), SmartObject.UpdateOption.WithinModeling);
        Sketch sketch = sketchBuilder.Commit() as Sketch;
        sketchBuilder.Destroy();

        // Start the sketch
        workPart.Sketches.SetActive(sketch);
        workPart.Sketches.StartSketch(sketch);

        // Define the corners of the rectangle
        Point3d point1 = baseCornerPoint;
        Point3d point2 = new Point3d(baseCornerPoint.X + length, baseCornerPoint.Y, baseCornerPoint.Z);
        Point3d point3 = new Point3d(baseCornerPoint.X + length, baseCornerPoint.Y + width, baseCornerPoint.Z);
        Point3d point4 = new Point3d(baseCornerPoint.X, baseCornerPoint.Y + width, baseCornerPoint.Z);

        // Create the lines of the rectangle
        workPart.Curves.CreateLine(point1, point2);
        workPart.Curves.CreateLine(point2, point3);
        workPart.Curves.CreateLine(point3, point4);
        workPart.Curves.CreateLine(point4, point1);

        // End the sketch
        workPart.Sketches.EndSketch();
        workPart.Sketches.Deactivate(sketch);

        // Create an extrusion feature
        Features.ExtrudeBuilder extrudeBuilder = workPart.Features.CreateExtrudeBuilder(null);
        extrudeBuilder.Section = workPart.Sections.CreateSection(0.001, 0.001, 0.5);
        extrudeBuilder.Section.AddAllObjects(new NXObject[] { sketch });
        extrudeBuilder.Limits.StartExtend.Value = 0.0;
        extrudeBuilder.Limits.EndExtend.Value = height;
        extrudeBuilder.BooleanOperation.Type = GeometricUtilities.BooleanOperation.BooleanType.Create;

        // Commit the feature
        Feature extrudeFeature = extrudeBuilder.CommitFeature();
        extrudeBuilder.Destroy();

        // Update the display
        Session.GetSession().Parts.Display.Refresh();
    }
}
```

En este ejemplo, la función `CreatePrismAtPoint` toma un `Part` y un `Point3d` como parámetros y crea un prisma rectangular en el punto especificado con dimensiones fijas. Esta función puede ser llamada desde cualquier parte de tu programa donde tengas acceso a un objeto `Part` y un `Point3d`.

Para usar esta función, simplemente llama a `CreatePrismAtPoint` pasando la pieza de trabajo y el punto donde deseas crear el prisma.
