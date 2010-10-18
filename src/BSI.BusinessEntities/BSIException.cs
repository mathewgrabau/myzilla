using System;
using System.Collections.Generic;
using System.Text;

namespace MyZilla.BusinessEntities
{
    public class CustomException : Exception
    {
        private MyZilla.BusinessEntities.SavingData _customData;

        public MyZilla.BusinessEntities.SavingData CustomData
        {
            get { return _customData; }
        }

        #region Constructors

        public CustomException() { }

        public CustomException(MyZilla.BusinessEntities.SavingData data) : base()
        {
            _customData = data;
        }

        public CustomException(MyZilla.BusinessEntities.SavingData data, string message): base(message)
        {
           _customData = data;
        }

        public CustomException(MyZilla.BusinessEntities.SavingData data, string message,
              Exception innerException): base(message, innerException)
           {
               _customData = data;
           }

        #endregion
       }
}
