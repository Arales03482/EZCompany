using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EZCompanyMod.Helpers
{
    internal class AccessHelper
    {
		public static void SetProperty(object a, string prop, object val)
		{
			FieldInfo field = a.GetType().GetField(prop, BindingFlags.Instance | BindingFlags.NonPublic);
			field.SetValue(a, val);
		}

		public static T GetProperty<T>(object a, string prop)
		{
			FieldInfo field = a.GetType().GetField(prop, BindingFlags.Instance | BindingFlags.NonPublic);
			return (T)field.GetValue(a);
		}

		public static object GetProperty(object a, string prop)
		{
			FieldInfo field = a.GetType().GetField(prop, BindingFlags.Instance | BindingFlags.NonPublic);
			return field.GetValue(a);
		}

		public static void SetSubProperty(object a, string prop, string prop2, object val)
		{
			FieldInfo field = a.GetType().GetField(prop, BindingFlags.Instance | BindingFlags.NonPublic);
			FieldInfo field2 = field.GetType().GetField(prop, BindingFlags.Instance | BindingFlags.NonPublic);
			field2.SetValue(field, val);
		}

		public static T CallSubFunc<T>(object a, string prop, string func, object val)
		{
			return CallSubFunc<T>(a, prop, func, new object[]
			{
				val
			});
		}

		public static T CallSubFunc<T>(object a, string prop, string func, object[] vals)
		{
			FieldInfo field = a.GetType().GetField(prop, BindingFlags.Instance | BindingFlags.NonPublic);
			MethodInfo method = field.GetType().GetMethod(func, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			return (T)method.Invoke(field, vals);
		}

		public static void CallSubFunc(object a, string prop, string func, object val)
		{
			CallSubFunc(a, prop, func, new object[]
			{
				val
			});
		}

		public static void CallSubFunc(object a, string prop, string func, object[] vals)
		{
			FieldInfo field = a.GetType().GetField(prop, BindingFlags.Instance | BindingFlags.NonPublic);
			MethodInfo method = field.GetType().GetMethod(func, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			method.Invoke(field, vals);
		}

		public static T CallFunc<T>(object a, string func, object val)
		{
			return CallFunc<T>(a, func, new object[]
			{
				val
			});
		}

		public static void CallFunc(object a, string func, object val)
		{
			CallFunc(a, func, new object[]
			{
				val
			});
		}

		public static void CallFunc(object a, string func, object[] vals)
		{
			MethodInfo method = a.GetType().GetMethod(func, BindingFlags.Instance | BindingFlags.NonPublic);
			method.Invoke(a, vals);
		}
	}
}
