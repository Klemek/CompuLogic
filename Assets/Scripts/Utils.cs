using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UntitledLogicGame
{
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

        #region Bool Utils

        public static bool[][] AllBoolArrayValues(int length)
        {
            int count = (int)Math.Pow(2, length);
            return new ArrayList[count].Select((v, i) => i.ToBoolArray(length)).ToArray();
        }

        #endregion

        #region Unity Utils

        public static void RandomName(string prefix, GameObject obj)
        {
            obj.name = $"{prefix}_{RandomString(5)}";
        }

        #endregion
    }

    public static class Extensions
    {
        public static bool[] ToBoolArray(this int value, int length)
        {
            string str = Convert.ToString(value, 2).PadLeft(length, '0');
            return str.Select((x) => x == '1').ToArray();
        }

        public static int ToInt(this bool[] array)
        {
            return array.Select((v, i) => (v ? 1 : 0) << (array.Length - i - 1)).Sum();
        }
    }
}