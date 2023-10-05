using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class HintCommand
    {
        private HintCommand()
        {
            hints_default = (from h in hints_default
                                  orderby h[0]
                                  select h).ToList<string[]>();
        }

        internal static HintCommand gI { get; } = new HintCommand();

        internal int maxWidth
        {
            get
            {
                if (mGraphics.zoomLevel <= 1)
                {
                    return 350;
                }
                return 280;
            }
        }

        internal int lenghtHintsShow
        {
            get
            {
                if (hints.Count >= 10)
                {
                    return 10;
                }
                return hints.Count;
            }
        }

        public void show()
        {
            isShow = true;
            selectedIndex = 0;
            scrollValue = 0;
            chatBack = null;
            hints = hints_default;
        }

        public void update()
        {
            isShow = ChatTextField.gI().isShow;
            if (!isShow)
            {
                hints = null;
                return;
            }
            TField tfChat = ChatTextField.gI().tfChat;
            if (tfChat.getText().Length == 0 || tfChat.getText()[0] != '/')
            {
                chatBack = null;
                isShow = false;
                return;
            }
            if (chatBack != tfChat.getText())
            {
                selectedIndex = 0;
                scrollValue = 0;
                chatBack = tfChat.getText();
                string cmd = chatBack.Substring(1);
                hints = hints_default.FindAll((string[] h) => h[0].Contains(cmd) || h[1].Contains(cmd));
            }
            if (GameCanvas.keyPressed[22])
            {
                selectedIndex++;
                if (selectedIndex > hints.Count - 1)
                {
                    selectedIndex = 0;
                }
                GameCanvas.keyPressed[22] = false;
                GameCanvas.clearKeyPressed();
                GameCanvas.clearKeyHold();
            }
            else if (GameCanvas.keyPressed[21])
            {
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = hints.Count - 1;
                }
                GameCanvas.keyPressed[21] = false;
                GameCanvas.clearKeyPressed();
                GameCanvas.clearKeyHold();
            }
            else if (GameCanvas.keyPressed[16])
            {
                if (chatBack != hints[selectedIndex][0])
                {
                    tfChat.setText(hints[selectedIndex][0]);
                }
                GameCanvas.keyPressed[16] = false;
                GameCanvas.clearKeyPressed();
                GameCanvas.clearKeyHold();
            }
            if (selectedIndex >= scrollValue + 10)
            {
                scrollValue = selectedIndex - 9;
            }
            if (selectedIndex < scrollValue)
            {
                scrollValue = selectedIndex;
            }
        }

        internal void paint(mGraphics g)
        {
            if (isShow && !string.IsNullOrEmpty(chatBack))
            {
                ChatTextField chatTextField = ChatTextField.gI();
                height = (lenghtHintsShow + 1) * 10;
                width = ((GameCanvas.w - 10 > maxWidth) ? maxWidth : (GameCanvas.w - 10));
                int h = lenghtHintsShow * (height - 10) / hints.Count;
                x = (GameCanvas.w - width) / 2;
                y = chatTextField.tfChat.y - 40 - height;
                g.setColor(0, 0.5f);
                g.fillRect(x, y, width, height);
                g.setColor(0, 1f);
                g.fillRect(x, y, width, 10);
                int num = x + width - mFont.tahoma_7_white.getWidth("Nhấn Tab để lựa chọn") - 5;
                mFont.tahoma_7_white.drawString(g, "Nhấn Tab để lựa chọn", num, y, 0);
                g.setColor(16777215, 0.5f);
                g.fillRect(x, y + 10 - 1, width, 1);
                g.setColor(8618883, 0.75f);
                g.fillRect(x, y + 10 + 10 * (selectedIndex - scrollValue), width - 5, 10);
                g.setColor(16777215, 0.75f);
                g.fillRect(x, y + 10 + 10 * (selectedIndex - scrollValue), 2, 10);
                g.setColor(16777215, 0.75f);
                g.fillRect(x + width - 5, y + 10, 1, height - 10);
                g.setColor(16777215, 0.75f);
                g.fillRect(x + width - 3, y + 10 + scrollValue * (height - 10) / hints.Count, 2, h);
                for (int i = scrollValue; i < scrollValue + lenghtHintsShow; i++)
                {
                    mFont.tahoma_7_white.drawString(g, hints[i][0] + " - " + hints[i][1], x + 5, y + 10 + 10 * (i - scrollValue), 0);
                }
            }
        }

        internal int selectedIndex;

        internal bool isShow;

        internal int scrollValue;

        internal int width;

        internal int height;

        internal int x;

        internal int y;

        private string chatBack = string.Empty;

        private List<string[]> hints;

        private List<string[]> hints_default = new List<string[]>
        {
            new string[]
            {
                "/tbb",
                "Hiển thị thông tin Boss xuất hiện"
            },
            new string[]
            {
                "/kssp",
                "KS SUPER BROLY"
            },
            new string[]
            {
                "/dmt",
                "Auto chuyển mục tiêu - Dùng để up bí kiếp"
            },
            new string[]
            {
                "/abt",
                "Auto dùng bông tai khi HP dưới 50%"
            },
            new string[]
            {
                "/cd",
                "Auto cho đậu"
            },
            new string[]
            {
                "/td",
                "Auto thu đậu và cất đậu vào rương đồ"
            },
            new string[]
            {
                "/xd",
                "Auto xin đậu"
            },
            new string[]
            {
                "/axd",
                "Auto spam xin đậu thoát ra vào lại liên tục"
            },
            new string[]
            {
                "/akok",
                "Auto jump - Hỗ trợ up đệ kaioken - Chống mất kết nối game"
            },
            new string[]
            {
                "/dtnsq",
                "Up đệ tử né siêu quái - Đệ đi theo sư phụ và đánh quái"
            },
            new string[]
            {
                "/aflag",
                "Auto bật cờ xám"
            },
            new string[]
            {
                "/petgohome",
                "Auto cho đệ về nhà khi tách hợp thể"
            },
            new string[]
            {
                "/ttdt",
                "Hiển thị thông tin - chỉ số đệ tử"
            },
            new string[]
            {
                "/addc",
                "Thêm nhân vật đang trỏ vào d/s trị thương đặc biệt"
            },
            new string[]
            {
                "/arc",
                "Auto trị thương theo d/s trị thương đặc biệt"
            },
            new string[]
            {
                "/ahs",
                "Auto dùng ngọc để hồi sinh"
            },
            new string[]
            {
                "/phpX",
                "Auto dùng đậu thần khi đệ tử dưới X% HP"
            },
            new string[]
            {
                "/hpX",
                "Auto cộng chỉ số lên X HPG"
            },
            new string[]
            {
                "/hpX",
                "Auto cộng chỉ số lên X HPG"
            },
            new string[]
            {
                "/kiX",
                "Auto cộng chỉ số lên X KIG"
            },
            new string[]
            {
                "/sdX",
                "Auto cộng chỉ số lên X SĐG"
            },
            new string[]
            {
                "/akX",
                "Auto dùng skill sau X mili giây [1 giây = 1000ms], X=0 => Tắt"
            },
            new string[]
            {
                "/hskill",
                "Auto di chuyển đến DHVT23 để hồi skill"
            },
            new string[]
            {
                "/kzX",
                "Auto chen vào khu X khi khu có dư slot"
            },
            new string[]
            {
                "/lcm",
                "Khóa không cho nhân vật chuyển map"
            },
            new string[]
            {
                "/lcz",
                "Khóa không cho nhân vật chuyển khu"
            },
            new string[]
            {
                "/cX",
                "Chỉnh tốc độ chạy thành X [Mặc định 7]"
            },
            new string[]
            {
                "/sX",
                "Chỉnh tốc độ game thành X [Mặc định 2]"
            },
            new string[]
            {
                "/rX",
                "Dịch phải X đơn vị"
            },
            new string[]
            {
                "/lX",
                "Dịch trái X đơn vị"
            },
            new string[]
            {
                "/uX",
                "Dịch trên X đơn vị"
            },
            new string[]
            {
                "/dX",
                "Dịch phải X đơn vị"
            },
            new string[]
            {
                "/frzX",
                "Fake time hồi chiêu thành X mili giây, X = 0 ==> Đóng băng skill"
            },
            new string[]
            {
                "/spos",
                "Lưu vị trí khi Goback"
            },
            new string[]
            {
                "/szone",
                "Lưu khu khi Goback"
            },
            new string[]
            {
                "/smap",
                "Lưu map khi Goback"
            },
            new string[]
            {
                "/autoitem",
                "Auto Item theo danh sách"
            },
            new string[]
            {
                "/quay",
                "Mở menu quay vòng quay thượng đế"
            },
            new string[]
            {
                "/alg",
                "Auto đăng nhập lại khi mất kết nối"
            },
            new string[]
            {
                "/xmp",
                "Mở Menu Xmap"
            },
            new string[]
            {
                "/add",
                "Thêm quái ở vị trí đang trỏ vào vị trí quái tàn sát"
            },
            new string[]
            {
                "/addt",
                "Thêm loại quái đang trỏ vào d/s loại quái tàn sát"
            },
            new string[]
            {
                "/anhat",
                "Auto nhặt vật phẩm"
            },
            new string[]
            {
                "/itm",
                "Lọc chỉ nhặt vật phẩm của bản thân"
            },
            new string[]
            {
                "/addiX",
                "Thêm idItem X vào d/s chỉ nhặt item theo ID"
            },
            new string[]
            {
                "/blockiX",
                "Thêm idItem X vào d/s không nhặt item"
            },
            new string[]
            {
                "/addtiX",
                "Thêm loại item X vào d/s nhặt theo loại item"
            },
            new string[]
            {
                "/clri",
                "Xóa danh sách nhặt item"
            },
            new string[]
            {
                "/cnn",
                "Lọc chỉ nhặt ngọc hồng, ngọc xanh"
            },
            new string[]
            {
                "/ts",
                "Tàn sát quái"
            },
            new string[]
            {
                "/nsq",
                "Tàn sát né siêu quái"
            },
            new string[]
            {
                "/addmX",
                "Thêm quái id X vào d/s ID quái tàn sát"
            },
            new string[]
            {
                "/addtmX",
                "Thêm loại quái X vào d/s tàn sát theo loại quái"
            },
            new string[]
            {
                "/clrm",
                "Xóa danh sách quái tàn sát"
            },
            new string[]
            {
                "/clrs",
                "Xóa danh sách skill tàn sát"
            },
            new string[]
            {
                "/skill",
                "Thêm skill đang trỏ vào d/s skill tàn sát"
            },
            new string[]
            {
                "/skillX",
                "Thêm skill ở vị trí X trong mục chỉ số vào d/s skill tàn sát"
            },
            new string[]
            {
                "/abfX Y",
                "Auto dùng đậu thần khi HP, KI lần lượt dưới X%, Y%"
            },
            new string[]
            {
                "/xmpX",
                "Auto di chuyển tới map id X"
            },
            new string[]
            {
                "/csb",
                "Bật/Tắt sử dụng capsule thường khi Xmap"
            },
            new string[]
            {
                "/csdb",
                "Bật/Tắt sử dụng capsule đặc biệt khi Xmap"
            },
            new string[]
            {
                "/kmtX",
                "Khóa mục tiêu ID X"
            }
        };
    }
}
