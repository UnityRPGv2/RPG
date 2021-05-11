using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevTV.Saving
{
    /// <summary>
    /// A `System.Serializable` wrapper for the `Vector3` class.
    /// </summary>
    [System.Serializable]
    public class SerializableVector3
    {

        public static object ToObject(Vector3 v)
        {
            return new List<float>{v.x, v.y, v.z};
        }

        public static Vector3 FromObject(object o)
        {
            Vector3 v = default;
            if (o is IList asList)
            {
                if (asList.Count != 3)
                {
                    return v;
                }

                v.x = Convert.ToSingle(asList[0]);
                v.y = Convert.ToSingle(asList[1]);
                v.z = Convert.ToSingle(asList[2]);
            }
            return v;
        }
    }
}