using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ArchitectIcons
{
    [StaticConstructorOnStartup]
    public static class Resources
    {
        public static readonly Dictionary<String, Texture2D> iconsCache = new Dictionary<string, Texture2D>();
        public static readonly Texture2D MissingTexture;
        static Resources()
        {
            //MISSING
            MissingTexture =  ContentFinder<Texture2D>.Get("UI/ArchitectIcons/Default/wrongsign");
        }


        public static Texture2D FindArchitectTabCategoryIcon(String categoryDefName)
        {
            if (iconsCache.ContainsKey(categoryDefName))
            {
                return iconsCache[categoryDefName];
            }
            else
            {
                //Search for mod support
                Texture2D icon = ContentFinder<Texture2D>.Get("UI/ArchitectIcons/" + categoryDefName);
                // if not supported
                
                if (!icon)
                {
                    //Chek if  ArchitectIcons adds support. If not, set missing texture.
                    icon = ContentFinder<Texture2D>.Get("UI/ArchitectIcons/Default/" + categoryDefName) ?? MissingTexture;
                }
                iconsCache.Add(categoryDefName, icon);
                return icon;
            }
        }
    }
}