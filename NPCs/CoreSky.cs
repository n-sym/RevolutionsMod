using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Revolutions.NPCs
{
	public class CoreSky : CustomSky
	{
		private bool Active;

		public override void Update(GameTime gameTime) 
		{
		}
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth) {
			if (maxDepth >= 0 && minDepth < 0) 
			{
				spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(30, 30, 50));
			}
		}
		public override float GetCloudAlpha() {
			return 255f;
		}

		public override void Activate(Vector2 position, params object[] args) {
			Active = true;
		}

		public override void Deactivate(params object[] args) {
			Active = false;
		}

		public override void Reset() {
			Active = false;
		}

		public override bool IsActive() {
			return Active;
		}
	}
}