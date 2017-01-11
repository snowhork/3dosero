using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board {

	Position widthNum;
	//public Position WidthNum { get { return widthNum; } }
	int WidthX { get { return widthNum.x; } }
	int WidthY { get { return widthNum.y; } }
	int WidthZ { get { return widthNum.z; } }
	public int MaxWidth { get { return WidthX; } }

	StoneInfo[,,] StoneInfos;

	public Type[,,] WidthArray<Type>() {
		return new Type[WidthX*2, WidthY*2, WidthZ*2];
	}

	public IEnumerable StonesIterator
	{
		get {			
			for (int x = 0; x < WidthX*2; x++) {				
				for (int y = 0; y < WidthY*2; y++) {
					for (int z = 0; z < WidthZ*2; z++) {
						yield return new Position (x, y, z);
					}
				}
			}
		}
	}

	public Board(Position widthNum) {
		this.widthNum = widthNum;
		StoneInfos = new StoneInfo[WidthX * 2, WidthY * 2, WidthZ * 2];
	}

	bool IsInRange(Position p) {
		return p.x >= 0 && p.x < WidthX * 2 && p.y >= 0 && p.y < WidthY * 2 && p.z >= 0 && p.z < WidthZ * 2;
	}

	Const.Color GetStoneColor(Position p) {
		return StoneInfos [p.x, p.y, p.z].color;
	}

	StoneInfo GetStoneInfo(Position p) {
		return StoneInfos [p.x, p.y, p.z];
	}

	public void SetStoneInfo(Position p, StoneInfo info) {
		StoneInfos [p.x, p.y, p.z] = info;
		info.board = this;
	}

	IEnumerable DirectionsIterator
	{
		get {			
			for (int x = -1; x <= 1; x++) {				
				for (int y = -1; y <= 1; y++) {
					for (int z = -1; z <= 1; z++) {
						if (x == 0 && y == 0 && z == 0) {
							continue;
						}
						yield return new Position (x, y, z);
					}
				}
			}
		}
	}

	public List<StoneInfo> SetStone(Position p, Const.Color color) {
		var list = new List<StoneInfo> ();
		var info = StoneInfos [p.x, p.y, p.z];
		if (info.color != Const.Color.Black) {
			return list;
		}
		foreach( Position d in DirectionsIterator) {			
			for(Position n = p + d; IsInRange(n); n = n + d) {			
				if (GetStoneColor (n) == Const.Color.Black) {
					break;
				}
				if (GetStoneColor (n) == color) {
					for(n = n - d; n != p; n = n - d) {					
						Debug.Log (n.x);	
						if (GetStoneColor (n) == Const.antiColor (color)) {
							list.Add (GetStoneInfo (n));
						}
					}
					break;
				}
			}
		}
		return list;
	}

	public bool Settable(Position p, Const.Color color) {
		var list = SetStone (p, color);
		return list.Count > 0;
	}
}
