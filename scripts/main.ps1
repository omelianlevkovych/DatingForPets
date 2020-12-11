$env = Read-Host 'Do you want to use local[1] or external[2] environment?'

if ( $env -eq 1 )
{
    .\menu.ps1
    exit
}
elseif ( $env -eq 2 ) {
    $url = Read-Host 'Please input URL of external environment (e.g: https://127.0.0.1:5001)'
    .\menu.ps1 -server $url
    exit
}
else {
    Write-Output "Plese input 1 or 2"
    .\main.ps1
    exit
}