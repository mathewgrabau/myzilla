using System;
using System.Collections.Generic;
using System.Text;
using System.IO ;
using System.Collections.Specialized ;
using System.Collections;
using System.ComponentModel;

namespace MyZilla.BusinessEntities
{

    /// <summary>
    /// A data set object that stores all the application settings.
    /// </summary>
    /// <remarks>
    /// Implemented as a singleton.
    /// </remarks> 
    public class MyZillaSettingsDataSet : TDSettings
    {
        #region Variables

        private static MyZillaSettingsDataSet _instance;

        private string _fileName;

        // used for encrypt/decript the password.
        // RijndaelCipher algorithm was used.
        private string encryptionKey = string.Empty;

        #endregion

        #region Events

        //public delegate void SaveSettingsHandler(object sender, MyZillaSettingsEventArgs  e);

        public event EventHandler<MyZillaSettingsEventArgs> SaveSettings;

        #endregion

        #region Constructor / Instance

        private MyZillaSettingsDataSet( string userPathData)
        {
            // set the path where the xml file are stored.
            _fileName = userPathData  + "\\BSISettings.xml";

            try
            {

                if (File.Exists(_fileName))
                {
                    this.ReadXml(_fileName);

                    if (this.Connection.Rows.Count > 0)
                    {
                        for (int i = 0; i < this.Connection .Rows.Count; i++)
                        {
                            TDSettings.ConnectionRow connection = this.Connection.Rows[i] as TDSettings.ConnectionRow ;

                            connection.Password = EncryptDecrypt.DecryptString(connection.Password, encryptionKey);
                        }
                    }

                    //prevent access to SortIndex field when this field is null
                    for (int i = 0; i < this.Columns.Count; i++) {
                        try
                        {
                            this.Columns[i].SortIndex = this.Columns[i].SortIndex;
                        }
                        catch {
                            this.Columns[i].SortIndex = 0;
                        }
                    }
                    this.Columns.AcceptChanges();
                    
                }


            }
            catch (IOException)
            {
                throw (new IOException("File " + _fileName + " not exist."));
            }

        }

