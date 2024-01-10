using GameNetcodeStuff;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace EZCompanyMod.GameInterface
{
    internal class ItemManager
    {
		internal static void SpawnObject(GameObject prefab, Vector3 p)
		{
			GameObject gameObject = Object.Instantiate(prefab, p, Quaternion.identity, DataStore.playerManager.propsContainer);
			spawnedObjects.Add(gameObject);
			NetworkObject component = gameObject.GetComponent<NetworkObject>();
			if (component != null)
			{
				component.Spawn(false);
			}
		}

		internal static void DespawnObjects()
		{
			foreach (GameObject gameObject in spawnedObjects)
			{
				NetworkObject component = gameObject.GetComponent<NetworkObject>();
				if (component != null)
				{
					component.Despawn(true);
				}
				else
				{
					Object.Destroy(gameObject);
				}
			}

			spawnedObjects.Clear();
		}

		internal static GameObject SpawnItem(string name, Vector3 p)
		{
			DataStore.itemNames.TryGetValue(name, out Item itemData);

			if (itemData == null)
				return null;

			PlayerControllerB player = DataStore.mainController;

			GameObject gameObject = Object.Instantiate(itemData.spawnPrefab, p, Quaternion.identity, DataStore.playerManager.propsContainer);
			GrabbableObject component = gameObject.GetComponent<GrabbableObject>();

			component.fallTime = 0f;
			gameObject.GetComponent<NetworkObject>().Spawn(false);

			NetworkObject networkObject = component.NetworkObject;
			if (networkObject != null && networkObject.IsSpawned)
			{
				spawnedItems.Add(gameObject);

				return gameObject;
			}

			Object.Destroy(gameObject);

			return null;
		}

		internal static GameObject[] SpawnScrap(string name, Vector3 p, int amount = 1, int value = -1)
		{
			DataStore.scrapNames.TryGetValue(name, out SpawnableItemWithRarity scrapData);

			if (scrapData == null)
				return null;

			PlayerControllerB player = DataStore.mainController;

			List<GameObject> objects = new List<GameObject> { };
			List<NetworkObjectReference> networkReferences = new List<NetworkObjectReference> { };
			List<int> scrapValues = new List<int> { };

			for (int i = 0; i < amount; i++)
            {
				GameObject gameObject = Object.Instantiate(scrapData.spawnableItem.spawnPrefab, p, Quaternion.identity, DataStore.playerManager.propsContainer);
				GrabbableObject component = gameObject.GetComponent<GrabbableObject>();
				NetworkObject networkObject = component.NetworkObject;

				component.fallTime = 0f;

				if (networkObject != null)
				{
					networkObject.Spawn(false);

					System.Random anomalyRandom = DataStore.roundManager.AnomalyRandom;

					if (anomalyRandom == null)
					{
						DataStore.roundManager.InitializeRandomNumberGenerators();
						anomalyRandom = DataStore.roundManager.AnomalyRandom;
					}

					if (networkObject.IsSpawned)
					{
						int scrapValue = value <= -1 ? (int)((float)anomalyRandom.Next(scrapData.spawnableItem.minValue, scrapData.spawnableItem.maxValue) * DataStore.roundManager.scrapValueMultiplier) : value;

						component.SetScrapValue(scrapValue);

						spawnedScrap.Add(gameObject);

						objects.Add(gameObject);
						networkReferences.Add(networkObject);
						scrapValues.Add(scrapValue);
					}
					else
						Object.Destroy(gameObject);
				}
				else
					Object.Destroy(gameObject);
			}

			if (objects.Count >= 1)
			{
				DataStore.roundManager.SyncScrapValuesClientRpc(networkReferences.ToArray(), scrapValues.ToArray());

				return objects.ToArray();
			}

			return null;
		}

		internal static GameObject PickupItem(GameObject item)
		{
			PlayerControllerB player = DataStore.mainController;

			GrabbableObject component = item.GetComponent<GrabbableObject>();

			Helpers.AccessHelper.SetProperty(player, "currentlyGrabbingObject", component);
			Helpers.AccessHelper.SetProperty(player, "grabInvalidated", false);

			int num = Helpers.AccessHelper.CallFunc<int>(player, "FirstEmptyItemSlot", null);
			if (player.inSpecialInteractAnimation || num == -1)
				return null;

			NetworkObject networkObject = component.NetworkObject;
			if (networkObject != null && networkObject.IsSpawned)
			{
				component.InteractItem();

				player.playerBodyAnimator.SetBool("GrabInvalidated", false);
				player.playerBodyAnimator.SetBool("GrabValidated", false);
				player.playerBodyAnimator.SetBool("cancelHolding", false);
				player.playerBodyAnimator.ResetTrigger("Throw");
				Helpers.AccessHelper.CallFunc(player, "SetSpecialGrabAnimationBool", new object[2] { true, null });

				player.isGrabbingObjectAnimation = true;
				player.cursorIcon.enabled = false;
				player.cursorTip.text = "";
				player.twoHanded = component.itemProperties.twoHanded;
				player.carryWeight += Mathf.Clamp(component.itemProperties.weight - 1f, 0f, 10f);

				if (component.itemProperties.grabAnimationTime > 0f)
				{
					player.grabObjectAnimationTime = component.itemProperties.grabAnimationTime;
				}
				else
				{
					player.grabObjectAnimationTime = 0.4f;
				}

				Helpers.AccessHelper.CallFunc(player, "GrabObjectServerRpc", new NetworkObjectReference(networkObject));

				Coroutine coroutine = (Coroutine)Helpers.AccessHelper.GetProperty(player, "grabObjectCoroutine");
				if (coroutine != null)
				{
					player.StopCoroutine(coroutine);
				}

				Helpers.AccessHelper.SetProperty(player, "grabObjectCoroutine", player.StartCoroutine("GrabObject"));

				spawnedItems.Add(item);

				return item;
			}

			return null;
		}

		internal static GameObject PickupItem(GameObject[] items)
		{
			if (items.Length >= 1)
				return PickupItem(items[0]);

			return null;
		}

		internal static List<GameObject> spawnedObjects = new List<GameObject>();

		internal static List<GameObject> spawnedItems = new List<GameObject>();

		internal static List<GameObject> spawnedScrap = new List<GameObject>();
	}
}
