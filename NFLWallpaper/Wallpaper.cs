using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;

public static class Wallpaper
{
    const int SPI_SETDESKWALLPAPER = 20;
    const int SPIF_UPDATEINIFILE = 0x01;
    const int SPIF_SENDWININICHANGE = 0x02;

    internal static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        [return:MarshalAs(UnmanagedType.I4)]
        internal static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
    }

    public enum Style : int
    {
        Tiled,
        Centered,
        Stretched
    }

    public static void Set(System.Drawing.Image img, Style style)
    {
//        System.IO.Stream s = new System.Net.WebClient().OpenRead(uri.ToString());
//        System.Drawing.Image img = System.Drawing.Image.FromStream(s);
        string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
        img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
        if (style == Style.Stretched)
        {
            key.SetValue(@"WallpaperStyle", 2.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());
        }

        if (style == Style.Centered)
        {
            key.SetValue(@"WallpaperStyle", 1.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());
        }

        if (style == Style.Tiled)
        {
            key.SetValue(@"WallpaperStyle", 1.ToString());
            key.SetValue(@"TileWallpaper", 1.ToString());
        }

        NativeMethods.SystemParametersInfo(SPI_SETDESKWALLPAPER,
            0,
            tempPath,
            SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
    }
}