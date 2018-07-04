using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ischool.Booking.Equipment
{
    class Permissions
    {
        public static string 設備管理單位 { get { return "1C4BD840-DFF1-4CF0-A8F9-DC0462F4DC2A"; } }
        public static bool 設定設備管理單位權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[設備管理單位].Executable;
            }
        }

        public static string 設備單位管理員 { get { return "81A459CC-50DB-4891-9728-26B5A4F9E9B2"; } }
        public static bool 設定設備單位權管理員權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[設備單位管理員].Executable;
            }
        }

        public static string 管理設備 { get { return "A5E99F9B-A6AB-40A5-BB02-5BB8674C69F0"; } }
        public static bool 管理設備權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[管理設備].Executable;
            }
        }

        public static string 匯出設備清單 { get { return "5EEC6C9D-40AD-432E-BC25-EEFF91A9438D"; } }
        public static bool 匯出設備清單權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[匯出設備清單].Executable;
            }
        }

        public static string 匯入設備清單 { get { return "1BE6FCD7-F008-4E17-92C7-113ABAD7BEDF"; } }
        public static bool 匯入設備清單權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[匯入設備清單].Executable;
            }
        }

        public static string 統計設備使用狀況 { get { return "DF736220-FB78-4F32-837D-87276F2B8421"; } }
        public static bool 統計設備使用狀況權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[統計設備使用狀況].Executable;
            }
        }
    }
}
