namespace WeaponRepairKit
{
    using System.Collections.Generic;
    using UnityEngine;
    using BepInEx;
    using HarmonyLib;
    using System;
    using SideLoader;
    using System.IO;

    [BepInPlugin(GUID, NAME, VERSION)]
    public class WeaponRepairKit : BaseUnityPlugin
    {
        public const string GUID = "com.ehaugw.weaponrepairkit";
        public const string VERSION = "1.0.0";
        public const string NAME = "Weapon Repair Kit";
        public static string ModFolderName = Directory.GetParent(typeof(WeaponRepairKit).Assembly.Location).Name.ToString();

        internal void Awake()
        {
            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            SL.OnPacksLoaded += OnPackLoaded;
        }
        private void OnPackLoaded()
        {
            WeaponRepairKitItem.MakeItem();
            WeaponRepairKitItem.MakeRecipe();
        }
    }
}