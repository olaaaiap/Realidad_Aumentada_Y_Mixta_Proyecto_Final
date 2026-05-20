
//using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] public Dictionary<string, List<GameObject>> pool; //Pool donde se guardarán los objetos reciclables
    public Transform poolParent; //El padre del pool, necesario para gestionarlo


    public void Awake()
    {
        //Inicializar el pool
        this.pool = new Dictionary<string, List<GameObject>>();
        //Inicializar pool parent
        this.poolParent = GetComponent<Transform>();
    }

    //Carga inicial de un objeto en el pool
    public void Load(GameObject prefab, int quantity = 1)
    {
        //Obtener el nombre del objeto a insertar en el pool
        var goName = prefab.name;
        //Si el pool no contiene ningun objeto con ese nombre, crear una lista de game objects
        if (!pool.ContainsKey(goName))
        {
            pool[goName] = new List<GameObject>();
        }

        //Para cada objeto a insertar, instanciar el prefab y añadir al pool
        for (int i = 0; i < quantity; i++)
        {
            var go = Instantiate(prefab);
            go.name = goName;
            go.transform.SetParent(poolParent, false);
            go.SetActive(false);
            pool[go.name].Add(go);
        }
    }

    //Sacar un objeto del pool
    public GameObject Spawn(GameObject prefab)
    {
        //Si no existe ningun objeto en el pool con ese nombre, añadir 1 al pool
        if (!pool.ContainsKey(prefab.name) || pool[prefab.name].Count == 0)
        {
            Load(prefab, 1);
        }
        // Obtener game object y quitarlo del pool para enseñarlo en la escena
        var l = pool[prefab.name];
        var go = l[0];
        l.RemoveAt(0);
        if (go)
        {
            go.SetActive(true);
            go.transform.SetParent(null, false);
            //Si el objeto a spawnaer utiliza la interfaz ISpawnable, llamar a su función OnSpawn
            foreach (var spawnable in go.GetComponents<ISpawnable>())
            {
                if (spawnable != null)
                    spawnable.OnSpawn();
            }
        }
        //Devolver el game objext spawneado
        return go;
    }

    //Para devolver un objeto al pool
    public void Despawn(GameObject go)
    {
        //Si el pool no tiene ningun elemento con ese nombre, crear una nueva lista
        if (!pool.ContainsKey(go.name))
        {

            //pool[go.name] = new List<GameObject>();
            Destroy(go);
            return;
        }

        //Añadire el objeto al pool y desactivarlo de la escena
        var objectList = pool[go.name];
        go.SetActive(false);
        go.transform.SetParent(poolParent, false);
        pool[go.name].Add(go);
    }
}
