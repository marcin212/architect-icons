using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace ArchitectIcons
{
    public class MainTabWindow_Architect : RimWorld.MainTabWindow_Architect
    {
        private List<ArchitectCategoryTabCustomOffset> desPanelsCached;
        public const float WinWidth = 200f + 2 * 16;
        private bool swaped = false;

        public override Vector2 RequestedTabSize
        {
            get { return new Vector2(WinWidth, this.WinHeight); }
        }

        public float WinHeight
        {
            get
            {
                if (desPanelsCached == null)
                    this.CacheDesPanels();
                return (float) Mathf.CeilToInt((float) desPanelsCached.Count / 2f) * 32f;
            }
        }

        public MainTabWindow_Architect()
        {
            this.CacheDesPanels();
        }

        public override void ExtraOnGUI()
        {
            if (this.selectedDesPanel == null)
                return;
            (selectedDesPanel as ArchitectCategoryTabCustomOffset)?.DesignationTabOnGUI();
        }

        private void CacheDesPanels()
        {
            this.desPanelsCached = new List<ArchitectCategoryTabCustomOffset>();
            foreach (DesignationCategoryDef def in (IEnumerable<DesignationCategoryDef>)
                DefDatabase<DesignationCategoryDef>.AllDefs.OrderByDescending<DesignationCategoryDef, int>(
                    (Func<DesignationCategoryDef, int>) (dc => dc.order)))
                desPanelsCached.Add(new ArchitectCategoryTabCustomOffset(def));

            List<string> listTab = new List<string>();
            for (int index = 0; index < desPanelsCached.Count; ++index)
            {
                listTab.Add(desPanelsCached[index].def.defName);
            }

            try
            {
                System.IO.File.WriteAllLines(Resources.GetSettingsPath("CategoryTabsName.txt"), listTab.ToArray());
            }
            catch (Exception e) { };
        }

        public override void DoWindowContents(Rect inRect)
        {
            SetInitialSizeAndPosition();
            Text.Font = GameFont.Small;
            float width = inRect.width / 2f;
            float num1 = 0.0f;
            float num2 = 0.0f;
            for (int index = 0; index < desPanelsCached.Count; ++index)
            {
                Rect rect = new Rect(num1 * width, num2 * 32f, width, 32f);
                ++rect.height;
                if ((double) num1 == 0.0)
                    ++rect.width;

                if (Widgets.ButtonTextSubtle(rect, desPanelsCached[index].def.LabelCap, 0.0f, 24f,
                    SoundDefOf.Mouseover_Category))
                    this.ClickedCategory(desPanelsCached[index]);
                if (this.selectedDesPanel != desPanelsCached[index])
                    UIHighlighter.HighlightOpportunity(rect, desPanelsCached[index].def.cachedHighlightClosedTag);
                ++num1;
                if ((double) num1 > 1.0)
                {
                    num1 = 0.0f;
                    ++num2;
                }

                GUI.DrawTexture(new Rect(rect.position + new Vector2(4, (32 - 16) / 2), new Vector2(16, 16)),
                    Resources.FindArchitectTabCategoryIcon(desPanelsCached[index].def.defName));
            }
        }
    }
}