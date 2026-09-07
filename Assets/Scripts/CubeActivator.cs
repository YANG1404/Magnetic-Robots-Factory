using UnityEngine;

public class CubeActivator : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;

    public void ChangeCubeState(bool isGreen)
    {
        if (isGreen)
        {
            cube1.SetActive(!cube1.activeSelf);
        }
        else
        {
            cube2.SetActive(!cube2.activeSelf);
        }

    }
}
