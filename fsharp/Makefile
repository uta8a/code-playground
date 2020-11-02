arg = "default"
target = "Program.fs"
new:
	@dotnet-sdk.dotnet new console -lang="F#" -o ./${arg}
	@cp .gitignore ./${arg}/.gitignore
run:
	@dotnet-sdk.dotnet run --project ./${arg}
fmt:
	@fantomas ./${target}