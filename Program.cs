using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K12.Data.Configuration;
using FISCA;
using FISCA.UDT;
using FISCA.Presentation;
using FISCA.Presentation.Controls;
using K12.Data;
using FISCA.Data;
using System.Data;
using FISCA.Permission;
using Campus.DocumentValidator;

namespace Ischool.Booking.Equipment
{
    public class Program
    {
        /// <summary>
        /// 設備預約模組專用角色ID
        /// </summary>
        static public string _roleUnitAdminID;
        /// <summary>
        /// 設備預約模組專用角色名稱
        /// </summary>
        public static string _roleUnitAdminName = "設備預約單位管理員";
        /// <summary>
        /// 設備預約管理者角色名稱
        /// </summary>
        public static string _roleAdminName = "設備預約管理員";
        /// <summary>
        /// 設備預約管理者專用角色ID
        /// </summary>
        static public string _roleAdminID;
        /// <summary>
        /// 設備預約模組角色權限
        /// </summary>
        public static string _adminPermission = @"
<Permissions>
<Feature Code=""449AADA3-79B1-4062-8783-767DC9EEAA30"" Permission=""Execute""/>
<Feature Code=""DF736220-FB78-4F32-837D-87276F2B8421"" Permission=""Execute""/>
<Feature Code=""1BE6FCD7-F008-4E17-92C7-113ABAD7BEDF"" Permission=""Execute""/>
<Feature Code=""5EEC6C9D-40AD-432E-BC25-EEFF91A9438D"" Permission=""Execute""/>
<Feature Code=""A5E99F9B-A6AB-40A5-BB02-5BB8674C69F0"" Permission=""Execute""/>
<Feature Code=""81A459CC-50DB-4891-9728-26B5A4F9E9B2"" Permission=""Execute""/>
<Feature Code=""1C4BD840-DFF1-4CF0-A8F9-DC0462F4DC2A"" Permission=""Execute""/>
<Feature Code=""CC10DD70-5DF0-421C-9234-0A2FD0BC0B1F"" Permission=""Execute""/>
</Permissions>
";
        public static string _unitAdminPermission = @"
<Permissions>
<Feature Code=""449AADA3-79B1-4062-8783-767DC9EEAA30"" Permission=""Execute""/>
<Feature Code=""DF736220-FB78-4F32-837D-87276F2B8421"" Permission=""Execute""/>
<Feature Code=""1BE6FCD7-F008-4E17-92C7-113ABAD7BEDF"" Permission=""Execute""/>
<Feature Code=""5EEC6C9D-40AD-432E-BC25-EEFF91A9438D"" Permission=""Execute""/>
<Feature Code=""A5E99F9B-A6AB-40A5-BB02-5BB8674C69F0"" Permission=""Execute""/>
<Feature Code=""CC10DD70-5DF0-421C-9234-0A2FD0BC0B1F"" Permission=""Execute""/>
</Permissions>
";


