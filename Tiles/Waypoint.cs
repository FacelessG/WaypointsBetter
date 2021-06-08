using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using waypoints.UI;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using Terraria.Localization;
    

namespace waypoints.Tiles
{
    public class Waypoint : ModTile
    {
		public waypoints w;
		Vector2 pos;
		ModTranslation name;
		string waypointName = "Waypoint";
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 18 };
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(GetInstance<TEWaypoint>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.addTile(Type);
			name = CreateMapEntryName();
			name.SetDefault(waypointName);
			AddMapEntry(new Color(173, 0, 255), name);
			dustType = DustID.Silver;
			disableSmartCursor = true;

			w = waypoints.w;
		}

        public override bool NewRightClick(int i, int j)
        {
			//Main.NewText("Open Waypoint Menu", 186, 0, 255);
			WaypointUI.Visible = true;
			WaypointUI.xPos = i;
			WaypointUI.yPos = j;
			WaypointUI.instance.OnMenuOpen();
			
			
			return true;
        }

		public override void PlaceInWorld(int i, int j, Item item)
        {
			w.AddNewWaypoint(i, j, this);
			pos = new Vector2(i*16, j*16);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 48, ItemType<Items.WaypointItem>());
			w.RemoveWaypoint(i, j);
			GetInstance<TEWaypoint>().Kill(i, j);
		}

		

    }
}
