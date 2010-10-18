using System;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MyZilla.UI
{
    public delegate bool PreRemoveTab(int index);

    public delegate void RemovingTabEventHandler(object sender, int index);

    public class TabControlExtended : TabControl
    {
        public TabControlExtended()
            : base()
        {
            this.DrawMode = TabDrawMode.OwnerDrawFixed ;
        }

        public PreRemoveTab PreRemoveTabPage;

        public event RemovingTabEventHandler OnRemoveTabEventHandler;

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Rectangle r = e.Bounds;
            r = GetTabRect(e.Index);
            //
            r.Offset(r.Width - 12, 5);
            r.Width = 7;
            r.Height = 7;
            e.Graphics.DrawImage(Properties.Resources.close_tab, r);

            r = GetTabRect(e.Index);
            r.Offset(4, 2);
            string titel = this.TabPages[e.Index].Text;
            if (titel.Length > 5)
                titel = titel.Substring(0, titel.Length - 5); ///de lete the 5 '*' added to enlarge the tab header
            Font f = this.Font;
            Brush b = new SolidBrush(Color.Black);
            e.Graphics.DrawString(titel, f, b, new PointF(r.X, r.Y));

        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Point p = e.Location;
            for (int i = 0; i < TabCount; i++)
            {
                Rectangle r = GetTabRect(i);
                r.Offset(r.Width - 12, 5);
                r.Width = 7;
                r.Height = 7;
                if (r.Contains(p))
                {
                    CloseTab(i);
                }
            }
        }

        private void CloseTab(int i)
        {
            if (PreRemoveTabPage != null)
            {
                bool closeIt = PreRemoveTabPage(i);
                if (!closeIt)
                    return;
            }
            //raise remove tab event
            if (OnRemoveTabEventHandler != null)
                OnRemoveTabEventHandler(null, i);

            TabPages.Remove(TabPages[i]);
        }
    }
}
