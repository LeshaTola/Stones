using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;
using System;
using System.Linq;
using System.IO;

public class PathFinding : MonoBehaviour {

	const int STRAIGHT_MOVE_COST = 10;

	private void Start() {
		FindPath(new int2(0, 0), new int2(2, 2));
	}

	private void FindPath(int2 startPosition, int2 endPosition) {
		int2 gridSize = new int2(4,4);

		NativeArray<PathNode> pathNodeArray = new(gridSize.x * gridSize.y, Allocator.Temp);

		for(int x= 0; x < gridSize.x; x++) {
			for(int y = 0;y< gridSize.y; y++) {
				PathNode pathNode = new PathNode();
				pathNode.x = x;
				pathNode.y = y;
				pathNode.index = CalculateTheIndex(x, y, gridSize.x);

				pathNode.gCost = int.MaxValue;
				pathNode.hCost = CalculateTheDistanceCost(new int2(x,y), endPosition);
				pathNode.CalculateFCost();

				pathNode.isWalkable= true;

				pathNode.cameFromNodeIndex = -1;

				pathNodeArray[pathNode.index] = pathNode;
			}
		}

		int endNodeIndex = CalculateTheIndex(endPosition.x,endPosition.y, gridSize.x);

		NativeArray <int2> NaighborOffsetArray = new NativeArray<int2>(new int2[] {
			new int2(1,0), //вправо
			new int2(-1,0), //влево
			new int2(0,1),	//вверх
			new int2(0,-1), //вниз
		}, Allocator.Temp);

		PathNode startNode = pathNodeArray[CalculateTheIndex(startPosition.x, startPosition.y, gridSize.x)];
		startNode.gCost = 0;
		startNode.CalculateFCost();
		pathNodeArray[CalculateTheIndex(startPosition.x, startPosition.y, gridSize.x)] = startNode;

		NativeList<int> OpenList= new NativeList<int>(Allocator.Temp);
		NativeList<int> ClosedList = new NativeList<int>(Allocator.Temp);
		
		OpenList.Add(startNode.index);

		while(OpenList.Length> 0) {
			int curentNodeIndex = GetTheLowestFCostNodeIndex(OpenList,pathNodeArray);
			PathNode curentNode = pathNodeArray[curentNodeIndex];

			if(curentNode.index == endNodeIndex) {
				break;
			}

			for(int i = 0; i< OpenList.Length; i++) {

				if(curentNodeIndex == OpenList[i]) {
					OpenList.RemoveAtSwapBack(i);
					break;
				}
			}
			ClosedList.Add(curentNodeIndex);

			for(int i = 0; i< NaighborOffsetArray.Length; i++) {
				int2 neigborOfset = NaighborOffsetArray[i];
				int2 neighborPosition = new int2(curentNode.x + neigborOfset.x,curentNode.y+ neigborOfset.y);
				if(!IsPositionInsideGrid(neighborPosition,gridSize)) {
					continue;
				}

				int neighborIndex = CalculateTheIndex(neighborPosition.x,neighborPosition.y, gridSize.x);

				if(ClosedList.Contains(neighborIndex)){
					continue;
				}

				PathNode neighborNode = pathNodeArray[neighborIndex];

				if (!neighborNode.isWalkable) {
					continue;
				}

				int2 curentNodePosition = new int2 (curentNode.x,curentNode.y);

				int tentativeGCost = curentNode.gCost + CalculateTheDistanceCost(curentNodePosition,neighborPosition);
				if(tentativeGCost < neighborNode.gCost) {
					neighborNode.cameFromNodeIndex= curentNodeIndex;
					neighborNode.gCost = tentativeGCost;
					neighborNode.CalculateFCost();
					pathNodeArray[neighborIndex] = neighborNode;

					if (!OpenList.Contains(neighborIndex)) {
						OpenList.Add(neighborIndex);
					}
				}
			}
		}

		PathNode endNode = pathNodeArray[endNodeIndex];
		if(endNode.cameFromNodeIndex == -1) {
			Debug.Log("не нашел");
		}
		else {
			NativeList<int2> path = CalculatePath(pathNodeArray,endNode);
			foreach(int2 index in path) {
				Debug.Log(index);
			}
			path.Dispose();
		}

		NaighborOffsetArray.Dispose();
		OpenList.Dispose();
		ClosedList.Dispose();
		pathNodeArray.Dispose();
	}

	private NativeList<int2> CalculatePath(NativeArray<PathNode> pathNodeArray, PathNode endNode) {
		if (endNode.cameFromNodeIndex == -1) {
			// Couldn't find a path!
			return new NativeList<int2>(Allocator.Temp);
		}
		else {
			// Found a path
			NativeList<int2> path = new NativeList<int2>(Allocator.Temp);
			path.Add(new int2(endNode.x, endNode.y));

			PathNode currentNode = endNode;
			while (currentNode.cameFromNodeIndex != -1) {
				PathNode cameFromNode = pathNodeArray[currentNode.cameFromNodeIndex];
				path.Add(new int2(cameFromNode.x, cameFromNode.y));
				currentNode = cameFromNode;
			}

			return path;
		}
	}

	private bool IsPositionInsideGrid(int2 position, int2 gridSize) {
		return position.x>=0
			&& position.y>=0
			&& position.x<gridSize.x
			&& position.y<gridSize.y;
	}

	private int GetTheLowestFCostNodeIndex(NativeList<int>OpenList,NativeArray<PathNode> pathNodeArray) {
		PathNode NodeWithLowestFCost = new PathNode();
		for(int i = 0; i < OpenList.Length; i++) {
			PathNode buffer = pathNodeArray[OpenList[i]];
			if (NodeWithLowestFCost.fCost > buffer.fCost) {
				NodeWithLowestFCost= buffer;
			}
		}
		return NodeWithLowestFCost.index;
	}

	int CalculateTheDistanceCost(int2 aPosition, int2 bPosition) {
		int dx = Math.Abs(aPosition.x - bPosition.x);
		int dy = Math.Abs(aPosition.y - bPosition.y);


		return (dy+dx)*STRAIGHT_MOVE_COST;
	}

	int CalculateTheIndex(int x, int y, int gridSize) {
		return x + y * gridSize;
	}

	struct PathNode {
		public int x;
		public int y;

		public int index;

		public int gCost;
		public int hCost;
		public int fCost;

		public bool isWalkable;

		public int cameFromNodeIndex;

		public void CalculateFCost() {
			fCost = gCost + hCost;
		}
	}
}
