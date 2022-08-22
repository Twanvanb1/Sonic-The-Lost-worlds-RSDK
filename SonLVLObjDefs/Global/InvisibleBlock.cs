using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Global
{
	class InvisibleBlock : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite img;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(1, 176, 16, 14), -8, -7);
			
			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Width", typeof(int), "Extended",
				"How wide the block will .", null,
				(obj) => (obj.PropertyValue & 15) + 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 240) | ((int)value & 15) - 1));
			
			properties[1] = new PropertySpec("Height", typeof(int), "Extended",
				"How tall the block will .", null,
				(obj) => ((obj.PropertyValue & 240) >> 4) + 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 15) | (((int)value & 240) << 4) - 1));
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
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			// TODO: broken :(
			// i'm not quite sure how this object works
			
			int width = (obj.PropertyValue & 15) + 1;
			int height = ((obj.PropertyValue & 240) >> 4) + 1;
			
			int sx = -((width * 16) / 2) - 8;
			int sy = -((height * 14) / 2) + 22;
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Sprite tmp = new Sprite(img);
					tmp.Offset(sx + (i * 16), sy + (j * 14));
					sprs.Add(tmp);
				}
			}
			return new Sprite(sprs.ToArray());
		}
	}
}