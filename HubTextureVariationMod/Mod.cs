using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Input;
using Game.Modding;
using Game.SceneFlow;
using UnityEngine;

namespace HubTextureVariationMod
{
	public class Mod : IMod
	{

#if DEBUG
		public static ILog log = LogManager.GetLogger($"{nameof(HubTextureVariationMod)}.{nameof(Mod)}").SetShowsErrorsInUI(true);
#else
		public static ILog log = LogManager.GetLogger($"{nameof(HubTextureVariationMod)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
#endif
		public void OnLoad(UpdateSystem updateSystem)
		{
			log.Info(nameof(OnLoad));

			if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
				log.Info($"Current mod asset at {asset.path}");


			updateSystem.UpdateAfter<AreaVisitor>(SystemUpdatePhase.GameSimulation);
		}

		public void OnDispose()
		{
		}
	}
}
