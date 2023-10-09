using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Library
{
    public class CharEffect
    {
        public static bool isEnabled = true;

        public static List<Char> storedChars = new List<Char>();

        public static bool isNRDAdded, isTieAdded, isTDHSAdded, isMobMeAdded, isMonkeyAdded;

        static short NRSDImageId;

        public static void updateMe()
        {
            CharEffectTime meEffTime = Char.myCharz().charEffectTime;
            if (meEffTime.hasMobMe)
            {
                if (!isMobMeAdded)
                {
                    isMobMeAdded = true;
                    if (Until.isMeWearingPikkoroDaimaoSet())
                        Char.vItemTime.addElement(new ItemTime(722, true));
                    else
                        Char.vItemTime.addElement(new ItemTime(722, CharExtensions.getTimeMobMe(Char.myCharz())));
                }
            }
            else if (isMobMeAdded)
            {
                isMobMeAdded = false;
                if (Until.isMeWearingPikkoroDaimaoSet())
                    removeElement(new ItemTime(722, true));
                else
                    removeElement(new ItemTime(722, 0));
            }
            if (meEffTime.isTied)
            {
                if (!isTieAdded)
                {
                    isTieAdded = true;
                    Char.vItemTime.addElement(new ItemTime(3779, 35, true));
                }
            }
            else if (isTieAdded)
            {
                isTieAdded = false;
                removeElement(new ItemTime(3779, 0, true));
            }
            if (meEffTime.isTDHS)
            {
                if (!isTDHSAdded)
                {
                    isTDHSAdded = true;
                    Char.vItemTime.addElement(new ItemTime(717, Char.myCharz().freezSeconds));
                }
            }
            else if (isTDHSAdded)
            {
                isTDHSAdded = false;
                removeElement(new ItemTime(717, 0));
            }
            if (meEffTime.hasMonkey)
            {
                if (!isMonkeyAdded)
                {
                    isMonkeyAdded = true;
                    if (Until.isMeWearingCadicSet())
                        Char.vItemTime.addElement(new ItemTime(718, CharExtensions.getTimeMonkey(Char.myCharz()) * 5));
                    else
                        Char.vItemTime.addElement(new ItemTime(718, CharExtensions.getTimeMonkey(Char.myCharz())));
                }
            }
            else if (isMonkeyAdded)
            {
                isMonkeyAdded = false;
                removeElement(new ItemTime(718, 0));
            }
            if (meEffTime.hasNRD)
            {
                if (!isNRDAdded)
                {
                    isNRDAdded = true;
                    NRSDImageId = Until.getNRSDId();
                    Char.vItemTime.addElement(new ItemTime(NRSDImageId, 300));
                }
            }
            else if (isNRDAdded)
            {
                isNRDAdded = false;
                removeElement(new ItemTime(NRSDImageId, 0));
            }

        }

        public static void removeElement(ItemTime item)
        {
            for (int i = 0; i < Char.vItemTime.size(); i++)
            {
                ItemTime itemTime = Char.vItemTime.elementAt(i) as ItemTime;
                if (itemTime.idIcon == item.idIcon && itemTime.isEquivalence == item.isEquivalence && itemTime.isInfinity == item.isInfinity)
                {
                    Char.vItemTime.removeElementAt(i);
                    return;
                }
            }
        }

        public static void update()
        {
            updateMe();
            for (int i = storedChars.Count - 1; i >= 0; i--)
            {
                Char c = storedChars.ElementAt(i);
                Char @char = GameScr.findCharInMap(c.charID);
                if (!c.charEffectTime.HasAnyEffect())
                    storedChars.RemoveAt(i);
                else if (@char == null)
                    c.charEffectTime.update();
                else
                {
                    GameScr.findCharInMap(c.charID).charEffectTime = c.charEffectTime;
                    storedChars[i] = @char;
                }
            }
        }

        public static bool isContains(int charId)
        {
            foreach (Char c in storedChars)
                if (c.charID == charId)
                    return true;
            return false;
        }

        public static void paint(mGraphics g)
        {
            if (!isEnabled)
                return;
            if (Char.myCharz().mobFocus != null && LibraryChar.enableShowInforMob)
            {
                List<string> list = new List<string>();
                int num = 62;
                Mob mobFocus = Char.myCharz().mobFocus;
                list.Add(string.Concat(new string[]
                {
                mobFocus.getTemplate().name,
                " [",
                NinjaUtil.getMoneys((long)mobFocus.hp),
                "/",
                NinjaUtil.getMoneys((long)mobFocus.maxHp),
                "]"
                }));
                if (mobFocus.mobEffectTime.isTied)
                {
                    list.Add("Đang bị trói " + mobFocus.mobEffectTime.timeTied.ToString() + " giây");
                }
                if (mobFocus.mobEffectTime.isTDHS)
                {
                    list.Add("Đang bị TDHS " + mobFocus.mobEffectTime.timeTDHS.ToString() + " giây");
                }
                if (mobFocus.mobEffectTime.isTeleported)
                {
                    list.Add("Đang bị DCTT " + mobFocus.mobEffectTime.timeTeleported.ToString() + " giây");
                }
                if (mobFocus.mobEffectTime.isHypnotized)
                {
                    list.Add("Đang bị Thôi miên " + mobFocus.mobEffectTime.timeHypnotized.ToString() + " giây");
                }
                foreach (string text in list)
                {
                    StringHandle.paint(mFont.tahoma_7b_yellow, g, text, GameCanvas.w / 2, num, mFont.CENTER, mFont.tahoma_7b_dark, "border", mGraphics.zoomLevel);
                    num += 10;
                }
            }
            Char charFocus = Char.myCharz().charFocus;
            if (charFocus != null && LibraryChar.enableShowInforChar)
            {
                List<string> list2 = new List<string>();
                int num2 = 62;
                list2.Add(string.Concat(new string[]
                {
                            charFocus.cName,
                            " [",
                            NinjaUtil.getMoneys((long)charFocus.cHP),
                            "/",
                            NinjaUtil.getMoneys((long)charFocus.cHPFull),
                            "]"
                }));
                if (charFocus.charEffectTime != null && charFocus.charEffectTime.HasAnyEffect())
                {
                    if (charFocus.charEffectTime.hasNRD)
                    {
                        list2.Add("NRD còn: " + charFocus.charEffectTime.timeHoldingNRD.ToString() + " giây");
                    }
                    if (charFocus.charEffectTime.hasShield)
                    {
                        list2.Add("Khiên còn: " + charFocus.charEffectTime.timeShield.ToString() + " giây");
                    }
                    if (charFocus.charEffectTime.hasMonkey)
                    {
                        list2.Add("Khỉ còn: " + charFocus.charEffectTime.timeMonkey.ToString() + " giây");
                    }
                    if (charFocus.charEffectTime.hasHuytSao)
                    {
                        list2.Add("Huýt sáo còn: " + charFocus.charEffectTime.timeHuytSao.ToString() + " giây");
                    }
                    if (charFocus.charEffectTime.hasMobMe)
                    {
                        list2.Add("Đẻ trứng còn: " + charFocus.charEffectTime.timeMobMe.ToString() + " giây");
                    }
                    if (charFocus.charEffectTime.isHypnotized)
                    {
                        list2.Add((charFocus.charEffectTime.isHypnotizedByMe ? "Bị bạn thôi miên: " : "Bị thôi miên: ") + charFocus.charEffectTime.timeHypnotized.ToString() + " giây");
                    }
                    if (charFocus.charEffectTime.isTeleported)
                    {
                        list2.Add("Bị DCTT: " + charFocus.charEffectTime.timeTeleported.ToString() + " giây");
                    }
                    if (charFocus.charEffectTime.isTDHS)
                    {
                        list2.Add("Bị TDHS: " + charFocus.charEffectTime.timeTDHS.ToString() + " giây");
                    }
                    if (charFocus.charEffectTime.isTied)
                    {
                        list2.Add((charFocus.charEffectTime.isTiedByMe ? "Bị bạn trói: " : "Bị trói: ") + charFocus.charEffectTime.timeTied.ToString() + " giây");
                    }
                    if (charFocus.charEffectTime.isStone)
                    {
                        list2.Add("Bị hóa đá: " + charFocus.charEffectTime.timeStone.ToString() + " giây");
                    }
                    if (charFocus.charEffectTime.isChocolate)
                    {
                        list2.Add("Bị biến Sôcôla: " + charFocus.charEffectTime.timeChocolate.ToString() + " giây");
                    }
                }
                foreach (string text2 in list2)
                {
                    if (text2[0].ToString().Split(new char[]
                    {
                                ' '
                    })[0] != Char.myCharz().cName)
                    {
                        StringHandle.paint(mFont.tahoma_7b_red, g, text2, GameCanvas.w / 2, num2, mFont.CENTER, mFont.tahoma_7b_dark, "border", mGraphics.zoomLevel);
                    }
                    else
                    {
                        StringHandle.paint(mFont.tahoma_7b_yellow, g, text2, GameCanvas.w / 2, num2, mFont.CENTER, mFont.tahoma_7b_dark, "border", mGraphics.zoomLevel);
                    }
                    num2 += 10;
                }
            }
            List<string> list3 = new List<string>();
            int num3 = 32;
            if (Char.myCharz().charEffectTime != null && Char.myCharz().charEffectTime.HasAnyEffect())
            {
                if (Char.myCharz().charEffectTime.hasNRD)
                {
                    list3.Add("NRD còn: " + Char.myCharz().charEffectTime.timeHoldingNRD.ToString() + " giây");
                }
                if (Char.myCharz().charEffectTime.isTied)
                {
                    for (int i = 0; i < GameScr.vCharInMap.size(); i++)
                    {
                        Char @char = GameScr.vCharInMap.elementAt(i) as Char;
                        if (@char != null && @char != Char.myCharz() && @char.holder && @char.charHold == Char.myCharz())
                        {
                            list3.Add(string.Concat(new string[]
                            {
                                            "Bị ",
                                            @char.cName,
                                            " trói: ",
                                            Char.myCharz().charEffectTime.timeTied.ToString(),
                                            " giây"
                            }));
                        }
                    }
                }
            }
            foreach (string text3 in list3)
            {
                StringHandle.paint(mFont.tahoma_7b_yellow, g, text3, GameCanvas.w / 2, num3, mFont.CENTER, mFont.tahoma_7b_dark, "border", mGraphics.zoomLevel);
                num3 += 10;
            }
        }

        public static void addEffectCreatedByMe(Skill skill)
        {
            if (Char.myCharz().charFocus != null)
            {
                if (skill.template.id == 22)
                    Char.myCharz().charFocus.charEffectTime.isHypnotizedByMe = true;
                if (skill.template.id == 23)
                    Char.myCharz().charFocus.charEffectTime.isTiedByMe = true;
            }
            if (skill.template.id == 6 && mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill > skill.coolDown)
                Char.vItemTime.addElement(new ItemTime(717, CharExtensions.getTimeTDHS() - 1));
        }

    }
}