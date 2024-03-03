










namespace Nez
{
    /// <summary>
    /// class that contains the names of all of the files processed by the Pipeline Tool
    /// </summary>
    /// <remarks>
    /// Nez includes a T4 template that will auto-generate the content of this file.
    /// See: https://github.com/prime31/Nez/blob/master/FAQs/ContentManagement.md#auto-generating-content-paths"
    /// </remarks>
    class Content
    {
		public static class Levels
		{
			public const string Main = @"Content\Levels\main.ldtk";
		}

		public static class Sprites
		{
			public static class UI
			{
				public const string Select = @"Content\Sprites\UI\select.png";
			}

			public const string DendroEffect = @"Content\Sprites\DendroEffect.png";
			public const string FireEffects = @"Content\Sprites\FireEffects.png";
			public const string Fruit = @"Content\Sprites\fruit.png";
			public const string IceEffects = @"Content\Sprites\IceEffects.png";
			public const string Tileset = @"Content\Sprites\Tileset.png";
		}



    }
}

