using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized ;

namespace MyZilla.BusinessEntities
{

    public class Catalogues
    {

        private int _connectionId = 0;

        public List<string> catalogueClassification;

        // <product, clasification>
        public  NameValueCollection catalogueProduct;

        // <  product, component>
        public  NameValueCollection catalogueComponent;

        // <  product, default priority>
        public NameValueCollection catalogueDefaultPriority;

        // <product, version>
        public NameValueCollection catalogueVersion;

        // <  product, target>
        public NameValueCollection catalogueTargetMilestone;

        public List<string> catalogueStatus;
        public List<string> catalogueResolution;
        public List<string> catalogueSeverity;
        public List<string> cataloguePriority;
        public List<string> catalogueHardware;
        public List<string> catalogueOS;

        public List<string> catalogueStringOperators;
        public List<string> catalogueFields;
        public List<string> catalogueOperators;

        // < product, CC >
        public NameValueCollection catalogueCC;

        //Remark 1.
        //TO DO: AssignTo catalogue do not depend of the components/products. 
        //The default value ( default asignee ) depends of the component.

        // < component, assignTo >
        public NameValueCollection catalogueAssignedTo;

        public Catalogues(int connectionId)
        {
            _connectionId = connectionId;
        }


        public int ConnectionId
        {
            get { return _connectionId; }
        }


    }



}
