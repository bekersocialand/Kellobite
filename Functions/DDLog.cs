using log4net;

namespace Kelloggs.Functions
{
    public static class DDLog
    {
        public static readonly ILog objLog4Net = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    }
}