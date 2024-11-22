using CollectionManagement.Models;
using UnityEngine;

namespace QFramework
{

    public class MainGameArchitecture : Architecture<MainGameArchitecture>
    {
        protected override void Init()
        {
            //Modelע��
             this.RegisterModel<IItemInterface>(new ItemModel());

            // System ע�� 

        }
    }
}