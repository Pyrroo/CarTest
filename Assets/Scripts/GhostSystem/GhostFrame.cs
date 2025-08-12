using UnityEngine;

[System.Serializable]
public class GhostFrame 
{
    public Vector3 position;
    public Quaternion rotation;
    public float time;
}

// Сериализуем для возможности записи в JSON в будущем и сохранения нескольких записей
