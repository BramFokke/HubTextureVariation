using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colossal.Entities;
using Colossal.Logging;
using Game;
using Game.Prefabs;
using Unity.Collections;
using Unity.Entities;

namespace HubTextureVariationMod
{
	public abstract partial class PrefabVisitor<TPrefab> : GameSystemBase where TPrefab: PrefabBase
	{

		protected ILog m_Log;
		private PrefabSystem m_PrefabSystem;
		private EntityQuery m_PrefabQuery;
		
		protected override void OnCreate()
		{
			base.OnCreate();
			m_Log = Mod.log;
			m_PrefabSystem = World.DefaultGameObjectInjectionWorld?.GetOrCreateSystemManaged<PrefabSystem>();
			m_PrefabQuery = GetEntityQuery(ComponentType.ReadOnly<PrefabData>());

			RequireForUpdate(m_PrefabQuery);
		}

		protected override void OnUpdate()
		{
			var startTime = DateTime.Now;
			NativeArray<Entity> prefabEntities = m_PrefabQuery.ToEntityArray(Allocator.Temp);
			int count_e = prefabEntities.Count();
			int i = 1;
			m_Log.Info($"{count_e} items found");
			foreach (Entity entity in prefabEntities)
			{
				PrefabData component;
				TPrefab prefabBase;
				if (!EntityManager.TryGetComponent<PrefabData>(entity, out component) || !m_PrefabSystem.TryGetPrefab<TPrefab>(component, out prefabBase))
					return;
				if (prefabBase != null)
				{
					try
					{
						Visit(entity, component, prefabBase);
					}
					catch (Exception e)
					{
						m_Log.Info($"ERROR: Item {i} - {e.ToString()}");
					}
				}
				i++;
			}
			prefabEntities.Dispose();
			Enabled = false;
			var loadTime = DateTime.Now - startTime;
			m_Log.Info("Dump Time: " + loadTime.TotalSeconds + "s");
		}


		protected abstract void Visit(Entity entity, PrefabData component, TPrefab prefab);
	}
}
