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
    Line line1 = workPart.Curves.CreateLine(point1, point2);
    Line line2 = workPart.Curves.CreateLine(point2, point3);
    Line line3 = workPart.Curves.CreateLine(point3, point4);
    Line line4 = workPart.Curves.CreateLine(point4, point1);

    // End the sketch
    workPart.Sketches.Deactivate(sketch);

    // Create an extrusion feature
    NXOpen.Features.ExtrudeBuilder extrudeBuilder = workPart.Features.CreateExtrudeBuilder(null);
    extrudeBuilder.Section = workPart.Sections.CreateSection(0.01, 0.01, 0.5);
    extrudeBuilder.Section.AddCurve(line1, null, null);
    extrudeBuilder.Section.AddCurve(line2, null, null);
    extrudeBuilder.Section.AddCurve(line3, null, null);
    extrudeBuilder.Section.AddCurve(line4, null, null);
    extrudeBuilder.Direction = workPart.Directions.CreateDirection(new Point3d(0.0, 0.0, 0.0), new Vector3d(0.0, 0.0, 1.0), SmartObject.UpdateOption.WithinModeling);
    extrudeBuilder.Limits.StartExtend.Value = 0.0;
    extrudeBuilder.Limits.EndExtend.Value = height;
    extrudeBuilder.BooleanOperation.Type = NXOpen.GeometricUtilities.BooleanOperation.BooleanType.Create;

    // Commit the extrusion feature
    NXOpen.Features.Feature extrudeFeature = extrudeBuilder.CommitFeature();
    extrudeBuilder.Destroy();
}
