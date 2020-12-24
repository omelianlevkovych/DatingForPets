# Requires PSVersion 5.1.18362.1171
. '.\auth.ps1'
. '.\getpet.ps1'
. '.\menu.ps1'

Set-StrictMode -version 3.0
Set-Variable -Name 'accessToken' -Scope global -Value $null

while($true)
{
    $env = Read-Host 'Do you want to use local[1] or external[2] environment?'
    if ( $env -eq 1 )
    {
        $urlDomain = 'https://localhost:5001'
        GetMenu($urlDomain)
        exit
    }
    elseif ( $env -eq 2 ) {
        $urlDomain = Read-Host 'Please input URL of external environment (e.g: https://127.0.0.1:5001)'
        GetMenu($urlDomain)
        exit
    }
    else {
        Write-Output 'Please input 1 or 2'
    }
}