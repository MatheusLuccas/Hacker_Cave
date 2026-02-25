using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class crypted_device : MonoBehaviour
{
    public int scanned_a, scanned_b, scanned_c;

    public int [] scan(int in_a = 0, int in_b = 0, int in_c = 0, int a_max = 4, int b_max = 4, int c_max = 4)
    {
        int [] result = new int[3];
        int a = in_a, b = in_b, c = in_c;
        if(a > b)
        {
            if(b < b_max)
            {
                b++;
            }else{
                b = 0;
            }
        }else if(a > c)
        {
            if(c < c_max)
            {
                c++;
            }else{
                c = 0;
            }
        }else
        {
            if(a < a_max)
            {
                a++;
            }else{
                a = 0;
            }
        }
        result[0] = a;
        result[1] = b;
        result[2] = c;
        return result;
    }
}