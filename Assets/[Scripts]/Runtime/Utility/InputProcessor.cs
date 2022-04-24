using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelProject.Utility
{
    public class InputProcessor : Singleton<InputProcessor>
    {
        public static bool Test()
        {
            InputProcessor ip = InputProcessor.Instance;
            return ip;
        }
    }
}