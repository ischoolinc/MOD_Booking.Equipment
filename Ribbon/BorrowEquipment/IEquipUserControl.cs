using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ischool.Booking.Equipment.Ribbon.BorrowEquipment
{
    interface IEquipUserControl
    {
        void SetEquipID(String equipID);
        void SetVisible(bool isVisible);
    }
}
