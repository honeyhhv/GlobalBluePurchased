using System.Reflection;

namespace GlobalBluePurchased.API.Common
{
    public static class ServiceAssembly
    {
        public static Assembly Current => typeof(ServiceAssembly).Assembly;
    }
}