using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Entrance
{
    Up,
    Down,
    Left,
    Right
}

public class SceneEntrance
{
    public bool canPassUp = false;
    public bool canPassDown = false;
    public bool canPassLeft = false;
    public bool canPassRight = false;

    // 用于初始化入口状态
    public SceneEntrance(bool up, bool down, bool left, bool right)
    {
        canPassUp = up;
        canPassDown = down;
        canPassLeft = left;
        canPassRight = right;
    }
}