        /// <summary>
        /// Called once, when global data are instantiated.
        /// </summary>
        /// <param name="userPathData"></param>
        /// <returns></returns>
        public static MyZillaSettingsDataSet CreateInstance(string userPathData)
        {
            if (_instance == null)
            {
                _instance = new MyZillaSettingsDataSet(userPathData);
            }

            return _instance;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static MyZillaSettingsDataSet GetInstance ()
        {
            if (_instance == null)
            {
                throw new NullReferenceException("The instance for the class has not been previously created");
            }
            else
            {
                return _instance;
            }
        }

        #endregion

        #region Public methods

        private void SaveXML( SavingData data)
        {
            this.EncryptPasswordsBeforeSaving();

            this.WriteXml(_fileName);

            this.DecryptPasswordsAfterSaving();

            this.AcceptChanges();

            if (data == null)
            {
                return;
            }
            else
            {
                // fire the event
                if (SaveSettings != null)
                {
                    this.SaveSettings(this, new MyZillaSettingsEventArgs ( data ) );
                }
           
            }

        }

        #region Connection

        public void AddConnectionType (string connectionType)
        {

            TDSettings.ConnectionTypeRow row = this.ConnectionType.NewConnectionTypeRow ();

            row.Name = connectionType ;

            this.ConnectionType.Rows.Add(row);

            // no event is fired for this functionality
            this.SaveXML( null); 

        }

        public int AddConnection(string connectionName, string url, string connectionType, string userName, string password, bool rememberPass, bool activeUser)
        {
            TDSettings.ConnectionRow connectionRow = this.Connection.NewConnectionRow();

            connectionRow.ConnectionName = connectionName;

            connectionRow.URL = url;

            connectionRow.Type = connectionType;

            connectionRow.UserName = userName;

            connectionRow.Password = password;

            connectionRow.RememberPassword = rememberPass;

            connectionRow.ActiveUser = activeUser; 

            this.Connection.Rows.Add(connectionRow);

            SavingData sp = new SavingData( OperationType.AddConnection , connectionRow  );

            this.SaveXML( sp);

            return connectionRow.ConnectionId ; 
        }

        public void EditConnection(int connectionId, string connectionName, string url, string connectionType, string userName, string password, bool rememberPass, bool activeUser)
        {
            TDSettings.ConnectionRow[] rowConn = this.Connection.Select("ConnectionId=" + connectionId.ToString()) as TDSettings.ConnectionRow[];

            if (rowConn != null && rowConn.Length == 1 )
            {

                rowConn[0].ConnectionName = connectionName;

                rowConn[0].URL = url;

                rowConn[0].Type = connectionType;

                rowConn[0].UserName = userName;

                rowConn[0].Password = password;

                rowConn[0].RememberPassword = rememberPass;

                rowConn[0].ActiveUser = activeUser; 

                SavingData sp = new SavingData( OperationType.EditConnection , rowConn[0]  );

                this.SaveXML( sp);
            }
        }

        public void EditConnection(TDSettings.ConnectionRow connectionValues)
        {
            TDSettings.ConnectionRow[] rowConn = this.Connection.Select("ConnectionId=" + connectionValues.ConnectionId) as TDSettings.ConnectionRow[];

            if (rowConn != null && rowConn.Length == 1)
            {

                rowConn[0].ConnectionName = connectionValues.ConnectionName;

                rowConn[0].URL = connectionValues.URL;

                rowConn[0].Type = connectionValues.Type;

                rowConn[0].UserName = connectionValues.UserName;

                rowConn[0].Password = connectionValues.Password;

                rowConn[0].RememberPassword = connectionValues.RememberPassword;

                rowConn[0].ActiveUser = connectionValues.ActiveUser;

                rowConn[0].Charset = connectionValues.Charset;

                SavingData sp = new SavingData(OperationType.EditConnection, rowConn[0]);

                this.SaveXML(sp);
            }
        }

        public void SetVersionForConnection(int connectionId, string version)
        {
            TDSettings.ConnectionRow[] rowConn = this.Connection.Select("ConnectionId=" + connectionId.ToString()) as TDSettings.ConnectionRow[];

            if (rowConn != null && rowConn.Length == 1)
            {
                rowConn[0].Version = version; 
            }
        }


        public void DeleteConnection(int connectionId)
        {
            TDSettings.ConnectionRow[] connRow = this.Connection.Select("ConnectionId=" + connectionId.ToString()) as TDSettings.ConnectionRow[];

            if (connRow != null && connRow.Length == 1)
            {
                this.Connection.RemoveConnectionRow(connRow[0]);

                ConnectionRow delConn = new ConnectionDataTable ().NewConnectionRow ();

                delConn.ConnectionId = connectionId; 

                SavingData sp = new SavingData (OperationType.DeleteConnection , delConn  );
                
                this.SaveXML( sp);
            }
        }

        #endregion

        #region Gets 

        public TDSettings.ConnectionRow GetConnectionById(int connectionId)
        {

            TDSettings.ConnectionRow result = null;

            TDSettings.ConnectionRow[] rows = this.Connection.Select("ConnectionId=" + connectionId.ToString()) as TDSettings.ConnectionRow [];

            if (rows != null &&  rows.Length == 1)
            {
                result = rows[0];
            }

            return result;

        }


        /// <summary>
        /// Get all the users who are active and the credentials are known. 
        /// </summary>
        /// <returns>
        /// A collection of active users if any.
        /// A collection with 0 elements if not active users.
        /// </returns>
        public TDSettings.ConnectionDataTable GetActiveConnections()
        {
            TDSettings.ConnectionDataTable dt = new ConnectionDataTable();


            foreach (TDSettings.ConnectionRow conn in this.Connection.Rows)
            {
                if (conn.ActiveUser == true && conn.RememberPassword == true)
                {
                    dt.Rows.Add(conn.ItemArray);
                }
            }

            return dt;
        }

        public string GetUrlForConnection(int connectionId)
        {
            string result = null;

            TDSettings.ConnectionRow[] connRow = this.Connection.Select("ConnectionId=" + connectionId) as TDSettings.ConnectionRow[];

            if (connRow != null && connRow.Length == 1 )
            {
                if ((connRow[0].URL.Length > 0) && (!connRow[0].URL.EndsWith("/")))
                    result = String.Concat(connRow[0].URL, "/");
                else
                    result = connRow[0].URL;
            }

            return result;
        }

        public string GetConnectionInfo(int connectionId)
        {
            string result = null;

            TDSettings.ConnectionRow[] connRow = this.Connection.Select("ConnectionId=" + connectionId.ToString()) as TDSettings.ConnectionRow[];

            if (connRow != null &&  connRow.Length == 1)
            {
                //result = connRow[0].ConnectionName + "\\" + connRow[0].UserName;
                result = connRow[0].ConnectionName;
            }
            return result;

        }

        /// <summary>
        /// 'userID, connectionName\userName'
        /// </summary>
        /// <returns></returns>
        public NameValueCollection GetAllConnections()
        {
            NameValueCollection result = new NameValueCollection();

            TDSettings.ConnectionDataTable lstConn = this.GetActiveConnections();

            foreach ( TDSettings.ConnectionRow conn in lstConn )
            {
                result.Add( conn.ConnectionId .ToString () , conn.ConnectionName + "\\" + conn.UserName   ); 
            }

            return result;

        }

        #endregion

        #region Global settings

        public TDSettings.GlobalSettingsRow GetGlobalSettings()
        {
            return GetGlobalSettings(new System.Drawing.Rectangle());
        }

        public TDSettings.GlobalSettingsRow GetGlobalSettings(System.Drawing.Rectangle screenSize)
        {

            if (_instance.GlobalSettings.Rows.Count == 0)
            {
                TDSettings.GlobalSettingsRow result = _instance.GlobalSettings.NewGlobalSettingsRow();

                result.ShowLoadingForm = true;
                result.TreePanelWidth = 180;

                result.MainFormHeight = 768 - 20;
                result.MainFormWidth = 1024 - 50;

                result.MainFormLeft = (screenSize.Width - result.MainFormWidth) / 2;
                result.MainFormTop = (screenSize.Height - result.MainFormHeight) / 2;

                result.ConfirmSuccessfullyEditBug = true;

                result.ShowBugsCount = false;

                result.ReportFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);   

                _instance.GlobalSettings.AddGlobalSettingsRow(result);

                _instance.GlobalSettings.AcceptChanges();
 
                return result;


            }
            else
            {
                GlobalSettingsRow settings = this.GlobalSettings.Rows[0] as TDSettings.GlobalSettingsRow;
                
                try{
                    if (settings.MainFormLeft == Int32.MaxValue) { }
                }
                catch {
                    settings.MainFormLeft = (screenSize.Width - settings.MainFormWidth) / 2;
                }
                try
                {
                    if (settings.MainFormTop == Int32.MaxValue) { }
                }
                catch
                {
                    settings.MainFormTop = (screenSize.Height - settings.MainFormHeight) / 2;
                }
                try
                {
                    if (settings.MainFormMaximized) { }
                }
                catch
                {
                    settings.MainFormMaximized = false;
                }

                try
                {
                    if (String.IsNullOrEmpty(settings.ReportFilesPath)) { }
                }
                catch
                {
                    settings.ReportFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);   
                }
                settings.AcceptChanges();

                return (settings);
            }

        }

