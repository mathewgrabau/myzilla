Requirements
============
Microsoft Visual Studio 2008
Microsoft .NET Framework 2.0

Signature
=========
For signing the application, put your certificate and keys in the following location:

\src\BSI.BL.BugzillaHttp\PublicPrivateKeyFile.snk
\src\BSI.BL.Interfaces\PublicPrivateKeyFile.snk
\src\BSI.BL.Utils\PublicPrivateKeyFile.snk
\src\BSI.BusinessEntities\PublicPrivateKeyFile.snk
\src\BSI.UI\PublicPrivateKeyFile.snk
\src\Libraries\PublicPrivateKeyFile.snk
\src\Tremend.Logging\PublicPrivateKeyFile.snk

Required DLLs (not available in source form)
============================================
The following DLLs are not included by default for .NET Framework projects. They have
to be added separately and placed in the following locations:

From Microsoft:
\src\Libraries\Interop.IWshRuntimeLibrary.dll
\src\Libraries\Microsoft.Practices.EnterpriseLibrary.Common.dll
\src\Libraries\Microsoft.Practices.EnterpriseLibrary.Logging.dll
\src\Libraries\Microsoft.Practices.ObjectBuilder.dll

From XML-RPC.NET (http://www.xml-rpc.net/):
\src\Libraries\CookComputing.XmlRpcV2.dll
