using NXOpen;
using NXOpen.Features;
using NXOpen.UF;

public class Program
{
    public static void Main()
    {
        Session theSession = Session.GetSession();
        Part workPart = theSession.Parts.Work;
        
        // Open the listing window and print "Hello, World!"
        theSession.ListingWindow.Open();
        theSession.ListingWindow.WriteLine("Hello, World!");

        // Get user to select a point where the block will be created
        Point3d origin = GetUserPoint();
        
        // Create a block feature builder
        BlockFeatureBuilder blockBuilder = workPart.Features.CreateBlockFeatureBuilder(null);
        
        // Set origin and dimensions of the block
        blockBuilder.SetOriginAndLengths(origin, "3.0", "3.0", "3.0");
        
        // Commit the block feature
        blockBuilder.Commit();
        
        // Clean up
        blockBuilder.Destroy();
        
        // Optionally, fit the view to display new geometry
        workPart.ModelingViews.WorkView.Fit();
    }
    
    private static Point3d GetUserPoint()
    {
        Session theSession = Session.GetSession();
        UI theUI = UI.GetUI();
        Point3d point = new Point3d();
        
        // Prompt the user to pick a point
        NXOpen.Selection.SelectionScope scope = NXOpen.Selection.SelectionScope.AnyInAssembly;
        NXOpen.Selection.SelectionAction action = NXOpen.Selection.SelectionAction.ClearAndEnableSpecific;
        NXOpen.Selection.MaskTriple[] mask = { new NXOpen.Selection.MaskTriple(NXOpen.UF.UFConstants.UF_datum_point_type, 0, 0) };

        Selection.Response response = theUI.SelectionManager.SelectPoint("Select a point for the block origin",
                                                                         "Select point", scope, action, false,
                                                                         false, mask, out point, out NXOpen.View view);
        if (response == Selection.Response.Ok)
        {
            return point;
        }
        else
        {
            // Fallback or default point if selection is cancelled
            return new Point3d(0, 0, 0);
        }
    }

    public static int GetUnloadOption(string arg)
    {
        // Specify how the .dll should be unloaded after execution
        return (int)Session.LibraryUnloadOption.Immediately;
    }
}
