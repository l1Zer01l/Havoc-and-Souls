/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.MVVM;
using HavocAndSouls.Infrastructure.MVVM.Binders;
using HavocAndSouls.Infrastructure.Reactive;
using UnityEngine;

namespace HavocAndSouls
{
    public class SpawnRoomBinder : ObservableBinder<BaseRoomViewModel>
    {
        [SerializeField] private Transform m_parentTransformSpawnedRoom;

        [SerializeField] private float m_roomSize = 1;
        [SerializeField] private Vector2 m_startPosition = new Vector2();

        [SerializeField] private QuadrupleRoomView m_QuadrupleRoomView;
        protected override void OnPropertyChanged(object sender, BaseRoomViewModel newValue)
        {
            
        }

        protected override IBinding BindInternal(IViewModel viewModel)
        {
            return BindCollection(PropertyName, viewModel, OnAdded, OnRemoved, OnClear);
        }

        private void OnAdded(BaseRoomViewModel viewModel)
        {
            var newPosition = new Vector3(viewModel.position.x * m_roomSize + m_startPosition.x, 0, viewModel.position.y * m_roomSize + m_startPosition.y);
            if (viewModel.typeRoom.Equals(typeof(QuadrupleRoomViewModel)))
            { 
                var newRoomView = Instantiate(m_QuadrupleRoomView, newPosition, Quaternion.Euler(viewModel.AngleRotationRoom.Value), m_parentTransformSpawnedRoom);
                newRoomView.Bind(viewModel);
            }
        }

        private void OnRemoved(BaseRoomViewModel viewModel)
        {
            
        }

        private void OnClear()
        {
            
        }

    }
}
