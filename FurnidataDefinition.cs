using Plus.Plugins;

namespace Furnidata
{
    public class FurnidataDefinition : IPluginDefinition
    {
        public string Name => "Furnidata Plugin";
        public string Author => "Harb#9937";
        public Version Version => new(1, 0, 0);
        public Type PluginClass => typeof(Furnidata);
    }
}