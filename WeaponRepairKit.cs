namespace WeaponRepairKit
{
    using System.Collections.Generic;
    using UnityEngine;
    using BepInEx;
    using HarmonyLib;
    using System;
    using SideLoader;
    using System.IO;
    using InstanceIDs;
    using System.Linq;

    [BepInPlugin(GUID, NAME, VERSION)]
    public class WeaponRepairKit : BaseUnityPlugin
    {
        public const string GUID = "com.ehaugw.weaponrepairkit";
        public const string VERSION = "1.0.2";
        public const string NAME = "Weapon Repair Kit";
        public static string ModFolderName = Directory.GetParent(typeof(WeaponRepairKit).Assembly.Location).Name.ToString();

        internal void Awake()
        {
            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            SL.OnPacksLoaded += OnPackLoaded;
            SL.OnSceneLoaded += OnSceneLoaded;
        }
        private void OnPackLoaded()
        {
            WeaponRepairKitItem.MakeItem();
            WeaponRepairKitItem.MakeRecipe();
        }

        private void OnSceneLoaded()
        {
            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.name == "HumanSNPC_Blacksmith"))
            {
                if (obj.GetComponentsInChildren<GuaranteedDrop>()?.FirstOrDefault(table => table.ItemGenatorName == "Recipes") is GuaranteedDrop recipes)
                {
                    if (At.GetField<GuaranteedDrop>(recipes, "m_itemDrops") is List<BasicItemDrop> drops)
                    {
                        foreach (Item item in new Item[] { ResourcesPrefabManager.Instance.GetItemPrefab(IDs.weaponRepairKitRecipeID) })
                        {
                            drops.Add(new BasicItemDrop() { ItemRef = item, MaxDropCount = 1, MinDropCount = 1 });
                        }
                    }
                }
            }
        }
    }
}