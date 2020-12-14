# Requires PSVersion 5.1.18362.1171
. ".\auth.ps1"
. ".\getpet.ps1"
. ".\menu.ps1"

Set-StrictMode -version 1.0

# Ignore self-signed certificates
add-type @"
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    public class TrustAllCertsPolicy : ICertificatePolicy {
        public bool CheckValidationResult(
            ServicePoint srvPoint, X509Certificate certificate,
            WebRequest request, int certificateProblem) {
            return true;
        }
    }
"@
[System.Net.ServicePointManager]::CertificatePolicy = New-Object TrustAllCertsPolicy

$url = "https://localhost:5001"
while($true)
{
    $env = Read-Host 'Do you want to use local[1] or external[2] environment?'
    if ( $env -eq 1 )
    {
        getMenu($url)
        exit
    }
    elseif ( $env -eq 2 ) {
        $url = Read-Host 'Please input URL of external environment (e.g: https://127.0.0.1:5001)'
        getMenu($url)
        exit
    }
    else {
        Write-Output 'Please input 1 or 2'
    }
}