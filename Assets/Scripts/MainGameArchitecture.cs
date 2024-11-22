using CollectionManagement.Models;
using UnityEngine;

namespace QFramework
{

    public class MainGameArchitecture : Architecture<MainGameArchitecture>
    {
        protected override void Init()
        {
            //Model×¢²á
             this.RegisterModel<IItemInterface>(new ItemModel());

            // System ×¢²á 

        }
    }
}