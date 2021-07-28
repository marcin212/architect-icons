using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RimWorld;
using UnityEngine;
using Verse;

namespace ArchitectIcons
{
    public class MainTabWindow_Architect : RimWorld.MainTabWindow_Architect
    {
        private static readonly Color NoMatchColor = Color.grey;
        public const float WinWidth = 200f + 2 * 16;

        private readonly ReflectionUtils.FieldAccess<RimWorld.MainTabWindow_Architect, List<ArchitectCategoryTab>> Parent_desPanelsCached;
        private readonly ReflectionUtils.FieldAccess<RimWorld.MainTabWindow_Architect, Designator> Parent_forceActivatedCommand;
        private readonly ReflectionUtils.FieldAccess<RimWorld.MainTabWindow_Architect, bool> Parent_didInitialUnfocus;
        private readonly ReflectionUtils.FieldAccess<RimWorld.MainTabWindow_Architect, QuickSearchWidget> Parent_quickSearchWidget;

        private readonly ReflectionUtils.MethodAccess<RimWorld.MainTabWindow_Architect, ArchitectCategoryTabCustomOffset> Parent_OpenTab;
        private readonly ReflectionUtils.MethodAccess<RimWorld.MainTabWindow_Architect, ArchitectCategoryTab> Parent_CacheSearchState;

        public override void ExtraOnGUI()
        {
            Parent_OpenTab.Invoke()?.DesignationTabOnGUI(this.Parent_forceActivatedCommand.Value);
            this.Parent_forceActivatedCommand.Value = (Designator) null;
        }

        public override Vector2 RequestedTabSize
        {
            get { return new Vector2(WinWidth, this.WinHeight); }
        }

        public float WinHeight
        {
            get
            {
                if (Parent_desPanelsCached.Value == null)
                    this.CacheDesPanels();
                return (float)((double)Mathf.CeilToInt((float)this.Parent_desPanelsCached.Value.Count / 2f) * 32.0 + 28.0);
            }
        }

        public MainTabWindow_Architect()
        {
            Parent_desPanelsCached = new ReflectionUtils.FieldAccess<RimWorld.MainTabWindow_Architect, List<ArchitectCategoryTab>>(this, "desPanelsCached");
            Parent_forceActivatedCommand = new ReflectionUtils.FieldAccess<RimWorld.MainTabWindow_Architect, Designator>(this, "forceActivatedCommand");
            Parent_didInitialUnfocus = new ReflectionUtils.FieldAccess<RimWorld.MainTabWindow_Architect, bool>(this, "didInitialUnfocus");
            Parent_quickSearchWidget = new ReflectionUtils.FieldAccess<RimWorld.MainTabWindow_Architect, QuickSearchWidget>(this, "quickSearchWidget");

            Parent_OpenTab = new ReflectionUtils.MethodAccess<RimWorld.MainTabWindow_Architect, ArchitectCategoryTabCustomOffset>(this, "OpenTab");
            Parent_CacheSearchState = new ReflectionUtils.MethodAccess<RimWorld.MainTabWindow_Architect, ArchitectCategoryTab>(this, "CacheSearchState");

            this.CacheDesPanels();
        }

        public override void PreOpen()
        {
            CacheDesPanelsWithCheck();
            base.PreOpen();
        }

        private void CacheDesPanelsWithCheck()
        {
            if (this.Parent_desPanelsCached.Value == null)
            {
                CacheDesPanels();
            }
        }

        private void CacheDesPanels()
        {
            Parent_desPanelsCached.Value = new List<ArchitectCategoryTab>();
            foreach (DesignationCategoryDef def in (IEnumerable<DesignationCategoryDef>)
                DefDatabase<DesignationCategoryDef>.AllDefs.OrderByDescending<DesignationCategoryDef, int>(
                    (Func<DesignationCategoryDef, int>) (dc => dc.order)))
                Parent_desPanelsCached.Value.Add(new ArchitectCategoryTabCustomOffset(def, Parent_quickSearchWidget.Value.filter));

            List<string> listTab = new List<string>();
            for (int index = 0; index < Parent_desPanelsCached.Value.Count; ++index)
            {
                listTab.Add(Parent_desPanelsCached.Value[index].def.defName);
            }

            try
            {
                System.IO.File.WriteAllLines(Resources.GetSettingsPath("CategoryTabsName.txt"), listTab.ToArray());
            }
            catch (Exception e) { };
        }


        public override void DoWindowContents(Rect inRect)
        {

            Text.Font = GameFont.Small;
            float width = inRect.width / 2f;
            float num1 = 0.0f;
            float num2 = 0.0f;
            float a = 0.0f;
            ArchitectCategoryTab architectCategoryTab = this.Parent_OpenTab.Invoke();
            if (KeyBindingDefOf.Accept.KeyDownEvent)
            {
                if (this.Parent_quickSearchWidget.Value.filter.Active && architectCategoryTab != null && architectCategoryTab.UniqueSearchMatch != null)
                    this.Parent_forceActivatedCommand.Value = architectCategoryTab.UniqueSearchMatch;
                else
                    this.Close(true);
                Event.current.Use();
            }
            for (int index = 0; index < this.Parent_desPanelsCached.Value.Count; ++index)
            {
                Rect rect = new Rect(num1 * width, num2 * 32f, width, 32f);
                ++rect.height;
                if ((double)num1 == 0.0)
                    ++rect.width;
                ArchitectCategoryTab Pan = this.Parent_desPanelsCached.Value[index];
                Color? labelColor = Pan.AnySearchMatches ? new Color?() : new Color?(NoMatchColor);
                string labelCap = (string)Pan.def.LabelCap;
                if (Widgets.ButtonTextSubtle(rect, labelCap, 0.0f, 24f, SoundDefOf.Mouseover_Category, new Vector2(-1f, -1f), labelColor, this.Parent_quickSearchWidget.Value.filter.Active && architectCategoryTab == Pan))
                    this.ClickedCategory(Pan);
                if (this.selectedDesPanel != Pan)
                    UIHighlighter.HighlightOpportunity(rect, Pan.def.cachedHighlightClosedTag);
                a = Mathf.Max(a, rect.yMax);
                ++num1;
                if ((double)num1 > 1.0)
                {
                    num1 = 0.0f;
                    ++num2;
                }
                GUI.DrawTexture(new Rect(rect.position + new Vector2(4, (32 - 16) / 2), new Vector2(16, 16)),
                         Resources.FindArchitectTabCategoryIcon(Parent_desPanelsCached.Value[index].def.defName));
            }

            this.Parent_quickSearchWidget.Value.OnGUI(new Rect(0.0f, a + 1f, inRect.width, 24f), new Action(() =>
            {
                CacheDesPanelsWithCheck();
                this.Parent_CacheSearchState.Invoke();
            }));
            if (this.Parent_didInitialUnfocus.Value)
                return;
            UI.UnfocusCurrentControl();
            Parent_didInitialUnfocus.Value = true;
        }

        public override void OnCancelKeyPressed()
        {
            CacheDesPanelsWithCheck();
            base.OnCancelKeyPressed();
        }
    }
}