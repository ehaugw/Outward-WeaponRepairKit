
namespace WeaponRepairKit
{
    using System.Collections.Generic;
    using SideLoader;
    using InstanceIDs;
    using TinyHelper;

    public class WeaponRepairKitItem
    {
        public const string SubfolderName = "WeaponRepairKit";
        public const string ItemName = "Weapon Repair Kit";

        public static Item MakeItem()
        {
            var myitem = new SL_Item()
            {
                Name = ItemName,
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.blessedPotionID,
                New_ItemID = IDs.weaponRepairKitID,
                Description = "Restores weapons to a better condition.",
                
                CastType = Character.SpellCastType.SpellBindLight,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1,
                CastSheatheRequired = 2,
                
                StatsHolder = new SL_ItemStats()
                {
                    RawWeight = 0.5f,
                    BaseValue = 25,
                },

                SLPackName = WeaponRepairKit.ModFolderName,
                SubfolderName = SubfolderName
            };
            myitem.ApplyTemplate();
            Item item = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);

            var addStatus = TinyGameObjectManager.GetOrMake(item.transform, "Effects", true, true).gameObject.AddComponent<RepairEquipmentEffect>();
            return item;
        }

        public static Item MakeRecipe()
        {
            string newUID = TinyUIDManager.MakeUID(ItemName, WeaponRepairKit.GUID, "Recipe");
            new SL_Recipe()
            {
                StationType = Recipe.CraftingType.Survival,
                Results = new List<SL_Recipe.ItemQty>() {
                    new SL_Recipe.ItemQty() { Quantity = 1, ItemID = IDs.weaponRepairKitID},
                },
                Ingredients = new List<SL_Recipe.Ingredient>() {
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.ironScrapsID},
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.hideID},
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.thickOilID},
                },
                UID = newUID,
            }.ApplyTemplate();

            var myitem = new SL_RecipeItem()
            {
                Name = "Crafting: " + ItemName,
                Target_ItemID = IDs.arbitraryAlchemyRecipeID,
                New_ItemID = IDs.weaponRepairKitRecipeID,
                EffectBehaviour = EditBehaviours.Override,
                RecipeUID = newUID
            };
            myitem.ApplyTemplate();
            Item item = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);
            return item;
        }
    }
}
