using NXOpen;
using NXOpen.UF;

public void GetFaceCentroid(Session theSession, Part workPart, Face face_select0)
{
    // Acceso al sistema de funciones universales
    UFSession theUfSession = UFSession.GetUFSession();

    double[] centroid = new double[3];
    theUfSession.Modl.AskFaceData(face_select0.Tag, out Tag faceTag, centroid, out double[] box, out int[] faceId, out int[] normDir);

    // Imprimir el centroide
    Point3d centroidPoint = new Point3d(centroid[0], centroid[1], centroid[2]);
    Console.WriteLine("Centroide: X=" + centroidPoint.X + ", Y=" + centroidPoint.Y + ", Z=" + centroidPoint.Z);
}


using NXOpen;

public void GetFaceVertices(Session theSession, Part workPart, Face face_select0)
{
    Edge[] edges = face_select0.GetEdges();
    foreach (Edge edge in edges)
    {
        Point3d startPoint = edge.StartPoint;
        Point3d endPoint = edge.EndPoint;

        Console.WriteLine("Start Point: X=" + startPoint.X + ", Y=" + startPoint.Y + ", Z=" + startPoint.Z);
        Console.WriteLine("End Point: X=" + endPoint.X + ", Y=" + endPoint.Y + ", Z=" + endPoint.Z);
    }
}

