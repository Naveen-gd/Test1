using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace System
{
    public static class ArrayExtensions
    {
        public static void ForEach(this Array array, Action<Array, int[]> action)
        {
            if (array.LongLength == 0) return;
            ArrayTraverse walker = new ArrayTraverse(array);
            do action(array, walker.Position);
            while (walker.Step());
        }
    }

    internal class ArrayTraverse
    {
        public int[] Position;
        private int[] maxLengths;

        public ArrayTraverse(Array array)
        {
            maxLengths = new int[array.Rank];
            for (int i = 0; i < array.Rank; ++i)
            {
                maxLengths[i] = array.GetLength(i) - 1;
            }
            Position = new int[array.Rank];
        }

        public bool Step()
        {
            for (int i = 0; i < Position.Length; ++i)
            {
                if (Position[i] < maxLengths[i])
                {
                    Position[i]++;
                    for (int j = 0; j < i; j++)
                    {
                        Position[j] = 0;
                    }
                    return true;
                }
            }
            return false;
        }
    }

    public static class ObjectExtensions
    {
        private static readonly MethodInfo CloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        public static bool IsPrimitive(this Type type)
        {
            if (type == typeof(String)) return true;
            return (type.IsValueType & type.IsPrimitive);
        }

        public static Object Copy(this Object originalObject)
        {
            return InternalCopy(originalObject, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
        }

        private static Object InternalCopy(Object originalObject, IDictionary<Object, Object> visited)
        {
            if (originalObject == null) return null;
            var typeToReflect = originalObject.GetType();
            if (IsPrimitive(typeToReflect)) return originalObject;
            if (visited.ContainsKey(originalObject)) return visited[originalObject];
            if (typeof(Delegate).IsAssignableFrom(typeToReflect)) return null;
            var cloneObject = CloneMethod.Invoke(originalObject, null);
            if (typeToReflect.IsArray)
            {
                var arrayType = typeToReflect.GetElementType();
                if (IsPrimitive(arrayType) == false)
                {
                    Array clonedArray = (Array)cloneObject;
                    clonedArray.ForEach((array, indices) => array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
                }

            }
            visited.Add(originalObject, cloneObject);
            CopyFields(originalObject, visited, cloneObject, typeToReflect);
            RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
            return cloneObject;
        }

        private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
        {
            if (typeToReflect.BaseType != null)
            {
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }

        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
        {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter != null && filter(fieldInfo) == false) continue;
                if (IsPrimitive(fieldInfo.FieldType)) continue;
                var originalFieldValue = fieldInfo.GetValue(originalObject);
                var clonedFieldValue = InternalCopy(originalFieldValue, visited);
                fieldInfo.SetValue(cloneObject, clonedFieldValue);
            }
        }

        public static T Copy<T>(this T original)
        {
            return (T)Copy((Object)original);
        }
    }

    public class ReferenceEqualityComparer : EqualityComparer<Object>
    {
        public override bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }
        public override int GetHashCode(object obj)
        {
            if (obj == null) return 0;
            return obj.GetHashCode();
        }
    }

    public static class Extensions
    {
        public static void DoubleBuffered(this Control dgv)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, true, null);
        }
    
        public static uint ParseAsUInt(this string data)
        {
            if ((data.Length >= 3) && (data.Substring(0, 2) == "0x")) 
                return Convert.ToUInt32(data, 16);
            return Convert.ToUInt32(data, 10);
        }

        public static byte ParseAsByte(this string data)
        {
            if ((data.Length >= 3) && (data.Length <= 4) && (data.Substring(0, 2) == "0x"))
                return Convert.ToByte(data, 16);
            if (data.Length <= 3)
                return Convert.ToByte(data, 10);
            throw new FormatException();
        }

        public static bool ParseAsBool(this string data)
        {
            if ((data.Length == 1) && (data.Substring(0, 1) == "0"))
                return false;
            if ((data.Length == 1) && (data.Substring(0, 1) == "1"))
                return true;
            throw new FormatException();
        }

        public static String ToString(this bool data)
        {
            return String.Format("{0:D1}", data ? 1 : 0);
        }

        public static String ToHexString(this byte data)
        {
            return String.Format("0x{0:X2}", data);
        }

        public static String ToHexString(this ushort data)
        {
            return String.Format("0x{0:X4}", data);
        }

        public static String ToHexString(this uint data, int bits)
        {
            String dataStr;
            if (bits == 1)
                dataStr = String.Format("{0:D}", data);
            else if (bits > 31) dataStr = String.Format("0x{0:X8}", data);
            else if (bits > 12) dataStr = String.Format("0x{0:X4}", data);
            else if (bits > 8) dataStr = String.Format("0x{0:X3}", data);
            else if (bits > 4) dataStr = String.Format("0x{0:X2}", data);
            else dataStr = String.Format("0x{0:X1}", data);
            return dataStr;
        }

        public static uint Pow(this uint bas, uint exp)
        {
            uint result = 1;
            for (uint i = 0; i < exp; i++)
                result *= bas;
            return result;
        }
    }

}
