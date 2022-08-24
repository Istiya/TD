using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    int rows;
    int cols;
    int border = 1;
    int seed { get; set; }
    int pathLength = 1000;

    int endScene = 0;
    int bufferEnd = 0;
    float exitDirectionWeight = 0.0f; // says the priority of progressing toward the exit 
    float previousDirectionWeight = 5.0f;
    float windyWeight = 5.0f;// will result in less direction changes -> simpler path
    float randomDirectionWeight = 3.0f; // filler weight to shift the odds towards randomness

    private Vector3 startPos = new Vector3(3f, 1.5f, 1f);

    bool[,] pathExistsOnNode;
    List<Vector2Int> pathNodes;

    public MapGenerator(int rows, int cols)
    {
        this.seed = Random.Range(0, System.Int32.MaxValue);
        Random.InitState(seed);
        this.rows = rows;
        this.cols = cols;
        GenerateRandomPath();
    }

    void SetSeed(int seed)
    {
        this.seed = seed;
    }

    bool IsPositionInBounds(Vector2Int pos)
    {
        return pos.x >= border && pos.x < rows - border
           && pos.y >= border && pos.y < cols - border;
    }

    bool IsTooCloseToPath(Vector2Int current, Vector2Int direction, ref bool[,] pathExistsOnNode)
    {
        Vector2Int possiblePosition = current + direction;
        if (pathExistsOnNode[possiblePosition.x, possiblePosition.y])
            return true;

        if (direction.y == 0)
        {
            Vector2Int temp = possiblePosition - new Vector2Int(0, 1);
            if (IsPositionInBounds(temp))
                if (pathExistsOnNode[temp.x, temp.y])
                    return true;

        }

        return false;
    }

    List<Vector2Int> GetAvailableDirections(Vector2Int current, Vector2Int end, ref bool[,] pathExistsOnNode)
    {

        List<Vector2Int> possibleDirections = new List<Vector2Int>();
        possibleDirections.Add(Vector2Int.right);
        possibleDirections.Add(Vector2Int.left);
        possibleDirections.Add(Vector2Int.up);


        List<Vector2Int> availableDirections = new List<Vector2Int>();

        bool isTooCloseToAnother;
        Vector2Int possiblePosition;

        for (int i = 0; i < possibleDirections.Count; i++)
        {
            // If that direction is still on the map
            possiblePosition = current + possibleDirections[i];
            if (IsPositionInBounds(possiblePosition))
            {
                // Check if the tile is directly next to another part of the path
                isTooCloseToAnother = IsTooCloseToPath(current, possibleDirections[i], ref pathExistsOnNode);
                if (!isTooCloseToAnother)
                {
                    availableDirections.Add(possibleDirections[i]);
                }
            }
        }

        return availableDirections;
    }


    private List<Vector2Int> GenerateRandomPath()
    {
        // What is the delta in any coordinate before the path for be forced toward the exit
        int endGameBuffer = 2 + bufferEnd + endScene;


        // Define odds
        float exitDirectionWeight = this.exitDirectionWeight;        // says the priority of progressing toward the exit 
        float previousDirectionWeight = this.previousDirectionWeight;   // will result in less direction changes -> simpler path
        float randomDirectionWeight = this.randomDirectionWeight;
        float windyWeight = this.windyWeight; // filler weight to shift the odds towards randomness

        // Build odd ranges 
        float oddsTotal = 0;
        SortedDictionary<string, Vector2> Odds = new SortedDictionary<string, Vector2>();
        Odds.Add("exitDirection", new Vector2(oddsTotal, oddsTotal += exitDirectionWeight));
        Odds.Add("previousDirection", new Vector2(oddsTotal, oddsTotal += previousDirectionWeight));
        Odds.Add("windy", new Vector2(oddsTotal, oddsTotal += windyWeight));
        Odds.Add("randomDirection", new Vector2(oddsTotal, oddsTotal += randomDirectionWeight));


        //##################################################################################


        Vector2Int start = new Vector2Int(
            Random.Range(border + bufferEnd, rows - 1 - border - bufferEnd),
            0
        );
        Vector2Int end = new Vector2Int(
            Random.Range(border + bufferEnd, rows - 1 - border - bufferEnd),
            cols - 1 - bufferEnd
        );




        // Define a map for where the pathing currently exists
        pathExistsOnNode = new bool[rows, cols];
        for (int x = 0; x < rows; ++x)
            for (int y = 0; y < cols; ++y)
                pathExistsOnNode[x, y] = false;

        // Initialize path
        pathNodes = new List<Vector2Int>();



        // Add starting point 
        Vector2Int next = start;
        Vector2Int current = next;
        pathNodes.Add(current);
        pathExistsOnNode[current.x, current.y] = true;


        Vector2Int previousDirection = new Vector2Int(0, 1);
        Vector2Int moveDirection = new Vector2Int(0, 1);

        while (current != end && pathNodes.Count < pathLength)
        {
            // Getting toward the end... don't mess around
            if (current.y + endGameBuffer >= end.y)
            {
                if (current.y + endGameBuffer - 1 >= end.y)
                {
                    // At the very end, align then straight out
                    moveDirection = AlignWithExitDirection(current, end);
                }
                else
                {
                    // Move forward to give little space between previous pathing
                    moveDirection = ApproachExitDirection(current, end);
                }
            }
            else
            {
                // Path can wander a bit
                List<Vector2Int> possibleDirections = GetAvailableDirections(current, end, ref pathExistsOnNode);

                // Analyze the possible directions 
                // Seperate into notible directions we would like to go and other possabilities for randoness
                bool previousDirectionPossible = false;
                bool towardExitDirectionPossible = false;
                bool horizontalDirectionsPossible = false;
                List<Vector2Int> horizontalDirections = new List<Vector2Int>();
                for (int i = 0; i < possibleDirections.Count; ++i)
                {
                    // Can move toward exit?
                    if (possibleDirections[i] == moveDirection)
                        towardExitDirectionPossible = true;

                    // Can keep current direction?
                    if (possibleDirections[i] == previousDirection)
                        previousDirectionPossible = true;

                    // Can move horizontally?
                    if (possibleDirections[i].y == 0)
                    {
                        horizontalDirectionsPossible = true;
                        horizontalDirections.Add(possibleDirections[i]);
                    }
                }



                // Decide direction to go randomly
                bool isDirectionApplied = false;
                string directionMode = "exitDirection"; // default go to the exit
                float randomNumber = Random.Range(0.0f, oddsTotal);
                foreach (KeyValuePair<string, Vector2> entry in Odds)
                {
                    if (entry.Value.x <= randomNumber && randomNumber <= entry.Value.y)
                    {
                        directionMode = entry.Key;
                        break;
                    }
                }


                // Choose from possible directions

                // Windy path
                if (horizontalDirectionsPossible && directionMode == "windy")
                {
                    isDirectionApplied = true;
                    moveDirection = horizontalDirections[Random.Range(0, horizontalDirections.Count - 1)];
                }

                // Move toward exit
                if (towardExitDirectionPossible && directionMode == "exitDirection")
                {
                    isDirectionApplied = true;
                    moveDirection = ApproachExitDirection(current, end);
                }

                // Keep current direction
                if (previousDirectionPossible && directionMode == "previousDirection")
                {
                    isDirectionApplied = true;
                    moveDirection = previousDirection;
                }

                // Pick a random direction form what is allowed
                if (!isDirectionApplied)
                {
                    if (possibleDirections.Count > 0)
                        moveDirection = possibleDirections[Random.Range(0, possibleDirections.Count - 1)];
                    else
                    {   // Something messed up bad!
                        Debug.Log("No possabilities exist");
                        break;
                    }
                }
            }


            // Set tile
            next = current + moveDirection;
            current = next;
            pathNodes.Add(current);
            pathExistsOnNode[current.x, current.y] = true;
            previousDirection = moveDirection;

        }
        return pathNodes;
    }

    Vector2Int ApproachExitDirection(Vector2Int current, Vector2Int end)
    {
        Vector2Int exitDirection = new Vector2Int(0, 0);
        if (end.y > current.y)
            exitDirection.y = 1;
        else if (end.y < current.y)
            exitDirection.y = -1;
        else if (end.x > current.x)
            exitDirection.x = 1;
        else if (end.x < current.x)
            exitDirection.x = -1;
        return exitDirection;
    }


    Vector2Int AlignWithExitDirection(Vector2Int current, Vector2Int end)
    {
        Vector2Int exitDirection = new Vector2Int(0, 0);
        if (end.x > current.x)
            exitDirection.x = 1;
        else if (end.x < current.x)
            exitDirection.x = -1;
        else if (end.y > current.y)
            exitDirection.y = 1;
        else if (end.y < current.y)
            exitDirection.y = -1;
        return exitDirection;
    }

    public string[,] GenerateMap()
    {
        
        string[,] nodesTypes = new string[rows, cols];

        for (int i = 0; i < pathNodes.Count; i++)
        {
            nodesTypes[pathNodes[i].x, pathNodes[i].y] = "Path";
        }

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
            {
                if (nodesTypes[i, j] == null)
                {
                    nodesTypes[i, j] = "Terrain";
                }
            }

        return nodesTypes;
    }

    public List<Vector3> GenerateWaypoints()
    {
        List<Vector3> waypointsList = Waypoints.GenerateWaypoints(pathNodes, startPos);
        return waypointsList;
    }
}
