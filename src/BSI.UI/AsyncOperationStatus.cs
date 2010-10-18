using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms ; 

namespace MyZilla.UI
{
    /// <summary>
    /// Represent the status of the asynchronous operation.
    /// </summary>
    public class AsyncOperationStatus
    {
        #region Variable

        private BackgroundWorker _backgroudWorker;

        private string _message;

        private int _percentage;

        #endregion

        public AsyncOperationStatus(BackgroundWorker backgroudWorker, string message, int percentage)
        {
            _backgroudWorker = backgroudWorker;

            _message = message;

            _percentage = percentage;
        }

        public bool IsCompleted ( )
        {
            bool isCompleted = _percentage == 100 ? true : false;

            return isCompleted;
        }

        public BackgroundWorker Instance
        {
            get
            {
                return _backgroudWorker;
            }

        }

        #region Properties

        public string  Message
        {
            set
            {
                _message = value;
            }
            get
            {
                return _message;
            }

        }

        public int Percentage
        {
            set
            {
                _percentage = value;
            }
            get
            {
                return _percentage;
            }

        }

        #endregion
    }

    /// <summary>
    /// Manage the status of all the asynchronous operations in the application.
    /// </summary>
    public class AsyncOperationManagerList : List<AsyncOperationStatus>
    {
        #region Variables

        private static AsyncOperationManagerList  _instance;

        #endregion

        #region Events

        public event EventHandler RefreshAsyncOperationListEvent;

        #endregion

        #region Constructor / Instance

        private AsyncOperationManagerList()
        {
            //stProgressBar = pb;

            //lblStatus = lbl;

            //stProgressBar.Visible = this.Count > 0;

            //lblStatus.Visible = this.Count > 0;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static AsyncOperationManagerList GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AsyncOperationManagerList();
            }

            return _instance;
        }

        #endregion

        /// <summary>
        /// Add an object that represent new asynchronous operation.
        /// </summary>
        /// <param name="bkg">The reference to the background worker that started the new thread.</param>
        /// <param name="message"></param>
        /// <param name="percentage"></param>
        public void BeginOperation(BackgroundWorker backgroudWorker, string message, int percentage)
        {
            AsyncOperationStatus operation = new AsyncOperationStatus(backgroudWorker, message, percentage);

            this.Add(operation);

            if ( RefreshAsyncOperationListEvent != null )
            {
                AsyncOperationStatus opStatus = new AsyncOperationStatus ( null, message , percentage );

                RefreshAsyncOperationListEvent(this, new AsyncOperationEventArgs(opStatus));
            }

        }

        /// <summary>
        /// Update the thread status.
        /// </summary>
        /// <param name="bkg">The reference to the background worker for the current thread.</param>
        /// <param name="message"></param>
        /// <param name="percentage"></param>
        public void UpdateStatus(BackgroundWorker backgroudWorker, string message, int percentage)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Instance == backgroudWorker)
                {
                    // Eliminate the operations that are completed. (percentage = 100)
                    if (percentage == 100)
                    {
                        this.Remove(this[i]);
                    }
                    else
                    {
                        this[i].Message = message;

                        this[i].Percentage = percentage;

                    }
                }
            }

            if (RefreshAsyncOperationListEvent != null)
            {

                AsyncOperationStatus opStatus = new AsyncOperationStatus ( null, message , percentage );
               
                RefreshAsyncOperationListEvent(this, new AsyncOperationEventArgs ( opStatus ));
            }

        }

        public AsyncOperationStatus GetLastAsyncOperation()
        {
            // show the last thread in the List.
            AsyncOperationStatus result = null;

            if (this.Count > 0)
            {
                result = this[this.Count - 1];
            }

            return result;
        }

    }

    public class AsyncOperationEventArgs : EventArgs
    {
        private AsyncOperationStatus _asyncOpStatus ;

        public AsyncOperationEventArgs(AsyncOperationStatus opStatus)
        {
            _asyncOpStatus = opStatus;
        }

        public AsyncOperationStatus Status
        {
            get
            {
                return _asyncOpStatus;
            }
        }
    }
}