        [MainMethod()]
        static public void Main()
        {
            // 匯入驗證規則
            FactoryProvider.FieldFactory.Add(new EquipmentFieldValidatorFactory());
            FactoryProvider.RowFactory.Add(new EquipmentRowValidatorFactory());

            #region Init UDT
            {
                ConfigData cd = K12.Data.School.Configuration["設備預約模組載入設定Version_1008"];

                bool checkUDT = false;
                string name = "設備預約UDT是否已載入";

                //如果尚無設定值,預設為
                if (string.IsNullOrEmpty(cd[name]))
                {
                    cd[name] = "false";
                }

                //檢查是否為布林
                bool.TryParse(cd[name], out checkUDT);

                if (!checkUDT)
                {
                    AccessHelper access = new AccessHelper();
                    access.Select<UDT.Equipment>("UID = '00000'");
                    access.Select<UDT.EquipmentUnit>("UID = '00000'");
                    access.Select<UDT.EquipmentUnitAdmin>("UID = '00000'");
                    access.Select<UDT.EquipmentApplication>("UID = '00000'");
                    access.Select<UDT.EquipmentApplicationDetail>("UID = '00000'");
                    access.Select<UDT.EquipmentIOHistory>("UID = '00000'");

                    cd[name] = "true";
                    cd.Save();
                }
            }
            #endregion

            #region 建立設備預約模組管理者專用角色
            {
                // 如果管理者角色不存在，建立角色並取回角色ID
                if (!DAO.Role.CheckIsRoleExist(_roleAdminName))
                {
                    _roleAdminID = DAO.Role.InsertRole(_roleAdminName,"",_adminPermission);
                }
                else // 更新角色權限
                {
                    DAO.Role.UpdateRole(_roleAdminID,_adminPermission);
                }
            }
            #endregion

            #region 建立設備預約單位管理員角色
            {
                // 如果專用角色不存在，建立角色並取回角色ID
                if (!DAO.Role.CheckIsRoleExist(_roleUnitAdminName))
                {
                    _roleUnitAdminID = DAO.Role.InsertRole(_roleUnitAdminName,"", _unitAdminPermission);   
                }
                else // 更新角色權限
                {
                    DAO.Role.UpdateRole(_roleUnitAdminID, _unitAdminPermission);
                }
            }
            #endregion

            // 取得登入帳號與身分
            Actor actor = Actor.Instance;

            // 建立設備預約分頁
            //MotherForm.AddPanel(BookingEquipmentAdmin.Instance);

            #region 設備預約
            {
                //2021-12-15 Cynthia 因原先操作指南無法在x64版本上作為背景載入，故參考俊威的意見，將操作指南另外用一個button點擊開啟。
                MotherForm.RibbonBarItems["設備預約", "使用說明"]["操作指南"].Image = Properties.Resources._03;
                MotherForm.RibbonBarItems["設備預約", "使用說明"]["操作指南"].Size = RibbonBarButton.MenuButtonSize.Large;
                #region 操作指南
                {
                    MotherForm.RibbonBarItems["設備預約", "使用說明"]["操作指南"].Click += delegate
                    {
                        System.Diagnostics.Process.Start("https://sites.google.com/ischool.com.tw/booking-equipment/%E9%A6%96%E9%A0%81");
                    };
                }
                #endregion

                MotherForm.RibbonBarItems["設備預約", "基本設定"]["設定管理單位"].Size = RibbonBarButton.MenuButtonSize.Medium;
                MotherForm.RibbonBarItems["設備預約", "基本設定"]["設定管理單位"].Image = Properties.Resources.meeting_config_64;
                MotherForm.RibbonBarItems["設備預約", "基本設定"]["設定單位管理員"].Size = RibbonBarButton.MenuButtonSize.Medium;
                MotherForm.RibbonBarItems["設備預約", "基本設定"]["設定單位管理員"].Image = Properties.Resources.foreign_language_config_64;
                MotherForm.RibbonBarItems["設備預約", "基本設定"]["設備管理"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["設備預約", "基本設定"]["設備管理"].Image = Properties.Resources.shopping_cart_config_64;
                MotherForm.RibbonBarItems["設備預約", "資料統計"]["匯出"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["設備預約", "資料統計"]["匯出"].Image = Properties.Resources.Export_Image;
                MotherForm.RibbonBarItems["設備預約", "資料統計"]["匯入"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["設備預約", "資料統計"]["匯入"].Image = Properties.Resources.Import_Image;
                MotherForm.RibbonBarItems["設備預約", "資料統計"]["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["設備預約", "資料統計"]["報表"].Image = Properties.Resources.Report;
                MotherForm.RibbonBarItems["設備預約", "設備出借作業"]["查詢出借紀錄"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["設備預約", "設備出借作業"]["查詢出借紀錄"].Image = Properties.Resources.barcode_zoom_128;
                MotherForm.RibbonBarItems["設備預約", "設備出借作業"]["設備出借/歸還"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["設備預約", "設備出借作業"]["設備出借/歸還"].Image = Properties.Resources.barcode_ok_128;
                
                #region 設定管理單位
                MotherForm.RibbonBarItems["設備預約", "基本設定"]["設定管理單位"].Enable = Permissions.設定管理單位權限;
                MotherForm.RibbonBarItems["設備預約", "基本設定"]["設定管理單位"].Click += delegate
                {
                    ManagementUnit form = new ManagementUnit();
                    form.ShowDialog();
                };
                #endregion

                #region 設定單位管理員
                MotherForm.RibbonBarItems["設備預約", "基本設定"]["設定單位管理員"].Enable = Permissions.設定單位管理員權限;
                MotherForm.RibbonBarItems["設備預約", "基本設定"]["設定單位管理員"].Click += delegate
                {
                    SetUnitAdmin form = new SetUnitAdmin();
                    form.ShowDialog();
                };
                #endregion

                #region 設備管理
                MotherForm.RibbonBarItems["設備預約", "基本設定"]["設備管理"].Enable = Permissions.管理設備權限;
                MotherForm.RibbonBarItems["設備預約", "基本設定"]["設備管理"].Click += delegate
                {
                    ManageEquipment form = new ManageEquipment();
                    form.ShowDialog();
                };
                #endregion

                #region 資料統計

                #region 匯出設備清單
                MotherForm.RibbonBarItems["設備預約", "資料統計"]["匯出"]["匯出設備清單"].Enable = Permissions.匯出設備清單權限;
                MotherForm.RibbonBarItems["設備預約", "資料統計"]["匯出"]["匯出設備清單"].Click += delegate
                {
                    ExportEquipmentForm form = new ExportEquipmentForm();
                    form.ShowDialog();
                };
                #endregion

                #region 匯入設備清單
                MotherForm.RibbonBarItems["設備預約", "資料統計"]["匯入"]["匯入設備清單"].Enable = Permissions.匯入設備清單權限;
                MotherForm.RibbonBarItems["設備預約", "資料統計"]["匯入"]["匯入設備清單"].Click += delegate
                {
                    new ImportEquipmentData().Execute();
                };
                #endregion

                #region 統計設備使用狀況
                MotherForm.RibbonBarItems["設備預約", "資料統計"]["報表"]["統計設備使用狀況"].Enable = Permissions.統計設備使用狀況權限;
                MotherForm.RibbonBarItems["設備預約", "資料統計"]["報表"]["統計設備使用狀況"].Click += delegate
                {
                    StatisticalTableForm form = new StatisticalTableForm();
                    form.ShowDialog();
                };
                #endregion

                #endregion

                #region 查詢設備出借紀錄
                {
                    MotherForm.RibbonBarItems["設備預約", "設備出借作業"]["查詢出借紀錄"].Enable = Permissions.查詢出借紀錄權限;
                    MotherForm.RibbonBarItems["設備預約", "設備出借作業"]["查詢出借紀錄"].Click += delegate
                    {
                        (new frmSearchApplication()).ShowDialog();
                    };
                }
                #endregion

                #region 設備出借/歸還
                MotherForm.RibbonBarItems["設備預約", "設備出借作業"]["設備出借/歸還"].Enable = Permissions.設備出借歸還權限;
                MotherForm.RibbonBarItems["設備預約", "設備出借作業"]["設備出借/歸還"].Click += delegate
                {
                    BorrowEquipmentForm form = new BorrowEquipmentForm();
                    form.ShowDialog();
                };
                #endregion
            }
            #endregion

            #region 權限管理
            Catalog detail = new Catalog();
            detail = RoleAclSource.Instance["設備預約"]["功能按鈕"];
            detail.Add(new RibbonFeature(Permissions.管理單位, "設定管理單位"));
            detail.Add(new RibbonFeature(Permissions.設備單位管理員, "設定設備單位管理員"));
            detail.Add(new RibbonFeature(Permissions.管理設備, "管理設備"));
            detail.Add(new RibbonFeature(Permissions.匯出設備清單, "匯出設備清單"));
            detail.Add(new RibbonFeature(Permissions.匯入設備清單, "匯入設備清單"));
            detail.Add(new RibbonFeature(Permissions.統計設備使用狀況, "統計設備使用狀況"));
            detail.Add(new RibbonFeature(Permissions.設備出借歸還, "設備出借歸還"));
            detail.Add(new RibbonFeature(Permissions.查詢出借紀錄, "查詢出借紀錄"));
            #endregion

        }
    }
}
