using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace MyZilla.BL.Utils
{


    public class MyPolicy: ICertificatePolicy
    {

        public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate cert, WebRequest request, int certificateProblem)
        {
        //Return True to force the certificate to be accepted.
            return true;
        }

    }
}
