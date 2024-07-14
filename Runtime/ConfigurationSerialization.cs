namespace Guyl.GLogger
{
    using System;

    [Serializable]
    public class ChannelsSerialization
    {
        public string[] Includes;
        public string[] Excludes;

        public ChannelsSerialization() { }

        public ChannelsSerialization(string[] includes, string[] excludes)
        {
            Includes = includes;
            Excludes = excludes;
        }
    }

    [Serializable]
    public class LoggersSerialization
    {
        public string[] LogLevel;
        public string Handler;
        
        public ChannelsSerialization Channels;

        public LoggersSerialization() { }

        public LoggersSerialization(string[] logLevel, string handler, ChannelsSerialization channels)
        {
            LogLevel = logLevel;
            Handler = handler;
            Channels = channels;
        }
    }
    
    [Serializable]
    public class ConfigurationSerialization
    {
        public static ConfigurationSerialization Default = new ConfigurationSerialization(new []
        {
            new LoggersSerialization(
                new []{ "VeryVerbose", "Trace", "Debug", "Information", "Warning", "Error", "Critical", "Exception" },
                "UnityConsole",
                new ChannelsSerialization(
                    new []{ "*"},
                    Array.Empty<string>()
                )
            )
        });
        
        public LoggersSerialization[] Loggers;

        public ConfigurationSerialization() { }

        public ConfigurationSerialization(LoggersSerialization[] loggers)
        {
            Loggers = loggers;
        }
    }
}
