using System.Collections.Generic;

namespace Common
{
    public interface IContextMenuItemHandler
    {
        public List<ContextMenuItem> GetContextMenuItems();
    }
}
