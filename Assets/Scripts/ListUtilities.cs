using System.Collections.Generic;
using UnityEngine;

static class ListUtilities
{
    public static T TakeRandom<T>(IList<T> collection)
    {
        return collection[Random.Range(0, collection.Count)];
    }
}
