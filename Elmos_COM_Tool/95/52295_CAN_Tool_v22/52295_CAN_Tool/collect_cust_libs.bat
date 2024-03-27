mkdir cust_libs
del cust_libs\*.* /s /f /q
copy dll\*.* cust_libs
copy PCANbasic_NET\bin\Release\PCANbasic_NET.dll cust_libs
copy Can_Comm_Lib\bin\Release\Can_Comm_lib.dll cust_libs
copy Device_52295_Lib\bin\Release\Device_52295_Lib.dll cust_libs
pause