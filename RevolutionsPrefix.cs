using System;
using Terraria;
using Terraria.ModLoader;

namespace Revolutions.Prefixes
{
	public class RevolutionsPrefix : ModPrefix
	{
		public override float RollChance(Item item)
			=> 5f;
		public override bool CanRoll(Item item)
			=> true;

		public override PrefixCategory Category
			=> PrefixCategory.Melee;

		public RevolutionsPrefix() {
		}
		public override bool Autoload(ref string name) {
			if (!base.Autoload(ref name)) {
				return false;
			}
			mod.AddPrefix("Piercing", new RevolutionsPrefix());
			return false;
		}

		public override void Apply(Item item)
		{
			item.damage = (int)Math.Floor(item.damage * 1.11f);
			item.crit += 10;
		}

		public override void ModifyValue(ref float valueMult) {
			valueMult *= 1.508727f;
		}
	}
}