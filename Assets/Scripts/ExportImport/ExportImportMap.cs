using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExportImportMap : MonoBehaviour
{
    // Refer�ncias para os gizmos de movimento, rota��o e escala
    public GameObject mMovementGizmo;
    public GameObject mRotationGizmo;
    public GameObject mScaleGizmo;

    // Array que cont�m os nomes dos tipos de objetos
    public string[] mObjectNames;

    // M�todo para exportar os dados do mapa para um arquivo JSON
    public void ExportMap()
    {
        // Re�ne os dados do mapa atual
        MapData vData = GatherMapData();

        // Converte os dados do mapa para uma string JSON
        string vJson = JsonUtility.ToJson(vData);

        // Salva a string JSON num arquivo
        SaveToFile(Application.dataPath + "/mapData.json", vJson);

        // Exibe o caminho do arquivo salvo na consola
        Debug.Log(Application.dataPath + "/mapData.json");
    }

    // M�todo para guardar um string JSON num arquivo
    private void SaveToFile(string vFilePath, string vJson)
    {
        File.WriteAllText(vFilePath, vJson);
    }

    // M�todo para reunir os dados do mapa atual
    private MapData GatherMapData()
    {
        // Cria uma nova inst�ncia da MapData para armazenar os dados do mapa
        MapData vData = new MapData();

        // Encontra todos os objetos do tipo ObjectManager na cena
        PrefabManager[] vObjectList = GameObject.FindObjectsOfType<PrefabManager>();

        // Cria uma lista para armazenar os GameObjects encontrados
        List<GameObject> vLevelObjects = new List<GameObject>();

        // Adiciona cada objeto encontrado � lista
        foreach (var vObjectInScene in vObjectList)
        {
            vLevelObjects.Add(vObjectInScene.transform.gameObject);
        }

        // Converte a lista para um array de GameObjects
        GameObject[] vAllGameObjects = vLevelObjects.ToArray();

        // Inicializa os arrays em MapData com base no n�mero de objetos encontrados
        vData.listSize = vAllGameObjects.Length;
        vData.types = new int[vData.listSize];
        vData.positions = new Vector3[vData.listSize];
        vData.scales = new Vector3[vData.listSize];
        vData.rotations = new Quaternion[vData.listSize];
        vData.meshes = new Mesh[vData.listSize];
        vData.names = new string[vData.listSize];

        // Preenche os arrays com os dados dos objetos encontrados
        for (int i = 0; i < vData.listSize; i++)
        {
            vData.types[i] = GetObjectTypeIndex(vAllGameObjects[i]);
            vData.positions[i] = vAllGameObjects[i].transform.position;
            vData.scales[i] = vAllGameObjects[i].transform.localScale;
            vData.rotations[i] = vAllGameObjects[i].transform.localRotation;
            vData.meshes[i] = vAllGameObjects[i].transform.GetComponent<MeshFilter>().sharedMesh;
            vData.names[i] = vAllGameObjects[i].transform.gameObject.name;
        }
        return vData;
    }

    // M�todo para determinar o �ndice do tipo de objeto com base no nome
    private int GetObjectTypeIndex(GameObject vGameObject)
    {
        for (int i = 0; i < mObjectNames.Length; i++)
        {
            if (vGameObject.name.Contains(mObjectNames[i]))
            {
                return i + 1;
            }
        }
        return 0;
    }





    // M�todo para carregar os dados do mapa a partir de um arquivo JSON
    public void ImportMap()
    {
        // L� o conte�do do arquivo JSON
        string vJson = LoadFromFile(Application.dataPath + "/mapData.json");

        // Converte a string JSON para um objeto MapData
        MapData vData = JsonUtility.FromJson<MapData>(vJson);

        // Instancia os objetos do mapa na cena com base nos dados carregados
        InstantiateMapObjects(vData);
    }

    // M�todo para ler uma string JSON de um arquivo
    private string LoadFromFile(string vFilePath)
    {
        return File.ReadAllText(vFilePath);
    }

    // M�todo para instanciar objetos na cena com base nos dados carregados
    private void InstantiateMapObjects(MapData vData)
    {
        for (int i = 0; i < vData.listSize; i++)
        {
            // Cria um novo GameObject
            GameObject vObjToSpawn = new GameObject();

            // Define a posi��o, rota��o, escala e nome do GameObject
            vObjToSpawn.transform.position = vData.positions[i];
            vObjToSpawn.transform.rotation = vData.rotations[i];
            vObjToSpawn.transform.localScale = vData.scales[i];
            vObjToSpawn.transform.tag = "Selectable";
            vObjToSpawn.name = vData.names[i];

            // Adiciona os componentes necess�rios ao GameObject
            vObjToSpawn.AddComponent<MeshFilter>();
            vObjToSpawn.AddComponent<MeshRenderer>();
            vObjToSpawn.AddComponent<BoxCollider>();
            vObjToSpawn.AddComponent<PrefabManager>();

            // Configura a mesh e o material do GameObject
            vObjToSpawn.GetComponent<MeshFilter>().sharedMesh = vData.meshes[i];
            vObjToSpawn.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
        }
    }
}