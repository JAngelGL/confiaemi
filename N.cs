using NXOpen;
using NXOpen.UF;
using System;

public class Example
{
    public static void Main()
    {
        // Inicializar la sesión
        Session theSession = Session.GetSession();
        UFSession theUFSession = UFSession.GetUFSession();

        // Obtener la pieza activa
        Part workPart = theSession.Parts.Work;
        
        if (workPart != null)
        {
            // Obtener todos los sistemas de coordenadas en la pieza
            CoordinateSystemCollection coordSystems = workPart.CoordinateSystems;
            foreach (CoordinateSystem csys in coordSystems)
            {
                // Obtener la información de cada sistema de coordenadas
                Point3d origin = csys.Origin;
                NXMatrix nxMatrix = csys.GetOrientationMatrix();
                double[] matrixArray = new double[9];
                nxMatrix.GetValues(matrixArray);

                // Convertir la matriz de orientación a Matrix3x3
                Matrix3x3 orientation = new Matrix3x3();
                orientation.Xx = matrixArray[0];
                orientation.Xy = matrixArray[1];
                orientation.Xz = matrixArray[2];
                orientation.Yx = matrixArray[3];
                orientation.Yy = matrixArray[4];
                orientation.Yz = matrixArray[5];
                orientation.Zx = matrixArray[6];
                orientation.Zy = matrixArray[7];
                orientation.Zz = matrixArray[8];
                
                Console.WriteLine($"Sistema de Coordenadas: {csys.Name}");
                Console.WriteLine($"Origen: ({origin.X}, {origin.Y}, {origin.Z})");
                Console.WriteLine("Orientación:");
                Console.WriteLine($"X: ({orientation.Xx}, {orientation.Xy}, {orientation.Xz})");
                Console.WriteLine($"Y: ({orientation.Yx}, {orientation.Yy}, {orientation.Yz})");
                Console.WriteLine($"Z: ({orientation.Zx}, {orientation.Zy}, {orientation.Zz})");
            }
        }
        else
        {
            Console.WriteLine("No hay una pieza activa.");
        }
    }
}
