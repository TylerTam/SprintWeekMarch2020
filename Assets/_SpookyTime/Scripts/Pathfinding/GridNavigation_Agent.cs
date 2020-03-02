using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNavigation_Agent : MonoBehaviour
{
    public GridNavigation m_navGrid;

    public bool m_drawDebugTools;
    private List<Node> m_debugPath;
    public Color m_pathColor;

    public int m_maxOuterIndex;
    public LayerMask m_avoidLayer;

    private void Start()
    {
        m_navGrid = GridNavigation.Instance;
    }

    ///<Summary>
    ///Calculates the path towards the target
    public List<Node> CreatePath(Vector3 startPoint, Vector3 targetPoint)
    {
        //Gets both positions, in terms of nodes
        Node startNode = m_navGrid.NodeFromWorldPoint(startPoint);
        Node endNode = m_navGrid.NodeFromWorldPoint(targetPoint);

        if (Physics2D.OverlapCircle(endNode.m_worldPosition, .1f, m_avoidLayer))
        {
            endNode = GetClosestWalkableNode(endNode);
        }

        if (endNode == null)
        {
            return null;
        }

        Heap<Node> openNodes = new Heap<Node>(m_navGrid.MaxSize);
        HashSet<Node> closedNodes = new HashSet<Node>();
        openNodes.Add(startNode);


        while (openNodes.Count > 0)
        {


            Node currentNode = openNodes.RemoveFirst();
            closedNodes.Add(currentNode);

            //If its the target node, stop calculating
            if (currentNode == endNode)
            {
                m_debugPath = RetracePath(startNode, endNode);
                //return RetracePath(startNode, endNode);
                return m_debugPath;
            }


            foreach (Node neighbour in m_navGrid.GetNeighbours(currentNode))
            {

                if (!neighbour.m_walkable || closedNodes.Contains(neighbour))
                {
                    continue;
                }


                int newMoveCostToNeighbour = currentNode.m_gCost + GetDistance(currentNode, neighbour);
                if (newMoveCostToNeighbour < neighbour.m_gCost || !openNodes.Contains(neighbour))
                {
                    neighbour.m_gCost = newMoveCostToNeighbour;
                    neighbour.m_hCost = GetDistance(neighbour, endNode);
                    neighbour.m_parent = currentNode;

                    if (!openNodes.Contains(neighbour))
                    {
                        openNodes.Add(neighbour);
                    }
                }
            }
        }
        Debug.Log("Path could not be calculated!");
        Debug.DrawLine(startPoint, targetPoint, Color.magenta);
        Debug.Break();
        return null;
    }

    ///<Summary>
    ///Returns the path to the target
    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();

        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.m_parent;
        }
        path.Reverse();

        return path;
    }

    ///<Summary>
    ///Used when finding the costs for the nodes
    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.m_gridPos.x - nodeB.m_gridPos.x);
        int distY = Mathf.Abs(nodeA.m_gridPos.y - nodeB.m_gridPos.y);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        else
        {
            return 14 * distX + 10 * (distY - distX);
        }
    }


    private Node GetClosestWalkableNode(Node p_endNode)
    {
        int currentOuterIndex = 1;
        bool p_pathFound = false;

        Vector2Int m_playerSide = new Vector2Int((int)Mathf.Sign(p_endNode.m_worldPosition.x - transform.position.x), (int)Mathf.Sign(p_endNode.m_worldPosition.y - transform.position.y));

        while (!p_pathFound)
        {
            if (currentOuterIndex >= m_maxOuterIndex)
            {
                break;
            }

            for (int x = (m_playerSide.x > 0) ? -currentOuterIndex : currentOuterIndex; (m_playerSide.x > 0) ? (x < currentOuterIndex) : (x > -currentOuterIndex); x += ((m_playerSide.x > 0)? 1:-1))
            {
                if (x == 0) continue;
                Node currentNode = m_navGrid.GetNodeFromIndex(p_endNode.m_gridPos.x + x, p_endNode.m_gridPos.y);
                if (!Physics2D.OverlapCircle(currentNode.m_worldPosition, .1f, m_navGrid.m_terrain))
                {
                    return currentNode;
                }
            }
            for (int y = (m_playerSide.y > 0) ? -currentOuterIndex : currentOuterIndex; (m_playerSide.y > 0) ? (y < currentOuterIndex) : (y > -currentOuterIndex); y += ((m_playerSide.x > 0) ? 1 : -1))
            {
                if (y == 0) continue;
                Node currentNode = m_navGrid.GetNodeFromIndex(p_endNode.m_gridPos.x, p_endNode.m_gridPos.y + y);
                if (!Physics2D.OverlapCircle(currentNode.m_worldPosition, .1f, m_navGrid.m_terrain))
                {
                    return currentNode;
                }
            }

            for (int x = (m_playerSide.x > 0) ? -currentOuterIndex : currentOuterIndex; (m_playerSide.x > 0) ? (x < currentOuterIndex) : (x > -currentOuterIndex); x += ((m_playerSide.x > 0) ? 1 : -1))
            {
                if (x == 0) continue;
                for (int y = (m_playerSide.y > 0) ? -currentOuterIndex : currentOuterIndex; (m_playerSide.y > 0) ? (y < currentOuterIndex) : (y > -currentOuterIndex); y += ((m_playerSide.x > 0) ? 1 : -1))
                {
                    if (y == 0) continue;
                    Node currentNode = m_navGrid.GetNodeFromIndex(p_endNode.m_gridPos.x + x, p_endNode.m_gridPos.y + y);
                    if (!Physics2D.OverlapCircle(currentNode.m_worldPosition, .1f, m_navGrid.m_terrain))
                    {
                        return currentNode;
                    }
                }
            }
            currentOuterIndex++;
        }
        print("No Path");
        return null;

    }
    private void OnDrawGizmos()
    {
        if (!m_drawDebugTools) return;
        if (m_debugPath == null) return;
        if (m_debugPath.Count > 0)
        {
            Gizmos.color = m_pathColor;
            foreach (Node path in m_debugPath)
            {
                Gizmos.DrawCube(path.m_worldPosition, new Vector3(.25f, .25f));
            }
        }
    }
}
