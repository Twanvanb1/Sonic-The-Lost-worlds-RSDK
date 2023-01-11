using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.WHZ
{
	class SpearBlock : ObjectDefinition
	{
		private PropertySpec[] properties;
		private readonly Sprite[] sprites = new Sprite[5];
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("WHZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(51, 30, 8, 32), -4, -16);
				sprites[1] = new Sprite(sheet.GetSection(69, 30, 32, 8), -16, -4);
				sprites[2] = new Sprite(sheet.GetSection(60, 30, 8, 32), -4, -16);
				sprites[3] = new Sprite(sheet.GetSection(69, 39, 32, 8), -16, -4);
				//block
				sprites[4] = new Sprite(sheet.GetSection(1,1, 32, 32), -16, -16);
			}
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Spear Direction", typeof(int), "Extended",
				"Where the Spear will point initially.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Right", 1 },
					{ "Downwards", 2 },
					{ "Left", 3 }
				},
				(obj) => (obj.PropertyValue & 3),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 252) | (byte)((int)value)));
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return null;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			// A bit odd, could probably be optimised a bit
			// This is mostly just a direct translation of what the base game does
			
			List<Sprite> sprs = new List<Sprite>();
			
			Sprite tmp = new Sprite(sprites[(obj.PropertyValue & 3)]);
			switch (obj.PropertyValue & 3)
			{
				case 0:
					tmp.Offset(0, -32);
					break;
				case 1:
					tmp.Offset(32, 0);
					break;
				case 2:
					tmp.Offset(0, 32);
					break;
				case 3:
					tmp.Offset(-32, 0);
					break;
			}
			sprs.Add(tmp);
			
			sprs.Add(sprites[4]);
			
			return new Sprite(sprs.ToArray());
		}
	}
}