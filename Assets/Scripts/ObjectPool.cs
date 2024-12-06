using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public GameObject Prefab;
    public int InitialObjectNumber = 30;
    List<GameObject> objs;
    private static ObjectPool instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ù ��° �ν��Ͻ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� ������ ������Ʈ ����
            return;
        }
    }
    void Start()
    {
        objs = new List<GameObject>();

        for(int i = 0; i<InitialObjectNumber; i++)
        {
            GameObject go = Instantiate(Prefab, transform);
            go.SetActive(false);
            objs.Add(go);
        }
    }

    public GameObject GetObject()
    {
        foreach(GameObject go in objs)
        {
            if(!go.activeSelf)
            {
                go.SetActive(true);
                return go;
            }
        }
        GameObject obj = Instantiate(Prefab, transform);
        objs.Add(obj);
        return obj;
    } 

}
