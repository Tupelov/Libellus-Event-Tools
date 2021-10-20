using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.Utils
{
	public static class Reflection
	{
		/// <summary>
		/// Checks if type has interface
		/// </summary>
		/// <param name="interfaceType">The Type of interface to be checked</param>
		/// <returns>Whether or not type has interface</returns>
		public static bool HasInterface(this Type obj, Type interfaceType)
		{
			return obj.GetInterfaces().Contains(interfaceType);
		}

		/// <summary>
		/// Creates a <
		/// </summary>
		/// <param name="type">The type of list to create</param>
		/// <returns></returns>
		public static IList CreateListFromType(Type type)
		{
			var genericListType = typeof(List<>).MakeGenericType(new[] { type });
			return (IList)Activator.CreateInstance(genericListType);
		}

	}
}