        public void SaveGlobalSettings(TDSettings.GlobalSettingsRow globalSettingsRow)
        {

            if (this.GlobalSettings.Rows.Count == 0 )
            {
                TDSettings.GlobalSettingsRow newGlobalSettingsRow = this.GlobalSettings.NewGlobalSettingsRow();

                newGlobalSettingsRow.ShowLoadingForm = globalSettingsRow.ShowLoadingForm;
                newGlobalSettingsRow.MainFormHeight = globalSettingsRow.MainFormHeight;
                newGlobalSettingsRow.MainFormWidth = globalSettingsRow.MainFormWidth;
                newGlobalSettingsRow.TreePanelWidth = globalSettingsRow.TreePanelWidth;
                newGlobalSettingsRow.ConfirmSuccessfullyEditBug = globalSettingsRow.ConfirmSuccessfullyEditBug;
                newGlobalSettingsRow.ShowBugsCount = globalSettingsRow.ShowBugsCount;
                newGlobalSettingsRow.ReportFilesPath = globalSettingsRow.ReportFilesPath; 
                

                this.GlobalSettings.Rows.Add(newGlobalSettingsRow);  
            }
            else
            {
                (this.GlobalSettings.Rows[0] as TDSettings.GlobalSettingsRow).ShowLoadingForm = globalSettingsRow.ShowLoadingForm;
                (this.GlobalSettings.Rows[0] as TDSettings.GlobalSettingsRow).MainFormHeight = globalSettingsRow.MainFormHeight;
                (this.GlobalSettings.Rows[0] as TDSettings.GlobalSettingsRow).MainFormWidth = globalSettingsRow.MainFormWidth;
                (this.GlobalSettings.Rows[0] as TDSettings.GlobalSettingsRow).TreePanelWidth = globalSettingsRow.TreePanelWidth;
                (this.GlobalSettings.Rows[0] as TDSettings.GlobalSettingsRow).ConfirmSuccessfullyEditBug = globalSettingsRow.ConfirmSuccessfullyEditBug  ;
                (this.GlobalSettings.Rows[0] as TDSettings.GlobalSettingsRow).ShowBugsCount  = globalSettingsRow.ShowBugsCount ;
                (this.GlobalSettings.Rows[0] as TDSettings.GlobalSettingsRow).ReportFilesPath = globalSettingsRow.ReportFilesPath ;

            }

            this.SaveXML( null );

        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// The passwords must be encrypted before saving them in xml.
        /// </summary>
        private void EncryptPasswordsBeforeSaving()
        {
            if (this.Connection.Rows.Count > 0)
            {
                for (int i = 0; i < this.Connection.Rows.Count; i++)
                {
                    TDSettings.ConnectionRow connection = this.Connection.Rows[i] as TDSettings.ConnectionRow ;

                    connection.Password = EncryptDecrypt.EncryptString(connection.Password, encryptionKey);
                }
            }
        }

        /// <summary>
        /// The passwords must be encrypted before saving them in xml.
        /// </summary>
        private void DecryptPasswordsAfterSaving()
        {
            if (this.Connection.Rows.Count > 0)
            {
                for (int i = 0; i < this.Connection.Rows.Count; i++)
                {
                    TDSettings.ConnectionRow connection = this.Connection.Rows[i] as TDSettings.ConnectionRow ;

                    connection.Password = EncryptDecrypt.DecryptString(connection.Password, encryptionKey);
                }
            }

        }

        #endregion

    }

    /// <summary>
    /// Manages user preferences.
    /// </summary>
    public class Preferences
    {

        public Preferences()
        {

        }

    }

    public enum OperationType
    {
        AddConnectionType,
        AddConnection,
        AddConnectionThrowsError,
        EditConnection,
        DeleteConnection,
        LogOnFailed,
        SaveGlobalSettings
    };

    public class SavingData  
    {

        private OperationType _operation;

        private string  _errorMessage;

        private TDSettings.ConnectionRow _connectionRow; 

        public SavingData( OperationType operation, TDSettings.ConnectionRow connectionRow )
        {
            _operation = operation;

            _connectionRow = connectionRow;
        }

        public OperationType Operation
        {
            get { return _operation; }
            set { _operation = value; }

        }

        public TDSettings.ConnectionRow ConnectionRow
        {
            get
            {
                return _connectionRow;
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

    }

    public class MyZillaSettingsEventArgs : EventArgs
    {
        private SavingData _data;

        public MyZillaSettingsEventArgs( SavingData data)
        {
            _data = data;
        }

        public SavingData SaveParameter
        {
            get
            {
                return _data;
             }
        }
}


}
