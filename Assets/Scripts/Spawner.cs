using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _square;
    // string Path = "F:/Program Files/Unity projects/Cupboards/Assets/Input/Input.txt";
    private List<GameObject> Squares;
    private int[] startGameState;
    private int[] endGameState;
    private string[] lines;
    private int[][] positions;
    private int[][] _netlist;
    private int _connectionsCount;
    private string[] position = new string[2];
    char delimiterChars = ',';
    void Start()
    {
        parseInputFile("F:/Program Files/Unity projects/Cupboards/Assets/Input/Input.txt");
    }

    private void parseInputFile(string path)
    {
        int k = 0;

        lines = File.ReadAllLines(path);
        int _quaresCount = Convert.ToInt32(lines[0]);
        int _positionsCount = Convert.ToInt32(lines[1]);

        parseCoordinates(2, _positionsCount + 2, ref positions, _positionsCount);
        createSquares(ref positions);

        parseGameStates(ref startGameState, _quaresCount, _positionsCount);
        parseGameStates(ref endGameState, _quaresCount, _positionsCount+1);
        
        _connectionsCount = Convert.ToInt32(lines[_positionsCount + 4]);
        parseCoordinates(_positionsCount + 5, lines.Length, ref _netlist, lines.Length - (_positionsCount + 5));
    }
    private void createSquares(ref int[][] positions)
    {
        foreach (var nextCoordinate in positions)
        {
            var square = Instantiate(_square);
            square.transform.position = new Vector2( nextCoordinate[0], nextCoordinate[1]);
        }
    }

    private void parseGameStates(ref int[] parsed, int _quaresCount, int _positionsCount)
    {
        string[] temp = new string[_quaresCount];
        parsed = new int[_quaresCount];
        temp = lines[_positionsCount + 2].Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < temp.Length; i++)
        {
            parsed[i] = Convert.ToInt32(temp[i]);
        }
    }
    
    private void parseCoordinates(int from, int to, ref int[][] parsed, int arrSize)
    {
        int k = 0;
        parsed = new int[arrSize][];
        for (int i = 0; i < arrSize; i++)
        {
            parsed[i] = new int[2];
        }
        
        for (int i = from; i < to; i++)
        {
            position =  lines[i].Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
            int _xCoordinate = Convert.ToInt32(position[0]);
            int _yCoordinate = Convert.ToInt32(position[1]);
            parsed[k][0] = _xCoordinate;
            parsed[k][1] = _yCoordinate;
            k++;
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
