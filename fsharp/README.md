# F#
- install
```
yay -S fsharp
yay -S dotnet-sdk
```
- `dotnet new console -lang="F#" -o helloworld`
- `dotnet run`
- `dotnet add package <PackageName>`
- `dotnet tool install -g fantomas-tool` # code formatter

## Ubuntu
- https://docs.microsoft.com/en-us/dotnet/core/install/linux-debian#manual-install
```
wget https://packages.microsoft.com/config/debian/10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-3.1
```
# formatter
- `fantomas --indent 2 *.fs`

# Finite General Linear Group
```
*init
*set_random
*set_at
*get_at
*determinant
*transpose
*__add__
*__sub__
*__mul__
*__pow__
*__eq__
*__str__

*__neg__
```
# Reference
- https://gitlab.com/zer0pts/zer0pts-ctf-2020/-/blob/master/nibelung/distfiles/fglg.py
  - zer0pts CTF nibelung fglg.py
