using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.UI;
using waypoints.Items;
using waypoints.Tiles;
using waypoints.UI;
namespace waypoints
{
	public struct PlacedWaypoints
	{
		public PlacedWaypoints(int x, int y, string name)
		{
			X = x;
			Y = y;
			NAME = name;
		}

		public void ChangeName(string name)
        {
			NAME = name;
        }

		public int X { get; set; }
		public int Y { get; set; }
		public string NAME;
	}
	public class waypoints : Mod
	{
		internal TeleporterUI teleporterUI;
		private UserInterface _teleporterUserIterface;

		internal WaypointUI waypointUI;
		private UserInterface _waypointUserInterface;

		internal MainInterface mainInterface;
		private UserInterface _mainUserInterface;

		public static waypoints w { get; private set; }
		public List<PlacedWaypoints> placedWaypoints = new List<PlacedWaypoints>();

        public override void Load()
        {
			w = this;
			teleporterUI = new TeleporterUI();
			teleporterUI.Activate();
			_teleporterUserIterface = new UserInterface();
			_teleporterUserIterface.SetState(teleporterUI);

			waypointUI = new WaypointUI();
			waypointUI.Activate();
			_waypointUserInterface = new UserInterface();
			_waypointUserInterface.SetState(waypointUI);


			mainInterface = new MainInterface();
			mainInterface.Activate();
			_mainUserInterface = new UserInterface();
			_mainUserInterface.SetState(mainInterface);

			GetAllPlacedWaypoints();
		}
		public override void UpdateUI(GameTime gameTime)
		{
			if (TeleporterUI.Visible)
			{
				_teleporterUserIterface?.Update(gameTime);
			}

			if (WaypointUI.Visible)
			{
				_waypointUserInterface?.Update(gameTime);
			}

			_mainUserInterface?.Update(gameTime);

		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1)
			{
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"ExampleMod: Coins Per Minute",
					delegate {
						if (TeleporterUI.Visible)
						{
							_teleporterUserIterface.Draw(Main.spriteBatch, new GameTime());
						}
						if (WaypointUI.Visible)
						{
							_waypointUserInterface.Draw(Main.spriteBatch, new GameTime());
						}
						_mainUserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}

		public int GetWaypointIndex(int x, int y)
        {
            for (int i = 0; i < placedWaypoints.Count; i++)
            {
				if (placedWaypoints[i].X == x * 16 && placedWaypoints[i].Y == y * 16)
                {
					Debug.WriteLine("Match found at index " + i);
					return i;
                }
            }
			return 0;
        }

		public void ChangeWaypointName(int x, int y, string name)
        {
			placedWaypoints[GetWaypointIndex(x, y)].ChangeName(name);
			Debug.WriteLine(placedWaypoints[GetWaypointIndex(x, y)].NAME);
        }

		public void AddNewWaypoint(int xPos, int yPos, Waypoint waypoint)
        {
			placedWaypoints.Add(new PlacedWaypoints(xPos*16, yPos*16, "Waypoint " + (placedWaypoints.Count + 1).ToString()));
            for (int i = 0; i < placedWaypoints.Count; i++)
            {
				Debug.WriteLine("Waypoint " + placedWaypoints[i].NAME + " is located at " + placedWaypoints[i].X + ", " + placedWaypoints[i].Y);
            }
			Debug.WriteLine("---------------------------------------------------------");
        }

		public void RemoveWaypoint(int xPos, int yPos)
		{
            xPos += 1;
			yPos += 2;

            for (int i = 0; i < placedWaypoints.Count; i++)
            {
				if(placedWaypoints[i].X/16 == xPos && placedWaypoints[i].Y/16 == yPos)
                {
					placedWaypoints.RemoveAt(i);
					Debug.WriteLine("Waypoint located at " + xPos *16 + ", " + yPos*16 + " was removed");
                }
            }
		}

		public void GetAllPlacedWaypoints()
        {
			ModTile tile = GetTile("Waypoint");
        }
	}
}