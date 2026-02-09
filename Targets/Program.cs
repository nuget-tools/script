using static Bullseye.Targets;
using static SimpleExec.Command;

Target("build", () => RunAsync("dotnet", "build --configuration Release"));
Target("test", dependsOn: ["build"], () => RunAsync("dotnet", "test --configuration Release --no-build"));
Target("default", dependsOn: ["test"]);
