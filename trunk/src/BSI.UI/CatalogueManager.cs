using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized ;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms; 

using MyZilla.BL.Interfaces;
using MyZilla.UI.Properties;
using MyZilla.BusinessEntities;

using Tremend.Logging;

namespace MyZilla.UI
{
    /// <summary>
    /// Manages catalogues for the active users in the application.
    /// </summary>
    /// <remarks >
    /// 1. Implemented as a singleton.
    /// 2. Spalsher class is used.
    /// </remarks>
    public class CatalogueManager
    {
        #region Variables

        private static CatalogueManager _instance;

        private static MyZillaSettingsDataSet _appSettings;

        // <userID (int), catalogues for userID (BSI.BusinessEntities.Catalogues)

        private List<MyZilla.BusinessEntities.Catalogues> globalCataloguesCollection = new List<MyZilla.BusinessEntities.Catalogues>();

        public int activeThreads;

        private static AsyncOperationManagerList _asyncOpManager = AsyncOperationManagerList.GetInstance () ;

        #endregion

        #region Private constructor

        private CatalogueManager( )
        {
            _appSettings = MyZillaSettingsDataSet.GetInstance ();

            _appSettings.SaveSettings +=new EventHandler<MyZillaSettingsEventArgs>(_appSettings_SaveSettings);

            if (_appSettings.ConnectionType.Rows.Count == 0 )     
            {
                _appSettings.AddConnectionType("Bugzilla"); 
            }

        }


        #endregion

        #region Get Instance

        public static CatalogueManager Instance (  )
        {
            if (_instance == null)
            {
                _instance = new CatalogueManager();

              

            }
            return _instance;
        }

        #endregion

        #region Event 


        public delegate void CataloguesEventHandler(object sender, MyZillaSettingsEventArgs e);

        public event CataloguesEventHandler CatalogueEvent;

        public event EventHandler DependentCataloguesLoadedCompleted;

        public event EventHandler CompAndVersionCataloguesLoadedCompleted;

        #endregion

        #region Public methods

        public MyZilla.BusinessEntities.Catalogues GetCataloguesForConnection(int connectionId)
        {
            MyZilla.BusinessEntities.Catalogues result = null;

            for (int i = 0; i < globalCataloguesCollection.Count; i++)
            {
                if (globalCataloguesCollection[i].ConnectionId  == connectionId )
                {
                    result = globalCataloguesCollection[i];

                    break;
                }
            }

            return result;
        }

        public bool AreCataloguesLoaded(int connectionId)
        {
            bool areLoaded = false;

            for (int i = 0; i < globalCataloguesCollection.Count; i++)
            {
                if (globalCataloguesCollection[i].ConnectionId  == connectionId )
                {
                    areLoaded = true;

                    break;
                }
            }

            return areLoaded;

        }

        public void RemoveCataloguesForConnection(int connectionId)
        {
            for (int i = 0; i < globalCataloguesCollection.Count; i++)
            {
                if (globalCataloguesCollection[i].ConnectionId  == connectionId )
                {
                    globalCataloguesCollection.RemoveAt(i);

                    break;
                }
            }

        }

        // TO DO load catalogues for connection
        public void LoadCataloguesForUser(int connectionId)
        {

            TDSettings.ConnectionRow connection = _appSettings.GetConnectionById (connectionId ) ;

            if (String.IsNullOrEmpty(connection.Charset))
            {
                IUtilities utilities = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(connectionId);

                connection.Charset = utilities.GetBugzillaCharset(connection.URL);

                _appSettings.EditConnection(connection);
            }

            SavingData data = new SavingData( OperationType.EditConnection , connection  );

            this.LoadCataloguesForUser(data);
        }

        public int Count()
        {
            return globalCataloguesCollection.Count; 
        }

        public Catalogues GetCataloguesByIndex(int index)
        {
            return globalCataloguesCollection[index]; 
        }

        public NameValueCollection GetActiveConnections()
        {
            NameValueCollection activeConnections = new NameValueCollection();

            foreach (Catalogues catalogues in globalCataloguesCollection)
            {
                int connID = catalogues.ConnectionId;

                string connInfo = _appSettings.GetConnectionInfo(connID);

                activeConnections.Add(connID.ToString(), connInfo);
            }

            return activeConnections;

        }

        #endregion

        #region Load catalogues for valid users 

