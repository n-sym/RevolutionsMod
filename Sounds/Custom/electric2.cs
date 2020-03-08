using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Sounds.Custom
{
	public class electric2 : ModSound
	{
		public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type) 
		{
			if (soundInstance.State == SoundState.Playing) {
				return null;
			}
			soundInstance.Volume = volume * 0.3f;
			soundInstance.Pan = pan;
			soundInstance.Pitch = Main.rand.Next(-5, 6) * .05f;
			return soundInstance;
		}
	}
}
