using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArchitectIcons
{
    class ReflectionUtils
    {
        public static MethodInfo GetMethodInfo(Type t, String methodName)
        {
            var info = t.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (info == null)
            {
                Logger.Error("Method: " + methodName + " not found.");
            }
            return info;
        }

        public static FieldInfo GetFieldInfo(Type t, String fieldName)
        {
            var info = t.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (info == null)
            {
                Logger.Error("Field: " + fieldName + " not found.");
            }
            return info;
        }

        public class FieldAccess<O,T>
        {
            private readonly FieldInfo info;
            private readonly object instance;

            public T Value
            {
                get => GetFieldValue();
                set => SetFieldValue(value);
            }

            public FieldAccess(O instance, String name)
            {
                this.instance = instance;
                info = GetFieldInfo(typeof(O), name);
            }

            private T GetFieldValue()
            {
                return (T) info.GetValue(instance);
            }

            private void SetFieldValue(T val)
            {
                info.SetValue(instance, val);
            }

        }

        public class MethodAccess<O,T>
        {
            private readonly MethodInfo info;
            private readonly object instance;

            public MethodAccess(O instance, String name)
            {
                this.instance = instance;
                info = GetMethodInfo(typeof(O), name);
            }

            public T Invoke()
            {
                return (T) info.Invoke(instance, new object[0]);
            }

        }


    }
}
