using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FISCA.UDT;

namespace Ischool.Booking.Equipment.UDT
{
    /// <summary>
    /// 設備管理單位
    /// </summary>
    [TableName("ischool.booking.equip_units")]
    class EquipmentUnit:ActiveRecord
    {
        /// <summary>
        /// 管理單位名稱
        /// </summary>
        [Field(Field = "name", Indexed = false)]
        public string Name { get; set; }

        /// <summary>
        /// 建立日期
        /// </summary>
        [Field(Field = "create_time", Indexed = false)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 建立者帳號
        /// </summary>
        [Field(Field = "created_by", Indexed = false)]
        public string CreatedBy { get; set; }

    }
}
