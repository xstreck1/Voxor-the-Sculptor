using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    public GameObject sourceCube = null;

    public float CubeSize { get { return sourceCube.transform.lossyScale.x; } }

    public int blockSide = 10;

    public float height = 1f;

    void Start()
    {
        sourceCube.SetActive(false);
        InstantiateBlockElements();
    }

    public Vector3 IDToPosition(int ID)
    {
        return new Vector3(ID % blockSide, (ID % (blockSide * blockSide))/ blockSide, ID / (blockSide * blockSide) );
    }

    public int PoisitionToID(Vector3 position)
    {
        if (position.x < 0 || position.y < 0 || position.z < 0 || position.x >= blockSide || position.y >= blockSide || position.z >= blockSide)
        {
            return -1;
        }
        return (int) (Mathf.Round((position.x) + (position.y * blockSide) + (position.z * blockSide * blockSide)));
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
        return logicalPos * CubeSize + (Vector3.up * height) - (Vector3.one * CubeSize * blockSide *.5f);
    }

    public Vector3 PhysicalToLogical(Vector3 physicalPos)
    {
        return (physicalPos - (Vector3.up * height) + (Vector3.one * CubeSize * blockSide * .5f)) / CubeSize;
    }

    public GameObject IDToObject(int ID)
    {
        if (ID >= 0 && ID < blockSide* blockSide* blockSide)
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

        for (int z = 0; z < blockSide; z++)
        {
            GameObject z_group = new GameObject();
            z_group.name = "Z" + z;
            z_group.transform.parent = transform; 
            for (int y = 0; y < blockSide; y++)
            {
                GameObject y_group = new GameObject();
                y_group.name = "Y" + y;
                y_group.transform.parent = z_group.transform;
                for (int x = 0; x < blockSide; x++)
                {
                    int ID = x + y * blockSide + z * blockSide * blockSide;
                    GameObject newCube = Instantiate(sourceCube, y_group.transform) as GameObject;
                    newCube.name = "X" + x;
                    newCube.SetActive(true);
                    newCube.transform.position = LogicalToPhysical(IDToPosition(ID));
                }
            }
        }
    }
}
