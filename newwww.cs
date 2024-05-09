using NXOpen;
using NXOpen.UF;

public void CalculateCentroidOfFaces()
{
    Session theSession = Session.GetSession();
    UFSession theUFSession = UFSession.GetUFSession();
    Part workPart = theSession.Parts.Work;

    // Crear un colector de caras (FaceCollector)
    FaceCollector faceCollector = workPart.Faces.CreateFaceCollector();

    // Supongamos que ya se han agregado las condiciones de selección al colector

    UFModl modl = theUFSession.Modl;
    Point3d centroid;
    Point3d totalCentroid = new Point3d();
    int count = 0;

    foreach (Face face in faceCollector.Faces)
    {
        Tag faceTag = face.Tag;
        double[] centroidArray = new double[3];

        // Llama a una función UF para obtener el centroide de la cara
        modl.AskFaceProps(faceTag, out double[] pointOnFace, out double[] normal, out double radius, out double[] box, out double[] cone, centroidArray);

        centroid.X = centroidArray[0];
        centroid.Y = centroidArray[1];
        centroid.Z = centroidArray[2];

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

    // Ahora totalCentroid contiene el centroide promedio de todas las caras seleccionadas
    Console.WriteLine("Centroide promedio: X={0}, Y={1}, Z={2}", totalCentroid.X, totalCentroid.Y, totalCentroid.Z);
}
