using System.Linq;
using UnityEngine;

public static class Utils
{
    #region String Utils

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[Random.Range(0, s.Length)]).ToArray());
    }

    #endregion

    #region Unity Utils

    public static void RandomName(string prefix, GameObject obj)
    {
        obj.name = $"{prefix}_{RandomString(5)}";
    }

    #endregion
}
