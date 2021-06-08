using Terraria;
using waypoints.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace waypoints.Items
{
    public class WaypointItem : ModItem
    {
		private bool canUseItem = true;
        public override void SetDefaults()
        {
			item.width = 22;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.rare = ItemRarityID.Expert;
			item.createTile = TileType<Waypoint>();
		}

        public override bool CanUseItem(Player player)
        {
            return canUseItem;
        }

        public override void UpdateInventory(Player player)
        {
            if (waypoints.w.placedWaypoints.Count >= 13)
                canUseItem = false;
            else
                canUseItem = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
