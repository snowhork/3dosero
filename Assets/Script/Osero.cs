using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Osero : MonoBehaviour
{
    [SerializeField] GameObject stone_pref;
    [SerializeField] GameObject frame_pref;

    private readonly Ai _ai = new Ai();

    private readonly Position _widthNum = new Position(2, 2, 2);
    public Stone[,,] Stones;

    public Board Board { get; private set; }

    public const float StoneScaling = 0.85f;
    public const float StoneStartScaling = 0.35f;
    public const float FrameWidth = 0.04f;
    private float _width = 2.2f;

    public Const.Color BoardColor
    {
        get { return Board.color; }
    }

    private void InstantiateFrame(Stone stone)
    {
        Vector3[] directions = new Vector3[3]
        {
            new Vector3(_width, FrameWidth, FrameWidth),
            new Vector3(FrameWidth, _width, FrameWidth),
            new Vector3(FrameWidth, FrameWidth, _width),
        };
        for (int i = 0; i < 3; i++)
        {
            var frame = Instantiate(frame_pref);
            frame.transform.position = stone.transform.position;
            frame.transform.rotation = Quaternion.identity;
            frame.transform.localScale = directions[i];
        }
    }

    public void SetStone(Position p, Const.Color color)
    {
        var stone = Stones[p.x, p.y, p.z];
        stone.ChangeColor(color);
    }

    private void set_stone_scale(Stone stone)
    {
        var abs_position = new float[3]
        {
            Mathf.Abs(stone.transform.position.x), Mathf.Abs(stone.transform.position.y),
            Mathf.Abs(stone.transform.position.z)
        };
        float max_position = -_width * Board.MaxWidth;
        for (int i = 0; i < 3; i++)
        {
            if (max_position < abs_position[i])
                max_position = abs_position[i];
        }
        float max_depth = (max_position - _width / 2) / _width;
        stone.transform.localScale = Vector3.one * (StoneStartScaling + max_depth * StoneScaling);
    }

    public void Initialize()
    {
        Board = new Board(_widthNum);
        Stones = Board.WidthArray<Stone>();
        var startPosition = Vector3.one * (-_width * Board.MaxWidth + _width / 2f);

        foreach (Position p in Board.StonesIterator)
        {
            var stone = Instantiate(stone_pref).GetComponent<Stone>();
            Stones[p.x, p.y, p.z] = stone;
            stone.SetOsero(this);

            stone.transform.position = startPosition + new Vector3(p.x, p.y, p.z) * _width;
            stone.transform.rotation = Quaternion.identity;
            set_stone_scale(stone);
            InstantiateFrame(stone);

            MaterialManager.Instance.SetMaterial(Const.Color.Black, stone.gameObject);
            var info = stone.Info;
            info.initialize(p);
            Board.SetStoneInfo(p, info);
        }
        SetStone(new Position(1, 1, 1), Const.Color.Red);
        SetStone(new Position(2, 1, 2), Const.Color.Red);
        SetStone(new Position(1, 2, 1), Const.Color.Red);
        SetStone(new Position(2, 2, 2), Const.Color.Red);
        SetStone(new Position(1, 1, 2), Const.Color.Blue);
        SetStone(new Position(2, 1, 1), Const.Color.Blue);
        SetStone(new Position(1, 2, 2), Const.Color.Blue);
        SetStone(new Position(2, 2, 1), Const.Color.Blue);
    }

    public int CountStone(Const.Color color)
    {
        return Board.CountStone(color);
    }

    bool exists_set_stone()
    {
//		for (int x = Osero.instance.wall_x_min; x < Osero.instance.wall_x_max; x++)
//			for (int y = Osero.instance.wall_y_min; y < Osero.instance.wall_y_max; y++)
//				for (int z = Osero.instance.wall_z_min; z < Osero.instance.wall_z_max; z++) {
//
//					Position position = new Position (x, y, z);
//					if (Osero.instance.set_stone (Osero.instance.get_stone_status (position), Osero.SetStoneMode.Settable)) {
//						return true;
//					}
//				}
        return false;
    }
}