using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace Ischool.Booking.Equipment.UDT
{
    /// <summary>
    /// 設備借出/歸還歷程
    /// </summary>
    [TableName("ischool.booking.equip_io_history")]
    class EquipmentIOHistory : ActiveRecord
    {
        /// <summary>
        /// 設備預約申請時段明細編號
        /// </summary>
        [Field(Field = "ref_app_detail_id", Indexed = false)]
        public int RefAppDetailID { get; set; }

        /// <summary>
        /// 借出時間
        /// </summary>
        [Field(Field = "borrow_time", Indexed = false)]
        public DateTime BorrowTime { get; set; }

        /// <summary>
        /// 歸還時間
        /// </summary>
        [Field(Field = "return_time", Indexed = false)]
        public DateTime ReturnTime { get; set; }
    }
}
