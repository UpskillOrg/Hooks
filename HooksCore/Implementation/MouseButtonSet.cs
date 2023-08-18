using System.Windows.Forms;

namespace HooksCore.Implementation
{
    internal class MouseButtonSet
    {
        private MouseButtons m_Set;

        public MouseButtonSet()
        {
            m_Set = MouseButtons.None;
        }

        public void Add(MouseButtons element)
        {
            m_Set |= element;
        }

        public void Remove(MouseButtons element)
        {
            m_Set &= ~element;
        }

        public bool Contains(MouseButtons element)
        {
            return (m_Set & element) != MouseButtons.None;
        }
    }
}
