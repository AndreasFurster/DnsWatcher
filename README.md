# DnsWatcher
Simple application that watches dns hostnames and notifies if anything changes. 

## Usage
`DnsWatcher.exe example.com [example2.com ...]`
### Download
Download zip from releases and extract to a prefered location. Optionally add DnsWatcher.exe to envoirement path.

### Build
This project is developed and build with Visual Studio 2019 Preview 1.1 and .net core 3.0 preview 1.

## Example output

    .\DnsWatcher.exe google.com bing.com
    
    12/11/2018 5:23:23 PM | google.com --> 172.217.17.110
    12/11/2018 5:30:22 PM | bing.com   --> 204.79.197.200
