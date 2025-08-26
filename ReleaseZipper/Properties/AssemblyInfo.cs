using System.Reflection;

[assembly: AssemblyTitle("ReleaseZipper")]
[assembly: AssemblyDescription("Compactador de arquivos que gera release.zip na Área de Trabalho")]
[assembly: AssemblyCompany("WhiteZ")]
[assembly: AssemblyProduct("ReleaseZipper")]
[assembly: AssemblyCopyright("Copyright © CaiqueZ 2025")]

// Versões básicas fixas
[assembly: AssemblyVersion("1.0.0")]
[assembly: AssemblyFileVersion("1.0.0")]

// Controla se é DEV ou RELEASE
#if DEBUG
[assembly: AssemblyInformationalVersion("dev")]
#else
[assembly: AssemblyInformationalVersion("1.0.0")] // <- será sobrescrito no GitHub Actions
#endif