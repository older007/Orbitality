using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace BattleSystem
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private RocketView rocketView;
        [SerializeField] private Transform rocketViewHolder;
        
        private List<RocketView> rocketViews = new List<RocketView>();
        
        public List<RocketView> CreateViews(IEnumerable<RocketData> data)
        {
            foreach (var rocketData in data)
            {
                var item = Instantiate(rocketView, rocketViewHolder);

                item.SetData(rocketData);
                
                rocketViews.Add(item);
            }

            return rocketViews;
        }

        public void ShowChosenWeapon(int index)
        {
            
        }
    }
}