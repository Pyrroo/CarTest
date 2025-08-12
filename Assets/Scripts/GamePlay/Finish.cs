using UnityEngine;
using System;
public class Finish : MonoBehaviour
{
    public static event Action Finished;


    private void OnTriggerEnter(Collider other)
    {
        Finished?.Invoke();
        GameController.isMenu = true;
    }
}
