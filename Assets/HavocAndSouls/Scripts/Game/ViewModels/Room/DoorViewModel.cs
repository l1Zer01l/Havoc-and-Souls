/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using HavocAndSouls.Infrastructure.MVVM;
using HavocAndSouls.Infrastructure.Reactive;

namespace HavocAndSouls
{
    public class DoorViewModel : IDoorViewModel
    {
        public ReactiveProperty<bool> IsOpenDoor { get; private set; } = new ();

        public DoorViewModel()
        {
            IsOpenDoor.SetValue(null, false);
        }

        public void Dispose()
        {

        }

        [ReactiveMethod]
        public void CloseDoor(object sender)
        {
            IsOpenDoor.SetValue(null, false);
        }

        [ReactiveMethod]
        public void OpenDoor(object sender)
        {
            IsOpenDoor.SetValue(null, true);   
        }
    }
}
