using NXOpen;
using NXOpen.Features;
using NXOpen.GeometricUtilities;

public static class NXOpenSession
{
    public static void Main()
    {
        Session theSession = Session.GetSession();
        Part workPart = theSession.Parts.Work;

        // Start an undo mark
        Session.UndoMarkId markId = theSession.SetUndoMark(Session.MarkVisibility.Visible, "Create Parametric Body");

        try
        {
            // Define dimensions
            Expression lengthExp = workPart.Expressions.CreateExpression("Number", "100");
            Expression widthExp = workPart.Expressions.CreateExpression("Number", "50");
            Expression heightExp = workPart.Expressions.CreateExpression("Number", "25");

            // Create block feature
            BlockFeatureBuilder blockBuilder = workPart.Features.CreateBlockFeatureBuilder(null);
            blockBuilder.SetOriginAndLengths(
                Origin: Point3d.Origin,
                Length: lengthExp,
                Width: widthExp,
                Height: heightExp
            );

            // Commit the feature and get the body
            BlockFeature blockFeature = (BlockFeature)blockBuilder.Commit();
            blockBuilder.Destroy();

            // Accessing the body created by the block feature
            Body body = blockFeature.GetBodies()[0];

            // Optionally, rename expressions to make them easily identifiable
            lengthExp.SetName("Length");
            widthExp.SetName("Width");
            heightExp.SetName("Height");

            // Additional manipulation of the body can be done here
        }
        catch (Exception ex)
        {
            // Handle exceptions
            theSession.UndoToMark(markId, "Create Parametric Body Failed");
            throw;  // Re-throw the exception
        }

        // Update the display
        theSession.UpdateManager.DoUpdate(markId);
    }
}
