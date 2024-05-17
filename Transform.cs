using System;
using NXOpen;
using NXOpen.Features;
using NXOpen.UF;

public class MoveFeatureBlock
{
    public static void Main(string[] args)
    {
        Session theSession = Session.GetSession();
        Part workPart = theSession.Parts.Work;

        // Suponiendo que ya tienes un FeatureBlock creado
        FeatureBlock featureBlock = (FeatureBlock)workPart.Features.FindObject("Block1");

        // Nueva posición del bloque (vector de traslación)
        Vector3d translationVector = new Vector3d(50.0, 20.0, 30.0); // Ajusta los valores según tus necesidades

        // Obtener la matriz de transformación
        Matrix3x3 identityMatrix = new Matrix3x3();
        identityMatrix.Xx = 1.0; identityMatrix.Xy = 0.0; identityMatrix.Xz = 0.0;
        identityMatrix.Yx = 0.0; identityMatrix.Yy = 1.0; identityMatrix.Yz = 0.0;
        identityMatrix.Zx = 0.0; identityMatrix.Zy = 0.0; identityMatrix.Zz = 1.0;

        // Aplicar la transformación al bloque
        TransformBlock(featureBlock, identityMatrix, translationVector);

        // Guardar los cambios
        workPart.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
    }

    private static void TransformBlock(FeatureBlock block, Matrix3x3 rotationMatrix, Vector3d translationVector)
    {
        NXOpen.UF.UFSession ufSession = NXOpen.UF.UFSession.GetUFSession();
        Tag blockTag = block.Tag;

        // Crear la matriz de transformación 4x4
        double[,] matrix = {
            {rotationMatrix.Xx, rotationMatrix.Xy, rotationMatrix.Xz, translationVector.X},
            {rotationMatrix.Yx, rotationMatrix.Yy, rotationMatrix.Yz, translationVector.Y},
            {rotationMatrix.Zx, rotationMatrix.Zy, rotationMatrix.Zz, translationVector.Z},
            {0, 0, 0, 1}
        };

        // Aplicar la transformación
        ufSession.Modl.TransformObject(blockTag, matrix);
    }
}
