using static Bullseye.Targets;
using static SimpleExec.Command;

Target("build", () => RunAsync("dotnet", "build --configuration Release"));
Target("test", dependsOn: ["build"], () => RunAsync("dotnet", "test --configuration Release --no-build"));
Target("dummy", () => RunAsync("bash", "-c 'dummy.sh'"));
Target("default", dependsOn: ["test"]);

await RunTargetsAndExitAsync(args, ex => ex is SimpleExec.ExitCodeException);
