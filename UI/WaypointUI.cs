using ExampleMod.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using waypoints.Items;
using waypoints.Tiles;
using waypoints.Tiles.UIElements;

namespace waypoints.UI
{
    internal class WaypointUI : UIState
    {
		public DragableUIPanel WaypointPanel;

		public static int xPos { get; set; }
		public static int yPos { get; set; }

		public static bool Visible;

		NewUITextBox textBox;

		public static WaypointUI instance { get; private set; }

		public override void OnInitialize()
		{
			WaypointPanel = new DragableUIPanel();
			WaypointPanel.SetPadding(0);
			WaypointPanel.Left.Set(400f, 0f);
			WaypointPanel.Top.Set(100f, 0f);
			WaypointPanel.Width.Set(500f, 0f);
			WaypointPanel.Height.Set(150f, 0f);
			WaypointPanel.BackgroundColor = new Color(73, 94, 171);

			Texture2D buttonDeleteTexture = ModContent.GetTexture("waypoints/UI/Icons/closeButton");
			UIHoverImageButton closeButton = new UIHoverImageButton(buttonDeleteTexture, Language.GetTextValue("Close"));
			closeButton.Left.Set(470f, 0f);
			closeButton.Top.Set(10, 0f);
			closeButton.Width.Set(22, 0f);
			closeButton.Height.Set(22, 0f);
			closeButton.OnClick += new MouseEvent(CloseButtonClicked);
			WaypointPanel.Append(closeButton);

			UIText text = new UIText("Change Waypoint Info");
			text.Width.Set(500f, 0f);
			text.Height.Set(20, 0f);
			text.Top.Set(10, 0f);
			WaypointPanel.Append(text);

			textBox = new NewUITextBox("Change Waypoint Name: ");
			textBox.Width.Set(500f, 0);
			textBox.Height.Set(25f, 0);
			textBox.Top.Set(35f, 0);
			textBox.BackgroundColor = new Color(62, 80, 145);
			textBox.BorderColor = Color.Black;
			textBox.OverflowHidden = true;
			WaypointPanel.Append(textBox);
			//36

			Texture2D tex = ModContent.GetTexture("waypoints/UI/Icons/Bar");
			UIHoverImageButton button = new UIHoverImageButton(tex, "");
			button.Left.Set(100, 0f);
			button.Top.Set(100, 0f);
			button.Width.Set(500, 0f);
			button.Height.Set(50, 0f);
			WaypointPanel.Append(button);
            button.OnClick += new MouseEvent(UpadteWaypointInfo);



            UIText bottomText = new UIText("Change");
			bottomText.Width.Set(500f, 0f);
			bottomText.Height.Set(20, 0f);
			bottomText.Top.Set(110, 0f);
			WaypointPanel.Append(bottomText);
				
			Append(WaypointPanel);

			instance = this;
		}

		public void OnMenuOpen()
        {
			textBox.SetText("");
        }

		private void UpadteWaypointInfo(UIMouseEvent evt, UIElement listeningElement)
		{
			waypoints w = waypoints.w;
			w.ChangeWaypointName(xPos, yPos, textBox.currentString);
			Visible = false;
		}
        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(SoundID.MenuClose);
			Visible = false;
		}
	}
}


