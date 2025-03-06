using System.Reflection;

namespace CarParkingSystem.Infrastructure.Tests.TestDataFiller
{
    public static class TestDataFiller
    {
        public static T FillTestData<T>() where T : class
        {
            // Use reflection to create an instance even if it has required members
            T instance = (T)Activator.CreateInstance(typeof(T), nonPublic: true)!;

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (!property.CanWrite) continue; // Skip readonly properties

                object? defaultValue = GetDefaultValue(property.PropertyType);

                if (defaultValue != null)
                {
                    property.SetValue(instance, defaultValue);
                }
            }

            return instance;
        }

        private static object? GetDefaultValue(Type type)
        {
            if (type == typeof(int)) return 42;
            if (type == typeof(double)) return 42.42;
            if (type == typeof(string)) return "TestString";
            if (type == typeof(bool)) return true;
            if (type == typeof(DateTime)) return DateTime.Now;
            if (type == typeof(Guid)) return Guid.NewGuid();
            if (type == typeof(byte[])) return new byte[] { 1, 2, 3, 4, 5 }; // Handle byte array
            if (type.IsEnum) return Enum.GetValues(type).Cast<object>().FirstOrDefault() ?? Activator.CreateInstance(type);

            // Handle arrays of primitive types
            if (type.IsArray)
            {
                Type? elementType = type.GetElementType();
                if (elementType == null) return null;

                Array arrayInstance = Array.CreateInstance(elementType, 3);
                for (int i = 0; i < arrayInstance.Length; i++)
                {
                    arrayInstance.SetValue(GetDefaultValue(elementType), i);
                }
                return arrayInstance;
            }

            // Handle complex objects
            if (type.IsClass && type.GetConstructor(Type.EmptyTypes) != null)
            {
                return Activator.CreateInstance(type, nonPublic: true);
            }

            return null;
        }
    }

}
