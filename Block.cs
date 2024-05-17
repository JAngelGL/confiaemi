using NXOpen;
using NXOpen.UF;

class Program
{
    static void Main()
    {
        Session theSession = Session.GetSession();
        UFSession theUFSession = UFSession.GetUFSession();

        Part workPart = theSession.Parts.Work;

        // Crear un conjunto de datos paramétricos
        ExpressionManager expressionManager = workPart.Expressions;
        Expression expressionWidth = expressionManager.CreateSystemExpressionWithUnits("Width", "mm");
        Expression expressionHeight = expressionManager.CreateSystemExpressionWithUnits("Height", "mm");
        Expression expressionDepth = expressionManager.CreateSystemExpressionWithUnits("Depth", "mm");

        // Crear un cuerpo base (cubo)
        BlockStyler.Cube cube = workPart.BlockStyler.CreateBlock(0, 0, 0, 1, 1, 1);

        // Aplicar operaciones paramétricas
        NXObject[] targets = new NXObject[] { cube };
        expressionWidth.Evaluate("10");
        expressionHeight.Evaluate("10");
        expressionDepth.Evaluate("10");
        workPart.UpdateManager.AddSimpleDimension(targets, expressionWidth, UpdateConstant.DimensionType.DimGlobal);
        workPart.UpdateManager.AddSimpleDimension(targets, expressionHeight, UpdateConstant.DimensionType.DimGlobal);
        workPart.UpdateManager.AddSimpleDimension(targets, expressionDepth, UpdateConstant.DimensionType.DimGlobal);
        workPart.UpdateManager.DoUpdate(new Update());
    }
}
