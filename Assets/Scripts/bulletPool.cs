using UnityEngine;
using System.Collections.Generic;

// made with help from https://unity3d.com/de/learn/tutorials/topics/scripting/object-pooling
public class BulletPool : MonoBehaviour{

    const int DEFAULT_POOL_SIZE = 50;

    class Pool {

        int nextId = 1;

        Stack<GameObject> inactive; //Stack oder List? Thereza fragen

        GameObject prefab;

        public Pool(GameObject prefab, int initialQuantity)
        {
            this.prefab = prefab;
            inactive = new Stack<GameObject>(initialQuantity);
        }

        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            GameObject obj;
            if (inactive.Count == 0)
            {
                obj = (GameObject)GameObject.Instantiate(prefab, position, rotation);
                obj.name = prefab.name + " (" + (nextId++) + ")";

                obj.AddComponent<PoolMember>().myPool = this;
            }
            else
            {
                obj = inactive.Pop();

                if (obj == null)
                {
                    return Spawn(position, rotation);
                }
            }

            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;

        }


        public void Despawn(GameObject obj)
        {
            obj.SetActive(false);
            inactive.Push(obj);
        }

    }


    class PoolMember : MonoBehaviour
    {
        public Pool myPool;
    }

    static Dictionary<GameObject, Pool> pools;


    static void Init(GameObject prefab = null, int qty = DEFAULT_POOL_SIZE)
    {
        if (pools == null)
        {
            pools = new Dictionary<GameObject, Pool>();
        }
        if (prefab != null && pools.ContainsKey(prefab) == false)
        {
            pools[prefab] = new Pool(prefab, qty);
        }
    }

    
    static public void Preload(GameObject prefab, int qty = 1) //für Prespawning - needed?
    {
        Init(prefab, qty);

        GameObject[] obs = new GameObject[qty];
        for (int i = 0; i < qty; i++)
        {
            obs[i] = Spawn(prefab, Vector3.zero, Quaternion.identity);
        }

        for (int i = 0; i < qty; i++)
        {
            Despawn(obs[i]);
        }
    }


    static public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        Init(prefab);

        return pools[prefab].Spawn(pos, rot);
    }


    static public void Despawn(GameObject obj)
    {
        PoolMember pm = obj.GetComponent<PoolMember>();
        if (pm == null)
        {
            Debug.Log("Object '" + obj.name + "' wasn't spawned from a pool. Destroying it instead.");
            GameObject.Destroy(obj);
        }
        else
        {
            pm.myPool.Despawn(obj);
        }
    }

}