        /// <summary>
        /// Load catalogues for all users that are active and the credentials are known.
        /// </summary>
        public void LoadCataloguesForActiveConnections()
        {
            #if DEBUG
                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            #endif


            // get all active users

            TDSettings.ConnectionDataTable  activeConnections = _appSettings.GetActiveConnections ();

            if (activeConnections.Rows.Count > 0)
            {

                for (int iThread = 0; iThread < activeConnections.Count; iThread++)
                {

                    Interlocked.Increment(ref activeThreads);

                    TDSettings.ConnectionRow currentConnection = activeConnections.Rows[iThread] as TDSettings.ConnectionRow;

                    ParameterizedThreadStart operation = new ParameterizedThreadStart(StartAsyncOperation);

                    Thread ts = new Thread(operation);

                    ts.Start(currentConnection);

                }

                // Wait for all threads to complete their task...
                do
                {

                }
                while (activeThreads != 0);

                
#if DEBUG
                watch.Stop();
                MyLogger.Write(watch.ElapsedMilliseconds.ToString(), "LoadCataloguesForActiveConnections", LoggingCategory.Debug);
#endif


            }
        }

        private static Catalogues LoadMainCatalogues(TDSettings.ConnectionRow connectionRow, BackgroundWorker backgroudWorker)
        {
#if DEBUG
            string methodName = "LoadMainCatalogues";
#endif
            #if DEBUG
                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            #endif

            backgroudWorker.ReportProgress(0);

            backgroudWorker.ReportProgress(10); 

            // when refresh catalogues, it is no need to verify login.

            IUser user = (IUser)BLControllerFactory.GetRegisteredConcreteFactory(connectionRow.ConnectionId );

            string userIsLogged = user.LogOnToBugzilla(connectionRow.UserName, connectionRow.Password);

            
            #if DEBUG
                watch.Stop();
                MyLogger.Write(watch.ElapsedMilliseconds.ToString(), methodName, LoggingCategory.Debug);
                watch.Start();
            #endif

            if (userIsLogged.Length > 0)
            {
                // this could happen because a wrong password was saved.

                backgroudWorker.ReportProgress(100);

                return null;

            }

            IUtilities catalogue = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(connectionRow.ConnectionId );

            string[] catalogNames = new string[] { "classification", "product", "bug_status", "resolution", "bug_severity", "priority", "rep_platform", "op_sys", "short_desc_type", "field0-0-0", "type0-0-0" };

            // get all the main catalogues (catalogues without dependencies) used in the application
            // request
            ArrayList collCatalogues = catalogue.GetCatalogues(catalogNames);



#if DEBUG
            watch.Stop();
            MyLogger.Write(watch.ElapsedMilliseconds.ToString(), methodName, LoggingCategory.Debug);
            watch.Start();
#endif
            // get the connection/user information

            MyZilla.BusinessEntities.Catalogues cataloguesPerUser = new MyZilla.BusinessEntities.Catalogues(connectionRow.ConnectionId);

            #region Main Catalogues

            // get the product catalogue
            List<string> lstProduct = collCatalogues[1] as List<string>;

            cataloguesPerUser.catalogueProduct = new NameValueCollection();

            foreach (string strProduct in lstProduct)
            {
                cataloguesPerUser.catalogueProduct.Add(strProduct, string.Empty);
            }

            backgroudWorker.ReportProgress(80);


            // get the rest of the main catalogues and populate the corresponding controls.
            // string[] catalogNames = new string[] {"product", 
            // "bug_status", 
            // "resolution", 
            // "bug_severity", 
            // "priority", 
            // "rep_platform", 
            // "op_sys",
            // "short_desc_type"};

            cataloguesPerUser.catalogueStatus = collCatalogues[2] as List<string>;

            cataloguesPerUser.catalogueResolution = collCatalogues[3] as List<string>;

            cataloguesPerUser.catalogueSeverity = collCatalogues[4] as List<string>;

            cataloguesPerUser.cataloguePriority = collCatalogues[5] as List<string>;

            cataloguesPerUser.catalogueHardware = collCatalogues[6] as List<string>;

            cataloguesPerUser.catalogueOS = collCatalogues[7] as List<string>;

            cataloguesPerUser.catalogueStringOperators = collCatalogues[8] as List<string>;

            cataloguesPerUser.catalogueFields = collCatalogues[9] as List<string>;

            cataloguesPerUser.catalogueOperators = collCatalogues[10] as List<string>;

            #endregion

            backgroudWorker.ReportProgress(100);

#if DEBUG
            watch.Stop();
            MyLogger.Write(watch.ElapsedMilliseconds.ToString(), methodName, LoggingCategory.Debug);
#endif

            return cataloguesPerUser;
        }

