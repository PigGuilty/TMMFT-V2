using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSpawner : NetworkBehaviour {

	private NetworkManager netManager;
	
	void Start(){
		netManager = NetworkManager.singleton;
	}
	
	public void Spawn(GameObject obj, Vector3 objPos, Quaternion objRot, int destroy, string args){
		CmdSpawn(obj.name, objPos, objRot, destroy, args);
		/*GameObject item = GameObject.FindWithTag("isLP");
		TextMesh text = item.GetComponent<TextMesh>();
		text.text = args;*/
	}
	
	[Command]
	void CmdSpawn(string objName, Vector3 objPos, Quaternion objRot, int destroy, string args){
		GameObject findObject = null;
		
		if(objName.EndsWith("(Clone)")){
			objName = objName.Substring(0, objName.Length - 7);
		}
		
        for (int i = 0; i < netManager.spawnPrefabs.Count; i++)
        {
            if (netManager.spawnPrefabs[i].name.Equals(objName))
            {
                findObject = netManager.spawnPrefabs[i];
                break;
            }
        }
 
        if (findObject != null)
        {
			GameObject apparitionCamToFollow = null;
			GameObject parent = null;
            if(args.StartsWith("apparition")){
				NetworkInstanceId netid = new NetworkInstanceId(UInt32.Parse(args.Split(':')[1]));
				apparitionCamToFollow = NetworkServer.FindLocalObject(netid).GetComponentInChildren<Camera>().gameObject;
			}else if(args.StartsWith("impact")){
				parent = GameObject.Find(args.Split(':')[2]);
			}
			
			GameObject spawnObject = Instantiate(findObject, objPos, objRot) as GameObject;
			
			if(args.StartsWith("apparition:")){
				spawnObject.GetComponent<Apparition>().camToFollow = apparitionCamToFollow;
			}else if(args.StartsWith("apparitionFAP:")){
				spawnObject.GetComponent<Apparitiondouille>().camToFollow = apparitionCamToFollow;
			}else if(args.StartsWith("apparitionP_M:")){
				spawnObject.GetComponent<ApparitionP_M>().camToFollow = apparitionCamToFollow;
			}else if(args.StartsWith("impact")){
				string vect = args.Split(':')[1];
				Vector3 norm = new Vector3(float.Parse(vect.Split(',')[0].Substring(1)), float.Parse(vect.Split(',')[1].Substring(1)), float.Parse(vect.Split(',')[2].Substring(1, 1)));
				
				GameObject item = GameObject.FindWithTag("isLP");
				TextMesh text = item.GetComponent<TextMesh>();
				text.text = args;
				
				spawnObject.transform.Translate(norm / 1000, Space.World);
				spawnObject.transform.parent = parent.transform;
				text.text = text.text + "\n" + parent;
			}
			
            NetworkServer.Spawn(spawnObject);
			if(parent != null)
				RpcSyncImpact(spawnObject, GetGameObjectPath(parent));
			
			if(destroy != -1){
				Destroy(spawnObject, destroy);
			}
			
        }
	}
	
	[ClientRpc]
	public void RpcSyncImpact(GameObject obj, string parentPath){
		GameObject parent = GameObject.Find(parentPath);
		if(obj != null && parent != null)
			obj.transform.parent = parent.transform;
		else if(obj != null)
			Destroy(obj);
		else
			print("undefined 1");
	}
	
	[ClientRpc]
	public void RpcSyncHandParent(GameObject obj, int parentId){
		Transform parent = null;
		if(isServer){
			parent = NetworkServer.FindLocalObject(new NetworkInstanceId(((uint) (int) parentId))).transform.Find("FirstPersonCharacter").Find("Place Pour Objet");
		}else
			parent = ClientScene.FindLocalObject(new NetworkInstanceId(((uint) (int) parentId))).transform.Find("FirstPersonCharacter").Find("Place Pour Objet");
		if(obj != null && parent != null){
			obj.transform.parent = parent.transform;
			obj.GetComponent<Rigidbody>().isKinematic = true;
		}else
			print("undefined 2");
	}
	
	[ClientRpc]
	public void RpcObjectSend(GameObject obj, Vector3 force){
		Rigidbody rigidbodyDeLObjetPris = obj.GetComponent<Rigidbody>();
		Collider ColliderDeLobjetPris = obj.GetComponent<Collider>();
		
		rigidbodyDeLObjetPris.isKinematic = false;
		obj.transform.parent = null;
		ColliderDeLobjetPris.enabled = true;
		
		rigidbodyDeLObjetPris.AddForce(force);
	}
	
	[ClientRpc]
	public void RpcShoot(Vector3 pos, Vector3 lookDir, float DegatArme){
		RaycastHit hit;
		Ray ShootingDirection = new Ray(pos, lookDir);
		
		if (Physics.Raycast(ShootingDirection, out hit))
		{

			Target target = hit.transform.GetComponent<Target>();

			if (hit.collider.tag == "Decor interractif")
			{
				hit.transform.SendMessage("HitByRaycast", SendMessageOptions.DontRequireReceiver);
			}

			if (hit.collider.tag == "Vache")
			{
				target.TakeDamage(DegatArme,true);


				if (hit.rigidbody != null)
				{
					hit.rigidbody.AddForce(-hit.normal * DegatArme * 4);
				}
			}
		}
	}
	
	[Command]
	public void CmdSyncHandParent(string objPath, int parentId){
		GameObject obj = GameObject.Find(objPath);
		
		Transform parent = NetworkServer.FindLocalObject(new NetworkInstanceId(((uint) (int) parentId))).transform.Find("FirstPersonCharacter").Find("Place Pour Objet");
		if(obj != null && parent != null){
			obj.transform.parent = parent.transform;
			obj.GetComponent<Rigidbody>().isKinematic = true;
			RpcSyncHandParent(obj, parentId);
		}else
			print("undefined 3");
	}
	
	[Command]
	public void CmdObjectSend(string objPath, Vector3 force){
		print(objPath);
		GameObject ObjectQuiEstPris = GameObject.Find(objPath);
		print(ObjectQuiEstPris);
		Collider ColliderDeLobjetPris = ObjectQuiEstPris.GetComponent<Collider>();
		Rigidbody rigidbodyDeLObjetPris = ObjectQuiEstPris.GetComponent<Rigidbody>();
		
		rigidbodyDeLObjetPris.isKinematic = false;
		ObjectQuiEstPris.transform.parent = null;
		ColliderDeLobjetPris.enabled = true;
		
		RpcObjectSend(ObjectQuiEstPris, force);
		rigidbodyDeLObjetPris.AddForce(force);
	}
	
	[Command]
	public void CmdShoot(Vector3 pos, Vector3 lookDir, float DegatArme){
		RaycastHit hit;
		Ray ShootingDirection = new Ray(pos, lookDir);
		
		if (Physics.Raycast(ShootingDirection, out hit))
		{

			Target target = hit.transform.GetComponent<Target>();

			if (hit.collider.tag == "Decor interractif")
			{
				hit.transform.SendMessage("HitByRaycast", SendMessageOptions.DontRequireReceiver);
			}

			if (hit.collider.tag == "Vache")
			{
				target.TakeDamage(DegatArme,true);


				if (hit.rigidbody != null)
				{
					hit.rigidbody.AddForce(-hit.normal * DegatArme * 4);
				}
			}
		}
		RpcShoot(pos, lookDir, DegatArme);
	}
	
	public static string GetGameObjectPath(GameObject obj)
	{
		string path = "/" + obj.name;
		while (obj.transform.parent != null)
		{
			obj = obj.transform.parent.gameObject;
			path = "/" + obj.name + path;
		}
		return path;
	}
}
