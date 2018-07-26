using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.IO;

// KMP string matcher
public class KMP
{
    public static int[] Prefix(string prefix)
    {
        int[] backArray = new int[prefix.Length];
        backArray[0] = -1;

        int i = -1;

        for(int q = 1; q < backArray.Length; q++)
        {
            while(i >= 0 && prefix[i + 1] != prefix[q])
            {
                i = backArray[i];
            }

            if(prefix[i + 1] == prefix[q])
            {
                i++;
            }

            backArray[q] = i;
        }

        return backArray;
    }

    public static int Matcher(string text, string prefix)
    {
        int[] backArray = KMP.Prefix(prefix);

        int q = 0;
        int i = -1;

        for(; q < text.Length; q++)
        {
            while(i >= 0 && prefix[i + 1] != text[q])
            {
                i = backArray[i];
            }

            if(prefix[i + 1] == text[q])
            {
                i++;
            }

            if(i == (prefix.Length - 1))
            {
                return (q - prefix.Length + 1);
            }
        }

        return -1;
    }
}