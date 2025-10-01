
namespace Chap7.NestedClass
{
    internal class NestedClass
    {
        static void Run()
        {
            Configuration config = new Configuration();
            config.SetConfig("Version", "V 5.0");
            config.SetConfig("Size","655,324 KB");

            Console.WriteLine(config.GetConfig("Version"));
            Console.WriteLine(config.GetConfig("Size"));
            config.SetConfig("Version","V 5.0.1");
            Console.WriteLine(config.GetConfig("Version"));
        }
    }

    internal class Configuration
    {
        List<ItemValue> listConfig = new List<ItemValue>();

        internal string GetConfig(string item)
        {
            foreach(ItemValue iv in listConfig)
            {
                if(iv.GetItem() == item) return iv.GetValue();
            }
            return "";
        }

        internal void SetConfig(string item, string value)
        {
            ItemValue iv = new ItemValue();
            iv.SetValue(this, item, value); 
        }

        private class ItemValue
        {
            private string item;
            private string value;
            public void SetValue(Configuration config, string item, string value)
            {
                this.item = item;
                this.value = value;

                bool found = false;
                for (int i = 0; i < config.listConfig.Count; i++)
                {
                    if (config.listConfig[i].item == item)
                    {
                        config.listConfig[i] = this;
                        found = true;
                        break;
                    }
                }
                if(!found) config.listConfig.Add(this);
            }
            public string GetItem() { return item; }
            public string GetValue() { return value; }
        }
    }
}
