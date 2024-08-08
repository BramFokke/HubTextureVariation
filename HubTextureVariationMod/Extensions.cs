using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colossal.Logging;
using UnityEngine;

namespace HubTextureVariationMod
{
	public static class Extensions
	{

		public static void LogObject<T>(this ILog log, string name, T obj) {

			log.Info($"{name} {{");
			foreach (var prop in typeof(T).GetProperties())
			{

				log.Info($"  {prop.Name}: {FormatValue(prop.GetValue(obj))}");
			}

			log.Info($"}}");

		}


		private static string FormatValue(object value) => value switch
		{
			null => "null",
			Texture texture => $"Texture {texture.name}",
			_ => value.ToString()
		};

	}
}
