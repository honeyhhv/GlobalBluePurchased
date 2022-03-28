using GlobalBluePurchased.Domain.Resources.ResourceManagers.Interface;

namespace GlobalBluePurchased.Tests
{
    public class MockResourceManager : IResourceManager
    {
        public string GetName(string name)
        {
            return "Mock";
        }

        public string GetName(string name, params string[] arguments)
        {
            return "Mock";
        }

        public string this[string name] => "Mock";

        public string this[string name, params string[] arguments] => "Mock";
    }
}