var TARGET = Argument ("target", Argument ("t", "Default"));
var VERSION = EnvironmentVariable ("APPVEYOR_BUILD_VERSION") ?? Argument("version", "0.0.9999");
var CONFIG = Argument("configuration", EnvironmentVariable ("CONFIGURATION") ?? "Release");
var SLN = "./src/Xamarin.BlurView.sln";

Task("Libraries").Does(()=>
{
	NuGetRestore (SLN);
	MSBuild (SLN, c => {
		c.Configuration = CONFIG;
		c.MSBuildPlatform = Cake.Common.Tools.MSBuild.MSBuildPlatform.x86;
	});
});

Task ("NuGet")
	.IsDependentOn ("Libraries")
	.Does (() =>
{
    if(!DirectoryExists("./dist"))
        CreateDirectory("./dist");
        
	NuGetPack ("./nuget/Xamarin.BlurView.nuspec", new NuGetPackSettings { 
		Version = VERSION,
		OutputDirectory = "./dist/",
		BasePath = "./"
	});	
});

//Build the component, which build samples, nugets, and libraries
Task ("Default").IsDependentOn("NuGet");

Task ("Clean").Does (() => 
{
	CleanDirectories ("./dist/");
	CleanDirectories ("./**/bin");
	CleanDirectories ("./**/obj");
});

RunTarget (TARGET);