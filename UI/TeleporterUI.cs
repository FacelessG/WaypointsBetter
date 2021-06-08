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

namespace waypoints.UI
{
	internal class TeleporterUI : UIState
	{
		public struct Buttons
		{
			public Buttons(UIHoverImageButton image, UIText text)
			{
				IMAGE = image;
				TEXT = text;
			}

			public UIHoverImageButton IMAGE { get; }
			public UIText TEXT { get; }
		}

		public static TeleporterUI instance { get; private set; }

		public DragableUIPanel TelportWaypointsPanel;
		public List<Buttons> buttons = new List<Buttons>();
		public static bool Visible;

		// In OnInitialize, we place various UIElements onto our UIState (this class).
		// UIState classes have width and height equal to the full screen, because of this, usually we first define a UIElement that will act as the container for our UI.
		// We then place various other UIElement onto that container UIElement positioned relative to the container UIElement.
		public override void OnInitialize()
		{
			instance = this;
			// Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features.
			TelportWaypointsPanel = new DragableUIPanel();
			TelportWaypointsPanel.SetPadding(0);
			// We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(coinCounterPanel);`. 
			// This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
			TelportWaypointsPanel.Left.Set(400f, 0f);
			TelportWaypointsPanel.Top.Set(100f, 0f);
			TelportWaypointsPanel.Width.Set(300f, 0f);
			TelportWaypointsPanel.Height.Set(500f, 0f);
			TelportWaypointsPanel.BackgroundColor = new Color(73, 94, 171);

			Texture2D buttonDeleteTexture = ModContent.GetTexture("waypoints/UI/Icons/closeButton");
			UIHoverImageButton closeButton = new UIHoverImageButton(buttonDeleteTexture, Language.GetTextValue("LegacyInterface.52")); // Localized text for "Close"
			closeButton.Left.Set(270, 0f);
			closeButton.Top.Set(10, 0f);
			closeButton.Width.Set(22, 0f);
			closeButton.Height.Set(22, 0f);
			closeButton.OnClick += new MouseEvent(CloseButtonClicked);
			TelportWaypointsPanel.Append(closeButton);

			UIText text = new UIText("Placed Waypoints");
			text.Width.Set(300f, 0f); 
			text.Height.Set(20, 0f);
			text.Top.Set(10, 0f);
			TelportWaypointsPanel.Append(text);

			Append(TelportWaypointsPanel);

			// As a recap, ExampleUI is a UIState, meaning it covers the whole screen. We attach coinCounterPanel to ExampleUI some distance from the top left corner.
			// We then place playButton, closeButton, and moneyDiplay onto coinCounterPanel so we can easily place these UIElements relative to coinCounterPanel.
			// Since coinCounterPanel will move, this proper organization will move playButton, closeButton, and moneyDiplay properly when coinCounterPanel moves.
		}

        public void OnOpen()
        {
			waypoints w = waypoints.w;

			while (buttons.Count > 0)
			{
				for (int i = 0; i < buttons.Count; i++)
				{
					buttons[i].TEXT.Remove();
					buttons[i].IMAGE.Remove();

					buttons.RemoveAt(i);
				}
			}

			for (int i = 0; i < w.placedWaypoints.Count; i++)
			{
				float top = 36 + (35 * i);
				UIHoverImageButton b = Button(top);
				UIText t = ButtonName(top, w.placedWaypoints[i].NAME);
				buttons.Add(new Buttons(b, t));

				TelportWaypointsPanel.Append(b);
				TelportWaypointsPanel.Append(t);
			}

			Debug.WriteLine(w.placedWaypoints.Count);
			Append(TelportWaypointsPanel);
		}

        UIHoverImageButton Button(float top)
        {
			Texture2D tex = ModContent.GetTexture("waypoints/UI/Icons/Bar");
			UIHoverImageButton button = new UIHoverImageButton(tex, "Teleport");
			button.Left.Set(1, 0f);
			button.Top.Set(top, 0f);
			button.Width.Set(300, 0f);
			button.Height.Set(50, 0f);
			button.OnClick += new MouseEvent(Teleport);
			return button;
		}

		UIText ButtonName(float top, string name)
		{
			UIText text = new UIText(name);
			text.Width.Set(300f, 0f);
			text.Height.Set(20, 0f);
			text.Top.Set(top+10, 0f);
			text.MarginLeft = -100;
			return text;
		}

        private void Teleport(UIMouseEvent evt, UIElement listeningElement)
        {
			UIHoverImageButton im = (UIHoverImageButton)listeningElement;
            for (int i = 0; i < buttons.Count; i++)
            {
				if (buttons[i].IMAGE == im)
                {
					waypoints w = waypoints.w;
					Main.LocalPlayer.position = new Vector2(w.placedWaypoints[i].X, w.placedWaypoints[i].Y-30);
				}
            }
			Main.PlaySound(SoundID.MenuClose);
        }

		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(SoundID.MenuClose);
			Visible = false;
		}
    }
}