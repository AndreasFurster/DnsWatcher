# DnsWatcher
Simple application that watches dns hostnames and notifies if anything changes. 

## Usage
Download exe from releases or build the solution yourself (executable found in bin folder).

`DnsWatcher.exe example.com [example2.com ...]`

## Example

    .\DnsWatcher.exe google.com bing.com
    12/11/2018 5:23:14 PM | google.com --> 172.217.17.110
    12/11/2018 5:23:14 PM | bing.com   --> 13.107.21.200
    12/11/2018 5:23:17 PM | google.com --> 172.217.17.110
    12/11/2018 5:23:17 PM | bing.com   --> 13.107.21.200
    12/11/2018 5:23:20 PM | google.com --> 172.217.17.110
    12/11/2018 5:23:20 PM | bing.com   --> 13.107.21.200
    12/11/2018 5:23:23 PM | google.com --> 172.217.17.110
    12/11/2018 5:23:23 PM | bing.com   --> 13.107.21.200
    12/11/2018 5:23:26 PM | google.com --> 172.217.17.110
    12/11/2018 5:23:26 PM | bing.com   --> 13.107.21.200

## OS limitiation 
Because an MessageBox is used this project can only run on Windows... Should be easy to change though.

## Build instructions
This project is developed and build with Visual Studio 2019 Preview 1.1 and .net core 3.0 preview 1.
