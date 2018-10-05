using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FISCA.UDT;

namespace Ischool.Booking.Equipment.UDT
{
    /// <summary>
    /// 設備
    /// </summary>
    [TableName("ischool.booking.equipment")]
    class Equipment : ActiveRecord
    {
        /// <summary>
        /// 照片URL
        /// </summary>
        [Field(Field = "picture", Indexed = false)]
        public string Picture { get; set; }

        /// <summary>
        /// 設備名稱
        /// </summary>
        [Field(Field = "name", Indexed = false)]
        public string Name { get; set; }

        /// <summary>
        /// 類別
        /// </summary>
        [Field(Field = "category", Indexed = false)]
        public string Category { get; set; }

        /// <summary>
        /// 財產編號 唯一編碼
        /// </summary>
        [Field(Field = "property_no", Indexed = false)]
        public string PropertyNo { get; set; }

        /// <summary>
        /// 廠牌
        /// </summary>
        [Field(Field = "company", Indexed = false)]
        public string Company { get; set; }

        /// <summary>
        /// 型號
        /// </summary>
        [Field(Field = "model_no", Indexed = false)]
        public string ModelNo { get; set; }

        /// <summary>
        /// 設備狀態 可預約，維修中，遺失，報廢。
        /// </summary>
        [Field(Field = "status", Indexed = false)]
        public string Status { get; set; }

        /// <summary>
        /// 未取用解除預約時間
        /// </summary>
        [Field(Field = "deadline", Indexed = false)]
        public int DeadLine { get; set; }

        /// <summary>
        /// 是否顯示
        /// </summary>
        [Field(Field = "is_able" , Indexed = false)]
        public bool IsAble { get; set; }

        /// <summary>
        /// 放置位置
        /// </summary>
        [Field(Field = "place", Indexed = false)]
        public string Place { get; set; }

        /// <summary>
        /// 管理單位編號
        /// </summary>
        [Field(Field = "ref_unit_id", Indexed = false)]
        public int RefUnitID { get; set; }

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
