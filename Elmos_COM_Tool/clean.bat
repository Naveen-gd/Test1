echo ------------------------------------------------------------

set build="%1bin\%2"
echo PREBUILD: clean %build%
del %build% /s /f /q
for /D %%a in ("%build%\*") do rd /q /s "%%a"

set build="%1obj\%2"
echo PREBUILD: clean %build%
del %build% /s /f /q
for /D %%a in ("%build%\*") do rd /q /s "%%a"

echo ------------------------------------------------------------
