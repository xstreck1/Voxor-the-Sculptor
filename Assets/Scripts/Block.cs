using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
    public GameObject sourceCube = null;

    public float CubeSize { get { return sourceCube.transform.lossyScale.x; } }

    public int blockSide = 10;

    public float height = 1f;

    void Start()
    {
        sourceCube.SetActive(false);
        InstantiateBlockElements();
    }

    private void Update()
    {
        if (LeftController.MenuPressed && RightController.MenuPressed)
        {
            Reset();
        }
    }

    public Vector3 IDToPosition(int ID)
    {
        return new Vector3(ID % blockSide, (ID % (blockSide * blockSide)) / blockSide, ID / (blockSide * blockSide));
    }

    public int PoisitionToID(Vector3 position)
    {
        if (position.x < 0 || position.y < 0 || position.z < 0 || position.x >= blockSide || position.y >= blockSide || position.z >= blockSide)
        {
            return -1;
        }
        return (int)(Mathf.Round((position.x) + (position.y * blockSide) + (position.z * blockSide * blockSide)));
    }

    static public Vector3 RoundVector(Vector3 source)
    {
        return new Vector3(Mathf.Round(source.x), Mathf.Round(source.y), Mathf.Round(source.z));
    }

    public Vector3 ClosestLogical(Vector3 originalPos)
    {
        return RoundVector(PhysicalToLogical(originalPos));
    }

    public Vector3 LogicalToPhysical(Vector3 logicalPos)
    {
        return logicalPos * CubeSize + (Vector3.up * height) - (Vector3.one * CubeSize * blockSide * .5f);
    }

    public Vector3 PhysicalToLogical(Vector3 physicalPos)
    {
        return (physicalPos - (Vector3.up * height) + (Vector3.one * CubeSize * blockSide * .5f)) / CubeSize;
    }

    public bool IsBoundary(int ID)
    {
        Vector3 pos = IDToPosition(ID);
        return pos.x == 0 || pos.y == 0 || pos.z == 0 || pos.x + 1 == blockSide || pos.y + 1 == blockSide || pos.z + 1 == blockSide;
    }

    public GameObject IDToObject(int ID)
    {
        if (ID >= 0 && ID < blockSide * blockSide * blockSide)
        {
            return transform.GetChild(ID).gameObject;
        }
        else
        {
            return null;
        }
    }

    public void InstantiateBlockElements()
    {
        for (int ID = 0; ID < blockSide * blockSide * blockSide; ID++)
        {
            GameObject newCube = Instantiate(sourceCube, transform) as GameObject;
            newCube.name = ID.ToString();
            newCube.SetActive(true);
            newCube.transform.position = LogicalToPhysical(IDToPosition(ID));
        }
    }

    public void Reset()
    {
        for (int ID = 0; ID < blockSide * blockSide * blockSide; ID++)
        {
            GameObject cube = IDToObject(ID);
            cube.SetActive(true);
            cube.GetComponent<Renderer>().material.color = Color.gray;
            cube.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        }
    }
}
