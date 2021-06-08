using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace waypoints
{
    public class WorldData : ModWorld
    {
        public override TagCompound Save()
        {
            var waypointData = waypoints.w.placedWaypoints;

            var wDS = new List<string>();
            var wDX = new List<int>();
            var wDY = new List<int>();

            for (int i = 0; i < waypointData.Count; i++)
            {
                wDS.Add(waypointData[i].NAME);
                wDX.Add(waypointData[i].X);
                wDY.Add(waypointData[i].Y);
            }

            return new TagCompound
            {
                ["wDS"] = wDS,
                ["wDX"] = wDX,
                ["wDSY"] = wDY
            };
        }

        public override void Load(TagCompound tag)
        {
            var wDS = tag.GetList<string>("wDS");
            var wDX = tag.GetList<int>("wDX");
            var wDY = tag.GetList<int>("wDSY");

            List<PlacedWaypoints> pW = new List<PlacedWaypoints>();

            for (int i = 0; i < wDS.Count; i++)
            {
                pW.Add(new PlacedWaypoints(wDX[i], wDY[i], wDS[i]));
            }
            waypoints.w.placedWaypoints = pW;
        }
    }
}
