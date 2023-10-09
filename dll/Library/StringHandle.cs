using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Library
{
    public class StringHandle
    {
        public static void drawStringBd(mFont font1, mGraphics g, string text, int x, int y, int align, mFont font2)
        {
            font2.drawString(g, text, x - 1, y - 1, align);
            font2.drawString(g, text, x - 1, y + 1, align);
            font2.drawString(g, text, x + 1, y - 1, align);
            font2.drawString(g, text, x + 1, y + 1, align);
            font2.drawString(g, text, x, y - 1, align);
            font2.drawString(g, text, x, y + 1, align);
            font2.drawString(g, text, x - 1, y, align);
            font2.drawString(g, text, x + 1, y, align);
            font1.drawString(g, text, x, y, align);
        }

        public static void drawStringBd(GUIStyle style1, mGraphics g, string text, float x, float y, GUIStyle style2)
        {
            g.drawString(text, x - 0.5f, y - 0.5f, style2);
            g.drawString(text, x - 0.5f, y + 0.5f, style2);
            g.drawString(text, x + 0.5f, y - 0.5f, style2);
            g.drawString(text, x + 0.5f, y + 0.5f, style2);
            g.drawString(text, x, y - 0.5f, style2);
            g.drawString(text, x, y + 0.5f, style2);
            g.drawString(text, x - 0.5f, y, style2);
            g.drawString(text, x + 0.5f, y, style2);
            g.drawString(text, x, y, style1);
        }

        public static GUIStyle guiStyle(int align, float size, FontStyle fontStyle, Color color)
        {
            GUIStyle guistyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = (int)size * mGraphics.zoomLevel,
                fontStyle = fontStyle
            };
            switch (align)
            {
                case 0:
                    guistyle.alignment = TextAnchor.UpperLeft;
                    break;
                case 1:
                    guistyle.alignment = TextAnchor.UpperRight;
                    break;
                case 2:
                case 3:
                    guistyle.alignment = TextAnchor.UpperCenter;
                    break;
            }
            guistyle.normal.textColor = color;
            return guistyle;
        }

        public static void paint(mFont ForwardFont, mGraphics g, string Text, int x, int y, int align, mFont BackgroundFont, string type, int ZoomLevel)
        {
            if (ZoomLevel != 1)
            {
                if (ZoomLevel != 2)
                {
                    return;
                }
                if (type == "border")
                {
                    ForwardFont.drawStringBd(g, Text, x, y, align, BackgroundFont);
                    return;
                }
                if (type == "noborder")
                {
                    ForwardFont.drawString(g, Text, x, y, align);
                    return;
                }
                if (!(type == "underline"))
                {
                    return;
                }
                ForwardFont.drawString(g, Text, x, y, align, BackgroundFont);
                return;
            }
            else
            {
                if (type == "border")
                {
                    StringHandle.drawStringBd(ForwardFont, g, Text, x, y, align, BackgroundFont);
                    return;
                }
                if (type == "noborder")
                {
                    ForwardFont.drawString(g, Text, x, y, align);
                    return;
                }
                if (!(type == "underline"))
                {
                    return;
                }
                ForwardFont.drawString(g, Text, x, y, align, BackgroundFont);
                return;
            }
        }

        internal static int getWidth(GUIStyle gUIStyle, string s)
        {
            return (int)(gUIStyle.CalcSize(new GUIContent(s)).x * 1.05f / mGraphics.zoomLevel);
        }

        internal static int getHeight(GUIStyle gUIStyle, string content)
        {
            return (int)gUIStyle.CalcSize(new GUIContent(content)).y / mGraphics.zoomLevel;
        }

        public static string status(bool Bool)
        {
            if (!Bool)
            {
                return "Tắt";
            }
            return "Bật";
        }

        public static string statusMenu(bool Bool)
        {
            if (!Bool)
            {
                return "Đang Tắt";
            }
            return "Đang Bật";
        }

        public static T[] getInfoChat<T>(string text, string s, int n)
        {
            T[] array = new T[n];
            string[] array2 = text.Substring(s.Length).Split(new char[]
            {
                ' '
            });
            for (int i = 0; i < n; i++)
            {
                array[i] = (T)((object)Convert.ChangeType(array2[i], typeof(T)));
            }
            return array;
        }

        public static bool isGetInfoChat<T>(string text, string s, int n)
        {
            if (!text.StartsWith(s))
            {
                return false;
            }
            try
            {
                string[] array = text.Substring(s.Length).Split(new char[]
                {
                    ' '
                });
                for (int i = 0; i < n; i++)
                {
                    Convert.ChangeType(array[i], typeof(T));
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool isGetInfoChat<T>(string text, string s)
        {
            if (!text.StartsWith(s))
            {
                return false;
            }
            try
            {
                Convert.ChangeType(text.Substring(s.Length), typeof(T));
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static T getInfoChat<T>(string text, string s)
        {
            return (T)((object)Convert.ChangeType(text.Substring(s.Length), typeof(T)));
        }

        public static long getLongByText(string src)
        {
            try
            {
                return long.Parse(src);
            }
            catch (Exception)
            {
            }
            return -1L;
        }
        
    }
}
