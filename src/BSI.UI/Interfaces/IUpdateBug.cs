using System;
using System.Collections.Generic;
using System.Text;

namespace MyZilla.UI.Interfaces
{
    public interface IUpdateBug
    {
        #region Properties

        //BugStatus Status;

        bool IsFormClosed
        {
            get;
            set;
        }

        string BugResolution
        {
            get;
            set;
        }

        string BugKnob
        {
            get;
            set;
        }

        string DuplicateBug
        {
            get;
            set;
        }

        string ReassignTo
        {
            get;
            set;
        }

        int BugNumber
        {
            set;
        }

        int ConnectionId
        {
            set;
        }

        MyZilla.BusinessEntities.Bug BugToUpdate
        {
            get;
            set;
        }

        bool UpdateSuccessfully
        {
            get;
        }

        #endregion

        #region Methods

        void Submit();

        MyZilla.BusinessEntities.Bug GetUpdatedBug();

        #endregion
    }
}
