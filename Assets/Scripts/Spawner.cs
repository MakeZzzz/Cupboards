using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject _square;
    [SerializeField] private Material _material;
    
    public Color c1 = Color.white;
    public Color c2 = Color.white;
    public int[] startGameState;
    public int[][] positions;
    public int cubesCount;
    
    private LineRenderer lr;
    private List<GameObject> LineRenderers;
    private int[] endGameState;
    private string[] lines;
    private int[][] _netlist;
    private int _connectionsCount;
    private string[] position = new string[2];
    
    char delimiterChars = ',';

    void Awake()
    {
        parseInputFile("F:/Program Files/Unity projects/Cupboards/Assets/Input/Input.txt");
        CreateLineRenderers();
        camera.transform.position = new Vector3(200, 200, 0);
        LinesCreation(ref LineRenderers);
    }
    
    private void parseInputFile(string path)
    {
        int k = 0;

        lines = File.ReadAllLines(path);
        cubesCount = Convert.ToInt32(lines[0]);
        int _positionsCount = Convert.ToInt32(lines[1]);

        parseCoordinates(2, _positionsCount + 2, ref positions, _positionsCount);
        createSquares(ref positions);

        parseGameStates(ref startGameState, cubesCount, _positionsCount);
        parseGameStates(ref endGameState, cubesCount, _positionsCount+1);
        
        _connectionsCount = Convert.ToInt32(lines[_positionsCount + 4]);
        parseCoordinates(_positionsCount + 5, lines.Length, ref _netlist, lines.Length - (_positionsCount + 5));
    }
    private void createSquares(ref int[][] positions)
    {
        foreach (var nextCoordinate in positions)
        {
            var square = Instantiate(_square);
            square.transform.position = new Vector3( nextCoordinate[0], nextCoordinate[1],0f);
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
    
    
    private void ChangeLineColor (ref GameObject LineRenderedObj)
    {
        lr = LineRenderedObj.GetComponent<LineRenderer>();
        float alpha = 3.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lr.colorGradient = gradient;
    }
    private void CreateLineRenderers()
    {
        LineRenderers = new List<GameObject>();
        for (int i = 0; i < _connectionsCount; i++)
        {
            GameObject lineRenderer = new GameObject($"{i}");
            lineRenderer.AddComponent<LineRenderer>();
            LineRenderers.Add(lineRenderer);
        }
    }
    private void DrawLine(ref int[] fromPoint, ref int[] toPoint, ref GameObject points)
    {   
            lr = points.GetComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.startWidth = 5;
            lr.endWidth = 5;
            // lr.startColor = Color.green;
            // lr.endColor = Color.white;
            lr.gameObject.GetComponent<Renderer>().material = _material;
            //ChangeLineColor(ref points);
            Vector2 firstPoint = new Vector3(fromPoint[0], fromPoint[1],0f);
            Vector2 secondPoint = new Vector3(toPoint[0], toPoint[1],0f);
            lr.SetPosition(0, firstPoint);
            lr.SetPosition(1, secondPoint);
    }

    private void LinesCreation(ref List<GameObject> lineRenderers)
    {
        for (int i = 0; i < _netlist.Length; i++)
        {
            int firstPosition = _netlist[i][0];
            int secondPosition = _netlist[i][1];
            int[] firstPoint = positions[firstPosition - 1];
            int[] secondPoint = positions[secondPosition - 1];
            GameObject currentRenderer = lineRenderers[i];
            DrawLine(ref firstPoint, ref secondPoint, ref currentRenderer);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
       
    }
}
