using System;
using System.Collections.Generic;
using System.Text;

namespace MyZilla.BusinessEntities
{
    public class ApplicationVersion
    {
        private int primaryVersion;

        private int secondaryVersion;

        private int buildNr;

        private string completeForm;

        public int BuildNr
        {
            get { return this.buildNr; }
            set { this.buildNr = value; }
        }
        
        public int SecondaryVersion
        {
            get { return this.secondaryVersion; }
            set { this.secondaryVersion = value; }
        }

	    public int PrimaryVersion
	    {
		    get { return this.primaryVersion;}
		    set { this.primaryVersion = value;}
	    }

        public ApplicationVersion(string version) {

            string[] versionParts = version.Split('.');

            if (versionParts != null && versionParts.GetLength(0) >= 2)
            {
                bool result = Int32.TryParse(versionParts[0], out this.primaryVersion);

                if (result)
                {
                    result = Int32.TryParse(versionParts[1], out this.secondaryVersion);

                    if (result)
                    {

                        if (versionParts.GetLength(0) >= 3)
                            result = Int32.TryParse(versionParts[2], out this.buildNr);
                        else
                            this.buildNr = 0;
                    }
                }
            }
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(completeForm))
            {
                if (this.buildNr==0)
                    completeForm = String.Format("{0}.{1}", this.primaryVersion, this.secondaryVersion);
                else
                    completeForm = String.Format("{0}.{1}.{2}", this.primaryVersion, this.secondaryVersion, this.buildNr);
            }

            return completeForm;
        }

        public static bool operator <  (ApplicationVersion version1, ApplicationVersion version2){
            return ApplicationVersion.Comparison(version1, version2) < 0;
        }

        public static bool operator <=(ApplicationVersion version1, ApplicationVersion version2)
        {
            return ApplicationVersion.Comparison(version1, version2) <= 0;
        }

        public static bool operator >=(ApplicationVersion version1, ApplicationVersion version2)
        {
            return ApplicationVersion.Comparison(version1, version2) >= 0;
        }

        public static bool operator >(ApplicationVersion version1, ApplicationVersion version2)
        {
            return ApplicationVersion.Comparison(version1, version2) > 0;
        }

        public static bool operator ==(ApplicationVersion version1, ApplicationVersion version2)
        {
            return ApplicationVersion.Comparison(version1, version2) == 0;
        }

        public static bool operator !=(ApplicationVersion version1, ApplicationVersion version2)
        {
            return ApplicationVersion.Comparison(version1, version2) != 0;
        }

        public override bool Equals(object obj)
        {

            if (!(obj is ApplicationVersion)) return false;

            return this == (ApplicationVersion)obj;

        }

        public static int Comparison(ApplicationVersion version1, ApplicationVersion version2)
        {

            if (version1.PrimaryVersion < version2.PrimaryVersion 
                || (version1.PrimaryVersion == version2.PrimaryVersion && version1.SecondaryVersion < version2.SecondaryVersion)
                || (version1.PrimaryVersion == version2.PrimaryVersion && version1.SecondaryVersion == version2.SecondaryVersion && version1.BuildNr < version2.BuildNr))

                return -1;

            else if (version1.PrimaryVersion == version2.PrimaryVersion && version1.SecondaryVersion == version2.SecondaryVersion && version1.BuildNr == version2.BuildNr)

                return 0;

            else if (version1.PrimaryVersion > version2.PrimaryVersion
           || (version1.PrimaryVersion == version2.PrimaryVersion && version1.SecondaryVersion > version2.SecondaryVersion)
           || (version1.PrimaryVersion == version2.PrimaryVersion && version1.SecondaryVersion == version2.SecondaryVersion && version1.BuildNr > version2.BuildNr))

                return 1;

            return 0;

        }
    }
}
