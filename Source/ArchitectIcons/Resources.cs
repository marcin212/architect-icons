using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ArchitectIcons
{
    [StaticConstructorOnStartup]
    public static class Resources
    {
        public static readonly Dictionary<String, Texture2D> icons = new Dictionary<string, Texture2D>();
        public static readonly Texture2D MissingTexture;
        static Resources()
        {
            //MISSING
            MissingTexture =  ContentFinder<Texture2D>.Get("UI/Icons/wrongsign");
            //CORE
            icons.Add("Floors", ContentFinder<Texture2D>.Get("UI/Icons/floors"));
            icons.Add("Furniture", ContentFinder<Texture2D>.Get("UI/Icons/furniture"));
            icons.Add("Joy", ContentFinder<Texture2D>.Get("UI/Icons/joy"));
            icons.Add("Misc", ContentFinder<Texture2D>.Get("UI/Icons/misc"));
            icons.Add("Orders", ContentFinder<Texture2D>.Get("UI/Icons/orders"));
            icons.Add("Power", ContentFinder<Texture2D>.Get("UI/Icons/power"));
            icons.Add("Production", ContentFinder<Texture2D>.Get("UI/Icons/production"));
            icons.Add("Security", ContentFinder<Texture2D>.Get("UI/Icons/security"));
            icons.Add("Ship", ContentFinder<Texture2D>.Get("UI/Icons/ship"));
            icons.Add("Structure", ContentFinder<Texture2D>.Get("UI/Icons/structure"));
            icons.Add("Temperature", ContentFinder<Texture2D>.Get("UI/Icons/temperature"));
            icons.Add("Zone", ContentFinder<Texture2D>.Get("UI/Icons/zone"));
            //MODS
            icons.Add("Blueprints", ContentFinder<Texture2D>.Get("UI/Icons/blueprints"));
            icons.Add("Fences", ContentFinder<Texture2D>.Get("UI/Icons/fences"));
            icons.Add("ANON2MF", ContentFinder<Texture2D>.Get("UI/Icons/anon2mf"));
            icons.Add("JeffBridges", ContentFinder<Texture2D>.Get("UI/Icons/jeffbridges"));
            icons.Add("VGBrewing", ContentFinder<Texture2D>.Get("UI/Icons/vgbrewing"));
            icons.Add("GardenTools", ContentFinder<Texture2D>.Get("UI/Icons/gardentools"));
            icons.Add("Rimatomics", ContentFinder<Texture2D>.Get("UI/Icons/rimatomics"));
            icons.Add("CentralClimateControl", ContentFinder<Texture2D>.Get("UI/Icons/centralclimatecontrol"));
            icons.Add("Hygiene", ContentFinder<Texture2D>.Get("UI/Icons/hygiene"));
        }
    }
}