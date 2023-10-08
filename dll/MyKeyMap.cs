using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class MyKeyMap
{
	private static Hashtable h;

	static MyKeyMap()
	{
		h = new Hashtable();
		h.Add(KeyCode.A, 97);
		h.Add(KeyCode.B, 98);
		h.Add(KeyCode.C, 99);
		h.Add(KeyCode.D, 100);
		h.Add(KeyCode.E, 101);
		h.Add(KeyCode.F, 102);
		h.Add(KeyCode.G, 103);
		h.Add(KeyCode.H, 104);
		h.Add(KeyCode.I, 105);
		h.Add(KeyCode.J, 106);
		h.Add(KeyCode.K, 107);
		h.Add(KeyCode.L, 108);
		h.Add(KeyCode.M, 109);
		h.Add(KeyCode.N, 110);
		h.Add(KeyCode.O, 111);
		h.Add(KeyCode.P, 112);
		h.Add(KeyCode.Q, 113);
		h.Add(KeyCode.R, 114);
		h.Add(KeyCode.S, 115);
		h.Add(KeyCode.T, 116);
		h.Add(KeyCode.U, 117);
		h.Add(KeyCode.V, 118);
		h.Add(KeyCode.W, 119);
		h.Add(KeyCode.X, 120);
		h.Add(KeyCode.Y, 121);
		h.Add(KeyCode.Z, 122);
		//h.Add(KeyCode.Alpha0, 48);
		//h.Add(KeyCode.Alpha1, 49);
		//h.Add(KeyCode.Alpha2, 50);
		//h.Add(KeyCode.Alpha3, 51);
		//h.Add(KeyCode.Alpha4, 52);
		//h.Add(KeyCode.Alpha5, 53);
		//h.Add(KeyCode.Alpha6, 54);
		//h.Add(KeyCode.Alpha7, 55);
		//h.Add(KeyCode.Alpha8, 56);
		//h.Add(KeyCode.Alpha9, 57);
		h.Add(KeyCode.Space, 32);
		h.Add(KeyCode.F1, -21);
		h.Add(KeyCode.F2, -22);
		h.Add(KeyCode.Equals, -25);
		//h.Add(KeyCode.Minus, 45);
		h.Add(KeyCode.F3, -23);
		h.Add(KeyCode.UpArrow, -1);
		h.Add(KeyCode.DownArrow, -2);
		h.Add(KeyCode.LeftArrow, -3);
		h.Add(KeyCode.RightArrow, -4);
		h.Add(KeyCode.Backspace, -8);
		h.Add(KeyCode.Return, -5);
		//h.Add(KeyCode.Period, 46);
		//h.Add(KeyCode.At, 64);
		h.Add(KeyCode.Tab, -26);
	}

    [DllImport("User32.dll")]
    public static extern short GetKeyState(int nVirtKey);

    public static int map(KeyCode k)
	{
        object obj = h[k];
        if (obj == null)
        {
            int num = (int)k;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || GetKeyState(20) > 0 /*cApS LoCK*/)
            {
                if (num >= 97 && num <= 122) num -= 32;
            }
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                switch (k)
                {
                    case KeyCode.BackQuote:
                        num = '~';
                        break;
                    case KeyCode.Alpha1:
                        num = (int)KeyCode.Exclaim;
                        break;
                    case KeyCode.Alpha2:
                        num = (int)KeyCode.At;
                        break;
                    case KeyCode.Alpha3:
                        num = (int)KeyCode.Hash;
                        break;
                    case KeyCode.Alpha4:
                        num = (int)KeyCode.Dollar;
                        break;
                    case KeyCode.Alpha5:
                        num = '%';
                        break;
                    case KeyCode.Alpha6:
                        num = (int)KeyCode.Caret;
                        break;
                    case KeyCode.Alpha7:
                        num = (int)KeyCode.Ampersand;
                        break;
                    case KeyCode.Alpha8:
                        num = (int)KeyCode.Asterisk;
                        break;
                    case KeyCode.Alpha9:
                        num = (int)KeyCode.LeftParen;
                        break;
                    case KeyCode.Alpha0:
                        num = (int)KeyCode.RightParen;
                        break;
                    case KeyCode.Minus:
                        num = (int)KeyCode.Underscore;
                        break;
                    case KeyCode.Equals:
                        num = (int)KeyCode.Plus;
                        break;
                    case KeyCode.LeftBracket:
                        num = '{';
                        break;
                    case KeyCode.RightBracket:
                        num = '}';
                        break;
                    case KeyCode.Backslash:
                        num = '|';
                        break;
                    case KeyCode.Semicolon:
                        num = (int)KeyCode.Colon;
                        break;
                    case KeyCode.Quote:
                        num = (int)KeyCode.DoubleQuote;
                        break;
                    case KeyCode.Comma:
                        num = (int)KeyCode.Less;
                        break;
                    case KeyCode.Period:
                        num = (int)KeyCode.Greater;
                        break;
                    case KeyCode.Slash:
                        num = (int)KeyCode.Question;
                        break;
                }
            }
            return num;
        }
        return (int)obj;
    }
}
