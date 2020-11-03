arg = "default" # Project Directory Name
target = "Program.fs" # path to file for fmt
new:
	@dotnet new console -lang="F#" -o ./${arg}
	@cp .gitignore ./${arg}/.gitignore
	@cd ./${arg} && dotnet new nunit -lang="F#" --force
run:
	@cd ./${arg} && dotnet run
test:
	@cd ./${arg} && dotnet test
fmt:
	@fantomas ./${target}