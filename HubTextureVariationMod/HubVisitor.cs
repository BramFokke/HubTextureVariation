using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colossal.Entities;
using Colossal.IO.AssetDatabase;
using Colossal.Json;
using Colossal.Logging;
using Game;
using Game.Prefabs;
using Unity.Collections;
using Unity.Entities;

namespace HubTextureVariationMod
{
	public partial class AreaVisitor : PrefabVisitor<AreaTypePrefab>
	{
		protected override void Visit(Entity entity, PrefabData component, AreaTypePrefab prefab)
		{
			m_Log.Info($"{prefab.name}: " + component.ToJSONString());
			m_Log.LogObject("Prefab", prefab);
			m_Log.LogObject("Prefab.m_Material", prefab.m_Material);
			m_Log.Info("");

		}
	}
}
