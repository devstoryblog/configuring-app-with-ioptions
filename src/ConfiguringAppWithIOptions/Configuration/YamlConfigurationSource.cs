using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using YamlDotNet.Serialization;

namespace ConfiguringAppWithIOptions.Configuration
{
    public class YamlConfigurationSource : IConfigurationSource
    {
        public YamlConfigurationSource(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new YamlConfigurationProvider(this, builder.GetFileProvider());
        }
    }

    public class YamlConfigurationProvider : ConfigurationProvider
    {
        private readonly IFileProvider _fileProvider;
        private readonly YamlConfigurationSource _source;

        public YamlConfigurationProvider(YamlConfigurationSource source, IFileProvider fileProvider)
        {
            _source = source;
            _fileProvider = fileProvider;
        }

        public override void Load()
        {
            IFileInfo file = _fileProvider.GetFileInfo(_source.FileName);
            using (Stream stream = file.CreateReadStream())
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    Dictionary<object, object> document = new DeserializerBuilder().Build().Deserialize(reader) as Dictionary<object, object>;
                    if (document != null)
                    {
                        foreach (KeyValuePair<object, object> item in document)
                        {
                            Data[item.Key.ToString()] = item.Value.ToString();
                        }
                    }
                }
            }
        }
    }

    public static class YamlConfigurationSourceExtensions
    {
        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string filename)
        {
            return builder.Add(new YamlConfigurationSource(filename));
        }
    }
}