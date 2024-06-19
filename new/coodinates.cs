        Point3d point1 = baseCornerPoint;
        Point3d point2 = new Point3d(baseCornerPoint.X + length, baseCornerPoint.Y, baseCornerPoint.Z);
        Point3d point3 = new Point3d(baseCornerPoint.X + length, baseCornerPoint.Y + width, baseCornerPoint.Z);
        Point3d point4 = new Point3d(baseCornerPoint.X, baseCornerPoint.Y + width, baseCornerPoint.Z);

        Point3d point5 = new Point3d(baseCornerPoint.X, baseCornerPoint.Y, baseCornerPoint.Z + height);
        Point3d point6 = new Point3d(baseCornerPoint.X + length, baseCornerPoint.Y, baseCornerPoint.Z + height);
        Point3d point7 = new Point3d(baseCornerPoint.X + length, baseCornerPoint.Y + width, baseCornerPoint.Z + height);
        Point3d point8 = new Point3d(baseCornerPoint.X, baseCornerPoint.Y + width, baseCornerPoint.Z + height);
