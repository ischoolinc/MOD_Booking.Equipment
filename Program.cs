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
        static public string _roleID;

        /// <summary>
        /// 設備預約管理者專用角色ID
        /// </summary>
        static public string _roleAdminID;

        Actor actor = Actor.Instance;

        [MainMethod()]
        static public void Main()
        {
            //驗證規則
            FactoryProvider.FieldFactory.Add(new EquipmentFieldValidatorFactory());
            FactoryProvider.RowFactory.Add(new EquipmentRowValidatorFactory());

            #region Init UDT
            ConfigData cd = K12.Data.School.Configuration["設備預約模組載入設定"];

            bool checkUDT = false;
            string name = "設備預約UDT是否已載入";

            //如果尚無設定值,預設為
            if (string.IsNullOrEmpty(cd[name]))
            {
                cd[name] = "false";
            }

            //檢查是否為布林
            bool.TryParse(cd[name], out checkUDT);

            if (!checkUDT) //
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
            #endregion

            #region permission

            string permission = @"
<Permissions>
<Feature Code=""ischool.System.UDQ.CreateQuery"" Permission=""None""/>
<Feature Code=""System0060"" Permission=""None""/><Feature Code=""System0050"" Permission=""None""/>
<Feature Code=""System0040"" Permission=""None""/><Feature Code=""System0030"" Permission=""None""/>
<Feature Code=""SelectFlagStudent.DDA9F584-1303-4A04-851F-CBC093F24ADA"" Permission=""None""/>
<Feature Code=""K12.獎懲統計表"" Permission=""None""/>
<Feature Code=""K12.Student.StudentDemeritClear"" Permission=""None""/>
<Feature Code=""K12.缺曠統計表"" Permission=""None""/>
<Feature Code=""SHSchool.Behavior.StuAdmin.TeacherDiffOpenConfig"" Permission=""None""/>
<Feature Code=""SHSchool.Behavior.StuAdmin.DisciplineStatistics"" Permission=""None""/>
<Feature Code=""JHSchool.StuAdmin.Ribbon0060"" Permission=""None""/>
<Feature Code=""K12.Student.AttendanceEditForm"" Permission=""None""/>
<Feature Code=""K12.Student.DisciplineForm"" Permission=""None""/>
<Feature Code=""K12.Student.ReduceForm"" Permission=""None""/>
<Feature Code=""K12.Student.PeriodConfigForm"" Permission=""None""/>
<Feature Code=""K12.Student.AbsenceConfigForm"" Permission=""None""/>
<Feature Code=""Button0780"" Permission=""None""/>
<Feature Code=""Button0765"" Permission=""None""/>
<Feature Code=""Button0760"" Permission=""None""/>
<Feature Code=""Button0705"" Permission=""None""/>
<Feature Code=""K12SwapAttendStudent"" Permission=""None""/>
<Feature Code=""K12.EduAdminDataMapping.NationalityMappingForm"" Permission=""None""/>
<Feature Code=""SHSchool.EduAdmin.Ribbon0070"" Permission=""None""/>
<Feature Code=""Button0870"" Permission=""None""/>
<Feature Code=""Button0860"" Permission=""None""/>
<Feature Code=""Button0850"" Permission=""None""/>
<Feature Code=""Button0840"" Permission=""None""/>
<Feature Code=""Button0830"" Permission=""None""/>
<Feature Code=""高中系統 / 匯出評量成績"" Permission=""None""/>
<Feature Code=""高中系統 / 匯入評量成績"" Permission=""None""/>
<Feature Code=""Button08201"" Permission=""None""/>
<Feature Code=""Button0820"" Permission=""None""/>
<Feature Code=""Button0720"" Permission=""None""/>
<Feature Code=""Button0810"" Permission=""None""/>
<Feature Code=""Button0800"" Permission=""None""/>
<Feature Code=""Button0795"" Permission=""None""/>
<Feature Code=""Button0790"" Permission=""None""/>
<Feature Code=""Button0690"" Permission=""None""/>
<Feature Code=""Button0680"" Permission=""None""/>
<Feature Code=""Button0670"" Permission=""None""/>
<Feature Code=""Button0660"" Permission=""None""/>
<Feature Code=""Report0290"" Permission=""None""/>
<Feature Code=""Content0210"" Permission=""None""/>
<Feature Code=""Content0200"" Permission=""None""/>
<Feature Code=""Sunset.Ribbon0130"" Permission=""None""/>
<Feature Code=""Button0610"" Permission=""None""/>
<Feature Code=""Button0600"" Permission=""None""/>
<Feature Code=""Button0590"" Permission=""None""/>
<Feature Code=""Button0580"" Permission=""None""/>
<Feature Code=""Button0570"" Permission=""None""/>
<Feature Code=""Button0560"" Permission=""None""/>
<Feature Code=""Button0550"" Permission=""None""/>
<Feature Code=""Button0540"" Permission=""None""/>
<Feature Code=""Button0530"" Permission=""None""/>
<Feature Code=""Button0520"" Permission=""None""/>
<Feature Code=""Teacher.Field.聯絡電話"" Permission=""None""/>
<Feature Code=""Teacher.Field.身分證號"" Permission=""None""/>
<Feature Code=""Content0190"" Permission=""None""/>
<Feature Code=""Content0180"" Permission=""None""/>
<Feature Code=""Content0170"" Permission=""None""/>
<Feature Code=""K12DeleteTeacher"" Permission=""None""/>
<Feature Code=""Button0500"" Permission=""None""/>
<Feature Code=""Button0490"" Permission=""None""/>
<Feature Code=""Button0480"" Permission=""None""/>
<Feature Code=""Button0470"" Permission=""None""/>
<Feature Code=""Button0460"" Permission=""None""/>
<Feature Code=""Button0450"" Permission=""None""/>
<Feature Code=""SHSchool.SemesterMoralScoreCalcForm"" Permission=""None""/>
<Feature Code=""SHSchool.Behavior.Class.KeptInSchoolAnAdviceNote"" Permission=""None""/>
<Feature Code=""Report0240"" Permission=""None""/>
<Feature Code=""Report0250"" Permission=""None""/>
<Feature Code=""Report0260"" Permission=""None""/>
<Feature Code=""SHSchool.Behavior.Class.DisciplineNotificationForm"" Permission=""None""/>
<Feature Code=""Behavior.Class.ClearDemeritReport"" Permission=""None""/>
<Feature Code=""Report0230"" Permission=""None""/>
<Feature Code=""JHSchool.Class.Report0040"" Permission=""None""/>
<Feature Code=""K12.Behavior.Class.AbsenceNotificationSelectDateRangeForm"" Permission=""None""/>
<Feature Code=""Report0190"" Permission=""None""/>
<Feature Code=""Report0280"" Permission=""None""/>
<Feature Code=""Report0270"" Permission=""None""/>
<Feature Code=""Report0220"" Permission=""None""/>
<Feature Code=""Report0180"" Permission=""None""/>
<Feature Code=""Report0170"" Permission=""None""/>
<Feature Code=""Report0160"" Permission=""None""/>
<Feature Code=""Report0155"" Permission=""None""/>
<Feature Code=""Report0150"" Permission=""None""/>
<Feature Code=""Report0145"" Permission=""None""/>
<Feature Code=""Report0140"" Permission=""None""/>
<Feature Code=""Report0130"" Permission=""None""/>
<Feature Code=""Report0120"" Permission=""None""/>
<Feature Code=""Report0110"" Permission=""None""/>
<Feature Code=""Report0100"" Permission=""None""/>
<Feature Code=""Report0090"" Permission=""None""/>
<Feature Code=""Report0080"" Permission=""None""/>
<Feature Code=""Report0070"" Permission=""None""/>
<Feature Code=""Content0160"" Permission=""None""/>
<Feature Code=""Content0150"" Permission=""None""/>
<Feature Code=""JHBehavior.Class.Ribbon0210"" Permission=""None""/>
<Feature Code=""Button0430"" Permission=""None""/>
<Feature Code=""Button0420"" Permission=""None""/>
<Feature Code=""Button0410"" Permission=""None""/>
<Feature Code=""Button0400"" Permission=""None""/>
<Feature Code=""Button0390"" Permission=""None""/>
<Feature Code=""Button0380"" Permission=""None""/>
<Feature Code=""Button0375"" Permission=""None""/>
<Feature Code=""Button0370"" Permission=""None""/>
<Feature Code=""Button0365"" Permission=""None""/>
<Feature Code=""Button0360"" Permission=""None""/>
<Feature Code=""Button0350"" Permission=""None""/>
<Feature Code=""Button0340"" Permission=""None""/>
<Feature Code=""Button0330"" Permission=""None""/>
<Feature Code=""Student.Field.聯絡電話"" Permission=""None""/>
<Feature Code=""Student.Field.戶籍電話"" Permission=""None""/>
<Feature Code=""Student.Field.身分證號"" Permission=""None""/>
<Feature Code=""SHSchool.Behavior.Student.KeptInSchoolAnAdviceNote"" Permission=""None""/>
<Feature Code=""SHSchool.DailyManifestation"" Permission=""None""/>
<Feature Code=""K12.Student.SelectMeritDemeritForm"" Permission=""None""/>
<Feature Code=""K12.Student.SelectAttendanceForm"" Permission=""None""/>
<Feature Code=""SHSchool.Behavior.Student.DisciplineNotificationForm"" Permission=""None""/>
<Feature Code=""Behavior.Student.ClearDemeritReport"" Permission=""None""/>
<Feature Code=""JHSchool.Student.Report0030"" Permission=""None""/>
<Feature Code=""K12.Behavior.Student.AbsenceNotificationSelectDateRangeForm"" Permission=""None""/>
<Feature Code=""Report0030"" Permission=""None""/>
<Feature Code=""SHSchool.Behavior.Student.SelectMeritForm"" Permission=""None""/>
<Feature Code=""Report0060"" Permission=""None""/>
<Feature Code=""Report0055"" Permission=""None""/>
<Feature Code=""Report0050"" Permission=""None""/>
<Feature Code=""Report0045"" Permission=""None""/>
<Feature Code=""Report0040"" Permission=""None""/>
<Feature Code=""K12.Student.AttendanceItem"" Permission=""None""/>
<Feature Code=""K12.Student.DemeritItem"" Permission=""None""/>
<Feature Code=""K12.Student.MeritItem"" Permission=""None""/>
<Feature Code=""Content0155"" Permission=""None""/>
<Feature Code=""Content0165.5"" Permission=""None""/>
<Feature Code=""Content0145"" Permission=""None""/>
<Feature Code=""Content0135"" Permission=""None""/>
<Feature Code=""Content0130"" Permission=""None""/>
<Feature Code=""Content0120"" Permission=""None""/>
<Feature Code=""Content0110"" Permission=""None""/>
<Feature Code=""Content0100"" Permission=""None""/>
<Feature Code=""Content0090"" Permission=""None""/>
<Feature Code=""Content0050"" Permission=""None""/>
<Feature Code=""Content0040"" Permission=""None""/>
<Feature Code=""Content0030"" Permission=""None""/>
<Feature Code=""Content0020"" Permission=""None""/>
<Feature Code=""Content0010"" Permission=""None""/>
<Feature Code=""JHSchool.Student.Ribbon0130"" Permission=""None""/>
<Feature Code=""JHSchool.Student.Ribbon0120"" Permission=""None""/>
<Feature Code=""K12BatchStudentSemesterHistory"" Permission=""None""/>
<Feature Code=""K12.Student.SpeedAddToTemp.0412"" Permission=""None""/>
<Feature Code=""K12DeleteStudentPhoto"" Permission=""None""/>
<Feature Code=""K12DeleteStudentData"" Permission=""None""/>
<Feature Code=""SHSchool.Behavior.Student.Import.Comment"" Permission=""None""/>
<Feature Code=""SHSchool.Behavior.Student.Export.Comment"" Permission=""None""/>
<Feature Code=""SHSchool.Behavior.Student.Import.MoralScore"" Permission=""None""/>
<Feature Code=""SHSchool.Behavior.Student.Export.MoralScore"" Permission=""None""/>
<Feature Code=""K12.Student.TestSingleEditor5"" Permission=""None""/>
<Feature Code=""K12.Student.AttendanceForm"" Permission=""None""/>
<Feature Code=""K12.Behavior.BatchClearDemerit.010"" Permission=""None""/>
<Feature Code=""Button0260"" Permission=""None""/>
<Feature Code=""Button0270"" Permission=""None""/>
<Feature Code=""Button0180"" Permission=""None""/>
<Feature Code=""Button0190"" Permission=""None""/>
<Feature Code=""K12.Student.DemeritEditForm"" Permission=""None""/>
<Feature Code=""K12.Student.MeritEditForm"" Permission=""None""/>
<Feature Code=""高中系統/匯入評量成績"" Permission=""None""/>
<Feature Code=""高中系統/匯出評量成績"" Permission=""None""/>
<Feature Code=""SHSchool.Student.Ribbon0171"" Permission=""None""/>
<Feature Code=""SHSchool.Student.Ribbon0170"" Permission=""None""/>
<Feature Code=""SHSchool.Student.Ribbon0169"" Permission=""None""/>
<Feature Code=""Button0310"" Permission=""None""/>
<Feature Code=""Button0300"" Permission=""None""/>
<Feature Code=""Button0250"" Permission=""None""/>
<Feature Code=""Button0240"" Permission=""None""/>
<Feature Code=""Button0230"" Permission=""None""/>
<Feature Code=""Button0220"" Permission=""None""/>
<Feature Code=""Button0210"" Permission=""None""/>
<Feature Code=""Button0175"" Permission=""None""/>
<Feature Code=""Button0170"" Permission=""None""/>
<Feature Code=""Button0160"" Permission=""None""/>
<Feature Code=""Button0150"" Permission=""None""/>
<Feature Code=""Button0140"" Permission=""None""/>
<Feature Code=""Button0130"" Permission=""None""/>
<Feature Code=""Button0120"" Permission=""None""/>
<Feature Code=""Button0116"" Permission=""None""/>
<Feature Code=""Button0113"" Permission=""None""/>
<Feature Code=""Button0110"" Permission=""None""/>
<Feature Code=""Button0095"" Permission=""None""/>
<Feature Code=""SelectFlagStudent.DDA9F584 - 1303 - 4A04 - 851F - CBC093F24ADA"" Permission=""None""/>
<Feature Code=""Button0092"" Permission=""None""/>
<Feature Code=""Button0090"" Permission=""None""/>
<Feature Code=""Button0050"" Permission=""None""/>
<Feature Code=""Button0040"" Permission=""None""/>
<Feature Code=""Button0085"" Permission=""None""/>
<Feature Code=""Button0030"" Permission=""None""/>
<Feature Code=""8EFBEC7D - D438 - 44EA - 81E3 - 6AFA00862429"" Permission=""Execute""/>
<Feature Code=""26751E07 - 00A0 - 4500 - BC31 - F2E57EE1C6F2"" Permission=""Execute""/>
<Feature Code=""74E0D4FA - F698 - 400D - B8A8 - 60F4DF304BBA"" Permission=""Execute""/>
<Feature Code=""24821EBA - 426E-4811 - 95B8 - DBF8D9AEEFA2"" Permission=""Execute""/>
<Feature Code=""AB164E2A - 516E-4427 - ADB0 - 79D27F1685CA"" Permission=""Execute""/>
<Feature Code=""Button0020"" Permission=""None""/>
<Feature Code=""Button0010"" Permission=""None""/>
<Feature Code=""ischool.System.DataRationality"" Permission=""None""/>
<Feature Code=""8EFBEC7D-D438-44EA-81E3-6AFA00862429"" Permission=""Execute""/>
<Feature Code=""26751E07-00A0-4500-BC31-F2E57EE1C6F2"" Permission=""Execute""/>
<Feature Code=""74E0D4FA-F698-400D-B8A8-60F4DF304BBA"" Permission=""Execute""/>
<Feature Code=""24821EBA-426E-4811-95B8-DBF8D9AEEFA2"" Permission=""Execute""/>
<Feature Code=""AB164E2A-516E-4427-ADB0-79D27F1685CA"" Permission=""Execute""/>
<Feature Code=""ischool.DiagnosticsMode"" Permission=""None""/>
<Feature Code=""ischool.AdvancedToolSet"" Permission=""None""/>
</Permissions>";

            #endregion

            #region 建立設備預約模組管理員專用角色
            {
                string roleName = "設備預約管理者";
                bool _checkAdminrole = CheckRole(roleName);
                // 如果管理者角色不存在，建立角色並取回角色ID
                if (!_checkAdminrole)
                {
                    string description = "";
                    string sqlInsert = string.Format(@"
WITH insert_role AS(
    INSERT INTO _role(
        role_name 
        , description
        , permission
    ) 
    VALUES (
        '{0}'
        ,'{1}'
        ,'{2}' 
    )
    RETURNING _role.id
)
SELECT * FROM insert_role

                    ", roleName, description, permission);

                    QueryHelper qh = new QueryHelper();
                    DataTable dt = qh.Select(sqlInsert);

                    _roleAdminID = "" + dt.Rows[0]["id"];
                }
            }
            #endregion

            #region 建立設備預約專用腳色
            {
                string roleName = "設備預約模組專用";
                bool _checkrole = CheckRole(roleName);
                // 如果專用角色不存在，建立角色並取回角色ID
                if (!_checkrole)
                {
                    string description = "";
                    string sqlInsert = string.Format(@"
WITH insert_role AS(
    INSERT INTO _role(
        role_name 
        , description
        , permission
    ) VALUES (
        '{0}'
        ,'{1}'
        ,'{2}' 
    )
    RETURNING _role.id
)
SELECT * FROM insert_role
                    ", roleName, description, permission);

                    QueryHelper qh = new QueryHelper();
                    DataTable dt = qh.Select(sqlInsert);
                    _roleID = "" + dt.Rows[0]["id"];
                    
                }
            }
            #endregion

            // 取得登入帳號與身分
            Actor actor = new Actor();
            //string identity = Actor.Identity;

            // 建立設備預約分頁
            MotherForm.AddPanel(BookingEquipmentAdmin.Instance);

            #region 基本設定

            RibbonBarItem settingItem = FISCA.Presentation.MotherForm.RibbonBarItems["設備預約", "基本設定"];

            settingItem["設定"].Size = RibbonBarButton.MenuButtonSize.Large;
            settingItem["設定"].Image = Properties.Resources.sandglass_unlock_64;

            settingItem["設定"]["設備管理單位"].Enable = Permissions.設定設備管理單位權限;
            settingItem["設定"]["設備管理單位"].Click += delegate
            {
                if (actor.isSysAdmin())
                {
                    ManagementUnit form = new ManagementUnit();
                    form.ShowDialog();
                }
                else
                {
                    MsgBox.Show("此帳號沒有設定設備管理單位權限");
                }

            };

            settingItem["設定"]["單位管理員"].Enable = Permissions.設定設備單位權管理員權限;
            settingItem["設定"]["單位管理員"].Click += delegate
            {
                if (actor.isSysAdmin())
                {
                    SetUnitAdmin form = new SetUnitAdmin();
                    form.ShowDialog();
                }
                else
                {
                    MsgBox.Show("此帳號沒有設定設備單位管理員權限");
                }
            };

            settingItem["管理"].Size = RibbonBarButton.MenuButtonSize.Large;
            settingItem["管理"].Image = Properties.Resources.network_lock_64;

            settingItem["管理"]["管理設備"].Enable = Permissions.管理設備權限;
            settingItem["管理"]["管理設備"].Click += delegate
            {
                if (actor.isSysAdmin() || actor.isUnitAdmin())
                {
                    ManageEquipment form = new ManageEquipment();
                    form.ShowDialog();
                }
                else
                {
                    MsgBox.Show("此帳號沒有管理設備權限");
                }
            };

            #endregion

            #region 資料統計

            RibbonBarItem dataItem = FISCA.Presentation.MotherForm.RibbonBarItems["設備預約", "資料統計"];

            dataItem["匯出"].Size = RibbonBarButton.MenuButtonSize.Large;
            dataItem["匯出"].Image = Properties.Resources.Export_Image;

            dataItem["匯出"]["設備清單"].Enable = Permissions.匯出設備清單權限;
            dataItem["匯出"]["設備清單"].Click += delegate 
            {
                if (actor.isSysAdmin() || actor.isUnitAdmin())
                {
                    ExportEquipmentForm form = new ExportEquipmentForm();
                    form.ShowDialog();
                }
                else
                {
                    MsgBox.Show("此帳號沒有匯出設備清單權限");
                }
            };

            dataItem["匯入"].Size = RibbonBarButton.MenuButtonSize.Large;
            dataItem["匯入"].Image = Properties.Resources.Import_Image;

            dataItem["匯入"]["設備清單"].Enable = Permissions.匯入設備清單權限;
            dataItem["匯入"]["設備清單"].Click += delegate 
            {
                if (actor.isSysAdmin() || actor.isUnitAdmin())
                {
                    new ImportEquipmentData().Execute();
                }
                else
                {
                    MsgBox.Show("此帳號沒有匯入設備清單權限");
                }
            };

            dataItem["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
            dataItem["報表"].Image = Properties.Resources.Report;

            dataItem["報表"]["統計設備使用狀況"].Enable = Permissions.統計設備使用狀況權限;
            dataItem["報表"]["統計設備使用狀況"].Click += delegate 
            {
                if (actor.isSysAdmin() || actor.isUnitAdmin())
                {
                    StatisticalTableForm form = new StatisticalTableForm();
                    form.ShowDialog();
                }
                else
                {
                    MsgBox.Show("此帳號沒有列印報表權限");
                }
            };

            #endregion

            #region 權限管理

            Catalog detail = new Catalog();
            detail = RoleAclSource.Instance["設備預約"]["功能按鈕"];
            detail.Add(new RibbonFeature(Permissions.設備管理單位, "設定設備管理單位"));
            detail.Add(new RibbonFeature(Permissions.設備單位管理員, "設定設備單位管理員"));
            detail.Add(new RibbonFeature(Permissions.管理設備, "管理設備"));
            detail.Add(new RibbonFeature(Permissions.匯出設備清單, "匯出設備清單"));
            detail.Add(new RibbonFeature(Permissions.匯入設備清單, "匯入設備清單"));
            detail.Add(new RibbonFeature(Permissions.統計設備使用狀況, "統計設備使用狀況"));

            #endregion

        }

        /// <summary>
        /// 檢查設備預約模組專用角色是否存在
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static bool CheckRole(string roleName)
        {
            string sql = string.Format("SELECT * FROM _role WHERE role_name = '{0}' ", roleName);
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);
            int n = 0;
            bool roleExist = true;
            if (roleName == "設備預約管理者")
            {
                foreach (DataRow row in dt.Rows)
                {
                    n++;
                    _roleAdminID = "" + row["id"];
                }
            }
            else if (roleName == "設備預約模組專用")
            {
                foreach (DataRow row in dt.Rows)
                {
                    n++;
                    _roleID = "" + row["id"];
                }
            }

            if (n > 1) // 角色重複
            {
                MsgBox.Show("設備預約模組專用角色重複! \n  請先確認角色權限管理是否有重複角色!");

            }
            else if (n == 1) // 角色存在
            {
                roleExist = true;
            }
            else // 角色不存在
            {
                roleExist = false;
            }
            return roleExist;
        }

    }
}
