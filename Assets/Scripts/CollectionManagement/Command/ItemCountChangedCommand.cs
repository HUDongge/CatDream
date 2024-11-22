using CollectionManagement.Models;
using QFramework;
using UnityEngine;

namespace CollectionManagement.Commands
{
    public class ItemCountChangedCommand : AbstractCommand
    {
        private int count;
        public ItemCountChangedCommand(int _count)
        {
            count = _count;
        }
        protected override void OnExecute()
        {
            var _itemModel = this.GetModel<IItemInterface>();
            _itemModel.itemCount = count;     
        }

    }

}