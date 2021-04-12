using System;

namespace Bb.ApplicationCooperationViewPoint
{
    public static class GuidsList
    {


        public static readonly Guid guidClientPkg = new Guid("{7F7FC010-0819-4C00-B58C-1EE3DA37CEE5}");

        public const string AutoMapperShowDetails = "{2EEC434F-E727-43C1-B1B5-561D2D5ACEE4}";

        //public static readonly Guid guidClientCmdSet = new Guid(AutoMapperShowDetails);

        public static Guid GuidAutoMapperShowDetails = new Guid(AutoMapperShowDetails);

        // Menu Show details
        //public static readonly global::System.ComponentModel.Design.CommandID ShowDetails
        //    = new global::System.ComponentModel.Design.CommandID(guidAutoMapperShowDetails, cmdidMyContextMenuCommand);


        public const int cmdidPersistedWindow = 0x2001;
        public const int grpidMyMenuGroup = 0x01001;
        public const int cmdidMyContextMenuCommand = 1;
        public const int IDM_MyToolbar = 0x0101;


        public const string CooperationCommandSetId = "0a71386e-4bcb-4ac5-a7a0-484a1a3abefa";

        public static readonly global::System.ComponentModel.Design.CommandID AutoMapperDetailsMenu = new global::System.ComponentModel.Design.CommandID(new global::System.Guid(CooperationCommandSetId), 0x10001);


    }


}
