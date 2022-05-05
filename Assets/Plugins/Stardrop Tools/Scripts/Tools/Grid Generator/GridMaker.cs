
using UnityEngine;

public class GridMaker : StardropTools.CoreObject
{
    [SerializeField] GridGizmos gridGizmos;
    [SerializeField] GridData grid;
    [SerializeField] bool isCenterPoint;
    [SerializeField] bool generate;
    [Space]
    [SerializeField] bool generateCopy;


    public GridData Grid { get => grid; }
    public Vector3[] GridPoints { get => grid.GridPoints; }
    public Vector3[] GridCorners { get => grid.GridCorners; }

    public Vector3 GetRandomPoint() => GridGenerator.GetRandomPoint(grid.GridPoints);
    public Vector3[] GetRandomPoints(int amount) => GridGenerator.GetRandomPoints(grid.GridPoints, amount);


    public void GenerateGrid()
    {
        if (isCenterPoint == false)
            grid.GenerateGridPoints(Position);

        else if (isCenterPoint == true)
        {
            Vector3 startPoint = Vector3.zero;

            float startX = 0;
            float startY = 0;
            float startZ = 0;
            
            if (grid.Distribuition == GridData.PointDistribuition.areaForPoints)
            {
                startX = grid.Width * .5f;
                startY = grid.Height * .5f;
                startZ = grid.Depth * .5f;
            }

            if (grid.Distribuition == GridData.PointDistribuition.spaceBetweenPoints)
            {
                startX = grid.Width * (grid.UnitWidth - 1) * .5f;
                startY = grid.Height * (grid.UnitHeight - 1) * .5f;
                startZ = grid.Depth * (grid.UnitDepth - 1) * .5f;
            }

            startPoint = GetStartPoint(startX, startY, startZ);
            grid.GenerateGridPoints(Position + startPoint * -1);
        }
    }

    public void GenerateGrid(GridData data)
    {
        grid = data;
        grid.GenerateGridPoints();
    }


    public Transform GenerateCopy()
    {
        string parentName = "Grid - " + GridPoints.Length;
        Transform parentGrid = Utilities.CreateEmpty(parentName, Position, Transform);

        for (int i = 0; i < GridPoints.Length; i++)
        {
            string pointName = "Point - " + i;
            Utilities.CreateEmpty(pointName, GridPoints[i], parentGrid);
        }

        return parentGrid;
    }


    Vector3 GetStartPoint(float startX, float startY, float startZ)
    {
        Vector3 startPoint = Vector3.zero;

        if (grid.Space == GridData.GridSpace.xy)
            startPoint = new Vector3(startX, startY, 0);
        else if (grid.Space == GridData.GridSpace.xz)
            startPoint = new Vector3(startX, 0, startZ);
        else if (grid.Space == GridData.GridSpace.yz)
            startPoint = new Vector3(0, startY, startZ);
        else if (grid.Space == GridData.GridSpace.xyz)
            startPoint = new Vector3(startX, startY, startZ);

        return startPoint;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        GenerateGrid();

        if (gridGizmos.drawGizmos == false)
            return;

        if (grid.GridPoints.Exists())
        {
            Gizmos.color = gridGizmos.colorPoints;
            for (int i = 0; i < GridPoints.Length; i++)
                Gizmos.DrawSphere(GridPoints[i], gridGizmos.sizePoints);
        }

        if (grid.GridCorners.Exists())
        {
            Gizmos.color = gridGizmos.colorCorner;
            for (int i = 0; i < GridCorners.Length; i++)
                Gizmos.DrawSphere(GridCorners[i], gridGizmos.sizeCorner);
        }
    }

    Vector3 oldPos;

    protected override void OnValidate()
    {
        base.OnValidate();

        GenerateGrid();

        if (generate)
        {
            GenerateGrid();
            generate = false;
        }

        if (generateCopy)
            GenerateCopy();
        generateCopy = false;

        if (oldPos != Position)
        {
            GenerateGrid();
            oldPos = Position;
        }
    }
}

[System.Serializable]
class GridGizmos
{
    public Color colorCorner = Color.gray;
    public Color colorPoints = Color.red;
    [Space]
    public float sizeCorner = .3f;
    public float sizePoints = .15f;
    [Space]
    public bool drawGizmos;
}