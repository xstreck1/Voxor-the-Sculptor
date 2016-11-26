using UnityEngine;
using System.Collections;

public class CreateBlock : MonoBehaviour {
    public GameObject sourceCube = null;

    public float cubeSize = 0.1f;

    public int blockSize = 10;

    public float height = 1f;

    void Start()
    {
        InstantiateBlockElements();
    }

    Vector3 IDToPosition(int ID)
    {
        return new Vector3(ID / (blockSize * blockSize), ID / (blockSize) % blockSize, ID % blockSize);
    }

    int PoisitionToID(Vector3 position)
    {
        return (int) ((position.x * blockSize * blockSize) + (position.y * blockSize) + position.z);
    }

    Vector3 LogicalToPhysical(Vector3 position)
    {
        return position * cubeSize + Vector3.up * height - (Vector3.one * cubeSize * blockSize *.5f);
    }

    Vector3 PhysicalToLogical(Vector3 position)
    {
        return (position - Vector3.up * height + (Vector3.one * cubeSize * blockSize * .5f)) / cubeSize;
    }


    public void InstantiateBlockElements()
    {
        GameObject block = new GameObject();
        block.name = "Block";

        for (int i = 0; i < blockSize * blockSize * blockSize; i++)
        {
            GameObject newCube = GameObject.Instantiate(sourceCube, block.transform) as GameObject;
            newCube.SetActive(true);
            newCube.transform.position = LogicalToPhysical(IDToPosition(i));
        }
    }
}
