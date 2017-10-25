using System.Threading;

namespace YT.Localization
{
    public static class CultureHelper
    {
        public static bool IsRtl => Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft;
    }
}
