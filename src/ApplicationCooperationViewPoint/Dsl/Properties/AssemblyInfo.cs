#region Using directives

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

#endregion

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle(@"")]
[assembly: AssemblyDescription(@"")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(@"Black.Beard")]
[assembly: AssemblyProduct(@"Galileo Application Cooperation Viewpoint")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: System.Resources.NeutralResourcesLanguage("en")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion(@"1.0.0.0")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: ReliabilityContract(Consistency.MayCorruptProcess, Cer.None)]

//
// Make the Dsl project internally visible to the DslPackage assembly
//
[assembly: InternalsVisibleTo(@"Bb.ApplicationCooperationViewPoint.DslPackage, PublicKey=0024000004800000940000000602000000240000525341310004000001000100D5F101F9D7867AF84E9ACABDB5AB34D28D1B81C8D65AD5A77FC62E174B1DA3644FE4562F6FC4FAAA1811239A6AA40F592D6D670295C319AB2A4C9ABE246A47A1BA6E2648C647A7E29DC11A6ED1F160BDEB866F7D08020AE316CD1B9770FD8D28CC4187420399CF08D2A7CF6489C712CA95E808D8121438B36AC0864E809EBBB1")]