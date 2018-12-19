using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace TTliteUtil
{
    public class MySqlClass
    {

        private MySqlConnection con = new MySqlConnection();
        private MySqlCommand _cmdInsertAdsDetail = new MySqlCommand();
        private MySqlDataAdapter sda = new MySqlDataAdapter();
        private DataSet ds = new DataSet();
        public static DataTable ExcelValues { get; set; }

        public static int ReturnInteger(string Query)
        {
            int count = 0;
            try
            {
                using (MySqlConnection oCon = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    oCon.Open();
                    DataTable Result = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(Query, oCon);
                    da.Fill(Result);
                    if (Result.Rows.Count > 0)
                        count = Convert.ToInt32(Result.Rows[0]["Count"]);
                    oCon.Close();
                }
            }
            catch (Exception ex)
            {
                //OpenConnection.Close();
            }
            return count;
        }

        public static DataTable ReturnDataTable(string query)
        {
            DataTable Result = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, _con);
                    da.Fill(Result);
                    _con.Close();
                }
            }
            catch (Exception de)
            {
                Util.WriteToEventLog("Error: " + de.Message);
                //OpenConnection.Close();
            }
            return Result;
        }
        //ReturnDataTableNew
        public static DataTable ReturnDataTableNew(string query)
        {
            DataTable Result = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, _con);
                    da.Fill(Result);
                    _con.Close();
                }
            }
            catch (Exception de)
            {
                Util.WriteToEventLog("Error: " + de.Message);
                //OpenConnection.Close();
            }
            return Result;
        }

        public static DataTable returnFilterdata(string SpName, string StatusIDs, string StoreName, string StartDate, string DueDate)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("ListStatusIDs", StatusIDs);
                    cmd.Parameters.Add("ListStoreName", StoreName);
                    cmd.Parameters.Add("ListSDate", StartDate);
                    cmd.Parameters.Add("ListDDate", DueDate);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }

        }


        /// <summary>
        /// insert data in db
        /// </summary>
        /// <param name="Query"></param>

        public static void Insert_Update(string Query)
        {

            try
            {
                using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _Con.Open();
                    MySqlCommand oCommandPro = new MySqlCommand(Query, _Con);
                    oCommandPro.ExecuteNonQuery();
                    //_con.Close();
                    _Con.Close();
                }
            }
            catch (MySqlException exx)
            {
                Util.WriteToEventLog("Insert query problem in mysql : " + exx.Message + " Query: " + Query);
            }
            catch (Exception se)
            {
                // Debug.WriteLine(se.Message);
                Util.WriteToEventLog("Insert query problem : " + se.Message + " Query: " + Query);
                //  _con.Close();
                //  OpenConnection.Close();
            }
        }
        public static int Insert_Update1(string Query)
        {
            int x = 0;
            try
            {
                using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _Con.Open();
                    MySqlCommand oCommandPro = new MySqlCommand(Query, _Con);
                    oCommandPro.ExecuteNonQuery();
                    //_con.Close();
                    _Con.Close();
                }
            }
            catch (MySqlException exx)
            {
                Util.WriteToEventLog("Insert query problem in mysql : " + exx.Message + " Query: " + Query);
                ++x;
            }

            return x;
        }
        public static string Insert_UpdateWithResponse(string Query)
        {
            string response = "";
            try
            {
                using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _Con.Open();
                    MySqlCommand oCommandPro = new MySqlCommand(Query, _Con);
                    oCommandPro.ExecuteNonQuery();
                    response = "1";
                    //_con.Close();
                    _Con.Close();
                }
            }
            catch (MySqlException eq)
            {
                response = "0";
            }
            catch (Exception se)
            {
                response = "0";
                // Debug.WriteLine(se.Message);
                Util.WriteToEventLog("Insert query problem : " + se.Message + " Query: " + Query);
                //  _con.Close();
                //  OpenConnection.Close();
            }
            return response;
        }

        public static DataTable InsertOtherTask(string SpName, string oTitle, string oDescription, string oDueDate, int oStatusID, int oCreatedBy, int oLastUpdatedBy, string oCreatedDate, bool oIsDeleted, int oClientID, string oUpdatedDate, int oTaskType, string oStoreName, bool oVisibleToClient, string oPriority, string oFeasibility, int ProjectType)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oTitle", oTitle);
                    cmd.Parameters.Add("oDescription", oDescription);
                    cmd.Parameters.Add("oDueDate", oDueDate);
                    cmd.Parameters.Add("oStatusID", oStatusID);
                    cmd.Parameters.Add("oCreatedBy", oCreatedBy);
                    cmd.Parameters.Add("oLastUpdatedBy", oLastUpdatedBy);
                    cmd.Parameters.Add("oCreatedDate", oCreatedDate);
                    cmd.Parameters.Add("oIsDeleted", oIsDeleted);
                    cmd.Parameters.Add("oClientID", oClientID);
                    cmd.Parameters.Add("oUpdatedDate", oUpdatedDate);
                    // cmd.Parameters.Add("oTaskType", oTaskType);
                    cmd.Parameters.Add("oStoreName", oStoreName);
                    cmd.Parameters.Add("oVisibleToClient", oVisibleToClient);
                    cmd.Parameters.Add("oPriority", oPriority);
                    cmd.Parameters.Add("oFeasibility", oFeasibility);
                    cmd.Parameters.Add("oProjectType", ProjectType);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }

        }
        //
        // update task
        //
        public static int UpdateWorkItem(string SpName, int oTaskID, string oTitle, string oDescription, string oDueDate, int oStatusID, int oLastUpdatedBy, int oClientID, bool oIsDeleted, string oUpdatedDate, bool oVisibleToClient, string oPriority, int oProjectTypeID)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                int response = 0;
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oTaskID", oTaskID);
                    cmd.Parameters.Add("oTitle", oTitle);
                    cmd.Parameters.Add("oDescription", oDescription);
                    cmd.Parameters.Add("oDueDate", oDueDate);
                    cmd.Parameters.Add("oStatusID", oStatusID);
                    cmd.Parameters.Add("oClientID", oClientID);
                    cmd.Parameters.Add("oLastUpdatedBy", oLastUpdatedBy);
                    cmd.Parameters.Add("oIsDeleted", oIsDeleted);
                    cmd.Parameters.Add("oUpdatedDate", oUpdatedDate);
                    cmd.Parameters.Add("oVisibleToClient", oVisibleToClient);
                    cmd.Parameters.Add("oPriority", oPriority);
                    cmd.Parameters.Add("oProjectTypeID", oProjectTypeID);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    // dt.Load(dr);
                    response = 1;
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);
                    response = -1;

                }
                return response;
            }

        }
        public static DataTable InsertTaskFile(string SpName, string oFileTitle, string oFileDescription, string oFilePath, string oAttachedOn, int oUploadedBy, int oFolderID, bool oIsDeleted, int oTaskID)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oFileTitle", oFileTitle);
                    cmd.Parameters.Add("oFileDescription", oFileDescription);
                    cmd.Parameters.Add("oFilePath", oFilePath);
                    cmd.Parameters.Add("oAttachedOn", oAttachedOn);
                    cmd.Parameters.Add("oUploadedBy", oUploadedBy);
                    cmd.Parameters.Add("oFolderID", oFolderID);
                    cmd.Parameters.Add("oIsDeleted", oIsDeleted);
                    cmd.Parameters.Add("oTaskID", oTaskID);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }
        }


        public static DataTable returnStoreDataTable(string SpName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _con;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                //  Debug.WriteLine(ex.Message);
                Util.WriteToEventLog("select problem : " + ex.Message + " Query: " + SpName);
            }
            return dt;
        }


        public static DataTable returnStoreDataTable(string SpName, int month)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("month", month);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }

        }
        //
        // Five Parameter
        //
        //"GetJobDetailsByGroup_demo", month, SDateEDate[0].ToString(),SDateEDate[1].ToString(), ""
        public static DataTable returnStoreDataTable(string SpName, int month, string sdate, string edate, string GroupName)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("month", month);
                    cmd.Parameters.Add("sdate", sdate);
                    cmd.Parameters.Add("edate", edate);
                    if (GroupName != "")
                    {
                        cmd.Parameters.Add("GName", GroupName);
                    }
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }


        }

        public static DataTable returnStoreDataTable(string SpName, int month, string sdate, string edate, string type, string GroupName, string Sname)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("month", month);
                    cmd.Parameters.Add("sdate", sdate);
                    cmd.Parameters.Add("edate", edate);
                    cmd.Parameters.Add("type", type);
                    cmd.Parameters.Add("GName", GroupName);
                    cmd.Parameters.Add("Sname", Sname);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }
        }
        public static DataTable returnStoreDataTable(string SpName, int month, int week, string GroupName)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("month", month);
                    cmd.Parameters.Add("week", week);
                    if (GroupName != "")
                        cmd.Parameters.Add("GName", GroupName);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);
                }
                return dt;
            }

        }
        public static DataTable returnStoreDataTable(string SpName, int month, int week, int type, string GroupName, string StoreName)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("month", month);
                    cmd.Parameters.Add("week", week);
                    cmd.Parameters.Add("type", type);
                    cmd.Parameters.Add("GName", GroupName);
                    cmd.Parameters.Add("Sname", StoreName);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }

        }
        /// <summary>
        /// Store Procedure and current Date
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        /// 
        public static DataTable returnCurrentdueDateDataTable(string SpName, int CID, string StatusIds, string IsClient, string CDate, string GroupName)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("CID", CID);
                    if (SpName == "GetInProgressTask")
                    {
                        cmd.Parameters.Add("SIDs", StatusIds);
                    }
                    else

                        cmd.Parameters.Add("StatusIds", StatusIds);
                    // 
                    cmd.Parameters.Add("IsClient", IsClient);
                    if (SpName == "GetCurrentdueTask" || SpName == "GetOverdueTask")
                        cmd.Parameters.Add("UDdate", CDate);
                    else if (SpName == "GetTodayStartDateTask")
                        cmd.Parameters.Add("UStdate", CDate);
                    else if (SpName == "GetCurrentdueTaskByGroupName")
                    {
                        cmd.Parameters.Add("UDdate", CDate);
                        cmd.Parameters.Add("GName", GroupName);
                    }
                    else if (SpName == "GetTodayStartTaskByGroupName")
                    {
                        cmd.Parameters.Add("UStdate", CDate);
                        cmd.Parameters.Add("GName", GroupName);

                    }
                    cmd.Connection = _Con;//OpenConnection;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }

        }
        /// <summary>
        /// filter Data
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        ///         
        public static DataTable returnGetAllFilterDataDt(string SpName, string ListStatusIDs, string ListStoreName, string ListSDate, string ListDDate, string IsOverdue, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("ListStatusIDs", ListStatusIDs);
                    cmd.Parameters.Add("ListStoreName", ListStoreName);
                    cmd.Parameters.Add("ListSDate", ListSDate);
                    cmd.Parameters.Add("ListDDate", ListDDate);
                    cmd.Parameters.Add("IsOverdue", IsOverdue);
                    if (SpName == "GetStoreListByGroupName" && GroupName != "")
                    {
                        cmd.Parameters.Add("GName", GroupName);
                    }
                     cmd.Connection = _con;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                // Debug.WriteLine(ex.Message);
            }
            return dt;
        }




        /// <summary>
        /// filter Data
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        ///         
        public static DataTable GetAllFilterChildDataGroupName(string SpName, string ListStatusIDs, string ListStoreName, string ListSDate, string ListDDate, string IsOverdue, string GroupName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("ListStatusIDs", ListStatusIDs);
                    cmd.Parameters.Add("ListStoreName", ListStoreName);
                    cmd.Parameters.Add("ListSDate", ListSDate);
                    cmd.Parameters.Add("ListDDate", ListDDate);
                    cmd.Parameters.Add("IsOverdue", IsOverdue);
                    cmd.Parameters.Add("GName", GroupName);
                    cmd.Connection = _con;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                // Debug.WriteLine(ex.Message);
            }
            return dt;
        }



        //
        //regular task
        //
        public static DataTable returnGetRegularDataDt(string SpName, int CID, string StatusIds, string IsClient, int TType, string Fdate, string EDate)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("CID", CID);
                    cmd.Parameters.Add("StatusIds", StatusIds);
                    cmd.Parameters.Add("IsClient", IsClient);
                    cmd.Parameters.Add("TType", TType);
                    cmd.Parameters.Add("Sdate", Fdate);
                    cmd.Parameters.Add("Edate", EDate);
                    cmd.Connection = _con;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                // Debug.WriteLine(ex.Message);
            }
            return dt;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Query"></param>
        /// <returns></returns>
        /// 
        public static DataTable returnGetStoreNameDt(string SpName, string StoreNameOrUserID, bool IsStore)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("StoreNameString", StoreNameOrUserID);

                    cmd.Connection = _con;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                // Debug.WriteLine(ex.Message);
                //OpenConnection.Close();
            }
            return dt;
        }
        public static DataTable returnGetDtWithDoubleParam(string SpName, string ParamName1, string ParamName2, string ParamNameVale1, int ParamNameVale2)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(ParamName1, ParamNameVale1);
                    cmd.Parameters.Add(ParamName2, ParamNameVale2);

                    cmd.Connection = _con;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    // OpenConnection.Close();
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                // OpenConnection.Close();
            }
            return dt;
        }
        public static DataTable returnGetDtWithDoubleParamAllStr(string SpName, string ParamName1, string ParamName2, string ParamNameVale1, string ParamNameVale2)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(ParamName1, ParamNameVale1);
                    cmd.Parameters.Add(ParamName2, ParamNameVale2);

                    cmd.Connection = _con;//OpenConnection;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _con.Close();
                    // OpenConnection.Close();
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
            }
            return dt;
        }

        public static DataTable returnGetDtWithThreeParamAllStr(string SpName, string ParamName1, string ParamName2, string ParamName3, string JobNumber, string ParamNameVale1, string ParamNameVale2, int ParamNameVale3, string ParamNameVale4)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(ParamName1, ParamNameVale1);
                    cmd.Parameters.Add(ParamName2, ParamNameVale2);
                    cmd.Parameters.Add(ParamName3, ParamNameVale3);
                    cmd.Parameters.Add(JobNumber, ParamNameVale4);

                    cmd.Connection = _con;//OpenConnection;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _con.Close();
                    // OpenConnection.Close();
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
            }
            return dt;
        }
        public static DataTable returnGetLogoDt(string SpName, string StoreNameOrUserID)
        {
            DataTable dt = new DataTable();
            try
            {
                int uid = Convert.ToInt32(StoreNameOrUserID);
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("UID", uid);
                    cmd.Connection = _con;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                //  Debug.WriteLine(ex.Message);
            }
            return dt;
        }
        public static DataTable returnDtWithProcedure(string SpName, bool ViToClient, int CurrentUserID, bool IsAdmin, string StatusIDs, string Titlval, int IsResp)
        {
            DataTable dt = new DataTable();
            try
            {
                // int uid = Convert.ToInt32(StoreNameOrUserID);
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("IsViTOClient", ViToClient);
                    cmd.Parameters.Add("CUserID", CurrentUserID);
                    cmd.Parameters.Add("IsAdmin", IsAdmin);
                    cmd.Parameters.Add("StatusIds", StatusIDs);
                    cmd.Parameters.Add("Titlval", Titlval);
                    cmd.Parameters.Add("IsResponsible", IsResp);
                    cmd.Connection = _con;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                //  Debug.WriteLine(ex.Message);
            }
            return dt;
        }
        public static string ReturnStrValue(string Query)
        {
            string str = "";
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    //  MySqlConnection oCon = OpenConnection;
                    _con.Open();
                    MySqlCommand oCom = new MySqlCommand(Query, _con);
                    string count = ((string)oCom.ExecuteScalar());
                    if (count != "")
                        str = count;
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                // Debug.WriteLine(ex.Message);
                // OpenConnection.Close();
            }
            return str;
        }

        public static string ReturnSingleValue(string Query)
        {
            string str = "";
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {

                    _con.Open();
                    MySqlCommand oCom = new MySqlCommand(Query, _con);
                    MySqlDataReader Reader = oCom.ExecuteReader();
                    if (Reader.Read())
                    {
                        str = Reader[0].ToString();
                    }
                    _con.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return str;
        }


        public static DataTable returnjobscount(string SpName, string tablename, string sdate, string edate, string website)
        {
            using (SqlConnection _Con = new SqlConnection(ConfigurationManager.AppSettings["connection"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (SpName == "CountAds")
                    {
                        cmd.Parameters.Add("@table", tablename);
                        cmd.Parameters.Add("website", website);
                    }
                    else
                    {
                        cmd.Parameters.Add("website", tablename);
                    }
                    cmd.Parameters.Add("sdate", sdate);
                    cmd.Parameters.Add("edate", edate);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }

        }

        public static DataTable returngetaddress(string SpName, string sdate, string edate)
        {
            using (SqlConnection _Con = new SqlConnection(ConfigurationManager.AppSettings["connection"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("sdate", sdate);
                    cmd.Parameters.Add("edate", edate);
                    cmd.Connection = _Con;//OpenConnection;
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);
                }
                return dt;
            }

        }

        public static DataTable returnDtDownloadSql(string SpName, string tablename, string Website, string T_group, string scheduledate)
        {
            using (SqlConnection _Con = new SqlConnection(ConfigurationManager.AppSettings["connection"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tablename", tablename);
                    cmd.Parameters.Add("@Website", Website);
                    cmd.Parameters.Add("@T_group", T_group);
                    cmd.Parameters.Add("@scheduledate", scheduledate);
                    cmd.Connection = _Con;
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {

                }
                return dt;
            }

        }

        //
        public static DataTable ReturnDataTableSql(string query)
        {
            DataTable Result = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(ConfigurationManager.AppSettings["connection"].ToString()))
                {
                    _con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, _con);
                    da.Fill(Result);
                    _con.Close();
                }
            }
            catch (Exception de)
            {
                Util.WriteToEventLog("Error: " + de.Message);
                //OpenConnection.Close();
            }
            return Result;
        }
        //insert sql
        public static void Insert_UpdateSql(string Query)
        {

            try
            {
                using (SqlConnection _Con = new SqlConnection(ConfigurationManager.AppSettings["connection"].ToString()))
                {
                    _Con.Open();
                    SqlCommand oCommandPro = new SqlCommand(Query, _Con);
                    oCommandPro.ExecuteNonQuery();
                    _Con.Close();
                }
            }
            catch (Exception se)
            {
                Util.WriteToEventLog("Insert query problem : " + se.Message + " Query: " + Query);

            }
        }
        //
        // insert time sheet on task
        //
        public static string InsertTimeSheet(string SpName, int Tid, decimal hr, int Uid, string Comments, string cdate)
        {
            string Hr = "0";
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Tid", Tid);
                    cmd.Parameters.Add("hr", hr);
                    cmd.Parameters.Add("Uid", Uid);
                    cmd.Parameters.Add("Comments", Comments);
                    cmd.Parameters.Add("cdate", cdate);
                    cmd.Connection = _con;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Hr = dr[0].ToString();

                    }
                    _con.Close();
                }
            }
            catch (Exception ex)
            {
                // Debug.WriteLine(ex.Message);
                //OpenConnection.Close();
            }
            return Hr;
        }
        //
        // Insert Task Message
        //
        public static DataTable InsertTaskMessage(string SpName, int oTaskID, string oComments, string oCommentedDate, int oCommentedBy, bool oIsEnbabled, string oCreatedDate, bool oIsDeleted, bool oIsDesktop, int isgen)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oTaskId", oTaskID);
                    cmd.Parameters.Add("oComments", oComments);
                    cmd.Parameters.Add("oCommentedDate", oCommentedDate);
                    cmd.Parameters.Add("oCommentedBy", oCommentedBy);
                    cmd.Parameters.Add("oIsEnbabled", oIsEnbabled);
                    cmd.Parameters.Add("oCreatedDate", oCreatedDate);
                    cmd.Parameters.Add("oIsDeleted", oIsDeleted);
                    cmd.Parameters.Add("oIsDesktop", oIsDesktop);
                    cmd.Parameters.Add("oIsGeneric", isgen);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        //
        // Insert all activity of work item
        //
        public static DataTable InsertActivity(string SpName, int oTaskId, int oActionTypeID, int oObjectTypeId, int oObjectID, int oUserID, string oActDateTime, string oMessage)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oTaskId", oTaskId);
                    cmd.Parameters.Add("oActionTypeID", oActionTypeID);
                    cmd.Parameters.Add("oObjectTypeId", oObjectTypeId);
                    cmd.Parameters.Add("oObjectID", oObjectID);
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Parameters.Add("oActDateTime", oActDateTime);
                    cmd.Parameters.Add("oMessage", oMessage);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        //
        // Get Assigned user list
        //
        public static DataTable ReturnDtAssignedUserList(string SpName, int oTaskId, int oCurrentUserID)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oTaskId", oTaskId);
                    cmd.Parameters.Add("oCurrentUserID", oCurrentUserID);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        // Get Assigned user list
        //
        public static DataTable ReturnGetAllActivityLog(string SpName, int boardid, int UserId)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("BoardId", boardid);
                    cmd.Parameters.Add("UserId", UserId);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        //
        //
        //

        public static DataTable ReturnGetEstimetedHours(string SpName, int boardid)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("BOARDID", boardid);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        //
        // Get All activity List By TaskID
        //
        public static DataTable ReturnDtAllActivityByTaskID(string SpName, int oTaskId)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oTaskID", oTaskId);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        //
        // Get offcycle status
        //
        public static DataTable ReturnDtOffcycleStatusList(string SpName, string oCDate, bool IsNew, string StatusIDs)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oCDate", oCDate);
                    cmd.Parameters.Add("oIsNew", IsNew);
                    cmd.Parameters.Add("StatusIDs", StatusIDs);
                    //cmd.Parameters.Add("WebSitelst", WebSitelst);   
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        public static DataTable ReturnDtOffcycleUnProcessList(string SpName, string oCDate, int StatusNew)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oCDate", oCDate);
                    cmd.Parameters.Add("oNewStatusId", StatusNew);
                    //cmd.Parameters.Add("WebSitelst", WebSitelst);   
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        //
        // Get no ads found offcycle web site
        //
        public static DataTable ReturnDtOffNoAdsFound(string SpName, string oCDate)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oCDate", oCDate);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        //
        // Get no ads found offcycle web site
        //
        public static string ReturnLastUpdateTime(string SpName, string oWebsite, string oTitle, string oAdDate, string oCDate, bool oIsUpdate)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                string Hr = "";
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oWebsite", oWebsite);
                    cmd.Parameters.Add("oTitle", oTitle);
                    cmd.Parameters.Add("oAdDate", oAdDate);
                    cmd.Parameters.Add("oCDate", oCDate);
                    cmd.Parameters.Add("oIsUpdate", oIsUpdate);

                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    // dt.Load(dr);
                    while (dr.Read())
                    {
                        Hr = dr[0].ToString();

                    }
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return Hr;
            }
        }
        //
        // Get no ads found offcycle web site
        //
        public static DataTable ReturnAdsLog(string SpName, string oWebsite, string oTitle, string oAdDate, string oCDate, bool oIsOffDate)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oWebsite", oWebsite);
                    cmd.Parameters.Add("oTitle", oTitle);
                    cmd.Parameters.Add("oAdDates", oAdDate);
                    cmd.Parameters.Add("oCDate", oCDate);
                    cmd.Parameters.Add("oIsOffDate", oIsOffDate);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);

                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        //
        // Get All zip code by website nad date
        //
        public static DataTable ReturnAllZipStoreIsByWebsite(string SpName, string oWebSite, string ofilters, bool oIsLimit)
        {
            using (SqlConnection _Con = new SqlConnection(ConfigurationManager.AppSettings["connection"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@WebSite", oWebSite);
                    cmd.Parameters.Add("@filters", ofilters);
                    cmd.Parameters.Add("@IsLimit", oIsLimit);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        //
        // change offcycle status
        //

        public static string UpdateStatusAndAddStatusLog(string SpName, int oStausid, int oOldStatusID, string oWebSite, string oTitle, string oAdDate, string oCdate, int oCUserID, string oCurrentDateTime, bool oIsUnProcess, string oldAdDate, bool oIsReport, int oAdGridType, string oStatus, string oLastUpdatedBy)
        {
            string Response = "0";
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oStausid", oStausid);
                    cmd.Parameters.Add("oOldStatusID", oOldStatusID);
                    cmd.Parameters.Add("oWebSite", oWebSite);
                    cmd.Parameters.Add("oTitle", oTitle);
                    cmd.Parameters.Add("oAdDate", oAdDate);
                    cmd.Parameters.Add("oCdate", oCdate);
                    cmd.Parameters.Add("oCUserID", oCUserID);
                    cmd.Parameters.Add("oCurrentDateTime", oCurrentDateTime);
                    cmd.Parameters.Add("oIsUnProcess", oIsUnProcess);
                    cmd.Parameters.Add("oldAdDate", oldAdDate);
                    if (oIsReport)
                    {
                        cmd.Parameters.Add("oIsReport", oIsReport);
                        cmd.Parameters.Add("oAdGridType", oAdGridType);
                        cmd.Parameters.Add("oStatus", oStatus);
                        cmd.Parameters.Add("oLastUpdatedBy", oLastUpdatedBy);
                    }
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    Response = "1";
                }
                catch (Exception ex)
                {

                    Response = "-1";
                }
                return Response;
            }
        }

        //
        //

        public static string InsertScheduleAds_Web(string SpName, int oJob_Number, string oStoreID, string oStoreName, string oAddress, string oCity, string oST, string oZip, string oDistrict, int oEstItems, decimal oEstHours, string oStartDate, string oTargetDate, string oAccountManager, string oAccountID, string oDueDate, string oDeliverDate, int oDefaultStatusID, int oCurrentUserID, int pCurrentUserID, string oCurrentDate, int oTaskType, int oClientID, string pCurrentDate, string Description, string oGroupName, bool oIsPDF, string oWebURL)
        {
            string Response = "0";
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oJob_Number", oJob_Number);
                    cmd.Parameters.Add("oStoreID", oStoreID);
                    cmd.Parameters.Add("oStoreName", oStoreName);
                    cmd.Parameters.Add("oAddress", oAddress);
                    cmd.Parameters.Add("oCity", oCity);
                    cmd.Parameters.Add("oST", oST);
                    cmd.Parameters.Add("oZip", oZip);
                    cmd.Parameters.Add("oDistrict", oDistrict);
                    cmd.Parameters.Add("oEstItems", oEstItems);
                    cmd.Parameters.Add("oEstHours", oEstHours);
                    cmd.Parameters.Add("oStartDate", oStartDate);
                    cmd.Parameters.Add("oTargetDate", oTargetDate);
                    cmd.Parameters.Add("oAccountManager", oAccountManager);
                    cmd.Parameters.Add("oAccountID", oAccountID);
                    cmd.Parameters.Add("oDueDate", oDueDate);
                    cmd.Parameters.Add("oDeliverDate", oDeliverDate);
                    cmd.Parameters.Add("oDefaultStatusID", oDefaultStatusID);
                    cmd.Parameters.Add("oCurrentUserID", oCurrentUserID);
                    cmd.Parameters.Add("pCurrentUserID", pCurrentUserID);
                    cmd.Parameters.Add("oCurrentDate", oCurrentDate);
                    cmd.Parameters.Add("oTaskType", oTaskType);
                    cmd.Parameters.Add("oClientID", oClientID);
                    cmd.Parameters.Add("pCurrentDate", pCurrentDate);
                    cmd.Parameters.Add("Description", Description);
                    cmd.Parameters.Add("oGroupName", oGroupName);
                    cmd.Parameters.Add("oIsPDF", oIsPDF);
                    cmd.Parameters.Add("oWebURL", oWebURL);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    Response = "1";
                }
                catch (Exception ex)
                {

                    Response = "-1";
                }
                return Response;
            }
        }


        //
        //
        // get total zip by website ,title addate and current of offcycle
        //
        public static string GetTotalZipCountByWebSite(string SpName, string oWebsite, string oTitle, string oAdDate, string oCdate, string oDateDetected, int oStatusID, bool IsUnProcessed)
        {
            string Response = "0";
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oWebsite", oWebsite);
                    cmd.Parameters.Add("oTitle", oTitle);
                    cmd.Parameters.Add("oAdDate", oAdDate);
                    cmd.Parameters.Add("oCdate", oCdate);
                    cmd.Parameters.Add("oDateDetected", oDateDetected);
                    cmd.Parameters.Add("oStatusID", oStatusID);
                    cmd.Parameters.Add("oIsUnProcessed", IsUnProcessed);

                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    // dt.Load(dr);
                    while (dr.Read())
                    {
                        Response = dr[0].ToString();

                    }
                    _Con.Close();
                    //Response = "1";
                }
                catch (Exception ex)
                {

                    Response = "-1";
                }
                return Response;
            }
        }
        //
        // get status and currentdate by website addate and title
        //
        public static string gettotaljobc(string SpName, string StatusIDd, string StartDatet, string StoreyName)
        {
            string Response = "0";
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("StoreyName", StoreyName);
                    cmd.Parameters.Add("StartDatet", StartDatet);
                    cmd.Parameters.Add("StatusIDd", StatusIDd);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    // dt.Load(dr);
                    while (dr.Read())
                    {
                        Response = dr[0].ToString();

                    }
                    _Con.Close();
                    //Response = "1";
                }
                catch (Exception ex)
                {

                    Response = "-1";
                }
                return Response;
            }

        }
        //
        public static string gettotaljobcpp(string SpName, string StartDatet, string StoreyName)
        {
            string Response = "0";
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("StoreyName", StoreyName);
                    cmd.Parameters.Add("StartDatet", StartDatet);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    // dt.Load(dr);
                    while (dr.Read())
                    {
                        Response = dr[0].ToString();

                    }
                    _Con.Close();
                    //Response = "1";
                }
                catch (Exception ex)
                {

                    Response = "-1";
                }
                return Response;
            }

        }
        //
        public static DataTable GetOffCycleAdsStatusByWTd(string SpName, string oWebsite, string oTitle, string oAdDate, string oCdate, bool IsUnProcessed, bool oIsReport, int oAdGridType)
        {
            //  string Response = "0";
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oWebsite", oWebsite);
                    cmd.Parameters.Add("oTitle", oTitle);
                    cmd.Parameters.Add("oAdDate", oAdDate);
                    cmd.Parameters.Add("oCdate", oCdate);
                    cmd.Parameters.Add("oIsUnProcessed", IsUnProcessed);
                    cmd.Parameters.Add("oIsReport", oIsReport);
                    cmd.Parameters.Add("oAdGridType", oAdGridType);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    //Response = "1";
                }
                catch (Exception ex)
                {

                    dt = null;
                }
                return dt;
            }
        }
        //
        // get screentshot datetime 
        //

        public static string GetScreenShotDateTime(string SpName, string oWebsite, string oCdate, string oStoreID, string oZipCode)
        {
            string Response = "";
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                //  DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oWebsite", oWebsite);
                    cmd.Parameters.Add("oCdate", oCdate);
                    cmd.Parameters.Add("oStoreID", oStoreID);
                    cmd.Parameters.Add("oZipCode", oZipCode);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    // dt.Load(dr);
                    while (dr.Read())
                    {
                        Response = dr[0].ToString();

                    }
                    _Con.Close();
                    //Response = "1";
                }
                catch (Exception ex)
                {

                    Response = "";
                }
                return Response;
            }
        }
        //
        // get all ad detected storelist
        //

        public static DataTable GetAllAdDetectedStoreList(string SpName, string oWebsite, string oTitle, string oAdDate, string Cdate, int oIsDetected, int oStatusID)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oWebsite", oWebsite);
                    cmd.Parameters.Add("oTitle", oTitle);
                    cmd.Parameters.Add("oAdDate", oAdDate);
                    cmd.Parameters.Add("Cdate", Cdate);
                    cmd.Parameters.Add("oIsDetected", oIsDetected);
                    cmd.Parameters.Add("oStatusID", oStatusID);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    //while (dr.Read())
                    //{
                    //    Response = dr[0].ToString();

                    //}
                    _Con.Close();
                    //Response = "1";
                }
                catch (Exception ex)
                {

                    dt = null;
                }
                return dt;
            }
        }

        public static DataTable ReturnOverDueDateTasksTable(string SpName, string Cdate)
        {
            DataTable Result = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();
                    // string qstr = "select a.AssignedTo,t.TaskID from taskassignmenttrail a inner join tasks t inner join users u on a.TaskID=t.TaskID and a.AssignedTo=u.UserID where (DATE_FORMAT(NOW(),'%m-%d-%Y') = DATE_FORMAT(DATE_SUB(duedate,INTERVAL 1 DAY),'%m-%d-%Y'));";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Cdate", Cdate);
                    cmd.Connection = _con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    Result.Load(dr);
                    _con.Close();
                }
            }
            catch (Exception de)
            {
                Util.WriteToEventLog("Error: " + de.Message);
                //OpenConnection.Close();
            }
            return Result;
        }
        public static DataTable RetrunTaskDetailsById(string SpName, int oTaskID)
        {
            DataTable Result = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oTaskID", oTaskID);
                    cmd.Connection = _con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    Result.Load(dr);
                    _con.Close();
                }
            }
            catch (Exception de)
            {
                Util.WriteToEventLog("Error: " + de.Message);
                //OpenConnection.Close();
            }
            return Result;
        }
        //
        // get assignedList with comma
        //

        public static string RetrunAssignedToById(string SpName, int oTaskID)
        {
            string Result = "";
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _con.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oTaskID", oTaskID);
                    cmd.Connection = _con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    //  Result.Load(dr);
                    while (dr.Read())
                    {
                        Result = dr[0].ToString();

                    }

                    _con.Close();
                }
            }
            catch (Exception de)
            {
                Util.WriteToEventLog("Error: " + de.Message);
                //OpenConnection.Close();
            }
            return Result;
        }

        //
        // user Login
        //
        public static DataTable returnStLoginDataTable(string SpName, string oEmail, string oPassword)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oEmail", oEmail);
                    cmd.Parameters.Add("oPassword", oPassword);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }

        }




        // Get Api Key
        //
        //
        // Get Api Key
        //
        public static string returnUserName(string SpName, string oEmail, string oPassword, int oUserID)
        {
            string str = "";
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Parameters.Add("oEmail", oEmail);
                    cmd.Parameters.Add("oPassword", oPassword);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    //  MySqlDataReader dr = cmd.ExecuteReader();
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    if (Reader.Read())
                    {
                        str = Reader[0].ToString();
                    }
                    // dt.Load(dr);

                    // if(dt.Rows.Count>0)
                    //     str=dt.Rows[0]["Apikey"].ToString();
                    _Con.Close();
                    // OpenConnection.Close();



                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return str;
            }

        }

        //
        // insert user time sheet log
        //
        public static void InsertLoginTime(string SpName, int oUserID, string oCDate)
        {
            string str = "";
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Parameters.Add("oCDate", oCDate);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    //  MySqlDataReader dr = cmd.ExecuteReader();
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    _Con.Close();
                    // OpenConnection.Close();



                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                //  return str;
            }
        }



        //
        // Create new Task
        //
        //
        // insert user time sheet log
        //
        public static DataTable CreatedNewTask(string SpName, string oTitle, int oUserID, int oProjectID, int oStatusID, int oClientID, string oCreatedDate)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("oTitle", oTitle);
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Parameters.Add("oProjectID", oProjectID);
                    cmd.Parameters.Add("oStatusID", oStatusID);
                    cmd.Parameters.Add("oClientID", oClientID);
                    cmd.Parameters.Add("oCreatedDate", oCreatedDate);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();


                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }
        }





        //
        // get total work time by task id on same date
        //       

        public static DataTable GetTotalWorkTimeByTaskID(string SpName, int oUserID, int oTaskID, string oCdate, string oLogType, int isgen)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Parameters.Add("oTaskID", oTaskID);
                    cmd.Parameters.Add("oCdate", oCdate);
                    cmd.Parameters.Add("oLogType", oLogType);
                    cmd.Parameters.Add("oIsGeneric", isgen);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                finally
                {
                    _Con.Close();
                }
                return dt;
            }
        }
        ///
        ///
        ///
        public static DataTable GetTotalBreakTimeByUserID(string SpName, int oUserID, string oCdate)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Parameters.Add("oCdate", oCdate);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                finally
                {
                    _Con.Close();
                }
                return dt;
            }
        }


        //
        // get usert time for a date
        //
        public static DataTable GetBreakTimeByUserID(string SpName, int oUserID, string oCdate, string oComment)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Parameters.Add("oCdate", oCdate);
                    cmd.Parameters.Add("oComment", oComment);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                finally
                {
                    _Con.Close();
                }
                return dt;
            }
        }


        //
        // insert /Update Task time in Sec



        //
        public static DataTable Insert_UpdateTaskworkTimeInSec(string SpName, int oWorkTimeInSec, int oUserID, int oTaskID, string oCdate, string oCurrentdateTime, string StartTime, string LogType, bool IsUpdate, string Comment, string oCreatedDate, int isgen)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oWorkTimeInsec", oWorkTimeInSec);
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Parameters.Add("oTaskID", oTaskID);
                    cmd.Parameters.Add("oCdate", oCdate);
                    cmd.Parameters.Add("oCurrentDatetime", oCurrentdateTime);
                    cmd.Parameters.Add("oStartDateTime", StartTime);
                    cmd.Parameters.Add("oLogType", LogType);
                    cmd.Parameters.Add("oIsUpdate", IsUpdate);
                    cmd.Parameters.Add("oComment", Comment);
                    cmd.Parameters.Add("oCreatedDate", oCreatedDate);
                    cmd.Parameters.Add("oIsGeneric", isgen);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();
                }
                catch (Exception ex)
                {


                }
                finally
                {
                    _Con.Close();
                }
                return dt;
            }
        }
        //
        // Insert/Update break time based on users
        //      

        public static DataTable Insert_UpdateUserBreakTimeInSec(string SpName, int oBreakTimeInsec, int oUserID, string oCdate, string oCurrentdateTime, string StartTime, string oComment, bool IsUpdate)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oBreakTimeInsec", oBreakTimeInsec);
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Parameters.Add("oCdate", oCdate);
                    cmd.Parameters.Add("oCurrentDatetime", oCurrentdateTime);
                    cmd.Parameters.Add("oStartDateTime", StartTime);
                    cmd.Parameters.Add("oComment", oComment);
                    cmd.Parameters.Add("oIsUpdate", IsUpdate);

                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                finally
                {
                    _Con.Close();
                }
                return dt;
            }
        }


        //
        // Get All activity List By TaskID
        //
        public static DataTable ReturnDtwithUserIDAndOptinalProjectID(string SpName, int oUserID, int ProjectId, int oTaskID)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (oTaskID != 0)
                    {
                        cmd.Parameters.Add("oTaskID", oTaskID);
                    }
                    else
                    {
                        cmd.Parameters.Add("oUserID", oUserID);
                        if (ProjectId != 0)
                            cmd.Parameters.Add("oProjectID", ProjectId);
                    }
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        //
        // get user prefrences by User ID
        //
        public static DataTable ReturnAnyDetailByUserID(string SpName, int oUserID)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        public static DataTable getavgelapsedtime(int UserId, int Month)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("getelapsedtime", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("UserID", UserId);
                    cmd.Parameters.Add("MonthNumber", Month);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {
                }
                return dt;
            }

        }



        public static DataTable GetTeamelapsedtime(int tid, int selectedmonth)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("getteamelapsedtime", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("TeamId", tid);
                    cmd.Parameters.Add("monthnumber", selectedmonth);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {
                }
                return dt;
            }

        }


        public static DataTable getindividualreopentask(int UserId, int Month)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("getindividualreopentask", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("userid", UserId);
                    cmd.Parameters.Add("monthnumber", Month);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {
                }
                return dt;
            }

        }
        //
        // Get indiviual Qa retrun 
        //

        public static DataTable getindividualQaRetrun(int UserId, int Month)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("getindividualQaRetrun", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("userid", UserId);
                    cmd.Parameters.Add("monthnumber", Month);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {
                }
                return dt;
            }

        }
        //
        // Get indiviual Issue Not Found
        //

        public static DataTable getindividualIssueNotFound(int UserId, int Month)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("getindividualIssueNotFound", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("userid", UserId);
                    cmd.Parameters.Add("monthnumber", Month);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {
                }
                return dt;
            }

        }


        public static DataTable GetTeamReopenCount(int tid, int month)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("getteamreopentasks", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("teamid", tid);
                    cmd.Parameters.Add("monthnumber", month);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }
        }

        public static DataTable GetTeamQaRetrunCount(int tid, int month)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("getteamQaretrun", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("teamid", tid);
                    cmd.Parameters.Add("monthnumber", month);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }
        }

        public static DataTable GetTeamIssueNotfound(int TID, int month)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("getteamIssueNotfound", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("teamid", TID);
                    cmd.Parameters.Add("monthnumber", month);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }
        }






        public static DataTable GetExistTitledt(string titleval, int pid)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetExistsTask", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("titleval", titleval);
                    cmd.Parameters.Add("ProjectTypeID", pid);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                }
                catch (Exception ex)
                {

                }
                return dt;
            }
        }
        public static DataTable GetTaskExistOrNot(string title, int ProjectTypeID)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetExistsTask", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("titleval", title);
                    cmd.Parameters.Add("ProjectTypeID", ProjectTypeID);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }
        }

        //public static DataTable GetCompletegroupname(string StoreName, string FinalSDate)
        //{

        //    using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
        //    {

        //        DataTable dt = new DataTable();
        //        try
        //        {
        //            _Con.Open();
        //            MySqlCommand cmd = new MySqlCommand("GetExistStorel", _Con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("StoreNamed", StoreName);
        //            cmd.Parameters.Add("FinalSDated", FinalSDate);
        //            cmd.Connection = _Con;//OpenConnection;
        //            cmd.CommandTimeout = 1000000;
        //            MySqlDataReader dr = cmd.ExecuteReader();
        //            dt.Load(dr);
        //            _Con.Close();
        //        }
        //        catch (Exception ex)
        //        {

        //            dt = null;
        //        }
        //        return dt;
        //    }
        //}

        public static DataTable GetAdsCompletegroupname(string StoreName, string FinalSDate, int TaskType)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    //MySqlCommand cmd = new MySqlCommand("GetExistStorel", _Con);
                    MySqlCommand cmd = new MySqlCommand("GetExistStorel2", _Con);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("StoreNamed", StoreName);
                    cmd.Parameters.Add("FinalSDated", FinalSDate);
                    cmd.Parameters.Add("otasktype", TaskType);
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
                catch (Exception ex)
                {

                    dt = null;
                }
                return dt;
            }
        }

        public static DataTable GetCompletegroupname(string StoreName, string FinalSDate)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                     MySqlCommand cmd = new MySqlCommand("GetExistStorel", _Con);
                     MySqlDataAdapter da = new MySqlDataAdapter();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("StoreNamed", StoreName);
                    cmd.Parameters.Add("FinalSDated", FinalSDate);
                     da.SelectCommand = cmd;
                    da.Fill(dt);
                }
                catch (Exception ex)
                {

                    dt = null;
                }
                return dt;
            }
        }

        public static DataTable GetTaskCountByIDe(int cUID, int STID)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetTaskCountbyStatusIdAndUserID", _Con);
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("CUserID", cUID);
                    cmd.Parameters.Add("statusID", STID);
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    _Con.Close();
                }
                catch (Exception ex)
                {

                    dt = null;
                }
                return dt;
            }
        }

        //
        // check exsiting ScreenShot
        //
        public static DataTable GetExistingScreenShot(string SpName, int oUserID, string oFileName)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Parameters.Add("oFileName", oFileName);
                    //cmd.Parameters.Add("oComment", oComment);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                finally
                {
                    _Con.Close();
                }
                return dt;
            }
        }

        //
        // check exsiting ScreenShot
        //
        public static DataTable InsertScreenShotByUserID(string SpName, int oUserID, string oFileName, string oCreatedDate, string oLogType)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Parameters.Add("oFileName", oFileName);
                    cmd.Parameters.Add("oCreatedDate", oCreatedDate);
                    cmd.Parameters.Add("oLogType", oLogType);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                finally
                {
                    _Con.Close();
                }
                return dt;
            }
        }

        public static DataTable GetTotalworktimegeight(int CUser, string StartDate)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetTotalWorktime_forGeight", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Cuser", CUser);
                    cmd.Parameters.Add("Sdate", StartDate);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();
                }
                catch (Exception ex)
                {

                    dt = null;
                }
                return dt;
            }
        }

        public static DataTable GetTotalworktimeLsix(int CUser, string StartDate)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetTotalWorktime_forlesssix", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Cuser", CUser);
                    cmd.Parameters.Add("Sdate", StartDate);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();
                }
                catch (Exception ex)
                {

                    dt = null;
                }
                return dt;
            }
        }

        public static DataTable GetTotalbreaktimeinSec(int CUser, string StartDate)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("TotalbreaktimeinSec", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Cuser", CUser);
                    cmd.Parameters.Add("Sdate", StartDate);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();
                }
                catch (Exception ex)
                {

                    dt = null;
                }
                return dt;
            }
        }

        public static DataTable GetTotalIdel_time(int CUser, string StartDate)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetIdel_time", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Cuser", CUser);
                    cmd.Parameters.Add("Sdate", StartDate);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();
                }
                catch (Exception ex)
                {

                    dt = null;
                }
                return dt;
            }
        }

        public static DataTable GetTasksdetail_withuserName(int CUser, string StartDate)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetTasks_detailwithuser", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Cuser", CUser);
                    cmd.Parameters.Add("Sdate", StartDate);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();
                }
                catch (Exception ex)
                {

                    dt = null;
                }
                return dt;
            }
        }

        public static DataTable GetTasksdetail_withuserNamefornextdate(int CUser, string StartDate)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetTasks_detailwithuserforNextDate", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Cuser", CUser);
                    cmd.Parameters.Add("Sdate", StartDate);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();
                }
                catch (Exception ex)
                {

                    dt = null;
                }
                return dt;
            }
        }

        public static DataTable GetTotalworktimeLsix_grpTid(int CUser, string StartDate)
        {

            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand("GetTotalWorktime_forlesssixgpbytid", _Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Cuser", CUser);
                    cmd.Parameters.Add("Sdate", StartDate);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    dt.Load(Reader);
                    _Con.Close();
                }
                catch (Exception ex)
                {

                    dt = null;
                }
                return dt;
            }
        }

        public static DataTable InsertintoTaskassigntrail_service(string SpName, int taskid, int assignedby, int assignedto, string assigneddate, bool isresponsible)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("tid", taskid);
                    cmd.Parameters.Add("assignedby", assignedby);
                    cmd.Parameters.Add("assignedto", assignedto);
                    cmd.Parameters.Add("assigneddate", assigneddate);
                    cmd.Parameters.Add("isresponsible", isresponsible);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        public static string Inserttaskstatuslog_service(string SpName, int taskid, int oldid, int newid, string createddate, string comments, int changeby)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                //DataTable dt = new DataTable();
                string response = string.Empty;
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("tid", taskid);
                    cmd.Parameters.Add("oldstatusid", oldid);
                    cmd.Parameters.Add("newstatusid", newid);
                    cmd.Parameters.Add("createddate", createddate);
                    cmd.Parameters.Add("comments", comments);
                    cmd.Parameters.Add("changeby", changeby);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    //dt.Load(dr);
                    response = "Successfully Inserted";
                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                return response;
            }
        }

        public static string Updatetasktimesheet_service(string SpName, int GetBreakTime, string CDate, int UId, int TaskID, string OnlyCDate, int ID, bool flag,int isgen)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                //DataTable dt = new DataTable();
                string res = string.Empty;
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("GetBreakTime", GetBreakTime);
                    cmd.Parameters.Add("cdate", CDate);
                    cmd.Parameters.Add("oUserID", UId);
                    cmd.Parameters.Add("tid", TaskID);
                    cmd.Parameters.Add("oCreatedDate", OnlyCDate);
                    cmd.Parameters.Add("oID", ID);
                    cmd.Parameters.Add("oflag", flag);
                    cmd.Parameters.Add("oIsGeneric", isgen);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    //dt.Load(dr);
                    _Con.Close();
                    res = "1";

                }
                catch (Exception ex)
                {

                    res = "-1";
                }
                return res;
            }
        }

        public static DataTable Getmessagelist_service(string SpName, int taskid, string lastcommentid)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("tid", taskid);
                    cmd.Parameters.Add("lastcommentid", lastcommentid);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        public static DataTable Getuserdetails_service(string SpName, string email)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("emailid", email);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        public static DataTable Getstatusid_service(string SpName, int taskid)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("tid", taskid);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        public static string Updatetaskstatusid_service(string SpName, int inprogress, int taskid)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                //DataTable dt = new DataTable();
                string res = string.Empty;
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("inprogress", inprogress);
                    cmd.Parameters.Add("tid", taskid);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    //dt.Load(dr);
                    _Con.Close();
                    res = "1";

                }
                catch (Exception ex)
                {

                    res = "-1";
                }
                return res;
            }
        }

        public static string Updatetaskall_service(string SpName, int status, int userid, string cdate, int taskid)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                //DataTable dt = new DataTable();
                string res = string.Empty;
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("complete", status);
                    cmd.Parameters.Add("uid", userid);
                    cmd.Parameters.Add("ocdate", cdate);
                    cmd.Parameters.Add("tid", taskid);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    //dt.Load(dr);
                    _Con.Close();
                    res = "1";

                }
                catch (Exception ex)
                {

                    res = "0";
                }
                return res;
            }
        }

        public static DataTable Getallfiles_service(string SpName, int taskid)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("tid", taskid);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        public static DataTable Getoverduetask_service(string SpName, string cdate, int userid, int projectid, bool flag)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("currentdate", cdate);
                    cmd.Parameters.Add("uid", userid);
                    cmd.Parameters.Add("projectid", projectid);
                    cmd.Parameters.Add("oflag", flag);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        public static DataTable ReturnDataTableForMarico(string query)
        {
            DataTable Result = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(ConfigurationManager.AppSettings["connectionStringMarco"].ToString()))
                {
                    _con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, _con);
                    da.Fill(Result);
                    _con.Close();
                }
            }
            catch (Exception de)
            {
                Util.WriteToEventLog("Error: " + de.Message);
                //OpenConnection.Close();
            }
            return Result;
        }

        public static DataTable ReturnDataTableForSec(string query)
        {
            DataTable Result = new DataTable();
            try
            {
                using (SqlConnection _con = new SqlConnection(ConfigurationManager.AppSettings["connection"].ToString()))
                {
                    _con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, _con);
                    da.Fill(Result);
                    _con.Close();
                }
            }
            catch (Exception de)
            {
                Util.WriteToEventLog("Error: " + de.Message);
                //OpenConnection.Close();
            }
            return Result;
        }
        public static DataTable ReturnDataTableForMobileApps(string query)
        {
            DataTable Result = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionStringMobile"].ToString()))
                {
                    _con.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(query, _con);
                    da.Fill(Result);
                    _con.Close();
                }

            }
            catch (Exception de)
            {

                //OpenConnection.Close();
            }
            return Result;
        }
        public static int SelectPriorty(string Query)
        {
            int x = 0;
            try
            {
                using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _Con.Open();
                    MySqlCommand oCommandPro = new MySqlCommand(Query, _Con);
                    x = Convert.ToInt32(oCommandPro.ExecuteScalar());
                    _Con.Close();

                }
                return x;
            }
            catch
            {
                return x;
            }

        }
        public static int UpdatePriorty(string Query)
        {
            int x = 0;
            try
            {
                using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _Con.Open();
                    MySqlCommand oCommandPro = new MySqlCommand(Query, _Con);
                    x = oCommandPro.ExecuteNonQuery();
                    _Con.Close();
                }
                return x;
            }
            catch
            {
                return x;
            }

        }

        public static DataTable ReturnDataTableForAppsQueryReports(string query, string Cdate, string FilterType)
        {
            DataTable Result = new DataTable();
            try
            {
                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionStringMobileQueryReport"].ToString()))
                {
                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand(query);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cdata", Cdate);
                    cmd.Parameters.AddWithValue("@FilterType", FilterType);
                    cmd.Connection = _con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    Result.Load(dr);
                    _con.Close();
                }

            }
            catch (Exception de)
            {
                Console.Write(de.Message);
            }
            return Result;
        }

        public static DataTable ReturnDataTableForMobileQueryReport(string query)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionStringMobileQueryReport"].ToString()))
            {
                _con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(query, _con);
                da.Fill(dt);
            }

            return dt;
        }
        public static int Insert(string Query)
        {
            int x = 0;
            try
            {
                using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _Con.Open();
                    MySqlCommand oCommandPro = new MySqlCommand(Query, _Con);
                    oCommandPro.ExecuteNonQuery();
                    //_con.Close();
                    _Con.Close();
                    ++x;
                }
            }
            catch (MySqlException exx)
            {
                Util.WriteToEventLog("Insert query problem in mysql : " + exx.Message + " Query: " + Query);

            }

            return x;
        }


        public static int InsertBoard(string Procedure, string boardname, string ispublic, int userid, int TeamID)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                //DataTable dt = new DataTable();
                int response = 0;
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("board_name", boardname);
                    cmd.Parameters.Add("IsPublic", ispublic);
                    cmd.Parameters.Add("createdBy", userid);
                    cmd.Parameters.Add("boardteamID", TeamID);

                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    response = Convert.ToInt32(cmd.ExecuteScalar());
                    //dt.Load(dr);

                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                return response;
            }
        }
        public static int createlist(string Procedure, string projecttype, int boardid, int ProjectPriority)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                //DataTable dt = new DataTable();
                int response = 0;
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("ProjectType", projecttype);
                    cmd.Parameters.Add("BoardID", boardid);
                    cmd.Parameters.Add("ProjectPriority", ProjectPriority);

                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    response = Convert.ToInt32(cmd.ExecuteScalar());
                    //dt.Load(dr);

                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                return response;
            }
        }
        public static int CreateCard(string Procedure, string title, int projectyypeid, int taskpriority)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                //DataTable dt = new DataTable();
                int responses = 0;
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Title", title);
                    cmd.Parameters.Add("ProjectTypeID", projectyypeid);
                    cmd.Parameters.Add("TaskPriority", taskpriority);

                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    responses = Convert.ToInt32(cmd.ExecuteScalar());
                    //dt.Load(dr);

                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                return responses;
            }
        }
        public static DataTable GetProjects(string Procedure, int Board_ID, int UserId)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                //int responce = 0;
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Board_ID", Board_ID);
                    cmd.Parameters.Add("User_Id", UserId);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);

                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        public static DataTable AddComment(string Procedure, string comment, int task_id, int commented_By)
        {
            DateTime now = DateTime.Now;
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("taskid", task_id);
                    cmd.Parameters.Add("comments", comment);
                    cmd.Parameters.Add("commentedBy", commented_By);

                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);

                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        public static int SaveNewCards(string SpName, string oTitle, int oUserID, int oProjectID, int oStatusID, int oClientID, int oTaskPriority)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                int result = 0;
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("oTitle", oTitle);
                    cmd.Parameters.Add("oCreatedBy", oUserID);
                    cmd.Parameters.Add("oProjectTypeID", oProjectID);
                    cmd.Parameters.Add("oStatusID", oStatusID);
                    cmd.Parameters.Add("oClientID", oClientID);
                    cmd.Parameters.Add("oTaskPriority", oTaskPriority);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    _Con.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return result;
            }
        }

        public static int getscalar(string Query)
        {
            int x = 0;
            try
            {
                using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _Con.Open();
                    MySqlCommand oCommandPro = new MySqlCommand(Query, _Con);
                    x = Convert.ToInt32(oCommandPro.ExecuteScalar());
                    _Con.Close();

                }
                return x;
            }
            catch
            {
                return x;
            }

        }
        // Praveen
        public static int SaveNewList(string SpName, string List_Name, int Board_id, int oIsSeq, int oNoOfCards, string Color)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                int result = 0;
                try
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("listName", List_Name);
                    cmd.Parameters.Add("BoardIdd", Board_id);
                    cmd.Parameters.Add("IsSeq", oIsSeq);
                    cmd.Parameters.Add("NoOfCards", oNoOfCards);
                    cmd.Parameters.Add("Color", Color);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    _Con.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return result;
            }
        }


        #region Select for due date Created by Shobhana

        //code for due date
        public static string SelectDuedate(string Query)
        {
            string x = "";
            try
            {
                using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _Con.Open();
                    MySqlCommand oCommandPro = new MySqlCommand(Query, _Con);
                    x = (oCommandPro.ExecuteScalar()).ToString();
                    _Con.Close();

                }
                return x;
            }
            catch
            {
                return x;
            }

        }

        #endregion

        public static int Home_Board(string Query)
        {
            int x = 0;
            try
            {
                using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _Con.Open();
                    MySqlCommand oCommandPro = new MySqlCommand(Query, _Con);
                    x = Convert.ToInt32(oCommandPro.ExecuteScalar());
                    //_con.Close();
                    _Con.Close();

                }
            }
            catch (MySqlException exx)
            {
                Util.WriteToEventLog("Insert query problem in mysql : " + exx.Message + " Query: " + Query);

            }

            return x;
        }


        public static DataTable ProcForSelectTeam(string OperationStatus, int id, string SpName)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("TeamName1", "");
                    cmd.Parameters.Add("TeamType1", "");
                    cmd.Parameters.Add("WebsiteName1", "");
                    cmd.Parameters.Add("TeamDescription1", "");
                    cmd.Parameters.Add("CreatedBy1", 0);
                    cmd.Parameters.Add("OperationStatus1", OperationStatus);
                    cmd.Parameters.Add("ID1", id);
                    cmd.Parameters.Add("IsDeleted1", 0);
                    cmd.Parameters.Add("IsEnabled1", 0);
                    cmd.Connection = _Con;//OpenConnection;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }

        }

        /*******manoj*******/
        public static int BoardTeamOperation(string Procedure, string TeamName, string TeamType, string WebsiteName, string TeamDescription, int CreatedBy, string OperationStatus, int Id, int IsDeleted, int IsEnabled)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                //DataTable dt = new DataTable();
                int response = 0;
                try
                {
                    //TeamName,TeamType,WebsiteName,TeamDescription,CreatedBy
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("TeamName1", TeamName);
                    cmd.Parameters.Add("TeamType1", TeamType);
                    cmd.Parameters.Add("WebsiteName1", WebsiteName);
                    cmd.Parameters.Add("TeamDescription1", TeamDescription);
                    cmd.Parameters.Add("CreatedBy1", CreatedBy);
                    cmd.Parameters.Add("OperationStatus1", OperationStatus);
                    cmd.Parameters.Add("ID1", Id);
                    cmd.Parameters.Add("IsDeleted1", IsDeleted);
                    cmd.Parameters.Add("IsEnabled1", IsEnabled);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    response = Convert.ToInt32(cmd.ExecuteScalar());
                    //dt.Load(dr);
                    _Con.Close();
                }
                catch (Exception ex)
                {
                }
                return response;
            }
        }

        public static DataTable TeamUserAssignOperation(string Procedure, int Id, int TeamId, int UserId, string Description, int CreatedBy, int IsDeleted, int IsEnabled, string OperationStatus, string txtsearch)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    //TeamName,TeamType,WebsiteName,TeamDescription,CreatedBy
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Id1", Id);
                    cmd.Parameters.Add("TeamId1", TeamId);
                    cmd.Parameters.Add("MemberID1", UserId);
                    cmd.Parameters.Add("Description1", Description);
                    cmd.Parameters.Add("CreatedBy1", CreatedBy);
                    cmd.Parameters.Add("OperationStatus", OperationStatus);
                    cmd.Parameters.Add("IsDeleted1", IsDeleted);
                    cmd.Parameters.Add("IsEnabled1", IsEnabled);
                    cmd.Parameters.Add("txtsearch1", txtsearch);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                }
                catch (Exception ex)
                {
                }
                return dt;
            }
        }

        public static DataTable GetBoard(string Procedure, int UserID)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                //int responce = 0;
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("UserID", UserID);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);

                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        public static DataTable GetRecentBoards(string Procedure, int UserID)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                //int responce = 0;
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oUserID", UserID);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);

                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        public static DataTable GetCards(string Procedure, int ProjectID, int UserID, int boardid)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                //int responce = 0;
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Project_ID", ProjectID);
                    cmd.Parameters.Add("User_ID", UserID);
                    cmd.Parameters.Add("Board_ID", boardid);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);

                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        public static int AddEditDeleteassignedmember(string Procedure, int Id, int TeamId, int UserId, string Description, int CreatedBy, int IsDeleted, int IsEnabled, string OperationStatus, string txtsearch)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                int response = 0;
                try
                {
                    //TeamName,TeamType,WebsiteName,TeamDescription,CreatedBy
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Id1", Id);
                    cmd.Parameters.Add("TeamId1", TeamId);
                    cmd.Parameters.Add("MemberID1", UserId);
                    cmd.Parameters.Add("Description1", Description);
                    cmd.Parameters.Add("CreatedBy1", CreatedBy);
                    cmd.Parameters.Add("OperationStatus", OperationStatus);
                    cmd.Parameters.Add("IsDeleted1", IsDeleted);
                    cmd.Parameters.Add("IsEnabled1", IsEnabled);
                    cmd.Parameters.Add("txtsearch1", txtsearch);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    response = Convert.ToInt32(cmd.ExecuteScalar());
                    _Con.Close();
                }
                catch (Exception ex)
                {
                }
                return response;
            }
        }

        public static DataTable prockforshowlist(string Procedure, string OperationStatus, int boardid, int projectid, string TaskID, string txtsearch)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    //TeamName,TeamType,WebsiteName,TeamDescription,CreatedBy
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("OperationStatus", OperationStatus);
                    cmd.Parameters.Add("boardid1", boardid);
                    cmd.Parameters.Add("ProjectTypeID1", projectid);
                    cmd.Parameters.Add("TaskID1", TaskID);
                    cmd.Parameters.Add("txtsearch1", txtsearch);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);

                    _Con.Close();
                }
                catch (Exception ex)
                {
                }
                return dt;
            }
        }
        public static int undotaskfor(string Procedure, string OperationStatus, int boardid, int projectid, string taskid, string txtsearch)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                int response = 0;
                try
                {
                    //TeamName,TeamType,WebsiteName,TeamDescription,CreatedBy
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("OperationStatus", OperationStatus);
                    cmd.Parameters.Add("boardid1", boardid);
                    cmd.Parameters.Add("ProjectTypeID1", projectid);
                    cmd.Parameters.Add("TaskID1", taskid);
                    cmd.Parameters.Add("txtsearch1", txtsearch);

                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    response = Convert.ToInt32(cmd.ExecuteNonQuery());
                    _Con.Close();
                }
                catch (Exception ex)
                {
                }
                return response;
            }
        }


        //#region RJ Methods
        //
        // Get All activity List By TaskID
        //
        public static DataTable ReturnAllActivityLog(string SpName)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        public static DataTable ReturnGetAllActivityLogByDate(string SpName, int boardid, int UserId, string ActivityDate)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("BoardId", boardid);
                    cmd.Parameters.Add("UserId", UserId);
                    cmd.Parameters.Add("ActivityDate", ActivityDate);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        public static DataTable GetActivityForGraph(string Procedure, int days, int UserID)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                //int responce = 0;
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = Procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("UserID", UserID);
                    cmd.Parameters.Add("Days", days);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);

                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }

        public static int GetBoardTemplateId(string procedure, string TamplateName, int BoardId, int TeamId, int IsPublic, int IsSaveList, int IsSaveListWithCard)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                int responce = 0;
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("Name", TamplateName);
                    cmd.Parameters.Add("BoardId", BoardId);
                    cmd.Parameters.Add("TeamId", TeamId);
                    cmd.Parameters.Add("IsPublic", IsPublic);
                    cmd.Parameters.Add("IsSaveList", IsSaveList);
                    cmd.Parameters.Add("IsSaveListWithCard", IsSaveListWithCard);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                        responce = Convert.ToInt32(dr["BoardTemplateId"]);

                    _Con.Close();


                }
                catch (Exception ex)
                {


                }
                return responce;

            }
        }

        public static int GetInsertBoardTemplateWithList(string procedure, int BoardTemplateid, int ProjectTypeId, string ListName)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                int responce = 0;
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("BoardTemplateid", BoardTemplateid);
                    cmd.Parameters.Add("ProjectTypeId", ProjectTypeId);
                    cmd.Parameters.Add("ListName", ListName);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                        responce = Convert.ToInt32(dr["BoardTemplateListId"]);

                    _Con.Close();


                }
                catch (Exception ex)
                {

                }
                return responce;

            }
        }

        public static int InsertBoardTemplateWithCard(string procedure, int BoardTemplateListId, int TaskId, string cardname)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {

                int responce = 0;
                try
                {

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = procedure;
                    cmd.Connection = _Con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("BoardTemplateListId", BoardTemplateListId);
                    cmd.Parameters.Add("TaskId", TaskId);
                    cmd.Parameters.Add("CardName", cardname);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    _Con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                        responce = Convert.ToInt32(dr["BoardTemplateCardId"]);

                    _Con.Close();


                }
                catch (Exception ex)
                {

                }
                return responce;

            }
        }


        public static DataTable GetAllgenericTask(string SpName, int oUserID)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }


        public static DataTable GetAllTaskByBoardId(string SpName, int oUserID, int oBoardID)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oUserID", oUserID);
                    cmd.Parameters.Add("oBoardID", oBoardID);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        public static DataTable ReturnAttachmentsCount(string SpName, int userid, string timetype)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("useridd", userid);
                    cmd.Parameters.Add("timetype", timetype);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        public static DataTable RunQueryInProcedure(string Query)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "RunQuery";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("querys", Query);

                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                    // OpenConnection.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }

        }

        public static DataTable GetAllProjectUserWise(string SpName)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {
                }
                return dt;
            }
        }
        public static DataTable GetAllBoardforManger(string SpName, int oUserid)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("UserID", oUserid);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }

        }

        public static DataTable GetAllProjectsforManager(string SpName, int oUserid, string Boardid)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("UserID", oUserid);
                    cmd.Parameters.Add("BoardId", Boardid);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
                }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }

        }
        public static void InsertFavoriteBoard(string SpName, int BoardID, int createdBy)
        {

            try
            {
                using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oBoardID", BoardID);
                    cmd.Parameters.Add("oCreatedBy", createdBy);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    cmd.ExecuteNonQuery();
                    //_con.Close();
                    _Con.Close();
                }
            }
            catch (MySqlException exx)
            {
                Util.WriteToEventLog("Insert query problem in mysql");
            }
            catch (Exception se)
            {
                // Debug.WriteLine(se.Message);
                Util.WriteToEventLog("Insert query problem");
                //  _con.Close();
                //  OpenConnection.Close();
            }
        }
        
        public static DataTable GetTaskReportTotalWorkingTime(string SpName, string oUserid, string oCurrentDate, string oNextdate)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oUserid", oUserid);
                    cmd.Parameters.Add("oCurrentDate", oCurrentDate);
                    cmd.Parameters.Add("oNextdate", oNextdate);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();
              }
                catch (Exception ex)
                {
                    // Debug.WriteLine(ex.Message);

                }
                return dt;
            }

        }
        public static DataSet ReturnDataTables(string SpName, int userid, string timetype)
        {
            DataSet Result = new DataSet();
            try
            {

                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {

                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("useridd", userid);
                    cmd.Parameters.Add("timetype", timetype);
                    cmd.Connection = _con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(Result);
                    _con.Close();
                }
            }
            catch (Exception de)
            {
                Util.WriteToEventLog("Error: " + de.Message);
                //OpenConnection.Close();
            }
            return Result;
        }
        public static DataTable ReturnEmailNotificationDetail(string SpName, string obasis)
        {
            using (MySqlConnection _Con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    _Con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("obasis", obasis);
                    cmd.Connection = _Con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    _Con.Close();

                }
                catch (Exception ex)
                {


                }
                return dt;
            }
        }
        public static DataSet ReturnMultipleDataTable(string SpName, string oBasis, int oUserid)
        {
            DataSet Result = new DataSet();
            try
            {

                using (MySqlConnection _con = new MySqlConnection(ConfigurationManager.AppSettings["connectionString"].ToString()))
                {

                    _con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("oBasis", oBasis);
                    cmd.Parameters.Add("oUserid", oUserid);
                    cmd.Connection = _con;
                    cmd.CommandTimeout = 1000000;
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(Result);
                    _con.Close();
                }
            }
            catch (Exception de)
            {
                Util.WriteToEventLog("Error: " + de.Message);
                //OpenConnection.Close();
            }
            return Result;
        }                                         


    }
}


