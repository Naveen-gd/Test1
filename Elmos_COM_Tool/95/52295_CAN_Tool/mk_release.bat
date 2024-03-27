del *.zip

@SET prjDir=%~dp0
@echo prjDir=%prjDir%

@SET curDir=52295_EEPROM_Tool\bin\Release_E52295A
@echo curDir=%curDir%
@SET curName=52295_EEPROM_Tool_E52295A.zip
@echo curName=%curName%
cd %curDir%
"C:\Program Files\7-Zip\7z" a -y -tzip %curName% *.exe *.dll *.pdf -mx5
cd %prjDir%
move %curDir%\%curName% .

@SET curDir=52295_CAN_Tool\bin\Release_E52295A
@echo curDir=%curDir%
@SET curName=52295_CAN_Tool_E52295A.zip
@echo curName=%curName%
cd %curDir%
"C:\Program Files\7-Zip\7z" a -y -tzip %curName% *.exe *.dll *.pdf -mx5
cd %prjDir%
move %curDir%\%curName% .
"C:\Program Files\7-Zip\7z" a -y -tzip %curName% -mx5

pause
