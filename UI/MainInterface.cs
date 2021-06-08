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
    public class MainInterface : UIState
    {
        UIHoverImageButton teleportMenu;

        public override void OnInitialize()
        {
            Texture2D buttonDeleteTexture = ModContent.GetTexture("waypoints/UI/Icons/Teleporter");
            teleportMenu = new UIHoverImageButton(buttonDeleteTexture, Language.GetTextValue("Teleport Menu")); // Localized text for "Close"
            teleportMenu.Left.Set(270, 0f);
            teleportMenu.Top.Set(Main.PendingResolutionHeight-  100, 0f);
            Debug.WriteLine(Main.screenHeight);
            teleportMenu.Width.Set(22, 0f);
            teleportMenu.Height.Set(22, 0f);

            Append(teleportMenu);
            teleportMenu.OnClick += new MouseEvent(OpenWaypointMenu);
        }

        private void OpenWaypointMenu(UIMouseEvent evt, UIElement listeningElement)
        {

            TeleporterUI.Visible = true;
            TeleporterUI.instance.OnOpen();
        }

        public override void Update(GameTime gameTime)
        {
            teleportMenu.Left.Set(50, 0f);
            teleportMenu.Top.Set(Main.PendingResolutionHeight - 50, 0f);
        }
    }
}
