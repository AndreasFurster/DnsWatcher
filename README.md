# DnsWatcher
Simple application that watches dns hostnames and notifies if anything changes. 

## Usage
Download exe from releases or build the solution yourself (executable found in bin folder).

`DnsWatcher.exe example.com [example2.com ...]`

## Output

    .\DnsWatcher.exe google.com bing.com
    
    12/11/2018 5:23:23 PM | google.com --> 172.217.17.110
    12/11/2018 5:23:23 PM | bing.com   --> 13.107.21.200
    12/11/2018 5:23:26 PM | google.com --> 172.217.17.110
    12/11/2018 5:23:26 PM | bing.com   --> 13.107.21.200
    12/11/2018 5:30:22 PM | bing.com   --> 204.79.197.200
    12/11/2018 5:32:09 PM | google.com --> 172.217.17.46
    12/11/2018 5:32:09 PM | bing.com   --> 204.79.197.200
    12/11/2018 5:32:12 PM | google.com --> 172.217.17.46
    12/11/2018 5:32:12 PM | bing.com   --> 204.79.197.200


![Notification](https://user-images.githubusercontent.com/7996369/49814741-956b5480-fd6a-11e8-848d-9ec124093dc7.png)

## OS limitiation 
Because an MessageBox is used this project can only run on Windows... Should be easy to change though.

## Build instructions
This project is developed and build with Visual Studio 2019 Preview 1.1 and .net core 3.0 preview 1.
