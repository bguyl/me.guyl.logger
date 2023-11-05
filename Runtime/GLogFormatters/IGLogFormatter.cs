namespace Guyl.Logger
{
    public interface IGLogFormatter
    {
        public string Format(GLogType logType,
            string channel,
            UnityEngine.Object context,
            string format,
            object caller,
            params object[] args
        );
    }
}