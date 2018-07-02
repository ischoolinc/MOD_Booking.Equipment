using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Presentation;

namespace Ischool.Booking.Equipment
{
    class BookingEquipmentAdmin:BlankPanel
    {
        public BookingEquipmentAdmin()
        {
            Group = "設備預約";
        }

        private static BookingEquipmentAdmin _BookingEquipmentAdmin;

        public static BookingEquipmentAdmin Instance
        {
            get
            {
                if (_BookingEquipmentAdmin == null)
                {
                    _BookingEquipmentAdmin = new BookingEquipmentAdmin();
                }
                return _BookingEquipmentAdmin;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ContentPanePanel
            // 
            this.ContentPanePanel.Location = new System.Drawing.Point(0, 163);
            this.ContentPanePanel.Size = new System.Drawing.Size(870, 421);
            // 
            // BookingEquipmentAdmin
            // 
            this.Name = "BookingEquipmentAdmin";
            this.ResumeLayout(false);

        }
    }
}
