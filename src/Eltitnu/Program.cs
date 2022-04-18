using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Eltitnu.Eltitnu
{
    public static class Program
    {
        public const int WIDTH = 800;
        public const int HEIGHT = 600;
        public const float LENGTH_UNIT = 1;

        private static void Main()
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(WIDTH, HEIGHT),
                Title = "Eltitnu",
                Flags = ContextFlags.ForwardCompatible,
            };

            using (var window = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}
