using UnityEngine;
using System.Collections;

public class Position {

	int X;
	int Y;
	int Z;

	public int x { get { return X; } set { this.X = value; } }
	public int y { get { return Y; } set { this.Y = value; } }
	public int z { get { return Z; } set { this.Z = value; } }

	public static Position operator+ (Position p1, Position p2)
	{
		return new Position (p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);
	}

	public static Position operator- (Position p1, Position p2)
	{
		return new Position (p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
	}

	public static bool operator== (Position p1, Position p2)
	{
		return p1.x == p2.x && p1.y == p2.y && p1.z == p2.z;
	}

	public static bool operator!= (Position p1, Position p2)
	{
		return !(p1 == p2);
	}

	public Position(Position p) {
		set_position (p);
	}

	public Position(int x = -100, int y = -100, int z = -100) {
		set_position (x, y, z);
	}
	public void set_position(int x, int y, int z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}
	public void set_position(Position p) {
		this.x = p.x;
		this.y = p.y;
		this.z = p.z;
	}
	public bool isequal(Position other) {
		return x == other.x && y == other.y && z == other.z;
	}
	public bool isequal(int x, int y, int z) {
		return this.x == x && this.y == y && this.z == z;
	}

}
