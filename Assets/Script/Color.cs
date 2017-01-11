using UnityEngine;
using System.Collections;

public partial class Const {
	public enum Color {
		None,
		Black,
		Red,
		Blue,
		Yellow
	}

	public static Color antiColor(Color color) {
		switch (color) {
		case Color.Red:
			return Color.Blue;
		case Color.Blue:
			return Color.Red;
		default:
			return Color.None;
		}
	}
}
