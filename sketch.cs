  public static void CreateSketchWithRectangle(Session session, Part workPart, Face selectedFace)
    {
        // Crear el punto de referencia para el sketch
        Point3d origin = new Point3d(0.0, 0.0, 0.0);
        Direction zAxis = workPart.Directions.CreateDirection(selectedFace, SmartObject.UpdateOption.WithinModeling);
        Axis originAxis = workPart.Axes.CreateAxis(origin, zAxis);
        
        // Crear el plano de trabajo sobre la cara seleccionada
        Plane workPlane = workPart.Planes.CreatePlane(selectedFace, SmartObject.UpdateOption.WithinModeling);
        
        // Crear el sketch en el plano de trabajo
        SketchInPlaceBuilder sketchBuilder = workPart.Sketches.CreateSketchInPlaceBuilder(null);
        sketchBuilder.PlaneReference = workPlane;
        sketchBuilder.OrientationMethod = SketchInPlaceBuilder.OrientationTypes.OrientToPlane;
        
        Sketch sketch = (Sketch)sketchBuilder.Commit();
        sketchBuilder.Destroy();
        
        // Dimensiones del rectángulo
        double width = 2.0;
        double height = 2.0;
        
        // Crear los puntos del rectángulo
        Point3d p1 = new Point3d(0.0, 0.0, 0.0);
        Point3d p2 = new Point3d(width, 0.0, 0.0);
        Point3d p3 = new Point3d(width, height, 0.0);
        Point3d p4 = new Point3d(0.0, height, 0.0);
        
        // Crear las líneas del rectángulo
        Line line1 = workPart.Curves.CreateLine(p1, p2);
        Line line2 = workPart.Curves.CreateLine(p2, p3);
        Line line3 = workPart.Curves.CreateLine(p3, p4);
        Line line4 = workPart.Curves.CreateLine(p4, p1);
        
        // Agregar las líneas al sketch
        sketch.AddGeometry(line1);
        sketch.AddGeometry(line2);
        sketch.AddGeometry(line3);
        sketch.AddGeometry(line4);
        
        // Finalizar el sketch
        sketch.Update();
    }
