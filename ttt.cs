using NXOpen;
using NXOpen.Utilities;
using NXOpen.UF;

public void CalculateCentroidOfFaces()
{
    theSession = Session.GetSession();
    UFSession theUFSession = UFSession.GetUFSession();
    Part workPart = theSession.Parts.Work;

    FaceCollector faceCollector = workPart.Faces.CreateFaceCollector();
    // Asumiendo que ya configuraste los criterios para faceCollector

    Point3d centroid;
    Point3d totalCentroid = new Point3d();
    int count = 0;

    foreach (Face face in faceCollector.GetFaces())
    {
        theUFSession.Modl.AskFaceData(face.Tag, out UFModl.FaceData faceData);
        centroid = faceData.cog;
        totalCentroid.X += centroid.X;
        totalCentroid.Y += centroid.Y;
        totalCentroid.Z += centroid.Z;
        count++;
    }

    if (count > 0)
    {
        totalCentroid.X /= count;
        totalCentroid.Y /= count;
        totalCentroid.Z /= count;
    }

    // Ahora totalCentroid contiene el centroide promedio de todas las caras
    Console.WriteLine("Centroide promedio: X={0}, Y={1}, Z={2}", totalCentroid.X, totalCentroid.Y, totalCentroid.Z);
}
