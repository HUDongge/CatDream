using QFramework;

namespace CollectionManagement.Models
{
    public interface IItemInterface : IModel
    {
        int itemCount { get; set; } //收集到多少个毛线球
    }
    public class ItemModel : AbstractModel, IItemInterface
    {
        public int itemCount { get; set; }
        protected override void OnInit()
        {
            itemCount = 0;
        }
    }
}