        private void StartAsyncOperation( object currentUser)
        {

            // get the main catalogues 
            BackgroundWorker bwLoadCatalogues = new BackgroundWorker();

            bwLoadCatalogues.DoWork += new DoWorkEventHandler(bwLoadCatalogues_DoWork);

            bwLoadCatalogues.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadCatalogues_RunWorkerCompleted);

            bwLoadCatalogues.ProgressChanged += new ProgressChangedEventHandler(bwLoadCatalogues_ProgressChanged);

            bwLoadCatalogues.WorkerReportsProgress = true;

            bwLoadCatalogues.WorkerSupportsCancellation = true;

            bwLoadCatalogues.RunWorkerAsync( currentUser );


        }

        private void bwLoadCatalogues_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;


            switch (e.ProgressPercentage)
            {
                case 0:


                    if (_asyncOpManager != null)
                    {
                        _asyncOpManager.BeginOperation(backgroundWorker, Messages.LoadCataloguesInProgress, e.ProgressPercentage);
                    }

                    break;


                case 100:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.UpdateStatus(backgroundWorker, Messages.LoadCataloguesInProgress , e.ProgressPercentage);
                    }

                    break;

                default:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.UpdateStatus(backgroundWorker, Messages.LoadCataloguesInProgress, e.ProgressPercentage);
                    }

                    break;

            }


        }

        private void bwLoadCatalogues_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {

                TDSettings.ConnectionRow   currentConnection = e.Argument as TDSettings.ConnectionRow  ;

                Catalogues cataloguesPerUser =  CatalogueManager.LoadMainCatalogues ( currentConnection, worker);

                e.Result = cataloguesPerUser  ;

            }
            catch (Exception ex)
            {
                SplashManager.Close(); // just in case

                MyLogger.Write(ex, "bwLoadCatalogues_DoWork", LoggingCategory.Exception);

                worker.ReportProgress(100); 
 
                throw ;
            }

        }

        private void bwLoadCatalogues_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check the status of the async operation.
                if (e.Error != null)
                {
                    string errorMessage = Messages.ErrLoadingCatalogues + Environment.NewLine + e.Error.Message;

                    MessageBox.Show(Utils.FormContainer, errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // status OK
                if (!e.Cancelled && e.Error == null)
                {
                    if (e.Result != null)
                    {
                        Catalogues cataloguesPerUser = e.Result as Catalogues;

                        globalCataloguesCollection.Add(cataloguesPerUser);
                    }

                }

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bwLoadCatalogues_RunWorkerCompleted", LoggingCategory.Exception);

                throw new Exception(Messages.ErrLoadingCatalogues, ex);

            }
            finally
            {
                Interlocked.Decrement(ref activeThreads);
            }
        }

        #endregion

        #region Load Catalogues For User (Async)

        void bkgCatalogues_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;

            string connectionInfo = string.Empty;

            if (e.UserState != null)
            {
                connectionInfo = e.UserState.ToString();
            }

            string strMessage = string.Format(Messages.LoadCataloguesInProgress, connectionInfo); 

            switch (e.ProgressPercentage)
            {
                case 0:


                    if (_asyncOpManager != null)
                    {
                        _asyncOpManager.BeginOperation(backgroundWorker, strMessage , e.ProgressPercentage);
                    }

                    break;


                case 100:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.UpdateStatus(backgroundWorker, strMessage , e.ProgressPercentage);
                    }

                    break;

                default:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.UpdateStatus(backgroundWorker, strMessage , e.ProgressPercentage);
                    }

                    break;

            }

        }

        void bkgCatalogues_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

                if (e.Error != null)
                {

                    CustomException ex = (CustomException)e.Error;

                    string errorMessage;

                    if (ex != null)
                    {
                        if (this.CatalogueEvent != null)
                        {
                            SavingData sp = ex.CustomData;

                            if (sp.Operation == OperationType.LogOnFailed)
                            {
                                // no code here.
                            }
                            else
                            {
                                sp.Operation = OperationType.AddConnectionThrowsError;
                            }

                            if (this.CatalogueEvent != null)
                            {

                                this.CatalogueEvent(this, new MyZillaSettingsEventArgs(sp));
                            }
                        }

                        errorMessage = Messages.ErrLoadingCatalogues + Environment.NewLine + ex.InnerException.Message;
                    }
                    else
                    {
                        errorMessage = Messages.ErrLoadingCatalogues + Environment.NewLine + e.Error.Message;
                    }

                    if (!string.IsNullOrEmpty(errorMessage)) {
                        MyLogger.Write(errorMessage, "bkgCatalogues_RunWorkerCompleted", LoggingCategory.Exception);
                    }

                }

                // status OK
                if (!e.Cancelled && e.Error == null)
                {
                    ArrayList result = e.Result as ArrayList;

                    Catalogues cataloguesPerUser = result[0] as Catalogues;

                    SavingData sp = result[1] as SavingData;

                    if (result[0] != null)
                    {

                        // check if catalogues exist
                        bool areLoaded = this.AreCataloguesLoaded(cataloguesPerUser.ConnectionId);

                        if (areLoaded)
                        {
                            // remove if exist
                            this.RemoveCataloguesForConnection(cataloguesPerUser.ConnectionId);
                        }


                        // add catalogues per user
                        globalCataloguesCollection.Add(cataloguesPerUser);


                        if (this.CatalogueEvent != null)
                        {
                            this.CatalogueEvent(this, new MyZillaSettingsEventArgs(sp));
                        }
                    }
                    else
                    {
                        if (this.CatalogueEvent != null)
                        {
                            TDSettings.ConnectionRow connectionRow = sp.ConnectionRow ;

                            SavingData spWhenNullCatalogues = new SavingData(OperationType.AddConnectionThrowsError, connectionRow);

                            this.CatalogueEvent(this, new MyZillaSettingsEventArgs(spWhenNullCatalogues));
                        }
                    }
                }
       }

        void bkgCatalogues_DoWork(object sender, DoWorkEventArgs e)
        {
            SavingData sp = null; 
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;
            ArrayList result = new ArrayList();

            try
            {
                ArrayList al = e.Argument as ArrayList;

                TDSettings.ConnectionRow  currentConnection  = al[0] as TDSettings.ConnectionRow ;

                sp = al[1] as SavingData;

                Catalogues cataloguesPerUser = CatalogueManager.GetCataloguesForUser(currentConnection, backgroundWorker);
                
                result.Add(cataloguesPerUser);

                result.Add(sp);
                
                e.Result = result;

            }
            catch (Exception ex)
            {
                sp.ErrorMessage = ex.Message;

                sp.Operation = OperationType.LogOnFailed;
            
                MyLogger.Write(ex, "bkgCatalogues_DoWork", LoggingCategory.Exception);

                SplashManager.Close(); // just in case

                backgroundWorker.ReportProgress(100);

                throw new CustomException(sp, String.Empty, ex);
            }

        }

        #endregion

        #region Private methods

        private void RemoveUsersForConnection(int connectionId)
        {
            for (int i = 0; i < globalCataloguesCollection.Count; i++)
            {
                if (globalCataloguesCollection[i].ConnectionId  == connectionId )
                {
                    globalCataloguesCollection.RemoveAt(i); 

                }
            }
        }

        private void LoadCataloguesForUser(SavingData data)
        {
            // check if the catalogues collection has loaded.
            bool isLoaded = false;

            for (int i = 0; i < globalCataloguesCollection.Count; i++)
            {
                if (globalCataloguesCollection[i].ConnectionId  == data.ConnectionRow.ConnectionId  )
                {
                    isLoaded = true;
                }
            }

            if (!isLoaded)
            {

                TDSettings.ConnectionRow  connection = _appSettings.GetConnectionById (data.ConnectionRow.ConnectionId );

                BackgroundWorker bwCatalogues = new BackgroundWorker();

                bwCatalogues.DoWork += new DoWorkEventHandler(bkgCatalogues_DoWork);

                bwCatalogues.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgCatalogues_RunWorkerCompleted);

                bwCatalogues.ProgressChanged += new ProgressChangedEventHandler(bkgCatalogues_ProgressChanged);

                bwCatalogues.WorkerReportsProgress = true;

                bwCatalogues.WorkerSupportsCancellation = true;

                ArrayList al = new ArrayList();

                al.Add(connection );

                al.Add(data);

                bwCatalogues.RunWorkerAsync(al);

            }


        }

        #endregion

        #region Process BSI event

        void _appSettings_SaveSettings(object sender, MyZillaSettingsEventArgs e)
        {
            switch (e.SaveParameter.Operation)
            {
                case OperationType.AddConnection :
                    if (e.SaveParameter.ConnectionRow.ActiveUser  == true)
                    {
                        // check if catalogues for user are already cached
                        Catalogues cataloguesPerUser = this.GetCataloguesForConnection (e.SaveParameter.ConnectionRow.ConnectionId );

                        if (cataloguesPerUser != null)
                        {
                            // catalogues are already cached.
                            // no action here.
                        }
                        else
                        {
                            if (this.CatalogueEvent != null)
                            {
                                this.CatalogueEvent(this, new MyZillaSettingsEventArgs(e.SaveParameter));
                            }

                            this.LoadCataloguesForUser(e.SaveParameter);
                        }
                    }
                    else
                    {
                        if (this.CatalogueEvent != null)
                        {
                            this.CatalogueEvent(this, new MyZillaSettingsEventArgs(e.SaveParameter));
                        }
                    }
                    break;

                case OperationType.EditConnection:
                    if (e.SaveParameter.ConnectionRow.ActiveUser == true)
                    {
                        // check if catalogues for user are already cached
                        Catalogues cataloguesPerUser = this.GetCataloguesForConnection (e.SaveParameter.ConnectionRow.ConnectionId );

                        if (cataloguesPerUser != null)
                        {
                            // catalogues are already cached.
                            // fire the event to be processed by the subscribers if any
                            if (this.CatalogueEvent != null)
                            {
                                this.CatalogueEvent(this, new MyZillaSettingsEventArgs(e.SaveParameter));
                            }
                        }
                        else
                        {
                            if (this.CatalogueEvent != null)
                            {
                                this.CatalogueEvent(this, new MyZillaSettingsEventArgs(e.SaveParameter));
                            }

                            this.LoadCataloguesForUser(e.SaveParameter);

                        }
                    }
                    else
                    {

                        //this.RemoveCataloguesForConnection (e.SaveParameter.ConnectionRow.ConnectionId );

                        if (this.CatalogueEvent != null)
                        {
                            this.CatalogueEvent(this, new MyZillaSettingsEventArgs(e.SaveParameter));
                        }

                    }
                    break;

                case OperationType.DeleteConnection:

                    this.RemoveUsersForConnection(e.SaveParameter.ConnectionRow.ConnectionId );

                    if (this.CatalogueEvent != null)
                    {
                        this.CatalogueEvent(this, new MyZillaSettingsEventArgs(e.SaveParameter));
                    }


                    break;

            }
        }

        #endregion

        #region Get All catalogues - Sync

        private static Catalogues GetCataloguesForUser(TDSettings.ConnectionRow currentConnection, BackgroundWorker bkgWork)
        {
            MyZilla.BusinessEntities.Catalogues cataloguesPerUser = null;

            string connInfo = _appSettings.GetConnectionInfo(currentConnection.ConnectionId);

            try
            {
#if DEBUG
                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
#endif


                bkgWork.ReportProgress(0, connInfo);
                bkgWork.ReportProgress(10, connInfo);


                // when refresh catalogues, it is no need to verify login.

                IUser user = (IUser)BLControllerFactory.GetRegisteredConcreteFactory(currentConnection.ConnectionId);

                // TO DO: eliminate the last param. 
                string loggedUser = user.LogOnToBugzilla(currentConnection.UserName, currentConnection.Password);


                if (loggedUser.Length > 0)
                {
                    // this could happen if a wrong password was saved.

                    bkgWork.ReportProgress(100, connInfo);

                    throw new Exception(loggedUser);
                }


                IUtilities catalogue = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(currentConnection.ConnectionId);

                string[] catalogNames = new string[] { "classification", "product", "bug_status", "resolution", "bug_severity", "priority", "rep_platform", "op_sys", "short_desc_type", "field0-0-0", "type0-0-0" };

                // get all the main catalogues (catalogues without dependencies) used in the application
                // request

                ArrayList collCatalogues = catalogue.GetCatalogues(catalogNames);

                // get the connection/user information

                cataloguesPerUser = new MyZilla.BusinessEntities.Catalogues(currentConnection.ConnectionId);

                #region Main Catalogues

                // get the product catalogue
                List<string> lstProduct = collCatalogues[1] as List<string>;

                cataloguesPerUser.catalogueProduct = new NameValueCollection();

                foreach (string strProduct in lstProduct)
                {
                    cataloguesPerUser.catalogueProduct.Add(strProduct, string.Empty);
                }


                // get the rest of the main catalogues and populate the corresponding controls.
                // string[] catalogNames = new string[] {"product", 
                // "bug_status", 
                // "resolution", 
                // "bug_severity", 
                // "priority", 
                // "rep_platform", 
                // "op_sys",
                // "short_desc_type"};

                cataloguesPerUser.catalogueStatus = collCatalogues[2] as List<string>;

                cataloguesPerUser.catalogueResolution = collCatalogues[3] as List<string>;

                cataloguesPerUser.catalogueSeverity = collCatalogues[4] as List<string>;

                cataloguesPerUser.cataloguePriority = collCatalogues[5] as List<string>;

                cataloguesPerUser.catalogueHardware = collCatalogues[6] as List<string>;

                cataloguesPerUser.catalogueOS = collCatalogues[7] as List<string>;

                cataloguesPerUser.catalogueStringOperators = collCatalogues[8] as List<string>;

                cataloguesPerUser.catalogueFields = collCatalogues[9] as List<string>;

                cataloguesPerUser.catalogueOperators = collCatalogues[10] as List<string>;

                #endregion

                bkgWork.ReportProgress(40, connInfo);

                #region Dependent catalogues


                // component and version catalogues

                // request
                ArrayList al = catalogue.GetValuesForDependentCatalogues(0, cataloguesPerUser.catalogueProduct);

                cataloguesPerUser.catalogueComponent = al[0] as NameValueCollection;

                cataloguesPerUser.catalogueVersion = al[1] as NameValueCollection;

                cataloguesPerUser.catalogueTargetMilestone = al[2] as NameValueCollection;

                #endregion

                bkgWork.ReportProgress(80, connInfo);

#if DEBUG
                watch.Stop();

                MyLogger.Write(watch.ElapsedMilliseconds.ToString(), "GetCataloguesForUser", LoggingCategory.Debug);
#endif

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "GetCataloguesForUser", LoggingCategory.Exception);

                throw;

            }
            finally
            {
                bkgWork.ReportProgress(100, connInfo);

           }
           return cataloguesPerUser;


        }

        #endregion

        #region Load dep catalogues (1)

        public void LoadCompAndVersionCatalogues(Catalogues cataloguesPerUser)
        {
            // start a new thread for Component and Version catalogues

            BackgroundWorker bkgDepCatalogues1 = new BackgroundWorker();
            bkgDepCatalogues1.DoWork += new DoWorkEventHandler(bkgDepCatalogues1_DoWork);
            bkgDepCatalogues1.ProgressChanged += new ProgressChangedEventHandler(bkgDepCatalogues1_ProgressChanged);
            bkgDepCatalogues1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgDepCatalogues1_RunWorkerCompleted);
            bkgDepCatalogues1.WorkerReportsProgress = true;
            bkgDepCatalogues1.WorkerSupportsCancellation = true;
            bkgDepCatalogues1.RunWorkerAsync(cataloguesPerUser);
        }

        void bkgDepCatalogues1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            // check the status of the async operation.
            if (e.Error != null)
            {
                string errorMessage = Messages.ErrLoadingCatalogues + Environment.NewLine + e.Error.Message;

                MessageBox.Show(Utils.FormContainer, errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
             {
                if (this.CompAndVersionCataloguesLoadedCompleted != null)
                {
                    this.CompAndVersionCataloguesLoadedCompleted(null, null);
                }
            }

            Interlocked.Decrement(ref activeThreads);

        }

        void bkgDepCatalogues1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;


            switch (e.ProgressPercentage)
            {
                case 0:


                    if (_asyncOpManager != null)
                    {
                        _asyncOpManager.BeginOperation(bkgWork, Messages.LoadDependentCatalogues , e.ProgressPercentage);
                    }

                    break;


                case 100:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.UpdateStatus(bkgWork, Messages.LoadDependentCatalogues, e.ProgressPercentage);
                    }

                    break;

                default:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.UpdateStatus(bkgWork, Messages.LoadDependentCatalogues, e.ProgressPercentage);
                    }

                    break;

            }
        }

        void bkgDepCatalogues1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            try
            {

#if DEBUG
                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
#endif

                Interlocked.Increment(ref activeThreads);


                bkgWork.ReportProgress(0);
                bkgWork.ReportProgress(10);


                // component and version catalogues
                Catalogues cataloguesPerUser = e.Argument as Catalogues;

                IUtilities catalogue = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(cataloguesPerUser.ConnectionId);

                bkgWork.ReportProgress(60);


                // request
                ArrayList al = catalogue.GetValuesForDependentCatalogues(0, cataloguesPerUser.catalogueProduct);

                cataloguesPerUser.catalogueComponent = al[0] as NameValueCollection;

                cataloguesPerUser.catalogueVersion = al[1] as NameValueCollection;

                cataloguesPerUser.catalogueTargetMilestone = al[2] as NameValueCollection; 

                bkgWork.ReportProgress(100);

                e.Result = cataloguesPerUser;



#if DEBUG
                watch.Stop();
                MyLogger.Write(watch.ElapsedMilliseconds.ToString(), "bkgDepCatalogues1_DoWork", LoggingCategory.Debug);
#endif
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bkgDepCatalogues1_DoWork", LoggingCategory.Exception);

                bkgWork.ReportProgress(100);

                throw;

            }

        }

        #endregion

        #region Load dep catalogues AssignTo and CC

        public void LoadAssignAndCCCollections(int userId, string product)
        {

            // start a new thread for CC and AssignTo for every product
            BackgroundWorker bkgDepCatalogues2 = new BackgroundWorker();
            bkgDepCatalogues2.DoWork += new DoWorkEventHandler(bkgDepCatalogues2_DoWork);
            bkgDepCatalogues2.ProgressChanged += new ProgressChangedEventHandler(bkgDepCatalogues2_ProgressChanged);
            bkgDepCatalogues2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgDepCatalogues2_RunWorkerCompleted);
            bkgDepCatalogues2.WorkerReportsProgress = true;
            bkgDepCatalogues2.WorkerSupportsCancellation = true;
            ArrayList al = new ArrayList();
            al.Add( userId );
            al.Add( product );
            bkgDepCatalogues2.RunWorkerAsync(al);

        }

        void bkgDepCatalogues2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {

                string errorMessage = Messages.ErrLoadingCatalogues + Environment.NewLine + e.Error.Message;

                MessageBox.Show(Utils.FormContainer, errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                
                if (DependentCataloguesLoadedCompleted != null)
                {
                    DependentCataloguesLoadedCompleted(this, null);
                }
            }

            Interlocked.Decrement(ref activeThreads);

        }

        void bkgDepCatalogues2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;


            switch (e.ProgressPercentage)
            {
                case 0:


                    if (_asyncOpManager != null)
                    {
                        _asyncOpManager.BeginOperation(bkgWork, Messages.LoadDependentCatalogues, e.ProgressPercentage);
                    }

                    break;


                case 100:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.UpdateStatus(bkgWork, Messages.LoadDependentCatalogues, e.ProgressPercentage);
                    }

                    break;

                default:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.UpdateStatus(bkgWork, Messages.LoadDependentCatalogues, e.ProgressPercentage);
                    }

                    break;

            }
        }

        void bkgDepCatalogues2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            try
            {
                bkgWork.ReportProgress(0);
                bkgWork.ReportProgress(10);

                Interlocked.Increment(ref activeThreads);

                ArrayList argument = e.Argument as ArrayList;

                //get connection id from the event arguments
                int connectionID = int.Parse(argument[0].ToString());

                //get product name from the event arguments
                string product = argument[1].ToString();

                Catalogues cataloguesPerUser = this.GetCataloguesForConnection(connectionID);

                IUtilities catalogue = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(connectionID);

                bkgWork.ReportProgress(60);

                // al[0] = AssignTo; al[1] = CC
                // request
                ArrayList alSpecificCatalogues = catalogue.GetSpecificCataloguesWhenManageBug(product, cataloguesPerUser.catalogueComponent);

                NameValueCollection assignToColl = alSpecificCatalogues[0] as NameValueCollection;

                cataloguesPerUser.catalogueAssignedTo = assignToColl;

                NameValueCollection ccColl = alSpecificCatalogues[1] as NameValueCollection;

                cataloguesPerUser.catalogueCC = ccColl;

                NameValueCollection ccPriority = alSpecificCatalogues[2] as NameValueCollection;

                cataloguesPerUser.catalogueDefaultPriority = ccPriority;

                bkgWork.ReportProgress(100);

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bkgDepCatalogues2_DoWork", LoggingCategory.Exception);

                bkgWork.ReportProgress(100);

                throw;
            }

        }

        #endregion

        #region Refresh catalogues - async

        /// <summary>
        /// Load asynchronouslly the catalogues for the valid users.
        /// </summary>
        public void RefreshCatalogues()
        {

            BackgroundWorker refreshCatalogues = new BackgroundWorker();

            refreshCatalogues.DoWork += new DoWorkEventHandler(refreshCatalogues_DoWork);

            refreshCatalogues.ProgressChanged += new ProgressChangedEventHandler(refreshCatalogues_ProgressChanged);

            refreshCatalogues.RunWorkerCompleted += new RunWorkerCompletedEventHandler(refreshCatalogues_RunWorkerCompleted);

            refreshCatalogues.WorkerReportsProgress = true;

            refreshCatalogues.WorkerSupportsCancellation = true;

            refreshCatalogues.RunWorkerAsync();

            //validUsers = _appSettings.GetValidUsers();

            //if (validUsers.Rows.Count > 0)
            //{

            //    for (int iThread = 0; iThread < validUsers.Rows.Count; iThread++)
            //    {
            //        BackgroundWorker bkgCatalogues = new BackgroundWorker();

            //        bkgCatalogues.DoWork += new DoWorkEventHandler(bkgCatalogues_DoWork);

            //        bkgCatalogues.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgCatalogues_RunWorkerCompleted);

            //        bkgCatalogues.ProgressChanged += new ProgressChangedEventHandler(bkgCatalogues_ProgressChanged);

            //        bkgCatalogues.WorkerReportsProgress = true;

            //        bkgCatalogues.WorkerSupportsCancellation = true;

            //        TDSettings.UserRow user = validUsers[iThread];

            //        ArrayList al = new ArrayList();

            //        al.Add(user);

            //        al.Add(null);

            //        bkgCatalogues.RunWorkerAsync(al);

            //    }

            //}

        }

        void refreshCatalogues_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
                // check the status of the async operation.
                if (e.Error != null)
                {
                    string errorMessage = Messages.ErrLoadingCatalogues + Environment.NewLine + e.Error.Message ;

                    MessageBox.Show( Utils.FormContainer, errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);      
                }

                // status OK
                if (!e.Cancelled && e.Error == null)
                {
                    // no code here
                }
        }

        void refreshCatalogues_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            string connectionInfo = string.Empty;

            if (e.UserState != null)
            {
                connectionInfo = e.UserState.ToString();
            }

            string strMessage = string.Format(Messages.LoadCataloguesInProgress, connectionInfo); 


            switch (e.ProgressPercentage)
            {
                case 0:


                    if (_asyncOpManager != null)
                    {
                        _asyncOpManager.BeginOperation(bkgWork, strMessage , e.ProgressPercentage);
                    }

                    break;


                case 100:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.UpdateStatus(bkgWork, strMessage , e.ProgressPercentage);
                    }

                    break;

                default:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.UpdateStatus(bkgWork, strMessage , e.ProgressPercentage);
                    }

                    break;

            }

        }

        void refreshCatalogues_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            try
            {

                bkgWork.ReportProgress(0);
 
                List<Catalogues> activeConnections = new List<Catalogues>();

                for (int i = 0; i < globalCataloguesCollection.Count; i++)
                {
                    activeConnections.Add(globalCataloguesCollection[i]); 
                }

                globalCataloguesCollection.Clear();

                for (int i = 0; i < activeConnections.Count; i++)
                {

                    TDSettings.ConnectionRow connRow = _appSettings.GetConnectionById(activeConnections [i].ConnectionId );

                    Catalogues newCatalogue = null;
                    try
                    {
                        newCatalogue = CatalogueManager.GetCataloguesForUser(connRow, bkgWork);
                    }
                    catch (Exception)
                    {
                        // login failed or another exception
                        // old collection are kept.

                    }

                    // add catalogues per user
                    if (newCatalogue != null)
                    {
                        globalCataloguesCollection.Add(newCatalogue);
                    }
                    else
                    {
                        // keep the old collections
                        globalCataloguesCollection.Add(activeConnections[i]); 
                    }

                    string connectionInfo = _appSettings.GetConnectionInfo(connRow.ConnectionId);   

                    bkgWork.ReportProgress(1 + i * 90 / activeConnections.Count, connectionInfo  );

                }

                bkgWork.ReportProgress(100 ); 

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "refreshCatalogues_DoWork", LoggingCategory.Exception);

                bkgWork.ReportProgress(100);

                throw;
            }

        }

        #endregion

    }
}
