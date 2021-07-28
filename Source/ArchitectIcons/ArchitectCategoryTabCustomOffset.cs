using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RimWorld;
using Verse;

namespace ArchitectIcons
{
    public class ArchitectCategoryTabCustomOffset : ArchitectCategoryTab
    {
        private readonly ReflectionUtils.FieldAccess<ArchitectCategoryTab, Func<Gizmo, bool>> Parent_shouldHighLightGizmoFunc;
        private readonly ReflectionUtils.FieldAccess<ArchitectCategoryTab, Func<Gizmo, bool>> Parent_shouldLowLightGizmoFunc;

        public ArchitectCategoryTabCustomOffset(DesignationCategoryDef def, QuickSearchFilter quickSearchFilter) : base(def, quickSearchFilter)
        {
            Parent_shouldHighLightGizmoFunc = new ReflectionUtils.FieldAccess<ArchitectCategoryTab, Func<Gizmo, bool>>(this, "shouldHighLightGizmoFunc");
            Parent_shouldLowLightGizmoFunc = new ReflectionUtils.FieldAccess<ArchitectCategoryTab, Func<Gizmo, bool>>(this, "shouldLowLightGizmoFunc");

        }

        public void DesignationTabOnGUI(Designator forceActivatedCommand)
        {
            if (Find.DesignatorManager.SelectedDesignator != null)
                Find.DesignatorManager.SelectedDesignator.DoExtraGuiControls(0.0f, (float)((double)(UI.screenHeight - 35) - (double)((RimWorld.MainTabWindow_Architect)MainButtonDefOf.Architect.TabWindow).WinHeight - 270.0));
            Func<Gizmo, bool> customActivatorFunc = forceActivatedCommand == null ? (Func<Gizmo, bool>)null : (Func<Gizmo, bool>)(cmd => cmd == forceActivatedCommand);
            Gizmo mouseoverGizmo;
            GizmoGridDrawer.DrawGizmoGrid((IEnumerable<Gizmo>)this.def.ResolvedAllowedDesignators, 210f + 2 * 16f, out mouseoverGizmo, customActivatorFunc, this.Parent_shouldHighLightGizmoFunc.Value, this.Parent_shouldLowLightGizmoFunc.Value);
            if (mouseoverGizmo == null && Find.DesignatorManager.SelectedDesignator != null)
                mouseoverGizmo = (Gizmo)Find.DesignatorManager.SelectedDesignator;
            this.DoInfoBox(ArchitectCategoryTab.InfoRect, (Designator)mouseoverGizmo);
        }

    }
}