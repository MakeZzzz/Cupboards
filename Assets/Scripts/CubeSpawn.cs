using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class CubeSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField]private FileReading gameСonditions;
    
    private List<Color> _colors = new List<Color>();
    private List<GameObject> _cubes  = new List<GameObject>();
    
    void Start()
    {
        StartCubesSpawn();
    }

    private void SetColor(ref GameObject cube)
    {
        var tempcolor = Random.ColorHSV();
        foreach (var color in _colors)
        {
            while(tempcolor.r == color.r && tempcolor.b == color.b && tempcolor.g == color.g)
            {
                tempcolor = Random.ColorHSV();
            }
        }
        _colors.Add(tempcolor);
        cube.GetComponent<Renderer>().material.color = tempcolor;
    }

    private void  StartCubesSpawn()
    {
        for (int i = 0; i < gameСonditions.cubesCount; i++)
        {
            var newCube = Instantiate(_cube);
            newCube.AddComponent<SortingGroup>();
            var layer = newCube.GetComponent<SortingGroup>();
            newCube.AddComponent<BoxCollider2D>();
            layer.sortingOrder = 3;
            SetColor(ref newCube);
            var positionNumber = gameСonditions.startGameState[i];
            newCube.transform.position = new  Vector3(gameСonditions.positions[positionNumber-1][0], gameСonditions.positions[positionNumber-1][1],1f);
            _cubes.Add(newCube);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
