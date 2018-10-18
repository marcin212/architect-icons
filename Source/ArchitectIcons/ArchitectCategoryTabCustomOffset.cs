using System.Linq;
using RimWorld;
using Verse;

namespace ArchitectIcons
{
    public class ArchitectCategoryTabCustomOffset : ArchitectCategoryTab
    {
        public ArchitectCategoryTabCustomOffset(DesignationCategoryDef def) : base(def)
        {
        }

        public void DesignationTabOnGUI()
        {
            if (Find.DesignatorManager.SelectedDesignator != null)
                Find.DesignatorManager.SelectedDesignator.DoExtraGuiControls(0.0f,
                    (float) ((double) (UI.screenHeight - 35) -
                             (double) ((RimWorld.MainTabWindow_Architect) MainButtonDefOf.Architect.TabWindow)
                             .WinHeight - 270.0));
            Gizmo mouseoverGizmo;
            GizmoGridDrawer.DrawGizmoGrid(this.def.ResolvedAllowedDesignators.Cast<Gizmo>(), 210f + 2 * 16,
                out mouseoverGizmo);
            if (mouseoverGizmo == null && Find.DesignatorManager.SelectedDesignator != null)
                mouseoverGizmo = (Gizmo) Find.DesignatorManager.SelectedDesignator;
            DoInfoBox(InfoRect, (Designator) mouseoverGizmo);
        }
    }
}